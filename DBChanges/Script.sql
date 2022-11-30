USE [Ver1]
GO

/****** Object:  Table [dbo].[OrganisationCentreWiseGSTCredential]    Script Date: 11/29/2022 6:09:57 PM ******/
DROP TABLE [dbo].[OrganisationCentreWiseGSTCredential]
GO

/****** Object:  Table [dbo].[OrganisationCentreWiseGSTCredential]    Script Date: 11/29/2022 6:09:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[OrganisationCentrewiseGSTCredential](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](30) NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[Urls] [varchar](max) NOT NULL,
	[EInvoiceUserName] [varchar](200) NOT NULL,
	[EInvoicePassword] [varchar](200) NOT NULL,
	[AspId] [varchar](200) NOT NULL,
	[AspUserPassword] [varchar](200) NOT NULL,
	[QrCodeSize] [int] NULL,
	[AuthToken] [varchar](200) NULL,
	[TokenExpiry] [varchar](200) NULL,
	[ClientId] [varchar](200) NULL,
	[IsLiveMode] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrganisationCentreWiseGSTCredential] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



INSERT INTO [dbo].[OrganisationCentreWiseGSTCredential]
           ([CentreCode]
           ,[Version]
           ,[Urls]
           ,[EInvoiceUserName]
           ,[EInvoicePassword]
           ,[AspId]
           ,[AspUserPassword]
           ,[QrCodeSize]
           ,[AuthToken]
           ,[TokenExpiry]
           ,[ClientId]
           ,[IsLiveMode]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           ('HO'
		   ,'1.1'
		   ,'https://gstsandbox.charteredinfo.com'
           ,'dustblowersnagpur'
           ,'admin@123456'
           ,'1712482722'
           ,'Gst@1007'
          ,250
           ,null
           ,null
		   ,'AACCC29GSPR5CM0'
           ,0
           ,1
           ,getdate()
           ,1
           ,getdate()
		   )
GO
