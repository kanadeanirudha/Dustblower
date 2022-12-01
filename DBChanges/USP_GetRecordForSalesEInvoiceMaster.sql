USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GetRecordForSalesEInvoiceMaster]    Script Date: 11/23/2022 10:13:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'USP_GetRecordForSalesEInvoiceMaster	'
-- based on the Primary Key.
-- Gets: @iSalesOrderMasterID int 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
create or alter  PROCEDURE [dbo].[USP_GetRecordForSalesEInvoiceMaster]
	@iSalesInvoiceMasterID int,
	@iErrorCode int OUTPUT
	-- Exec USP_GetRecordForSalesEInvoiceMaster @iSalesInvoiceMasterID=10787, @iErrorCode =null
AS
BEGIN
	BEGIN TRY
	 begin
	 DECLARE @_nsCentreCode	nvarchar(15),@_iCreatedBy INT,@_TimeZone nvarchar(50);

    Declare @_iCustomerMasterID int,@_iCustomerBranchMasterID int,@_bIsOtherFlag bit
	--,@_iGeneralUnitsID int,@_bIsTaxExempted bit,@_tiReasonForExemption tinyint,@_nsTaxExemptionRemark nvarchar(250);
	
	 -- Get CentreCode
	 Select @_nsCentreCode=B.CentreCode from  SalesInvoiceMaster A inner join InventoryLocationMaster B 
										on (A.StorageLocationID=B.ID
											and A.ID=@iSalesInvoiceMasterID
											and A.IsDeleted=0 
											and B.IsDeleted=0)
	Select 
			@_iCustomerMasterID			= A.CustomerMasterID
		   ,@_iCustomerBranchMasterID   = A.CustomerBranchMasterID
		 --  ,@_iGeneralUnitsID			= A.GeneralUnitsID
	From SalesInvoiceMaster A where ID=@iSalesInvoiceMasterID

	select @_TimeZone= Timezone from OrganisationStudyCentreMaster where CentreCode=@_nsCentreCode; 
	--Select 
	--	@_bIsTaxExempted = case when A.CustomerType= 1 then A.IsTaxExempted else B.IsTaxExempted end
	--	,@_tiReasonForExemption = case when A.CustomerType= 1 then A.ReasonForExemption else B.ReasonForExemption end
	--	,@_nsTaxExemptionRemark = case when A.CustomerType= 1 then A.TaxExemptionRemark else B.TaxExemptionRemark end
	--From CustomerMaster A
	--		left outer join CustomerBranchMaster B on (A.ID = B.CustomerMasterID
	--												and (B.ID = @_iCustomerBranchMasterID or @_iCustomerBranchMasterID = 0))
	--where A.ID = @_iCustomerMasterID

	Select @_bIsOtherFlag=dbo.fGetIsOtherfromCustomerMasterByCentreCode(@_iCustomerMasterID,@_iCustomerBranchMasterID,@_nsCentreCode)
	
	--;With Cte1 as(
	select
		@_nsCentreCode CentreCode,
		CustomerInvoiceNumber,
		sim.TransactionDate,
		CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (sim.TransactionDate,'UTC',@_TimeZone) ,103) as InvoiceDate,
		osm.GSTINNumber SellerGSTINNumber,
		osm.CentreName,
		osm.CentreSpecialization,
		osm.CentreAddress CentreAddress1,
		case 
			when osm.StreetName is null then '' 
			else osm.StreetName
		End CentreAddress2 ,
		gc.Description CentreCity,
		osm.Pincode CentrePincode,
		grm.RegionName CentreRegionName,
		grm.TinNumber CentreStateCode,
		osm.PhoneNumberOffice,
		osm.CellPhone ,
		osm.EmailID SellerEmailID,
		-- Customer Info
		cm.GSTNumber as CustomerGSTNumber,
		cm.CompanyName,
		cgrm.TinNumber CustomerPOS, -- Place_Of_Supply_State_ Code
		cm.Address1 CustomerAddress1,
		case when cm.Address2 is null then '' 
			 else cm.Address2
		End CustomerAddress2 ,
		cgc.Description CustomerCity,
		cm.PinCode CustomerPinCode,
		cgrm.TinNumber CustomerStateCode,
		cm.MobileNumber CustomerMobileNumber,
		cm.Email CustomerEmail,
		-- Line Item Info
		gim.HSNCode,
		sid.InvoiceQuantity,
		gim.ItemDescription,
		gim.IsServiceItem,
		sid.SaleUomCode,
		sid.Rate UnitPrice,
		sid.InvoiceQuantity * sid.Rate as LineItemSubTotal,
		sid.TaxAmount LineItemTaxAmount,
		round((sid.InvoiceQuantity * sid.Rate) + sid.TaxAmount,2) LineItemTotalAmount,
		@_bIsOtherFlag IsOtherState,
		LineItemTaxRates = STUFF((
         SELECT ',' + CONCAT(
								SUBSTRING(gtm.TaxName,0,5) ,
								'~' ,cast (gtm.TaxRate as varchar)
								,'~',cast (gtgm.TaxGroupRate as varchar)
							) 
            FROM GeneralTaxGroupMaster gtgm
			inner join GeneralTaxGroupMasterDetails gtgmd on gtgm.ID = gtgmd.GenTaxGroupMasterID
			inner join GeneralTaxMaster gtm on gtm.ID = gtgmd.GenTaxMasterID and gtm.IsOtherState = @_bIsOtherFlag 
			where gtgm.ID =sid.GenTaxGroupMasterID
            FOR XML PATH('')),1 , 1, ''),
		-- Invoice Amount Details
		sim.NetAmount,
		sim.TaxAmount TotalTaxAmount,
		sim.TotalIInvoiceAmount

		From SalesInvoiceMaster sim
		inner join OrganisationStudyCentreMaster osm on (osm.CentreCode=@_nsCentreCode and osm.IsDeleted=0)
		inner join CustomerMaster cm on sim.CustomerMasterID = cm.ID
		inner join CustomerBranchMaster cbm on cm.ID = cbm.CustomerMasterID
		inner join SalesInvoiceDetail sid on sim.ID = sid.SalesInvoiceMasterID
		inner join GeneralItemMaster gim on gim.ItemNumber = sid.ItemNumber
		left join GeneralCity gc on osm.CityID=gc.ID 
		left join GeneralCity cgc on cm.CityID=cgc.ID 
		left join GeneralRegionMaster grm on (grm.ID=gc.RegionID)
		left join GeneralRegionMaster cgrm on (cgrm.ID=cm.StateID)
		where sim.ID=@iSalesInvoiceMasterID 
		
	 END
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End
