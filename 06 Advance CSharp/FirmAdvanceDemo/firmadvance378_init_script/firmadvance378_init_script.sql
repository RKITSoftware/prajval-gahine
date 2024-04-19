
CREATE DATABASE IF NOT EXISTS `firmadvance378`;

USE firmadvance378;

---------------------------------------------------- STG01 ----------------------------------------------------
CREATE TABLE `STG01` 
(
  `G01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `G01F02` DATETIME NOT NULL, 
  `G01F03` DATETIME NOT NULL, 
  `G01F04` DATETIME NULL 
);
INSERT INTO `STG01` (`G01F02`,`G01F03`) VALUES (timestamp('2024-04-19 17:27:58.645786'),timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- RLE01 ----------------------------------------------------
CREATE TABLE `RLE01` 
(
  `E01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `E01F02` CHAR(1) NOT NULL, 
  `E01F03` DATETIME NOT NULL, 
  `E01F04` DATETIME NULL 
);
INSERT INTO `RLE01` (`E01F02`,`E01F03`) VALUES ('A',timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `RLE01` (`E01F02`,`E01F03`) VALUES ('E',timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- DPT01 ----------------------------------------------------
CREATE TABLE `DPT01` 
(
  `T01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `T01F02` VARCHAR(255) NULL, 
  `T01F03` DATETIME NOT NULL, 
  `T01F04` DATETIME NULL 
);
INSERT INTO `DPT01` (`T01F02`,`T01F03`) VALUES ('Development',timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `DPT01` (`T01F02`,`T01F03`) VALUES ('Marketing',timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `DPT01` (`T01F02`,`T01F03`) VALUES ('Testing',timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `DPT01` (`T01F02`,`T01F03`) VALUES ('Network',timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- PSN01 ----------------------------------------------------
CREATE TABLE `PSN01` 
(
  `N01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `N01F02` VARCHAR(255) NULL, 
  `N01F03` DOUBLE NOT NULL, 
  `N01F04` DOUBLE NOT NULL, 
  `N01F05` DOUBLE NOT NULL, 
  `N01F06` INT(11) NOT NULL, 
  `N01F07` DATETIME NOT NULL, 
  `N01F08` DATETIME NULL 
);
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Full Stack Developer',6.5,50000,10000,1,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Flutter',4.5,35000,10000,1,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Project Manager',8.5,68000,25000,1,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Social Media Coordinator',3.5,25000,5000,2,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Marketing Executive',4,30000,4000,2,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Director of Marketing',7.5,60000,30000,2,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('QA Engineer',4.5,35000,10000,3,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Test Engineer',4.5,35000,10000,3,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Software Tester',5.5,41500,12000,3,timestamp('2024-04-19 17:27:58.645786'));
INSERT INTO `PSN01` (`N01F02`,`N01F03`,`N01F04`,`N01F05`,`N01F06`,`N01F07`) VALUES ('Computer Technician',4,3000,8000,4,timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- USR01 ----------------------------------------------------
CREATE TABLE `USR01` 
(
  `R01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `R01F02` VARCHAR(255) NULL, 
  `R01F03` VARCHAR(255) NULL, 
  `R01F04` VARCHAR(255) NULL, 
  `R01F05` VARCHAR(255) NULL, 
  `R01F06` DATETIME NOT NULL, 
  `R01F07` DATETIME NULL 
);
INSERT INTO `USR01` (`R01F02`,`R01F03`,`R01F04`,`R01F05`,`R01F06`) VALUES ('prajvalgahine','OtVijuqtbTTj4+J7GN0NYSQUpMn/nJkf5OiU9Vs4b4c=','prajvalgahine7121@gmail.com',@NULL,timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- ULE02 ----------------------------------------------------
CREATE TABLE `ULE02` 
(
  `E02F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `E02F02` INT(11) NOT NULL, 
  `E02F03` INT(11) NOT NULL, 
  `E02F04` DATETIME NOT NULL, 
  `E02F05` DATETIME NULL 
);
INSERT INTO `ULE02` (`E02F02`,`E02F03`,`E02F04`) VALUES (1,1,timestamp('2024-04-19 17:27:58.645786'));

---------------------------------------------------- EMP01 ----------------------------------------------------
CREATE TABLE `EMP01` 
(
  `P01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `P01F02` VARCHAR(255) NULL, 
  `P01F03` VARCHAR(255) NULL, 
  `P01F04` CHAR(1) NOT NULL, 
  `P01F05` DATETIME NOT NULL, 
  `P01F06` INT(11) NOT NULL, 
  `P01F07` DATETIME NOT NULL, 
  `P01F08` DATETIME NULL 
);

---------------------------------------------------- UMP02 ----------------------------------------------------
CREATE TABLE `UMP02` 
(
  `P02F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `P02F02` INT(11) NOT NULL, 
  `P02F03` INT(11) NOT NULL, 
  `P02F04` DATETIME NOT NULL, 
  `P02F05` DATETIME NULL 
);

---------------------------------------------------- ATD01 ----------------------------------------------------
CREATE TABLE `ATD01` 
(
  `D01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `D01F02` INT(11) NOT NULL, 
  `D01F03` DATE NOT NULL, 
  `D01F04` DOUBLE NOT NULL, 
  `D01F05` DATETIME NOT NULL, 
  `D01F06` DATETIME NULL 
);

---------------------------------------------------- STG01 ----------------------------------------------------
CREATE TABLE `LVE02` 
(
  `E02F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `E02F02` INT(11) NOT NULL, 
  `E02F03` DATETIME NOT NULL, 
  `E02F04` INT(11) NOT NULL, 
  `E02F05` VARCHAR(255) NULL, 
  `E02F06` CHAR(1) NOT NULL, 
  `E02F07` DATETIME NOT NULL, 
  `E02F08` DATETIME NULL 
);

---------------------------------------------------- SLY01 ----------------------------------------------------
CREATE TABLE `SLY01` 
(
  `Y01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `Y01F02` INT(11) NOT NULL, 
  `Y01F03` DATETIME NOT NULL, 
  `Y01F04` DOUBLE NOT NULL, 
  `Y01F05` INT(11) NOT NULL, 
  `Y01F06` DATETIME NOT NULL, 
  `Y01F07` DATETIME NOT NULL, 
  
    CONSTRAINT `FK_SLY01_EMP01_Y01F02` FOREIGN KEY (`Y01F02`) REFERENCES `EMP01` (`P01F01`) ON DELETE CASCADE, 
  
    CONSTRAINT `FK_SLY01_PSN01_Y01F05` FOREIGN KEY (`Y01F05`) REFERENCES `PSN01` (`N01F01`) ON DELETE CASCADE 
);

---------------------------------------------------- PCH01 ----------------------------------------------------
CREATE TABLE `PCH01` 
(
  `D01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `H01F02` INT(11) NOT NULL, 
  `H01F03` DATETIME NOT NULL, 
  `H01F04` INT(11) NULL, 
  `H01F05` DATETIME NOT NULL, 
  `H01F06` DATETIME NOT NULL, 
  
    CONSTRAINT `FK_PCH01_EMP01_H01F02` FOREIGN KEY (`H01F02`) REFERENCES `EMP01` (`P01F01`) ON DELETE CASCADE 
);