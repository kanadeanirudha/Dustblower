USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_ServiceInvoiceMasterAndDetailsMaster_SelectAll]    Script Date: 12/3/2022 7:13:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored procedure that will select all rows from the table '[USP_ServiceInvoiceMasterAndDetailsMaster_SelectAll]'
-- Gets: @iStartRow int 
-- Gets: @iEndRow int 
-- Gets: @sSortBy varchar(200) 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_ServiceInvoiceMasterAndDetailsMaster_SelectAll]
	@iStartRow			 INT ,
	@iEndRow			 INT,
	@sSortBy			 varchar(200),
	@nsSearchBy			 nvarchar(200),
	@sSortDirection		 varchar (10),
	@iAdminRoleId         INT,
	@nsMonthName nvarchar(25),
	@nsMonthYear nvarchar(25),
	@iErrorCode			 int OUTPUT
AS

BEGIN
DECLARE @SQLString nvarchar(4000);
		
	BEGIN TRY
		BEGIN 

		CREATE TABLE 
		  #TmpCentreListForEstablishmentManager 
		  (
		  	RightName				nvarchar(30)	,
			AdminRoleRightTypeID	tinyint	,
			CentreCode 				nvarchar(15)		,		
			CentreName				nvarchar(100),
			ScopeIdentity			varchar(10)		,
		   );	
		   INSERT INTO	#TmpCentreListForEstablishmentManager 
		 (
			RightName
			,AdminRoleRightTypeID
			,CentreCode
			,CentreName
			,ScopeIdentity
		) 
		select
			B.RightName,
			A.AdminRoleRightTypeID,
			A.CentreCode,
			D.CentreName,
			'Centre' as ScopeIdentity
		from
			AdminRoleCentreRights A
			inner join AdminRoleRightType B on (A.AdminRoleRightTypeID = B.ID
												and A.AdminRoleMasterID=@iAdminRoleId
												and B.IsDeleted = 0
												and A.IsDeleted = 0
												and A.IsActive = 1
												and B.RightName='Sales Manager'
												)
			inner join AdminRoleMaster C on (A.AdminRoleMasterID = C.ID
											--and C.MonitoringLevel = 'Self'
											and C.IsActive = 1)
			inner join OrganisationStudyCentreMaster D on (A.CentreCode = D.CentreCode)
		Union

	     Select 
			 D.RightName
			,B.AdminRoleRightTypeID
			,B.CentreCode
			,C.CentreName
			,E.EntityName as ScopeIdentity 
		from
			AdminRoleSpecialRightMaster A
		Inner join AdminRoleSpecialRightDetails B on ( 
										 A.AdminRoleMasterID=@iAdminRoleId
										 and a.ID=B.AdminRoleSpecialRightMasterID										 
										 and A.IsDeleted=B.IsDeleted
										 and A.IsDeleted=0	
										 and B.IsActive=1														
										 )	
		Inner join AdminRoleRightType D on (B.AdminRoleRightTypeID = D.ID
											and D.RightName='Sales Manager')
		Inner join OrganisationStudyCentreMaster C on (C.CentreCode=B.CentreCode
											  )
		inner join AdminRoleEntity E on (B.AdminRoleEntityID=E.ID)

		-- SELECT First and Last rows as per Parameter set from the table.'; 
		
		Begin
		--SET @SQLString=concat(N'
		
	CREATE TABLE 
		  #TmpDeliveryMasterList
		  (
		  	SalesOrderMasterID		 int	,
			SalesOrderNumber nvarchar(50),	
			DeliveryNumber nvarchar(50),
			SalesInvoiceID int,
			CustomerMasterID int,
			Customername nvarchar(100),
			CreatedDate datetime,
			CustomerInvoiceNumber nvarchar(50),
			SalesQuotationMasterID int,
			CustomerBranchMasterID int,
			GeneralUnitsID smallint	,
			Isinvoiced bit,
			ApprovalStatus tinyint,
			CancelApprovalStatus tinyint,
			GSTEInvoiceMasterId int,
			IsCancelledEInvoice bit,
			TotalRecords int
		   );	
		   ;With CteLocation as(	
	                   Select 
	                       ID as InventoryLocationMasterID ,LocationName 
	                   From InventoryLocationMaster A Inner join #TmpCentreListForEstablishmentManager B on (A.CentreCode=B.CentreCode)
	                        )
		  
			,cteDM as 
						(
							Select 
								B.SalesOrderDeliveryMasterID,
								A.CustomerInvoiceNumber,
								B.SalesInvoiceMasterID,
								A.CreatedDate,
								A.CustomerMasterID,
								A.CustomerBranchMasterID,
								A.GeneralUnitsID,
								A.StorageLocationID,
								ISNULL(A.ApprovalStatus,2) as ApprovalStatus,
								ISNULL(A.CancelApprovalStatus,2) as CancelApprovalStatus,
								C.ID as GSTEInvoiceMasterId,
								C.IsCancelledEInvoice
						  from 
								SalesInvoiceMaster A inner join SalesInvoiceDetail B on (A.ID=B.SalesInvoiceMasterID and A.InvoiceType=2 and A.IsDeleted = 0)
								left join GSTEInvoiceMaster C on C.SalesInvoiceMasterID = A.ID
								--where MONTH(CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC','Asia/Kolkata') ,106)) = @nsMonthName 
								--	  and YEAR(Convert(varchar(15),dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC','Asia/Kolkata') ,106)) = @nsMonthYear
								group by B.SalesOrderDeliveryMasterID,A.CustomerInvoiceNumber,B.SalesInvoiceMasterID,A.CreatedDate,A.CustomerMasterID,
								A.CustomerBranchMasterID,A.GeneralUnitsID, A.StorageLocationID,A.ApprovalStatus,A.CancelApprovalStatus,
								C.ID,C.IsCancelledEInvoice
								
					)
					
				    ,cte2 as(
						     Select
							     null as SalesOrderMasterID
							    ,null as SalesOrderNumber
							    ,A.CustomerMasterID
							    ,Null as SalesOrderDate
							    ,case when B.CustomerType = 1 then concat(B.FirstName,' ',B.MiddleName,' ',B.LastName) else B.CompanyName end as CustomerName
							    ,null as SalesQuotationMasterID
								,A.CustomerBranchMasterID
								,A.GeneralUnitsID
							    ,0 as SalesOrderDeliveryMasterID
							    ,null as DeliveryNumber	
							    ,null as DeliveryTransDate	
							    ,A.SalesInvoiceMasterID as  SalesInvoiceID
							    ,A.CreatedDate
							    ,A.CustomerInvoiceNumber
							    ,A.StorageLocationID as  IssueFromLocationID	
								,1 as Isinvoiced
								,A.ApprovalStatus
								,A.CancelApprovalStatus		
								,A.GSTEInvoiceMasterId
								,A.IsCancelledEInvoice
						   From
							    cteDM A inner join CustomerMaster B on (A.CustomerMasterID=B.ID)
							)
				   ,Cte4 AS (
						SELECT
							A.SalesOrderMasterID,
							A.SalesOrderNumber,							
							STUFF((
							SELECT ', ' + cte2.DeliveryNumber
							FROM 
								cte2
							WHERE (A.SalesOrderNumber = cte2.SalesOrderNumber
									and A.Isinvoiced=cte2.Isinvoiced and Isnull(A.CustomerInvoiceNumber,0)=Isnull(cte2.CustomerInvoiceNumber,0))
							FOR XML PATH (''))
							,1,2,'') AS DeliveryNumber,
							A.SalesInvoiceID,
							A.CustomerMasterID,
							A.Customername,
							A.CreatedDate 
							,A.CustomerInvoiceNumber
							,A.SalesQuotationMasterID
							,A.IssueFromLocationID
							,A.CustomerBranchMasterID
							,A.GeneralUnitsID
							,A.Isinvoiced
							,A.ApprovalStatus
							,A.CancelApprovalStatus
							,A.GSTEInvoiceMasterId
							,A.IsCancelledEInvoice
						FROM 
							cte2 A
						group by A.SalesOrderNumber,A.SalesOrderMasterID,A.SalesInvoiceID,A.CustomerInvoiceNumber,A.CustomerMasterID,A.Customername,A.SalesQuotationMasterID,A.CreatedDate,
						A.IssueFromLocationID,A.CustomerBranchMasterID,A.GeneralUnitsID,A.Isinvoiced,A.ApprovalStatus,A.CancelApprovalStatus
						,A.GSTEInvoiceMasterId
						,A.IsCancelledEInvoice
							)
							--select * from Cte4
							

		   INSERT INTO	#TmpDeliveryMasterList 
		 (
			 SalesOrderMasterID,
			 SalesOrderNumber,	
			 DeliveryNumber,
			 SalesInvoiceID,
			 CustomerMasterID,
			 Customername,
			 CreatedDate,
			 CustomerInvoiceNumber,
			 SalesQuotationMasterID,
			 CustomerBranchMasterID,
			 GeneralUnitsID	,
			 Isinvoiced,
			 ApprovalStatus,
			 CancelApprovalStatus,
			 GSTEInvoiceMasterId,
			 IsCancelledEInvoice,
			 TotalRecords
		) 
		Select
					   A.SalesOrderMasterID,
					   A.SalesOrderNumber,	
					   A.DeliveryNumber,
					   A.SalesInvoiceID,
					   A.CustomerMasterID,
					   A.Customername,
					   A.CreatedDate,
					   A.CustomerInvoiceNumber,
					   A.SalesQuotationMasterID,
					   A.CustomerBranchMasterID,
					   A.GeneralUnitsID	,
					   A.Isinvoiced,
					   A.ApprovalStatus,
					   A.CancelApprovalStatus,
					    A.GSTEInvoiceMasterId,
					    A.IsCancelledEInvoice,
					   COUNT(*) OVER() AS TotalRecords
					From Cte4 A inner join  CteLocation B ON (A.IssueFromLocationID=B.InventoryLocationMasterID)

					SET @SQLString=concat(N'
					select 
						A.SalesOrderMasterID,
						A.SalesOrderNumber,	
						A.DeliveryNumber,
						A.SalesInvoiceID,
						A.CustomerMasterID,
						A.Customername,
						A.CreatedDate,
						A.CustomerInvoiceNumber,
						A.SalesQuotationMasterID,
						A.CustomerBranchMasterID,
						A.GeneralUnitsID	,
						A.Isinvoiced,
						A.ApprovalStatus,
						A.CancelApprovalStatus,
						A.GSTEInvoiceMasterId,
					    A.IsCancelledEInvoice,
						A.TotalRecords
					from #TmpDeliveryMasterList A

		','');
		end
		IF @nsSearchBy is not null and @nsSearchBy <>''
		Begin
			SET @SQLString=concat (@SQLString,' Where ',(@nsSearchBy),' ');
		End

		SET @SQLString=concat (@SQLString,' order by  ',@sSortBy ,' ', @sSortDirection,'
		OFFSET ',@iStartRow,' ROWS
		FETCH NEXT ',@iEndRow,' ROWS ONLY
		');
	--	select @SQLString;
		EXECUTE sp_executesql @SQLString
	END; 
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
			--SELECT ERROR_MESSAGE();
	END CATCH
End


















