CREATE DATABASE [QuickSQL.Test]
GO

USE [QuickSQL.Test]
GO

CREATE TABLE [dbo].[TokenBalances] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Token]  NVARCHAR (MAX) NULL,
    [Owner]  NVARCHAR (MAX) NULL,
    [Amount] NVARCHAR (MAX) NULL
);
GO

SET IDENTITY_INSERT [dbo].[TokenBalances] ON
INSERT INTO [dbo].[TokenBalances] ([Id], [Token], [Owner], [Amount]) VALUES (1, N'ADH', N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', N'400')
INSERT INTO [dbo].[TokenBalances] ([Id], [Token], [Owner], [Amount]) VALUES (2, N'Poolz', N'0x2a01ee5557c9d69c35577496555b1bc95558b552', N'300')
INSERT INTO [dbo].[TokenBalances] ([Id], [Token], [Owner], [Amount]) VALUES (3, N'ETH', N'0x3a31ee5557c9369c35573496555b1bc93553b553', N'200')
INSERT INTO [dbo].[TokenBalances] ([Id], [Token], [Owner], [Amount]) VALUES (4, N'BTH', N'0x4a71ee5577c9d79c37577496555b1bc95558b554', N'100')
SET IDENTITY_INSERT [dbo].[TokenBalances] OFF

SELECT * FROM [dbo].[TokenBalances]
