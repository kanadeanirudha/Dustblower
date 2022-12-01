USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[USP_OrganisationCentrewiseGSTCredentialBy_CentreCode]    Script Date: 11/29/2022 9:26:28 PM ******/
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
Create Or ALTER PROCEDURE [dbo].[USP_OrganisationCentrewiseGSTCredentialBy_CentreCode]
	@nsCentreCode			 nvarchar(200),
	@bIsLiveMode			 bit,
	@iErrorCode			 int OUTPUT
AS
--exec USP_OrganisationCentrewiseGSTCredentialBy_CentreCode @nsCentreCode='HO',@bIsLiveMode=0, @iErrorCode=0
BEGIN
	BEGIN TRY
		BEGIN 

			select 
			   a.[ID]
			  ,a.[CentreCode]
			  ,b.GSTINNumber
			  ,a.[Version]
			  ,a.[Urls]
			  ,a.[EInvoiceUserName]
			  ,a.[EInvoicePassword]
			  ,a.[AspId]
			  ,a.[AspUserPassword]
			  ,a.[QrCodeSize]
			  ,case when  getdate() < a.[TokenExpiry] then  AuthToken
			   else ''
			   end AuthToken
			  ,IsNull(a.[TokenExpiry],'') TokenExpiry
			  ,IsNull(a.[ClientId],'') ClientId
			from OrganisationCentreWiseGSTCredential a
			inner join OrganisationStudyCentreMaster b on a.CentreCode = b.CentreCode
			where a.CentreCode = @nsCentreCode
			      and a.IsLiveMode=@bIsLiveMode
				  and b.GSTINNumber is not null

		END; 
	END TRY
	BEGIN CATCH
		SELECT  @iErrorCode =@@ERROR
		--	SELECT ERROR_MESSAGE();
	END CATCH
End

