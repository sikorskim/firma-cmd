-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               10.2.9-MariaDB - mariadb.org binary distribution
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Zrzut struktury bazy danych firma_dev
CREATE DATABASE IF NOT EXISTS `firma_dev` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `firma_dev`;

-- Zrzut struktury tabela firma_dev.aspnetroleclaims
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetroleclaims: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetroles
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetroles: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetuserclaims
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetuserclaims: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetuserlogins
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetuserlogins: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetuserroles
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetuserroles: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetusers
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

-- Zrzucanie danych dla tabeli firma_dev.aspnetusers: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.aspnetusertokens
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.aspnetusertokens: ~0 rows (około)
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.contractor
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
  `Discriminator` longtext NOT NULL,
  `REGON` longtext DEFAULT NULL,
  `InvoiceIssuerName` longtext DEFAULT NULL,
  `InvoiceIssueCity` longtext DEFAULT NULL,
  `BankName` longtext DEFAULT NULL,
  `BankAccountNumber` longtext DEFAULT NULL,
  `Website` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.contractor: ~14 rows (około)
/*!40000 ALTER TABLE `contractor` DISABLE KEYS */;
INSERT INTO `contractor` (`Id`, `NIP`, `FullName`, `Name`, `CountryCode`, `Voivodeship`, `County`, `Community`, `City`, `Street`, `BuldingNo`, `PostalCode`, `PostOffice`, `Email`, `Phone`, `Discriminator`, `REGON`, `InvoiceIssuerName`, `InvoiceIssueCity`, `BankName`, `BankAccountNumber`, `Website`) VALUES
	(1, '7851769827', 'Computerman Maciej Sikorski', 'Computerman', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Leśna', '15', '63-100', 'Śrem', 'biuro@computerman.pl', '796655550', 'Company', NULL, 'Maciej Sikorski', 'Śrem', 'Bank PKO BP S.A.', '05 1020 4160 0000 2902 0207 5596', 'computerman.pl'),
	(2, '7851166649', 'Studio Reklamy JAG Janusz Gadziński', 'Studio Reklamy JAG', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Wojciechowskiego', '6', '63-100', 'Śrem', 'biuro@reklamajag.pl', '612837268', 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(3, '7851793027', 'Szpital Powiatowy im. Tadeusza Malińskiego w Śremie sp. z o.o.', 'Szpital Powiatowy im. T. Malińskiego w Śremie', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Chełmońskiego', '1', '63-100', 'Śrem', 'sekretariat@szpital-srem.pl', '612815400', 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(9, '7822556342', 'Medeus sp. z o.o. sp.k.', 'Medeus', 'PL', 'wielkopolskie', 'poznański', 'Poznań', 'Poznań', 'Katowicka', '93A/18', '61-131', 'Poznań', 'biuro@medeus.pl', NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(10, '7851589901', 'Z.M.D. Bianek s.c.', 'Z.M.D. Bianek', 'PL', 'wielkopolskie', 'śremski', 'Śrem', 'Śrem', 'Kochanowskiego', '2', '63-100', 'Śrem', 'poczta@salajedynka.pl', NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(11, '9492107026', 'X-KOM sp. z o.o.', 'X-KOM', NULL, NULL, NULL, NULL, 'Częstochowa', 'Al. Wolności', '31', '42-202', NULL, 'x-kom@x-kom.pl', '343770000', 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(12, '5261040567', 'T-Mobile Polska S.A.', 'T-Mobile', NULL, NULL, NULL, NULL, 'Warszawa', 'Marynarska', '12', '02-674', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(13, '7822239989', '"MAKASAR" sp. z o.o.', 'Makasar', NULL, NULL, NULL, NULL, 'Śrem', 'Szkolna', '15', '63-100', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(14, '5732623134', 'BarellyBags Łukasz Bareła', 'BarellyBags', NULL, NULL, NULL, NULL, 'Mykanów', 'Kasztanowa', '23', '42-233', NULL, 'biuro@barellybags.pl', '505851682', 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(15, '7822254718', 'Carlota sp. z o.o.', 'Carlota', NULL, NULL, NULL, NULL, 'Śrem', 'Szkolna', '15A', '63-100', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(16, '7851689005', 'Przedsiębiorstwo Handlowo-Usługowe "EKO-PARTNER" s.c. M. Cieślak M. Miłowski', 'EKO-PARTNER', NULL, NULL, NULL, NULL, 'Śrem', 'Staszica', '3', '63-100', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(17, '8992520556', 'OVH sp. z o.o.', 'OVH', NULL, NULL, NULL, NULL, 'Wrocław', 'Szkocka', '5 lok. 1', '54-402', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(18, '5261009959', 'CASTORAMA POLSKA sp. z o.o.', 'Castorama', NULL, NULL, NULL, NULL, 'Warszawa', 'Krakowiaków', '78', '02-255', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL),
	(19, '7740001454', 'Polski Koncern Naftowy ORLEN S.A.', 'ORLEN', NULL, NULL, NULL, NULL, 'Płock', 'Chemików', '7', '09-411', NULL, NULL, NULL, 'Contractor', NULL, NULL, NULL, NULL, NULL, NULL);
/*!40000 ALTER TABLE `contractor` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.fixedassets
CREATE TABLE IF NOT EXISTS `fixedassets` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DateOfBuy` datetime(6) NOT NULL,
  `DateOfUseStart` datetime(6) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `Identfier` longtext DEFAULT NULL,
  `OriginalValue` decimal(65,30) NOT NULL,
  `DepreciationRate` decimal(65,30) DEFAULT NULL,
  `UpgradeValue` decimal(65,30) DEFAULT NULL,
  `UpdatedOriginalValue` decimal(65,30) DEFAULT NULL,
  `LiquidationDate` datetime(6) DEFAULT NULL,
  `LiquidationReason` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.fixedassets: ~0 rows (około)
/*!40000 ALTER TABLE `fixedassets` DISABLE KEYS */;
INSERT INTO `fixedassets` (`Id`, `DateOfBuy`, `DateOfUseStart`, `Name`, `Identfier`, `OriginalValue`, `DepreciationRate`, `UpgradeValue`, `UpdatedOriginalValue`, `LiquidationDate`, `LiquidationReason`) VALUES
	(1, '2016-12-09 00:00:00.000000', '2018-05-02 00:00:00.000000', 'Skoda Fabia 1.4 KAT', 'ST-1', 2845.530000000000000000000000000000, NULL, NULL, NULL, NULL, NULL);
/*!40000 ALTER TABLE `fixedassets` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.invoice
CREATE TABLE IF NOT EXISTS `invoice` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` longtext DEFAULT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `DateOfDelivery` datetime(6) NOT NULL,
  `ContractorId` int(11) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  `PaymentMethodId` int(11) NOT NULL,
  `InvoiceStatusId` int(11) NOT NULL,
  `Discriminator` longtext NOT NULL,
  `InvoiceId` int(11) DEFAULT NULL,
  `DateOfCorrection` datetime(6) DEFAULT NULL,
  `CorrectionCause` longtext DEFAULT NULL,
  `Paid` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`),
  KEY `IX_Invoice_CompanyId` (`CompanyId`),
  KEY `IX_Invoice_ContractorId` (`ContractorId`),
  KEY `IX_Invoice_InvoiceStatusId` (`InvoiceStatusId`),
  KEY `IX_Invoice_PaymentMethodId` (`PaymentMethodId`),
  CONSTRAINT `FK_Invoice_Contractor_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `contractor` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Invoice_Contractor_ContractorId` FOREIGN KEY (`ContractorId`) REFERENCES `contractor` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Invoice_InvoiceStatus_InvoiceStatusId` FOREIGN KEY (`InvoiceStatusId`) REFERENCES `invoicestatus` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Invoice_PaymentMethod_PaymentMethodId` FOREIGN KEY (`PaymentMethodId`) REFERENCES `paymentmethod` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.invoice: ~4 rows (około)
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
INSERT INTO `invoice` (`Id`, `Number`, `DateOfIssue`, `DateOfDelivery`, `ContractorId`, `CompanyId`, `PaymentMethodId`, `InvoiceStatusId`, `Discriminator`, `InvoiceId`, `DateOfCorrection`, `CorrectionCause`, `Paid`) VALUES
	(15, 'FV/2018/07/1', '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 12, 1, 1, 4, 'Invoice', NULL, NULL, NULL, b'0'),
	(16, 'FV/2018/07/2', '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 9, 1, 1, 4, 'Invoice', NULL, NULL, NULL, b'0'),
	(17, 'FV/2018/07/3', '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 13, 1, 1, 1, 'Invoice', NULL, NULL, NULL, b'0'),
	(18, 'FV/2018/07/4', '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 1, 1, 1, 1, 'Invoice', NULL, NULL, NULL, b'0');
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.invoiceitem
CREATE TABLE IF NOT EXISTS `invoiceitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `Name` longtext DEFAULT NULL,
  `VATValue` decimal(65,30) NOT NULL,
  `UnitOfMeasureShortName` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_InvoiceItem_InvoiceId` (`InvoiceId`),
  CONSTRAINT `FK_InvoiceItem_Invoice_InvoiceId` FOREIGN KEY (`InvoiceId`) REFERENCES `invoice` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.invoiceitem: ~4 rows (około)
/*!40000 ALTER TABLE `invoiceitem` DISABLE KEYS */;
INSERT INTO `invoiceitem` (`Id`, `InvoiceId`, `Quantity`, `Price`, `Name`, `VATValue`, `UnitOfMeasureShortName`) VALUES
	(21, 15, 1, 250.000000000000000000000000000000, 'usługi informatyczne', 23.000000000000000000000000000000, 'szt.'),
	(22, 16, 1, 49.990000000000000000000000000000, 'hosting e-mail', 23.000000000000000000000000000000, 'szt.'),
	(23, 16, 1, 99.990000000000000000000000000000, 'pozycja testowa', 23.000000000000000000000000000000, 'szt.'),
	(24, 17, 1, 299.990000000000000000000000000000, 'pozycja testowa', 23.000000000000000000000000000000, 'szt.'),
	(25, 18, 1, 100.000000000000000000000000000000, 'usługi informatyczne', 23.000000000000000000000000000000, 'szt.');
/*!40000 ALTER TABLE `invoiceitem` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.invoicestatus
CREATE TABLE IF NOT EXISTS `invoicestatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.invoicestatus: ~2 rows (około)
/*!40000 ALTER TABLE `invoicestatus` DISABLE KEYS */;
INSERT INTO `invoicestatus` (`Id`, `Name`) VALUES
	(1, 'nowa'),
	(4, 'zatwierdzona');
/*!40000 ALTER TABLE `invoicestatus` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.item
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.item: ~6 rows (około)
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` (`Id`, `Name`, `UnitOfMeasureId`, `VATId`, `Price`) VALUES
	(1, 'usługi informatyczne', 1, 1, 100.000000000000000000000000000000),
	(2, 'instalacja oprogramowania', 1, 1, 120.000000000000000000000000000000),
	(3, 'konfiguracja oprogramowania', 1, 1, 200.000000000000000000000000000000),
	(4, 'Pamięć RAM GoodRAM 8GB DDR3', 1, 1, 290.000000000000000000000000000000),
	(5, 'hosting e-mail', 1, 1, 150.000000000000000000000000000000),
	(6, 'pozycja testowa', 1, 1, 99.990000000000000000000000000000),
	(7, 'pozycja testowa 2', 1, 1, 199.990000000000000000000000000000);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.parameter
CREATE TABLE IF NOT EXISTS `parameter` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.parameter: ~0 rows (około)
/*!40000 ALTER TABLE `parameter` DISABLE KEYS */;
/*!40000 ALTER TABLE `parameter` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.paymentmethod
CREATE TABLE IF NOT EXISTS `paymentmethod` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `DueTerm` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.paymentmethod: ~3 rows (około)
/*!40000 ALTER TABLE `paymentmethod` DISABLE KEYS */;
INSERT INTO `paymentmethod` (`Id`, `Name`, `DueTerm`) VALUES
	(1, 'przelew 7 dni', 7),
	(2, 'gotówka', 0),
	(3, 'przelew 14 dni', 14);
/*!40000 ALTER TABLE `paymentmethod` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.taxbookitem
CREATE TABLE IF NOT EXISTS `taxbookitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL,
  `InvoiceNumber` longtext DEFAULT NULL,
  `Description` longtext DEFAULT NULL,
  `SellValue` decimal(65,30) DEFAULT NULL,
  `OtherIncome` decimal(65,30) DEFAULT NULL,
  `GoodsBuys` decimal(65,30) DEFAULT NULL,
  `BuysSideEffects` decimal(65,30) DEFAULT NULL,
  `Salary` decimal(65,30) DEFAULT NULL,
  `OtherCosts` decimal(65,30) DEFAULT NULL,
  `Column15` decimal(65,30) DEFAULT NULL,
  `CostDescription` longtext DEFAULT NULL,
  `ResearchCostValue` decimal(65,30) DEFAULT NULL,
  `Comments` longtext DEFAULT NULL,
  `ContractorId` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_TaxBookItem_ContractorId` (`ContractorId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.taxbookitem: ~1 rows (około)
/*!40000 ALTER TABLE `taxbookitem` DISABLE KEYS */;
INSERT INTO `taxbookitem` (`Id`, `Number`, `Date`, `InvoiceNumber`, `Description`, `SellValue`, `OtherIncome`, `GoodsBuys`, `BuysSideEffects`, `Salary`, `OtherCosts`, `Column15`, `CostDescription`, `ResearchCostValue`, `Comments`, `ContractorId`) VALUES
	(4, 1, '2018-07-11 00:00:00.000000', 'FV/2018/07/1', 'sprzedaż', 250.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, NULL, 0.000000000000000000000000000000, NULL, 12),
	(5, 1, '2018-07-11 00:00:00.000000', 'FV/2018/07/2', 'sprzedaż', 149.980000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, NULL, 0.000000000000000000000000000000, NULL, 9);
/*!40000 ALTER TABLE `taxbookitem` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.unitofmeasure
CREATE TABLE IF NOT EXISTS `unitofmeasure` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `ShortName` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.unitofmeasure: ~0 rows (około)
/*!40000 ALTER TABLE `unitofmeasure` DISABLE KEYS */;
INSERT INTO `unitofmeasure` (`Id`, `Name`, `ShortName`) VALUES
	(1, 'sztuka', 'szt.');
/*!40000 ALTER TABLE `unitofmeasure` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.vat
CREATE TABLE IF NOT EXISTS `vat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Value` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.vat: ~0 rows (około)
/*!40000 ALTER TABLE `vat` DISABLE KEYS */;
INSERT INTO `vat` (`Id`, `Value`) VALUES
	(1, 23.000000000000000000000000000000);
/*!40000 ALTER TABLE `vat` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.vatregisterbuy
CREATE TABLE IF NOT EXISTS `vatregisterbuy` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `DeliveryDate` datetime(6) NOT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `DocumentNumber` longtext DEFAULT NULL,
  `ValueBrutto` decimal(65,30) NOT NULL,
  `ValueNetto` decimal(65,30) NOT NULL,
  `TaxDeductibleValue` decimal(65,30) DEFAULT NULL,
  `TaxFreeBuysValue` decimal(65,30) DEFAULT NULL,
  `NoTaxDeductibleBuysValue` decimal(65,30) DEFAULT NULL,
  `ContractorId` int(11) NOT NULL DEFAULT 0,
  `Month` int(11) NOT NULL DEFAULT 0,
  `Year` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_VATRegisterBuy_ContractorId` (`ContractorId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.vatregisterbuy: ~3 rows (około)
/*!40000 ALTER TABLE `vatregisterbuy` DISABLE KEYS */;
INSERT INTO `vatregisterbuy` (`Id`, `Number`, `DeliveryDate`, `DateOfIssue`, `DocumentNumber`, `ValueBrutto`, `ValueNetto`, `TaxDeductibleValue`, `TaxFreeBuysValue`, `NoTaxDeductibleBuysValue`, `ContractorId`, `Month`, `Year`) VALUES
	(7, 1, '2018-05-09 00:00:00.000000', '2018-05-09 00:00:00.000000', '7726K2/4040/18', 199.400000000000000000000000000000, 162.110000000000000000000000000000, 18.640000000000000000000000000000, NULL, NULL, 19, 5, 2018),
	(8, 2, '2018-05-18 00:00:00.000000', '2018-05-18 00:00:00.000000', 'PL2139078/F', 59.000000000000000000000000000000, 47.970000000000000000000000000000, 11.030000000000000000000000000000, NULL, NULL, 17, 5, 2018),
	(9, 3, '2018-05-29 00:00:00.000000', '2018-05-29 00:00:00.000000', 'FF/10563/2018/28', 100.200000000000000000000000000000, 81.460000000000000000000000000000, 9.370000000000000000000000000000, NULL, NULL, 13, 5, 2018);
/*!40000 ALTER TABLE `vatregisterbuy` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.vatregistersell
CREATE TABLE IF NOT EXISTS `vatregistersell` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `DeliveryDate` datetime(6) NOT NULL,
  `DateOfIssue` datetime(6) NOT NULL,
  `DocumentNumber` longtext DEFAULT NULL,
  `ValueBrutto` decimal(65,30) DEFAULT NULL,
  `ValueNetto23` decimal(65,30) DEFAULT NULL,
  `VATValue23` decimal(65,30) DEFAULT NULL,
  `ValueNetto7_8` decimal(65,30) DEFAULT NULL,
  `VATValue7_8` decimal(65,30) DEFAULT NULL,
  `ValueNetto3_5` decimal(65,30) DEFAULT NULL,
  `VATValue3_5` decimal(65,30) DEFAULT NULL,
  `ValueNetto0` decimal(65,30) DEFAULT NULL,
  `ValueTaxFree` decimal(65,30) DEFAULT NULL,
  `ValueNoTax` decimal(65,30) DEFAULT NULL,
  `Month` int(11) NOT NULL DEFAULT 0,
  `Year` int(11) NOT NULL DEFAULT 0,
  `ContractorId` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_VATRegisterSell_ContractorId` (`ContractorId`),
  CONSTRAINT `FK_VATRegisterSell_Contractor_ContractorId` FOREIGN KEY (`ContractorId`) REFERENCES `contractor` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.vatregistersell: ~2 rows (około)
/*!40000 ALTER TABLE `vatregistersell` DISABLE KEYS */;
INSERT INTO `vatregistersell` (`Id`, `Number`, `DeliveryDate`, `DateOfIssue`, `DocumentNumber`, `ValueBrutto`, `ValueNetto23`, `VATValue23`, `ValueNetto7_8`, `VATValue7_8`, `ValueNetto3_5`, `VATValue3_5`, `ValueNetto0`, `ValueTaxFree`, `ValueNoTax`, `Month`, `Year`, `ContractorId`) VALUES
	(13, 1, '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 'FV/2018/07/1', 307.500000000000000000000000000000, 250.000000000000000000000000000000, 57.500000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 7, 2018, 12),
	(14, 1, '2018-07-11 00:00:00.000000', '2018-07-11 00:00:00.000000', 'FV/2018/07/2', 184.475400000000000000000000000000, 149.980000000000000000000000000000, 34.495400000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, 7, 2018, 9);
/*!40000 ALTER TABLE `vatregistersell` ENABLE KEYS */;

-- Zrzut struktury tabela firma_dev.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Zrzucanie danych dla tabeli firma_dev.__efmigrationshistory: ~4 rows (około)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20180626090209_initial', '2.1.0-rtm-30799'),
	('20180710115237_migr1', '2.1.0-rtm-30799'),
	('20180710115537_migr1', '2.1.0-rtm-30799'),
	('20180711094330_migr2', '2.1.0-rtm-30799');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
