USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GSTEInvoiceResponse_Insert]    Script Date: 12/2/2022 10:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or ALTER PROCEDURE [dbo].[USP_GSTEInvoiceResponse_Insert]
	@iSalesInvoiceMasterID int,
	@sAcknowledgementNo varchar(50),	
	@sAcknowledgementDate varchar(50),	
	@sIrn  varchar(200),	
	@sQrCodeImage varchar(max),
	@bIsCancelledEInvoice bit,
	@sGSTEInvoiceDetails varchar(max),
	@iErrorCode int OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		declare @id int 
		INSERT INTO [dbo].[GSTEInvoiceMaster]
			   ([SalesInvoiceMasterID]
			   ,[AcknowledgementNo]
			   ,[AcknowledgementDate]
			   ,[Irn]
			   ,[QrCodeImage]
			   ,[IsCancelledEInvoice]
			   ,[CancelledEInvoiceDate]
			   ,[CancelledEInvoiceReason]
			   ,[CancelledEInvoiceDescription])
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
			   )
		-- Get the Error Code for the statement just executed.
		SELECT @iErrorCode=@@ERROR
		SET @id=SCOPE_IDENTITY()


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











