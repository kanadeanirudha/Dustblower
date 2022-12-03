USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_SalesInvoiceMaster_ViewDetailsByInvoiceID]    Script Date: 12/3/2022 5:57:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select all category name list from the table 'USP_SalesInvoiceMaster_ViewDetailsByInvoiceID'
-- based on the Primary Key.
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_SalesInvoiceMaster_ViewDetailsByInvoiceID]
	@iSalesInvoiceMasterID int,
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY
			Begin

				
				;With CteFirst as(
				(
						select 
							 A.ID as SalesInvoiceMasterID
							,A.SalesOrderMasterID
							,B.ItemNumber
							,B.InvoiceQuantity
							,B.BatchNumber
							,B.ExpiryDate
							,B.SaleUomCode
							,C.Id as SalesOrderDeliveryMasterID
							,B.ID as SalesInvoiceDetailsID
						From
							SalesInvoiceMaster A 
							inner join SalesInvoiceDetail B on (A.ID=B.SalesInvoiceMasterID 
																and A.ID=@iSalesInvoiceMasterID and B.InvoiceQuantity>0)
							inner join SalesOrderDeliveryMaster C on (B.SalesOrderDeliveryMasterID=C.ID And C.IsInvoiced=1) 
									
				))

				 ,cte1 as(
						select 
								A.SalesOrderMasterID	,					
								A.InvoiceQuantity
								,A.ItemNumber
								,A.SaleUomCode
								,A.SalesOrderDeliveryMasterID
								,A.SalesInvoiceDetailsID
								,A.SalesInvoiceMasterID
								,A.BatchNumber
								,A.ExpiryDate
								
						 from 
								CteFirst A
				)
				,cteTaxes as(
					select 
						sum((A.Rate * cte1.InvoiceQuantity * D.TaxRate)/100) as TaxAmount
						,cte1.SalesInvoiceDetailsID
						,D.IsOtherState
						,B.TaxGroupName
						,Sum(D.TaxRate) as TaxRate
						,cte1.SaleUomCode
						,A.ItemNumber
						,cte1.SalesOrderDeliveryMasterID
						,D.TaxName
					from
						cte1 
						inner join SalesInvoiceDetail A on (cte1.SalesInvoiceDetailsID = A.ID
																and cte1.ItemNumber = A.ItemNumber)
						inner join GeneralTaxGroupMaster B on (A.GenTaxGroupMasterID  = B.ID)
						inner join GeneralTaxGroupMasterDetails C on (B.ID = C.GenTaxGroupMasterID)
						inner join GeneralTaxMaster D on (C.GenTaxMasterID = D.ID 
															and D.IsOtherState = 0)
					group by cte1.SalesInvoiceDetailsID,D.IsOtherState,B.TaxGroupName,cte1.SaleUomCode,A.ItemNumber,cte1.SalesOrderDeliveryMasterID,B.ID,D.ID,D.TaxName
					)
					
				,cteFinalTax as (
				select 
					Sum(TaxAmount) as TaxAmount 
					,TaxGroupName
					,itemnumber
					,SalesOrderDeliveryMasterID
					,SalesInvoiceDetailsID
					,SaleUomCode
					,TaxRateList=stuff((
									select
										', ' +  cast (E.TaxRate as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.SalesInvoiceDetailsID=E.SalesInvoiceDetailsID
									
									order by E.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,TaxList=stuff((
									select
										', ' +  cast (E.TaxName as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.SalesInvoiceDetailsID=E.SalesInvoiceDetailsID
										
										--and cte1.ItemNumber=A.ItemNumber
									order by E.TaxName

									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
					,TaxAmountList=stuff((
									select
										', ' +  cast (E.TaxAmount as varchar)    
									from  
										cteTaxes E 
									where
										A.TaxGroupName = E.TaxGroupName and A.ItemNumber=E.ItemNumber and A.SalesInvoiceDetailsID=E.SalesInvoiceDetailsID
										
										--and cte1.ItemNumber=A.ItemNumber
									order by E.TaxName

									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
									
				from 
					cteTaxes A
				group by TaxGroupName,itemnumber,SalesOrderDeliveryMasterID,SalesInvoiceDetailsID,SaleUomCode
					)

				
				,cte2 as (
				select
					A.ID as SalesOrderDeliveryMasterID ,
					D.ID as SalesInvoiceDetailsID,
					A.SalesOrderMasterID,
					M.DeliveryNumber,
					M.DeliveryTransDate,
					null as ReceivingLocationID,
					null as Remark,
					D.GeneralItemCodeID,
					D.ItemNumber,
					null as BarCode,
					D.SaleUOMcode,
					null as BaseUOMQuantity,
					null as BaseUOMCode,
					A.CustomerMasterID,
					A.CustomerBranchMasterID,
					null as Freight,
					cast(cteFinalTax.TaxAmount as money) as TotalTaxAmount,
					cteFinalTax.TaxGroupName,
					null as SalesOrderNumber,
					null as SalesOrderDate,
					null as ShippingHandling,
					null as Discount,
					D.ID,
					D.Rate,
					E.ItemDescription as ItemName,
					F.LocationName,
					case when CustomerType=1 then CONCAT(G.FirstName,' ',G.MiddleName,' ',G.LastName) 
						  else CompanyName end as CustomerName,
					B.BranchName,
					I.InvoiceQuantity as Quantity,
					A.CustomerInvoiceNumber,
					A.StorageLocationID as IssueFromLocationID,
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
					GSTIM.IsCancelledEInvoice
				from SalesInvoiceMaster A 
					inner join cte1 I ON(I.SalesOrderMasterID= A.SalesOrderMasterID and A.ID=I.SalesInvoiceMasterID
												and A.id = @iSalesInvoiceMasterID)
				    inner join SalesInvoiceDetail D on (A.ID=D.SalesInvoiceMasterID
												         And D.ItemNumber=I.ItemNumber
												         And D.IsDeleted=0)
				    inner join GeneralItemMaster E on (I.ItemNumber=E.ItemNumber
													   And E.IsDeleted=0)		
				    inner join InventoryLocationMaster F on(F.ID=A.StorageLocationID
															and D.IsDeleted=0)	
				    inner join CustomerMaster G on (G.ID=A.CustomerMasterID
														And G.IsDeleted=0)
					inner join cteFinalTax on (cteFinalTax.SalesInvoiceDetailsID = D.ID
											and cteFinalTax.ItemNumber=D.ItemNumber
											and cteFinalTax.SaleUomCode=D.SaleUomCode
											--and cteFinalTax.SalesOrderDeliveryMasterID=A.ID
											)
					inner join SalesOrderDeliveryMaster M on (A.SalesOrderMasterID = M.SalesOrderMasterID and M.IsDeleted = 0
															and D.SalesOrderDeliveryMasterID = M.ID)
					left outer join CustomerBranchMaster B on (A.CustomerBranchMasterID = B.ID
																and B.CustomerMasterID = G.ID)
					left join GSTEInvoiceMaster GSTIM on GSTIM.SalesInvoiceMasterID = A.ID
				
				   )
					SELECT
							cte3.SalesOrderMasterID,
							cte3.SalesOrderNumber						
							,cte3.CustomerName 
							,cte3.Quantity
							,cte3.CustomerMasterID
							,cte3.ItemName as ItemDescription
							,cte3.ItemNumber
							,cte3.GeneralItemCodeID
							,cte3.SaleUOMcode
							,cte3.BaseUOMQuantity
							,cte3.BaseUOMCode
							,cte3.Freight
							,cte3.ShippingHandling
							,cte3.Discount
							,cte3.TotalTaxAmount
							,cte3.Rate
							,cte3.LocationName
							,cte3.BatchNumber
							,cte3.ExpiryDate
							,cte3.CustomerInvoiceNumber
							,cte3.IssueFromLocationID
							,cte3.GeneralUnitsID
							,cte3.TaxGroupName
							,cte3.SalesOrderDeliveryMasterID
							,cte3.DeliveryNumber as DMNumber
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
						FROM 
							cte2 cte3
						group by cte3.SalesOrderNumber,cte3.SalesOrderMasterID,cte3.Quantity,cte3.CustomerName,cte3.CustomerMasterID,cte3.ItemName,
							cte3.ItemNumber,cte3.GeneralItemCodeID	,cte3.SaleUOMcode,cte3.BaseUOMQuantity,cte3.BaseUOMCode,cte3.Freight,cte3.ShippingHandling
						    ,cte3.Discount,cte3.TotalTaxAmount,cte3.Rate,cte3.LocationName,cte3.BatchNumber,cte3.ExpiryDate,cte3.CustomerInvoiceNumber,cte3.IssueFromLocationID,cte3.GeneralUnitsID,cte3.TaxGroupName
							,cte3.SalesOrderDeliveryMasterID,cte3.DeliveryNumber,cte3.TaxRateList,cte3.TaxList,cte3.TaxAmountList,cte3.CustomerBranchMasterID
							 ,cte3.BranchName,cte3.TotalIInvoiceAmount,cte3.NetAmount,cte3.IsCanceled,cte3.SalesInvoiceDetailsID,cte3.ApprovalStatus,cte3.CancelApprovalStatus,cte3.CustomerGSTNumber
							 ,cte3.GSTEInvoiceMasterId
							 ,cte3.IsCancelledEInvoice
						order by cte3.SalesInvoiceDetailsID
			
			End
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
	
End



