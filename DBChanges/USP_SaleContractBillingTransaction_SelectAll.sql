USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_SaleContractBillingTransaction_SelectAll]    Script Date: 12/7/2022 2:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[USP_SaleContractBillingTransaction_SelectAll]
	@iStartRow      INT ,
	@iEndRow        INT,
	@nsSearchBy       nvarchar(200),
	@sSortDirection varchar (10),
	@sSortBy        varchar(200),
	@iAdminRoleID	int,
	@iCustomerMasterID int,
	@iCustomerBranchMasterID int,
	@iErrorCode int OUTPUT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @SQLString nvarchar(4000);
DECLARE @ParmDefinition nvarchar(500);
DECLARE @valSortBy VARCHAR(250);
DECLARE @valSearchString nVARCHAR(250);
	BEGIN TRY
		BEGIN 
		SET @ParmDefinition = N'@StartRow int, @EndRow int'; 
		SET @valSortBy= @sSortBy; 
		SET @valSearchString = @nsSearchBy;
		-- SELECT First and Last rows as per Parameter set from the table.'; 
		SET @SQLString=concat (N' WITH cte_SaleContractBillingTransactionData as (
			select
				A.ID as SaleContractMasterID,
				A.ContractNumber,
				A.BillingType,
				B.ID as SaleContractBillingSpanID,
				CONCAT(CONVERT(varchar(30),B.StartDate,106),'' - '',CONVERT(varchar(30),B.EndDate,106)) as SaleContractBillingSpanName,
				B.IsBillGenerated,
				B.TotalBillAmount,
				B.RoundOffAmount,
				B.CreatedDate,
				B.ModifiedDate,
				C.CustomerInvoiceNumber,
				case when ISNULL(E.ID,0) = 0 then 0 else 1 end as IsTempBillGenerated,
				F.ID as GSTEInvoiceMasterId,
				F.IsCancelledEInvoice
			from
				SaleContractMaster A
				inner join SaleContractBillingSpan B on (A.ID = B.SaleContractMasterID and A.CustomerMasterID = ',@iCustomerMasterID,' and A.CustomerBranchMasterID = ',@iCustomerBranchMasterID,')
				inner join CustomerMaster D on (A.CustomerMasterID = D.ID
												and (ISNULL(D.IsCentre,0) = 0 or (ISNULL(D.IsCentre,0) = 1 and ',@iAdminRoleID,' in (11,13))))
				left outer join SalesInvoiceMaster C on (B.SalesInvoiceMasterID = C.ID)
				left outer join TempSalesInvoiceMaster E on (B.TempSalesInvoiceMasterID = E.ID)
				left join GSTEInvoiceMaster F on F.SalesInvoiceMasterID = C.ID
		), cte_SaleContractBillingTransaction AS
		(
			SELECT 
				SaleContractMasterID,
				ContractNumber,
				BillingType,
				SaleContractBillingSpanID,
				SaleContractBillingSpanName,
				IsBillGenerated,
				IsTempBillGenerated,
				TotalBillAmount,
				RoundOffAmount,
				CustomerInvoiceNumber,
				GSTEInvoiceMasterId,
				IsCancelledEInvoice,
				COUNT(*) OVER() AS TotalRecords,
				ROW_NUMBER() OVER(ORDER BY ', @valSortBy ,' ', @sSortDirection ,') AS RowNumber
			FROM
				cte_SaleContractBillingTransactionData
				 ');
		 IF ISNULL(@valSearchString,'')= ''   
		 begin 
			SET @SQLString= concat( @SQLString, ')');  
		 end		
		 else
		 begin
			SET @SQLString= concat( @SQLString, ' WHERE (' , @valSearchString , '))');  
		 end
		
		 SET @SQLString= concat( @SQLString,'	
											SELECT
												SaleContractMasterID,
												ContractNumber,
												BillingType,
												SaleContractBillingSpanID,
												SaleContractBillingSpanName,
												IsBillGenerated,
												IsTempBillGenerated,
												TotalBillAmount,
												RoundOffAmount,
												CustomerInvoiceNumber,
												GSTEInvoiceMasterId,
												IsCancelledEInvoice,
												TotalRecords
											FROM
												cte_SaleContractBillingTransaction
											WHERE
												RowNumber > @StartRow
												AND RowNumber <= @EndRow');
		-- Get the Error Code for the statement just executed.
	    --SELECT	@SQLString
		EXECUTE sp_executesql @SQLString, @ParmDefinition,
				@StartRow= @iStartRow,
				@EndRow = @iEndRow
				
	END; 
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End