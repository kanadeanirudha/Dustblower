USE [Ver2]
GO
/****** Object:  StoredProcedure [dbo].[USP_GetRecordForServiceInvoiceMasterPDF]    Script Date: 2/22/2023 4:02:53 PM ******/
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
ALTER PROCEDURE [dbo].[USP_GetRecordForServiceInvoiceMasterPDF]
	@iSalesInvoiceMasterID int,--salesOrderMasterID
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY
	 begin
	 	
	 DECLARE @_nsCentreCode	nvarchar(15),
			 @_iCreatedBy INT,@_TimeZone nvarchar(50);
     Declare @_iCustomerMasterID int,@_iCustomerBranchMasterID int,@_bIsOtherFlag bit,@_iGeneralUnitsID int,@_bIsTaxExempted bit,@_tiReasonForExemption tinyint,@_nsTaxExemptionRemark nvarchar(250);
	 
	 Select @_nsCentreCode=B.CentreCode from  SalesInvoiceMaster A inner join GeneraLunits B 
										on (A.GeneralUnitsID=B.ID
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

	Select @_bIsOtherFlag=dbo.fGetIsOtherfromCustomerMasterByUnit (@_iCustomerMasterID,@_iCustomerBranchMasterID,@_iGeneralUnitsID)
	;With CteFirst as(
						select 
								0 as SalesOrderMasterID	,
								 A.ID as SalesInvoiceMasterID,					
								 B.InvoiceQuantity
								,B.ItemNumber
								,null as BatchNumber
								,null as ExpiryDate
								, B.SaleUomCode,
								B.ID as varience
						 from 
								SalesInvoiceMaster A 
								inner join SalesInvoiceDetail B ON(
																A.ID = B.SalesInvoiceMasterID
																and A.ID=@iSalesInvoiceMasterID
																And A.IsDeleted = 0
																And B.IsDeleted = 0)
								
				)
			
				

				, cte1 as(
						select 
								0 as SalesOrderMasterID	,
								SalesInvoiceMasterID,					
								Sum(A.InvoiceQuantity) as InvoiceQuantity
								,A.ItemNumber
								,A.SaleUomCode
								,varience
								
						 from 
								CteFirst A
						Group By A.SalesOrderMasterID,A.ItemNumber ,A.SaleUomCode,SalesInvoiceMasterID,varience
				)

				,ctesample	 as
				(
				select 
					--	(A.Rate * cte1.InvoiceQuantity * D.TaxRate)/100 as TaxAmount
						cte1.SalesOrderMasterID
						,cte1.SalesInvoiceMasterID
						,D.IsOtherState
						,B.TaxGroupName
						,D.TaxRate as TaxRate
						,cte1.SaleUomCode
						,A.ItemNumber
						,cte1.varience,
						A.Rate	,cte1.InvoiceQuantity

					from
						cte1 
						inner join SalesInvoiceDetail A on (cte1.SalesInvoiceMasterID = A.SalesInvoiceMasterID
																and cte1.ItemNumber = A.ItemNumber
																and cte1.varience = A.ID)
						inner join GeneralTaxGroupMaster B on (A.GenTaxGroupMasterID  = B.ID)
						inner join GeneralTaxGroupMasterDetails C on (B.ID = C.GenTaxGroupMasterID)
						inner join GeneralTaxMaster D on (C.GenTaxMasterID = D.ID 
															and D.IsOtherState = 0)
						--group by cte1.varience,cte1.SalesOrderMasterID,D.IsOtherState,B.TaxGroupName,cte1.SaleUomCode,A.ItemNumber,cte1.SalesInvoiceMasterID,D.TaxRate,
						--A.Rate,Cte1.InvoiceQuantity
					)

					
				,cteTaxes as(
					select 
						sum((cte1.Rate * cte1.InvoiceQuantity * cte1.TaxRate)/100) as TaxAmount
						,cte1.SalesOrderMasterID
						,cte1.SalesInvoiceMasterID
						,cte1.IsOtherState
						,cte1.TaxGroupName
						,Sum(cte1.TaxRate) as TaxRate
						,cte1.SaleUomCode
						,cte1.ItemNumber
						,cte1.varience

					from
						ctesample cte1
					group by cte1.varience,cte1.SalesOrderMasterID,cte1.IsOtherState,cte1.TaxGroupName,cte1.SaleUomCode,cte1.ItemNumber,cte1.SalesInvoiceMasterID
					)
					

			--Select * from cteTaxes
				,cte2 as (
				select
					null as DeliveryNumber,
					CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC',@_TimeZone) ,106) as InvoiceDate,
					D.ID as SalesInvoiceDetailsID,
					case when A.BillingSpanEndDate is not null then CONVERT(varchar(30),A.BillingSpanEndDate ,106) else CONVERT(varchar(30),dbo.FnConvertDateToTimeZoneDateTime (A.TransactionDate,'UTC',@_TimeZone) ,106) end as DateTimeOfSupply,
					null as BarCode,
					case when isnull(D.DisplayUOMCode,'') <> '' then D.DisplayUOMCode else I.SaleUomCode end as SaleUOMcode,
					0 as Freight,
					cast(cteTaxes.TaxAmount as money) as TotalTaxAmount,
					cteTaxes.TaxGroupName,
					cteTaxes.TaxRate,
					null as SalesOrderNumber,
					null as SalesOrderDate,
					null as ShippingHandling,
					null as Discount,
					A.CustomerMasterID,
					A.CustomerBranchMasterID,
					D.ID,
					D.Rate,
					D.DisplayRate,
					--E.ItemDescription as ItemName,
					D.BillingDisplayName as ItemName,
					F.LocationName,
					E.HSNCode,
					null as BatchNumbers,
					null as ExpiryDate,
					I.InvoiceQuantity as InvoiceQuantity,
					D.InvoiceQuantity as SalesOrderQty,
					A.CustomerInvoiceNumber,
					L.LogoPath,
					D.ItemNumber,
					A.PurchaseOrderNumber,
					A.PurchaseOrderDate,
					A.InvoiceDeductionName,
					A.InvoiceDeductionAmount,
					A.IsCanceled,
					m.QrCodeImage,
					m.Irn,
					m.AcknowledgementNo,
					m.AcknowledgementDate
				from  SalesInvoiceMaster A 
					inner join cte1 I ON(I.SalesInvoiceMasterID= A.ID)
				    inner join SalesInvoiceDetail D on (A.ID=D.SalesInvoiceMasterID
												         And D.ItemNumber=I.ItemNumber
														 and I.varience = D.ID
												         And D.IsDeleted=0)
				    inner join GeneralItemMaster E on (I.ItemNumber=E.ItemNumber
													   And E.IsDeleted=0)		
				    inner join InventoryLocationMaster F on(F.ID=A.StorageLocationID
															and D.IsDeleted=0)	
					inner join cteTaxes on (cteTaxes.SalesInvoiceMasterID = D.SalesInvoiceMasterID
											and cteTaxes.ItemNumber=D.ItemNumber
											and cteTaxes.SaleUomCode=D.SaleUomCode
											and cteTaxes.varience = D.ID
											)
					inner join GeneralUnits L on (A.GeneralUnitsID=L.ID and L.IsDeleted=0)
					left join GSTEInvoiceMaster m on m.SalesInvoiceMasterID = A.ID 
					
					
				
				   )
				  --Select * from cte2


				    ,ctefinal as(
					Select 
							cte3.SalesOrderNumber,							
							STUFF((
							SELECT ', ' + cte2.DeliveryNumber
							FROM 
								cte2
							WHERE (cte3.SalesOrderNumber = cte2.SalesOrderNumber and cte3.ItemNumber=cte2.ItemNumber)
							FOR XML PATH (''))
							,1,2,'') AS DeliveryNumber
							,cte3.InvoiceQuantity
							,cte3.ItemName as ItemDescription
							,cte3.SaleUOMcode
							,cte3.Freight
							,cte3.ShippingHandling
							,cte3.Discount
							,cte3.TotalTaxAmount
							,cte3.Rate
							,cte3.DisplayRate
							,cte3.LocationName
							,cte3.BatchNumbers
							,cte3.ExpiryDate
							,cte3.CustomerInvoiceNumber
							,cte3.TaxGroupName
							,cte3.TaxRate
							,Cte3.CustomerMasterID
							,cte3.CustomerBranchMasterID
							,Cte3.LogoPath
							,cte3.InvoiceDate
							,cte3.ItemNumber
							,cte3.HSNCode
							,cte3.SalesOrderQty
							,cte3.PurchaseOrderNumber
							,cte3.PurchaseOrderDate
							,cte3.InvoiceDeductionName
							,cte3.InvoiceDeductionAmount
							,cte3.IsCanceled
							,cte3.DateTimeOfSupply
							,cte3.SalesInvoiceDetailsID
							,cte3.QrCodeImage
							,cte3.Irn
							,cte3.AcknowledgementNo
							,cte3.AcknowledgementDate
						FROM 
							cte2 cte3
							group by cte3.SalesOrderNumber,cte3.InvoiceQuantity,cte3.ItemName,cte3.SaleUOMcode,cte3.Freight,cte3.ShippingHandling
						    ,cte3.Discount,cte3.TotalTaxAmount,cte3.Rate,cte3.LocationName,cte3.BatchNumbers,cte3.ExpiryDate,cte3.CustomerInvoiceNumber,cte3.TaxGroupName,
							cte3.TaxRate,Cte3.CustomerMasterID,cte3.CustomerBranchMasterID,Cte3.LogoPath,cte3.InvoiceDate,cte3.ItemNumber,cte3.HSNCode,cte3.SalesOrderQty,
							cte3.DisplayRate,cte3.PurchaseOrderNumber,cte3.PurchaseOrderDate,cte3.InvoiceDeductionName,cte3.InvoiceDeductionAmount,cte3.IsCanceled,cte3.DateTimeOfSupply
							,cte3.SalesInvoiceDetailsID,cte3.QrCodeImage,cte3.Irn,cte3.AcknowledgementNo,cte3.AcknowledgementDate
							)

							Select 
							 A.SalesOrderNumber,	
							 A.InvoiceDate,						
							 A.DateTimeOfSupply,
							 A.DeliveryNumber
							,A.InvoiceQuantity
							,A.ItemDescription
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
							 A.QrCodeImage,
							 A.Irn,
							 A.AcknowledgementNo,
							 A.AcknowledgementDate
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
			order by A.SalesInvoiceDetailsID

		   
	END
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End




