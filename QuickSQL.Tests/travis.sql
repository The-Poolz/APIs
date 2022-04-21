CREATE USER 'test'@'localhost' IDENTIFIED BY 'test';
GRANT ALL PRIVILEGES ON * . * TO 'test'@'localhost';

# Create DB 
CREATE DATABASE IF NOT EXISTS `QuickSQL.Test`;  
USE `QuickSQL.Test`;

# Create table LeaderBoard with data
CREATE TABLE IF NOT EXISTS `LeaderBoard` (
  `Id` int(100) NOT NULL,
  `Rank` varchar(200) DEFAULT NULL,
  `Owner` varchar(200) DEFAULT NULL,
  `Amount` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `LeaderBoard`
  ADD PRIMARY KEY (`Id`);
ALTER TABLE `LeaderBoard`
  MODIFY `Id` int(100) NOT NULL AUTO_INCREMENT;
INSERT INTO `LeaderBoard` (`Id`, `Rank`, `Owner`, `Amount`) VALUES (1, N'1', N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', N'750.505823765680934368');
INSERT INTO `LeaderBoard` (`Id`, `Rank`, `Owner`, `Amount`) VALUES (2, N'2', N'0x2a01ee5557c9d69c35577496555b1bc95558b552', N'251.795264077704686136');
INSERT INTO `LeaderBoard` (`Id`, `Rank`, `Owner`, `Amount`) VALUES (3, N'3', N'0x3a31ee5557c9369c35573496555b1bc93553b553', N'250.02109769151781894');
INSERT INTO `LeaderBoard` (`Id`, `Rank`, `Owner`, `Amount`) VALUES (4, N'4', N'0x4a71ee5577c9d79c37577496555b1bc95558b554', N'233.279855562249360519');
  
# Create table SignUp with data
CREATE TABLE IF NOT EXISTS `SignUp` (
  `Id` int(100) NOT NULL,
  `Address` varchar(200) DEFAULT NULL,
  `PoolId` int(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `SignUp`
  ADD PRIMARY KEY (`Id`);
ALTER TABLE `SignUp`
  MODIFY `Id` int(100) NOT NULL AUTO_INCREMENT;
INSERT INTO `SignUp` (`Id`, `Address`, `PoolId`) VALUES (1, N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', 1);
INSERT INTO `SignUp` (`Id`, `Address`, `PoolId`) VALUES (2, N'0x2a01ee5557c9d69c35577496555b1bc95558b552', 2);
INSERT INTO `SignUp` (`Id`, `Address`, `PoolId`) VALUES (3, N'0x3a31ee5557c9369c35573496555b1bc93553b553', 3);
INSERT INTO `SignUp` (`Id`, `Address`, `PoolId`) VALUES (4, N'0x4a71ee5577c9d79c37577496555b1bc95558b554', 4);

# Create table TokenBalances with data
CREATE TABLE IF NOT EXISTS `TokenBalances` (
  `Id` int(100) NOT NULL,
  `Token` varchar(200) DEFAULT NULL,
  `Owner` varchar(200) DEFAULT NULL,
  `Amount` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `TokenBalances`
  ADD PRIMARY KEY (`Id`);
ALTER TABLE `TokenBalances`
  MODIFY `Id` int(100) NOT NULL AUTO_INCREMENT;
INSERT INTO `TokenBalances` (`Id`, `Token`, `Owner`, `Amount`) VALUES (1, N'ADH', N'0x1a01ee5577c9d69c35a77496565b1bc95588b521', N'400');
INSERT INTO `TokenBalances` (`Id`, `Token`, `Owner`, `Amount`) VALUES (2, N'Poolz', N'0x2a01ee5557c9d69c35577496555b1bc95558b552', N'300');
INSERT INTO `TokenBalances` (`Id`, `Token`, `Owner`, `Amount`) VALUES (3, N'ETH', N'0x3a31ee5557c9369c35573496555b1bc93553b553', N'200');
INSERT INTO `TokenBalances` (`Id`, `Token`, `Owner`, `Amount`) VALUES (4, N'BTH', N'0x4a71ee5577c9d79c37577496555b1bc95558b554', N'100');

# Create table Wallets with data
CREATE TABLE IF NOT EXISTS `Wallets` (
  `Id` int(100) NOT NULL,
  `Owner` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `Wallets`
  ADD PRIMARY KEY (`Id`);
ALTER TABLE `Wallets`
  MODIFY `Id` int(100) NOT NULL AUTO_INCREMENT;
INSERT INTO `Wallets` (`Id`, `Owner`) VALUES (1, N'0x1a01ee5577c9d69c35a77496565b1bc95588b521');
INSERT INTO `Wallets` (`Id`, `Owner`) VALUES (2, N'0x2a01ee5557c9d69c35577496555b1bc95558b552');
INSERT INTO `Wallets` (`Id`, `Owner`) VALUES (3, N'0x3a31ee5557c9369c35573496555b1bc93553b553');
INSERT INTO `Wallets` (`Id`, `Owner`) VALUES (4, N'0x4a71ee5577c9d79c37577496555b1bc95558b554');
  
SHOW TABLES;
SELECT * FROM `LeaderBoard`;
SELECT * FROM `SignUp`;
SELECT * FROM `TokenBalances`;
SELECT * FROM `Wallets`;

select (@dbConnection)
