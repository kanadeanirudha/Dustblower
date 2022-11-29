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
ALTER   PROCEDURE [dbo].[USP_GetRecordForSalesEInvoiceMaster]
	@iSalesInvoiceMasterID int,
	@iErrorCode int OUTPUT
	-- Exec USP_GetRecordForSalesEInvoiceMaster @iSalesInvoiceMasterID=10787, @iErrorCode =null
AS
BEGIN
	BEGIN TRY
	 begin
	 DECLARE @_nsCentreCode	nvarchar(15),@_iCreatedBy INT,@_TimeZone nvarchar(50);
	 -- Get CentreCode
	 Select @_nsCentreCode=B.CentreCode from  SalesInvoiceMaster A inner join InventoryLocationMaster B 
										on (A.StorageLocationID=B.ID
											and A.ID=@iSalesInvoiceMasterID
											and A.IsDeleted=0 
											and B.IsDeleted=0)
	
	select @_TimeZone= Timezone from OrganisationStudyCentreMaster where CentreCode=@_nsCentreCode; 

	;With Cte1 as(
	select
		CustomerInvoiceNumber,
		sim.TransactionDate,
		CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (sim.TransactionDate,'UTC',@_TimeZone) ,103) as InvoiceDate,
		--osm.GSTINNumber 
		'34AACCC1596Q002' SellerGSTINNumber,
		osm.CentreName,
		osm.CentreSpecialization,
		osm.CentreAddress CentreAddress1,
		case 
			when osm.StreetName is null then '' 
			else osm.StreetName
		End CentreAddress2 ,
		gc.Description CentreCity,
		--osm.Pincode
		'605001' CentrePincode,
		grm.RegionName CentreRegionName,
		--grm.TinNumber
		34 CentreStateCode,
		osm.PhoneNumberOffice,
		osm.CellPhone ,
		osm.EmailID SellerEmailID,
		cm.GSTNumber CustomerGSTNumber,
		cm.CompanyName Customer,
		cgrm.TinNumber CustomerPOS, -- Place_Of_Supply_State_ Code
		cm.Address1 CustomerAddress1,
		case 
			when cm.Address2 is null then '' 
			else cm.Address2
		End CustomerAddress2 ,
		cgc.Description CustomerCity,
		cm.PinCode CustomerPinCode,
		cgrm.TinNumber CustomerStateCode,
		cm.MobileNumber CustomerMobileNumber,
		cm.Email CustomerEmail,
		gim.HSNCode,
		sid.InvoiceQuantity,
		gim.ItemDescription,
		gim.IsServiceItem,
		sid.SaleUomCode,
		sid.Rate UnitPrice,
		sid.InvoiceQuantity * sid.Rate as TotalAmount,
		sid.TaxAmount
		
		From SalesInvoiceMaster sim
		inner join OrganisationStudyCentreMaster osm on (osm.CentreCode=@_nsCentreCode and osm.IsDeleted=0)
		inner join CustomerMaster cm on sim.CustomerMasterID = cm.ID
		inner join SalesInvoiceDetail sid on sim.ID = sid.SalesInvoiceMasterID
		inner join GeneralItemMaster gim on gim.ItemNumber = sid.ItemNumber
		left join GeneralCity gc on osm.CityID=gc.ID 
		left join GeneralCity cgc on cm.CityID=cgc.ID 
		left join GeneralRegionMaster grm on (grm.ID=gc.RegionID)
		left join GeneralRegionMaster cgrm on (cgrm.ID=cm.StateID)


		where sim.ID=@iSalesInvoiceMasterID
		) 
		select top(1)*,
		round((Cte1.TaxAmount *100)/TotalAmount,2) TaxRate,
		TotalAmount + TaxAmount InvoiceLineItemTotalTamount
		from Cte1
		where TaxAmount > 0
	 END
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End





