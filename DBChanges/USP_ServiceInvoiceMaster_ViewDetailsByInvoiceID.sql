USE [InvoiceTestingDB]
GO
/****** Object:  StoredProcedure [dbo].[USP_ServiceInvoiceMaster_ViewDetailsByInvoiceID]    Script Date: 1/9/2023 4:19:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select all category name list from the table 'USP_SalesInvoiceMaster_ViewDetailsByInvoiceID'
-- based on the Primary Key.
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_ServiceInvoiceMaster_ViewDetailsByInvoiceID]
	@iSalesInvoiceMasterID int,
	@iErrorCode int OUTPUT
	--exec USP_ServiceInvoiceMaster_ViewDetailsByInvoiceID @iSalesInvoiceMasterID=18362, @iErrorCode=null
AS
BEGIN
	BEGIN TRY
			Begin
			     DECLARE @_nsCentreCode	nvarchar(15), @_sSellerGSTNumber nvarchar(200), @_iGSTEInvoiceCancellationPeriodInMinute int;
			 	 Select @_nsCentreCode=B.CentreCode from  SalesInvoiceMaster A inner join InventoryLocationMaster B 
										on (A.StorageLocationID=B.ID
											and A.ID=@iSalesInvoiceMasterID
											and A.IsDeleted=0 
											and B.IsDeleted=0)
				select @_sSellerGSTNumber = GSTINNumber from OrganisationStudyCentreMaster where CentreCode = @_nsCentreCode
				select	@_iGSTEInvoiceCancellationPeriodInMinute = FieldValue from GeneralSystemSettingMaster where FieldName ='GSTEInvoiceCancellationPeriodInMinute'
				;With CteFirst as(
						select 
							 A.ID as SalesInvoiceMasterID
							,A.SalesOrderMasterID
							,B.ItemNumber
							,B.InvoiceQuantity
							,B.BatchNumber
							,B.ExpiryDate
							,B.SaleUomCode
							--,C.Id as SalesOrderDeliveryMasterID
							,B.ID as SalesInvoiceDetailsID
							,B.ID as VarianceID
						From
							SalesInvoiceMaster A 
							inner join SalesInvoiceDetail B on (A.ID=B.SalesInvoiceMasterID 
																and A.ID=@iSalesInvoiceMasterID
																And A.IsDeleted = 0
																And B.IsDeleted = 0)
									
				)
				
				 ,cte1 as(
						select 
								A.SalesOrderMasterID,					
								A.InvoiceQuantity
								,A.ItemNumber
								,A.SaleUomCode
								--,A.SalesOrderDeliveryMasterID
								,A.SalesInvoiceDetailsID
								,A.SalesInvoiceMasterID
								,A.BatchNumber
								,A.ExpiryDate
								,A.VarianceID
						 from 
								CteFirst A
				)
				
				,cteTaxes as(
					select 
						sum((A.Rate * cte1.InvoiceQuantity * D.TaxRate)/100) as TaxAmount
						,cte1.SalesOrderMasterID
						,D.IsOtherState
						,B.TaxGroupName
						,Sum(D.TaxRate) as TaxRate
						,cte1.SaleUomCode
						,A.ItemNumber
						--,cte1.SalesOrderDeliveryMasterID
						,D.TaxName
						,cte1.VarianceID
					from
						cte1 
						inner join SalesInvoiceDetail A on (cte1.SalesInvoiceDetailsID = A.ID
																and cte1.ItemNumber = A.ItemNumber)
						inner join GeneralTaxGroupMaster B on (A.GenTaxGroupMasterID  = B.ID)
						inner join GeneralTaxGroupMasterDetails C on (B.ID = C.GenTaxGroupMasterID)
						inner join GeneralTaxMaster D on (C.GenTaxMasterID = D.ID 
															and D.IsOtherState = 0)
					group by cte1.SalesOrderMasterID,D.IsOtherState,B.TaxGroupName,cte1.SaleUomCode,A.ItemNumber,B.ID,D.ID,D.TaxName,cte1.VarianceID
					)
					
				,cteFinalTax as (
				select 
					Sum(TaxAmount) as TaxAmount 
					,TaxGroupName
					,itemnumber
					--,SalesOrderDeliveryMasterID
					,SalesOrderMasterID
					,SaleUomCode
					,VarianceID
					,TaxRateList=stuff((
									select
										', ' +  cast (E.TaxRate as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.VarianceID=E.VarianceID
									
									order by E.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,TaxList=stuff((
									select
										', ' +  cast (E.TaxName as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.VarianceID=E.VarianceID
										
										--and cte1.ItemNumber=A.ItemNumber
									order by E.TaxName

									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
					,TaxAmountList=stuff((
									select
										', ' +  cast (E.TaxAmount as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.VarianceID=E.VarianceID
										
										--and cte1.ItemNumber=A.ItemNumber
									order by E.TaxName

									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
									
				from 
					cteTaxes A
				group by TaxGroupName,itemnumber,VarianceID,SalesOrderMasterID,SaleUomCode
					)

				
				,cte2 as (
				select
					A.ID as SalesOrderDeliveryMasterID ,
					@_nsCentreCode CentreCode,
					@_sSellerGSTNumber SellerGSTNumber,
					A.SalesOrderMasterID,
					null as DeliveryNumber,
					null as DeliveryTransDate,
					null as ReceivingLocationID,
					null as Remark,
					D.GeneralItemCodeID,
					D.ItemNumber,
					--A.BarCode,
					D.SaleUomCode as SaleUOMcode,
					--D.BaseUOMQuantity,
					--D.BaseUOMCode,
					A.CustomerMasterID,
					A.CustomerBranchMasterID,
					A.FreightAmount as Freight,
					cast(cteFinalTax.TaxAmount as money) as TotalTaxAmount,
					cteFinalTax.TaxGroupName,
					null as SalesOrderNumber,
					null as SalesOrderDate,
					null as ShippingHandling,
					A.DiscountAmount as Discount,
					D.ID,
					D.Rate,
					CONCAT(E.ItemDescription,case when E.ItemCode is not null then CONCAT(' (',E.ItemCode,')') else '' end) as ItemName,
				--	E.ItemDescription as ItemName,
					F.LocationName,
					case when CustomerType=1 then CONCAT(G.FirstName,' ',G.MiddleName,' ',G.LastName) 
						  else CompanyName end as CustomerName,
					B.BranchName,
					B.IsTaxExempted,
					I.InvoiceQuantity as Quantity,
					A.CustomerInvoiceNumber,
					A.StorageLocationID,
					A.GeneralUnitsID,
					cteFinalTax.TaxRateList,
					cteFinalTax.TaxList,
					cteFinalTax.TaxAmountList,
					I.BatchNumber,
					I.ExpiryDate,
					A.TotalIInvoiceAmount,
					A.NetAmount,
					ISNULL(A.IsCanceled,0) as IsCanceled,
					ISNULL(A.ApprovalStatus,2) as ApprovalStatus,
					ISNULL(A.CancelApprovalStatus,2) as CancelApprovalStatus,
					B.GSTNumber CustomerGSTNumber,
					GSTIM.ID GSTEInvoiceMasterId,
					GSTIM.IsCancelledEInvoice,
					case when DATEDIFF(MINUTE,GSTIM.AcknowledgementDate, GETDATE()) < @_iGSTEInvoiceCancellationPeriodInMinute  
						 then 1 else 0 end IsPossibleToCancelledEInvoice,
					GSTIM.Irn
				from SalesInvoiceMaster A 
					inner join cte1 I ON(I.SalesInvoiceMasterID= A.ID)
				    inner join SalesInvoiceDetail D on (A.ID=D.SalesInvoiceMasterID
												         And D.ItemNumber=I.ItemNumber
														 and I.VarianceID = D.ID
												         And D.IsDeleted=0)
				    inner join GeneralItemMaster E on (I.ItemNumber=E.ItemNumber
													   And E.IsDeleted=0)		
				    inner join InventoryLocationMaster F on(F.ID=A.StorageLocationID
															and D.IsDeleted=0)	
				    inner join CustomerMaster G on (G.ID=A.CustomerMasterID
														And G.IsDeleted=0)
					inner join cteFinalTax on (cteFinalTax.ItemNumber=D.ItemNumber
											and cteFinalTax.SaleUomCode=D.SaleUomCode
											and cteFinalTax.VarianceID=D.ID
											)
					left outer join CustomerBranchMaster B on (A.CustomerBranchMasterID = B.ID
																and G.ID = B.CustomerMasterID)
					left join GSTEInvoiceMaster GSTIM on GSTIM.SalesInvoiceMasterID = A.ID
				   )
				   --select * from cte2
					SELECT
							cte3.SalesOrderMasterID
							,CentreCode
							,SellerGSTNumber
							,cte3.SalesOrderNumber						
							,cte3.CustomerName 
							,cte3.IsTaxExempted
							,cte3.Quantity
							,cte3.CustomerMasterID
							,cte3.ItemName as ItemDescription
							,cte3.ItemNumber
							,cte3.GeneralItemCodeID
							,cte3.SaleUOMcode
							,0 as BaseUOMQuantity
							,null as BaseUOMCode
							,cte3.Freight
							,cte3.ShippingHandling
							,cte3.Discount
							,cte3.TotalTaxAmount
							,cte3.Rate
							,cte3.LocationName
							,cte3.BatchNumber
							,cte3.ExpiryDate
							,cte3.CustomerInvoiceNumber
							,cte3.StorageLocationID as IssueFromLocationID
							,cte3.GeneralUnitsID
							,cte3.TaxGroupName
							,cte3.SalesOrderDeliveryMasterID
							,null as DMNumber
							,cte3.TaxRateList
							 ,cte3.TaxList
							 ,cte3.TaxAmountList
							 ,cte3.CustomerBranchMasterID
							 ,cte3.BranchName
							 ,cte3.TotalIInvoiceAmount
							 ,cte3.NetAmount
							 ,cte3.IsCanceled
							 ,cte3.ApprovalStatus
							 ,cte3.CancelApprovalStatus
							 ,cte3.CustomerGSTNumber
							 ,cte3.GSTEInvoiceMasterId
							 ,cte3.IsCancelledEInvoice
							 ,cte3.IsPossibleToCancelledEInvoice
							 ,cte3.Irn

						FROM 
							cte2 cte3
						group by CentreCode,cte3.SalesOrderNumber,cte3.SalesOrderMasterID,cte3.Quantity,cte3.CustomerName,cte3.CustomerMasterID,cte3.ItemName,
							cte3.ItemNumber,cte3.GeneralItemCodeID	,cte3.SaleUOMcode,cte3.Freight,cte3.ShippingHandling
						    ,cte3.Discount,cte3.TotalTaxAmount,cte3.Rate,cte3.LocationName,cte3.BatchNumber,cte3.ExpiryDate,cte3.CustomerInvoiceNumber,cte3.StorageLocationID,cte3.GeneralUnitsID,cte3.TaxGroupName
							,cte3.SalesOrderDeliveryMasterID,cte3.TaxRateList,cte3.TaxList,cte3.TaxAmountList,cte3.CustomerBranchMasterID,cte3.BranchName,cte3.TotalIInvoiceAmount
							 ,cte3.NetAmount,cte3.IsCanceled,cte3.ApprovalStatus,cte3.CancelApprovalStatus,cte3.CustomerGSTNumber
							  ,cte3.GSTEInvoiceMasterId,cte3.IsTaxExempted
							 ,cte3.IsCancelledEInvoice,SellerGSTNumber,cte3.IsPossibleToCancelledEInvoice,cte3.Irn
						order by cte3.SalesOrderDeliveryMasterID,cte3.ItemNumber
			
			End
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
	
End


