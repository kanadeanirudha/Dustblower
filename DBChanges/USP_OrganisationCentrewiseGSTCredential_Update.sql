USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_OrganisationCentrewiseGSTCredential_Update]    Script Date: 11/29/2022 9:26:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored procedure that will select all rows from the table '[USP_SalesInvoiceMaster_SelectAll]'
-- Gets: @iStartRow int 
-- Gets: @iEndRow int 
-- Gets: @sSortBy varchar(200) 
-- Returns: @iErrorCode int 
------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------
Create Or ALTER PROCEDURE [dbo].[USP_OrganisationCentrewiseGSTCredential_Update]
	@iID					int,
	@nsAuthToken			Nvarchar(200),
	@sTokenExpiry			Nvarchar(200),
	@iModifiedBy			int = 1,
	@iErrorCode				int OUTPUT
AS

BEGIN
	BEGIN TRY
		BEGIN 

			update OrganisationCentrewiseGSTCredential
			set AuthToken = @nsAuthToken, TokenExpiry = @sTokenExpiry, @iModifiedBy = @iModifiedBy,ModifiedDate = getdate()
			where Id= @iID
			SELECT @iErrorCode=@@ERROR
		END; 
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
		--	SELECT ERROR_MESSAGE();
	END CATCH
End




