-- MySQL dump 10.13  Distrib 5.5.23, for Win32 (x86)
--
-- Host: localhost    Database: projektBD
-- ------------------------------------------------------
-- Server version	5.5.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `can_documents`
--

DROP TABLE IF EXISTS `can_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `can_documents` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `file_location` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `can_documents`
--

LOCK TABLES `can_documents` WRITE;
/*!40000 ALTER TABLE `can_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `can_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `can_test`
--

DROP TABLE IF EXISTS `can_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `can_test` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid` int(11) NOT NULL,
  `spid` int(11) NOT NULL,
  `date` date NOT NULL,
  `name` varchar(45) NOT NULL,
  `score` int(11) NOT NULL,
  `description` varchar(255) NOT NULL,
  `test_complited` char(1) DEFAULT NULL,
  `date_accepted` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `can_test`
--

LOCK TABLES `can_test` WRITE;
/*!40000 ALTER TABLE `can_test` DISABLE KEYS */;
/*!40000 ALTER TABLE `can_test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `candidates`
--

DROP TABLE IF EXISTS `candidates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `candidates` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `did` int(11) DEFAULT NULL,
  `name` varchar(45) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `city` varchar(45) NOT NULL,
  `pesel` decimal(11,0) NOT NULL,
  `sex` char(1) NOT NULL,
  `education` varchar(255) NOT NULL,
  `skills` varchar(255) NOT NULL,
  `experience` varchar(255) DEFAULT NULL,
  `courses` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `candidates`
--

LOCK TABLES `candidates` WRITE;
/*!40000 ALTER TABLE `candidates` DISABLE KEYS */;
/*!40000 ALTER TABLE `candidates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departments`
--

DROP TABLE IF EXISTS `departments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `departments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `department` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `department_UNIQUE` (`department`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (5,'Administracja'),(1,'IT'),(2,'Ksiegowosc'),(3,'Marketing'),(4,'Sprzedaż');
/*!40000 ALTER TABLE `departments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `privilages`
--

DROP TABLE IF EXISTS `privilages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `privilages` (
  `uid` int(11) NOT NULL,
  `level` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `privilages`
--

LOCK TABLES `privilages` WRITE;
/*!40000 ALTER TABLE `privilages` DISABLE KEYS */;
INSERT INTO `privilages` VALUES (1,1),(2,2);
/*!40000 ALTER TABLE `privilages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recruitment_specialization_test_types`
--

DROP TABLE IF EXISTS `recruitment_specialization_test_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `recruitment_specialization_test_types` (
  `rid` int(11) NOT NULL,
  `spid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recruitment_specialization_test_types`
--

LOCK TABLES `recruitment_specialization_test_types` WRITE;
/*!40000 ALTER TABLE `recruitment_specialization_test_types` DISABLE KEYS */;
/*!40000 ALTER TABLE `recruitment_specialization_test_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recruitments`
--

DROP TABLE IF EXISTS `recruitments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `recruitments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `decription` varchar(255) NOT NULL,
  `department` varchar(255) NOT NULL,
  `needed_ppl` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recruitments`
--

LOCK TABLES `recruitments` WRITE;
/*!40000 ALTER TABLE `recruitments` DISABLE KEYS */;
/*!40000 ALTER TABLE `recruitments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialization_type`
--

DROP TABLE IF EXISTS `specialization_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specialization_type` (
  `spid` int(11) NOT NULL AUTO_INCREMENT,
  `name_of_subject` varchar(50) NOT NULL,
  PRIMARY KEY (`spid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialization_type`
--

LOCK TABLES `specialization_type` WRITE;
/*!40000 ALTER TABLE `specialization_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `specialization_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_specializations`
--

DROP TABLE IF EXISTS `user_specializations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_specializations` (
  `uid` int(11) NOT NULL,
  `spid` int(11) NOT NULL,
  `degree` varchar(45) NOT NULL,
  PRIMARY KEY (`spid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_specializations`
--

LOCK TABLES `user_specializations` WRITE;
/*!40000 ALTER TABLE `user_specializations` DISABLE KEYS */;
INSERT INTO `user_specializations` VALUES (1,1,'1'),(2,2,'1');
/*!40000 ALTER TABLE `user_specializations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(255) NOT NULL,
  `name` varchar(45) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `city` varchar(45) NOT NULL,
  `pesel` decimal(11,0) NOT NULL,
  `sex` char(1) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login_UNIQUE` (`login`),
  UNIQUE KEY `uid_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'asystentka','haslo1','Asia','Adsad','Gliwice',12345678901,'f'),(2,'kierownik','haslo1','Mateusz','Ndsaa','Gliwice',12345678902,'m');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2012-05-11 21:26:40
