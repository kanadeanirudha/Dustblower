USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GSTEInvoiceResponse_Insert_Update]    Script Date: 12/9/2022 11:01:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or ALTER   PROCEDURE [dbo].[USP_GSTEInvoiceResponse_Insert_Update]
	@iSalesInvoiceMasterID int,
	@sAcknowledgementNo varchar(50) = null,	
	@sAcknowledgementDate varchar(50) = null,
	@sIrn  varchar(200),	
	@sQrCodeImage varchar(max) = null,
	@bIsCancelledEInvoice bit,
	@sCancelledEInvoiceReason varchar(200) = null,
	@CancelledEInvoiceDescription varchar(200) = null,
	@sCancelledEInvoiceDate varchar(200) = null,
	@sGSTEInvoiceDetails varchar(max) = null,
	@iCreatedBy int,
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		declare @id int 
		if(@sCancelledEInvoiceReason = 0)
		Begin
			INSERT INTO [dbo].[GSTEInvoiceMaster]
				   ([SalesInvoiceMasterID]
				   ,[AcknowledgementNo]
				   ,[AcknowledgementDate]
				   ,[Irn]
				   ,[QrCodeImage]
				   ,[IsCancelledEInvoice]
				   ,[CancelledEInvoiceDate]
				   ,[CancelledEInvoiceReason]
				   ,[CancelledEInvoiceDescription]
				   ,CreatedBy
				   ,CreatedDate
				   )
			 VALUES
				   (@iSalesInvoiceMasterID
				   ,@sAcknowledgementNo
				   ,@sAcknowledgementDate
				   ,@sIrn
				   ,@sQrCodeImage
				   ,@bIsCancelledEInvoice
				   ,null
				   ,null
				   ,null
				   ,@iCreatedBy
				   ,GETDATE()
				   )
			 SET @id=SCOPE_IDENTITY()
		End
		else
		--begin
		--	update 
		--End
		-- Get the Error Code for the statement just executed.
		SELECT @iErrorCode=@@ERROR
		


		INSERT INTO [dbo].[GSTEInvoiceDetails]
           ([GSTEInvoiceMasterID]
           ,[GSTEInvoiceDetails])
		VALUES
           (@id
           ,@sGSTEInvoiceDetails
		   )

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End











