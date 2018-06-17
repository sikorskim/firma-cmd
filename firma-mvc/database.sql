-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               10.3.7-MariaDB - mariadb.org binary distribution
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Zrzut struktury bazy danych firma
CREATE DATABASE IF NOT EXISTS `firma` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `firma`;

-- Zrzut struktury tabela firma.aspnetroleclaims
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetroleclaims: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetroles
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetroles: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetuserclaims
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetuserclaims: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetuserlogins
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetuserlogins: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetuserroles
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetuserroles: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetusers
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetusers: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;

-- Zrzut struktury tabela firma.aspnetusertokens
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.aspnetusertokens: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;

-- Zrzut struktury tabela firma.contractor
CREATE TABLE IF NOT EXISTS `contractor` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NIP` longtext DEFAULT NULL,
  `FullName` longtext DEFAULT NULL,
  `Name` longtext DEFAULT NULL,
  `CountryCode` longtext DEFAULT NULL,
  `Voivodeship` longtext DEFAULT NULL,
  `County` longtext DEFAULT NULL,
  `Community` longtext DEFAULT NULL,
  `City` longtext DEFAULT NULL,
  `Street` longtext DEFAULT NULL,
  `BuldingNo` longtext DEFAULT NULL,
  `PostalCode` longtext DEFAULT NULL,
  `PostOffice` longtext DEFAULT NULL,
  `Email` longtext DEFAULT NULL,
  `Phone` longtext DEFAULT NULL,
  `InvoiceIssueCity` longtext DEFAULT NULL,
  `InvoiceIssuerName` longtext DEFAULT NULL,
  `Discriminator` longtext NOT NULL DEFAULT '',
  `BankAccountNumber` longtext DEFAULT NULL,
  `BankName` longtext DEFAULT NULL,
  `REGON` longtext DEFAULT NULL,
  `Website` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.contractor: ~1 rows (około)
/*!40000 ALTER TABLE `contractor` DISABLE KEYS */;
REPLACE INTO `contractor` (`Id`, `NIP`, `FullName`, `Name`, `CountryCode`, `Voivodeship`, `County`, `Community`, `City`, `Street`, `BuldingNo`, `PostalCode`, `PostOffice`, `Email`, `Phone`, `InvoiceIssueCity`, `InvoiceIssuerName`, `Discriminator`, `BankAccountNumber`, `BankName`, `REGON`, `Website`) VALUES
	(1, '77788855552', 'Studio reklamy JAG Jarosław Gadziński', 'Studio reklamy JAG', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Wojciechowskiego', '6', '63-100', 'Śrem', 'biuro@reklamajag.pl', '6128355555', NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL),
	(2, '7851769827', 'Computerman Maciej Sikorski', 'Computerman', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Leśna', '15', '63-100', 'Śrem', 'biuro@computerman.pl', '796655550', 'Śrem', 'Maciej Sikorski', 'Company', '0032422 24242 42424', 'PKO BP S.A.', '300200100', 'www.computerman.pl'),
	(3, '787878787878', 'Szpital powiatowy im. Tadeusza Malińskiego w Śremie Sp. z o.o.', 'Szpital powiatowy im. T. Malińskiego w Śremie Sp. z o.o.', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Chełmońskiego', '1', '63-100', 'Śrem', 'sekretariat@szpital-srem.pl', '6128154000', NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL);
/*!40000 ALTER TABLE `contractor` ENABLE KEYS */;

-- Zrzut struktury tabela firma.fixedassets
CREATE TABLE IF NOT EXISTS `fixedassets` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DateOfBuy` datetime(6) NOT NULL,
  `DateOfUseStart` datetime(6) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `Identfier` longtext DEFAULT NULL,
  `OriginalValue` decimal(65,30) NOT NULL,
  `DepreciationRate` decimal(65,30) NOT NULL,
  `UpgradeValue` decimal(65,30) NOT NULL,
  `UpdatedOriginalValue` decimal(65,30) NOT NULL,
  `LiquidationDate` datetime(6) NOT NULL,
  `LiquidationReason` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.fixedassets: ~0 rows (około)
/*!40000 ALTER TABLE `fixedassets` DISABLE KEYS */;
/*!40000 ALTER TABLE `fixedassets` ENABLE KEYS */;

