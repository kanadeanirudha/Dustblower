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


/****** Object:  Table [dbo].[GSTEInvoiceMaster]    Script Date: 12/1/2022 11:33:15 PM ******/
DROP TABLE [dbo].[GSTEInvoiceMaster]
GO

/****** Object:  Table [dbo].[GSTEInvoiceMaster]    Script Date: 12/1/2022 11:33:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GSTEInvoiceMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvoiceMasterID] [int] NOT NULL,
	[AcknowledgementNo] [varchar](50) NOT NULL,
	[AcknowledgementDate] [varchar](50) NOT NULL,
	[Irn] [varchar](200) NOT NULL,
	[QrCodeImage] [varchar](max) NULL,
	[IsCancelledEInvoice] [bit] NOT NULL,
	[CancelledEInvoiceDate] [varchar](200) NULL,
	[CancelledEInvoiceReason] [varchar](200) NULL,
	[CancelledEInvoiceDescription] [varchar](200) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
GO

/****** Object:  Table [dbo].[GSTEInvoiceDetails]    Script Date: 12/2/2022 9:17:51 AM ******/
DROP TABLE [dbo].[GSTEInvoiceDetails]
GO

/****** Object:  Table [dbo].[GSTEInvoiceDetails]    Script Date: 12/2/2022 9:17:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GSTEInvoiceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GSTEInvoiceMasterID] [int] NOT NULL,
	[SalesInvoiceMasterID] [int] NOT NULL,
	[GSTEInvoiceDetails] [varchar](max) NULL,
 CONSTRAINT [PK_GSTEInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
