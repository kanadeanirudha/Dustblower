USE [Ver1]
GO
/****** Object:  StoredProcedure [dbo].[RARIndia_GetCountryList]    Script Date: 12/14/2022 4:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create or ALTER  PROCEDURE [dbo].[RARIndia_GetCountryList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec RARIndia_GetCountryList @WhereClause=null,@Rows=100,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_CountryDetail TABLE
			 (
				 CountryId			smallint,
				 CountryName		NVARCHAR(60),
				 ContryCode			NVARCHAR(50),
				 DefaultFlag		BIT,
				 RowId				INT,
				 CountNo			INT
			 )

             SET @SQL = '
						;with Cte_filterCountry AS 
						(
							SELECT ID CountryId,ContryCode,CountryName,'
							+dbo.Fn_GetPagingRowId(@Order_By,'DefaultFlag')+',Count(*)Over() CountNo 
							FROM GeneralCountryMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  CountryId,ContryCode,CountryName,RowId,CountNo
						FROM Cte_filterCountry
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_CountryDetail (CountryId,ContryCode,CountryName,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_CountryDetail),0)
			 SELECT CountryId,ContryCode,CountryName
			 FROM @TBL_CountryDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC RARIndia_GetCountryList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'RARIndia_GetCountryList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
