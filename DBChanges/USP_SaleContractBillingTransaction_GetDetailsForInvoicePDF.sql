USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_SaleContractBillingTransaction_GetDetailsForInvoicePDF]    Script Date: 12/7/2022 2:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table '[[USP_SaleContractBillingTransaction_GetDetailsForInvoicePDF]'
-- based on the Primary Key.
-- Gets: @iAdminRoleId int,
-- Return: @iErrorCode int 
 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_SaleContractBillingTransaction_GetDetailsForInvoicePDF]
	@iSaleContractBillingSpanID bigint,	
	@iSaleContractMasterID bigint,
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY

		Declare @_iCustomerMasterID int,@_iCustomerBranchMasterID int,@_bIsOtherFlag bit,@_nsCentreCode nvarchar(30),@iGeneralUinitsID int,@_bIsTaxExempted bit,@_tiReasonForExemption tinyint;

		Select 
			@_iCustomerMasterID			= A.CustomerMasterID
			,@_iCustomerBranchMasterID   = A.CustomerBranchMasterID
			,@_nsCentreCode			= A.CentreCode
		From SaleContractMaster A
		where ID=@iSaleContractMasterID

		Select @_bIsOtherFlag=dbo.fGetIsOtherfromCustomerMasterByCentreCode (@_iCustomerMasterID,@_iCustomerBranchMasterID,@_nsCentreCode)
		Select 
			@iGeneralUinitsID= A.ID 
		from 
			Generalunits A 
			inner join SaleContractMaster B on (A.CentreCode=B.CentreCode 
												and B.ID=@iSaleContractMasterID 
												and A.IsDeleted=0 
												and B.IsDeleted=0)
			inner join SaleContractBillingSpan C on (B.ID = C.SaleContractMasterID
													and C.ID = @iSaleContractBillingSpanID)
			inner join SalesInvoiceMaster D on (C.SalesInvoiceMasterID = D.ID)
			inner join GeneralUnitsStorageLocation E on (D.StorageLocationID = E.InventoryLocationMasterID
															and E.GeneralUnitsID = A.ID)
 
		Select 
			@_bIsTaxExempted = case when A.CustomerType= 1 then A.IsTaxExempted else B.IsTaxExempted end
			,@_tiReasonForExemption = case when A.CustomerType= 1 then A.ReasonForExemption else B.ReasonForExemption end
		From CustomerMaster A
			 left outer join CustomerBranchMaster B on (A.ID = B.CustomerMasterID
														and (B.ID = @_iCustomerBranchMasterID or @_iCustomerBranchMasterID = 0))
		where A.ID = @_iCustomerMasterID

		CREATE TABLE 
		  #TmpSaleContractBillingTransaction 
		  (
		  	ContractNumber				nvarchar(35),
			BillingType					tinyint,
			CustomerMasterName			nvarchar(100),
			CustomerBranchMasterName	nvarchar(100),
			SaleContractBillingSpanName nvarchar(50),
			ItemNumber					int,
			Rate						decimal(10,3),
			Quantity					decimal(18,3),
			FixedQuantity				decimal(18,3),
			OriginalQuantity			decimal(18,3),
			ShortExtraRate				money,
			TaxableAmount				money,
			TaxAmount					money,
			NetAmount					money,
			TotalBillAmount				money,
			RoundOffAmount				money,
			UOMCode						nvarchar(30),
			ItemDescription				nvarchar(150),
			ItemAssignedPeriod			nvarchar(50),
			IsOtherState				bit,
			GeneralTaxGroupMasterName	nvarchar(30),
			TaxRate						decimal(8,3),
			HSNCode						nvarchar(50),
			SalesInvoiceMasterID		int,
			CustomerInvoiceNumber		nvarchar(35),
			LocationID					int,
			LocationName				nvarchar(50),
			CustomerMasterID			int,
			CustomerBranchMasterID		int,
			InvoiceTransactionDate		nvarchar(35),
			SaleContractRequiredTypeID	tinyint,
			IsServiceChargesAppliedToServiceItem bit,
			GeneralTaxGroupMasterID tinyint,
			TaxExemptionRemark nvarchar(250),
			IsDisplayPurchaseDetails bit,
			PurchaseOrderNumber nvarchar(35),
			PurchaseOrderDate nvarchar(35),
			DateTimeOfSupply nvarchar(35),
			IsCanceled bit,
			QrCodeImage varchar(max)
		   );	

		   ;With cteTotalTax as(
			select 
				B.ID
				,B.TaxGroupName
				,Sum(D.TaxRate) as TaxRate
			from
				GeneralTaxGroupMaster B 
				inner join GeneralTaxGroupMasterDetails C on (B.ID = C.GenTaxGroupMasterID)
				inner join GeneralTaxMaster D on (C.GenTaxMasterID = D.ID 
													and D.IsOtherState = @_bIsOtherFlag)
			group by D.IsOtherState,B.TaxGroupName,B.ID
		),FixItemQuantity as (
			select
				B.TotalDays,
				B.TotalAttendance,
				B.SaleContractFixItemID,
				B.TotalBillingDays,
				E.Description as Name,
				F.ChargeAmount
			from
				SaleContractMaster A
				inner join SaleContractFixAttendance B on (A.ID = B.SaleContractMasterID
														and A.ID = @iSaleContractMasterID
														and B.SaleContractBillingSpanID = @iSaleContractBillingSpanID)
				inner join SaleContractTermDetails T on (A.ID = T.SaleContractMasterID)
				inner join SaleContractManPowerItem D on (B.SaleContractFixItemID = D.ID
															and D.IsRevised = 0)
				inner join EmployeeDesignationMaster E on (D.DesignationMasterID = E.ID)
				inner join SaleContractRequirementDetails C on (A.ID = C.SaleContractMasterID
																and C.SaleContractRequiredTypeID = 5
																and C.SaleContractManPowerItemID = B.SaleContractFixItemID)
				left outer join SaleContractServiceChargeOnManPowerItem F on (T.ID = F.SaleContractTermDetailsID
																				and F.SaleContractManPowerItemID = B.SaleContractFixItemID)
			--group by B.SaleContractJobWorkItemID

		),ManPowerQuantity as (
			select
				C.Rate * C.Quantity as ManPowerTotalRate,
				B.SaleContractManPowerItemID,
				F.ChargeAmount * C.Quantity as ChargeAmount
			from
				SaleContractMaster A
				inner join SaleContractRequirementDetails C on (A.ID = C.SaleContractMasterID
																and A.ID = @iSaleContractMasterID
																and C.SaleContractRequiredTypeID = 1)
				inner join SaleContractTermDetails E on (A.ID = E.SaleContractMasterID)
				inner join SaleContractManPowerAssign D on (C.ID = D.SaleContractRequirementDetailsID
															and D.SaleContractManPowerItemID = C.SaleContractManPowerItemID)
				inner join SaleContractAttendance B on (A.ID = B.SaleContractMasterID
														and B.SaleContractBillingSpanID = @iSaleContractBillingSpanID
														and D.SaleContractManPowerItemID = B.SaleContractManPowerItemID
														and B.SaleContractEmployeeMasterID = D.SaleContractEmployeeMasterID
														and D.ID = B.SaleContractManPowerAssignID
														)
				inner join SaleContractManPowerItem G on (D.SaleContractManPowerItemID = G.ID
															and G.IsRevised = 0)
				left outer join SaleContractOvertime OT on (A.ID = OT.SaleContractMasterID
															and OT.SaleContractManPowerItemID = G.ID)
				left outer join SaleContractServiceChargeOnManPowerItem F on (E.ID = F.SaleContractTermDetailsID
																				and F.SaleContractManPowerItemID = B.SaleContractManPowerItemID)
			group by B.SaleContractManPowerItemID,C.ID,E.IsInclusiveServiceCharges,E.ServiceChargesPercentage,E.ServiceChargesDependOn,E.ServiceChargesCalculateOn,OT.OverTimeDisplayFormat,C.Rate,C.Quantity,F.ChargeAmount
		)

		   INSERT INTO	#TmpSaleContractBillingTransaction 
		 (
			
			 ContractNumber	
			,BillingType			
			,CustomerMasterName			
			,CustomerBranchMasterName	
			,SaleContractBillingSpanName
			,ItemNumber					
			,Rate						
			,Quantity				
			,FixedQuantity
			,OriginalQuantity	
			,ShortExtraRate
			,TaxableAmount				
			,TaxAmount					
			,NetAmount					
			,TotalBillAmount				
			,RoundOffAmount				
			,UOMCode						
			,ItemDescription				
			,ItemAssignedPeriod			
			,IsOtherState				
			,GeneralTaxGroupMasterName	
			,TaxRate
			,HSNCode						
			,SalesInvoiceMasterID		
			,CustomerInvoiceNumber		
			,LocationID					
			,LocationName	
			,CustomerMasterID			
			,CustomerBranchMasterID	
			,InvoiceTransactionDate 
			,SaleContractRequiredTypeID
			,IsServiceChargesAppliedToServiceItem
			,GeneralTaxGroupMasterID
			,TaxExemptionRemark
			,IsDisplayPurchaseDetails
			,PurchaseOrderNumber
			,PurchaseOrderDate
			,DateTimeOfSupply
			,IsCanceled
			,QrCodeImage
		) 
		select
			A.ContractNumber,
			A.BillingType,
			case when C.CustomerType = 1 then CONCAT(C.FirstName,' ',C.MiddleName,' ',C.LastName) else C.CompanyName end as CustomerMasterName,
			D.BranchName as CustomerBranchMasterName,
			CONCAT(CONVERT(varchar(30),B.StartDate,106),' - ',CONVERT(varchar(30),B.EndDate,106)) as SaleContractBillingSpanName,
			E.ItemNumber,
			case when F.SaleContractRequiredTypeID = 6 then T.ServiceChargesPercentage else E.Rate end as Rate,
			E.Quantity,
			0 as FixedQuantity,
			0 as OriginalQuantity,
			0 as ShortExtraRate,
			E.TaxableAmount,
			E.TaxAmount,
			E.NetAmount,
			B.TotalBillAmount,
			B.RoundOffAmount,
			case when ID.DisplayUOMCode is not null then ID.DisplayUOMCode else	ID.SaleUomCode end as UOMCode,
			ID.BillingDisplayName as ItemDescription,
			case when F.SaleContractRequiredTypeID = 2 then CONCAT('from ',CONVERT(varchar(30), H.FromDate, 106),' to ',CONVERT(varchar(30), H.UptoDate, 106)) 
				when F.SaleContractRequiredTypeID = 1 then CONCAT('from ',CONVERT(varchar(30), B.StartDate, 106),' to ',CONVERT(varchar(30), B.EndDate, 106))
				else CONCAT('from ',CONVERT(varchar(30), B.StartDate, 106),' to ',CONVERT(varchar(30), B.EndDate, 106)) end as ItemAssignedPeriod,
			@_bIsOtherFlag as IsOtherState,
			Concat(L.TaxGroupName,' %') as GeneralTaxGroupMasterName,
			L.TaxRate,
			K.HSNCode,
			B.SalesInvoiceMasterID,
			IM.CustomerInvoiceNumber,
			IM.StorageLocationID as LocationID,
			SL.LocationName,
			C.ID as CustomerMasterID,
			D.ID as CustomerBranchMasterID,
			Convert( nvarchar(35),IM.TransactionDate,106) as InvoiceTransactionDate,
			F.SaleContractRequiredTypeID,
			T.IsServiceChargesAppliedToServiceItem,
			ID.GenTaxGroupMasterID,
			IM.TaxExemptionRemark,
			A.IsDisplayPurchaseDetails,
			A.PurchaseOrderNumber,
			CONVERT(varchar(30),A.PurchaseOrderDate,106) as PurchaseOrderDate,
			CONVERT(varchar(30),B.EndDate,106) as DateTimeOfSupply,
			IM.IsCanceled,
			gsteim.QrCodeImage
		from
			SaleContractMaster A
			inner join SaleContractBillingSpan B on (A.ID = B.SaleContractMasterID
													and A.ID = @iSaleContractMasterID
													and B.ID = @iSaleContractBillingSpanID
													and B.IsBillGenerated = 1
													and A.BillingType = 1)
			inner join SaleContractTermDetails T on (A.ID = T.SaleContractMasterID)
			inner join SalesInvoiceMaster IM on (B.SalesInvoiceMasterID = IM.ID)
			inner join SalesInvoiceDetail ID on (IM.ID = ID.SalesInvoiceMasterID)
			inner join CustomerMaster C on (A.CustomerMasterID = C.ID)
			inner join InventoryLocationMaster SL on (IM.StorageLocationID = SL.ID)
			left outer join CustomerBranchMaster D on (A.CustomerBranchMasterID = D.ID)
			inner join SaleContractBillingTransaction E on (E.SaleContractBillingSpanID = B.ID
															and E.SaleContractMasterID = A.ID
															and ID.ItemNumber = E.ItemNumber
															and ISNULL(E.IsCanceled,0) = 0)
			inner join SaleContractRequirementDetails F on (E.SaleContractRequirementDetailsID = F.ID
															and ID.VariationMasterID = F.ID)
			inner join GeneralItemMaster K on (E.ItemNumber = K.ItemNumber)
			left outer join cteTotalTax L on (E.GeneralTaxGroupMasterID = L.ID)
			left outer join SaleContractMachineAssign H on (H.SaleContractRequirementDetailsID = F.ID
																	and H.SaleContractMachineMasterID = F.SaleContractMachineMasterID)
			left outer join SaleContractMachineMaster G on (F.SaleContractMachineMasterID = G.ID
															and F.ItemNumber = G.ItemNumber
															and F.SaleContractRequiredTypeID = 2)
			left outer join SaleContractManPowerItem I on (F.SaleContractManPowerItemID = I.ID	
															and F.SaleContractRequiredTypeID = 1)
			left outer join EmployeeDesignationMaster J on (I.DesignationMasterID = J.ID)
			left outer join SaleContractManPowerItem M on (F.SaleContractManPowerItemID = M.ID	
															and F.SaleContractRequiredTypeID = 7)
			left outer join EmployeeDesignationMaster N on (M.DesignationMasterID = N.ID)
			left outer join SaleContractManPowerItem O on (F.SaleContractManPowerItemID = O.ID	
															and F.SaleContractRequiredTypeID = 9)
			left outer join EmployeeDesignationMaster P on (O.DesignationMasterID = P.ID)
			left join GSTEInvoiceMaster gsteim on gsteim.SalesInvoiceMasterID = IM.ID
		
		union

		select
			A.ContractNumber,
			A.BillingType,
			case when C.CustomerType = 1 then CONCAT(C.FirstName,' ',C.MiddleName,' ',C.LastName) else C.CompanyName end as CustomerMasterName,
			D.BranchName as CustomerBranchMasterName,
			CONCAT(CONVERT(varchar(30),B.StartDate,106),' - ',CONVERT(varchar(30),B.EndDate,106)) as SaleContractBillingSpanName,
			E.ItemNumber,
			case when F.SaleContractRequiredTypeID = 6 then T.ServiceChargesPercentage else E.Rate end as Rate,
			E.Quantity,
			0 as FixedQuantity,
			0 as OriginalQuantity,
			0 as ShortExtraRate,
			E.TaxableAmount,
			E.TaxAmount,
			E.NetAmount,
			B.TotalBillAmount,
			B.RoundOffAmount,
			case when ID.DisplayUOMCode is not null then ID.DisplayUOMCode else	ID.SaleUomCode end as UOMCode,
			ID.BillingDisplayName as ItemDescription,
			CONCAT('from ',CONVERT(varchar(30), B.StartDate, 106),' to ',CONVERT(varchar(30), B.EndDate, 106)) as ItemAssignedPeriod,
			@_bIsOtherFlag as IsOtherState,
			CONCAT(L.TaxGroupName,' %') as GeneralTaxGroupMasterName,
			L.TaxRate,
			K.HSNCode,
			B.SalesInvoiceMasterID,
			IM.CustomerInvoiceNumber,
			IM.StorageLocationID as LocationID,
			SL.LocationName,
			C.ID as CustomerMasterID,
			D.ID as CustomerBranchMasterID,
			Convert( nvarchar(35),IM.TransactionDate,106) as InvoiceTransactionDate,
			F.SaleContractRequiredTypeID,
			T.IsServiceChargesAppliedToServiceItem,
			ID.GenTaxGroupMasterID,
			IM.TaxExemptionRemark,
			A.IsDisplayPurchaseDetails,
			A.PurchaseOrderNumber,
			CONVERT(varchar(30),A.PurchaseOrderDate,106) as PurchaseOrderDate,
			CONVERT(varchar(30),B.EndDate,106) as DateTimeOfSupply,
			IM.IsCanceled,
			gsteim.QrCodeImage
		from
			SaleContractMaster A
			inner join SaleContractBillingSpan B on (A.ID = B.SaleContractMasterID
													and A.ID = @iSaleContractMasterID
													and B.ID = @iSaleContractBillingSpanID
													and B.IsBillGenerated = 1
													and A.BillingType = 3)
			inner join SaleContractTermDetails T on (A.ID = T.SaleContractMasterID)
			inner join SalesInvoiceMaster IM on (B.SalesInvoiceMasterID = IM.ID)
			inner join SalesInvoiceDetail ID on (IM.ID = ID.SalesInvoiceMasterID)
			inner join CustomerMaster C on (A.CustomerMasterID = C.ID)
			inner join InventoryLocationMaster SL on (IM.StorageLocationID = SL.ID)
			left outer join CustomerBranchMaster D on (A.CustomerBranchMasterID = D.ID)
			inner join SaleContractRequirementDetails F on (A.ID = F.SaleContractMasterID
															and F.SaleContractRequiredTypeID = 4
															and ID.VariationMasterID= F.ID)
			inner join SaleContractBillingTransaction E on (E.SaleContractBillingSpanID = B.ID
															and E.SaleContractMasterID = A.ID
															and ID.ItemNumber = E.ItemNumber
															and F.ID = E.SaleContractRequirementDetailsID
															and ISNULL(E.IsCanceled,0) = 0)
			inner join GeneralItemMaster K on (E.ItemNumber = K.ItemNumber)
			left outer join cteTotalTax L on (E.GeneralTaxGroupMasterID = L.ID)
			inner join SaleContractJobWorkItem M on (F.SaleContractJobWorkItemID = M.ID)
			inner join SaleContractJobWorkData N on (N.SaleContractJobWorkItemID = M.ID)
			left join GSTEInvoiceMaster gsteim on gsteim.SalesInvoiceMasterID = IM.ID
	  
		union

		select
			A.ContractNumber,
			A.BillingType,
			case when C.CustomerType = 1 then CONCAT(C.FirstName,' ',C.MiddleName,' ',C.LastName) else C.CompanyName end as CustomerMasterName,
			D.BranchName as CustomerBranchMasterName,
			CONCAT(CONVERT(varchar(30),B.StartDate,106),' - ',CONVERT(varchar(30),B.EndDate,106)) as SaleContractBillingSpanName,
			E.ItemNumber,
			case when F.SaleContractRequiredTypeID = 6 then T.ServiceChargesPercentage else E.Rate end as Rate,
			E.Quantity,
			N.TotalBillingDays as FixedQuantity,
			N.TotalAttendance as OriginalQuantity,
			case when A.ShortExtraPostingRateAccTo = 1 then (case when T.IsInclusiveServiceCharges = 1 and T.ServiceChargesDependOn = 1 and T.ServiceChargesPercentage > 0 
																  then (case when isnull(A.IsIncludeAllPostingForShortExtraRate,0) = 1 then F.Rate else MQ.ManPowerTotalRate end + (case when isnull(A.IsIncludeAllPostingForShortExtraRate,0) = 1 then F.Rate else MQ.ManPowerTotalRate end * T.ServiceChargesPercentage / 100)) 
																  when T.IsInclusiveServiceCharges = 1 and T.ServiceChargesDependOn = 2
																  then (case when isnull(A.IsIncludeAllPostingForShortExtraRate,0) = 1 then F.Rate else MQ.ManPowerTotalRate end + MQ.ChargeAmount) 
																  else case when isnull(A.IsIncludeAllPostingForShortExtraRate,0) = 1 then F.Rate else MQ.ManPowerTotalRate end end / JI.TotalBillingDays)
			     when A.ShortExtraPostingRateAccTo = 2 then (case when A.FixedBillingType = 3 then (F.FixedRate * F.Quantity) else F.FixedRate end / JI.TotalBillingDays)
				 else 0 end as ShortExtraRate,
			E.TaxableAmount,
			E.TaxAmount,
			E.NetAmount,
			B.TotalBillAmount,
			B.RoundOffAmount,
			case when ID.DisplayUOMCode is not null then ID.DisplayUOMCode else	ID.SaleUomCode end as UOMCode,
			ID.BillingDisplayName as ItemDescription,
			CONCAT('from ',CONVERT(varchar(30), B.StartDate, 106),' to ',CONVERT(varchar(30), B.EndDate, 106)) as ItemAssignedPeriod,
			@_bIsOtherFlag as IsOtherState,
			CONCAT(L.TaxGroupName,' %') as GeneralTaxGroupMasterName,
			L.TaxRate,
			K.HSNCode,
			B.SalesInvoiceMasterID,
			IM.CustomerInvoiceNumber,
			IM.StorageLocationID as LocationID,
			SL.LocationName,
			C.ID as CustomerMasterID,
			D.ID as CustomerBranchMasterID,
			Convert( nvarchar(35),IM.TransactionDate,106) as InvoiceTransactionDate,
			F.SaleContractRequiredTypeID,
			T.IsServiceChargesAppliedToServiceItem,
			ID.GenTaxGroupMasterID,
			IM.TaxExemptionRemark,
			A.IsDisplayPurchaseDetails,
			A.PurchaseOrderNumber,
			CONVERT(varchar(30),A.PurchaseOrderDate,106) as PurchaseOrderDate,
			CONVERT(varchar(30),B.EndDate,106) as DateTimeOfSupply,
			IM.IsCanceled,
			gsteim.QrCodeImage
		from
			SaleContractMaster A
			inner join SaleContractBillingSpan B on (A.ID = B.SaleContractMasterID
													and A.ID = @iSaleContractMasterID
													and B.ID = @iSaleContractBillingSpanID
													and B.IsBillGenerated = 1
													and A.BillingType = 2)
			inner join SaleContractTermDetails T on (A.ID = T.SaleContractMasterID)
			inner join SalesInvoiceMaster IM on (B.SalesInvoiceMasterID = IM.ID)
			inner join SalesInvoiceDetail ID on (IM.ID = ID.SalesInvoiceMasterID)
			inner join CustomerMaster C on (A.CustomerMasterID = C.ID)
			left outer join InventoryLocationMaster SL on (IM.StorageLocationID = SL.ID)
			left outer join CustomerBranchMaster D on (A.CustomerBranchMasterID = D.ID)
			inner join SaleContractRequirementDetails F on (A.ID = F.SaleContractMasterID
															and (F.SaleContractRequiredTypeID = 5 or F.SaleContractRequiredTypeID = 2 
																or F.SaleContractRequiredTypeID = 8 or F.SaleContractRequiredTypeID = 6)
															and ID.VariationMasterID= F.ID)
			inner join SaleContractBillingTransaction E on (E.SaleContractBillingSpanID = B.ID
															and E.SaleContractMasterID = A.ID
															and ID.ItemNumber = E.ItemNumber
															and F.ID = E.SaleContractRequirementDetailsID
															and ISNULL(E.IsCanceled,0) = 0)
			inner join GeneralItemMaster K on (E.ItemNumber = K.ItemNumber)
			left outer join cteTotalTax L on (E.GeneralTaxGroupMasterID = L.ID)
			left outer join SaleContractManPowerItem M on (F.SaleContractManPowerItemID = M.ID
															and F.SaleContractRequiredTypeID = 5)
			left outer join EmployeeDesignationMaster O on (M.DesignationMasterID = O.ID)
			left outer join SaleContractFixAttendance N on (N.SaleContractFixItemID = F.SaleContractManPowerItemID
															and N.SaleContractBillingSpanID = B.ID)
			left outer join SaleContractMachineMaster P on (F.SaleContractMachineMasterID = P.ID
															and F.ItemNumber = P.ItemNumber
															and F.SaleContractRequiredTypeID = 2)
			left outer join FixItemQuantity JI on (M.ID = JI.SaleContractFixItemID
														and F.SaleContractRequiredTypeID = 5)
			left outer join ManPowerQuantity MQ on (F.SaleContractManPowerItemID = MQ.SaleContractManPowerItemID) 
			left join GSTEInvoiceMaster gsteim on gsteim.SalesInvoiceMasterID = IM.ID

	  select 
			 A.ContractNumber
			,A.BillingType				
			,case when C.IsBillToSameAsShipTo=1 then A.CustomerBranchMasterName	 else A.CustomerMasterName end as CustomerMasterName	
			,A.CustomerBranchMasterName	
			,A.SaleContractBillingSpanName
			,A.ItemNumber					
			,A.Rate						
			,A.Quantity			
			,A.FixedQuantity
			,A.OriginalQuantity		
			,A.ShortExtraRate
			,A.TaxableAmount				
			,A.TaxAmount					
			,A.NetAmount					
			,A.TotalBillAmount				
			,A.RoundOffAmount				
			,A.UOMCode						
			,A.ItemDescription				
			,A.ItemAssignedPeriod			
			,A.IsOtherState				
			,A.GeneralTaxGroupMasterName
			,A.GeneralTaxGroupMasterID
			,A.TaxRate	
			,A.HSNCode						
			,A.SalesInvoiceMasterID		
			,A.CustomerInvoiceNumber		
			,A.LocationID					
			,A.LocationName 
			,U.LogoPath
			,case when C.IsBillToSameAsShipTo=1 then CONCAT(C.Address1,' ',C.Address2,' ',C.Address3)  else CONCAT(B.Address1,' ',B.Address2,' ',B.Address3) end as CustomerAddress,
			 case when C.ID is null then CONCAT(B.Address1,' ',B.Address2,' ',B.Address3)  
			 					   else  CONCAT(C.Address1,' ',C.Address2,' ',C.Address3) 
			 					   end as CustomerBranchAddress,
			 P.CurrencyCode as Currency
			,case when C.IsBillToSameAsShipTo = 1 then K.Description else G.Description end as CityName,
			 case when C.IsBillToSameAsShipTo = 1 then L.CountryName else H.CountryName end as CountryName ,
			 case when C.IsBillToSameAsShipTo = 1 then M.RegionName else I.RegionName end as Statename,
			 case when C.ID  is null then G.Description else  K.Description end as BranchCityName,
			 case when C.ID  is null then H.CountryName else  L.CountryName end as BranchCountryName ,
			 case when C.ID  is null then I.RegionName  else  M.RegionName end as BranchStatename,
			N.CellPhone,
		    N.CentreName,
		    N.EmailID,
		    N.FaxNumber,
		    N.PhoneNumberOffice,
		    --CONCAT(N.PlotNo,',',N.StreetName,',',CentreAddress,' ',GC.Description,'- ',N.Pincode) as Address1,
		    --Concat(GC.Description,'- ',N.Pincode) as Address2,
			CONCAT(N.StreetName,' ',GC.Description,'- ',N.Pincode) as Address2,
			CentreAddress as Address1,
		    N.Url as Website,
		    @_bIsOtherFlag as IsOther,
		    A.HSNCode,
		    case when C.IsBillToSameAsShipTo = 1 then  CONCAT(C.GSTNumber,'     Pan Number:', C.PanNumber ) else  CONCAT(B.GSTNumber,'     Pan Number:', B.PanNumber) end as CustomerGSTNumber,
		    C.GSTNumber as BranchGSTNumber,
			A.InvoiceTransactionDate,
			A.SaleContractRequiredTypeID,
			A.IsServiceChargesAppliedToServiceItem,
			@_bIsTaxExempted as IsTaxExempted,
			@_tiReasonForExemption as ReasonForExemption,
			N.CINNumber,
			N.GSTINNumber,
			N.PanNumber,
			N.PFNumber,
			N.ESICNumber,
			N.CentreSpecialization,
			N.WaterMark,
			Q.PrintingLinebelowLogo,
			case when C.IsBillToSameAsShipTo=1 then  M.TinNumber else  I.TinNumber end as StateCode,
			case when C.ID  is null then I.TinNumber  else  M.TinNumber end as BranchStateCode,
			case when C.IsBillToSameAsShipTo=1 then C.PinCode else B.PinCode end as CustomerPinCode,
			case when C.ID is null then B.PinCode else  C.PinCode end as CustomerBranchPinCode,
			A.TaxExemptionRemark,
			A.IsDisplayPurchaseDetails,
			A.PurchaseOrderNumber,
			A.PurchaseOrderDate,
			A.DateTimeOfSupply,
			A.IsCanceled,
			A.QrCodeImage
	from #TmpSaleContractBillingTransaction A 
		 inner join GeneralUnits U on (U.ID=@iGeneralUinitsID)
		 inner Join CustomerMaster B ON( A.CustomerMasterID=B.ID)
		 Left Outer join CustomerBranchMaster C on (A.CustomerBranchMasterID=C.ID and A.CustomerMasterID=C.CustomerMasterID)
		 left outer join GeneralCity	G on (G.ID=B.CityID)
		 inner join GeneralCountryMaster H on (H.ID=B.CountryID)
		 inner join GeneralRegionMaster I on (I.ID=B.StateID)
		 
		 left outer join GeneralCity	K on (K.ID=C.CityID)
		 left outer join GeneralCountryMaster L on (L.ID=C.CountryID)
		 left outer join GeneralRegionMaster M on (M.ID=C.StateID)
		 
		 inner join OrganisationStudyCentreMaster N on (N.CentreCode=@_nsCentreCode and N.IsDeleted=0)
		 left outer join GeneralCity GC on (N.CityID=GC.ID )
		 left outer join GeneralCurrencyMaster P on (B.Currency=P.ID)
		 left outer join OrganisationStudyCentrePrintingFormat Q on (Q.CentreCode=@_nsCentreCode and Q.IsDeleted=0)
			
	  END TRY
	BEGIN CATCH
		
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End	











