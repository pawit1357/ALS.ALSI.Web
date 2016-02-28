CREATE DATABASE  IF NOT EXISTS `alsi` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `alsi`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: 202.47.250.203    Database: alsi
-- ------------------------------------------------------
-- Server version	5.6.19

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
-- Table structure for table `job_info`
--

DROP TABLE IF EXISTS `job_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_info` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `contract_person_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `date_of_request` date DEFAULT NULL,
  `date_of_receive` date DEFAULT NULL,
  `customer_ref_no` varchar(45) DEFAULT NULL COMMENT 'job number',
  `company_name_to_state_in_report` varchar(255) DEFAULT NULL,
  `job_prefix` int(11) NOT NULL,
  `s_pore_ref_no` varchar(45) DEFAULT NULL,
  `spec_ref_rev_no` varchar(45) DEFAULT NULL,
  `sample_diposition` varchar(1) DEFAULT NULL COMMENT '0=Discard,1=Return All',
  `status_sample_enough` varchar(1) DEFAULT NULL,
  `status_sample_full` varchar(1) DEFAULT NULL,
  `status_personel_and_workload` varchar(1) DEFAULT NULL,
  `status_test_tool` varchar(1) DEFAULT NULL,
  `status_test_method` varchar(1) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `document_type` varchar(1) DEFAULT '1' COMMENT '1=,2=,3=',
  PRIMARY KEY (`ID`),
  KEY `fk_contract_person_id_idx` (`contract_person_id`),
  KEY `fk_customer_id_idx` (`customer_id`),
  CONSTRAINT `fk_contract_person_id` FOREIGN KEY (`contract_person_id`) REFERENCES `m_customer_contract_person` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_customer_id` FOREIGN KEY (`customer_id`) REFERENCES `m_customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_info`
--