-- Zrzut struktury tabela firma.invoice
CREATE TABLE IF NOT EXISTS `invoice` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` longtext DEFAULT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `ContractorId` int(11) NOT NULL,
  `PaymentMethodId` int(11) NOT NULL,
  `InvoiceStatusId` int(11) NOT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  `DateOfDelivery` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Invoice_ContractorId` (`ContractorId`),
  KEY `IX_Invoice_InvoiceStatusId` (`InvoiceStatusId`),
  KEY `IX_Invoice_PaymentMethodId` (`PaymentMethodId`),
  KEY `IX_Invoice_CompanyId` (`CompanyId`),
  CONSTRAINT `FK_Invoice_Contractor_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `contractor` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Invoice_Contractor_ContractorId` FOREIGN KEY (`ContractorId`) REFERENCES `contractor` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Invoice_InvoiceStatus_InvoiceStatusId` FOREIGN KEY (`InvoiceStatusId`) REFERENCES `invoicestatus` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Invoice_PaymentMethod_PaymentMethodId` FOREIGN KEY (`PaymentMethodId`) REFERENCES `paymentmethod` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.invoice: ~0 rows (około)
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
REPLACE INTO `invoice` (`Id`, `Number`, `DateOfIssue`, `ContractorId`, `PaymentMethodId`, `InvoiceStatusId`, `CompanyId`, `DateOfDelivery`) VALUES
	(1, 'FV/2018/6/1', '2018-06-16 00:00:00.000000', 1, 1, 1, NULL, '0001-01-01 00:00:00.000000');
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;

-- Zrzut struktury tabela firma.invoiceitem
CREATE TABLE IF NOT EXISTS `invoiceitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceId` int(11) NOT NULL,
  `ItemId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_InvoiceItem_InvoiceId` (`InvoiceId`),
  KEY `IX_InvoiceItem_ItemId` (`ItemId`),
  CONSTRAINT `FK_InvoiceItem_Invoice_InvoiceId` FOREIGN KEY (`InvoiceId`) REFERENCES `invoice` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_InvoiceItem_Item_ItemId` FOREIGN KEY (`ItemId`) REFERENCES `item` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.invoiceitem: ~0 rows (około)
/*!40000 ALTER TABLE `invoiceitem` DISABLE KEYS */;
REPLACE INTO `invoiceitem` (`Id`, `InvoiceId`, `ItemId`, `Quantity`) VALUES
	(1, 1, 1, 1),
	(2, 1, 2, 2);
/*!40000 ALTER TABLE `invoiceitem` ENABLE KEYS */;

-- Zrzut struktury tabela firma.invoicestatus
CREATE TABLE IF NOT EXISTS `invoicestatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.invoicestatus: ~2 rows (około)
/*!40000 ALTER TABLE `invoicestatus` DISABLE KEYS */;
REPLACE INTO `invoicestatus` (`Id`, `Name`) VALUES
	(1, 'nowa'),
	(2, 'opłacona'),
	(3, 'nieopłacona');
/*!40000 ALTER TABLE `invoicestatus` ENABLE KEYS */;

-- Zrzut struktury tabela firma.item
CREATE TABLE IF NOT EXISTS `item` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `UnitOfMeasureId` int(11) NOT NULL,
  `VATId` int(11) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Item_UnitOfMeasureId` (`UnitOfMeasureId`),
  KEY `IX_Item_VATId` (`VATId`),
  CONSTRAINT `FK_Item_UnitOfMeasure_UnitOfMeasureId` FOREIGN KEY (`UnitOfMeasureId`) REFERENCES `unitofmeasure` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Item_VAT_VATId` FOREIGN KEY (`VATId`) REFERENCES `vat` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.item: ~0 rows (około)
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
REPLACE INTO `item` (`Id`, `Name`, `UnitOfMeasureId`, `VATId`, `Price`) VALUES
	(1, 'usługa informatyczna', 1, 1, 100.000000000000000000000000000000),
	(2, 'instalacja oprogramowania', 1, 1, 120.000000000000000000000000000000);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;

-- Zrzut struktury tabela firma.parameter
CREATE TABLE IF NOT EXISTS `parameter` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.parameter: ~0 rows (około)
/*!40000 ALTER TABLE `parameter` DISABLE KEYS */;
/*!40000 ALTER TABLE `parameter` ENABLE KEYS */;

-- Zrzut struktury tabela firma.paymentmethod
CREATE TABLE IF NOT EXISTS `paymentmethod` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `DueTerm` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.paymentmethod: ~0 rows (około)
/*!40000 ALTER TABLE `paymentmethod` DISABLE KEYS */;
REPLACE INTO `paymentmethod` (`Id`, `Name`, `DueTerm`) VALUES
	(1, 'przelew 7 dni', 7),
	(2, 'gotówka', 0);
