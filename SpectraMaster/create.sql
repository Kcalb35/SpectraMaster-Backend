START TRANSACTION;

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

CREATE TABLE `MassProblems` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IonPeak` float NOT NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MassProblems_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `NmrProblems` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `H` int NOT NULL,
    `C` int NOT NULL,
    `N` int NOT NULL,
    `O` int NOT NULL,
    `F` int NOT NULL,
    `Si` int NOT NULL,
    `P` int NOT NULL,
    `S` int NOT NULL,
    `Cl` int NOT NULL,
    `Br` int NOT NULL,
    `I` int NOT NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_NmrProblems_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ProblemPics` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Path` text NULL,
    `AnswerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ProblemPics_SpectraAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `SpectraAnswers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AnswerPics_AnswerId` ON `AnswerPics` (`AnswerId`);

CREATE UNIQUE INDEX `IX_MassProblems_AnswerId` ON `MassProblems` (`AnswerId`);

CREATE UNIQUE INDEX `IX_NmrProblems_AnswerId` ON `NmrProblems` (`AnswerId`);

CREATE INDEX `IX_ProblemPics_AnswerId` ON `ProblemPics` (`AnswerId`);
COMMIT;

START TRANSACTION;

CREATE TABLE `Managers` (
                            `Id` int NOT NULL AUTO_INCREMENT,
                            `Username` text NULL,
                            `Password` text NULL,
                            PRIMARY KEY (`Id`)
);


COMMIT;
