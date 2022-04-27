# Create DB 
CREATE DATABASE IF NOT EXISTS `QuickSQL.Test`;  
USE `QuickSQL.Test`;

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
  
SELECT * FROM `TokenBalances`;