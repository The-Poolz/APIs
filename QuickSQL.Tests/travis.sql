# Create Testuser
CREATE USER 'dev'@'localhost' IDENTIFIED BY 'dev';
GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP ON *.* TO 'dev'@'localhost';

# Create DB
CREATE DATABASE IF NOT EXISTS `QuickSQL.Test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `QuickSQL.Test`;

# Create table LeaderBoard
CREATE TABLE IF NOT EXISTS `LeaderBoard` (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Rank]   NVARCHAR (MAX) NULL,
    [Owner]  NVARCHAR (MAX) NULL,
    [Amount] NVARCHAR (MAX) NULL
);
SET IDENTITY_INSERT `LeaderBoard` ON
INSERT INTO `LeaderBoard` ([Id], [Rank], [Owner], [Amount]) VALUES (1, N'1', N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', N'750.505823765680934368')
INSERT INTO `LeaderBoard` ([Id], [Rank], [Owner], [Amount]) VALUES (2, N'2', N'0x2a01ee5557c9d69c35577496555b1bc95558b552', N'251.795264077704686136')
INSERT INTO `LeaderBoard` ([Id], [Rank], [Owner], [Amount]) VALUES (3, N'3', N'0x3a31ee5557c9369c35573496555b1bc93553b553', N'250.02109769151781894')
INSERT INTO `LeaderBoard` ([Id], [Rank], [Owner], [Amount]) VALUES (4, N'4', N'0x4a71ee5577c9d79c37577496555b1bc95558b554', N'233.279855562249360519')
SET IDENTITY_INSERT `LeaderBoard` OFF

# Create table SignUp
CREATE TABLE IF NOT EXISTS `SignUp` (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Address] NVARCHAR (MAX) NULL,
    [PoolId]  INT            NOT NULL
);
SET IDENTITY_INSERT `SignUp` ON
INSERT INTO `SignUp` ([Id], [Address], [PoolId]) VALUES (1, N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', 1)
INSERT INTO `SignUp` ([Id], [Address], [PoolId]) VALUES (2, N'0x2a01ee5557c9d69c35577496555b1bc95558b552', 2)
INSERT INTO `SignUp` ([Id], [Address], [PoolId]) VALUES (3, N'0x3a31ee5557c9369c35573496555b1bc93553b553', 3)
INSERT INTO `SignUp` ([Id], [Address], [PoolId]) VALUES (4, N'0x4a71ee5577c9d79c37577496555b1bc95558b554', 4)
SET IDENTITY_INSERT `SignUp` OFF

# Create table TokenBalances
CREATE TABLE IF NOT EXISTS `TokenBalances` (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Token]  NVARCHAR (MAX) NULL,
    [Owner]  NVARCHAR (MAX) NULL,
    [Amount] NVARCHAR (MAX) NULL
);
SET IDENTITY_INSERT `TokenBalances` ON
INSERT INTO `TokenBalances` ([Id], [Token], [Owner], [Amount]) VALUES (1, N'ADH', N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', N'400')
INSERT INTO `TokenBalances` ([Id], [Token], [Owner], [Amount]) VALUES (2, N'Poolz', N'0x2a01ee5557c9d69c35577496555b1bc95558b552', N'300')
INSERT INTO `TokenBalances` ([Id], [Token], [Owner], [Amount]) VALUES (3, N'ETH', N'0x3a31ee5557c9369c35573496555b1bc93553b553', N'200')
INSERT INTO `TokenBalances` ([Id], [Token], [Owner], [Amount]) VALUES (4, N'BTH', N'0x4a71ee5577c9d79c37577496555b1bc95558b554', N'100')
SET IDENTITY_INSERT `TokenBalances` OFF

# Create table Wallets
CREATE TABLE IF NOT EXISTS `Wallets` (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Owner] NVARCHAR (MAX) NULL
);
SET IDENTITY_INSERT `Wallets` ON
INSERT INTO `Wallets` ([Id], [Owner]) VALUES (1, N'0x1a01ee5577c9d69c35a77496565b1bc95588b521')
INSERT INTO `Wallets` ([Id], [Owner]) VALUES (2, N'0x2a01ee5557c9d69c35577496555b1bc95558b552')
INSERT INTO `Wallets` ([Id], [Owner]) VALUES (3, N'0x3a31ee5557c9369c35573496555b1bc93553b553')
INSERT INTO `Wallets` ([Id], [Owner]) VALUES (4, N'0x4a71ee5577c9d79c37577496555b1bc95558b554')
SET IDENTITY_INSERT `Wallets` OFF
