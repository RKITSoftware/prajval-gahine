-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: 378esa
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cnt01`
--

DROP TABLE IF EXISTS `cnt01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cnt01` (
  `t01f01` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'User Expense Payee ID',
  `t01f02` int unsigned NOT NULL COMMENT 'Expense ID',
  `t01f03` int unsigned NOT NULL COMMENT 'Payee user ID',
  `t01f04` double(10,2) unsigned NOT NULL COMMENT 'Amount to pay',
  `t01f05` tinyint unsigned NOT NULL DEFAULT '0' COMMENT 'Is paid',
  `t01f98` datetime NOT NULL,
  `t01f99` datetime DEFAULT NULL,
  PRIMARY KEY (`t01f01`),
  UNIQUE KEY `p02f01_UNIQUE` (`t01f01`) /*!80000 INVISIBLE */,
  UNIQUE KEY `uidx_uxe01_e01f02_e01f03` (`t01f02`,`t01f03`),
  KEY `FK_UXE01_USR01_E01F03_idx` (`t01f03`),
  CONSTRAINT `FK_CNT01_EXP01_T01F02` FOREIGN KEY (`t01f02`) REFERENCES `exp01` (`p01f01`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_CNT01_USR01_T01F03` FOREIGN KEY (`t01f03`) REFERENCES `usr01` (`r01f01`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cnt01`
--

LOCK TABLES `cnt01` WRITE;
/*!40000 ALTER TABLE `cnt01` DISABLE KEYS */;
INSERT INTO `cnt01` VALUES (1,1,1,183.33,1,'2024-06-02 18:43:45',NULL),(2,1,2,183.33,1,'2024-06-02 18:43:45',NULL),(3,1,3,183.33,1,'2024-06-02 18:43:45',NULL),(4,2,1,50.00,1,'2024-06-02 20:18:17',NULL),(5,2,2,150.00,1,'2024-06-02 20:18:17',NULL),(6,3,2,1000.00,1,'2024-06-02 20:20:06',NULL),(7,3,3,1000.00,1,'2024-06-02 20:20:06',NULL),(8,4,3,1465.00,1,'2024-06-02 20:22:31',NULL),(9,4,2,1465.00,1,'2024-06-02 20:22:31',NULL);
/*!40000 ALTER TABLE `cnt01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exp01`
--

DROP TABLE IF EXISTS `exp01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exp01` (
  `p01f01` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Expense ID',
  `p01f02` int unsigned NOT NULL COMMENT 'User ID',
  `p01f03` varchar(150) DEFAULT NULL COMMENT 'Description',
  `p01f04` double(10,2) unsigned NOT NULL COMMENT 'Amout paid',
  `p01f05` datetime NOT NULL COMMENT 'Date of Expense',
  `p01f98` datetime NOT NULL COMMENT 'Expense creation datetime',
  `p01f99` datetime DEFAULT NULL COMMENT 'Expense last modified datetime',
  PRIMARY KEY (`p01f01`),
  UNIQUE KEY `p01f01_UNIQUE` (`p01f01`),
  KEY `FK_EXP01_USR01_P01F02_idx` (`p01f02`),
  CONSTRAINT `FK_EXP01_USR01_P01F02` FOREIGN KEY (`p01f02`) REFERENCES `usr01` (`r01f01`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exp01`
--

LOCK TABLES `exp01` WRITE;
/*!40000 ALTER TABLE `exp01` DISABLE KEYS */;
INSERT INTO `exp01` VALUES (1,1,'dinner',550.00,'2024-06-02 18:43:45','2024-06-02 18:43:45',NULL),(2,1,'lunch',200.00,'2024-06-02 18:43:45','2024-06-02 18:43:45',NULL),(3,2,'dmart',2000.00,'2024-06-02 20:20:06','2024-06-02 20:20:06',NULL),(4,3,'rent',2930.00,'2024-06-02 20:22:31','2024-06-02 20:22:31',NULL);
/*!40000 ALTER TABLE `exp01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usr01`
--

DROP TABLE IF EXISTS `usr01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usr01` (
  `r01f01` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'User ID',
  `r01f02` varchar(20) NOT NULL COMMENT 'Username',
  `r01f03` varchar(44) NOT NULL COMMENT 'Password',
  `r01f98` datetime NOT NULL COMMENT 'User creation datetime',
  `r01f99` datetime DEFAULT NULL COMMENT 'User last modified datetime',
  PRIMARY KEY (`r01f01`),
  UNIQUE KEY `r01f01_UNIQUE` (`r01f01`),
  UNIQUE KEY `r01f02_UNIQUE` (`r01f02`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usr01`
--

LOCK TABLES `usr01` WRITE;
/*!40000 ALTER TABLE `usr01` DISABLE KEYS */;
INSERT INTO `usr01` VALUES (1,'u1','123','2024-06-02 18:39:21',NULL),(2,'u2','123','2024-06-02 18:39:25',NULL),(3,'u3','123','2024-06-02 18:39:28',NULL);
/*!40000 ALTER TABLE `usr01` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-03  8:20:32
