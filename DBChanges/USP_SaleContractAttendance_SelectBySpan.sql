USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_SaleContractAttendance_SelectBySpan]    Script Date: 1/29/2023 8:04:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored procedure that will select all rows from the table 'USP_AccCentreOpeningBalance_SelectAll '
-- Gets: @iStartRow int 
-- Gets: @iEndRow int 
-- Gets: @sSortBy varchar(200) 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_SaleContractAttendance_SelectBySpan]
	@iSaleContractMasterID BIGINT,
	@iSaleContractBillingSpanID bigint,
	@iErrorCode int OUTPUT
AS
BEGIN
declare @_firstDayOfMonth datetime,@_lastDayOfMonth datetime,@_TotalWeeklyOff int;
	BEGIN TRY
		BEGIN 

		select
			@_firstDayOfMonth = StartDate,
			@_lastDayOfMonth = EndDate
		from		
			SaleContractBillingSpan
		where ID = @iSaleContractBillingSpanID

		;with mycte as
		(
			select  @_firstDayOfMonth DateValue
			union all
			select DateValue + 1 from mycte
			where DateValue + 1 <= @_lastDayOfMonth
		)

		SELECT @_TotalWeeklyOff = (datediff(day,@_firstDayOfMonth,@_lastDayOfMonth) +1) - COUNT(*) FROM mycte
		WHERE DATENAME(weekday,dateValue)='Sunday'
		
		;with cteEmployeeList as (
			select
				ID as SaleContractManPowerAssignID,
				SaleContractEmployeeMasterID,
				IsActive,
				ROW_NUMBER() OVER(PARTITION BY SaleContractRequirementDetailsID,SaleContractEmployeeMasterID ORDER BY ID DESC) AS LatestID
			from
				SaleContractManPowerAssign
		),cteEmployeeAssigned as (
			select
				C.SaleContractEmployeeMasterID,
				C.IsSalaryDaysCountFix,
				C.IsSalaryDaysOnWeeklyOff,
				C.IsBillingDaysOnWeeklyOff,
				C.SaleContractRequirementDetailsID,
				C.IsBillingDaysFixed,
				C.FixedDays,
				C.FixedBillingDays,
				E.StartDate,
				E.EndDate,
				E.ID as SaleContractBillingSpanID,
				A.ID as SaleContractManPowerItemID,
				C.ID as SaleContractManPowerAssignID,
				H.Description as SaleContractManPowerItemName
			from
				SaleContractMaster F
				inner join SaleContractRequirementDetails D on (F.ID = D.SaleContractMasterID
																and F.ID = @iSaleContractMasterID
																and D.SaleContractRequiredTypeID = 1 and ISNULL(F.ApprovalStatus,2) = 2)
				inner join SaleContractManPowerItem A on (D.SaleContractManPowerItemID = A.ID
														 and A.IsRevised = 0 and ISNULL(A.ApprovalStatus,2) = 2)
				inner join EmployeeDesignationMaster H on (A.DesignationMasterID = H.ID)
				inner join SaleContractManPowerAssign C on (D.ID = C.SaleContractRequirementDetailsID
															and C.SaleContractManPowerItemID = A.ID)
				inner join cteEmployeeList G on (C.ID = G.SaleContractManPowerAssignID
												and G.LatestID = 1)
				inner join SaleContractEmployeeMaster B on (C.SaleContractEmployeeMasterID = B.ID)
			
				inner join SaleContractBillingSpan E on (E.SaleContractMasterID = D.SaleContractMasterID
															and E.ID = @iSaleContractBillingSpanID
															and ((C.FromDate between E.StartDate and E.EndDate or C.UptoDate between E.StartDate and E.EndDate)
																	or (C.FromDate < E.StartDate and C.UptoDate > E.EndDate))
															and (B.IsLeft = 0 or (B.IsLeft = 1 and (B.LastLeftDate between E.StartDate and E.EndDate or B.LastLeftDate > E.EndDate))))
			group by C.SaleContractEmployeeMasterID,
				C.IsSalaryDaysCountFix,
				C.IsSalaryDaysOnWeeklyOff,
				C.IsBillingDaysOnWeeklyOff,
				C.SaleContractRequirementDetailsID,
				C.IsBillingDaysFixed,
				C.FixedDays,
				C.FixedBillingDays,
				E.StartDate,
				E.EndDate,
				E.ID,
				A.ID,
				C.ID,
				H.Description
		)

		select 
			A.ID,
			A.TotalAttendance,
			C.SaleContractEmployeeMasterID,
			C.SaleContractManPowerAssignID,
			CONCAT(B.FirstName,' ',B.MiddleName,' ',B.LastName,' - ',B.EmployeeCode,' - ',C.SaleContractManPowerItemName) as SaleContractEmployeeMasterName,
			C.SaleContractManPowerItemID,
			case when A.TotalDays is null then case when C.IsSalaryDaysCountFix = 1 then C.FixedDays 
													--when C.IsSalaryDaysOnWeeklyOff = 1 then @_TotalWeeklyOff
													else DATEDIFF(DAY,C.StartDate,C.EndDate) + 1 end else A.TotalDays end as TotalDays,
			A.OvertimeHours,
			C.IsSalaryDaysOnWeeklyOff,
			C.IsBillingDaysOnWeeklyOff,
			case when A.TotalBillingDays is null then case when C.IsBillingDaysFixed = 1 then C.FixedBillingDays 
													--when C.IsSalaryDaysOnWeeklyOff = 1 then @_TotalWeeklyOff
													else DATEDIFF(DAY,C.StartDate,C.EndDate) + 1 end else A.TotalBillingDays end as TotalBillingDays,
			isnull(A.TotalWeeklyOffDays,0) as TotalWeeklyOffDays,
			case when G.ID is not null then case when isnull(A.TotalOverTimeSalaryDays,0) = 0 then case when G.IsOverTimeDaysFix = 1 then G.OTFixedDays
													else DATEDIFF(DAY,C.StartDate,C.EndDate) + 1 end else A.TotalOverTimeSalaryDays end else 0 end as TotalOverTimeSalaryDays,
			case when G.ID is not null then case when isnull(A.TotalOverTimeBillingDays,0) = 0 then case when G.IsOverTimeBillingDaysFix = 1 then G.OTBillingFixedDays
													else DATEDIFF(DAY,C.StartDate,C.EndDate) + 1 end else A.TotalOverTimeBillingDays end else 0 end as TotalOverTimeBillingDays,
			G.IsOTDaysOnTotalOff,
			G.IsOTBillingDaysOnTotalOff,
			ISNULL(A.TotalWeeklyOffBillingDays,0) as TotalWeeklyOffBillingDays,
			C.IsSalaryDaysCountFix,
			C.IsBillingDaysFixed,
			G.IsOverTimeDaysFix,
			G.IsOverTimeBillingDaysFix,
			I.Description as SalaryForManPowerItemName,
			ISNULL(A.ApprovalStatus,2) as ApprovedStatus,
			'12~A' PreviousAttendenceDetails
		from 
			SaleContractMaster F
			inner join SaleContractRequirementDetails D on (F.ID = D.SaleContractMasterID
															and F.ID = @iSaleContractMasterID
															and D.SaleContractRequiredTypeID = 1)
			inner join cteEmployeeAssigned C on (D.ID = C.SaleContractRequirementDetailsID)
			inner join SaleContractEmployeeMaster B on (C.SaleContractEmployeeMasterID = B.ID)
			
			left outer join SaleContractOvertime G on (C.SaleContractManPowerItemID = G.SaleContractManPowerItemID
														and G.SaleContractMasterID = D.SaleContractMasterID)
			left outer join SaleContractAttendance A on ( A.SaleContractBillingSpanID = C.SaleContractBillingSpanID
														and A.SaleContractMasterID = D.SaleContractMasterID
														and C.SaleContractEmployeeMasterID = A.SaleContractEmployeeMasterID
														and C.SaleContractManPowerAssignID = A.SaleContractManPowerAssignID)
			left outer join SaleContractManPowerItem H on (A.SalaryForManPowerItemID = H.ID)
			left outer join EmployeeDesignationMaster I on (H.DesignationMasterID = I.ID)
		
	END; 
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End





















