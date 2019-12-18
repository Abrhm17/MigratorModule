
use [Test2]


/*
drop TABLE Addresses
drop TABLE ClientAccount
drop TABLE Account
drop TABLE Client
drop TABLE Company
*/


/* Create schema */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO



CREATE TABLE [dbo].[Company](
	[CompanyRef] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [varchar](20) NOT NULL,
	[CompanyName] [varchar](50) NOT NULL
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyRef] ASC
),
 CONSTRAINT [IX_CompanyID] UNIQUE NONCLUSTERED 
(
	[CompanyID] ASC
)
)
GO



CREATE TABLE [dbo].[Client](
	[ClientRef] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [varchar](20) NOT NULL,
	[ClientName] [varchar](150) NULL,
	[SortName] [varchar](30) NULL,
	[CalculationDate] [datetime] NULL,
	[CompanyRef] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientRef] ASC
),
 CONSTRAINT [IX_UnqClient] UNIQUE NONCLUSTERED 
(
	[ClientID] ASC,
	[CompanyRef] ASC
)
)
GO

ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Company] FOREIGN KEY([CompanyRef])
REFERENCES [dbo].[Company] ([CompanyRef])
GO

ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Company]
GO



CREATE TABLE [dbo].[Account](
	[AccountRef] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](20) NOT NULL,
	[AccountName] [varchar](150) NULL,
	[CompanyRef] [int] NOT NULL,
	[IsCashAccount] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountRef] ASC
),
 CONSTRAINT [IX_UnqAccount] UNIQUE NONCLUSTERED 
(
	[AccountID] ASC,
	[CompanyRef] ASC
)
)
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Company] FOREIGN KEY([CompanyRef])
REFERENCES [dbo].[Company] ([CompanyRef])
GO

ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Company]
GO



CREATE TABLE [dbo].[ClientAccount](
	[ClientAccountRef] [int] IDENTITY(1,1) NOT NULL,
	[ClientRef] [int] NOT NULL,
	[AccountRef] [int] NOT NULL,
	[Ownership] [decimal](5, 4) NOT NULL,
 CONSTRAINT [PK_ClientAccount] PRIMARY KEY CLUSTERED 
(
	[ClientAccountRef] ASC
),
 CONSTRAINT [IX_ClientAccount] UNIQUE NONCLUSTERED 
(
	[ClientRef] ASC,
	[AccountRef] ASC
)
)
GO

ALTER TABLE [dbo].[ClientAccount]  WITH CHECK ADD  CONSTRAINT [FK_ClientAccount_Account] FOREIGN KEY([AccountRef])
REFERENCES [dbo].[Account] ([AccountRef])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ClientAccount] CHECK CONSTRAINT [FK_ClientAccount_Account]
GO

ALTER TABLE [dbo].[ClientAccount]  WITH CHECK ADD  CONSTRAINT [FK_ClientAccount_Client] FOREIGN KEY([ClientRef])
REFERENCES [dbo].[Client] ([ClientRef])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ClientAccount] CHECK CONSTRAINT [FK_ClientAccount_Client]
GO



CREATE TABLE [dbo].[Addresses](
	[AddressRef] [int] IDENTITY(1,1) NOT NULL,
	[ClientRef] [int] NULL,
	[AddressName] [varchar](200) NULL,
	[AddressLine1] [varchar](50) NOT NULL,
	[AddressLine2] [varchar](50) NULL,
	[AddressLine3] [varchar](50) NULL,
	[Postcode] [varchar](50) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressRef] ASC
)
)
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Client] FOREIGN KEY([ClientRef])
REFERENCES [dbo].[Client] ([ClientRef])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Client]
GO



SET ANSI_PADDING OFF
GO

SET ANSI_PADDING ON
GO



/* Create sample data */

INSERT INTO [dbo].[Company]
           ([CompanyID]
           ,[CompanyName])
     VALUES
           ('ZZZ', 'The Lazy Company'),
		   ('ABC', 'ABC Trading Limited')
GO



/*
select * from Company
select * from Client
select * from Account
select * from ClientAccount
select * from Addresses
*/

