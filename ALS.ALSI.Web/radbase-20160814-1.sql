-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: muraddb
-- ------------------------------------------------------
-- Server version	5.7.11-log

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
-- Table structure for table `menu`
--

DROP TABLE IF EXISTS `menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu` (
  `MENU_ID` int(11) NOT NULL,
  `MENU_ICON` varchar(50) DEFAULT NULL,
  `MENU_NAME` varchar(100) NOT NULL,
  `URL_NAVIGATE` varchar(100) DEFAULT NULL,
  `MENU_TAG` varchar(100) DEFAULT NULL,
  `PREVIOUS_MENU_ID` int(11) DEFAULT NULL,
  `DISPLAY_ORDER` int(11) NOT NULL,
  `UPDATE_BY` varchar(25) NOT NULL,
  `CREATE_DATE` date DEFAULT NULL,
  `UPDATE_DATE` date DEFAULT NULL,
  PRIMARY KEY (`MENU_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu`
--

LOCK TABLES `menu` WRITE;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` VALUES (1,'icon-screen-desktop','บันทึกข้อมูล',NULL,NULL,-1,1,'SYSTEM','2016-03-20','2015-02-13'),(2,'icon-grid','ข้อมูลผู้ใช้',NULL,NULL,-1,2,'SYSTEM','2015-02-13','2015-02-13'),(3,'icon-settings','ข้อมูลหลัก',NULL,NULL,-1,3,'SYSTEM','2015-02-13','2015-02-13'),(4,'fa fa-code','รหัสประเภทการใช้งาน','/index.php/MCodeUsage','MCodeUsage|Create|Update',3,1,'SYSTEM','2015-02-13','2015-02-13'),(5,'fa fa-code','ลักษณะการใช้งาน','/index.php/MUseType','MUseType|Create|Update',3,2,'SYSTEM','2015-02-13','2015-02-13'),(6,'fa fa-code','หน่วยกำลังไฟ','/index.php/MPowerUnit','MPowerUnit|Create|Update',3,3,'SYSTEM','2015-02-13','2015-02-13'),(7,'fa fa-building','ห้อง/สถานที่','/index.php/MRoom','MRoom|Create|Update',3,4,'SYSTEM','2015-02-13','2015-02-13'),(8,'fa fa-ship','บริษัทผู้แทนจำหน่าย','/index.php/MDealerCompany','MDealerCompany|Create|Update',3,5,'SYSTEM','2015-02-13','2015-02-13'),(9,'fa fa-cube','ประเภทวัสดุ','/index.php/MMaterialType','MMaterialType|Create|Update',3,6,'SYSTEM','2015-02-13','2015-02-13'),(10,'fa fa-code','สมบัติทางกายภาพ','/index.php/MPhisicalStatus','MPhisicalStatus|Create|Update',3,7,'SYSTEM','2015-02-13','2015-02-13'),(11,'fa fa-ship','บริษัทผู้ผลิต','/index.php/MManufacturer','MManufacturer|Create|Update',3,8,'SYSTEM','2015-02-13','2015-02-13'),(12,'fa fa-server','ชื่อเครื่องกำเนิดรรังสี','/index.php/MRadMachine','MRadMachine|Create|Update',3,9,'SYSTEM','2015-02-13','2015-02-13'),(13,'fa fa-code','ธาตุ-เลขมวล','/index.php/MRadioactiveElement','MRadioactiveElement|Create|Update',3,10,'SYSTEM','2015-02-13','2015-02-13'),(14,'fa fa-university','หน่วยงาน','/index.php/MDepartment','MDepartment|Create|Update',3,11,'SYSTEM','2015-02-13','2015-02-13'),(15,'fa fa-user','ผู้ใช้','/index.php/Users','Users|Create|Update',2,1,'SYSTEM','2015-02-13','2015-02-13'),(16,'fa fa-users','กลุ่มผู้ใช้','/index.php/UsersRole','UserRole|Create|Update',2,2,'SYSTEM','2015-02-13','2015-02-13'),(18,'fa fa-edit','3. เครื่องกำเนิดรังสี','/index.php/Form1','Form1|Create|Update',1,3,'SYSTEM','2015-02-13','2015-02-13'),(19,'fa fa-edit','4.1 ชนิดปิดผนึก','/index.php/Form2','Form2|Create|Update',33,1,'SYSTEM','2015-02-13','2015-02-13'),(20,'fa fa-edit','5. การเคลื่อนย้ายวัสดุกัมมันตรังสี','/index.php/Form3','Form3|Create|Update',1,5,'SYSTEM','2015-02-13','2015-02-13'),(21,'fa fa-tasks','2. บุคลากรทางรังสี','#','',1,2,'SYSTEM','2015-02-13','2015-02-13'),(22,'fa fa-edit','6. การกำจัดขยะรังสี','/index.php/Form5','Form5|Create|Update',1,6,'SYSTEM','2015-02-13','2015-02-13'),(23,'fa fa-edit','7. อุบัติเหตุทางรังสี','/index.php/Form6','Form6|Create|Update',1,7,'SYSTEM','2015-02-13','2015-02-13'),(24,'fa fa-edit','2.2 การอบรมทางรังสี','/index.php/Form7','Form7|Create|Update',21,2,'SYSTEM','2015-02-13','2015-02-13'),(25,'fa fa-puzzle-piece','ตำแหน่ง','/index.php/MPosition','MPosition|Create|Update',3,12,'SYSTEM','2016-05-31','2016-05-31'),(26,'fa fa-puzzle-piece','คำนำหน้าชื่อ','/index.php/MTitle','MTitle|Create|Update',3,13,'SYSTEM','2016-05-31','2016-05-31'),(27,'fa fa-edit','4.2 ชนิดไม่ปิดผนึก','/index.php/Frm21','Frm21|Create|Update',33,2,'SYSTEM','2016-05-30','2016-06-30'),(28,'fa fa-gear','ตั้งค่าระบบ','/index.php/MSetting','MSetting|Create|Update',3,14,'SYSTEM','2016-05-30','2016-05-30'),(29,'fa fa-pie-chart','บุคลากรทางรังสี','#',NULL,30,1,'SYSTEM','2016-07-02','2016-07-02'),(30,'icon-book-open','รายงาน','',NULL,-1,4,'SYSTEM','2016-07-02','2016-07-02'),(31,'fa fa-edit','8. ไฟล์รายชื่อกรรมการ','/index.php/Form8','Form8|Create|Update',1,8,'SYSTEM','2016-07-09','2016-07-09'),(32,'fa fa-edit','1. การบริหารจัดการความปลอดภัยทางรังสี','/index.php/FormQuestionnaire','FormQuestionnaire|Create|Update',1,1,'SYSTEM','2016-07-09','2016-07-09'),(33,'fa fa-tasks','4. วัสดุกัมมันตรังสี','#',NULL,1,4,'SYSTEM','2016-07-24','2016-07-24'),(34,'fa fa-edit','2.1 ปริมาณรังสีที่ได้รับ','/index.php/Form4','Form4|Create|Update',21,1,'SYSTEM','2016-07-24','2016-07-24'),(35,'fa fa-tasks','9. สรุปผล','#',NULL,1,9,'SYSTEM','2016-07-24','2016-07-24'),(36,'fa fa-edit','ปริมาณรังสีที่ได้รับ','/index.php/Report/R372','Report|R372',29,1,'SYSTEM','2016-07-24','2016-07-24'),(37,'fa fa-edit','การอบรมทางรังสี','/index.php/Report/R373','Report|R373',29,2,'SYSTEM','2016-07-24','2016-07-24'),(38,'fa fa-pie-chart','เครื่องกำเนิดรังสี','/index.php/Report/R32','Report|R32',30,2,'SYSTEM','2016-07-24','2016-07-24'),(39,'fa fa-pie-chart','การเคลื่อนย้ายวัสดุกัมมันตรังสี','/index.php/Report/R34','Report|R34',30,4,'SYSTEM','2016-07-24','2016-07-24'),(40,'fa fa-pie-chart','การกำจัดขยะรังสี','/index.php/Report/R35','Report|R35',30,5,'SYSTEM','2016-07-24','2016-07-24'),(41,'fa fa-pie-chart','อุบัติเหตุทางรังสี','/index.php/Report/R36','Report|R36',30,6,'SYSTEM','2016-07-24','2016-07-24'),(42,'fa fa-pie-chart','วัสดุกัมมันตรังสี','#',NULL,30,3,'SYSTEM','2016-07-24','2016-07-24'),(43,'fa fa-edit','ชนิดปิดผนึก','/index.php/Report/R331','Report|R331',42,1,'SYSETEM','2016-07-24','2016-07-24'),(44,'fa fa-edit','ชนิดไม่ปิดผนึก','/index.php/Report/R332','Report|R332',42,2,'SYSTEM','2016-07-24','2016-07-24');
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `menu_role`
--

DROP TABLE IF EXISTS `menu_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menu_role` (
  `ROLE_ID` int(11) NOT NULL,
  `MENU_ID` int(11) NOT NULL,
  `IS_ACTIVE` bit(1) DEFAULT NULL,
  `IS_REQUIRED_ACTION` bit(1) DEFAULT NULL,
  `IS_CREATE` bit(1) DEFAULT NULL,
  `IS_EDIT` bit(1) DEFAULT NULL,
  `IS_DELETE` bit(1) DEFAULT NULL,
  `UPDATE_BY` varchar(25) NOT NULL,
  `CREATE_DATE` date NOT NULL,
  `UPDATE_DATE` date NOT NULL,
  PRIMARY KEY (`ROLE_ID`,`MENU_ID`),
  KEY `mr_fk01_idx` (`MENU_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu_role`
--

LOCK TABLES `menu_role` WRITE;
/*!40000 ALTER TABLE `menu_role` DISABLE KEYS */;
INSERT INTO `menu_role` VALUES (1,1,'','\0','','','','1','2016-08-07','2016-08-07'),(1,2,'','\0','','','','1','2016-08-07','2016-08-07'),(1,3,'','\0','','','','1','2016-08-07','2016-08-07'),(1,4,'','\0','','','','1','2016-08-07','2016-08-07'),(1,5,'','\0','','','','1','2016-08-07','2016-08-07'),(1,6,'','\0','','','','1','2016-08-07','2016-08-07'),(1,7,'','\0','','','','1','2016-08-07','2016-08-07'),(1,8,'','\0','','','','1','2016-08-07','2016-08-07'),(1,9,'','\0','','','','1','2016-08-07','2016-08-07'),(1,10,'','\0','','','','1','2016-08-07','2016-08-07'),(1,11,'','\0','','','','1','2016-08-07','2016-08-07'),(1,12,'','\0','','','','1','2016-08-07','2016-08-07'),(1,13,'','\0','','','','1','2016-08-07','2016-08-07'),(1,14,'','\0','','','','1','2016-08-07','2016-08-07'),(1,15,'','\0','','','','1','2016-08-07','2016-08-07'),(1,16,'','\0','','','','1','2016-08-07','2016-08-07'),(1,18,'','\0','','','','1','2016-08-07','2016-08-07'),(1,19,'','\0','','','','1','2016-08-07','2016-08-07'),(1,20,'','\0','','','','1','2016-08-07','2016-08-07'),(1,21,'','\0','','','','1','2016-08-07','2016-08-07'),(1,22,'','\0','','','','1','2016-08-07','2016-08-07'),(1,23,'','\0','','','','1','2016-08-07','2016-08-07'),(1,24,'','\0','','','','1','2016-08-07','2016-08-07'),(1,25,'','\0','','','','1','2016-08-07','2016-08-07'),(1,26,'','\0','','','','1','2016-08-07','2016-08-07'),(1,27,'','\0','','','','1','2016-08-07','2016-08-07'),(1,28,'','\0','','','','1','2016-08-07','2016-08-07'),(1,29,'','\0','','','','1','2016-08-07','2016-08-07'),(1,30,'','\0','','','','1','2016-08-07','2016-08-07'),(1,31,'','\0','','','','1','2016-08-07','2016-08-07'),(1,32,'\0','\0','','','','1','2016-08-07','2016-08-07'),(1,33,'','\0','','','','1','2016-08-07','2016-08-07'),(1,34,'','\0','','','','1','2016-08-07','2016-08-07'),(1,35,'\0','\0','','','','1','2016-08-07','2016-08-07'),(1,36,'','\0','','','','1','2016-08-07','2016-08-07'),(1,37,'','\0','','','','1','2016-08-07','2016-08-07'),(1,38,'','\0','','','','1','2016-08-07','2016-08-07'),(1,39,'','\0','','','','1','2016-08-07','2016-08-07'),(1,40,'','\0','','','','1','2016-08-07','2016-08-07'),(1,41,'','\0','','','','1','2016-08-07','2016-08-07'),(1,42,'','\0','','','','1','2016-08-07','2016-08-07'),(1,43,'','\0','','','','1','2016-08-07','2016-08-07'),(1,44,'','\0','','','','1','2016-08-07','2016-08-07'),(2,1,'','\0','','','','1','2016-08-07','2016-08-07'),(2,2,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(2,3,'','\0','','','','1','2016-08-07','2016-08-07'),(2,4,'','\0','','','','1','2016-08-07','2016-08-07'),(2,5,'','\0','','','','1','2016-08-07','2016-08-07'),(2,6,'','\0','','','','1','2016-08-07','2016-08-07'),(2,7,'','\0','','','','1','2016-08-07','2016-08-07'),(2,8,'','\0','','','','1','2016-08-07','2016-08-07'),(2,9,'','\0','','','','1','2016-08-07','2016-08-07'),(2,10,'','\0','','','','1','2016-08-07','2016-08-07'),(2,11,'','\0','','','','1','2016-08-07','2016-08-07'),(2,12,'','\0','','','','1','2016-08-07','2016-08-07'),(2,13,'','\0','','','','1','2016-08-07','2016-08-07'),(2,14,'','\0','','','','1','2016-08-07','2016-08-07'),(2,15,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(2,16,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(2,18,'','\0','','','','1','2016-08-07','2016-08-07'),(2,19,'','\0','','','','1','2016-08-07','2016-08-07'),(2,20,'','\0','','','','1','2016-08-07','2016-08-07'),(2,21,'','\0','','','','1','2016-08-07','2016-08-07'),(2,22,'','\0','','','','1','2016-08-07','2016-08-07'),(2,23,'','\0','','','','1','2016-08-07','2016-08-07'),(2,24,'','\0','','','','1','2016-08-07','2016-08-07'),(2,25,'','\0','','','','1','2016-08-07','2016-08-07'),(2,26,'','\0','','','','1','2016-08-07','2016-08-07'),(2,27,'','\0','','','','1','2016-08-07','2016-08-07'),(2,28,'','\0','','','','1','2016-08-07','2016-08-07'),(2,29,'','\0','','','','1','2016-08-07','2016-08-07'),(2,30,'','\0','','','','1','2016-08-07','2016-08-07'),(2,31,'','\0','','','','1','2016-08-07','2016-08-07'),(2,32,'\0','\0','','','','1','2016-08-07','2016-08-07'),(2,33,'','\0','','','','1','2016-08-07','2016-08-07'),(2,34,'','\0','','','','1','2016-08-07','2016-08-07'),(2,35,'\0','\0','','','','1','2016-08-07','2016-08-07'),(2,36,'','\0','','','','1','2016-08-07','2016-08-07'),(2,37,'','\0','','','','1','2016-08-07','2016-08-07'),(2,38,'','\0','','','','1','2016-08-07','2016-08-07'),(2,39,'','\0','','','','1','2016-08-07','2016-08-07'),(2,40,'','\0','','','','1','2016-08-07','2016-08-07'),(2,41,'','\0','','','','1','2016-08-07','2016-08-07'),(2,42,'','\0','','','','1','2016-08-07','2016-08-07'),(2,43,'','\0','','','','1','2016-08-07','2016-08-07'),(2,44,'','\0','','','','1','2016-08-07','2016-08-07'),(3,1,'','\0','','','','1','2016-08-07','2016-08-07'),(3,2,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,3,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,4,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,5,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,6,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,7,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,8,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,9,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,10,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,11,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,12,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,13,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,14,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,15,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,16,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,18,'','\0','','','','1','2016-08-07','2016-08-07'),(3,19,'','\0','','','','1','2016-08-07','2016-08-07'),(3,20,'','\0','','','','1','2016-08-07','2016-08-07'),(3,21,'','\0','','','','1','2016-08-07','2016-08-07'),(3,22,'','\0','','','','1','2016-08-07','2016-08-07'),(3,23,'','\0','','','','1','2016-08-07','2016-08-07'),(3,24,'','\0','','','','1','2016-08-07','2016-08-07'),(3,25,'','\0','','','','1','2016-08-07','2016-08-07'),(3,26,'','\0','','','','1','2016-08-07','2016-08-07'),(3,27,'','\0','','','','1','2016-08-07','2016-08-07'),(3,28,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,29,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,30,'','\0','','','','1','2016-08-07','2016-08-07'),(3,31,'','\0','','','','1','2016-08-07','2016-08-07'),(3,32,'\0','\0','','','','1','2016-08-07','2016-08-07'),(3,33,'','\0','','','','1','2016-08-07','2016-08-07'),(3,34,'','\0','','','','1','2016-08-07','2016-08-07'),(3,35,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,36,'','\0','','','','1','2016-08-07','2016-08-07'),(3,37,'','\0','','','','1','2016-08-07','2016-08-07'),(3,38,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,39,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,40,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,41,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,42,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(3,43,'','\0','','','','1','2016-08-07','2016-08-07'),(3,44,'','\0','','','','1','2016-08-07','2016-08-07'),(4,1,'','\0','','','','1','2016-08-07','2016-08-07'),(4,2,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,3,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,4,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,5,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,6,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,7,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,8,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,9,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,10,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,11,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,12,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,13,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,14,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,15,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,16,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,18,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,19,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,20,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,21,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,22,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,23,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,24,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,25,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,26,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,27,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,28,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,29,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,30,'','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,31,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,32,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,33,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,34,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,35,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,36,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,37,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,38,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,39,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,40,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,41,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,42,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,43,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(4,44,'\0','\0','\0','\0','\0','1','2016-08-07','2016-08-07'),(5,1,'','\0','','','','1','2016-06-18','2016-06-18'),(5,2,'','\0','\0','\0','\0','1','2016-06-18','2016-06-18'),(5,3,'','\0','\0','\0','\0','1','2016-06-18','2016-06-18'),(5,4,'','\0','','','','1','2016-06-18','2016-06-18'),(5,5,'','\0','','','','1','2016-06-18','2016-06-18'),(5,6,'','\0','','','','1','2016-06-18','2016-06-18'),(5,7,'','\0','','','','1','2016-06-18','2016-06-18'),(5,8,'','\0','','','','1','2016-06-18','2016-06-18'),(5,9,'','\0','','','','1','2016-06-18','2016-06-18'),(5,10,'','\0','','','','1','2016-06-18','2016-06-18'),(5,11,'','\0','','','','1','2016-06-18','2016-06-18'),(5,12,'','\0','','','','1','2016-06-18','2016-06-18'),(5,13,'','\0','','','','1','2016-06-18','2016-06-18'),(5,14,'','\0','','','','1','2016-06-18','2016-06-18'),(5,15,'','\0','','','','1','2016-06-18','2016-06-18'),(5,16,'','\0','','','','1','2016-06-18','2016-06-18'),(5,18,'','\0','','','','1','2016-06-18','2016-06-18'),(5,19,'','\0','','','','1','2016-06-18','2016-06-18'),(5,20,'','\0','','','','1','2016-06-18','2016-06-18'),(5,21,'','\0','','','','1','2016-06-18','2016-06-18'),(5,22,'','\0','','','','1','2016-06-18','2016-06-18'),(5,23,'','\0','','','','1','2016-06-18','2016-06-18'),(5,24,'','\0','','','','1','2016-06-18','2016-06-18'),(5,25,'','\0','','','','1','2016-06-18','2016-06-18'),(5,26,'','\0','','','','1','2016-06-18','2016-06-18'),(5,27,'','\0','','','','1','2016-06-18','2016-06-18'),(5,28,'\0','\0','\0','\0','\0','1','2016-06-18','2016-06-18'),(6,1,'','\0','','\0','\0','1','2016-06-07','2016-06-07'),(6,2,'','\0','','','','1','2016-06-07','2016-06-07'),(6,3,'','\0','','','','1','2016-06-07','2016-06-07'),(6,4,'','\0','','','','1','2016-06-07','2016-06-07'),(6,5,'','\0','','','','1','2016-06-07','2016-06-07'),(6,6,'','\0','','','','1','2016-06-07','2016-06-07'),(6,7,'','\0','','','','1','2016-06-07','2016-06-07'),(6,8,'','\0','','','','1','2016-06-07','2016-06-07'),(6,9,'','\0','','','','1','2016-06-07','2016-06-07'),(6,10,'','\0','','','','1','2016-06-07','2016-06-07'),(6,11,'','\0','','','','1','2016-06-07','2016-06-07'),(6,12,'','\0','','','','1','2016-06-07','2016-06-07'),(6,13,'','\0','','','','1','2016-06-07','2016-06-07'),(6,14,'','\0','','','','1','2016-06-07','2016-06-07'),(6,15,'','\0','','','','1','2016-06-07','2016-06-07'),(6,16,'','\0','','','','1','2016-06-07','2016-06-07'),(6,18,'','\0','','','','1','2016-06-07','2016-06-07'),(6,19,'','\0','','','','1','2016-06-07','2016-06-07'),(6,20,'','\0','','','','1','2016-06-07','2016-06-07'),(6,21,'','\0','','','','1','2016-06-07','2016-06-07'),(6,22,'','\0','','','','1','2016-06-07','2016-06-07'),(6,23,'','\0','','','','1','2016-06-07','2016-06-07'),(6,24,'','\0','','','','1','2016-06-07','2016-06-07'),(6,25,'','\0','','','','1','2016-06-07','2016-06-07'),(6,26,'','\0','','','','1','2016-06-07','2016-06-07'),(6,27,'','\0','','','','1','2016-06-07','2016-06-07');
/*!40000 ALTER TABLE `menu_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form1`
--

DROP TABLE IF EXISTS `tb_form1`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form1` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rad_machine_id` int(11) DEFAULT NULL,
  `name` varchar(150) DEFAULT NULL,
  `code_usage_id` int(11) DEFAULT NULL,
  `maufacturer_id` int(11) DEFAULT NULL,
  `model` varchar(45) DEFAULT NULL,
  `serial_number` varchar(45) DEFAULT NULL,
  `use_type_id` int(11) DEFAULT NULL,
  `max_power` double DEFAULT NULL,
  `power_unit_id` int(11) DEFAULT NULL,
  `dealer_id` int(11) DEFAULT NULL,
  `license_no` varchar(45) DEFAULT NULL,
  `license_expire_date` date DEFAULT NULL,
  `supervisor_id` int(11) DEFAULT NULL,
  `mobile_number` varchar(45) DEFAULT NULL,
  `usage_status_id` int(11) DEFAULT NULL,
  `room_id` int(11) DEFAULT NULL,
  `setup_date` date DEFAULT NULL,
  `config_date` date DEFAULT NULL,
  `config_company_id` int(11) DEFAULT NULL,
  `sci_config_date` date DEFAULT NULL,
  `check_date` date DEFAULT NULL,
  `refer_doc` varchar(45) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `form1_fk01_idx` (`rad_machine_id`),
  KEY `form1_fk02_idx` (`code_usage_id`),
  KEY `form1_fk03_idx` (`maufacturer_id`),
  KEY `form1_fk04_idx` (`use_type_id`),
  KEY `form1_fk05_idx` (`power_unit_id`),
  KEY `form1_fk06_idx` (`dealer_id`),
  KEY `form1_fk07_idx` (`usage_status_id`),
  KEY `form1_fk08_idx` (`room_id`),
  KEY `from1_fk09_idx` (`create_by`),
  KEY `form1_fk10_idx` (`update_by`),
  CONSTRAINT `form1_fk01` FOREIGN KEY (`rad_machine_id`) REFERENCES `tb_m_rad_machine` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk02` FOREIGN KEY (`code_usage_id`) REFERENCES `tb_m_code_usage` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk03` FOREIGN KEY (`maufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk04` FOREIGN KEY (`use_type_id`) REFERENCES `tb_m_use_type` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk05` FOREIGN KEY (`power_unit_id`) REFERENCES `tb_m_power` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk06` FOREIGN KEY (`dealer_id`) REFERENCES `tb_m_dealer_company` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk07` FOREIGN KEY (`usage_status_id`) REFERENCES `tb_m_usage_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk08` FOREIGN KEY (`room_id`) REFERENCES `tb_m_room` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk09` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form1_fk10` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form1`
--

LOCK TABLES `tb_form1` WRITE;
/*!40000 ALTER TABLE `tb_form1` DISABLE KEYS */;
INSERT INTO `tb_form1` VALUES (50,5,NULL,19,6,'LightSpeed','991YCO',1,140,1,3,'4XM0410/58F','2017-05-27',22,NULL,1,9,'2008-02-11','2016-07-29',3,'2014-04-24',NULL,'GX33-214',22,22,'2016-08-05','2016-08-05',1,1,'T',0);
/*!40000 ALTER TABLE `tb_form1` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form2`
--

DROP TABLE IF EXISTS `tb_form2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(11) DEFAULT NULL,
  `ref_doc` varchar(20) DEFAULT NULL,
  `code_usage_id` int(11) DEFAULT NULL,
  `bpm_usage_status_id` int(11) DEFAULT NULL,
  `bpm_radioactive_elements_id` int(11) DEFAULT NULL,
  `bpm_manufacturer_id` int(11) DEFAULT NULL,
  `bpm_model` varchar(20) DEFAULT NULL,
  `bpm_phisical_id` int(11) DEFAULT NULL,
  `bpm_no` varchar(45) DEFAULT NULL,
  `bpm_volume` double DEFAULT NULL,
  `bpm_as_of_date` date DEFAULT NULL,
  `bpm_number` int(11) DEFAULT NULL,
  `machine_manufacturer_id` int(11) DEFAULT NULL,
  `machine_model` varchar(20) DEFAULT NULL,
  `machine_number` varchar(20) DEFAULT NULL,
  `machine_radioactive_highest` double DEFAULT NULL,
  `room_id` int(11) DEFAULT NULL,
  `machine_vendor_id` int(11) DEFAULT NULL,
  `material_status_id` int(11) DEFAULT NULL,
  `cause_increase_decrease` varchar(100) DEFAULT NULL,
  `license_no` varchar(20) DEFAULT NULL,
  `license_expire_date` date DEFAULT NULL,
  `supervisor_id` int(11) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `from2_fk01_idx` (`code_usage_id`),
  KEY `form2_fk02_idx` (`bpm_radioactive_elements_id`),
  KEY `form2_fk03_idx` (`machine_manufacturer_id`),
  KEY `form2_fk04_idx` (`bpm_manufacturer_id`),
  KEY `form2_fk05_idx` (`bpm_phisical_id`),
  KEY `form2_fk06_idx` (`machine_vendor_id`),
  KEY `form2_fk08_idx` (`supervisor_id`),
  KEY `form2_fk09_idx` (`bpm_usage_status_id`),
  KEY `form2_fk10_idx` (`create_by`),
  KEY `form2_fk11_idx` (`update_by`),
  CONSTRAINT `form2_fk01` FOREIGN KEY (`code_usage_id`) REFERENCES `tb_m_code_usage` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk02` FOREIGN KEY (`bpm_radioactive_elements_id`) REFERENCES `tb_m_radioactive_elements` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk03` FOREIGN KEY (`machine_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk04` FOREIGN KEY (`bpm_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk05` FOREIGN KEY (`bpm_phisical_id`) REFERENCES `tb_m_phisical_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk06` FOREIGN KEY (`machine_vendor_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk08` FOREIGN KEY (`supervisor_id`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk09` FOREIGN KEY (`bpm_usage_status_id`) REFERENCES `tb_m_usage_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk10` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form2_fk11` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form2`
--

LOCK TABLES `tb_form2` WRITE;
/*!40000 ALTER TABLE `tb_form2` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form2` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form3`
--

DROP TABLE IF EXISTS `tb_form3`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form3` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bpm_radioactive_elements_id` int(11) NOT NULL,
  `bpm_manufacturer_id` int(11) NOT NULL,
  `bpm_model` varchar(45) NOT NULL,
  `bpm_phisical_id` int(11) NOT NULL,
  `bpm_volume` double NOT NULL,
  `bpm_as_of_date` date NOT NULL,
  `bpm_number` int(11) NOT NULL,
  `bpm_no` varchar(45) NOT NULL,
  `machine_manufacturer_id` int(11) NOT NULL,
  `machine_model` varchar(45) NOT NULL,
  `machine_number` varchar(45) NOT NULL,
  `machine_radioactive_highest` double NOT NULL,
  `from_room_id` int(11) NOT NULL,
  `to_room_id` int(11) NOT NULL,
  `date_from` date NOT NULL,
  `date_to` date NOT NULL,
  `supervisor_id` int(11) NOT NULL,
  `create_by` int(11) NOT NULL,
  `create_date` date NOT NULL,
  `update_by` int(11) NOT NULL,
  `update_date` date NOT NULL,
  `revision` int(11) NOT NULL,
  `ref_doc` varchar(45) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `form3_fk01_idx` (`bpm_radioactive_elements_id`),
  KEY `form3_fk02_idx` (`bpm_manufacturer_id`),
  KEY `form3_fk03_idx` (`bpm_phisical_id`),
  KEY `form3_fk04_idx` (`machine_manufacturer_id`),
  KEY `form3_fk05_idx` (`supervisor_id`),
  KEY `form3_fk06_idx` (`from_room_id`),
  KEY `from3_fk07_idx` (`to_room_id`),
  KEY `from3_fk08_idx` (`create_by`),
  KEY `from3_fk09_idx` (`update_by`),
  CONSTRAINT `form3_fk01` FOREIGN KEY (`bpm_radioactive_elements_id`) REFERENCES `tb_m_radioactive_elements` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form3_fk02` FOREIGN KEY (`bpm_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form3_fk03` FOREIGN KEY (`bpm_phisical_id`) REFERENCES `tb_m_phisical_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form3_fk04` FOREIGN KEY (`machine_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form3_fk05` FOREIGN KEY (`supervisor_id`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form3_fk06` FOREIGN KEY (`from_room_id`) REFERENCES `tb_m_room` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `from3_fk07` FOREIGN KEY (`to_room_id`) REFERENCES `tb_m_room` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `from3_fk08` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `from3_fk09` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form3`
--

LOCK TABLES `tb_form3` WRITE;
/*!40000 ALTER TABLE `tb_form3` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form3` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form4`
--

DROP TABLE IF EXISTS `tb_form4`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form4` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  `hp_10_volume` double DEFAULT NULL,
  `hp_007_volume` double DEFAULT NULL,
  `hp_3_volume` double DEFAULT NULL,
  `report_date` date DEFAULT NULL,
  `report_period_id` int(11) DEFAULT NULL,
  `position_id` int(11) DEFAULT NULL,
  `rso_license_no` varchar(20) DEFAULT NULL,
  `rso_license_expire_date` date DEFAULT NULL,
  `is_rso_staff` varchar(1) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  `result` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `form3_fk_01_idx` (`position_id`),
  KEY `form4_fk02_idx` (`create_by`),
  KEY `form4_fk03_idx` (`update_by`),
  CONSTRAINT `form4_fk02` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form4_fk03` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form4_fk_01` FOREIGN KEY (`position_id`) REFERENCES `tb_m_position` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form4`
--

LOCK TABLES `tb_form4` WRITE;
/*!40000 ALTER TABLE `tb_form4` DISABLE KEYS */;
INSERT INTO `tb_form4` VALUES (8,'วีน้ส วิเศษแสง',0.07,0.07,0.07,'2016-06-28',1,4,'RSO-ML-1-000397/1','2019-09-30','1',22,'2016-08-05',22,'2016-08-05',1,1,'T',0,'');
/*!40000 ALTER TABLE `tb_form4` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form5`
--

DROP TABLE IF EXISTS `tb_form5`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form5` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `clear_date` date NOT NULL,
  `material_type_id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `phisical_status_id` int(11) NOT NULL,
  `rad_or_maximum_weight` double NOT NULL,
  `department_id` int(11) NOT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) NOT NULL,
  `update_date` date NOT NULL,
  `revision` int(11) NOT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `form5_fk01_idx` (`material_type_id`),
  KEY `form5_fk02_idx` (`phisical_status_id`),
  KEY `form5_fk03_idx` (`department_id`),
  KEY `form5_fk04_idx` (`create_by`),
  KEY `form5_fk05_idx` (`update_by`),
  CONSTRAINT `form5_fk01` FOREIGN KEY (`material_type_id`) REFERENCES `tb_m_material_type` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form5_fk02` FOREIGN KEY (`phisical_status_id`) REFERENCES `tb_m_phisical_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form5_fk03` FOREIGN KEY (`department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form5_fk04` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form5_fk05` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form5`
--

LOCK TABLES `tb_form5` WRITE;
/*!40000 ALTER TABLE `tb_form5` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form5` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form6`
--

DROP TABLE IF EXISTS `tb_form6`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form6` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ref_doc` varchar(20) DEFAULT NULL,
  `bpm_radioactive_elements_id` int(11) DEFAULT NULL,
  `bpm_manufacturer_id` int(11) DEFAULT NULL,
  `bpm_model` varchar(20) DEFAULT NULL,
  `bpm_phisical_id` int(11) DEFAULT NULL,
  `bpm_volume` double DEFAULT NULL,
  `bpm_as_of_date` date DEFAULT NULL,
  `bpm_number` int(11) DEFAULT NULL,
  `machine_manufacturer_id` int(11) DEFAULT NULL,
  `machine_model` varchar(20) DEFAULT NULL,
  `machine_number` varchar(20) DEFAULT NULL,
  `machine_radioactive_highest` double DEFAULT NULL,
  `room_id` int(11) DEFAULT NULL,
  `previous_leaks` double DEFAULT NULL,
  `after_leaks` double DEFAULT NULL,
  `accident_type_id` int(11) DEFAULT NULL,
  `accident_date` date DEFAULT NULL,
  `accident_room_id` int(11) DEFAULT NULL,
  `accident_situation` varchar(20) DEFAULT NULL,
  `accident_cause` varchar(20) DEFAULT NULL,
  `accident_count` int(11) DEFAULT NULL,
  `accident_estimated_loss` int(11) DEFAULT NULL,
  `accident_Prevention` varchar(20) DEFAULT NULL,
  `document_path` varchar(100) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `bpm_no` varchar(45) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `from6_fk01_idx` (`bpm_manufacturer_id`),
  KEY `form6_fk02_idx` (`bpm_phisical_id`),
  KEY `form6_fk03_idx` (`machine_manufacturer_id`),
  KEY `form6_fk04_idx` (`room_id`),
  KEY `form6_fk05_idx` (`accident_room_id`),
  KEY `form6_fk06_idx` (`bpm_radioactive_elements_id`),
  CONSTRAINT `form6_fk01` FOREIGN KEY (`bpm_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form6_fk02` FOREIGN KEY (`bpm_phisical_id`) REFERENCES `tb_m_phisical_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form6_fk03` FOREIGN KEY (`machine_manufacturer_id`) REFERENCES `tb_m_manufacturer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form6_fk04` FOREIGN KEY (`room_id`) REFERENCES `tb_m_room` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form6_fk05` FOREIGN KEY (`accident_room_id`) REFERENCES `tb_m_room` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form6_fk06` FOREIGN KEY (`bpm_radioactive_elements_id`) REFERENCES `tb_m_radioactive_elements` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form6`
--

LOCK TABLES `tb_form6` WRITE;
/*!40000 ALTER TABLE `tb_form6` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form6` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form7`
--

DROP TABLE IF EXISTS `tb_form7`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form7` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) DEFAULT NULL,
  `course` varchar(20) DEFAULT NULL,
  `department_id` int(11) DEFAULT NULL,
  `training_date` date DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'T',
  `update_from_id` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `form7_fk01_idx` (`department_id`),
  CONSTRAINT `form7_fk01` FOREIGN KEY (`department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form7`
--

LOCK TABLES `tb_form7` WRITE;
/*!40000 ALTER TABLE `tb_form7` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form7` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form8`
--

DROP TABLE IF EXISTS `tb_form8`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form8` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `file_path` varchar(100) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT NULL,
  `update_from_id` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `form8_fk01_idx` (`create_by`),
  KEY `form8_fk02_idx` (`update_by`),
  KEY `form8_fk03_idx` (`owner_department_id`),
  CONSTRAINT `form8_fk01` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form8_fk02` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `form8_fk03` FOREIGN KEY (`owner_department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form8`
--

LOCK TABLES `tb_form8` WRITE;
/*!40000 ALTER TABLE `tb_form8` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_form8` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form_questionnaire`
--

DROP TABLE IF EXISTS `tb_form_questionnaire`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form_questionnaire` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `number` varchar(45) DEFAULT NULL,
  `question` varchar(200) DEFAULT NULL,
  `parent` int(11) DEFAULT NULL,
  `seq` varchar(45) DEFAULT NULL,
  `has_attach_file` varchar(1) DEFAULT '0',
  `has_other` varchar(1) DEFAULT '0',
  `type` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form_questionnaire`
--

LOCK TABLES `tb_form_questionnaire` WRITE;
/*!40000 ALTER TABLE `tb_form_questionnaire` DISABLE KEYS */;
INSERT INTO `tb_form_questionnaire` VALUES (1,'3.1','การบริหารจัดการด้านความปลอดภัยทางรังสี',-1,'1','0','0',1,NULL),(2,'3.1.1','มีการแต่งตั้งคณะกรรมการหรือผู้รับผิดชอบในการกำกับ ดูแลงานด้านความปลอดภัยทางรังสี',1,'1','0','0',1,NULL),(3,'3.1.2','มีแต่งตั้งคณะกรรมการพิจารณารับรองงานวิจัยด้านรังสี',1,'2','0','0',1,NULL),(4,'3.1.3','มีแผนงานความปลอดภัยทางรังสีที่ครอบคลุมด้านต่าง ๆ ',1,'3','0','0',1,NULL),(5,'3.1.4','ส่วนงานมีการจัดทำเอกสารด้านความปลอดภัยในการใช้รังสี',1,'4','0','0',1,NULL),(6,'3.1.5','มีการตรวจสอบคุณภาพและขออนุญาตมีไว้ในครอบครอง นำเข้าหรือส่งออกเครื่องกำเนิดรังสีหรือสารกัมมันตรังสี ตามมาตรฐานและกฎหมายกำหนด',1,'5','0','0',1,NULL),(7,'3.1.6','มีการขึ้นทะเบียนเครื่องกำเนินรังสีและสารกัมมันตรังสี',1,'6','0','0',1,NULL),(8,'3.1.7','มีการตรวจสอบประเมินคุณภาพจากหน่วยงานที่ได้รับการรับรองมาตรฐานตามข้อกำหนดของสำนักงานปรมาณูเพื่อสันติ',1,'7','0','0',1,NULL),(9,'3.1.8','มีการจัดหาวัสดุที่สามารถป้องกันรังสีได้อย่างปลอดภัย และไม่เป็นอันตรายต่อบุคคลากรและบุคคลภายนอก (เช่น เสื้อตะกั่ว แว่นตากันรังสี ฉากกำบังรังสี)',1,'8','0','0',1,NULL),(10,'3.1.9','มีการควบคุมการใช้ปริมาณรังสี ตามหลัก As Low As Reasonably Achievable (ALARA) และกำกับ ดูแลให้ผลการตรวจจัดรังสีส่วนบุคคลไม่เกินเกณฑ์ที่กำหนด',1,'9','0','0',1,NULL),(11,'3.1.10','มีการจัดหาอุปกรณ์วัดรังสีประจำตัวบุคคลให้กับบคุลากรที่ปฏิบัติงานเกี่ยวกับรังสี',1,'10','0','0',1,NULL),(12,'3.1.11','มีการอบรมความปลอดภัยในการใช้รังสีให้กับบุคลากรก่อนปฏิบัติงานครั้งแรก',1,'11','0','0',1,NULL),(13,'3.1.12','มีการอบรมทบทวนความรู้ด้านความปลอดภัยในการใช้รังสีให้กับบุคลากรอย่างน้อยปีละ 1 ครั้ง',1,'12','0','0',1,NULL),(14,'3.1.13','มีระบบแจ้งเหตุ หรือการรายงานเกิดอุบัติเหตุทางรังสี',1,'13','0','0',1,NULL),(15,'3.1.14','มีการตรวจสอบอุบัติเหตุทางรังสี',1,'14','0','0',1,NULL),(16,NULL,'มี (โปรดแนบสำเนาคำสั่งแต่งตั้งคะณะกรรมการหรือรายชื่อผู้รับผิดชอบ (Radiation Safety Officer:RSO)',2,'1','1','0',2,NULL),(17,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',2,'2','0','1',2,NULL),(18,NULL,'มี (โปรดแนบสำเนาคำสั่งแต่งตั้งคณะกรรมการ)',3,'1','1','0',2,NULL),(19,NULL,'ไม่มี',3,'2','0','0',2,NULL),(20,NULL,'มีแผนงาน',4,NULL,'0','0',2,NULL),(21,NULL,'ด้านการปฏิบัติงานที่เกี่ยวข้องกับการใช้รังสีที่มีผลต่อสุขภาพ',20,'1','0','0',2,NULL),(22,NULL,'ด้านการประเมินความเสี่ยงด้านความปลอดภัยทางรังสี',20,'2','0','0',2,NULL),(23,NULL,'ด้านการโต้ตอบเหตุฉุกเฉินกรณีเกิดอุบัติเหตุจากรังสี',20,'3','0','0',2,NULL),(24,NULL,'ด้านการกำจัดกากกัมมันตรังสี',20,'4','0','0',2,NULL),(25,NULL,'ด้านการกำกับดูแล ติดตามและตรวจสอบการใช้รังสีให้เป็นไปตามที่กำหมายกำหนด',20,'5','0','0',2,NULL),(26,NULL,'อื่น ๆ (โปรดระบุ)',20,'6','0','1',2,NULL),(27,NULL,'ไม่มีแผนงาน คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',4,'1','0','1',2,NULL),(28,NULL,'มี',5,'1','0','0',2,NULL),(29,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',5,'2','0','1',2,NULL),(30,NULL,'คู่มือ',28,'1','0','0',2,NULL),(31,NULL,'แนวปฏิบัติ',28,'2','0','0',2,NULL),(32,NULL,'กฎ/ระเบียบ/ข้อบังคับ',28,'3','0','0',2,NULL),(33,NULL,'อื่น ๆ (โปรดระบุ)',28,'4','0','1',2,NULL),(34,NULL,'มี',6,'1','0','0',2,NULL),(35,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',6,'2','0','1',2,NULL),(36,NULL,'มี (โปรดแนบสำเนารายชื่อเครื่องกำเนิดรังสีและสารกัมมันตรังสีที่ขึ้นทะเบียน)',7,'1','1','0',2,NULL),(37,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',7,'2','0','1',2,NULL),(38,NULL,'มี จากหน่วยงาน',8,'1','1','0',2,NULL),(39,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',8,'2','0','0',2,NULL),(40,NULL,'กรมวิทยาศาสตร์การแพทย์',38,'1','0','0',2,NULL),(41,NULL,'อื่น ๆ (โปรดระบุ)',38,'2','0','1',2,NULL),(42,NULL,'มี',9,'1','0','0',2,NULL),(43,NULL,'ไม่มี คาดว่าจะมีการดำเนินการเสร็จสิ้นภายใน',9,'2','0','1',2,NULL),(44,NULL,'มี (โปรดแนบเอกสารแนวทางหรือวิธีการควบคุม)',10,'1','1','0',2,NULL),(45,NULL,'ไม่มี',10,'2','0','0',2,NULL),(46,NULL,'มี (โปรดระบุ)',11,'1','0','1',2,NULL),(47,NULL,'ไม่มี',11,'2','0','0',2,NULL),(48,NULL,'มี',12,'1','0','0',2,NULL),(49,NULL,'ไม่มี',12,'2','0','0',2,NULL),(50,NULL,'มี',13,'1','0','0',2,NULL),(51,NULL,'ไม่มี',13,'2','0','0',2,NULL),(52,NULL,'มี ผ่านระบบ',14,'1','0','0',2,NULL),(53,NULL,'เอกสาร',52,'1','0','0',2,NULL),(54,NULL,'ไม่มี',14,'2','0','0',2,NULL),(55,NULL,'อิเล็กทรอนิกส์ (โปรดระบุชื่อ)',52,'2','0','1',2,NULL),(56,NULL,'มี',15,'1','0','0',2,NULL),(57,NULL,'ไม่มี',15,'2','0','0',2,NULL);
/*!40000 ALTER TABLE `tb_form_questionnaire` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_form_questionnaire_answer`
--

DROP TABLE IF EXISTS `tb_form_questionnaire_answer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_form_questionnaire_answer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `form_questionnaire_id` int(11) DEFAULT NULL,
  `other_description` varchar(200) DEFAULT NULL,
  `file_path` varchar(200) DEFAULT NULL,
  `revision` int(11) DEFAULT NULL,
  `owner_department_id` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT NULL,
  `update_from_id` int(11) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_fqa01_idx` (`form_questionnaire_id`),
  KEY `fk_fqa02_idx` (`owner_department_id`),
  KEY `fk_fqa03_idx` (`create_by`),
  KEY `fk_fqa04_idx` (`update_by`),
  CONSTRAINT `fk_fqa01` FOREIGN KEY (`form_questionnaire_id`) REFERENCES `tb_form_questionnaire` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_fqa02` FOREIGN KEY (`owner_department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_fqa03` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_fqa04` FOREIGN KEY (`update_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=131 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_form_questionnaire_answer`
--

LOCK TABLES `tb_form_questionnaire_answer` WRITE;
/*!40000 ALTER TABLE `tb_form_questionnaire_answer` DISABLE KEYS */;
INSERT INTO `tb_form_questionnaire_answer` VALUES (115,16,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(116,18,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(117,20,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(118,21,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(119,28,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(120,30,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(121,35,'',NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(122,37,'',NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(123,38,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(124,42,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(125,44,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(126,46,'',NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(127,48,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(128,50,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(129,54,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02'),(130,56,NULL,NULL,1,3,'T',NULL,20,'2016-08-02',20,'2016-08-02');
/*!40000 ALTER TABLE `tb_form_questionnaire_answer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_code_usage`
--

DROP TABLE IF EXISTS `tb_m_code_usage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_code_usage` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_code_usage`
--

LOCK TABLES `tb_m_code_usage` WRITE;
/*!40000 ALTER TABLE `tb_m_code_usage` DISABLE KEYS */;
INSERT INTO `tb_m_code_usage` VALUES (1,'Security X-ray System'),(2,'Industrial X-Ray'),(3,'Research X-Ray'),(4,'Medical Diagnostic X-Ray'),(5,'Radiotherapy X-Ray'),(6,'LINAC'),(7,'Cyclotron'),(8,'Synchrotron'),(9,'Other'),(16,'Research'),(17,'รังสีรักษา'),(19,'รังสีวินิจฉัย'),(22,'Leakage Testing'),(23,'Standard Source'),(27,'อื่น ๆ โปรดระบุ');
/*!40000 ALTER TABLE `tb_m_code_usage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_dealer_company`
--

DROP TABLE IF EXISTS `tb_m_dealer_company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_dealer_company` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `addr` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_dealer_company`
--

LOCK TABLES `tb_m_dealer_company` WRITE;
/*!40000 ALTER TABLE `tb_m_dealer_company` DISABLE KEYS */;
INSERT INTO `tb_m_dealer_company` VALUES (1,'บ.ดีเคเอสเอช จำกัด','dsfsdf'),(2,'บ.สยามเดนท์ จำกัด',NULL),(3,'OAEP',NULL),(5,'TINT',NULL),(6,'Perkinelmer',NULL);
/*!40000 ALTER TABLE `tb_m_dealer_company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_department`
--

DROP TABLE IF EXISTS `tb_m_department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_department` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `faculty_id` varchar(45) DEFAULT NULL,
  `branch_id` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_department`
--

LOCK TABLES `tb_m_department` WRITE;
/*!40000 ALTER TABLE `tb_m_department` DISABLE KEYS */;
INSERT INTO `tb_m_department` VALUES (-1,'ผู้ดูแลระบบ',' ',' '),(1,'คณะแพทยศาสตร์ศิริราชพยาบาล',' ',' '),(3,'ศูนย์การแพทย์กาญจนาภิเษก',' ',' '),(4,'กองกายภาพและสิ่งแวดล้อม',' ',' '),(5,'กรรมการ',NULL,NULL),(6,'คณะแพทยศาสตร์โรงพยาบาลรามาธิบดี',NULL,NULL);
/*!40000 ALTER TABLE `tb_m_department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_manufacturer`
--

DROP TABLE IF EXISTS `tb_m_manufacturer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_manufacturer` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `country_id` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_manufacturer`
--

LOCK TABLES `tb_m_manufacturer` WRITE;
/*!40000 ALTER TABLE `tb_m_manufacturer` DISABLE KEYS */;
INSERT INTO `tb_m_manufacturer` VALUES (1,'Pacard instrument company','U.S.A.'),(2,'EG&G Berthold','Germany'),(3,'OAEP',' ไทย'),(4,'TINT','ไทย'),(5,'Ludlum Measurements Inc.','-'),(6,'xx','xx');
/*!40000 ALTER TABLE `tb_m_manufacturer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_material_status`
--

DROP TABLE IF EXISTS `tb_m_material_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_material_status` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_material_status`
--

LOCK TABLES `tb_m_material_status` WRITE;
/*!40000 ALTER TABLE `tb_m_material_status` DISABLE KEYS */;
INSERT INTO `tb_m_material_status` VALUES (1,'ใช้งานปกติ'),(2,'เก็บสำรอง'),(3,'ยกเลิกใช้งาน'),(4,'รอจัดการกาก'),(5,'กำลังสั่งนำเข้า'),(6,'ขอสำรอง');
/*!40000 ALTER TABLE `tb_m_material_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_material_type`
--

DROP TABLE IF EXISTS `tb_m_material_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_material_type` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_material_type`
--

LOCK TABLES `tb_m_material_type` WRITE;
/*!40000 ALTER TABLE `tb_m_material_type` DISABLE KEYS */;
INSERT INTO `tb_m_material_type` VALUES (1,'ประเภทวัสดุ 1'),(2,'ประเภทวัสดุ 2');
/*!40000 ALTER TABLE `tb_m_material_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_phisical_status`
--

DROP TABLE IF EXISTS `tb_m_phisical_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_phisical_status` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_phisical_status`
--

LOCK TABLES `tb_m_phisical_status` WRITE;
/*!40000 ALTER TABLE `tb_m_phisical_status` DISABLE KEYS */;
INSERT INTO `tb_m_phisical_status` VALUES (1,'ของแข็ง'),(2,'ของเหลว'),(3,'ก๊าซ');
/*!40000 ALTER TABLE `tb_m_phisical_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_position`
--

DROP TABLE IF EXISTS `tb_m_position`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_position` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_position`
--

LOCK TABLES `tb_m_position` WRITE;
/*!40000 ALTER TABLE `tb_m_position` DISABLE KEYS */;
INSERT INTO `tb_m_position` VALUES (1,'แพทย์'),(2,'พยาบาล'),(3,'นักวิทยาศาสตร์'),(4,'นักรังสี'),(5,'การแพทย์'),(6,'คนงาน'),(7,'อื่น ๆ');
/*!40000 ALTER TABLE `tb_m_position` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_power`
--

DROP TABLE IF EXISTS `tb_m_power`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_power` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_power`
--

LOCK TABLES `tb_m_power` WRITE;
/*!40000 ALTER TABLE `tb_m_power` DISABLE KEYS */;
INSERT INTO `tb_m_power` VALUES (1,'kV'),(2,'MeV'),(3,'mA');
/*!40000 ALTER TABLE `tb_m_power` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_rad_machine`
--

DROP TABLE IF EXISTS `tb_m_rad_machine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_rad_machine` (
  `id` int(11) NOT NULL,
  `name` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_rad_machine`
--

LOCK TABLES `tb_m_rad_machine` WRITE;
/*!40000 ALTER TABLE `tb_m_rad_machine` DISABLE KEYS */;
INSERT INTO `tb_m_rad_machine` VALUES (1,'Angiogram'),(2,'Bone mineral densitometry'),(3,'Blood irradiator (Gamar)'),(4,'Cobalt-60 Teletherapy Machine'),(5,'Computerized Tomography (CT)'),(6,'Computed Tomography Simulator'),(7,'Conventional Simulator'),(8,'CR'),(9,'Cyber Knife Machine'),(10,'Deep X-ray Therapy machine'),(11,'Dential X-ray machine'),(12,'Digital subtraction'),(13,'DR'),(14,'Fluoroscopy X-ray'),(15,'Gamma Knife Machine'),(16,'General X-ray'),(17,'High Dose Rate Remote Controlled Afterloading Brachytherapy System'),(18,'High Energy Medical Linear Accelerator (เครื่องเร่งอนุภาคที่ผลิตโฟตอนพลังงานมากกว่า 6 MV)'),(19,'Low Dose Rate Remote Controlled Afterloading Brachytherapy System,'),(20,'Low Energy Medical Linear Accelerator (เครื่องเร่งอนุภาคที่ผลิตโฟตอนพลังงานไม่เกิน 6 MV)'),(21,'MRI'),(22,'Mammography'),(23,'Mobile X-ray'),(24,'Orthovoltage'),(25,'PET-CT'),(26,'Portable'),(27,'Powder X-ray diffractometer'),(28,'SPEC'),(29,'Superficial X-ray Therapy Machine'),(30,'X-ray fluorescence spectrometer'),(31,'X-ray generator');
/*!40000 ALTER TABLE `tb_m_rad_machine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_radioactive_elements`
--

DROP TABLE IF EXISTS `tb_m_radioactive_elements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_radioactive_elements` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_radioactive_elements`
--

LOCK TABLES `tb_m_radioactive_elements` WRITE;
/*!40000 ALTER TABLE `tb_m_radioactive_elements` DISABLE KEYS */;
INSERT INTO `tb_m_radioactive_elements` VALUES (1,'Ba-133'),(2,'C-11'),(3,'C-14'),(4,'Ca-137'),(5,'Co-57'),(6,'Cr-51'),(7,'Cs-137'),(8,'F-18'),(9,'Ga-67'),(10,'Ga-68'),(11,'H-3'),(12,'I-131'),(13,'I-125'),(14,'I-123'),(15,'I-124'),(16,'Ir-192'),(17,'In-111'),(18,'N-13'),(19,'Ni-63'),(20,'O-15'),(21,'P-32'),(22,'Ra-223'),(23,'Re-186'),(24,'Re-188'),(25,'S-35'),(26,'Sm-153'),(27,'Sr-90'),(28,'Tc-99m'),(29,'Tl-201m'),(30,'U-acetate'),(31,'Y-90');
/*!40000 ALTER TABLE `tb_m_radioactive_elements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_room`
--

DROP TABLE IF EXISTS `tb_m_room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_room` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `number` varchar(45) DEFAULT NULL,
  `building_id` varchar(45) DEFAULT NULL,
  `room_plan` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_room`
--

LOCK TABLES `tb_m_room` WRITE;
/*!40000 ALTER TABLE `tb_m_room` DISABLE KEYS */;
INSERT INTO `tb_m_room` VALUES (1,'ห้องทดลอง 1','66','1','/uploads/muradbase_rpt21_01_2016-06-19.xls'),(2,'รังสีรักษาฯ ภ.รังสีวิทยา',NULL,'-',NULL),(3,'รังสีวิทยา /รังสีรักษา',NULL,'-',NULL),(4,'รังสีวิทยา/เวชศาสตร์นิวเคลียร์',NULL,'ศูนย์โรคหัวใจสมเด็จพระราชินีนาถ',NULL),(5,'รังสีเทคนิค',NULL,'วิทยาศาสตร์และเทคโนโลยีการแพทย์',NULL),(6,'รังสีวินิจฉัย',NULL,'ตระหนักจิตหะริณสุต',NULL),(7,'อายุรศาสตร์ เขตร้อน',NULL,'ตึก 50 ปี, ชั้น 4',NULL),(8,'อายุรศาสตร์ เขตร้อน',NULL,'ตึก 50 ปี, ชั้น 5',NULL),(9,'รังสีวินิจฉัย',NULL,'โรงพยาบาลสัตว์',NULL),(332,'32','3232','2332',NULL),(333,'d','d','d','/uploads/muradbase_rpt21_03_2016-06-19.xls');
/*!40000 ALTER TABLE `tb_m_room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_setting`
--

DROP TABLE IF EXISTS `tb_m_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_setting` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `value` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_setting`
--

LOCK TABLES `tb_m_setting` WRITE;
/*!40000 ALTER TABLE `tb_m_setting` DISABLE KEYS */;
INSERT INTO `tb_m_setting` VALUES (1,'แจ้งเตือน (ช่วงที่ 1)','180'),(2,'แจ้งเตือน (ช่วงที่ 2)','60');
/*!40000 ALTER TABLE `tb_m_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_title`
--

DROP TABLE IF EXISTS `tb_m_title`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_title` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_title`
--

LOCK TABLES `tb_m_title` WRITE;
/*!40000 ALTER TABLE `tb_m_title` DISABLE KEYS */;
INSERT INTO `tb_m_title` VALUES (1,'นาย'),(2,'นาง'),(3,'นางสาว');
/*!40000 ALTER TABLE `tb_m_title` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_usage_status`
--

DROP TABLE IF EXISTS `tb_m_usage_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_usage_status` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_usage_status`
--

LOCK TABLES `tb_m_usage_status` WRITE;
/*!40000 ALTER TABLE `tb_m_usage_status` DISABLE KEYS */;
INSERT INTO `tb_m_usage_status` VALUES (1,'ใช้งานอยู่'),(2,'หมดสภาพการใช้งาน'),(3,'บริจาค'),(4,'ส่งคืนพัสดุ');
/*!40000 ALTER TABLE `tb_m_usage_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_use_type`
--

DROP TABLE IF EXISTS `tb_m_use_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_use_type` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_use_type`
--

LOCK TABLES `tb_m_use_type` WRITE;
/*!40000 ALTER TABLE `tb_m_use_type` DISABLE KEYS */;
INSERT INTO `tb_m_use_type` VALUES (1,'Fixes'),(2,'Mobile'),(3,'Portable'),(4,'Stationary');
/*!40000 ALTER TABLE `tb_m_use_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_logged`
--

DROP TABLE IF EXISTS `users_logged`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users_logged` (
  `USER_ID` int(11) NOT NULL,
  `IP_ADDRESS` varchar(20) NOT NULL,
  `SESSION_ID` varchar(255) NOT NULL,
  `LAST_SIGN_IN_DATE` datetime NOT NULL,
  `UPDATE_BY` varchar(25) NOT NULL,
  `CREATE_DATE` datetime NOT NULL,
  `UPDATE_DATE` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_logged`
--

LOCK TABLES `users_logged` WRITE;
/*!40000 ALTER TABLE `users_logged` DISABLE KEYS */;
/*!40000 ALTER TABLE `users_logged` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_login`
--

DROP TABLE IF EXISTS `users_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users_login` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `role_id` int(11) NOT NULL,
  `username` varchar(100) DEFAULT NULL,
  `password` varchar(100) DEFAULT '1234',
  `latest_login` datetime DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  `is_force_change_password` bit(1) DEFAULT b'0',
  `title_id` int(11) DEFAULT NULL,
  `first_name` varchar(45) DEFAULT NULL,
  `last_name` varchar(45) DEFAULT NULL,
  `mobile_phone` varchar(45) DEFAULT NULL,
  `department_id` int(11) DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_role_id_idx` (`role_id`),
  KEY `fk_title_id_2_idx` (`title_id`),
  KEY `fk_department_id_idx` (`department_id`),
  CONSTRAINT `fk_department_id` FOREIGN KEY (`department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_role_id` FOREIGN KEY (`role_id`) REFERENCES `users_role` (`ROLE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_id_2` FOREIGN KEY (`title_id`) REFERENCES `tb_m_title` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_login`
--

LOCK TABLES `users_login` WRITE;
/*!40000 ALTER TABLE `users_login` DISABLE KEYS */;
INSERT INTO `users_login` VALUES (1,1,'admin','161ebd7d45089b3446ee4e0d86dbcf92','2015-03-05 17:10:31','pawit1357@hotmail.com',0,'2015-02-13 15:43:58','A','\0',1,'Cochem','-','-',-1,1,'2016-07-31'),(2,2,'cochem01','bb725bb008e426a1f3fcc5521324ff52','2016-03-21 21:25:13','cochem01@mahidol.ac.th',1,'2016-03-21 21:25:13','A','\0',1,'cochem01',' ','0000000000',4,1,'2016-07-31'),(14,3,'usr01','70868e96e218e5990e5910883835a4af',NULL,'usr01',1,'2016-06-05 10:50:40','A','\0',1,'usr01',' ','0000000000',3,1,'2016-07-31'),(15,2,'cochem03','c8d1a976fc07087cb93b38d362ede091',NULL,'cochem03@mahidol.ac.th',1,'2016-06-18 14:07:04','A','\0',1,'cochem03',' ','0000000000',4,1,'2016-07-31'),(16,2,'cochem02','d9e688ddb0f75ddd697c55f9e2e6c433',NULL,'cochem01@mahidol.ac.th',1,'2016-07-31 06:56:00','A','',1,'cochem02',' ','0000000000',4,1,'2016-07-31'),(17,3,'usr02','970e15d60d8d5b728be5267dc7691919',NULL,'usr02',1,'2016-07-31 06:57:49','A','',1,'usr02',' ','0000000000',3,1,'2016-07-31'),(18,3,'usr03','807627ca3fa00671977708b8b052a78e',NULL,'usr03',1,'2016-07-31 06:58:22','A','',1,'usr03',' ','0000000000',3,1,'2016-07-31'),(19,3,'usr04','080e420a7fff930b08256da9e0289d80',NULL,'usr04',1,'2016-07-31 06:59:19','A','',1,'usr04',' ','0000000000',3,1,'2016-07-31'),(20,3,'usr05','6078646ea7f16ba9a3d8936455a716ba',NULL,'usr05',1,'2016-07-31 06:59:49','A','',1,'usr05',' ','0000000000',3,1,'2016-07-31'),(21,3,'usr06','36d9fba95b33caec9830b65f6156e853',NULL,'usr06',1,'2016-07-31 07:00:28','A','',1,'usr06',' ','0000000000',1,1,'2016-07-31'),(22,3,'usr07','fad19b201138930166c38457306efbb0',NULL,'usr07',1,'2016-07-31 07:00:52','A','',1,'usr07',' ','0000000000',1,1,'2016-07-31'),(23,3,'usr08','15957ddf53d1eaa048bd2662ab9f2905',NULL,'usr08',1,'2016-07-31 07:01:18','A','',1,'usr08',' ','0000000000',1,1,'2016-07-31'),(24,3,'usr09','db075773848c5d463382a1f82aa3c4fa',NULL,'usr09',1,'2016-07-31 07:01:42','A','',1,'usr09',' ','0000000000',1,1,'2016-07-31'),(25,3,'usr10','1b748fd48b4a60413d983168ab44d617',NULL,'usr10',1,'2016-07-31 07:02:06','A','',1,'usr10',' ','0000000000',1,1,'2016-07-31'),(29,3,'usr11','3e380696eaf00d2365be9249ba13dc1a',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr11','','',5,1,'2016-08-01'),(30,3,'usr12','6f0d21aa455295c2973a881aedfc1b03',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr12','','',5,1,'2016-08-01'),(31,3,'usr13','94ae705f024316488e107508dbe2d4fe',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr13','','',5,1,'2016-08-01'),(32,3,'usr14','c23028e22d67960f59ae41dcd33a5a5d',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr14','','',5,1,'2016-08-01'),(33,3,'usr11','fede2961e811505b7ecc3cc6e415370e',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr15','','',5,1,'2016-08-01'),(34,3,'usr16','47db5ba508582d443c03385b2caef8ca',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr16','','',6,1,'2016-08-01'),(35,3,'usr17','6ea818801e2ceb20f44dfa3bbc4c47b7',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr17','','',6,1,'2016-08-01'),(36,3,'usr18','c4e0c776895fae6357c8945eaebb217d',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr18','','',6,1,'2016-08-01'),(37,3,'usr19','8ca2cd2dc8842e8248ac492b898551b2',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr19','','',6,1,'2016-08-01'),(38,3,'usr20','4c82cf0800743165efa04081bc0c6814',NULL,'',1,'2016-08-01 12:35:03','A','',1,'usr20','','',6,1,'2016-08-01');
/*!40000 ALTER TABLE `users_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_role`
--

DROP TABLE IF EXISTS `users_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users_role` (
  `ROLE_ID` int(11) NOT NULL,
  `ROLE_NAME` varchar(50) NOT NULL,
  `ROLE_DESC` varchar(100) DEFAULT NULL,
  `UPDATE_BY` varchar(25) NOT NULL,
  `CREATE_DATE` date NOT NULL,
  `UPDATE_DATE` date NOT NULL,
  PRIMARY KEY (`ROLE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_role`
--

LOCK TABLES `users_role` WRITE;
/*!40000 ALTER TABLE `users_role` DISABLE KEYS */;
INSERT INTO `users_role` VALUES (1,'ADMIN','SYSTEM','1','2016-08-07','2016-08-07'),(2,'STAFF','สามารถดูข้อมูลได้ทั้งหมด','1','2016-08-07','2016-08-07'),(3,'USER','เปิดข้อมูลได้บางส่วนและไม่สามารถแก้ไขข้อมูล','1','2016-08-07','2016-08-07'),(4,'EXCUTIVE','','1','2016-08-07','2016-08-07');
/*!40000 ALTER TABLE `users_role` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-08-14 15:22:15
