USE [Ver2]
GO
/****** Object:  StoredProcedure [dbo].[USP_GetRecordForSalesInvoiceMasterPDF]    Script Date: 2/22/2023 3:53:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'USP_GetRecordForSalesInvoiceMasterPDF	'
-- based on the Primary Key.
-- Gets: @iSalesOrderMasterID int 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_GetRecordForSalesInvoiceMasterPDF]
	@iSalesInvoiceMasterID int,--salesOrderMasterID
	@iErrorCode int OUTPUT

	-- exec USP_GetRecordForSalesInvoiceMasterPDF @iSalesInvoiceMasterID=10787,@iErrorCode=null
AS
BEGIN
	BEGIN TRY
	 begin
	 DECLARE @DeliveryNumbers NVARCHAR(MAX), @DeliveryGTransDate NVARCHAR(MAX);
	  DECLARE @_nsCentreCode	nvarchar(15),
			 @_iCreatedBy INT,@_TimeZone nvarchar(50);
     Declare @_iCustomerMasterID int,@_iCustomerBranchMasterID int,@_bIsOtherFlag bit,@_iGeneralUnitsID int,@_bIsTaxExempted bit,@_tiReasonForExemption tinyint,@_nsTaxExemptionRemark nvarchar(250);
	 
	 Select @_nsCentreCode=B.CentreCode from  SalesInvoiceMaster A inner join InventoryLocationMaster B 
										on (A.StorageLocationID=B.ID
											and A.ID=@iSalesInvoiceMasterID
											and A.IsDeleted=0 
											and B.IsDeleted=0)

	Select 
			@_iCustomerMasterID			= A.CustomerMasterID
		   ,@_iCustomerBranchMasterID   = A.CustomerBranchMasterID
		   ,@_iGeneralUnitsID			= A.GeneralUnitsID
	From SalesInvoiceMaster A where ID=@iSalesInvoiceMasterID
	select @_TimeZone= Timezone from OrganisationStudyCentreMaster where CentreCode=@_nsCentreCode; 

	Select 
		@_bIsTaxExempted = case when A.CustomerType= 1 then A.IsTaxExempted else B.IsTaxExempted end
		,@_tiReasonForExemption = case when A.CustomerType= 1 then A.ReasonForExemption else B.ReasonForExemption end
		,@_nsTaxExemptionRemark = case when A.CustomerType= 1 then A.TaxExemptionRemark else B.TaxExemptionRemark end
	From CustomerMaster A
			left outer join CustomerBranchMaster B on (A.ID = B.CustomerMasterID
													and (B.ID = @_iCustomerBranchMasterID or @_iCustomerBranchMasterID = 0))
	where A.ID = @_iCustomerMasterID

	Select @_bIsOtherFlag=dbo.fGetIsOtherfromCustomerMasterByCentreCode(@_iCustomerMasterID,@_iCustomerBranchMasterID,@_nsCentreCode)
	;With CteSaleInvoice as (
							Select 
								 C.ID as SalesOrderMasterID	,
								 A.ID as SalesInvoiceMasterID,
								 B.InvoiceQuantity
								,B.ItemNumber
								,null as BatchNumber
								,null as ExpiryDate
								,B.SaleUomCode
								,B.GenTaxGroupMasterID
								,B.Rate
								,A.CustomerMasterID
								,A.CustomerBranchMasterID
								,A.PurchaseOrderNumber,
								A.PurchaseOrderDate,
								A.InvoiceDeductionName,
								A.InvoiceDeductionAmount,
								A.IsCanceled,
								B.ID,
								B.DisplayRate
								,B.DisplayUOMCode
								,BillingDisplayName
								,B.SalesOrderDeliveryMasterid
								,A.transactionDate
							from
									SalesInvoiceMaster A 
									inner join SalesInvoiceDetail B ON(
																	A.ID = B.SalesInvoiceMasterID
																	and A.ID=@iSalesInvoiceMasterID
																	and B.InvoiceQuantity>0
																	And A.IsDeleted = 0
																	And B.IsDeleted = 0)
								inner   JOIN SalesOrderMaster c ON c.ID=a.SalesOrderMasterID
							)
				,CteGrpConcate  as (
									select distinct 
										A.salesinvoicemasterid, B.id,B.deliverynumber,
										--CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (B.DeliveryTransDate,'UTC',@_TimeZone) ,106) as DeliveryTransDate
										dbo.FnConvertDateToTimeZoneDateTime (B.DeliveryTransDate,'UTC',@_TimeZone)as DeliveryTransDate,
										dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC',@_TimeZone)as TransactionDate
									from CteSaleInvoice A 
									inner join SalesorderDeliveryMAster B on (A.SalesOrderMasterID=B.SalesOrderMasterID )
									)
				SELECT @DeliveryNumbers = COALESCE(@DeliveryNumbers + N', ', N'') + deliverynumber, 
				--@DeliveryGTransDate = COALESCE(@DeliveryGTransDate + N', ', N'')+ CONVERT(varchar(30),DeliveryTransDate,106)
				@DeliveryGTransDate = CONVERT(varchar(30),DeliveryTransDate,106) 
					FROM CteGrpConcate	ORDER BY deliverynumber;

	;With CteSaleInvoice as (
							Select 
								 0 as SalesOrderMasterID	,
								 A.ID as SalesInvoiceMasterID,
								 B.InvoiceQuantity
								,B.ItemNumber
								,null as BatchNumber
								,null as ExpiryDate
								,B.SaleUomCode
								,B.GenTaxGroupMasterID
								,B.Rate
								,A.CustomerMasterID
								,A.CustomerBranchMasterID
								,A.PurchaseOrderNumber,
								A.PurchaseOrderDate,
								A.InvoiceDeductionName,
								A.InvoiceDeductionAmount,
								A.IsCanceled,
								B.ID as SalesInvoiceDetailID,
								B.DisplayRate
								,B.DisplayUOMCode
								,BillingDisplayName
								,B.SalesOrderDeliveryMasterid
								,A.transactionDate
								,c.QrCodeImage
								,c.Irn
								,c.AcknowledgementNo
								,C.AcknowledgementDate
							from
									SalesInvoiceMaster A 
									inner join SalesInvoiceDetail B ON(
																	A.ID = B.SalesInvoiceMasterID
																	and A.ID=@iSalesInvoiceMasterID
																	and B.InvoiceQuantity>0
																	And A.IsDeleted = 0
																	And B.IsDeleted = 0)
									left join GSTEInvoiceMaster c on c.SalesInvoiceMasterID = A.ID 
							)

				,CteFirst as(
	
							select 
								 SalesInvoiceMasterID,					
								 sum(InvoiceQuantity) as InvoiceQuantity
								,ItemNumber
								,SaleUomCode
								,GenTaxGroupMasterID
								,Rate
								,DisplayUOMCode
								,DisplayRate
								,BillingDisplayName
								,SalesInvoiceDetailID
								,QrCodeImage
								,Irn
								,AcknowledgementNo
								,AcknowledgementDate
							from 
								CteSaleInvoice						
							group by SalesInvoiceMasterID,ItemNumber, SaleUomCode,GenTaxGroupMasterID,Rate	,DisplayUOMCode,DisplayRate	,BillingDisplayName,SalesInvoiceDetailID,QrCodeImage,Irn
								,AcknowledgementNo,AcknowledgementDate
				)

				,cteTaxes	 as
				(
				select 
						(A.Rate * A.InvoiceQuantity *sum( D.TaxRate))/100 as TaxAmount
						,Sum(D.TaxRate) as TaxRate
						,A.SalesInvoiceMasterID
						,D.IsOtherState
						,B.TaxGroupName
						,A.SaleUomCode
						,A.ItemNumber,
						A.Rate	,
						A.InvoiceQuantity
						,A.SalesInvoiceDetailID

					from
						CteFirst A						
						inner join GeneralTaxGroupMaster B on (A.GenTaxGroupMasterID  = B.ID)
						inner join GeneralTaxGroupMasterDetails C on (B.ID = C.GenTaxGroupMasterID)
						inner join GeneralTaxMaster D on (C.GenTaxMasterID = D.ID 
															and D.IsOtherState = 0)
						group by D.IsOtherState,B.TaxGroupName,A.SaleUomCode,A.ItemNumber,A.SalesInvoiceMasterID,D.TaxRate,	A.Rate,InvoiceQuantity,A.SalesInvoiceDetailID
					)

				,CteFinal as (
				select
					@DeliveryNumbers as DeliveryNumber,
					@DeliveryGTransDate as DateTimeOfSupply,
					----D.ID as SalesInvoiceDetailsID,
					CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC',@_TimeZone) ,106) as InvoiceDate,
					null as BarCode,
					case when isnull(D.DisplayUOMCode,'') <> '' then D.DisplayUOMCode else I.SaleUomCode end as SaleUOMcode,
					0 as Freight,
					cast(cteTaxes.TaxAmount as money) as TotalTaxAmount,
					I.TaxGroupName,
					I.TaxRate,
					N.SalesOrderNumber ,
					null as SalesOrderDate,
					null as ShippingHandling,
					null as Discount,
					A.CustomerMasterID,
					A.CustomerBranchMasterID,
					D.SalesInvoiceDetailID,
					--D.ID,
					D.Rate,
					D.DisplayRate,
					case when D.BillingDisplayName is not null then D.BillingDisplayName else case when O.ArabicTransalation is not null then O.ArabicTransalation else E.ItemDescription end end as ItemName,
					--D.BillingDisplayName as ItemName,
					F.LocationName,
					E.HSNCode,
					null as BatchNumbers,
					null as ExpiryDate,
					I.InvoiceQuantity as InvoiceQuantity,
					I.InvoiceQuantity as SalesOrderQty,
					A.CustomerInvoiceNumber,
					L.LogoPath,
					I.ItemNumber,
					A.PurchaseOrderNumber,
					A.PurchaseOrderDate,
					A.InvoiceDeductionName,
					A.InvoiceDeductionAmount,
					A.IsCanceled,
					N.PurchaseOrderNumberClient,
					case when isnull(E.IsServiceItem,0)=1 then 1 else 0 end as serviceItem
					,D.QrCodeImage
					,D.Irn
					,D.AcknowledgementNo
					,D.AcknowledgementDate
				from  SalesInvoiceMaster A 
					inner join cteTaxes I ON(I.SalesInvoiceMasterID= A.ID )
					inner join CteFirst D on (A.ID=D.SalesInvoiceMasterID
												         And D.ItemNumber=I.ItemNumber
														 and D.InvoiceQuantity > 0
														 and I.SalesInvoiceDetailID=D.SalesInvoiceDetailID
												         )
				    inner join GeneralItemMaster E on (I.ItemNumber=E.ItemNumber
													   And E.IsDeleted=0)		
					left outer join GeneralItemGeneralData O on (E.ItemNumber = O.ItemNumber
															and O.IsDeleted = 0)
				    inner join InventoryLocationMaster F on(F.ID=A.StorageLocationID
															)	
					inner join cteTaxes on (cteTaxes.SalesInvoiceMasterID = D.SalesInvoiceMasterID
											and cteTaxes.ItemNumber=D.ItemNumber
											and cteTaxes.SaleUomCode=D.SaleUomCode
											and cteTaxes.SalesInvoiceDetailID=D.SalesInvoiceDetailID
											)
					inner join GeneralUnits L on (F.CentreCode=L.CentreCode and L.IsDeleted=0)
					inner join SalesOrderMaster N on (A.SalesOrderMasterID = N.ID)
					)
					
					Select 
							 A.SalesOrderNumber,	
							 A.InvoiceDate,	
							 A.DateTimeOfSupply,					
							 A.DeliveryNumber
							,A.InvoiceQuantity
							,A.itemname as  ItemDescription
							,A.SaleUOMcode
							,A.Freight
							,A.ShippingHandling
							,A.Discount
							,A.TotalTaxAmount
							,case when isnull(A.DisplayRate,0) <> 0 then A.DisplayRate else A.Rate end as Rate
							,A.Rate * A.InvoiceQuantity as ItemWiseAmount
							,((A.Rate * A.InvoiceQuantity )*A.TaxRate)/100 as ItemWiseTaxAmount
							,A.LocationName
							,A.BatchNumbers
							,A.ExpiryDate
							,A.CustomerInvoiceNumber
							,A.TaxGroupName
							,A.TaxRate
							,A.CustomerMasterID
							,A.CustomerBranchMasterID
							,A.LogoPath
							,A.SalesOrderQty
							,case when B.CustomerType=1 then Concat (B.FirstName,' ',B.MiddleName,' ',B.LastName)
									   else B.CompanyName
									   End as CustomerName,
							  case when C.IsBillToSameAsShipTo =1 then CONCAT(C.Address1,' ',C.Address2,' ',C.Address3) else 
																	  CONCAT(B.Address1,' ',B.Address2,' ',B.Address3) end as CustomerAddress,
							 case when C.ID is null then CONCAT(B.Address1,' ',B.Address2,' ',B.Address3)  
							 					   else  CONCAT(C.Address1,' ',C.Address2,' ',C.Address3) 
							 					   end as CustomerBranchAddress,
							 case when C.IsBillToSameAsShipTo =1 then C.PinCode else B.PinCode end as CustomerPinCode,
							 case when C.ID is null then B.PinCode else  C.PinCode end as CustomerBranchPinCode,
							 P.CurrencyCode as Currency
							,case when isnull(C.IsBillToSameAsShipTo,0)=1 then  K.Description else  G.Description end as CityName,
							 case when C.IsBillToSameAsShipTo=1 then L.CountryName  else H.CountryName end as CountryName,
							 case when C.IsBillToSameAsShipTo=1 then M.RegionName else I.RegionName end as Statename,
							 case when C.IsBillToSameAsShipTo=1 then isnull(M.TinNumber,0) else Isnull(I.TinNumber,0) end as StateCode,
							 case when C.ID  is null then G.Description else  K.Description end as BranchCityName,
							 case when C.ID  is null then H.CountryName else  L.CountryName end as BranchCountryName ,
							 case when C.ID  is null then I.RegionName  else  M.RegionName end as BranchStatename,
							  case when C.ID  is null then isnull(I.TinNumber,0)  else  isnull(M.TinNumber,0) end as BranchStateCode,
							 case when C.ID is null then  Concat (B.FirstName,' ',B.MiddleName,' ',B.LastName) else isnull(C.BranchName,'-') end as BranchName ,
							 N.CellPhone,
							 N.CentreName,
							 N.EmailID,
							 N.FaxNumber,
							 N.PhoneNumberOffice,
							 CONCAT(N.StreetName,' ',GC.Description,'- ',N.Pincode) as Address2,
							 CentreAddress as Address1,
							 N.Url as Website,
							 @_bIsOtherFlag as IsOther,
							 A.HSNCode,
							 CONCAT(B.GSTNumber,'     Pan Number:', C.PanNumber )  as CustomerGSTNumber,
							-- B.GSTNumber as CustomerGSTNumber,
							 C.GSTNumber as BranchGSTNumber,
							 N.CINNumber,
							 N.GSTINNumber,
							 N.PanNumber,
							 N.PFNumber,
							 N.ESICNumber,
							 N.CentreSpecialization,
							 N.WaterMark,
							 Q.PrintingLinebelowLogo,
							 A.PurchaseOrderNumber,
							 A.PurchaseOrderDate,
							 A.InvoiceDeductionName,
							 A.InvoiceDeductionAmount,
							 @_bIsTaxExempted as IsTaxExempted,
							 @_tiReasonForExemption as ReasonForExemption,
							 @_nsTaxExemptionRemark as TaxExemptionRemark,
							 A.IsCanceled,
							 A.PurchaseOrderNumberClient,
							 A.serviceItem as ServiceItemFlag
							,a.QrCodeImage
							,a.Irn
							,a.AcknowledgementNo
							,a.AcknowledgementDate
							from ctefinal A 
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
							order by A.SalesInvoiceDetailID		   
	END
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End



