USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GSTEWayBillResponse_Insert_Update]    Script Date: 12/24/2022 12:27:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 Create or ALTER     PROCEDURE [dbo].[USP_GSTEWayBillResponse_Insert_Update]
	@iSalesInvoiceMasterID int,
	@sEwbNo  varchar(200)= null,	
	@sEwbDt  varchar(200)= null,	
	@sEwbValidTill  varchar(200)= null,	
	@sGSTEInvoiceDetails varchar(max) = null,
	@iCreatedBy int,
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		declare @id int =0;
		select @id = ID from GSTEInvoiceMaster where SalesInvoiceMasterID = @iSalesInvoiceMasterID
		
		update GSTEInvoiceMaster
		set	 EwbNo= @sEwbNo
			,EwbDt=@sEwbDt
			,EwbValidTill = @sEwbValidTill
			,CreatedBy=@iCreatedBy
			,CreatedDate= GETDATE()
		where Id = @id and SalesInvoiceMasterID= @iSalesInvoiceMasterID;

		if @sGSTEInvoiceDetails is not null and @sGSTEInvoiceDetails !=''
		Begin
			-- Get the Error Code for the statement just executed.
			INSERT INTO [dbo].[GSTEInvoiceDetails]
			   ([GSTEInvoiceMasterID]
			   ,[SalesInvoiceMasterID]
			   ,[GSTEInvoiceDetails])
			VALUES
			   (@id
			   ,@iSalesInvoiceMasterID
			   ,@sGSTEInvoiceDetails
			   )
	    End
		SELECT @iErrorCode=@@ERROR
		
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End