LOCK TABLES `job_info` WRITE;
/*!40000 ALTER TABLE `job_info` DISABLE KEYS */;
INSERT INTO `job_info` VALUES (17,7,7,'2015-02-21',NULL,'','',3,'','','Y','N','Y','Y','Y','Y',1,1,'2015-02-21','2015-02-21','1'),(18,17,18,'2015-02-22','2015-02-22','','',1,'','','Y','N','Y','Y','Y','Y',1,1,'2015-02-22','2015-02-22','1');
/*!40000 ALTER TABLE `job_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_reiew_requistion`
--

DROP TABLE IF EXISTS `job_reiew_requistion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_reiew_requistion` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `job_id` int(11) DEFAULT NULL,
  `detail` varchar(45) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_job_info_id_idx` (`job_id`),
  CONSTRAINT `fk_job_info_id` FOREIGN KEY (`job_id`) REFERENCES `job_info` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_reiew_requistion`
--

LOCK TABLES `job_reiew_requistion` WRITE;
/*!40000 ALTER TABLE `job_reiew_requistion` DISABLE KEYS */;
/*!40000 ALTER TABLE `job_reiew_requistion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_running`
--

DROP TABLE IF EXISTS `job_running`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_running` (
  `ID` int(11) NOT NULL,
  `prefix` varchar(45) DEFAULT NULL,
  `running_number` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_running`
--

LOCK TABLES `job_running` WRITE;
/*!40000 ALTER TABLE `job_running` DISABLE KEYS */;
INSERT INTO `job_running` VALUES (1,'ELP',5),(2,'ELS',2),(3,'FA',11),(4,'ELWA',1),(5,'GRP',1),(6,'TRB',1);
/*!40000 ALTER TABLE `job_running` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_sample`
--

DROP TABLE IF EXISTS `job_sample`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_sample` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `job_id` int(11) NOT NULL,
  `specification_id` int(11) NOT NULL,
  `type_of_test_id` int(11) NOT NULL,
  `template_id` int(11) NOT NULL,
  `job_number` varchar(45) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `model` varchar(255) DEFAULT NULL,
  `surface_area` varchar(255) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL,
  `no_of_report` int(11) DEFAULT NULL,
  `uncertainty` varchar(1) DEFAULT NULL,
  `po_number` varchar(45) DEFAULT NULL,
  `date_of_step_1` date DEFAULT NULL,
  `date_of_step_2` date DEFAULT NULL,
  `date_of_step_3` date DEFAULT NULL,
  `date_of_step_4` date DEFAULT NULL,
  `date_of_step_5` date DEFAULT NULL,
  `date_of_step_6` date DEFAULT NULL,
  `owner_of_step_1` int(11) DEFAULT NULL,
  `owner_of_step_2` int(11) DEFAULT NULL,
  `owner_of_step_3` int(11) DEFAULT NULL,
  `owner_of_step_4` int(11) DEFAULT NULL,
  `owner_of_step_5` int(11) DEFAULT NULL,
  `owner_of_step_6` int(11) DEFAULT NULL,
  `job_status` int(11) DEFAULT NULL,
  `job_role` int(11) DEFAULT NULL,
  `due_date` date DEFAULT NULL,
  `path_word` varchar(100) DEFAULT NULL,
  `path_pdf` varchar(100) DEFAULT NULL,
  `status_completion_scheduled` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_job_id_idx` (`job_id`),
  KEY `type_of_test_fk_idx` (`type_of_test_id`),
  KEY `fk_specification_id_idx` (`specification_id`),
  KEY `fk_status_id_idx` (`job_status`),
  CONSTRAINT `fk_spec_id` FOREIGN KEY (`specification_id`) REFERENCES `m_specification` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_status_id` FOREIGN KEY (`job_status`) REFERENCES `m_status` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `job_info_fk` FOREIGN KEY (`job_id`) REFERENCES `job_info` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `type_of_test_fk` FOREIGN KEY (`type_of_test_id`) REFERENCES `m_type_of_test` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_sample`
--

LOCK TABLES `job_sample` WRITE;
/*!40000 ALTER TABLE `job_sample` DISABLE KEYS */;
INSERT INTO `job_sample` VALUES (46,17,1,1,-1,'FA-00011-F','RQ13-0662 Kojin APFA ww# 10 After Crest# 11 cell# 14 MinAik/ Engtek/ TI+SUMI/ Rencol/ NMB/ IPT(ECD)','Kojin APFA ','APFA = 143 cm2 AFA = 132 cm2','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,10,1,'2015-03-01',NULL,NULL,1),(47,17,1,2,29,'FA-00011-F','RQ13-0662 Kojin APFA ww# 10 After Crest# 11 cell# 14 MinAik/ Engtek/ TI+SUMI/ Rencol/ NMB/ IPT(ECD)','Kojin APFA ','APFA = 143 cm2 AFA = 132 cm2','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,12,1,'2015-03-01',NULL,NULL,1),(48,17,1,3,29,'FA-00011-F','RQ13-0662 Kojin APFA ww# 10 After Crest# 11 cell# 14 MinAik/ Engtek/ TI+SUMI/ Rencol/ NMB/ IPT(ECD)','Kojin APFA ','APFA = 143 cm2 AFA = 132 cm2','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,'2015-03-01','20150223/FA-00011-F.doc','uploads/20150222/FA-00011-F.pdf',2),(49,18,1,14,-1,'ELP-00005-IB','11111','2222','3333','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,10,1,'2015-03-02',NULL,NULL,1),(50,18,1,7,-1,'ELP-00005-DB','11111','2222','3333','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,10,1,'2015-03-02',NULL,NULL,3),(51,18,1,8,-1,'ELP-00005-G','11111','2222','3333','',1,'N',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,10,1,'2015-03-02',NULL,NULL,2);
/*!40000 ALTER TABLE `job_sample` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_completion_scheduled`
--

DROP TABLE IF EXISTS `m_completion_scheduled`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_completion_scheduled` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `value` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_completion_scheduled`
--

LOCK TABLES `m_completion_scheduled` WRITE;
/*!40000 ALTER TABLE `m_completion_scheduled` DISABLE KEYS */;
INSERT INTO `m_completion_scheduled` VALUES (1,'Normal',8),(2,'Urgent',5),(3,'Express',3);
/*!40000 ALTER TABLE `m_completion_scheduled` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_customer`
--

DROP TABLE IF EXISTS `m_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_customer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `customer_code` varchar(45) DEFAULT NULL,
  `company_name` varchar(45) DEFAULT NULL,
  `address` varchar(200) DEFAULT NULL,
  `sub_district` varchar(45) DEFAULT NULL,
  `mobile_number` varchar(45) DEFAULT NULL,
  `email_address` varchar(45) DEFAULT NULL,
  `branch` varchar(45) DEFAULT NULL,
  `district` varchar(45) DEFAULT NULL,
  `ext` varchar(45) DEFAULT NULL,
  `department` varchar(45) DEFAULT NULL,
  `province` varchar(45) DEFAULT NULL,
  `code` varchar(45) DEFAULT NULL,
  `tel_number` varchar(45) DEFAULT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` date DEFAULT NULL,
  `update_by` int(11) DEFAULT NULL,
  `update_date` date DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=179 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_customer`
--

LOCK TABLES `m_customer` WRITE;
/*!40000 ALTER TABLE `m_customer` DISABLE KEYS */;
INSERT INTO `m_customer` VALUES (3,'','3M Thailand Limited','159 Asoke-Montri Road','','','ctangtreerat@mmm.com','','','','Customer Technical Center','','','+66 2326 0025',0,'2015-01-27',0,'2015-02-12','A'),(4,'','3M Thailand Limited','150 Soi Chalongkrung 31','Ladkrabang','+66 85 441 9193','achakkaew@mmm.com','','Ladkrabang','2287','Customer Technical Center','Bangkok','10520','+66 2326 0025',0,'2015-01-27',0,'2015-01-27','A'),(5,'CDGAAFIN01','Aaf International (Thailand) Co.,Ltd.','100 Moo 4 Bangkaew','Bangplee-Yai','','witoon.pon@aafthailand.com','Head Office','Bangplee','','Sales','Samutprakarn','10540','+66 2738 7788',0,'2015-01-27',0,'2015-01-27','A'),(6,'CDGAAPIC01','Aapico Hitech Pasts Co., Ltd.','99/2 Moo 1 Hi-tech Industrial Estate','Ban-Lane','+66 81 321 4264','Atthakorn.t@aapico.com','Head Office','Bang Pa-In','','QA Engineer','Ayutthaya','13160','+66 3535 0880',0,'2015-01-27',0,'2015-01-27','A'),(7,'CDGADAMP01','Adampak (Thailand) Limited','700/686 Moo 1 Amata Nakorn Industrial Estate','Phanthong','','waraporn@adampak.co.th','Head Office','Phanthong','144','QA Engineer','Chonburi','20160','+66 3807 9850',0,'2015-01-27',0,'2015-01-27','A'),(8,'CDGADVAN01','Advance Packaging Co., Ltd.','57 Moo 9 Rojana Industrial Park','Thanu','','qa@advance-pack.co.th','Head Office','U-Thai','','','Ayutthaya','13210','+66 3533 0568',0,'2015-01-27',0,'2015-01-27','A'),(9,'CDGAGRUT01','Agru Technology (Thailand) Co., Ltd.','420/51 Kanchanapisek Road','Dokmai','+66 83 135 8998','Pisit@agru.co.th','Head Office','Pravet ','','Sales Engineer Manager','Bangkok','10250','+66 2728 4477',0,'2015-01-27',0,'2015-01-27','A'),(10,'CDGALLEN01','Allen Enineering Co.,Ltd. ','226 Moo 6 Rama 2 Road','Smaedam','+66 86 688 8341','ittthikorn@allenthai.com','Head Office','Bangkhuntien','','Sales Engineer','Bangkok','10150','',0,'2015-01-27',0,'2015-01-27','A'),(11,'CDGALMON01','Almond (Thailand) Limited ','31 Moo 14, Bangchan Industrial Estate Seri Thai Road','Minburi','+66 86 565 7231','almondbkk@almond.co.th','Head Office','Minburi','','Quality Assurance ','Bangkok','10510','+66 2517 0042',0,'2015-01-27',0,'2015-01-27','A'),(12,'CDGALTUM01','Altum Precision Co., Ltd.','133 Moo 1 Hi-Tech Industrial Estate Asia-Nakornsawan Rd.,','Ban-Lane','+66 86 105 8722','lab.apt@altum.co.th','Head Office','Bang Pa-In','','QA Lab','Ayutthaya   ','13160','+66 3572 9100',0,'2015-01-27',0,'2015-01-27','A'),(13,'CDGAPCB001','Apcb Electronics (Thailand) Co., Ltd.','139/2 Moo 2 BangPa-In Industrial Estate Udomsorayuth Road,','Klong-Jig','','waleepun@apcb.co.th','Head Office','Bang Pa-In','1320','QC','Ayutthaya  ','13160','+66 3525 8222',0,'2015-01-27',0,'2015-01-27','A'),(14,'CDGASIAN01','Asian Micro (Thailand) Co., Ltd.','130/171-2 Moo 3 Factory Land','Wangchula','','jeeranan@asianmicro.co.th','Head Office','Wangnoi','','Quality Assurance ','Ayutthaya','13170','+66 3572 2022',0,'2015-01-27',0,'2015-01-27','A'),(15,'CDGASTCL01','A.S.T. Clean Label Co., Ltd.','242 Soi Soonvijai 4, Rama 9 Rd.,','Bangkapi','','onnisara_a@astcleanlabel.com','Head Office','Huai Khwang','30','Quality Assurance','Bangkok','10310','+66 2719 6377-9',0,'2015-01-27',0,'2015-01-27','A'),(16,'CDGATMCO01','A.T.M. Corporation Ltd','179/5 Moo 7','Lamtasao','','atm.corporation@yahoo.com','Head Office','Wangnoi','','','Ayutthaya ','13170','+66 3579 8134',0,'2015-01-27',0,'2015-01-27','A'),(17,'CDGBELLS01','Bell Survey Ltd.','87/77-81 5th Flr. Modern Town Bldd.,Ekamai Soi 3, Sukh. 63','Klongtoey-Nua','','Martin.covell@bellsurveybkk.th.com','Head Office','Wattana','','','Bangkok','10110','+66 2381 7291',0,'2015-01-27',0,'2015-01-27','A'),(18,'CDGBELTO01','Belton Industrial (Thailand) Ltd.','101/110 Moo 20 , Phaholyothin Rd.,','Klongneung','','puntharee.s@beltontechnology.com','Head Office','Klong Luang','1905','QA / M&P Lab','Pathumthani ','12120','+66 2529 7300',0,'2015-01-27',0,'2015-01-27','A'),(19,'CDGBENCH01','Benchmark Electronics (Thailand) Pcl.','94 Moo 1, Hi-Tech Industrial Estate','Ban-Lane','','Runglawan.Seeburth@bench.com','Head Office','Bang Pa-In','6770','Quality Assurance ','Ayutthaya','13160','+66 3527 6300',0,'2015-01-27',0,'2015-01-27','A'),(20,'CDGBESCO01','Besco & C Co.,Ltd','39 Moo 10','Nongkainam ','','walaipon@bandc.co.th','Head Office','Nong-Khae','','Accounting','Saraburi','18140','+66 3638 0927-8',0,'2015-01-27',0,'2015-01-27','A'),(21,' CDGBESTR01','Bestrade Precision Limited.','160 Moo 4','Makhamkou','+66 83 899 6346','patpong@bestradegroup.com','Head Office','Nikompattana','','Quality  Control  Supervisor','Rayong','21180','+66 3889 3800-4',0,'2015-01-27',0,'2015-01-27','A'),(22,'CDGBKKRO01','Bangkok Royal Express Co.,Ltd','57/214 Moo 3','Sam Ruen','','panachonp@gmail.com','Head Office','Bang Pa-In','','Quality Control','Ayutthaya','13170','+66 2531 5818',0,'2015-01-27',0,'2015-01-27','A'),(23,'CDGBOYD001 ','Boyd Technologies (Thailand) Co., Ltd.','555 Rasa Tower ll, Unit 1403, the 14th Floor. Phaholyothin Road','Chatuchuk','+66 89 920 3494','sukanya.prasertsung@boydcorp.com','Head Office','Chatuchuk','','Lab & Contamination Control ','Bangkok','10900','+66 2793 9200',0,'2015-01-27',0,'2015-01-27','A'),(24,'CDGBPCAS01','Bp - Castrol (Thailand) Limited.','23rd Floor, Rajanakarn Building 183 South Sathorn Road','Yannawa ','','sukanya.chantaworakit@se1.bp.com','Head Office','Yannawa ','','Technical Manager ','Bangkok','10120','+66 2684 3555',0,'2015-01-27',0,'2015-01-27','A'),(25,'CDGBUSIN01','Bissiness Logistic (Thailand) Co., Ltd.','1168/32 Lumpini Tower, 16th Fl., Rama IV Rd.,','Thungmahamek','','charnchai@blt.th.com','Head Office','Sathorn','','Packaging','Bangkok','10120','+66 2285 5998',0,'2015-01-27',0,'2015-01-27','A'),(26,'CDGCAMFI01','Camfil Farr (Thailand) Ltd.','A3 Fl.,Le-Concorde Tower, Room No.A305,202,Ratchadapisek Rd','Huai Khwang','+66 85 111 6710','Bantita.Takkhapiwat@camfil.com','Head Office','Huai Khwang','','Product Development Engineer ','Bangkok','10310','+66 2694 1480-84',0,'2015-01-27',0,'2015-01-27','A'),(27,'CDGCANON01','Canon Hi-Tech (Thailand) Ltd ','789 Moo 1','Naklang','+66 86 873 8006','chairat_l@cht.canon.co.th','Branch 0006','Soongnuen','','Products Quality Assurance ','Nakhonratchasima','30380','',0,'2015-01-27',0,'2015-01-27','A'),(28,'CDGCANON02','Canon Hi-Tech (Thailand) Ltd.','Hi-Tech Industrial Estate 89 Moo 1,','Bhan-Lain','','jiraporn@cht.canon.co.th','Head Office','Bang Pa-In','2521','PRQA','Ayutthaya','13160','+66 3535 0080',0,'2015-01-27',0,'2015-01-27','A'),(29,'CDGCELES01','Celestica (Thailand) Ltd','49/18 Moo 5,Leam Chabang Industrial Estate,','Tungsukhla','+66 81 840 5083','dnatapor@celestica.com','Head Office','Sriracha','3476','Lead Failure Analysis Engineer','Chonburi','20230','+66 3849 3561',0,'2015-01-27',0,'2015-01-27','A'),(30,'CDGCENT001','Cent - Engineering (Thailand) Co., Ltd.','70/3 M.9, Rojana Industraial Park, Rojana Road','Thanu','','iso-network-coordinator@cent-eng.com','Head Office','U-Thai','','Quality Assurance ','Ayutthaya','13210','+66 3533 1246',0,'2015-01-27',0,'2015-01-27','A'),(31,'CDGCLEAN01','Cleanstat (Thailand) Co., Ltd.','207 Moo 1','Ban-Lane','','Kanuengnit@cleanstat.com','Head Office','Bang Pa-In','','QA/Lab','Ayutthaya','13160','+66 3535 0555',0,'2015-01-27',0,'2015-01-27','A'),(32,'CDGCOLIB01','Colibri Assembly (Thailand) Co., Ltd.','150/82 Moo 9, Pinthong Industrial Estate2','Nongkham','+66 89 066 3125','sunanta.s@colibri-assembly.com ','Head Office','Sriracha','','QS Officer ','Chonburi','20110','+66 3300 5118',0,'2015-01-27',0,'2015-01-27','A'),(33,'CDGCOMPA01','Compart Precision (Thailand) Co., Ltd.','135 Moo 1 Hi-Tech Industrial Estate','Banpo','+66 84 094 4633','patchara.to@compart-grp.com','Head Office','Bang Pa-In','','M&P Lab Asst. Manager.','Ayutthaya','13160','+66 3531 5600',0,'2015-01-27',0,'2015-01-27','A'),(34,'CDGCONTI01','Continental Automotive (Thailand) Co.,Ltd.','7/259 Moo6 Amata City Industrial Estate','Mabyangporn','','Rungnapa.pasuriwong@continental-corporation.c','Head Office','Pluakdaeng','6347','Laboratory Manager / Quality','Rayong','21140','+66 3892 6299',0,'2015-01-27',0,'2015-01-27','A'),(35,'CDGDDK0001','Ddk (Thailand) Ltd','55/25 Moo 13, Navanakorn Industrial Estate,','Klong Nueng','','thiraporn@ddk.fujikura.co.th','Head Office','Klong Luang','1117','Quality Assurance ','Pathumthani','12120','+66 2529 1428',0,'2015-01-27',0,'2015-01-27','A'),(36,'CDGDEESI01','Deesiri Trading Co., Ltd','89/37  Moo 4','Bangtalard','+66 94 651 4664','deesiri.sales@hotmail.com','Head Office','Pakkret','','Sales manager','Nonthaburi ','11120','',0,'2015-01-27',0,'2015-01-27','A'),(37,'CDGDISKP01','Disk Precision Industries(Thailand) Co., Ltd.','193 Moo. 1 Hi Tech Industrial Estate,','Ban-Lane','+66 86 125 5152','warut@diskprecision.co.th','Head Office','Bang Pa-In','','NPD&QA Engineer','Ayutthaya','13160','+66 3531 4501-5',0,'2015-01-27',0,'2015-01-27','A'),(38,'CDGDONAL01','Donaldson (Thailand)  Ltd.','Amata City Industrial Estate 7/217 Moo 6,Soi Pornprapa,','Mabyangpron','','natnapat.methanorapat@donaldson.com','Head Office','Pluakdaeng','2504','Senior Chemist','Rayong','21140','+66 3865 0280',0,'2015-01-27',0,'2015-01-27','A'),(39,'CDGDOUYE01','Dou Yee Enterprises (Thailand) Co., Ltd. ','75/27 Moo 11, Phaholyothin Road,','Klongnueng','','bksales3@douyee.co.th','Head Office','Klong Luang','','','Pathumthani','12120','',0,'2015-01-27',0,'2015-01-27','A'),(40,'CDGDRUBB01','D-Rubber Products Co.,Ltd. ','38 MOO 4','Bua-Loi','','purchase@d-rubber.co.th','Head Office','Nong-Khae','','Purchasing','Saraburi','18140','+66 3637 3234-5',0,'2015-01-27',0,'2015-01-27','A'),(41,'CDGECOGS01','Eco Green Solutions Co.,Ltd','1/292','Bangpood','+66 81 988 3996','Sirikitjarak996@yahoo.com','Head Office','Pakkret','','','Nonthaburi','11120','',0,'2015-01-27',0,'2015-01-27','A'),(42,'CDGEMERS0','Emerson Electric (Thailand) Ltd.','24 Moo 4, Eastern Seaboard Industrial Estate,','Pluakdaeng','+66 81 945 5833','Yeamyuth.M@Emercon.com','Head Office','Pluakdaeng','','Facility','Rayong','21140','+66 3895 7390',0,'2015-01-27',0,'2015-01-27','A'),(43,'CDGEMPOR01','Emporio Controls Co., Ltd. ','205 Soi Charansanitwong 40, Charansanitwong Rd.','Bangyeekhan','+66 86 993 2026','wannuttee@emporiocontrols.com','Head Office','Bangplad','','Senior Sales Engineer','Bangkok','10700','+66 2433 6990',0,'2015-01-27',0,'2015-01-27','A'),(44,'CDGENPRO01','Enpro Products (Thailand) Co., Ltd.','101/47/18 M.20, Navanakorn Industrial Estate,Phaholyothin Rd.','Klong Nueng','+66 81-639 0394','yuthtaphoom_s@enpro.co.th','Head Office','Klong Luang','','Sale & CSM','Pathumthani','12120','+66 2529 1380-82',0,'2015-01-27',0,'2015-01-27','A'),(45,'CDGEXACT01','Exact Design & Tooling. Co., Ltd.','19/17 Moo 8','Klongha','+66 89 814 2276','aree_s@exact-thailand.com ','Head Office','Klong Luang','','','Pathumthani','12120','+66 2904 6177  ',0,'2015-01-27',0,'2015-01-27','A'),(46,'CDGEXCEL01','Excellent Product Manufacturing Co.,Ltd.','10 Moo 2 Phaholyotin Road, KM. 57,','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya','13210','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27','A'),(47,'CDGFAGOR01','Fagor Electronics (Thailand) Ltd.','Wellgrow Industrial Estate, Bangna-Trad KM.36, 82 Moo 5','Bangsamak','+66 89 767 0202','clyde_magsino@fagorelectronics.co.th','Head Office','Bangpakong','','Purchasing Manager','Chachoengsao ','24180','+66 3857 0087',0,'2015-01-27',0,'2015-01-27','A'),(48,'CDGFOAMT01','Foamtec International Co., Ltd. ','111/1 Moo 2 K.M.56 of Phaholyothin Road,','Lamsai','','pornpitp@foamtecintl.com  ','Branch 0003','Wangnoi ','','Quality Assurance ','Ayutthaya','13170','+66 3574 0717',0,'2015-01-27',0,'2015-01-27','A'),(49,'CDGFOAMT02','Foamtec International Co., Ltd.','700/50,52,54 Moo 6 Amata-Nakorn Industrial Estate,57 KM of Bangna-Trad Rd.','Nongmaidang','+66 81 555 8659','netnapay@foamtecintl.com ','Branch 0002','Muang','319','QA Supervisor','Chonburi ','20000','+66 3846 5795-800',0,'2015-01-27',0,'2015-01-27','A'),(50,' CDGFOAMT03','Foamtec International Co., Ltd. ','Free Trade Zone 259/1 Moo 3,','Toongsukla','+66 86 972 6138 ','namphungf@foamtecintl.com','Branch 0006','Sriracha','','Senior Data Controller','Chonburi ','20230','+66 3367 8877',0,'2015-01-27',0,'2015-01-27','A'),(51,'CDGFOAMT05','Foamtec International Co., Ltd.','259 Moo 3, Leam Chabang Industrial Estate, Export Processing Zone 1    ','Toongsukla','','daraneep@foamtecintl.com','Branch 0004','Sriracha','832','R&D','Chonburi ','20230','+66 3840 1888',0,'2015-01-27',0,'2015-01-27','A'),(52,'CDGFORTU01','Fortune And Star Technology Co., Ltd.','1597 Soi Ladprao 94 (Phanchamit), Ladprao Rd.,','Wangthonglang','+66 86 030 0398','surasak@fnstechnology.com','Head Office','Wangthonglang','','Business Development Manager','Bangkok','10310','+66 2516 1407',0,'2015-01-27',0,'2015-01-27','A'),(53,'CDGFUJIK01','Fujikura Electronics (Thailand) Ltd. ','118/2 Moo 11 Suwannasorn Road ','Banpra','','uthen.w@th.fujikura.com','Branch 0004','Muang','3123','QA Engineer','Prachinburi ','25230','+66 3721 3323',0,'2015-01-27',0,'2015-01-27','A'),(54,'CDGFUJIK02','Fujikura Electronics (Thailand) Ltd.','68/1 Moo 4,Northern Region Industrial Estate  ','Ban Klang','','pattamon.k@th.fujikura.com','Branch 0006','Muang','2936','Laboratory Quality Assurance','Lumphun','51000','+66 5358 1002',0,'2015-01-27',0,'2015-01-27','A'),(55,'CDGFUJIT01','Fujitsu Ten (Thailand) Co., Ltd.',' 253 M.11 Rojana Industrial Estate                                                                                                                                                                      ','Bankhai','','supansa@fttl.ten.fujitsu.com','Head Office','Nongbua','652','Purchasing officer ','Rayong      ','21120','+66 3896 2025-30',0,'2015-01-27',0,'2015-01-27','A'),(56,'CDGGREEN01','Green Pack Industries Co., Ltd.','40/8 Moo 5','Chiangraknoi ','+66 86 345 6553','Amornchai_t@g-p-industries.com','Head Office','Bang Pa-In ','','Production','Ayutthaya   ','13180','+66 3574 6005-6',0,'2015-01-27',0,'2015-01-27','A'),(57,'CDGGREEN02','Greendii Co.,Ltd.  ','55/157 Moo 2','Lumphakkood','+66 89 900 9413','Sudarat@greendii.com','Head Office','Thanyaburi','','','Pathumthani  ','12110','+66 2150 7694 - 7   ',0,'2015-01-27',0,'2015-01-27','A'),(58,'CDGHAGEM01','Hagemeyer - Pps (Thailand) Ltd.  ','21 Tower, 16 th Fl, 805 Srinakarin Rd.','Suanluang','+66 81 931 2166 ','nitjawan.inthajakr@hagemeyerasia.com','Head Office','Suanluang','','Sourcing Engineer','Bangkok','10250','+66 2529 5910-3 ',0,'2015-01-27',0,'2015-01-27','A'),(59,'CDGHANAS01','Hana Semiconductor (Ayutthaya) Co., Ltd.','Asia Road, K.M. 59, 100 Moo 1','Baan-Lane','','DoungdaoP@ayt.hanabk.th.com','Head Office','Bang Pa-In','','Environmental Officer / Facility','Ayutthaya   ','13160','+66 2209 8000',0,'2015-01-27',0,'2015-01-27','A'),(60,'CDGHARAD01','Harada Corporation (Thailand) Co.,Ltd ','147,9th Floor Unit 903 Rangsit-Pathumthani Road','Prachatipat','+66 81 840 5389','sakchai@haradacorp.co.jp ','Head Office','Thanyaburi','','Sale','Pathumthani  ','12130','+66 2959 2188    ',0,'2015-01-27',0,'2015-01-27','A'),(61,'CDGHENKE01','Henkel (Thailand) Ltd.  ','The Offices at Centralworld, 35th Floor,.999/9 Rama 1 Rd.,','Pathumwan','+66 81 939 2808','Tula.Chankana@henkel.com','Head Office','Pathumwan','','Senior Sales Engineer ','Bangkok ','10330','+66 2209 8000',0,'2015-01-27',0,'2015-01-27','A'),(62,'CDGHIP0001','Hi-P (Thailand) Co., Ltd.    ','7/132 Moo 4,','Mabyangporn','','Patinya.p@hi-p.com','Head Office','Pluakdeang','','Customer Quality Engineer','Rayong ','21140','+66 3865 0432-3 ',0,'2015-01-27',0,'2015-01-27','A'),(63,'CDGHUTCH02 ','Hutchinson Technology Operations (Thailand) C','50 Moo 4, Rojana Ind.park, Ph 8 (FZ), ','U-Thai','+66 87 201 9195 ','sukanya.yujan@hti.htch.com','Head Office','U-Thai','7309','Chemical Lab Engineer ','Ayutthaya ','13210','+66 3533 4800  ',0,'2015-01-27',0,'2015-01-27','A'),(64,'CDGIME0001','I.M.E. (Thailand) Co., Ltd.    ',' 1/64 Moo 5 Rojana Industrial Park, Rojana Road,','Khan-Ham','','sittiporn@ime.co.th','Head Office','U-Thai','','QA','Ayutthaya ','13210','+66 3533 0801',0,'2015-01-27',0,'2015-01-27','A'),(65,'CDGINNOV02','Innovalues Precision (Thailand) Ltd.   ','83 Moo 2 Hi-Tech Industrial  Estate,   ','Bann-Len','+66 90 961 3636','warunya@innovalues.com','Head Office','Bang Pa-In','1513','CQE/ QA&QC ','Ayutthaya   ','13160','+66 3536 1701-5 ',0,'2015-01-27',0,'2015-01-27','A'),(66,'CDGINOUT01','Inout Enterprise (Thailand) Co., Ltd. ','310/89 Moo 2   ','Sam Ruen  ','','aniruth@inoutthai.com','Head Office','Bang Pa-In','','Sales','Ayutthaya   ','13160','+66 3570 9801-2 ',0,'2015-01-27',0,'2015-01-27','A'),(67,'CDGINTEG01 ','Integrated Metal Finishing (Thailand) Co., Lt','129/18-19 Moo 3 ,Phaholyothin Rd., ','Wangchula','+66 86 523 6555','suriam@itmf.co.th','Head Office','Wangnoi','','Material Control','Ayutthaya   ','13170','+66 3572 1126-8 ',0,'2015-01-27',0,'2015-01-27','A'),(68,'CDGINTRI01','Intriplex (Thailand) Ltd.  ','158-160 Moo 1, Hi-Tech Industrial Estate, ','Baan-Lane','+66 81 754 9808','Supaktra.Sanguanrat@mmi.com.sg','Head Office','Bang Pa-In','','Sr.Chemical and LAB Engineer','Ayutthaya   ','13160','+66 3572 9183',0,'2015-01-27',0,'2015-01-27','A'),(69,'CDGISCM001','Iscm Technology (Thailand) Co., Ltd.      ','70/5 Moo 9,   ','Thanu','','mana@iscmthai.com','Head Office','U-Thai','','Quality Assurance Manager ','Ayutthaya ','13210','+66 3580 0116-9 ',0,'2015-01-27',0,'2015-01-27','A'),(70,'CDGISCM002','Iscm Technology (Thailand) Co., Ltd. ','70/6 Moo 9,   ','Thanu','','mana@iscmthai.com','Branch 0001','U-Thai','','Quality Assurance Manager ','Ayutthaya ','13210','+66 3580 0116-9',0,'2015-01-27',0,'2015-01-27','A'),(71,'CDGJCY0001','Jcy Hdd Technology Co., Ltd    ','70 Moo. 4 SIL Industrial,   ','Bua-Loi','','srirahayu@jcyinternational.com','Head Office','Nong-Khae','132','QA / Lab','Saraburi   ','18140','+66 3637 3992',0,'2015-01-27',0,'2015-01-27','A'),(72,'CDGJETHO01','Je Tho Vision Co., Ltd.  ','37/4 Moo 1,','Ban-Chang','','panachonp@gmail.com','Head Office','U-Thai','','Quality Control','Ayutthaya ','13210','+66 3525 5262',0,'2015-01-27',0,'2015-01-27','A'),(73,'CDGKATAY01','Katayama Advanced Precision (Thailand) Ltd.  ',' 42/6-7 Moo 4  ','Ban-Chang','','sompong@katayama-ap.co.th','Branch 0001','U-Thai','504','Quality Assurance Engineer','Ayutthaya ','13210','+66 3574 6608-11 ',0,'2015-01-27',0,'2015-01-27','A'),(74,'CDGKTEC001','K-Tech Industrial (Thailand) Co., Ltd      ','7/297 Moo 6 ','Mabyangporn  ','+66 86 320 6456','Saleela.w@ktech-th.co.th ','Head Office','Pluakdeang   ','8002','Quality Assurance Department','Rayong       ','21140','+66 3803 6210-16',0,'2015-01-27',0,'2015-01-27','A'),(75,'CDGLASER01','Laser Printing (Thailand) Co.,Ltd      ','Amata Nakorn Industrial Estate, 700/394 Moo 6,    ','Don Hau Roh','+66 85 071 1626','qclaserthi@csloxinfo.com','Head Office','Muang','','Quality Assurance Engineer','Chonburi ','20000','+66 3846 5222  ',0,'2015-01-27',0,'2015-01-27','A'),(76,'CDGLEADE01','Leader Industries Ltd., Part    ','150/78 Moo 3 ,Teparuk Road,   ','Bangplee-Yai','','bancha_r@leaderindustries.co.th','Head Office','Bangplee','','QA','Samutprakarn','10540','+66 2385 5585 ',0,'2015-01-27',0,'2015-01-27','A'),(77,'CDGLINDE01','Linde (Thailand) Public Company Limited ',' 15th Floor, Bangna Tower A, 2/3 M. 14, Bangna Trad Rd','Bangkaew','+66 85 109 5682 ','Sutthipong.Itthipalangkul@linde.com','Head Office','Bangplee','','Engineer','Samutprakarn','10540','+66 2338 0800 ',0,'2015-01-27',0,'2015-01-27','A'),(78,'CDGLINTE01','Lintec Bkk Pte Ltd.',' 11 Q. House Sathorn Bldg., 8th Fl., South Sathorn','Thungmahamek','','dusit@lintec.com.sg','Head Office','Sathorn    ','','Sales','Bangkok ','10120','+66 2287 0660   ',0,'2015-01-27',0,'2015-01-27','A'),(79,'CDGLINTEC2','Lintec (Thailand) Co., Ltd.   ','128/2 Moo 5 Bangna-Trad Road,','Bangsamak','','dusit@lintec.com.sg','Head Office','Bangpakong','','Sales','Chachoengsao  ','24130','+66 3857 1195 ',0,'2015-01-27',0,'2015-01-27','A'),(80,'CDGMACSY01','Macsys Industries (Thailand) Co., Ltd.    ','789/23 Moo 1, Soi 3 Pinthong Industrial Estate  ','Nongkham','+66 87 859 0620','jariya@macsth.co.th','Head Office','Sriracha','','Production Engineer','Chonburi        ','20230','+66 3834 8411-3',0,'2015-01-27',0,'2015-01-27','A'),(81,'CDGMAGNE01','Magnecomp Precision Technology Public Co., Lt','162 Moo 5, Phaholyothin Road, ','Lamsai','','kittichaip@magnecomp.com','Head Office','Wangnoi','2835','SQE','Ayutthaya ','13170','+66 3521 5225',0,'2015-01-27',0,'2015-01-27','A'),(82,' CDGMARUA01','Maruai (Asia) Co., Ltd.   ','Hi-Tech Industrial Estate 135 Moo.1   ','Ban-Lane','','i.teerawat@maruai-asia.co.th','Head Office','Bang Pa-In','','Quality Assurance ','Ayutthaya   ','13160','+66 3535 0663-5 ',0,'2015-01-27',0,'2015-01-27','A'),(83,'CDGMATER01','Material Expertise Co., Ltd.',' 50 Moo.1','Klong Pra Udom','','adul@malugo.co.th','Head Office','Latlumkaew ','','','Pathumthani ','12140','+66 2194 5904-9',0,'2015-01-27',0,'2015-01-27','A'),(84,'CDGMEKTE01','Mektec Manufacuring Corporation (Thailand) Lt','560 Moo2, Bangpa-in Industrial Estate, Udomsorayuth Rd','Klong-Jik','','pallapar@mektec.co.th','Head Office','Bang Pa-In','4552','Sr. Lab Chemistry officer','Ayutthaya   ','13160','+66 3525 8888',0,'2015-01-27',0,'2015-01-27','A'),(85,'CDGMIMAN01 ','Mi Manufacturing (Thailand) Ltd.  ','7/239 Moo 6,    ','Mabyangporn','','daungrutai.boonyen@mi-st-group.com','Head Office','Pluakdaeng','','QA','Rayong       ','21140','+66 3865 0587-8',0,'2015-01-27',0,'2015-01-27','A'),(86,'CDGMITTA01','Mittapab Plastic Industry Co.,Ltd. ','10/25 Moo 5 Soi Subpaisan 3, Leabklongsewapasawat Rd     ','Khokkrabue','+66 81 854 5843','suphoj@mittapab.com','Head Office','Muang ','','','Samutsakorn  ','74000','',0,'2015-01-27',0,'2015-01-27','A'),(87,'CDGMIYOS01','Miyoshi Hi-Tech Co., Ltd. ','38 MOO1, Hi-Tech Industrial Estate ','Banpo','','wivornchai.mht@th.miyoshi.biz','Head Office','Bang Pa-In','','Quality Assurance','Ayutthaya   ','13160','+66 3531 4031-4  ',0,'2015-01-27',0,'2015-01-27','A'),(88,'CDGMKL0001','Mkl Tool Limited Partnership','26/273-275 Moo 18     ','Klong-Nueng','','purchase_mkl@hotmail.com','Head Office','Klong Luang','','','Pathumthani  ','12120','+66 2908-1107',0,'2015-01-27',0,'2015-01-27','A'),(89,'CDGMMIPA01','Mmi Precision Assembly (Thailand) Co., Ltd.','888 Moo 1, Mittraphap Road','Naklang','','daungnapa@mmi.com.sg','Head Office','Sungnoen','1202','QA Contamination','Nakhonratchasima ','30380','+66 4429 1579 ',0,'2015-01-27',0,'2015-01-27','A'),(90,'CDGMMIPF01  ','Mmi Precision Forming (Thailand) Limited. ','70/8-11 Moo 9 Rojana Industrial Park','Thanu','+66 89 666 3097','noppawan@mmi.com.sg','Head Office','U-Thai','','Senior Engineer, MSL Lab','Ayutthaya   ','13210','+66 3571 9339',0,'2015-01-27',0,'2015-01-27','A'),(91,'CDGMODUL01','Modular Engineered Products Supply Co., Ltd. ',' 39/13-16 Unit 4D., 4th Floor, Soi Suanplu, Sathorn Rd.','Thungmahamek','+66 81 694 3804','nuantip@mepscosystems.com','Head Office','Sathorn','','Purchasing Manager','Bangkok','10120','+66 2755 0779',0,'2015-01-27',0,'2015-01-27','A'),(92,'CDGMPM0001','Mpm Technology (Thailand) Limited','101/79, Moo 20 Navanakorn Industrial Estate','Klong Nung','+66 87 517 8490','nithima@mmi.com.sg','Head Office','Klong Luang','','QA Engineer','Pathumthani ','12120','+66 2909 0909',0,'2015-01-27',0,'2015-01-27','A'),(93,'CDGMTEC001','National Metal And Meterials Technology','114 Thailand Science Park Paholyothin Rd.','Klong Nung','','daruneea@mtec.or.th','Head Office','Klong Luang','4757','Plastics Technology Lab','Pathumthani','12120','+66 2564 6500',0,'2015-01-27',0,'2015-01-27','A'),(94,'CDGMYLER01','Myler Company Limited ','25/11 Moo 8','Chimplee','','pornpimol@myler.co.th','Head Office','Talingchan','','','Bangkok','10170','+66 2422 3975   ',0,'2015-01-27',0,'2015-01-27','A'),(95,'CDGNEXAS01','Nexas Elechemic Co., Ltd.  ','115/10 M. 4, Saharatananakorn Industrial Estate','Bangphrakru','+66 89 923 3156','cqa1@nexasthai.com','Head Office','Nakornluang','','QA','Ayutthaya   ','13260','+66 3571 6576-7',0,'2015-01-27',0,'2015-01-27','A'),(96,'CDGNHK0001','Nhk Spring (Thailand) Co., Ltd.','115 M. 5, Bangna Trad Rd, ','Bangsamak','','piyachat.mab@nhkspg.co.th','Branch 0003','Bangpakong ','5242','Quality Assurance  Engineer','Chachoengsao','24180','+66 3884 2830',0,'2015-01-27',0,'2015-01-27','A'),(97,'CDGNIDEC01 ','Nidec Component Technology (Thailand) Co., Lt','38 Moo 1','Bua-Loi','+66 90 429 5861','put.saelew@nidec.com','Head Office','Nong-Khae','304','Lab Sr.Engineer','Saraburi ','18140','+66 3637 3741-4',0,'2015-01-27',0,'2015-01-27','A'),(98,'CDGNIDEC02','Nidec Precision (Thailand) Co., Ltd. ','(NPT R/F) 29 Moo 2, U-Thai-Pachi Rd., ','Ban-Chang','','NPTA_CAL@notes.nidec.co.jp','Head Office','U-Thai','307','NPTA_Calibration','Ayutthaya ','13210','+66 3574 6683-6',0,'2015-01-27',0,'2015-01-27','A'),(99,'CDGNIDEC03','Nidec Electronics (Thailand) Co., Ltd. ','199/12 Moo 3, Thunyaburi-Lumlookka Road, ','Rangsit','','Kitisak.Pandaranantaka@nidec.com','Head Office','Thanyaburi','2214','QA','Pathumthani  ','12110','+66 2577 5077  ',0,'2015-01-27',0,'2015-01-27','A'),(100,'CDGNIDEC05','Nidec Electronics (Thailand) Co., Ltd.  ','Rojana Factory 44 Moo 9, Rojana Industrial Park','Thanu','','NETR_QA-LAB@notes.nidec.co.jp','Branch 0002','U-Thai','3901','QA-LAB','Ayutthaya   ','13210','+66 3533 0742',0,'2015-01-27',0,'2015-01-27','A'),(101,'CDGNIDEC06','Nidec Precision (Thailand) Co., Ltd.     ','NPT Ayutthaya Factory 118 Moo 5, Phaholyothin Rd.','Lamsai','','SO_SIRANEE@notes.nidec.co.jp','Branch 0004','Wangnoi','','QA-Base / LAB','Ayutthaya     ','13170','+66 3521 5318 ',0,'2015-01-27',0,'2015-01-27','A'),(102,'CDGNIPPO01','Nippon Paint (Thailand) Co., Ltd.    ','700/29,31 Moo 6, 700/33 Moo 5','Klong Tam Rhu','','qc_waterbase@nipponpaint.co.th','Head Office','Muang','363','QC (WATER BASE)','Chonburi   ','20000','+66 3821 3701-5',0,'2015-01-27',0,'2015-01-27','A'),(103,'CDGNISSH01','Nissho Seiko (Thailand) Ltd.  ','7/225 Amata City Industrial Estate Moo 6','Mabyangporn','+66 86 001 7870','nstqa-mattika@nisshoseiko.com','Head Office','Pluakdaeng  ','145','QA','Rayong ','21140','+66 3865 0175-8',0,'2015-01-27',0,'2015-01-27','A'),(104,'CDGNITT01','Nitto Matex (Thailand) Co., Ltd   ','700/611 Moo 7 Bangna-Trad Rd. Km.57    ','Don Hau Roh','','jirachaya_sei-lim@gg.nitto.co.jp','Head Office','Muang ','431','Quality Assurance ','Chonburi   ','20000','+66 3804 7015   ',0,'2015-01-27',0,'2015-01-27','A'),(105,'CDGNITTO01','Nitto Denko Material (Thailand) Co., Ltd.  ','Rojana Industrial Park, 1/75 Moo 5, Rojana Rd.','Khan-Ham','','rungthiwa_buatoom@gg.nitto.co.jp','Head Office','U-Thai','420','Purchasing Section','Ayutthaya   ','13210','+66 3522 6750 ',0,'2015-01-27',0,'2015-01-27','A'),(106,'CDGNMB0001','Nmb-Minebea Thai Limited    ','1/14 MOO 5, ROJANA ROAD','Khan-Ham','','naruemon.p@minebea.co.th','Branch 0003','U-Thai','2449','Spindle Motor','Ayutthaya   ','13210','+66 3533 0506-9',0,'2015-01-27',0,'2015-01-27','A'),(107,'CDGNMB0002 ','Nmb-Minebea Thai Ltd.    ','1 Moo 7, Phaholyothin Road, Km. 51  ','Chiangraknoi','+66 87 916 5343','somkid.p@minebea.co.th','Head Office','Bang Pa-In','','Purchasing ( Spindle Motor Div )','Ayutthaya   ','13180','+66 3523 7268-75',0,'2015-01-27',0,'2015-01-27','A'),(108,'CDGNOK0001','Nok Precision Component (Thailand) Ltd. ','189, 198, 296 Moo 16 Bangpa-in Industrial Estate, Udomsorayath Rd.',' Bangkrasan','','alisad@nokpct.com','Head Office','Bang Pa-In','','QA','Ayutthaya   ','13160','+66 3525 8666 ',0,'2015-01-27',0,'2015-01-27','A'),(109,'CDGNTN0001','Ntn Manufacturing (Thailand) Co., Ltd.  ','64/89 Moo 4,    ','Pluakdaeng','','nipaporn@nmt.th.com','Branch 0002','Pluakdaeng','207','QA NMT','Rayong ','21140','+66 3895 5935-8',0,'2015-01-27',0,'2015-01-27','A'),(110,'CDGOKIPR01  ','Oki Proserve (Thailand) Co., Ltd. ','1168/32 Lumpini Tower, 16th Fl., Rama IV Rd., ','Thungmahamek','+66 89 922 8459','Charnchai@blt.co.th','Head Office','Sathorn','402','','Bangkok','10120','+66 2285 5998',0,'2015-01-27',0,'2015-01-27','A'),(111,'CDGONOFL01','O.N.O. Flow Co., Ltd.    ','48/53 Floor 4 Moo 7 Boonkum Rd.,','Kukot','','janeth@connols.co.th','Head Office','Lamlukka','','Purchase ','Pathumthani  ','12130','+66 2900 6900-5',0,'2015-01-27',0,'2015-01-27','A'),(112,'CDGP&NIN01','P&N Intelligent Provision Co.,Ltd.','316/47 , Ladprow87 Road','Klongchaokhunsing','+66 81 172 3247','chaipiwong@yahoo.com','Head Office','Wangthonglang','','Managing Director','Bangkok ','10310','+66 2932 1524   ',0,'2015-01-27',0,'2015-01-27','A'),(113,'CDGPACIF01','Pacific Cleanroom Services Limited','269/2 P.K.Building 2nd Floor,  S.Chockchaijongchamroen, Rama3 Rd.','Bangpongpang','','logistics@pcst.th.com','Head Office','Yannawa','','Purchase ','Bangkok ','10120','+66 2284 2616-8',0,'2015-01-27',0,'2015-01-27','A'),(114,'CDGPHOLD01','Pholdhanya Public Company Limited','1/11 MOO3 , Lamlukka Rd.,','Ladsawai','+66 91 557 8690','ammarin@pdgth.com','Head Office','Lamlukka','','','Pathumthani  ','12150','+66 2791 0230',0,'2015-01-27',0,'2015-01-27','A'),(115,'CDGPMCLA01','Pmc Label Materials Co., Ltd. ','30/28 Moo 2','Khokkham','','numtip@pmclabel.com','Head Office','Muang','223','QA/QC','Samutsakorn','74000','+66 3445 2000    ',0,'2015-01-27',0,'2015-01-27','A'),(116,'CDGPOOMJ02','Poomjai Engineering Co., Ltd.','136 Soi Lasalle 42','Bangna','+66 81 811 6057','Somsak_j@poomjai.co.th','Head Office','Bangna','','Quality Manager','Bangkok ','10260','+66 2752 5570-4',0,'2015-01-27',0,'2015-01-27','A'),(117,'CDGPOSCO01','Posco-Thainox Public Company Limited','324 Moo 8, Highway No 3191 Road','Mabkha','','hattaya@poscothainox.com','Branch 0002','Nikompattana','211','Products Technical Service Team','Rayong','21180','+66 3863 6125-32',0,'2015-01-27',0,'2015-01-27','A'),(118,'CDGPRIMA01','Prima Kleen Ltd. ','733/401-3 Moo 8 Phaholyothin Rd.','Kookod','','primakl@loxinfo.co.th','Head Office','Lamlukka','','','Pathumthani  ','12130','+66 2998 9308-11',0,'2015-01-27',0,'2015-01-27','A'),(119,'CDGPSC0001 ','Psc Technology (Ayutthaya) Co., Ltd.','593 Moo 2 Bang Pa-in Industrial Estate','Klong-Jig','','purchase.psca@psc-tech.com','Head Office','Bang Pa-In','201','Purchase ','Ayutthaya   ','13160','+66 3525 8152-5',0,'2015-01-27',0,'2015-01-27','A'),(120,'CDGRCI0001','Rci Labscan Limited ','24 Rama 1 Rd.','Rong Muang','','archara.c@rcilabscan.com','Head Office','Pathumwan','','','Bangkok','10330','+66 2613 7911-4',0,'2015-01-27',0,'2015-01-27','A'),(121,'CDGROJAN02','Rojanapat Engineering Co., Ltd.       ','60/9 Moo 4, Soi Klong 4 Tawan-OK 17,  Leab Klong 4 Road','Klong Si','','enquiry@rojanapatfilter.com','Head Office','Klong Luang ','201',' Admin','Pathumthani ','12120','+66 2908 1928 ',0,'2015-01-27',0,'2015-01-27','A'),(122,'CDGROYCE01','Royce Universal Co., Ltd.   ','86 Moo 9 Lang Wat Tha-Pood Rd.,','Raiking','','daw@royceuniversal.net','Head Office','Sampran','','QA','Nakronpathom ','73210','+66 2810 2533-5 ',0,'2015-01-27',0,'2015-01-27','A'),(123,'CDGRTSUP01','R T Supply Co.,Ltd.','116/5  Prayasurain Rd. ','Samwatawantok','+66 95 535 1448','purchase2@rt-supply.com','Head Office','Klongsamwa   ','','Purchasing','Bangkok','10510','+66 2914 3041',0,'2015-01-27',0,'2015-01-27','A'),(124,'CDGSAKUR01','Sakura Tech (Thailand) Ltd.','64/146 MOO 4. ESTERN SEABOARD INDUSTRIAL ESTATE ','Pluakdaeng ','+66 86 361  0647','kamolsaya16@gmail.com','Head Office','Pluakdaeng ','120','Project & Procurement Engineer ','Rayong','21140','+66 3895 9016-8   ',0,'2015-01-27',0,'2015-01-27','A'),(125,'CDGSAMSU01','Samsung Electro-Mechanics Nakhonratchasima Co','Suranaree Industrial Zone 555 Moo 6 ','Nong Rawieng','','jutima.p@samsung.com','Head Office','Muang','2170','Chemical test part','Nakhonratchasima','30000','+66 4421 2905-12',0,'2015-01-27',0,'2015-01-27','A'),(126,'CDGSCHAF01','Schaffner Emc Co. Ltd.','67 Moo 4','Ban Klang','','sakpaiboon.tajumpa@schaffner.com','Head Office','Muang ','','QA Engineer','Lumphun  ','51000','+66 5358 1104',0,'2015-01-27',0,'2015-01-27','A'),(127,'CDGSEAGA01','Seagate Technology (Thailand) Ltd.   ','1627 Moo 7, Teparuk Road','Teparuk','','suparat.yangsanthia@seagate.com ','Head Office','Muang ','2408','Indirect Material Purchasing','Samutprakarn  ','10270','+66 2715 2273 ',0,'2015-01-27',0,'2015-01-27','A'),(128,'','Seagate Technology (Thailand) Ltd.   ','90 Moo 15 ','Sungnoen','','kitti.kaewrattanapattama@seagate.com','Branch 0007','Sungnoen ','','Facility Engineer','Nakhonratchasima','30170','+66 4470 4315',0,'2015-01-27',0,'2015-01-27','A'),(129,'CDGSEIIN01','Sei Interconnect Products (Thailand) Ltd.','700/128 Moo 5 Bangna-Trad Rd.','Klong Tam Roo','+66 86 677 7697','hunsa-opanuruk@sept.sei.co.jp','Head Office','Muang','1143','QA/Lab','Chonburi   ','20000','+66 3846 5804-10',0,'2015-01-27',0,'2015-01-27','A'),(130,'CDGSEIKO01 ','Seiko Instruments (Thailand) Ltd.','60/83 Moo 19, Nava-nakorn Industrial Estate Zone 3 ','Klong Nueng','','chiraporn.s@sit.co.th','Head Office','Klong Luang ','','Ass t Dept Manager Chemical ','Pathumthani ','12120','+66 2529 2420-5',0,'2015-01-27',0,'2015-01-27','A'),(131,'CDGSEIKO02','Seikou Communication Co., Ltd','29/4 Moo 2, Putthabucha 36','Bangmod','+66 81 648 5545 ','chanukid@seikou.co.th','Head Office','Thongkru ','','','Bangkok','10140','+66 2426 3402',0,'2015-01-27',0,'2015-01-27','A'),(132,'CDGSEKSU01','Seksun Technology (Thailand) Co., Ltd.    ','99 Moo 9,','Thanu','+66 89 734 3620','pinanong@seksun.co.th','Head Office','U-Thai','244','QA/Lab','Ayutthaya   ','13210','+66 3580 0100',0,'2015-01-27',0,'2015-01-27','A'),(133,'CDGSHINE01','Shin-Etsu Magnetics (Thailand) Ltd. ','56/26 Moo 20','Klong Nueng','+66 86 977 7416','Kantinunt.s@set.co.th','Head Office','Klong Luang ','','QA Department','Pathumthani ','12120','+66 2520 4293-8',0,'2015-01-27',0,'2015-01-27','A'),(134,'CDGSHINE02 ','Shin-Etsu Magnetics (Thailand) Ltd. ','60/120, 122,123 Moo 19','Klong Nueng','+66 86 977 7416','Kantinunt.s@set.co.th','Head Office','Klong Luang ','','QA Department','Pathumthani ','12120','+66 2529 6230-1',0,'2015-01-27',0,'2015-01-27','A'),(135,'CDGSHINS01','Shinsei (Thailand) Co., Ltd.   ','40/18-19 Moo 5, Rojana Industrial park','U-Thai','','rattana@shinsei.co.th','Head Office','U-Thai','','QA','Ayutthaya   ','13210','+66 3574 1741',0,'2015-01-27',0,'2015-01-27','A'),(136,'CDGSIMAT01','Simat Label Company Limited  ','123, Soi Chalongkrung 31, Ladkrabang Industrial Estate, Chalongkrung Road','Lamplatew','+66 81 913 2684','somchoke.s@simat.co.th','Head Office','Ladkrabang','','','Bangkok','10520','+66 2326 0999',0,'2015-01-27',0,'2015-01-27','A'),(137,'CDGSKILL01','Skill Development Co.,Ltd   ','109 Moo 7, Kingkaew-Bangplee Rd., ','Bangplee-Yai','+66 90 198 7801 ','sirichai.eng@skill1999.com','Head Office','Bangplee','18','Mechanical Eng. Manager','Samutprakarn','10540','+66 2751 1256 ',0,'2015-01-27',0,'2015-01-27','A'),(138,'CDGSOLAR01','Solarlens Co., Ltd. ',' 2999Bang pa-in Industrial Estate,Export Processing Zone 4/2, Udomsorayuth Rd','Klong-Gik','+66 81 916 5824','wothiwunnarak@intercast-group.com','Head Office','Bang Pa-In','','Engineering','Ayutthaya   ','13160','+66 3526 8219-26  ',0,'2015-01-27',0,'2015-01-27','A'),(139,'CDGSOODE01','Soode Nagano (Thailand) Co., Ltd. ','130/129 Moo 3 Factoryland wangnoi','Wangchula','+66 86 753 2179','sayfon.w@soode.co.th','Head Office','Wangnoi','105','Quality & Environment Management System','Ayutthaya   ','13170','+66 3872 1711-5 ',0,'2015-01-27',0,'2015-01-27','A'),(140,'CDGSPC0001 ','Siam Precision Components Ltd.  ','19/29 Moo 10 Paholyothin Rd.','Klong Nueng','+66 81 812 3967','pairogew@siamprecision.com','Head Office','Klong Luang ','','Quality  Assurance','Pathumthani ','12120','+66 2529 6242',0,'2015-01-27',0,'2015-01-27','A'),(141,'CDGSPECI01','Specialty Tech Corporation Limited ',' 1/8 Moo 1','Klong Nueng','','nuntaporn@specialty.co.th','Head Office','Klong Luang ','','Purchasing','Pathumthani ','12120','+66 2833 3999',0,'2015-01-27',0,'2015-01-27','A'),(142,'CDGSUMIT01','Sumitomo Electric (Thailand) Ltd  ','54 B.B. Building, 15th Floor, Sukhumvit 21 Road','North Klongtoey','','angsana-phummee@gr.sei.co.jp','Head Office','Wattana','','Sales Department ','Bangkok ','10110','+66 2260 7231-5 ',0,'2015-01-27',0,'2015-01-27','A'),(143,'CDGSUNLI01','Sunlit (Thailand) Co., Ltd. ','Eastern Seaboard Industrial Estate , 64/155 Moo 4 ','Pluakdaeng ','+66 82 120 5685     ','pro_2@sunlit.co.th','Head Office','Pluakdaeng ','','Maintenance Engineer','Rayong','21140','+66 3895 9383',0,'2015-01-27',0,'2015-01-27','A'),(144,'CDGTAIKI01','Taikisha (Thailand) Co.,Ltd.','62 SILOM ROAD','Suriyawong','+66 92 617 7789','tkc_nantagan@hotmail.com','Head Office','Bangrak','','Senior Engineer','Bangkok','10500','+66 2236 8055-9',0,'2015-01-27',0,'2015-01-27','A'),(145,'CDGTAIYO02','Taiyo Technology Industry (Thailand) Co., Ltd','55/1,3,5,9,11  Moo.15','Bangsaotong','+66 85 482 9426','Wanyupa@taiyotech.th.com','Head Office','Bangsaotong','','','Samutprakarn','10540','+66 2182 5228-9',0,'2015-01-27',0,'2015-01-27','A'),(146,'CDGTANAB01','Tanabe (Thailand) Co., Ltd ','304 Industrial Park, 229 Moo. 7','Thatoom','+66 89 244 7006','niramol@tanabe.co.th','Head Office','Srimahaphot','29','QA','Prachinburi ','25140','+66 3720 8591-3',0,'2015-01-27',0,'2015-01-27','A'),(147,'CDGTDI0001','Thai Dai-Ichi Seiko Co., Ltd. ','700/390 Moo 6','Don Hau Roh','+66 81 999 5913','Qa3@thai-daiichi.co.th','Head Office','Muangchonburi','706','QA-TDI','Chonburi ','20000','+66 3846 8316',0,'2015-01-27',0,'2015-01-27','A'),(148,'CDGTDK0001','Tdk (Thailand) Co., Ltd. ','1/62 Moo 5, Rojana Industrial Park, Rojana Rd.','Khan-Ham','','RungrapeeB@th.tdk.com','Head Office','U-Thai','3323','QA MSL Analysis Lab','Ayutthaya   ','13210','+66 3533 0614-24  ',0,'2015-01-27',0,'2015-01-27','A'),(149,'CDGTDK0002','Tdk (Thailand) Co., Ltd.','149 Moo 5, Phaholyothin Rd.','Lamsai','+66 83 123 4684','Rattiyat@th.tdk.com','Head Office','Wangnoi ','5331','Sr.QA-Analysis engineer','Ayutthaya','13170','+66 3521 5299',0,'2015-01-27',0,'2015-01-27','A'),(150,'CDGTECH02','Techno Gateway (Asia) Co., Ltd.','99/9 Central Tower office Building Chaengwattana, Fl.12 Room 1202 M.2 Chaeng Wattana Rd','Bangtala','+66 80 603 2417','noboruhayakawa@techno-gateway.asia','Head Office','Pakkret','','','Nonthaburi ','11120','',0,'2015-01-27',0,'2015-01-27','A'),(151,'CDGTECHN01','Techno Packaging Industries Co.,Ltd ','69 Rojana Industrial moo9','Thanu','','sophie@technopackaging.asia','Head Office','U-Thai','','','Ayutthaya   ','13210','+66 3580 0233',0,'2015-01-27',0,'2015-01-27','A'),(152,'CDGTEXCH01','Texchem - Pack (Thailand) Co., Ltd. ','234 Moo 2, Bangpa-in Industrial Estate','Klong-Jig','+66 81 714 9685','qa_engineer@th.texchem-pack.com','Head Office','Bang Pa-In','4701','QA engineer','Ayutthaya   ','13160','+66 3525 8428',0,'2015-01-27',0,'2015-01-27','A'),(153,'CDGTHAIK01','Thai Kajima Co., Ltd. ','952 19th Fl.,Ramaland Bldg., Rama IV Rd.','Suriyawong','','thawatchai@kajima.co.th','Head Office','Bangrak','','','Bangkok','10500','+66 2632 9300',0,'2015-01-27',0,'2015-01-27','A'),(154,'CDGTHERM01','Thermal Pack Co., Ltd.   ',' 88/5  Moo 1, Bangbuathong-Pathumthani Rd.','Bangtanai','','kittisak@thermalpack.net','Head Office','Pakkret','','','Nonthaburi ','11120','+66 2598 6291-3',0,'2015-01-27',0,'2015-01-27','A'),(155,'CDGTHHOU01','Thai Houghton 1993 Co., Ltd.','77/105-106, 25th Flr., Sinn Sathorn Tower',' Krungthonburi','+66 89 403 9293 ','Udaporn.Phookduang@houghtonintl.com','Branch 0001','Klongsarn','','Senior Executive Administrator ','Bangkok ','10600','+66 2440 1262 ',0,'2015-01-27',0,'2015-01-27','A'),(156,'CDGTHJUR01','Thai Jurong Engineering Limited  ','75/43 Ocean Tower 2, 22 Floor, Sukumvit 19','North Klongtoey','','wendywee@tjel.co.th','Head Office','Wattana','','Commissioning','Bangkok  ','10110','+66 2260 5181-4',0,'2015-01-27',0,'2015-01-27','A'),(157,'CDGTHKOK01 ','Thai Kokoku Rubber Co., Ltd.','36 Moo 9 Rojana Industrial Park, Rojana Road','U-Thai','','achari@kokoku.co.th','Head Office','U-Thai','','Quality Control','Ayutthaya   ','13210','+66 3533 0781',0,'2015-01-27',0,'2015-01-27','A'),(158,'CDGTHMEK01 ','Thai Mekki Company Limited ','113 Soi Bangkradi 8, Bangkradi Rd.','Smaedam','+66 81 786 1639','patcharapong@thaimekki.com','Head Office','Bangkhuntien','','QA','Bangkok  ','10150','+66 2896 9008',0,'2015-01-27',0,'2015-01-27','A'),(159,'CDGTHMEK02 ','Thai Mekki (2012) Company Limited','113 Soi Bangkradi 8, Bangkradi Rd.','Smaedam','+66 81 786 1639','patcharapong@thaimekki.com','Head Office','Bangkhuntien','','QA','Bangkok  ','10150','+66 2452 1290 ',0,'2015-01-27',0,'2015-01-27','A'),(160,'CDGTHREE01','Three Bond Manufacturing (Thailand) Co., Ltd.','700/432 M. 7, Bangna-Trad Rd','Don Hau Roh','','gusmar@tbmt.co.th','Head Office','Muangchonburi','46','Development Department','Chonburi ','20000','+66 3845 4251-3',0,'2015-01-27',0,'2015-01-27','A'),(161,'CDGTHREE02','Three Bond Technology (Thailand) Co., Ltd.','153 Moo 1  ','Ban-Lane','+66 86 546 3205','wassana@tbtt.co.th','Head Office','Bang Pa-In','','QC Section','Ayutthaya   ','13160','+66 3572 9259-61',0,'2015-01-27',0,'2015-01-27','A'),(162,'CDGTHTAK01','Thai Takasago Co., Ltd.',' Bangna TowersC16th Fl., 40/14 M.12, Bangna-Trad Rd, K.M. 6.5','Bangkaew','+66 81 825 3667','theeraphan.j@thaitakasago.co.th','Head Office','Bangplee','',' Engineering','Samutprakarn ','10540','+66 2751 9695-99',0,'2015-01-27',0,'2015-01-27','A'),(163,'CDGTODAP01','Toda Pipe (Thailand) Co., Ltd',' 208 Moo 2 Phaholyothin Rd.','Chamab','','qa_dcc@toda.co.th','Head Office','Wangnoi ','','Document Control Center','Ayutthaya   ','13170','+66 3574 4200-4 ',0,'2015-01-27',0,'2015-01-27','A'),(164,'CDGTOKYO01','Tokyo Seimitsu(Thailand)Co.,Ltd.','93/11-13 Kensington Place 4th FL.,Chaeng Wattana13','Thung Song Hong','+66 81 910 2013 ','Yutthana@accretech.com.my','Branch 0001','Lak Si','','Sr. Sales Engineer','Bangkok  ','10210','+66 2617 9497-8 ',0,'2015-01-27',0,'2015-01-27','A'),(165,'CDGTOLI001','Toli Packaging (Thailand) Co., Ltd.','58-59 Moo 3 Phahinyothin Rd.','Lamsai','','piyapong@tolipackaging.com','Head Office','Wangnoi ','200','QA','Ayutthaya','13170','+66 3528 7597',0,'2015-01-27',0,'2015-01-27','A'),(166,'CDGTRUE001','True Internet Data Center Co., Ltd.  ','18 True Tower, 14th Floor  , Ratchadaphisek Road','Huai Khwang','+66 84 075 3668','Thanawat_Taw@truecorp.co.th','Head Office','Huai Khwang','','','Bangkok ','10310','+66 2643 1111  ',0,'2015-01-27',0,'2015-01-27','A'),(167,'CDGTSCAN01','Thai Scan Tube Co., Ltd.  ','143 Moo 1  ','Bo-Kwangthong','','somporn@scantube.com','Head Office','Bothong','','General Manager','Chonburi  ','20270','+66 3836 3221-2',0,'2015-01-27',0,'2015-01-27','A'),(168,'','Valtech Corporation   ','1/21 Soi Sarmmit,','Klongtoey','+66 86 754 5268','c.sakuna@valtechcorp.com','','Klongtoey','','Manager-South East Asia','Bangkok','10110','',0,'2015-01-27',0,'2015-01-27','A'),(169,'CDGWDB0001','Western Digital (Thailand) Co., Ltd. ',' 140 Moo 2','Klong-Jig','','Kingfa.kitchainugool@wdc.com','Head Office','Bang Pa-In','','TSE/AS Lab','Ayutthaya   ','13160','+66 3527 8764',0,'2015-01-27',0,'2015-01-27','A'),(170,'CDGWEMA001','Wema Environmental Technologies Limited','999/18 Moo 15, Bangna-Trad KM.23 Road','Bangsaotong','+66 81 863 3660','chsu@wema.com','Head Office','Bangsaotong','','','Samutprakarn','10540','+66 2182 5291',0,'2015-01-27',0,'2015-01-27','A'),(171,'CDGWINGF01','Wing Fung Adhesive Manufacturing (Thailand) C','10 Moo 2 Paholyotin Road, KM. 57','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya   ','13170','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27','A'),(172,'CDGWINGF02 ','Wing Fung Packaging Co.,Ltd    ','10 Moo 2 Paholyotin Road, KM. 57','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya   ','13170','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27','A'),(173,'CDGWINNE01','Winner Export (Thailand) Co., Ltd. ','16 Soi Panitchayakarn Thonbui 9, Yak 10 ','Wat Thaphra','+66 86 774 3233','chitrapat.winner@gmail.com','','Bangkok Yai','','Control Production','Bangkok ','10600','+66 2411 6097',0,'2015-01-27',0,'2015-01-27','A'),(174,'CDGWORLD01','World Hitech Marketing Co.,Ltd    ','85/34-36 Moo5 ','Wat-Tum','','wht_tape@hotmail.com','','Ayutthaya','','Admin','Ayutthaya','13000','+66 3571 3543',0,'2015-01-27',0,'2015-01-27','A'),(175,'CDGYUSHI01','Yushiro (Thailand) Co., Ltd.   ','700/533 Moo 7 Amata Nakorn Industrial Estate','Don Hau Roh','','takahashi@yushiro.co.th','','Muangchonburi','','Sales','Chonburi','20000','+66 3845 4873',0,'2015-01-27',0,'2015-01-27','A'),(176,'CDGZOSEN01','Zos Engineering Co., Ltd  ','53/501 Moo 9','Bangpood','+66 86 309 3330','kongsak.s@zos-engineering.com','','Pakkret','','Business Development Manager','Nonthaburi','11120','+66 2960 1038',0,'2015-01-27',0,'2015-01-27','A'),(177,'CDGZKURO01','Z.Kuroda (Thailand) Co., Ltd','1/24  Moo. 5 Rojana Industrial Park,','Khan-Ham','+66 96 416 2469','korawan@zkurodath.com','','U-Thai','159','Laboratory Engineer','Ayutthaya','13210','+66 3533 0066',0,'2015-01-27',0,'2015-01-27','A'),(178,'CDGไฟฟ้า01','Egat Mae Moh','800 Moo.6','Mae Moh ','+66 86 654 7601','sawek.c@egat.co.th','','Mae Moh ','','แผนกเคมี กองเชื้อเพลิงถ่านและน้ำ','Lumpang','52220','+66 5425 2331',0,'2015-01-27',0,'2015-01-27','A');
/*!40000 ALTER TABLE `m_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_customer_contract_person`
--

DROP TABLE IF EXISTS `m_customer_contract_person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_customer_contract_person` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `company_id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `phone_number` varchar(45) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`),
  KEY `fk_company_id_idx` (`company_id`),
  CONSTRAINT `fk_company_id` FOREIGN KEY (`company_id`) REFERENCES `m_customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=177 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_customer_contract_person`
--

LOCK TABLES `m_customer_contract_person` WRITE;
/*!40000 ALTER TABLE `m_customer_contract_person` DISABLE KEYS */;
INSERT INTO `m_customer_contract_person` VALUES (1,4,'Choltinee Tangtreerat ','+66 81 170 7109','A'),(2,3,'Anusorn Chakkaew','+66 85 441 9193','A'),(3,15,'Witoon Ponglaohapun','','A'),(4,16,'Atthakorn Termpittayapaisit','+66 81 321 4264','A'),(5,5,'Waraporn Pholyiem','','A'),(6,6,'Kanokwan','','A'),(7,7,'Pisit Rujipichapornkul','+66 83 135 8998','A'),(8,8,'Ittthikorn Jinuntuya','+66 86 688 8341','A'),(9,9,'Yutthana','+66 86 565 7231','A'),(10,10,'Nongluk','+66 86 105 8722','A'),(11,11,'Waleepun Kaewpatch','','A'),(12,12,'Jeeranan Pimta','','A'),(13,13,'Onnisara Archariya','','A'),(14,14,'Saengtean','','A'),(15,22,'Martin Covell','','A'),(16,17,'Puntharee Sirojtantikorn','','A'),(17,18,'Runglawan Seeburth','','A'),(18,19,'Walaipon','','A'),(19,20,'Patpong  Boonraksah','+66 83 899 6346','A'),(20,21,'Panachon','','A'),(21,25,'Sukanya Prasertsung ','+66 89 920 3494','A'),(22,23,'Sukanya Chantaworakit ','','A'),(23,24,'Charnchai Jamrasfuangfoo','','A'),(24,26,'Bantita Takkhapiwat (Kate)','+66 85 111 6710','A'),(25,27,'Chairat Luepongpattana','+66 86 873 8006','A'),(26,28,'Jiraporn Somnuam','','A'),(27,29,'Nataporn  Phunnarungsi','+66 81 840 5083','A'),(28,30,'Vanida Keswong','','A'),(29,31,'Kanuengnit','','A'),(30,32,'Sunanta Sukpool','+66 89 066 3125','A'),(31,33,'Patchara Toprated','+66 84 094 4633','A'),(32,34,'Rungnapa Pasuriwong','','A'),(33,40,'Thiraporn Yaiying (Pu)','','A'),(34,35,'Sakranta Tonsanguan','+66 94 651 4664','A'),(35,36,'Warut Sutjaritjun','+66 86 125 5152','A'),(36,37,'Natnapat Methanorapat ','','A'),(37,38,'Kampol','','A'),(38,39,'Pacharathon Chueateaw','','A'),(39,41,'Patipat','+66 81 988 3996','A'),(40,178,'Yeamyuth  Manitras','+66 81 945 5833','A'),(41,42,'Wannuttee Malithong ','+66 86 993 2026','A'),(42,43,'Yuthtaphoom Silachan','+66 81-639 0394','A'),(43,44,'Aree Krataitong','+66 89 814 2276','A'),(44,45,'Usani','+66 87 9711959','A'),(45,46,'Kristoffer Clyde G. Magsino (Clyde)','+66 89 767 0202','A'),(46,47,'Pornpit Phetrak','','A'),(47,49,'Netnapa Y. ( Yui)','+66 81 555 8659','A'),(48,51,'Namphung Feepakprao','+66 86 972 6138 ','A'),(49,48,'Daranee Phoyu','','A'),(50,50,'Surasak Kornnitikul','+66 86 030 0398','A'),(51,52,'Uthen Wiriyakasikon','','A'),(52,54,'Patthamon   Kittisophon ','','A'),(53,53,'Supansa  Noinamkam','','A'),(54,55,'Amornchai','+66 86 345 6553','A'),(55,56,'Sudarat (Art)','+66 89 900 9413','A'),(56,57,'Nitjawan Inthajakr ','+66 81 931 2166 ','A'),(57,58,'Doungdao Phanthong','','A'),(58,59,'Sakchai','+66 81 840 5389','A'),(59,60,'Tula Chankana','+66 81 939 2808','A'),(60,61,'Patinya  Pansuk ','','A'),(61,62,'Sukanya Yujan (Su)','+66 87 201 9195 ','A'),(62,63,'Sittiporn','','A'),(63,64,'Warunya','+66 90 961 3636','A'),(64,65,'Aniruth Uthaisawek','','A'),(65,66,'Suriam','+66 86 523 6555','A'),(66,67,'Supaktra Sa-Nguanrat','+66 81 754 9808','A'),(67,68,'Mana','','A'),(68,70,'Mana','','A'),(69,69,'Sarisanach Iamsripeng','','A'),(70,71,'Panachon','','A'),(71,72,'Sompong Suphap','','A'),(72,74,'Saleela','+66 86 320 6456','A'),(73,73,'Wuttichai  Chamnan','+66 85 071 1626','A'),(74,75,'Bancha','','A'),(75,76,'Sutthipong Itthipalangkul ','+66 85 109 5682 ','A'),(76,77,'Dusit Suratham','','A'),(77,79,'Dusit Suratham','','A'),(78,78,'Jariya  Makan  (Sai)','+66 87 859 0620','A'),(79,80,'Kittichai Pinwiset','','A'),(80,81,'Teerawat','','A'),(81,82,'Charida','','A'),(82,83,'Pallapa Rattanawaraporn ','','A'),(83,84,'Daungrutai Boonyen','','A'),(84,85,'Suphoj Cholkujanan','+66 81 854 5843','A'),(85,86,'Wivornchai  Boonyon','','A'),(86,87,'Sutheera','','A'),(87,88,'Daungnapa','','A'),(88,89,'Noppawan Jomkamsing','+66 89 666 3097','A'),(89,90,'Nuantip Tanya','+66 81 694 3804','A'),(90,91,'Nithima Pimpijit','+66 87 517 8490','A'),(91,92,'Darunee Aussawasathien','','A'),(92,94,'Pornpimol','','A'),(93,93,'Sasipapha  Ruang U-Rai (Rin)','+66 89 923 3156','A'),(94,95,'Piyachat','','A'),(95,96,'Put Sae-Lew','+66 90 429 5861','A'),(96,97,'Jutharat   Phonkaew','','A'),(97,99,'Kittisak Pandaranandaka','','A'),(98,100,'Suwimon','','A'),(99,98,'Siranee Songsawas','','A'),(100,101,'Pratchapol Marutapun','','A'),(101,102,'Mattika Sri-On','+66 86 001 7870','A'),(102,103,'Jiratchaya Sae-Lim','','A'),(103,105,'Rungthiwa  Buatoom (Daeng)','','A'),(104,104,'Naruemon Pinthong','','A'),(105,106,'Somkid Pichi  ( Aoy ) ','+66 87 916 5343','A'),(106,107,'Alisa  Dararung','','A'),(107,108,'Nipaporn Muangnak ','','A'),(108,109,'Charnchai Jamrasfuangfoo','+66 89 922 8459','A'),(109,111,'Janeth A. Mallorca','','A'),(110,110,'Bongkot Chaipiwong (Pom)','+66 81 172 3247','A'),(111,112,'Aktana','','A'),(112,113,'Ammarin','+66 91 557 8690','A'),(113,114,'Numtip Sangchai','','A'),(114,115,'Somsak','+66 81 811 6057','A'),(115,116,'Hattaya Aranyanarth','','A'),(116,117,'Kittipong','','A'),(117,118,'Wilaiporn','','A'),(118,119,'Achara','','A'),(119,123,'Praphaporn  K. (อ้วน ) ','','A'),(120,120,'Daw Petchsri','','A'),(121,121,'Wijittra Mokchai(Angie)','+66 95 535 1448','A'),(122,122,'Kanok','+66 86 361  0647','A'),(123,124,'Jutima Phonchalard','','A'),(124,125,'Sakpaiboon Tajumpa','','A'),(125,126,'Suparat Yangsanthia','','A'),(126,127,'Kitti Kaewrattanapattama','','A'),(127,128,'Hunsa Opanuruk','+66 86 677 7697','A'),(128,129,'Chiraporn','','A'),(129,130,'Chanukid Siripakornchai','+66 81 648 5545 ','A'),(130,131,'Pinanong','+66 89 734 3620','A'),(131,132,'Kantinunt Supsermpol','+66 86 977 7416','A'),(132,133,'Kantinunt Supsermpol','+66 86 977 7416','A'),(133,134,'Rattana  Bandasak','','A'),(134,135,'Somchok','+66 81 913 2684','A'),(135,140,'Sirichai Ratthanaphuripat','+66 90 198 7801 ','A'),(136,136,'Worawun Thiwunnarak','+66 81 916 5824','A'),(137,137,'Sayfon  Wangkeeree ','+66 86 753 2179','A'),(138,138,'Pairoge Wongsakulchuen','+66 81 812 3967','A'),(139,139,'Nuntaporn Krasaesut','','A'),(140,141,'Angsana Phummee','','A'),(141,142,'Yhutapol  Tommahon','+66 82 120 5685     ','A'),(142,143,'Nantakan Klongduangjit','+66 92 617 7789','A'),(143,144,'Wanyupa  Waikulpech','+66 85 482 9426','A'),(144,145,'Niramol','+66 89 244 7006','A'),(145,146,'Tassanee','+66 81 999 5913','A'),(146,149,'Rungrapee Boonmee','','A'),(147,148,'Rattiya Tangyoo ','+66 83 123 4684','A'),(148,150,'Noboru Hayakawa ','+66 80 603 2417','A'),(149,151,'Sophie Zhou','','A'),(150,152,'Aree  Boonpa','+66 81 714 9685','A'),(151,147,'Thawatchai','','A'),(152,155,'Kittisak','','A'),(153,156,'Udaporn  Phookduang (Gap)','+66 89 403 9293 ','A'),(154,153,'Wendy Wee','','A'),(155,157,'Achari Saardiem','','A'),(156,159,'Patcharapong','+66 81 786 1639','A'),(157,158,'Patcharapong','+66 81 786 1639','A'),(158,167,'Gusmar Thungsupanich','','A'),(159,162,'Wassana','+66 86 546 3205','A'),(160,154,'Teeraphan','+66 81 825 3667','A'),(161,160,'Nattamon','','A'),(162,161,'Yutthana','+66 81 910 2013 ','A'),(163,163,'Piyapong Inthaphophan','','A'),(164,164,'Thanawat Taweeprecharat ','+66 84 075 3668','A'),(165,165,'Somporn','','A'),(166,166,'Sakuna Chongkittisakul','+66 86 754 5268','A'),(167,168,'Kingfa Kitchainugool','','A'),(168,170,'Chonchalerm Suwannapho ','+66 81 863 3660','A'),(169,169,'Usani','+66 87 9711959','A'),(170,171,'Usani','+66 87 9711959','A'),(171,172,'Chitlrapat','+66 86 774 3233','A'),(172,173,'Thitima','','A'),(173,174,'Shousuke Takahashi','','A'),(174,175,'Kongsak Sanguanpong','+66 86 309 3330','A'),(175,177,'Korawan  Sowichai','+66 96 416 2469','A'),(176,176,'Sawek Chaisalee','+66 86 654 7601','A');
/*!40000 ALTER TABLE `m_customer_contract_person` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_role`
--

DROP TABLE IF EXISTS `m_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_role` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_role`
--

LOCK TABLES `m_role` WRITE;
/*!40000 ALTER TABLE `m_role` DISABLE KEYS */;
INSERT INTO `m_role` VALUES (1,'Root','H'),(2,'Login','A'),(3,'Chemist','A'),(4,'SrChemist','A'),(5,'Admin','A'),(6,'LabManager','A');
/*!40000 ALTER TABLE `m_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_role_permission`
--

DROP TABLE IF EXISTS `m_role_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_role_permission` (
  `ID` int(11) NOT NULL,
  `role_id` int(11) DEFAULT NULL,
  `code` varchar(45) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`),
  KEY `fk_role_ie_idx` (`role_id`),
  CONSTRAINT `fk_role_ie` FOREIGN KEY (`role_id`) REFERENCES `m_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_role_permission`
--

LOCK TABLES `m_role_permission` WRITE;
/*!40000 ALTER TABLE `m_role_permission` DISABLE KEYS */;
/*!40000 ALTER TABLE `m_role_permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_specification`
--

DROP TABLE IF EXISTS `m_specification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_specification` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_specification`
--

LOCK TABLES `m_specification` WRITE;
/*!40000 ALTER TABLE `m_specification` DISABLE KEYS */;
INSERT INTO `m_specification` VALUES (1,'Segate','A'),(2,'WD','A'),(3,'**ISO/IEC 17025 (IDEMA METHOD)','A'),(4,'Others','A');
/*!40000 ALTER TABLE `m_specification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_status`
--

DROP TABLE IF EXISTS `m_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_status` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `status_group_id` int(11) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_status_group_id_idx` (`status_group_id`),
  CONSTRAINT `fk_status_group_id` FOREIGN KEY (`status_group_id`) REFERENCES `m_status_group` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_status`
--

LOCK TABLES `m_status` WRITE;
/*!40000 ALTER TABLE `m_status` DISABLE KEYS */;
INSERT INTO `m_status` VALUES (1,1,'JOB_CANCEL'),(2,1,'JOB_HOLD'),(3,1,'JOB_COMPLETE'),(4,1,'SR_CHEMIST_CHECKING'),(5,1,'SR_CHEMIST_APPROVE'),(6,1,'SR_CHEMIST_DISAPPROVE'),(7,1,'LABMANAGER_APPROVE'),(8,1,'LABMANAGER_DISAPPROVE'),(9,1,'LABMANAGER_CHECKING'),(10,1,'LOGIN_CONVERT_TEMPLATE'),(11,1,'LOGIN_SELECT_SPEC'),(12,1,'CHEMIST_TESTING'),(13,1,'ADMIN_CONVERT_WORD'),(14,1,'ADMIN_CONVERT_PDF');
/*!40000 ALTER TABLE `m_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_status_group`
--

DROP TABLE IF EXISTS `m_status_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_status_group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `description` varchar(100) DEFAULT NULL,
  `status` varchar(45) DEFAULT 'A',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_status_group`
--

LOCK TABLES `m_status_group` WRITE;
/*!40000 ALTER TABLE `m_status_group` DISABLE KEYS */;
INSERT INTO `m_status_group` VALUES (1,'JOB_FLOW','Job Flow Status','A');
/*!40000 ALTER TABLE `m_status_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_template`
--

DROP TABLE IF EXISTS `m_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_template` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `path_source_file` varchar(100) DEFAULT NULL,
  `path_url` varchar(100) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_template`
--

LOCK TABLES `m_template` WRITE;
/*!40000 ALTER TABLE `m_template` DISABLE KEYS */;
INSERT INTO `m_template` VALUES (1,'Canon-DHS.xlt','/uploads/template/Canon-DHS.xlt','~/view/template/Canon_DHS_xlt.aspx','A'),(2,'DI Water Template (Asia Micro,Thailand Sample, MClean)_Ver 2.XLT','/uploads/template/DI Water Template (Asia Micro,Thailand Sample, MClean)_Ver 2.XLT','~/view/template/DI_Water_Template_Asia_Micro_Thailand_Sample_MClean_Ver_2_XLT.aspx','A'),(3,'Donaldson-DHS.xlt','/uploads/template/Donaldson-DHS.xlt','~/view/template/Donaldson_DHS_xlt.aspx','A'),(4,'Donaldson-FTIR & NVR.xlt','/uploads/template/Donaldson-FTIR & NVR.xlt','~/view/template/Donaldson_FTIR_&_NVR_xlt.aspx','A'),(5,'Donalson-LPC.xlt','/uploads/template/Donalson-LPC.xlt','~/view/template/Donalson_LPC_xlt.aspx','A'),(6,'ELWA (IC Water).xlt','/uploads/template/ELWA (IC Water).xlt','~/view/template/ELWA_IC_Water_xlt.aspx','A'),(7,'NTN-IC, NVR & FTIR (Bearphite).xlt','/uploads/template/NTN-IC, NVR & FTIR (Bearphite).xlt','~/view/template/NTN_IC_NVR_&_FTIR_Bearphite_xlt.aspx','A'),(8,'NTN-IC, NVR & FTIR (Housing).xlt','/uploads/template/NTN-IC, NVR & FTIR (Housing).xlt','~/view/template/NTN_IC_NVR_&_FTIR_Housing_xlt.aspx','A'),(9,'Samsung-Pivot.xlt','/uploads/template/Samsung-Pivot.xlt','~/view/template/Samsung_Pivot_xlt.aspx','A'),(10,'Seagate-Corrosion Copper Wire.xlt','/uploads/template/Seagate-Corrosion Copper Wire.xlt','~/view/template/Seagate_Corrosion_Copper_Wire_xlt.aspx','A'),(11,'Seagate-Corrosion.xlt','/uploads/template/Seagate-Corrosion.xlt','~/view/template/Seagate_Corrosion_xlt.aspx','A'),(12,'Seagate-DHS (Cleanroom mat Construction).xlt','/uploads/template/Seagate-DHS (Cleanroom mat Construction).xlt','~/view/template/Seagate_DHS_Cleanroom_mat_Construction_xlt.aspx','A'),(13,'Seagate-DHS Adhesive.xlt','/uploads/template/Seagate-DHS Adhesive.xlt','~/view/template/Seagate_DHS_Adhesive_xlt.aspx','A'),(14,'Seagate-DHS Bag.xlt','/uploads/template/Seagate-DHS Bag.xlt','~/view/template/Seagate_DHS_Bag_xlt.aspx','A'),(15,'Seagate-DHS Component 1.1.xlt','/uploads/template/Seagate-DHS Component 1.1.xlt','~/view/template/Seagate_DHS_Component_1_1_xlt.aspx','A'),(16,'Seagate-DHS Component Rev. BE 1.5.xlt','/uploads/template/Seagate-DHS Component Rev. BE 1.5.xlt','~/view/template/Seagate_DHS_Component_Rev__BE_1_5_xlt.aspx','A'),(17,'Seagate-DHS Component.xlt','/uploads/template/Seagate-DHS Component.xlt','~/view/template/Seagate_DHS_Component_xlt.aspx','A'),(18,'Seagate-DHS Indirect Material.xlt','/uploads/template/Seagate-DHS Indirect Material.xlt','~/view/template/Seagate_DHS_Indirect_Material_xlt.aspx','A'),(19,'Seagate-DHS Tape Label.xlt','/uploads/template/Seagate-DHS Tape Label.xlt','~/view/template/Seagate_DHS_Tape_Label_xlt.aspx','A'),(20,'Seagate-FTIR Component Rev.BE 1.5.xlt','/uploads/template/Seagate-FTIR Component Rev.BE 1.5.xlt','~/view/template/Seagate_FTIR_Component_Rev_BE_1_5_xlt.aspx','A'),(21,'Seagate-FTIR Component.xlt','/uploads/template/Seagate-FTIR Component.xlt','~/view/template/Seagate_FTIR_Component_xlt.aspx','A'),(22,'Seagate-FTIR Consumable 1.2.xlt','/uploads/template/Seagate-FTIR Consumable 1.2.xlt','~/view/template/Seagate_FTIR_Consumable_1_2_xlt.aspx','A'),(23,'Seagate-FTIR Consumable.xlt','/uploads/template/Seagate-FTIR Consumable.xlt','~/view/template/Seagate_FTIR_Consumable_xlt.aspx','A'),(24,'Seagate-GCMS Component 1.9.xlt','/uploads/template/Seagate-GCMS Component 1.9.xlt','~/view/template/Seagate_GCMS_Component_1_9_xlt.aspx','A'),(25,'Seagate-GCMS Component.xlt','/uploads/template/Seagate-GCMS Component.xlt','~/view/template/Seagate_GCMS_Component_xlt.aspx','A'),(26,'Seagate-HPA Filtration Rev.BE 1.4.xlt','/uploads/template/Seagate-HPA Filtration Rev.BE 1.4.xlt','~/view/template/Seagate_HPA_Filtration_Rev_BE_1_4_xlt.aspx','A'),(27,'Seagate-HPA Filtration3.xlt','/uploads/template/Seagate-HPA Filtration3.xlt','~/view/template/Seagate_HPA_Filtration3_xlt.aspx','A'),(28,'Seagate-HPA Swab.xlt','/uploads/template/Seagate-HPA Swab.xlt','~/view/template/Seagate_HPA_Swab_xlt.aspx','A'),(29,'Seagate-IC Component 1.4.xlt','/uploads/template/Seagate-IC Component 1.4.xlt','~/view/template/Seagate_IC_Component_1_4.ascx','A'),(30,'Seagate-IC Component HGA.xlt','/uploads/template/Seagate-IC Component HGA.xlt','~/view/template/Seagate_IC_Component_HGA_xlt.aspx','A'),(31,'Seagate-IC Component PCBA.xlt','/uploads/template/Seagate-IC Component PCBA.xlt','~/view/template/Seagate_IC_Component_PCBA_xlt.aspx','A'),(32,'Seagate-IC Component.xlt','/uploads/template/Seagate-IC Component.xlt','~/view/template/Seagate_IC_Component_xlt.aspx','A'),(33,'Seagate-IC Consumable 1.2.xlt','/uploads/template/Seagate-IC Consumable 1.2.xlt','~/view/template/Seagate_IC_Consumable_1_2_xlt.aspx','A'),(34,'Seagate-LPC Component 1.4 Rev.BE.xlt','/uploads/template/Seagate-LPC Component 1.4 Rev.BE.xlt','~/view/template/Seagate_LPC_Component_1_4_Rev_BE_xlt.aspx','A'),(35,'Seagate-LPC Component.xlt','/uploads/template/Seagate-LPC Component.xlt','~/view/template/Seagate_LPC_Component_xlt.aspx','A'),(36,'Seagate-LPC Consumable.xlt','/uploads/template/Seagate-LPC Consumable.xlt','~/view/template/Seagate_LPC_Consumable_xlt.aspx','A'),(37,'Segate HPA(MgSiO)-Wiper&Paper.xlt','/uploads/template/Segate HPA(MgSiO)-Wiper&Paper.xlt','~/view/template/Segate_HPA_MgSiO_Wiper&Paper_xlt.aspx','A'),(38,'WD-Corrosion 1.0.xlt','/uploads/template/WD-Corrosion 1.0.xlt','~/view/template/WD_Corrosion_1_0_xlt.aspx','A'),(39,'WD-CVR 1.0.xlt','/uploads/template/WD-CVR 1.0.xlt','~/view/template/WD_CVR_1_0_xlt.aspx','A'),(40,'WD-DHS (Label, Sticker, Tape) 1.0.xlt','/uploads/template/WD-DHS (Label, Sticker, Tape) 1.0.xlt','~/view/template/WD_DHS_Label_Sticker_Tape_1_0_xlt.aspx','A'),(41,'WD-DHS Component 1.0.xlt','/uploads/template/WD-DHS Component 1.0.xlt','~/view/template/WD_DHS_Component_1_0_xlt.aspx','A'),(42,'WD-DHS Component 1.5.xlt','/uploads/template/WD-DHS Component 1.5.xlt','~/view/template/WD_DHS_Component_1_5_xlt.aspx','A'),(43,'WD-DHS Template 1.0.xlt','/uploads/template/WD-DHS Template 1.0.xlt','~/view/template/WD_DHS_Template_1_0_xlt.aspx','A'),(44,'WD-GCMS 1.0.xlt','/uploads/template/WD-GCMS 1.0.xlt','~/view/template/WD_GCMS_1_0_xlt.aspx','A'),(45,'WD-GCMS 1.8.xlt','/uploads/template/WD-GCMS 1.8.xlt','~/view/template/WD_GCMS_1_8_xlt.aspx','A'),(46,'WD-GCMS Tooling 1.0.xlt','/uploads/template/WD-GCMS Tooling 1.0.xlt','~/view/template/WD_GCMS_Tooling_1_0_xlt.aspx','A'),(47,'WD-HPA Filtration 1.2.xlt','/uploads/template/WD-HPA Filtration 1.2.xlt','~/view/template/WD_HPA_Filtration_1_2_xlt.aspx','A'),(48,'WD-HPA Tape Test (Edge Damper, FCOF) 1.2.xlt','/uploads/template/WD-HPA Tape Test (Edge Damper, FCOF) 1.2.xlt','~/view/template/WD_HPA_Tape_Test_Edge_Dampe,_FCOF_1_2_xlt.aspx','A'),(49,'WD-HPA Tape Test (HGA, Suspension) 1.0.xlt','/uploads/template/WD-HPA Tape Test (HGA, Suspension) 1.0.xlt','~/view/template/WD_HPA_Tape_Test_HGA_Suspension_1_0_xlt.aspx','A'),(50,'WD-HPA Tape Test (HSA, APFA, ACA) 1.1.xlt','/uploads/template/WD-HPA Tape Test (HSA, APFA, ACA) 1.1.xlt','~/view/template/WD_HPA_Tape_Test_HSA_APFA_ACA_1_1_xlt.aspx','A'),(51,'WD-HPA Tape Test (HSA, APFA, ACA) 1.6.xlt','/uploads/template/WD-HPA Tape Test (HSA, APFA, ACA) 1.6.xlt','~/view/template/WD_HPA_Tape_Test_HSA_APFA_ACA_1_6_xlt.aspx','A'),(52,'WD-HPA Tape Test 1.2.xlt','/uploads/template/WD-HPA Tape Test 1.2.xlt','~/view/template/WD_HPA_Tape_Test_1_2_xlt.aspx','A'),(53,'WD-IC Component 1.1.xlt','/uploads/template/WD-IC Component 1.1.xlt','~/view/template/WD_IC_Component_1_1_xlt.aspx','A'),(54,'WD-IC Component 1.3.xlt','/uploads/template/WD-IC Component 1.3.xlt','~/view/template/WD_IC_Component_1_3_xlt.aspx','A'),(55,'WD-IC Template 1.0.xlt','/uploads/template/WD-IC Template 1.0.xlt','~/view/template/WD_IC_Template_1_0_xlt.aspx','A'),(56,'WD-IR Component 1.5.xlt','/uploads/template/WD-IR Component 1.5.xlt','~/view/template/WD_IR_Component_1_5_xlt.aspx','A'),(57,'WD-LPC Component 2.0.xlt','/uploads/template/WD-LPC Component 2.0.xlt','~/view/template/WD_LPC_Component_2_0_xlt.aspx','A'),(58,'WD-LPC Component 2.2.xlt','/uploads/template/WD-LPC Component 2.2.xlt','~/view/template/WD_LPC_Component_2_2_xlt.aspx','A'),(59,'WD-LPC Template 1.0.xlt','/uploads/template/WD-LPC Template 1.0.xlt','~/view/template/WD_LPC_Template_1_0_xlt.aspx','A'),(60,'WD-MESA Template 1.1.xlt','/uploads/template/WD-MESA Template 1.1.xlt','~/view/template/WD_MESA_Template_1_1_xlt.aspx','A');
/*!40000 ALTER TABLE `m_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_title`
--

DROP TABLE IF EXISTS `m_title`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_title` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_title`
--

LOCK TABLES `m_title` WRITE;
/*!40000 ALTER TABLE `m_title` DISABLE KEYS */;
INSERT INTO `m_title` VALUES (1,'Mr.','A'),(2,'Mrs.','A'),(3,'Miss.','A'),(4,'Ms.','A');
/*!40000 ALTER TABLE `m_title` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_type_of_test`
--

DROP TABLE IF EXISTS `m_type_of_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_type_of_test` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `specification_id` int(11) NOT NULL,
  `prefix` varchar(10) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `parent` int(11) DEFAULT NULL,
  `status` varchar(1) DEFAULT 'A',
  PRIMARY KEY (`ID`),
  KEY `fk_specification_id_1_idx` (`specification_id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_type_of_test`
--

LOCK TABLES `m_type_of_test` WRITE;
/*!40000 ALTER TABLE `m_type_of_test` DISABLE KEYS */;
INSERT INTO `m_type_of_test` VALUES (1,1,'F','NVR',-1,'A'),(2,1,'F','FTIR',-1,'A'),(3,1,'F','NVR & FTIR',-1,'A'),(5,1,'IB','IC',-1,'A'),(6,1,'LB','LPC',-1,'A'),(7,1,'DB','DHS',-1,'A'),(8,1,'G','GCMS',-1,'A'),(10,1,'H','SEM/EDX',-1,'A'),(11,1,'CORB','Corrosion by Humidity Chamber',-1,'A'),(12,1,'COP','Copper Wire (Seagate only)',-1,'A'),(13,1,'IB','Anion+NH4',5,'A'),(14,1,'IB','Anion',5,'A'),(15,1,'IB','Cation',5,'A'),(16,1,'LB','0.3',6,'A'),(17,1,'LB','0.5',6,'A'),(18,1,'LB','0.6',6,'A'),(19,1,'G','4X Rinse (Seagate)',8,'A'),(20,1,'G','Hydro Oil (Seagate)',8,'A'),(21,1,'G','Organic residue (WD)',8,'A'),(22,1,'H','HPA Tape Test',10,'A'),(23,1,'H','HPA Filtration',10,'A'),(24,1,'H','Swap',10,'A'),(25,1,'H','Talc',10,'A'),(26,1,'H','Contaminate',10,'A'),(27,2,'F','NVR',-1,'A'),(28,2,'F','FTIR',-1,'A'),(29,2,'F','NVR & FTIR',-1,'A'),(30,2,'CVR','CVR',-1,'A'),(31,2,'IB','IC',-1,'A'),(32,2,'LB','LPC',-1,'A'),(33,2,'D','DHS',-1,'A'),(34,2,'G','GCMS',-1,'A'),(35,2,'M','MESA',-1,'A'),(36,2,'H','SEM/EDX',-1,'A'),(37,2,'CORB','Corrosion by Humidity Chamber',-1,'A'),(38,2,'COP','Copper Wire (Seagate only)',-1,'A'),(39,2,'IB','Anion+NH4',31,'A'),(40,2,'IB','Anion',31,'A'),(41,2,'IB','Cation',31,'A'),(42,2,'LB','0.3',32,'A'),(43,2,'LB','0.5',32,'A'),(44,2,'LB','0.6',32,'A'),(45,2,'G','4X Rinse (Seagate)',34,'A'),(46,2,'G','Hydro Oil (Seagate)',34,'A'),(47,2,'G','Organic residue (WD)',34,'A'),(48,2,'H','HPA Tape Test',36,'A'),(49,2,'H','HPA Filtration',36,'A'),(50,2,'H','Swap',36,'A'),(51,2,'H','Talc',36,'A'),(52,2,'H','Contaminate',36,'A'),(65,3,'IB','Anion+NH4',57,'A'),(66,3,'IB','Anion',57,'A'),(67,3,'IB','Cation',57,'A'),(68,3,'LB','0.3',58,'A'),(69,3,'LB','0.5',58,'A'),(70,3,'LB','0.6',58,'A'),(71,3,'G','4X Rinse (Seagate)',60,'A'),(72,3,'G','Hydro Oil (Seagate)',60,'A'),(73,3,'G','Organic residue (WD)',60,'A'),(74,3,'H','HPA Tape Test',62,'A'),(75,3,'H','HPA Filtration',62,'A'),(76,3,'H','Swap',62,'A'),(77,3,'H','Talc',62,'A'),(78,3,'H','Contaminate',62,'A');
/*!40000 ALTER TABLE `m_type_of_test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_29_specification`
--

DROP TABLE IF EXISTS `template_29_specification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_29_specification` (
  `ID` int(11) NOT NULL,
  `A` varchar(150) DEFAULT NULL,
  `B` varchar(45) DEFAULT NULL,
  `C` varchar(45) DEFAULT NULL,
  `D` varchar(45) DEFAULT NULL,
  `E` varchar(45) DEFAULT NULL,
  `F` varchar(45) DEFAULT NULL,
  `G` varchar(45) DEFAULT NULL,
  `H` varchar(45) DEFAULT NULL,
  `I` varchar(45) DEFAULT NULL,
  `J` varchar(45) DEFAULT NULL,
  `K` varchar(45) DEFAULT NULL,
  `L` varchar(45) DEFAULT NULL,
  `M` varchar(45) DEFAULT NULL,
  `N` varchar(45) DEFAULT NULL,
  `O` varchar(45) DEFAULT NULL,
  `P` varchar(45) DEFAULT NULL,
  `Q` varchar(45) DEFAULT NULL,
  `R` varchar(45) DEFAULT NULL,
  `S` varchar(45) DEFAULT NULL,
  `T` varchar(45) DEFAULT NULL,
  `U` varchar(45) DEFAULT NULL,
  `V` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_29_specification`
--

LOCK TABLES `template_29_specification` WRITE;
/*!40000 ALTER TABLE `template_29_specification` DISABLE KEYS */;
INSERT INTO `template_29_specification` VALUES (1,'Mechanical Cmpts in Contact w Discs (non EN Plated) to be cleaned','20800010-100 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','4','SO4'),(2,'Mechanical Cmpts in Contact w Discs (non EN Plated) to be used directly in cleanroom','20800010-100 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.1','<0.1','NA','NA','NA','NA','NA','NA','N','','5','Br'),(3,'Mechanical Cmpts in Contact w Discs (EN Plated) to be cleaned','20800010-100 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.15','<0.2 (excluded PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','6','F'),(4,'Mechanical Cmpts in Contact w Discs (EN Plated) to be used directly in cleanroom','20800010-100 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.1 (excluded PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','7','Cl'),(5,'Mechanical Cmpts in Contact w Discs (DDG EN Plated) to be cleaned','20800010-100 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.9','<0.2 (excluded PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','6','F'),(6,'Mechanical Cmpts in Contact w Discs (DDG EN Plated) to be used directly in cleanroom','20800010-100 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.48','<0.1 (excluded PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','7','Cl'),(7,'Head Stack Ass y (HSA)','20800010-110 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','8','NO3'),(8,'Head Stack Assy (HSA) (EN Plated)','20800010-110 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.2 (excluded PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','9','NO2'),(9,'Mechanical Cmpts Not in Contact w Discs (non EN, non FC) to be cleaned','20800010-120 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.2','<0.2','NA','NA','NA','NA','NA','NA','N','','10','PO4'),(10,'Mechanical Cmpts Not in Contact w Discs (non EN, non FC) to be used directly in the cleanroom','20800010-120 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','11','Total'),(11,'Mechanical Cmpts Not in Contact w Discs (EN Plated, non FC) to be Cleaned','20800010-120 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.15','<0.2 (excluded PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','12','NH4'),(12,'Mechanical Cmpts Not in Contact w Discs (EN Plated, non FC) to be used Directly in the cleanroom','20800010-120 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.2 (excluded PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','13','Li'),(13,'Mechanical Cmpts Not in Contact with Discs (non EN Plated, fluorocarbon mat l) Parts to be Cleaned','20800010-120 Rev.BE','µg/cm2','<0.1','<0.1','<0.5','<0.1','<0.1','<0.1','<0.1','<0.2','<0.2','NA','NA','NA','NA','NA','NA','N','','14','Ca'),(14,'Mechanical Cmpts Not in Contact w Discs (non EN Plated, flurocarbon material) to be used Directly in the cleanroom','20800010-120 Rev.BE','µg/cm2','<0.03','<0.05','<0.1','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','15','K'),(15,'Mechanical Cmpts Not in Contact w Discs (EN Plated, fluorocarbon material) to be Cleaned','20800010-120 Rev.BE','µg/cm2','<0.1','<0.1','<0.5','<0.1','<0.1','<0.1','<0.15','<0.2 (excluded PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','16','Na'),(16,'Mechanical Cmpts Not in Contact w Discs (EN Plated, fluorocarbon material) to be used Directly in the cleanroom','20800010-120 Rev.BE','µg/cm2','<0.03','<0.05','<0.1','<0.05','<0.05','<0.05','<0.15','<0.2 (exclude PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','17','Mg'),(17,'Breather Filters','20800010-130 Rev.BE','µg/cm2','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','18','Total Cations'),(18,'For Future Component','20800010-140 Rev.BE','','','','','','','','','','','','','','','','','','','19','Exclude PO4?'),(19,'Recirculation Filter','20800010-150 Rev.BE','µg/cm2','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','',''),(20,'Spindle Mtr / Integr d MBA / MBA (non EN Plated) ','20800010-160 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','',''),(21,'Spindle Mtr / Integr d MBA / MBA (EN Plated) ','20800010-160 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.2 (excluded PO4)','<0.1','NA','NA','NA','NA','NA','NA','Y','','',''),(22,'Spindle Mtr / Integr d MBA / MBA (Crashstop - Fluorocarbon Elastomer only) to be cleaned ','20800010-270 Rev.BE','µg/cm2','<0.1','<0.1','<0.5','<0.1','<0.1','<0.1','<0.1','<1','<0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(23,'Spindle Mtr / Integr d MBA / MBA (Crashstop - Fluorocarbon Elastomer only) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','<0.03','<0.05','<0.1','<0.05','<0.05','<0.05','<0.05','<0.3','<0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(24,'Spindle Mtr / Integr d MBA / MBA (Crashstop - Non-Fluorocarbon Elastomer only) to be cleaned ','20800010-270 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.2','<0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(25,'Spindle Mtr / Integr d MBA / MBA (Crashstop - Non-Fluorocarbon Elastomer only) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(26,'For Future Component','20800010-170 Rev.BE','','','','','','','','','','','','','','','','','','','',''),(27,'Voice Coil Mechanism (non EN Plate, applied to FC) to be used directly in the cleanroom','20800010-180 Rev.BE','µg/cm2','<0.05','<0.05','<0.10','<0.05','<0.05','<0.05','<0.15',' <1.00','<0.2','NA','NA','NA','NA','NA','NA','N','','',''),(28,'Voice Coil Mechanism (non EN Plate, applied to Non FC) to be used directly in the cleanroom','20800010-180 Rev.BE','µg/cm2','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15',' <0.20','<0.2','NA','NA','NA','NA','NA','NA','N','','',''),(29,'Voice Coil Mechanism (EN Plate, applied to FC) to be used directly in the cleanroom','20800010-180 Rev.BE','µg/cm2','<0.05','<0.05','<0.10','<0.05','<0.05','<0.05','<0.15','<1.00 (excluded PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','',''),(30,'Voice Coil Mechanism (EN Plate, applied to Non FC) to be used directly in the cleanroom','20800010-180 Rev.BE','µg/cm2','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.15','<0.20 (exclude PO4)','<0.2','NA','NA','NA','NA','NA','NA','Y','','',''),(31,'Voice Coil Mechanism (Crashstop - Fluorocarbon Elastomer only) to be cleaned ','20800010-270 Rev.BE','µg/cm2','<0.1','<0.1','<0.5','<0.1','<0.1','<0.1','<0.1','<1','<0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(32,'Voice Coil Mechanism (Crashstop - Fluorocarbon Elastomer only) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','<0.03','<0.05','<0.1','<0.05','<0.05','<0.05','<0.05','<0.3','<0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(33,'Voice Coil Mechanism (Crashstop - Non-Fluorocarbon Elastomer only) to be cleaned ','20800010-270 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.2','<0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(34,'Voice Coil Mechanism (Crashstop - Non-Fluorocarbon Elastomer only) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','<0.2','<0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(35,'Head Gimbal Assy (HGA) (PZT)','20800010-190 Rev.BE','ng/part','< 5.0','< 1.0','< 1.0','< 3.0','< 3.0','< 3.0','< 10.0','< 15.0','< 2.3','HIDE','< 2.5','< 4.5','< 6.0','NA','< 10.0','N','','',''),(36,'Head Gimbal Assy (HGA) (Sliders*)','20800010-190 Rev.BE','ng/slider','< 5.0','< 1.0','< 1.0','< 3.0','< 3.0','< 3.0','< 10.0','< 15.0','< 2.3','HIDE','< 2.5','< 4.5','< 6.0','NA','< 10.0','N','','',''),(37,'Head Gimbal Assy (HGA) (Load Arm Assembly)','20800010-190 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.10','< 0.10','HIDE','< 0.10','< 0.10','< 0.05','< 0.05','< 0.05','N','','',''),(38,'Head Gimbal Assy (HGA) (TGA Circuit)','20800010-190 Rev.BE','µg/cm2','< 0.30','< 0.02','< 0.02','< 0.03','< 0.05','< 0.05','< 0.05','< 0.35','< 0.20','HIDE','< 0.10','< 0.50','< 0.40','NA','< 0.70','N','','',''),(39,'Head Gimbal Assy (HGA) (TGA)','20800010-190 Rev.BE','µg/cm2','< 0.30','< 0.02','< 0.02','< 0.07','< 0.05','< 0.05','< 0.05','< 0.35','< 0.20','HIDE','< 0.10','< 0.50','< 0.40','0.15','< 0.70','N','','',''),(40,'Head Gimbal Assy (HGA) (Head Gimbal Assembly)','20800010-190 Rev.BE','µg/cm2','< 0.30','< 0.02','< 0.02','< 0.07','NA','< 0.05','< 0.05','< 0.35','< 0.20','HIDE','<0.10','<0.50','<0.40','<0.15','<0.70','N','','',''),(41,'Actuators, Extruded/Overmolded/Stamped & Arm-Coil, Arm Coil PCCA w & w/o Bearing Assy (ACPB) (non EN Plated) to be used directly in the cleanroom / As','20800010-200 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.10','NA','NA','NA','NA','NA','NA','N','','',''),(42,'Actuators, Extruded/Overmolded/Stamped & Arm-Coil, Arm Coil PCCA w & w/o Bearing Assy (ACPB) (EN Plated) to be used directly in the cleanroom / As rec','20800010-200 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.15','< 0.2 (excluded PO4)','< 0.1','NA','NA','NA','NA','NA','NA','Y','','',''),(43,'Coil or Voice Coil (non EN Plated) to be cleaned','20800010-210 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(44,'Coil or Voice Coil (non EN Plated) to be used directly in the cleanroom','20800010-210 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(45,'Actuator Cartridge Bearing Ass y','20800010-220 Rev.BE','µg/cm2','< 0.1','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(46,'Flexible Printed Circuit Ass y - to be cleaned','20800010-230 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(47,'Flexible Printed Circuit Ass y - to be used directly in the cleanroom','20800010-230 Rev.BE','µg/cm2','< 0.03','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(48,'Flexible Printed Circuit - to be cleaned','20800010-240 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(49,'Flexible Printed Circuit - to be used directly in the cleanroom','20800010-240 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(50,'Drive Internal Hardware (non EN Teflon) to be cleaned','20800010-250 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(51,'Drive Internal Hardware (non EN Teflon) to be used directly in the cleanroom','20800010-250 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(52,'Drive Internal Hardware (EN Teflon) to be cleaned','20800010-250 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.15','< 0.2 (excluded PO4)','< 0.2','NA','NA','NA','NA','NA','NA','Y','','',''),(53,'Drive Internal Hardware (EN Teflon) to be used directly in the cleanroom','20800010-250 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.15','< 0.2 (excluded PO4)','< 0.1','NA','NA','NA','NA','NA','NA','Y','','',''),(54,'Plastic Components - to be cleaned','20800010-260 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(55,'Plastic Components - to be used directly in the cleanroom','20800010-260 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.20','< 0.1','NA','NA','NA','NA','NA','NA','N','','',''),(56,'Elastomer Materials (Fluorocarbon Elastomers) to be cleaned','20800010-270 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.5','< 0.1','< 0.1','< 0.1','< 0.1','< 1.00','< 0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(57,'Elastomer Materials (Fluorocarbon Elastomers) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','< 0.03','< 0.05','< 0.1','< 0.05','< 0.05','< 0.05','< 0.05','< 0.30','< 0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(58,'Elastomer Materials (non-Fluorocarbon Elastomers) to be cleaned','20800010-270 Rev.BE','µg/cm2','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','<0.1','< 0.20','<0.2','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(59,'Elastomer Materials (non-Fluorocarbon Elastomers) to be used directly in the cleanroom','20800010-270 Rev.BE','µg/cm2','<0.03','<0.05','<0.05','<0.05','<0.05','<0.05','<0.05','< 0.20','<0.1','NA','NA','NA','NA','NA','NA','N','','','refer 20800010-270'),(60,' Damper (Internal & External to HDA), ACA Damper, VCM Damper','20800010-280 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(61,'Top Cover Gasket (No Spec)','20800010-290 Rev.BE','','','','','','','','','','','','','','','','','','','',''),(62,'PCBA Insulation Foams / Sheet','20800010-300 Rev.BE','µg/g','< 8.0','HIDE','HIDE','< 88.0','HIDE','HIDE','HIDE','HIDE','< 20.0','HIDE','HIDE','HIDE','HIDE','HIDE','HIDE','N','','',''),(63,'Seals (Pressure Sensitive Adhesive)','20800010-310 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(64,'For Future Component','20800010-320 Rev.BE','','','','','','','','','','','','','','','','','','','',''),(65,'For Future Component','20800010-330 Rev.BE','','','','','','','','','','','','','','','','','','','',''),(66,'Carbon Adsorbent Filter and Desiccant','20800010-340 Rev.BE','µg/cm2','< 0.05','< 0.05','< 0.05','< 0.05','< 0.05','< 0.15','< 0.05','< 0.20','< 0.16','NA','NA','NA','NA','NA','NA','N','','',''),(67,'VCM Damper','20800010-350 Rev.BE','µg/cm2','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.1','< 0.20','< 0.2','NA','NA','NA','NA','NA','NA','N','','',''),(68,' no spec','-','µg/cm2','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','NA','','','','');
/*!40000 ALTER TABLE `template_29_specification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_29_workingpg_ic`
--

DROP TABLE IF EXISTS `template_29_workingpg_ic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_29_workingpg_ic` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `sample_id` int(11) DEFAULT NULL,
  `specification_id` int(11) DEFAULT NULL,
  `b9` decimal(10,0) DEFAULT NULL,
  `b10` decimal(10,0) DEFAULT NULL,
  `b11` decimal(10,0) DEFAULT NULL,
  `b14` decimal(10,0) DEFAULT NULL,
  `b15` decimal(10,0) DEFAULT NULL,
  `b16` decimal(10,0) DEFAULT NULL,
  `b17` decimal(10,0) DEFAULT NULL,
  `b18` decimal(10,0) DEFAULT NULL,
  `b19` decimal(10,0) DEFAULT NULL,
  `b20` decimal(10,0) DEFAULT NULL,
  `b23` decimal(10,0) DEFAULT NULL,
  `b24` decimal(10,0) DEFAULT NULL,
  `b25` decimal(10,0) DEFAULT NULL,
  `b26` decimal(10,0) DEFAULT NULL,
  `b27` decimal(10,0) DEFAULT NULL,
  `b28` decimal(10,0) DEFAULT NULL,
  `c14` decimal(10,0) DEFAULT NULL,
  `c15` decimal(10,0) DEFAULT NULL,
  `c16` decimal(10,0) DEFAULT NULL,
  `c17` decimal(10,0) DEFAULT NULL,
  `c18` decimal(10,0) DEFAULT NULL,
  `c19` decimal(10,0) DEFAULT NULL,
  `c20` decimal(10,0) DEFAULT NULL,
  `c23` decimal(10,0) DEFAULT NULL,
  `c25` decimal(10,0) DEFAULT NULL,
  `c26` decimal(10,0) DEFAULT NULL,
  `c27` decimal(10,0) DEFAULT NULL,
  `c28` decimal(10,0) DEFAULT NULL,
  `c24` decimal(10,0) DEFAULT NULL,
  `result_c25` decimal(10,0) DEFAULT NULL,
  `result_c26` decimal(10,0) DEFAULT NULL,
  `result_c27` decimal(10,0) DEFAULT NULL,
  `result_c28` decimal(10,0) DEFAULT NULL,
  `result_c29` decimal(10,0) DEFAULT NULL,
  `result_c30` decimal(10,0) DEFAULT NULL,
  `result_c31` decimal(10,0) DEFAULT NULL,
  `result_c32` decimal(10,0) DEFAULT NULL,
  `result_c34` decimal(10,0) DEFAULT NULL,
  `result_c35` decimal(10,0) DEFAULT NULL,
  `result_c36` decimal(10,0) DEFAULT NULL,
  `result_c37` decimal(10,0) DEFAULT NULL,
  `result_c38` decimal(10,0) DEFAULT NULL,
  `result_c39` decimal(10,0) DEFAULT NULL,
  `result_c40` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_sample_id_idx` (`sample_id`),
  KEY `fk_specification_id_idx` (`specification_id`),
  CONSTRAINT `fk_sample_id` FOREIGN KEY (`sample_id`) REFERENCES `job_sample` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_specification_id` FOREIGN KEY (`specification_id`) REFERENCES `template_29_specification` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_29_workingpg_ic`
--

LOCK TABLES `template_29_workingpg_ic` WRITE;
/*!40000 ALTER TABLE `template_29_workingpg_ic` DISABLE KEYS */;
INSERT INTO `template_29_workingpg_ic` VALUES (1,48,17,1,2,3,4,6,8,10,12,14,16,18,20,22,24,26,28,5,7,9,11,13,15,17,19,23,25,27,29,21,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(2,47,54,1,2,3,4,6,8,10,12,14,16,18,20,22,24,26,28,5,7,9,11,13,15,17,19,23,25,27,29,21,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1),(3,47,67,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `template_29_workingpg_ic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_login`
--

DROP TABLE IF EXISTS `user_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_login` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `role_id` int(11) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL DEFAULT '1234',
  `latest_login` datetime DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `status` varchar(1) NOT NULL DEFAULT 'A',
  `responsible_test` varchar(45) DEFAULT NULL,
  `is_force_change_password` bit(1) DEFAULT b'0',
  PRIMARY KEY (`id`),
  KEY `fk_role_id_idx` (`role_id`),
  CONSTRAINT `fk_role_id` FOREIGN KEY (`role_id`) REFERENCES `m_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_login`
--

LOCK TABLES `user_login` WRITE;
/*!40000 ALTER TABLE `user_login` DISABLE KEYS */;
INSERT INTO `user_login` VALUES (1,1,'admin','21232F297A57A5A743894A0E4A801FC3','2015-02-13 15:43:58','pawit1357@hotmail.com',0,'2015-02-13 15:43:58','A',NULL,'\0'),(2,2,'pikul.totassa','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:34:25','pikul.totassa@alsglobal.com',0,'2015-02-14 17:34:25','A',NULL,NULL),(3,3,'chanchira.chanprasert','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:39:30','chanchira.chanprasert@alsglobal.com',0,'2015-02-14 17:39:30','A','GCMS',NULL),(4,3,'lampoon.srihnongwa','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:40:26','lampoon.srihnongwa@alsglobal.com',0,'2015-02-14 17:40:26','A','DHS,GCMS',NULL),(5,3,'nunta.thotho','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:41:08','nunta.thotho@alsglobal.com',0,'2015-02-14 17:41:08','A','LPC',NULL),(6,3,'onanong.pithakpong','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:41:50','onanong.pithakpong@alsglobal.com',0,'2015-02-14 17:41:50','A','IC',NULL),(7,3,'pavinee.phawaphotano','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:42:43','pavinee.phawaphotano@alsglobal.com',0,'2015-02-14 17:42:43','A','MESA',NULL),(8,3,'rattana.tammasin','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:43:24','rattana.tammasin@alsglobal.com',0,'2015-02-14 17:43:24','A','DHS',NULL),(9,3,'sayan.songka','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:44:27','sayan.songka@alsglobal.com',0,'2015-02-14 17:44:27','A','FTIR',NULL),(10,3,'jakkrit.chairasamee','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:45:15','jakkrit.chairasamee@alsglobal.com',0,'2015-02-14 17:45:15','A','GCMS',NULL),(11,3,'wattana.trachoo','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:46:07','wattana.trachoo@alsglobal.com',0,'2015-02-14 17:46:07','A','MESA',NULL),(12,3,'yuwadee.kenming','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:47:06','yuwadee.kenming@alsglobal.com',0,'2015-02-14 17:47:06','A','DHS',NULL),(13,3,'thanyaporn.vongyara','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:47:56','thanyaporn.vongyara@alsglobal.com',0,'2015-02-14 17:47:56','A','GCMS',NULL),(14,4,'udomlak.pattanajitpitak','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:49:08','udomlak.pattanajitpitak@alsglobal.com',0,'2015-02-14 17:49:08','A',NULL,NULL),(15,4,'rossukhon.khongphuwet','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:50:06','rossukhon.khongphuwet@alsglobal.com',0,'2015-02-14 17:50:06','A',NULL,NULL),(16,5,'pornpan.wingwon','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:50:59','pornpan.wingwon@alsglobal.com',0,'2015-02-14 17:50:59','A',NULL,NULL),(17,5,'orapin.maliwan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:51:35','orapin.maliwan@alsglobal.com',0,'2015-02-14 17:51:35','A',NULL,NULL),(18,5,'sukanya.dawan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:52:16','sukanya.dawan@alsglobal.com',0,'2015-02-14 17:52:16','A',NULL,NULL),(19,6,'Warunee.Maneesuwan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:52:53','Warunee.Maneesuwan@alsglobal.com',0,'2015-02-14 17:52:53','A',NULL,NULL),(20,6,'chayan.jutaphan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:53:29','chayan.jutaphan@alsglobal.com',0,'2015-02-14 17:53:29','A',NULL,NULL);
/*!40000 ALTER TABLE `user_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_profile`
--

DROP TABLE IF EXISTS `user_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_profile` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_login_id` int(11) NOT NULL,
  `personal_card_id` varchar(13) DEFAULT NULL,
  `personal_title` int(11) NOT NULL,
  `first_name` varchar(255) DEFAULT NULL,
  `last_name` varchar(255) DEFAULT NULL,
  `gender` varchar(5) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `address1` varchar(255) DEFAULT NULL,
  `address2` varchar(255) DEFAULT NULL,
  `sub_district` varchar(255) DEFAULT NULL,
  `district` varchar(255) DEFAULT NULL,
  `province` varchar(255) DEFAULT NULL,
  `postal_code` varchar(5) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `mobile` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_title_id_1_idx` (`personal_title`),
  CONSTRAINT `fk_title_id_1` FOREIGN KEY (`personal_title`) REFERENCES `m_title` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_profile`
--

LOCK TABLES `user_profile` WRITE;
/*!40000 ALTER TABLE `user_profile` DISABLE KEYS */;
INSERT INTO `user_profile` VALUES (1,1,NULL,1,'pawit','sae-eaung',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,2,NULL,3,'pikul','totassa',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,3,NULL,3,'chanchira','chanprasert',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(10,4,NULL,3,'lampoon','srihnongwa',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,5,NULL,3,'nunta','thotho',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(12,6,NULL,3,'onanong','pithakpong',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(13,7,NULL,3,'pavinee','phawaphotano',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(14,8,NULL,3,'rattana','tammasin',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(15,9,NULL,1,'sayan','songka',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(16,10,NULL,1,'jakkrit','chairasamee',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(17,11,NULL,1,'wattana','trachoo',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(18,12,NULL,3,'yuwadee','kenming',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(19,13,NULL,3,'thanyaporn','vongyara',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(20,14,NULL,3,'udomlak','pattanajitpitak',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(21,15,NULL,3,'rossukhon','khongphuwet',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(22,16,NULL,3,'pornpan','wingwon',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(23,17,NULL,3,'orapin','maliwan',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(24,18,NULL,3,'sukanya','dawan',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(25,19,NULL,2,'Warunee','Maneesuwan',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(26,20,NULL,1,'chayan','jutaphan',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `user_profile` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-02-25 15:26:36
