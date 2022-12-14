USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_GSTEInvoiceResponse_Insert_Update]    Script Date: 12/14/2022 9:01:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[USP_GSTEInvoiceResponse_Insert_Update]
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
		declare @id int =0;
		select @id = ID from GSTEInvoiceMaster where SalesInvoiceMasterID = @iSalesInvoiceMasterID
		if(@bIsCancelledEInvoice = 0 and @id = 0 )
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
		--else if(@bIsCancelledEInvoice = 0 and @id > 0 )
		--Begin
		--	update GSTEInvoiceMaster
		--	set	 
		--		 AcknowledgementNo = @sAcknowledgementNo
		--		,AcknowledgementDate = @sAcknowledgementDate
		--		,Irn= @sIrn
		--		,QrCodeImage=@sQrCodeImage
		--		,IsCancelledEInvoice = @bIsCancelledEInvoice
		--		,CancelledEInvoiceDate = null
		--		,CancelledEInvoiceReason =null
		--		,CancelledEInvoiceDescription=null
		--		,CreatedBy=@iCreatedBy
		--		,CreatedDate= GETDATE()
		--	where Id = @id and SalesInvoiceMasterID= @iSalesInvoiceMasterID;
		--End
		else if(@bIsCancelledEInvoice = 1 and @id > 0 )
		begin
			update GSTEInvoiceMaster
			set	 Irn= @sIrn
				,QrCodeImage=null
				,IsCancelledEInvoice = @bIsCancelledEInvoice
				,CancelledEInvoiceDate = @sCancelledEInvoiceDate
				,CancelledEInvoiceReason =@sCancelledEInvoiceReason
				,CancelledEInvoiceDescription=@CancelledEInvoiceDescription
				,CreatedBy=@iCreatedBy
				,CreatedDate= GETDATE()
			where Id = @id and SalesInvoiceMasterID= @iSalesInvoiceMasterID;
		End

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
	
		SELECT @iErrorCode=@@ERROR
		
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT  @iErrorCode =@@ERROR
	END CATCH
End











