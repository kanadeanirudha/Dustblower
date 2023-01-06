USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GeneralItemMaster_SearchlistForCustomerWithTax]    Script Date: 1/7/2023 12:49:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'USP_GeneralItemMaster_SearchlistForCustomerWithTax'
-- based on the Primary Key.
-- Gets: @iID int 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[USP_GeneralItemMaster_SearchlistForCustomerWithTax]
	@sSearchWord NVARCHAR(150),
	@iGeneralUnitID int,
	@iCustomerMasterID int,
	@iCustomerBranchMasterID int,
	@iErrorCode int OUTPUT
AS
BEGIN
Declare @sSearchWord1 nvarchar(150),@bIsOtherFlag bit,@bIsTaxExempted bit;
SET @sSearchWord1 =  @sSearchWord
SET @sSearchWord = '%' +@sSearchWord + '%'
	BEGIN TRY
	Declare @iArticalCount int;
	Select @bIsOtherFlag=dbo.fGetIsOtherfromCustomerMasterByUnit (@iCustomerMasterID,@iCustomerBranchMasterID,@iGeneralUnitID)
	--Select @bIsOtherFlag as flag

	select 
		@bIsTaxExempted = case when ISNULL(B.ID,0) <> 0 then ISNULL(B.IsTaxExempted,0) else ISNULL(A.IsTaxExempted,0) end 
	from CustomerMaster A 
		left outer join CustomerBranchMaster B on (A.ID = B.CustomerMasterID
													and (B.ID = @iCustomerBranchMasterID or @iCustomerBranchMasterID = 0))
	where A.ID = @iCustomerMasterID

	DECLARE @TBL_ItemData TABLE (ItemNumber int,ItemDescription nvarchar(150),GenTaxGroupMasterId int,SerialAndBatchManagedBy tinyint,PurchaseUoMCode nvarchar(35), PurchasePrice money,ConversionFactor float)
	

		Insert @TBL_ItemData
			(
			ItemNumber,
			ItemDescription,
			GenTaxGroupMasterId,
			SerialAndBatchManagedBy,
			PurchaseUoMCode,
			PurchasePrice,
			ConversionFactor

			)
		select  
			A.ItemNumber,
			A.ItemDescription,
			A.GenTaxGroupMasterId,
			B.SerialAndBatchManagedBy,
			C.PurchaseUoMCode,
			C.LastPurchasePrice as PurchasePrice,
			D.ConversionFactor

		from GeneralItemMaster A 
		inner join GeneralItemGeneralData B on (A.ItemNumber=B.ItemNumber 
												and A.RetailSale=1
												and  A.ItemDescription = @sSearchWord1
												and A.IsDeleted=0 
												and B.IsDeleted=0)
		inner join GeneralItemSupliersData C on (A.ItemNumber=C.ItemNumber and C.IsDeleted=0 and IsDefaultVendor=1)
		inner join GeneralItemCode D on (D.ItemNumber=C.ItemNumber and C.PurchaseUoMCode=D.UomCode)

		select @iArticalCount=COUNT(*) from @TBL_ItemData
		if (@iArticalCount > 0)
			begin
			;With cteTaxes as(
						select 
								A.ID as GeneralTaxGroupMasterID,								
								C.TaxName,
								C.TaxRate,
								A.TaxGroupName
						 from 
								GeneralTaxGroupMaster A 
								inner join GeneralTaxGroupMasterDetails B ON(A.ID = B.GenTaxGroupMasterID
																			And A.IsDeleted = 0
																			And B.IsDeleted = 0)
								Inner join GeneralTaxMaster C ON(B.GenTaxMasterID = C.ID
																and C.IsOtherState=@bIsOtherFlag
																And C.IsDeleted = 0)
				)
				, cteTaxList as (

					select --top 1
						A.GeneralTaxGroupMasterID
						,TaxRateList=stuff((
									select
										', ' +  cast (B.TaxRate as varchar)    
									from  
										cteTaxes B 
									where
										A.GeneralTaxGroupMasterID = B.GeneralTaxGroupMasterID
									order by B.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,TaxList=stuff((
									select
										', ' +  cast (B.TaxName as varchar)    
									from  
										cteTaxes B 
									where
										A.GeneralTaxGroupMasterID = B.GeneralTaxGroupMasterID
									order by B.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,sum(TaxRate ) as TaxRate
						,A.TaxGroupName
					from
						cteTaxes A
					group by A.GeneralTaxGroupMasterID,A.TaxGroupName
				)
				select A.ItemNumber,
					   A.ItemDescription ,
					   A.GenTaxGroupMasterId,
					   A.SerialAndBatchManagedBy
					  ,A.PurchasePrice
					  ,A.PurchaseUoMCode
					  ,A.ConversionFactor
					  ,D.TaxList
					  ,D.TaxRateList
					  ,D.TaxRate
					  ,@bIsTaxExempted as IsTaxExempted
					  ,D.TaxGroupName
			from @TBL_ItemData A 	inner  join cteTaxList D on (D.GeneralTaxGroupMasterID=A.GenTaxGroupMasterId)
			end 
		Else
		begin
			;With cteTaxes as(
						select 
								A.ID as GeneralTaxGroupMasterID,								
								C.TaxName,
								C.TaxRate,
								A.TaxGroupName
						 from 
								GeneralTaxGroupMaster A 
								inner join GeneralTaxGroupMasterDetails B ON(A.ID = B.GenTaxGroupMasterID
																			And A.IsDeleted = 0
																			And B.IsDeleted = 0)
								Inner join GeneralTaxMaster C ON(B.GenTaxMasterID = C.ID
																and C.IsOtherState=@bIsOtherFlag
																And C.IsDeleted = 0)
				)
				, cteTaxList as (

					select --top 1
						A.GeneralTaxGroupMasterID
						,TaxRateList=stuff((
									select
										', ' +  cast (B.TaxRate as varchar)    
									from  
										cteTaxes B 
									where
										A.GeneralTaxGroupMasterID = B.GeneralTaxGroupMasterID
									order by B.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,TaxList=stuff((
									select
										', ' +  cast (B.TaxName as varchar)    
									from  
										cteTaxes B 
									where
										A.GeneralTaxGroupMasterID = B.GeneralTaxGroupMasterID
									order by B.TaxName
									FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 2, '')
						,sum(TaxRate ) as TaxRate
						,A.TaxGroupName
					from
						cteTaxes A
					group by A.GeneralTaxGroupMasterID,A.TaxGroupName
				)
				,cteItemdetails as(
				
				Select 
				 A.ItemNumber
				,A.ItemDescription
				,A.GenTaxGroupMasterId
				,B.SerialAndBatchManagedBy
				,C.PurchaseUoMCode,
				C.LastPurchasePrice as PurchasePrice,
				D.ConversionFactor

			from GeneralItemMaster A 
			inner join GeneralItemGeneralData B on (A.ItemNumber=B.ItemNumber 
												and A.RetailSale=1
												and  A.ItemDescription LIKE @sSearchWord
												and A.IsDeleted=0 
												and B.IsDeleted=0)
				inner join GeneralItemSupliersData C on (A.ItemNumber=C.ItemNumber and C.IsDeleted=0 and IsDefaultVendor=1)
				inner join GeneralItemCode D on (D.ItemNumber=C.ItemNumber and C.PurchaseUoMCode=D.UomCode)

				)
				
			Select 
				 A.ItemNumber
				,A.ItemDescription
				,A.GenTaxGroupMasterId
				,A.SerialAndBatchManagedBy
				,A.PurchasePrice
				,A.PurchaseUoMCode
				,A.ConversionFactor
				,D.TaxList
				,D.TaxRate
				,D.TaxRateList 
				,@bIsTaxExempted as IsTaxExempted
				,D.TaxGroupName
			from cteItemdetails A 
			 Left Outer Join  cteTaxList D on (D.GeneralTaxGroupMasterID=A.GenTaxGroupMasterId)
		end


	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End














