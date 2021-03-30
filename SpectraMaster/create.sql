CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `MassProblems` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IonPeak` float NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `NmrProblems` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `H` smallint NOT NULL,
    `C` smallint NOT NULL,
    `N` smallint NOT NULL,
    `O` smallint NOT NULL,
    `F` smallint NOT NULL,
    `Si` smallint NOT NULL,
    `P` smallint NOT NULL,
    `S` smallint NOT NULL,
    `Cl` smallint NOT NULL,
    `Br` smallint NOT NULL,
    `I` smallint NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `SpectraAnswers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProblemDescription` text NULL,
    `AnswerDescription` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AnswerPics` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Path` text NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AnswerPics_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `MassProblemAnswer` (
    `AnswerId` int NOT NULL,
    `MassProblemId` int NOT NULL,
    PRIMARY KEY (`AnswerId`, `MassProblemId`),
    CONSTRAINT `FK_MassProblemAnswer_MassProblems_MassProblemId` FOREIGN KEY (`MassProblemId`) REFERENCES `MassProblems` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_MassProblemAnswer_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `NMRProblemAnswer` (
    `NMRProblemId` int NOT NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`AnswerId`, `NMRProblemId`),
    CONSTRAINT `FK_NMRProblemAnswer_NmrProblems_NMRProblemId` FOREIGN KEY (`NMRProblemId`) REFERENCES `NmrProblems` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_NMRProblemAnswer_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ProblemPics` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Path` text NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ProblemPics_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AnswerPics_AnswerId` ON `AnswerPics` (`AnswerId`);

CREATE UNIQUE INDEX `IX_MassProblemAnswer_AnswerId` ON `MassProblemAnswer` (`AnswerId`);

CREATE UNIQUE INDEX `IX_MassProblemAnswer_MassProblemId` ON `MassProblemAnswer` (`MassProblemId`);

CREATE UNIQUE INDEX `IX_NMRProblemAnswer_AnswerId` ON `NMRProblemAnswer` (`AnswerId`);

CREATE UNIQUE INDEX `IX_NMRProblemAnswer_NMRProblemId` ON `NMRProblemAnswer` (`NMRProblemId`);

CREATE INDEX `IX_ProblemPics_AnswerId` ON `ProblemPics` (`AnswerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210330072607_init-db', '5.0.4');

COMMIT;