/*!40000 ALTER TABLE `paymentmethod` ENABLE KEYS */;

-- Zrzut struktury tabela firma.taxbookitem
CREATE TABLE IF NOT EXISTS `taxbookitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL,
  `InvoiceNumber` longtext DEFAULT NULL,
  `Name` longtext DEFAULT NULL,
  `NIP` longtext DEFAULT NULL,
  `Address` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  `SellValue` decimal(65,30) NOT NULL,
  `OtherIncome` decimal(65,30) NOT NULL,
  `GoodsBuys` decimal(65,30) NOT NULL,
  `BuysSideEffects` decimal(65,30) NOT NULL,
  `Salary` decimal(65,30) NOT NULL,
  `OtherCosts` decimal(65,30) NOT NULL,
  `Column15` decimal(65,30) NOT NULL,
  `CostDescription` longtext DEFAULT NULL,
  `ResearchCostValue` decimal(65,30) NOT NULL,
  `Comments` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.taxbookitem: ~0 rows (około)
/*!40000 ALTER TABLE `taxbookitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `taxbookitem` ENABLE KEYS */;

-- Zrzut struktury tabela firma.unitofmeasure
CREATE TABLE IF NOT EXISTS `unitofmeasure` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `ShortName` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.unitofmeasure: ~0 rows (około)
/*!40000 ALTER TABLE `unitofmeasure` DISABLE KEYS */;
REPLACE INTO `unitofmeasure` (`Id`, `Name`, `ShortName`) VALUES
	(1, 'sztuka', 'szt.');
/*!40000 ALTER TABLE `unitofmeasure` ENABLE KEYS */;

-- Zrzut struktury tabela firma.vat
CREATE TABLE IF NOT EXISTS `vat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Value` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.vat: ~0 rows (około)
/*!40000 ALTER TABLE `vat` DISABLE KEYS */;
REPLACE INTO `vat` (`Id`, `Value`) VALUES
	(1, 23.000000000000000000000000000000);
/*!40000 ALTER TABLE `vat` ENABLE KEYS */;

-- Zrzut struktury tabela firma.vatregisterbuy
CREATE TABLE IF NOT EXISTS `vatregisterbuy` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `DeliveryDate` datetime(6) NOT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `DocumentNumber` longtext DEFAULT NULL,
  `Contractor` longtext DEFAULT NULL,
  `ValueBrutto` decimal(65,30) NOT NULL,
  `ValueNetto` decimal(65,30) NOT NULL,
  `TaxDeductibleValue` decimal(65,30) NOT NULL,
  `TaxFreeBuysValue` decimal(65,30) NOT NULL,
  `NoTaxDeductibleBuysValue` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.vatregisterbuy: ~0 rows (około)
/*!40000 ALTER TABLE `vatregisterbuy` DISABLE KEYS */;
/*!40000 ALTER TABLE `vatregisterbuy` ENABLE KEYS */;

-- Zrzut struktury tabela firma.vatregistersell
CREATE TABLE IF NOT EXISTS `vatregistersell` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `DeliveryDate` datetime(6) NOT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `DocumentNumber` longtext DEFAULT NULL,
  `Contractor` longtext DEFAULT NULL,
  `ValueBrutto` decimal(65,30) NOT NULL,
  `ValueNetto23` decimal(65,30) NOT NULL,
  `VATValue23` decimal(65,30) NOT NULL,
  `ValueNetto7_8` decimal(65,30) NOT NULL,
  `VATValue7_8` decimal(65,30) NOT NULL,
  `ValueNetto3_5` decimal(65,30) NOT NULL,
  `VATValue3_5` decimal(65,30) NOT NULL,
  `ValueNetto0` decimal(65,30) NOT NULL,
  `ValueTaxFree` decimal(65,30) NOT NULL,
  `ValueNoTax` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.vatregistersell: ~0 rows (około)
/*!40000 ALTER TABLE `vatregistersell` DISABLE KEYS */;
/*!40000 ALTER TABLE `vatregistersell` ENABLE KEYS */;

-- Zrzut struktury tabela firma.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma.__efmigrationshistory: ~2 rows (około)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
REPLACE INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20180616193327_initial', '2.1.0-rtm-30799'),
	('20180616212647_migr0', '2.1.0-rtm-30799'),
	('20180617192251_migr1', '2.1.0-rtm-30799');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
