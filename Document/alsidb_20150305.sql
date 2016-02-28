CREATE DATABASE  IF NOT EXISTS `alsi` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `alsi`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: alsi
-- ------------------------------------------------------
-- Server version	5.0.51b-community-nt-log

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
-- Not dumping tablespaces as no INFORMATION_SCHEMA.FILES table on this server
--

--
-- Table structure for table `job_info`
--

DROP TABLE IF EXISTS `job_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_info` (
  `ID` int(11) NOT NULL auto_increment,
  `contract_person_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `date_of_request` date default NULL,
  `date_of_receive` date default NULL,
  `customer_ref_no` varchar(45) default NULL COMMENT 'job number',
  `company_name_to_state_in_report` varchar(255) default NULL,
  `job_prefix` int(11) NOT NULL,
  `job_number` int(11) default NULL,
  `job_invoice` varchar(45) default NULL,
  `customer_po_ref` varchar(45) default NULL,
  `s_pore_ref_no` varchar(45) default NULL,
  `spec_ref_rev_no` varchar(45) default NULL,
  `sample_diposition` varchar(1) default NULL COMMENT '0=Discard,1=Return All',
  `status_sample_enough` varchar(1) default NULL,
  `status_sample_full` varchar(1) default NULL,
  `status_personel_and_workload` varchar(1) default NULL,
  `status_test_tool` varchar(1) default NULL,
  `status_test_method` varchar(1) default NULL,
  `create_by` int(11) default NULL,
  `update_by` int(11) default NULL,
  `create_date` date default NULL,
  `update_date` date default NULL,
  `document_type` varchar(1) default '1' COMMENT '1=,2=,3=',
  PRIMARY KEY  (`ID`),
  KEY `fk_contract_person_id_idx` (`contract_person_id`),
  KEY `fk_customer_id_idx` (`customer_id`),
  KEY `fk_job_running_id_1_idx` (`job_prefix`),
  CONSTRAINT `fk_contract_person_id` FOREIGN KEY (`contract_person_id`) REFERENCES `m_customer_contract_person` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_customer_id` FOREIGN KEY (`customer_id`) REFERENCES `m_customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_job_running_id_1` FOREIGN KEY (`job_prefix`) REFERENCES `job_running` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_info`
--

LOCK TABLES `job_info` WRITE;
/*!40000 ALTER TABLE `job_info` DISABLE KEYS */;
INSERT INTO `job_info` VALUES (5,8,8,'2015-03-08','2015-03-08','','',1,8,NULL,'','','','Y','N','Y','Y','Y','Y',2,2,'2015-03-08','2015-03-08','1');
/*!40000 ALTER TABLE `job_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_reiew_requistion`
--

DROP TABLE IF EXISTS `job_reiew_requistion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_reiew_requistion` (
  `ID` int(11) NOT NULL auto_increment,
  `job_id` int(11) default NULL,
  `detail` varchar(45) default NULL,
  `status` varchar(45) default NULL,
  `create_by` int(11) default NULL,
  `create_date` date default NULL,
  `update_by` int(11) default NULL,
  `update_date` date default NULL,
  PRIMARY KEY  (`ID`),
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
  `prefix` varchar(45) default NULL,
  `running_number` int(11) default '0',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_running`
--

LOCK TABLES `job_running` WRITE;
/*!40000 ALTER TABLE `job_running` DISABLE KEYS */;
INSERT INTO `job_running` VALUES (1,'ELP',8),(2,'ELS',4),(3,'FA',11),(4,'ELWA',1),(5,'GRP',2),(6,'TRB',2);
/*!40000 ALTER TABLE `job_running` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_sample`
--

DROP TABLE IF EXISTS `job_sample`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_sample` (
  `ID` int(11) NOT NULL auto_increment,
  `job_id` int(11) NOT NULL,
  `specification_id` int(11) NOT NULL,
  `type_of_test_id` int(11) NOT NULL,
  `template_id` int(11) NOT NULL,
  `job_number` varchar(45) default NULL,
  `description` varchar(255) default NULL,
  `model` varchar(255) default NULL,
  `surface_area` varchar(255) default NULL,
  `remarks` varchar(255) default NULL,
  `no_of_report` int(11) default NULL,
  `uncertainty` varchar(1) default NULL,
  `job_status` int(11) default NULL,
  `job_role` int(11) default NULL,
  `due_date` date default NULL,
  `path_word` varchar(100) default NULL,
  `path_pdf` varchar(100) default NULL,
  `status_completion_scheduled` int(11) default NULL,
  `step1owner` int(11) default NULL,
  `step2owner` int(11) default NULL,
  `step3owner` int(11) default NULL,
  `step4owner` int(11) default NULL,
  `step5owner` int(11) default NULL,
  `step6owner` int(11) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `fk_job_id_idx` (`job_id`),
  KEY `type_of_test_fk_idx` (`type_of_test_id`),
  KEY `fk_specification_id_idx` (`specification_id`),
  KEY `fk_status_id_idx` (`job_status`),
  KEY `fk_template_id_idx` (`template_id`),
  CONSTRAINT `fk_specification_id_1` FOREIGN KEY (`specification_id`) REFERENCES `m_specification` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_template_id` FOREIGN KEY (`template_id`) REFERENCES `m_template` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_type_of_test_id` FOREIGN KEY (`type_of_test_id`) REFERENCES `m_type_of_test` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_sample`
--

LOCK TABLES `job_sample` WRITE;
/*!40000 ALTER TABLE `job_sample` DISABLE KEYS */;
INSERT INTO `job_sample` VALUES (16,5,1,7,16,'ELP-00008-DB','RQ13-0662 Kojin APFA ww# 10 After Crest# 11 cell# 14 MinAik/ Engtek/ TI+SUMI/ Rencol/ NMB/ IPT(ECD)','Kojin APFA ','APFA = 143 cm2 AFA = 132 cm2','',1,'N',12,2,'2015-03-16',NULL,NULL,1,2,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `job_sample` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_sample_logs`
--

DROP TABLE IF EXISTS `job_sample_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_sample_logs` (
  `ID` int(11) NOT NULL auto_increment,
  `job_sample_id` int(11) default NULL,
  `job_status` int(11) default NULL,
  `job_remark` varchar(150) default NULL,
  `get_alerts` varchar(1) default '0',
  `date` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `fk_job_sample_id_2_idx` (`job_sample_id`),
  KEY `fk_job_status_id_2_idx` (`job_status`),
  CONSTRAINT `fk_job_sample_id_2` FOREIGN KEY (`job_sample_id`) REFERENCES `job_sample` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_job_status_id_2` FOREIGN KEY (`job_status`) REFERENCES `m_status` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_sample_logs`
--

LOCK TABLES `job_sample_logs` WRITE;
/*!40000 ALTER TABLE `job_sample_logs` DISABLE KEYS */;
INSERT INTO `job_sample_logs` VALUES (16,16,12,'','0','2015-03-08 14:01:37'),(17,16,12,'','0','2015-03-08 15:25:01'),(18,16,12,'','0','2015-03-08 15:50:56'),(19,16,12,'','0','2015-03-08 15:58:09'),(20,16,12,'','0','2015-03-08 16:12:03'),(21,16,12,'','0','2015-03-08 16:15:34');
/*!40000 ALTER TABLE `job_sample_logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_completion_scheduled`
--

DROP TABLE IF EXISTS `m_completion_scheduled`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_completion_scheduled` (
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(45) default NULL,
  `value` int(11) default NULL,
  PRIMARY KEY  (`ID`)
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
  `ID` int(11) NOT NULL auto_increment,
  `customer_code` varchar(45) default NULL,
  `company_name` varchar(45) default NULL,
  `address` varchar(200) default NULL,
  `sub_district` varchar(45) default NULL,
  `mobile_number` varchar(45) default NULL,
  `email_address` varchar(45) default NULL,
  `branch` varchar(45) default NULL,
  `district` varchar(45) default NULL,
  `ext` varchar(45) default NULL,
  `department` varchar(45) default NULL,
  `province` varchar(45) default NULL,
  `code` varchar(45) default NULL,
  `tel_number` varchar(45) default NULL,
  `create_by` int(11) default NULL,
  `create_date` date default NULL,
  `update_by` int(11) default NULL,
  `update_date` date default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
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
  `ID` int(11) NOT NULL auto_increment,
  `company_id` int(11) NOT NULL,
  `name` varchar(45) default NULL,
  `phone_number` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`),
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
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_role`
--

LOCK TABLES `m_role` WRITE;
/*!40000 ALTER TABLE `m_role` DISABLE KEYS */;
INSERT INTO `m_role` VALUES (1,'Root','A'),(2,'Login','A'),(3,'Chemist','A'),(4,'SrChemist','A'),(5,'Admin','A'),(6,'LabManager','A'),(7,'NTN-IC, NVR & FTIR (Bearphite).xlt',NULL);
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
  `role_id` int(11) default NULL,
  `code` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`),
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
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
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
  `ID` int(11) NOT NULL auto_increment,
  `status_group_id` int(11) default NULL,
  `name` varchar(100) default NULL,
  PRIMARY KEY  (`ID`),
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
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(100) default NULL,
  `description` varchar(100) default NULL,
  `status` varchar(45) default 'A',
  PRIMARY KEY  (`ID`)
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
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(100) default NULL,
  `path_source_file` varchar(100) default NULL,
  `path_url` varchar(100) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_template`
--

LOCK TABLES `m_template` WRITE;
/*!40000 ALTER TABLE `m_template` DISABLE KEYS */;
INSERT INTO `m_template` VALUES (-1,'-','-','-','H'),(1,'Canon-DHS.xlt','/uploads/template/Canon-DHS.xlt','~/view/template/Canon_DHS_xlt.aspx','A'),(2,'DI Water Template (Asia Micro,Thailand Sample, MClean)_Ver 2.XLT','/uploads/template/DI Water Template (Asia Micro,Thailand Sample, MClean)_Ver 2.XLT','~/view/template/DI_Water_Template_Asia_Micro_Thailand_Sample_MClean_Ver_2_XLT.aspx','A'),(3,'Donaldson-DHS.xlt','/uploads/template/Donaldson-DHS.xlt','~/view/template/Donaldson_DHS_xlt.aspx','A'),(4,'Donaldson-FTIR & NVR.xlt','/uploads/template/Donaldson-FTIR & NVR.xlt','~/view/template/Donaldson_FTIR_&_NVR_xlt.aspx','A'),(5,'Donalson-LPC.xlt','/uploads/template/Donalson-LPC.xlt','~/view/template/Donalson_LPC_xlt.aspx','A'),(6,'ELWA (IC Water).xlt','/uploads/template/ELWA (IC Water).xlt','~/view/template/ELWA_IC_Water_xlt.aspx','A'),(7,'NTN-IC, NVR & FTIR (Bearphite).xlt','/uploads/template/NTN-IC, NVR & FTIR (Bearphite).xlt','~/view/template/NTN_IC_NVR_&_FTIR_Bearphite_xlt.aspx','A'),(8,'NTN-IC, NVR & FTIR (Housing).xlt','/uploads/template/NTN-IC, NVR & FTIR (Housing).xlt','~/view/template/NTN_IC_NVR_&_FTIR_Housing_xlt.aspx','A'),(9,'Samsung-Pivot.xlt','/uploads/template/Samsung-Pivot.xlt','~/view/template/Samsung_Pivot_xlt.aspx','A'),(10,'Seagate-Corrosion Copper Wire.xlt','/uploads/template/Seagate-Corrosion Copper Wire.xlt','~/view/template/Seagate_Corrosion_Copper_Wire_xlt.aspx','A'),(11,'Seagate-Corrosion.xlt','/uploads/template/Seagate-Corrosion.xlt','~/view/template/Seagate_Corrosion_xlt.aspx','A'),(12,'Seagate-DHS (Cleanroom mat Construction).xlt','/uploads/template/Seagate-DHS (Cleanroom mat Construction).xlt','~/view/template/Seagate_DHS_Cleanroom_mat_Construction_xlt.aspx','A'),(13,'Seagate-DHS Adhesive.xlt','/uploads/template/Seagate-DHS Adhesive.xlt','~/view/template/Seagate_DHS_Adhesive_xlt.aspx','A'),(14,'Seagate-DHS Bag.xlt','/uploads/template/Seagate-DHS Bag.xlt','~/view/template/Seagate_DHS_Bag_xlt.aspx','A'),(15,'Seagate-DHS Component 1.1.xlt','/uploads/template/Seagate-DHS Component 1.1.xlt','~/view/template/Seagate_DHS_Component_1_1_xlt.aspx','A'),(16,'Seagate-DHS Component Rev. BE 1.5.xlt','/uploads/template/Seagate-DHS Component Rev. BE 1.5.xlt','~/view/template/Seagate_DHS_Component_Rev_BE_1_5.ascx','A'),(17,'Seagate-DHS Component.xlt','/uploads/template/Seagate-DHS Component.xlt','~/view/template/Seagate_DHS_Component_xlt.aspx','A'),(18,'Seagate-DHS Indirect Material.xlt','/uploads/template/Seagate-DHS Indirect Material.xlt','~/view/template/Seagate_DHS_Indirect_Material_xlt.aspx','A'),(19,'Seagate-DHS Tape Label.xlt','/uploads/template/Seagate-DHS Tape Label.xlt','~/view/template/Seagate_DHS_Tape_Label_xlt.aspx','A'),(20,'Seagate-FTIR Component Rev.BE 1.5.xlt','/uploads/template/Seagate-FTIR Component Rev.BE 1.5.xlt','~/view/template/Seagate_FTIR_Component_Rev_BE_1_5_xlt.aspx','A'),(21,'Seagate-FTIR Component.xlt','/uploads/template/Seagate-FTIR Component.xlt','~/view/template/Seagate_FTIR_Component_xlt.aspx','A'),(22,'Seagate-FTIR Consumable 1.2.xlt','/uploads/template/Seagate-FTIR Consumable 1.2.xlt','~/view/template/Seagate_FTIR_Consumable_1_2_xlt.aspx','A'),(23,'Seagate-FTIR Consumable.xlt','/uploads/template/Seagate-FTIR Consumable.xlt','~/view/template/Seagate_FTIR_Consumable_xlt.aspx','A'),(24,'Seagate-GCMS Component 1.9.xlt','/uploads/template/Seagate-GCMS Component 1.9.xlt','~/view/template/Seagate_GCMS_Component_1_9_xlt.aspx','A'),(25,'Seagate-GCMS Component.xlt','/uploads/template/Seagate-GCMS Component.xlt','~/view/template/Seagate_GCMS_Component_xlt.aspx','A'),(26,'Seagate-HPA Filtration Rev.BE 1.4.xlt','/uploads/template/Seagate-HPA Filtration Rev.BE 1.4.xlt','~/view/template/Seagate_HPA_Filtration_Rev_BE_1_4_xlt.aspx','A'),(27,'Seagate-HPA Filtration3.xlt','/uploads/template/Seagate-HPA Filtration3.xlt','~/view/template/Seagate_HPA_Filtration3_xlt.aspx','A'),(28,'Seagate-HPA Swab.xlt','/uploads/template/Seagate-HPA Swab.xlt','~/view/template/Seagate_HPA_Swab_xlt.aspx','A'),(29,'Seagate-IC Component 1.4.xlt','/uploads/template/Seagate-IC Component 1.4.xlt','~/view/template/Seagate_IC_Component_1_4.ascx','A'),(30,'Seagate-IC Component HGA.xlt','/uploads/template/Seagate-IC Component HGA.xlt','~/view/template/Seagate_IC_Component_HGA_xlt.aspx','A'),(31,'Seagate-IC Component PCBA.xlt','/uploads/template/Seagate-IC Component PCBA.xlt','~/view/template/Seagate_IC_Component_PCBA_xlt.aspx','A'),(32,'Seagate-IC Component.xlt','/uploads/template/Seagate-IC Component.xlt','~/view/template/Seagate_IC_Component_xlt.aspx','A'),(33,'Seagate-IC Consumable 1.2.xlt','/uploads/template/Seagate-IC Consumable 1.2.xlt','~/view/template/Seagate_IC_Consumable_1_2_xlt.aspx','A'),(34,'Seagate-LPC Component 1.4 Rev.BE.xlt','/uploads/template/Seagate-LPC Component 1.4 Rev.BE.xlt','~/view/template/Seagate_LPC_Component_1_4_Rev_BE_xlt.aspx','A'),(35,'Seagate-LPC Component.xlt','/uploads/template/Seagate-LPC Component.xlt','~/view/template/Seagate_LPC_Component_xlt.aspx','A'),(36,'Seagate-LPC Consumable.xlt','/uploads/template/Seagate-LPC Consumable.xlt','~/view/template/Seagate_LPC_Consumable_xlt.aspx','A'),(37,'Segate HPA(MgSiO)-Wiper&Paper.xlt','/uploads/template/Segate HPA(MgSiO)-Wiper&Paper.xlt','~/view/template/Segate_HPA_MgSiO_Wiper&Paper_xlt.aspx','A'),(38,'WD-Corrosion 1.0.xlt','/uploads/template/WD-Corrosion 1.0.xlt','~/view/template/WD_Corrosion_1_0_xlt.aspx','A'),(39,'WD-CVR 1.0.xlt','/uploads/template/WD-CVR 1.0.xlt','~/view/template/WD_CVR_1_0_xlt.aspx','A'),(40,'WD-DHS (Label, Sticker, Tape) 1.0.xlt','/uploads/template/WD-DHS (Label, Sticker, Tape) 1.0.xlt','~/view/template/WD_DHS_Label_Sticker_Tape_1_0_xlt.aspx','A'),(41,'WD-DHS Component 1.0.xlt','/uploads/template/WD-DHS Component 1.0.xlt','~/view/template/WD_DHS_Component_1_0_xlt.aspx','A'),(42,'WD-DHS Component 1.5.xlt','/uploads/template/WD-DHS Component 1.5.xlt','~/view/template/WD_DHS_Component_1_5_xlt.aspx','A'),(43,'WD-DHS Template 1.0.xlt','/uploads/template/WD-DHS Template 1.0.xlt','~/view/template/WD_DHS_Template_1_0_xlt.aspx','A'),(44,'WD-GCMS 1.0.xlt','/uploads/template/WD-GCMS 1.0.xlt','~/view/template/WD_GCMS_1_0_xlt.aspx','A'),(45,'WD-GCMS 1.8.xlt','/uploads/template/WD-GCMS 1.8.xlt','~/view/template/WD_GCMS_1_8_xlt.aspx','A'),(46,'WD-GCMS Tooling 1.0.xlt','/uploads/template/WD-GCMS Tooling 1.0.xlt','~/view/template/WD_GCMS_Tooling_1_0_xlt.aspx','A'),(47,'WD-HPA Filtration 1.2.xlt','/uploads/template/WD-HPA Filtration 1.2.xlt','~/view/template/WD_HPA_Filtration_1_2_xlt.aspx','A'),(48,'WD-HPA Tape Test (Edge Damper, FCOF) 1.2.xlt','/uploads/template/WD-HPA Tape Test (Edge Damper, FCOF) 1.2.xlt','~/view/template/WD_HPA_Tape_Test_Edge_Dampe,_FCOF_1_2_xlt.aspx','A'),(49,'WD-HPA Tape Test (HGA, Suspension) 1.0.xlt','/uploads/template/WD-HPA Tape Test (HGA, Suspension) 1.0.xlt','~/view/template/WD_HPA_Tape_Test_HGA_Suspension_1_0_xlt.aspx','A'),(50,'WD-HPA Tape Test (HSA, APFA, ACA) 1.1.xlt','/uploads/template/WD-HPA Tape Test (HSA, APFA, ACA) 1.1.xlt','~/view/template/WD_HPA_Tape_Test_HSA_APFA_ACA_1_1_xlt.aspx','A'),(51,'WD-HPA Tape Test (HSA, APFA, ACA) 1.6.xlt','/uploads/template/WD-HPA Tape Test (HSA, APFA, ACA) 1.6.xlt','~/view/template/WD_HPA_Tape_Test_HSA_APFA_ACA_1_6_xlt.aspx','A'),(52,'WD-HPA Tape Test 1.2.xlt','/uploads/template/WD-HPA Tape Test 1.2.xlt','~/view/template/WD_HPA_Tape_Test_1_2_xlt.aspx','A'),(53,'WD-IC Component 1.1.xlt','/uploads/template/WD-IC Component 1.1.xlt','~/view/template/WD_IC_Component_1_1_xlt.aspx','A'),(54,'WD-IC Component 1.3.xlt','/uploads/template/WD-IC Component 1.3.xlt','~/view/template/WD_IC_Component_1_3_xlt.aspx','A'),(55,'WD-IC Template 1.0.xlt','/uploads/template/WD-IC Template 1.0.xlt','~/view/template/WD_IC_Template_1_0_xlt.aspx','A'),(56,'WD-IR Component 1.5.xlt','/uploads/template/WD-IR Component 1.5.xlt','~/view/template/WD_IR_Component_1_5_xlt.aspx','A'),(57,'WD-LPC Component 2.0.xlt','/uploads/template/WD-LPC Component 2.0.xlt','~/view/template/WD_LPC_Component_2_0_xlt.aspx','A'),(58,'WD-LPC Component 2.2.xlt','/uploads/template/WD-LPC Component 2.2.xlt','~/view/template/WD_LPC_Component_2_2_xlt.aspx','A'),(59,'WD-LPC Template 1.0.xlt','/uploads/template/WD-LPC Template 1.0.xlt','~/view/template/WD_LPC_Template_1_0_xlt.aspx','A'),(60,'WD-MESA Template 1.1.xlt','/uploads/template/WD-MESA Template 1.1.xlt','~/view/template/WD_MESA_Template_1_1_xlt.aspx','A');
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
  `name` varchar(45) default NULL,
  `status` varchar(45) default NULL,
  PRIMARY KEY  (`id`)
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
  `ID` int(11) NOT NULL auto_increment,
  `specification_id` int(11) NOT NULL,
  `prefix` varchar(10) default NULL,
  `name` varchar(45) default NULL,
  `parent` int(11) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`),
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
-- Table structure for table `tb_cas`
--

DROP TABLE IF EXISTS `tb_cas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_cas` (
  `ID` int(11) NOT NULL auto_increment,
  `sample_id` int(11) default NULL,
  `pk` varchar(255) default NULL,
  `rt` varchar(255) default NULL,
  `library_id` varchar(255) default NULL,
  `classification` varchar(255) default NULL,
  `cas` varchar(255) default NULL,
  `qual` varchar(255) default NULL,
  `area` varchar(255) default NULL,
  `amount` varchar(255) default NULL,
  `create_date` datetime default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_cas`
--

LOCK TABLES `tb_cas` WRITE;
/*!40000 ALTER TABLE `tb_cas` DISABLE KEYS */;
/*!40000 ALTER TABLE `tb_cas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_16_component`
--

DROP TABLE IF EXISTS `template_16_component`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_16_component` (
  `ID` int(11) NOT NULL,
  `A` varchar(150) default NULL,
  `B` varchar(45) default NULL,
  `C` varchar(45) default NULL,
  `D` varchar(45) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_16_component`
--

LOCK TABLES `template_16_component` WRITE;
/*!40000 ALTER TABLE `template_16_component` DISABLE KEYS */;
INSERT INTO `template_16_component` VALUES (1,'Head Stack Assembly (PSG, ESG, 3.5\", 2.5\")','20800010-110 Rev.BE','ng/part','3hrs'),(2,'Head Stack Assembly (Notebook 2.5\"(9.5mm))','20800010-110 Rev.BE','ng/part','3hrs'),(3,'Head Stack Assembly (Notebook 2.5\", 5mm & 7mm)','20800010-110 Rev.BE','ng/part','3hrs'),(4,'Head Stack Assembly (Notebook 2.5\", 5mm & 7mm) Applicable to Science Park NSG, Angsana 2D and beyond','20800010-110 Rev.BE','ng/part','3hrs'),(5,'Head Stack Assembly M8 RHO & M8 BP2','20800010-110 Rev.BE','ng/part','3hrs'),(6,'Crashstops on Headstacks (Fluorocarbon Elastomers (For All))','20800010-110 Rev.BE','ng/g','3hrs'),(7,'Crashstops on Headstacks (EPDM Elastomers (For All))','20800010-110 Rev.BE','ug/g','3hrs'),(8,'Top Cover Ass\'y with Gasket (FIPG-Epoxy Based Materials) (Storage Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(9,'Top Cover Ass\'y with Gasket (FIPG-Epoxy Based Materials) (Day 0 Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(10,'Top Cover Ass\'y with Gasket (FIPG-Urethane Base Materials) (Storage Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(11,'Top Cover Ass\'y with Gasket (FIPG-Urethane Base Materials) (Day 0 Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(12,'Top Cover Assy with Form in place Damper (Damper with PSA) (Personal Storage)','20800010-120 Rev.BE','ng/cm2','3hrs'),(13,'Top Cover Assy with FIP Damper (FIPG-Epoxy Based Materials) (Storage Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(14,'Top Cover Assy with FIP Damper (FIPG-Epoxy Based Materials) (Day 0 Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(15,'Top Cover Assy with FIP Damper (FIPG-Urethane Base Materials) (Storage Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(16,'Top Cover Assy with FIP Damper (FIPG-Urethane Base Materials) (Day 0 Condition)','20800010-120 Rev.BE','ng/g','3hrs'),(17,'Breather Filters ','20800010-130 Rev.BE','ng/part','3hrs'),(18,'Recirculation Filter ','20800010-150 Rev.BE','ug/g','3hrs'),(19,'Integrated Motor Base Assembly/Motor Base Assembly (Enterprise/Personal 3.5\")','20800010-160 Rev.BE','ng/cm²','3hrs'),(20,'Integrated Motor Base Assembly/Motor Base Assembly (Enterprise/Notebook 2.5\")','20800010-160 Rev.BE','ng/cm²','3hrs'),(21,'Integrated Motor Base Assembly/Motor Base Assembly (Notebook 5mm&7mm (2.5\", Angsana ID & beyond)','20800010-160 Rev.BE','ng/cm²','3hrs'),(22,'Crashstops Only (Fluorocarbon Elastomers (For All)) (Personal Storage & Notebook NSG)','20800010-160 Rev.BE','ng/g','3hrs'),(23,'Crashtops Only (EPDM Elastomers (For All)) (Personal Storage & Notebook NSG)','20800010-160 Rev.BE','ug/g','3hrs'),(24,'VCM Assembly with Subcomponents (Personal / Enterprise 3.5\")','20800010-180 Rev.BE','ng/cm2','3hrs'),(25,'VCM Assembly with Subcomponents (Notebook 2.5\")','20800010-180 Rev.BE','ng/cm2','3hrs'),(26,'VCM Assembly with Subcomponents (Notebook 2.5\", Angsana ID and beyond)','20800010-180 Rev.BE','ng/cm2','3hrs'),(27,'VCM Assembly with Subcomponents (Enterprise 2.5\")','20800010-180 Rev.BE','ng/cm2','3hrs'),(28,'Crashstops Only (Fluorocarbon Elastomers (For All)) (Personal Storage & Notebook NSG)','20800010-160 Rev.BE','ng/g','3hrs'),(29,'Crashstops Only (EPDM Elastomers (For All)) (Personal Storage & Notebook NSG)','20800010-160 Rev.BE','ug/g','3hrs'),(30,'Head Gimbal Assembly (HGA) - 1 Hr Sampling','20800010-190 Rev.BE','ng/part','1hr'),(31,'Head Gimbal Assembly (HGA) - 3 Hr Sampling','20800010-190 Rev.BE','ng/part','3hrs'),(32,'Arm Coil PCCA / Overmold PCCA w clip type process in Seagate & w/o Bearing (Supplier) (PSG, ESG, NL 2.5\" & 3.5\")','20800010-200 Rev.BE','ng/part','3hrs'),(33,'Arm Coil / Overmold / PCCA (Notebook 2.5\")','20800010-200 Rev.BE','ng/part','3hrs'),(34,'Arm Coil / Overmold / PCCA with & without Pivot Bearing (Notebook 2.5\") Applicable to Science Park NGS, Angsana 2D and beyond.','20800010-200 Rev.BE','ng/part','3hrs'),(35,'Arm Coil PCCA / Overmold PCCA with glued in bearing or Tolerance ring from supplier (PSG, ESG 2.5\" & 3.5\")','20800010-200 Rev.BE','ng/part','3hrs'),(36,'Coil or Voice Coil (Notebook/Personal)','20800010-210 Rev.BE','ng/part','3hrs'),(37,'Coil or Voice Coil (Enterprise)','20800010-210 Rev.BE','ng/part','3hrs'),(38,'Actuator Cartridge Bearing Assembly (Personel 3.5\", ESG 2.5\" & 3.5\")','20800010-220 Rev.BE','Cartridge ng/part','3hrs'),(39,'Actuator Cartridge Bearing Assembly (Notebook 2.5\")','20800010-220 Rev.BE','Cartridge ng/part','3hrs'),(40,'Flexible Printed Circuit Assembly (Personal/Enterprise Storage)','20800010-230 Rev.BE','ng/cm2','3hrs'),(41,'Motor Flexible Printed Circuit Assembly','20800010-230 Rev.BE','ng/cm2','3hrs'),(42,'Flexible Printed Circuit (Personal/Enterprise Storage)','20800010-240 Rev.BE','ng/cm2','3hrs'),(43,'All Plastic Components ( Filter Brackets, Air Latch, Ramp, etc.) (Personel/Notebook 2.5\")','20800010-260 Rev.BE','ng/cm2','3hrs'),(44,'All Plastic Components except Connector & DSP  (Enterprise)','20800010-260 Rev.BE','ng/g','3hrs'),(45,'Plastic Components (Connectors) (Enterprise)','20800010-260 Rev.BE','ng/g','3hrs'),(46,'Plastic Components (DSP) (Enterprise/Personal)','20800010-260 Rev.BE','ng/cm2','3hrs'),(47,'Elastomer Materials (Fluorocarbon Elastomers (For All))','20800010-270 Rev.BE','ng/g','3hrs'),(48,'EPDM Elastomers (For All)','20800010-270 Rev.BE','ug/g','3hrs'),(49,'Damper, ACA Damper (Internal or External damper with adhesive exposed to HDA) (Personal Storage)','20800010-280 Rev.BE','ng/cm2','3hrs'),(50,'Damper, ACA Damper (Internal or External damper with adhesive exposed to HDA) (Enterprise Storage)','20800010-280 Rev.BE','ng/cm2','3hrs'),(51,'Damper with  PSA (Personal Storage)','20800010-280 Rev.BE','ng/cm2','3hrs'),(52,'Form- In-Place (FIP) Damper (FIPG - Epoxy Based Materials) (Storage Condition)','20800010-280 Rev.BE','ng/g','3hrs'),(53,'Form- In-Place (FIP) Damper (FIPG - Epoxy Based Materials) (Day 0 Condition)','20800010-280 Rev.BE','ng/g','3hrs'),(54,'Form- In-Place (FIP) Damper (FIPG - Urethane Based Materials) (Storage Condition)','20800010-280 Rev.BE','ng/g','3hrs'),(55,'Form- In-Place (FIP) Damper (FIPG - Urethane Based Materials) (Day 0 Condition)','20800010-280 Rev.BE','ng/g','3hrs'),(56,'Top Cover Gasket (FIPG - Epoxy Based Materials) (Storage Condition)','20800010-290 Rev.BE','ng/g','3hrs'),(57,'Top Cover Gasket (FIPG - Epoxy Based Materials) (Day 0 Condition)','20800010-290 Rev.BE','ng/g','3hrs'),(58,'Top Cover Gasket (FIPG - Urethane Based Materials) (Storage Condition)','20800010-290 Rev.BE','ng/g','3hrs'),(59,'Top Cover Gasket (FIPG - Urethane Based Materials) (Day 0 Condition)','20800010-290 Rev.BE','ng/g','3hrs'),(60,'PCBA Insulation Foam (Notebook 2.5\")  PCBA Foam Assy','20800010-300 Rev.BE','ng/cm2','3hrs'),(61,'PCBA TIM (Gap, Pad) (Notebook 2.5\") Raw TIM','20800010-300 Rev.BE','ng/part','3hrs'),(62,'PCBA TIM (Gap Pad) (Notebook 2.5\") PCBA TIM Assy','20800010-300 Rev.BE','ng/part','3hrs'),(63,'Seals (Pressure Sensitive Adhesive), Labels for HDA Internal','20800010-310 Rev.BE','ng/cm2','3hrs'),(64,'Carbon Adsorbent Filter & Desiccant (Personal Storage, Enterprise Storage)','20800010-340 Rev.BE','ng/cm2','3hrs'),(65,'Carbon Adsorbent Filter & Desiccant (Notebook)','20800010-340 Rev.BE','ng/cm2','3hrs');
/*!40000 ALTER TABLE `template_16_component` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_16_coverpage`
--

DROP TABLE IF EXISTS `template_16_coverpage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_16_coverpage` (
  `ID` int(11) NOT NULL auto_increment,
  `sample_id` int(11) default NULL,
  `component_id` int(11) default NULL,
  `name` varchar(200) default NULL,
  `ng_part` varchar(200) default NULL,
  `result` float default NULL,
  `cas_id` int(11) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `fk_job_sample_idx_2_idx` (`sample_id`),
  KEY `fk_component_idx_2_idx` (`component_id`),
  KEY `fk_detail_spec_idx_2_idx` (`name`),
  CONSTRAINT `fk_component_idx_3` FOREIGN KEY (`component_id`) REFERENCES `template_16_component` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_job_sample_idx_2` FOREIGN KEY (`sample_id`) REFERENCES `job_sample` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_16_coverpage`
--

LOCK TABLES `template_16_coverpage` WRITE;
/*!40000 ALTER TABLE `template_16_coverpage` DISABLE KEYS */;
INSERT INTO `template_16_coverpage` VALUES (52,16,22,'Phenol & Derivatives','400',0,0),(53,16,22,'Naphthalene','30',0,0),(54,16,22,'Alcohol','200',0,0),(55,16,22,'Aliphatic Hydrocarbon','500',0,0),(56,16,22,'Unknowns, Others','2100',0,0),(57,16,22,'Total of All Compounds','5000',0,0);
/*!40000 ALTER TABLE `template_16_coverpage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_16_detail_spec`
--

DROP TABLE IF EXISTS `template_16_detail_spec`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_16_detail_spec` (
  `ID` int(11) NOT NULL auto_increment,
  `component_order` int(11) default NULL,
  `component_id` int(11) default NULL,
  `name` varchar(150) default NULL,
  `ng_part` varchar(45) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `fk_template_16_component_id_1_idx` (`component_id`),
  CONSTRAINT `fk_template_16_component_id` FOREIGN KEY (`component_id`) REFERENCES `template_16_component` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3921 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_16_detail_spec`
--

LOCK TABLES `template_16_detail_spec` WRITE;
/*!40000 ALTER TABLE `template_16_detail_spec` DISABLE KEYS */;
INSERT INTO `template_16_detail_spec` VALUES (2621,1,1,'Acetophenone & Derivatives','1800'),(2622,2,1,'Alcohols','6000'),(2623,3,1,'Aliphatic HC','12000'),(2624,4,1,'Aliphatic & Aromatic Esters','1410'),(2625,5,1,'Alkylacrylate','1000'),(2626,6,1,'Amines & Amides','1340'),(2627,7,1,'Aromatic Hydrocarbon','6870'),(2628,8,1,'Benzoic Acid & Derivatives','300'),(2629,9,1,'Organic Acid','500'),(2630,10,1,'Organic Sulfur','600'),(2631,11,1,'BHT','1000'),(2632,12,1,'Halogen Containing Hydrocarbons','6020'),(2633,13,1,'Phenols & Derivatievs','3000'),(2634,14,1,'Siloxanes','500'),(2635,15,1,'Styrene','1500'),(2636,16,1,'Ethyl Hexanol','5500'),(2637,17,1,'Others & Unknowns','6000'),(2638,18,1,'Total of All Compounds','30000'),(2639,19,1,'-','-'),(2640,20,1,'-','-'),(2641,1,2,'Acetophenone & Derivatives','650'),(2642,2,2,'Alcohols','1260'),(2643,3,2,'Aliphatic HC','4600'),(2644,4,2,'Aliphatic & Aromatic Esters','300'),(2645,5,2,'Alkylacrylate','150'),(2646,6,2,'Amines & Amides','900'),(2647,7,2,'Aromatic Hydrocarbon','2800'),(2648,8,2,'Benzoic Acid & Derivatives','50'),(2649,9,2,'Organic Acid','150'),(2650,10,2,'Organic Sulfur','300'),(2651,11,2,'BHT','350'),(2652,12,2,'Halogen Containing Hydrocarbons','6000'),(2653,13,2,'Phenols & Derivatievs','950'),(2654,14,2,'Siloxanes','80'),(2655,15,2,'Styrene','160'),(2656,16,2,'Ethyl Hexanol','300'),(2657,17,2,'Others & Unknowns','500'),(2658,18,2,'Total of All Compounds','12000'),(2659,19,2,'-','-'),(2660,20,2,'-','-'),(2661,1,3,'Acetophenone & Derivatives','1000'),(2662,2,3,'Alcohols','2500'),(2663,3,3,'Aliphatic HC','5000'),(2664,4,3,'Aliphatic & Aromatic Esters','1100'),(2665,5,3,'Alkylacrylate','TBD'),(2666,6,3,'Amines & Amides','600'),(2667,7,3,'Aromatic Hydrocarbon','3300'),(2668,8,3,'Benzoic Acid & Derivatives','50'),(2669,9,3,'Organic Acid','150'),(2670,10,3,'Organic Sulfur','300'),(2671,11,3,'BHT','350'),(2672,12,3,'Halogen Containing Hydrocarbons','6000'),(2673,13,3,'Phenols & Derivatievs','500'),(2674,14,3,'Siloxanes','80'),(2675,15,3,'Styrene','120'),(2676,16,3,'Ethyl Hexanol','500'),(2677,17,3,'Others & Unknowns','900'),(2678,18,3,'Total of All Compounds','11000'),(2679,19,3,'-','-'),(2680,20,3,'-','-'),(2681,1,4,'Acetophenone & Derivatives','1000'),(2682,2,4,'Alcohols','2500'),(2683,3,4,'Aliphatic HC','5000'),(2684,4,4,'Aliphatic & Aromatic Esters','1100'),(2685,5,4,'Alkylacrylate','2,500*'),(2686,6,4,'Amines & Amides','600'),(2687,7,4,'Aromatic Hydrocarbon','3300'),(2688,8,4,'Benzoic Acid & Derivatives','50'),(2689,9,4,'Organic Acid','150'),(2690,10,4,'Organic Sulfur','300'),(2691,11,4,'BHT','350'),(2692,12,4,'Halogen Containing Hydrocarbons','6000'),(2693,13,4,'Phenols & Derivatievs','500'),(2694,14,4,'Siloxanes','80'),(2695,15,4,'Styrene','120'),(2696,16,4,'Ethyl Hexanol','500'),(2697,17,4,'Others & Unknowns','900'),(2698,18,4,'Total of All Compounds','11000'),(2699,19,4,'-','-'),(2700,20,4,'-','-'),(2701,1,5,'Acetophenone & Derivatives','650'),(2702,2,5,'Alcohols','1260'),(2703,3,5,'Aliphatic HC','20000'),(2704,4,5,'Aliphatic & Aromatic Esters','3000'),(2705,5,5,'Alkylacrylate','13000'),(2706,6,5,'Amines & Amides','900'),(2707,7,5,'Aromatic Hydrocarbon','2800'),(2708,8,5,'Benzoic Acid & Derivatives','50'),(2709,9,5,'Organic Acid','470'),(2710,10,5,'Organic Sulfur','1300'),(2711,11,5,'BHT','1000'),(2712,12,5,'Halogen Containing Hydrocarbons','6000'),(2713,13,5,'Phenols & Derivatievs','950'),(2714,14,5,'Siloxanes','150'),(2715,15,5,'Styrene','160'),(2716,16,5,'Ethyl Hexanol','300'),(2717,17,5,'Others & Unknowns','6000'),(2718,18,5,'Total of All Compounds','30000'),(2719,19,5,'-','-'),(2720,20,5,'-','-'),(2721,1,6,'Phenol & Derivatives','400'),(2722,2,6,'Naphthalene','30'),(2723,3,6,'Alcohol','200'),(2724,4,6,'Aliphatic Hydrocarbons','500'),(2725,5,6,'Unknowns, Others','2100'),(2726,6,6,'Total of All Compounds','5000'),(2727,7,6,'-','-'),(2728,8,6,'-','-'),(2729,9,6,'-','-'),(2730,10,6,'-','-'),(2731,11,6,'-','-'),(2732,12,6,'-','-'),(2733,13,6,'-','-'),(2734,14,6,'-','-'),(2735,15,6,'-','-'),(2736,16,6,'-','-'),(2737,17,6,'-','-'),(2738,18,6,'-','-'),(2739,19,6,'-','-'),(2740,20,6,'-','-'),(2741,1,7,'Hydrocarbons, Unknowns, Others','20'),(2742,2,7,'Total of All Compounds','20'),(2743,3,7,'-','-'),(2744,4,7,'-','-'),(2745,5,7,'-','-'),(2746,6,7,'-','-'),(2747,7,7,'-','-'),(2748,8,7,'-','-'),(2749,9,7,'-','-'),(2750,10,7,'-','-'),(2751,11,7,'-','-'),(2752,12,7,'-','-'),(2753,13,7,'-','-'),(2754,14,7,'-','-'),(2755,15,7,'-','-'),(2756,16,7,'-','-'),(2757,17,7,'-','-'),(2758,18,7,'-','-'),(2759,19,7,'-','-'),(2760,20,7,'-','-'),(2761,1,8,'Siloxanes','1000'),(2762,2,8,'Alcohols','2500'),(2763,3,8,'Aliphatic Hydrocarbons','35000'),(2764,4,8,'Aromatic Hydrocarbons','20000'),(2765,5,8,'Each Unspecified Compound','1500'),(2766,6,8,'Peak 241 Initiator','500'),(2767,7,8,'Total of All Compounds','40000'),(2768,8,8,'-','-'),(2769,9,8,'-','-'),(2770,10,8,'-','-'),(2771,11,8,'-','-'),(2772,12,8,'-','-'),(2773,13,8,'-','-'),(2774,14,8,'-','-'),(2775,15,8,'-','-'),(2776,16,8,'-','-'),(2777,17,8,'-','-'),(2778,18,8,'-','-'),(2779,19,8,'-','-'),(2780,20,8,'-','-'),(2781,1,9,'Siloxanes','460'),(2782,2,9,'Alcohols','2500'),(2783,3,9,'Aliphatic Hydrocarbons','10000'),(2784,4,9,'Aromatic Hydrocarbons','3000'),(2785,5,9,'Each Unspecified Compound','1500'),(2786,6,9,'Peak 241 Initiator','500'),(2787,7,9,'Total of All Compounds','12000'),(2788,8,9,'-','-'),(2789,9,9,'-','-'),(2790,10,9,'-','-'),(2791,11,9,'-','-'),(2792,12,9,'-','-'),(2793,13,9,'-','-'),(2794,14,9,'-','-'),(2795,15,9,'-','-'),(2796,16,9,'-','-'),(2797,17,9,'-','-'),(2798,18,9,'-','-'),(2799,19,9,'-','-'),(2800,20,9,'-','-'),(2801,1,10,'Siloxanes','800'),(2802,2,10,'Acetophenone','310'),(2803,3,10,'Alcohols','14240'),(2804,4,10,'Aliphatic Hydrocarbons','15350'),(2805,5,10,'Aromatic Hydrocarbons','2350'),(2806,6,10,'Acrylate Esters','730'),(2807,7,10,'Aldehydes','3470'),(2808,8,10,'Ketones','1340'),(2809,9,10,'Phenol & Derivatives','3180'),(2810,10,10,'CHAP','350'),(2811,11,10,'Unknowns & Others','3000'),(2812,12,10,'Total of All Compounds','33700'),(2813,13,10,'-','-'),(2814,14,10,'-','-'),(2815,15,10,'-','-'),(2816,16,10,'-','-'),(2817,17,10,'-','-'),(2818,18,10,'-','-'),(2819,19,10,'-','-'),(2820,20,10,'-','-'),(2821,1,11,'Siloxanes','60'),(2822,2,11,'Acetophenone','100'),(2823,3,11,'Alcohols','3810'),(2824,4,11,'Aliphatic Hydrocarbons','6760'),(2825,5,11,'Aromatic Hydrocarbons','2170'),(2826,6,11,'Acrylate Esters','320'),(2827,7,11,'Aldehyde','3470'),(2828,8,11,'Ketones','1340'),(2829,9,11,'Phenol & Derivatives','2390'),(2830,10,11,'CHAP','350'),(2831,11,11,'Unknowns & Others','3000'),(2832,12,11,'Total of All Compounds','15720'),(2833,13,11,'-','-'),(2834,14,11,'-','-'),(2835,15,11,'-','-'),(2836,16,11,'-','-'),(2837,17,11,'-','-'),(2838,18,11,'-','-'),(2839,19,11,'-','-'),(2840,20,11,'-','-'),(2841,1,12,'Toluene','30'),(2842,2,12,'Butanol','10'),(2843,3,12,'Siloxane','20'),(2844,4,12,'Octyl Acrylate','10'),(2845,5,12,'Dimethyl Benzoic Acid','10'),(2846,6,12,'Hydrocarbons, Unknown & Others','920'),(2847,7,12,'Total of All Compounds','1000'),(2848,8,12,'-','-'),(2849,9,12,'-','-'),(2850,10,12,'-','-'),(2851,11,12,'-','-'),(2852,12,12,'-','-'),(2853,13,12,'-','-'),(2854,14,12,'-','-'),(2855,15,12,'-','-'),(2856,16,12,'-','-'),(2857,17,12,'-','-'),(2858,18,12,'-','-'),(2859,19,12,'-','-'),(2860,20,12,'-','-'),(2861,1,13,'Siloxanes','1000'),(2862,2,13,'Alcohols','2500'),(2863,3,13,'Aliphatic Hydrocarbons','35000'),(2864,4,13,'Aromatic Hydrocarbons','20000'),(2865,5,13,'Each Unspecified Compound','1500'),(2866,6,13,'Peak 241 Initiator','500'),(2867,7,13,'Total of All Compounds','40000'),(2868,8,13,'-','-'),(2869,9,13,'-','-'),(2870,10,13,'-','-'),(2871,11,13,'-','-'),(2872,12,13,'-','-'),(2873,13,13,'-','-'),(2874,14,13,'-','-'),(2875,15,13,'-','-'),(2876,16,13,'-','-'),(2877,17,13,'-','-'),(2878,18,13,'-','-'),(2879,19,13,'-','-'),(2880,20,13,'-','-'),(2881,1,14,'Siloxanes','460'),(2882,2,14,'Alcohols','2500'),(2883,3,14,'Aliphatic Hydrocarbons','10000'),(2884,4,14,'Aromatic Hydrocarbons','3000'),(2885,5,14,'Each Unspecified Compound','1500'),(2886,6,14,'Peak 241 Initiator','500'),(2887,7,14,'Total of All Compounds','12000'),(2888,8,14,'-','-'),(2889,9,14,'-','-'),(2890,10,14,'-','-'),(2891,11,14,'-','-'),(2892,12,14,'-','-'),(2893,13,14,'-','-'),(2894,14,14,'-','-'),(2895,15,14,'-','-'),(2896,16,14,'-','-'),(2897,17,14,'-','-'),(2898,18,14,'-','-'),(2899,19,14,'-','-'),(2900,20,14,'-','-'),(2901,1,15,'Siloxanes','800'),(2902,2,15,'Acetophenone','310'),(2903,3,15,'Alcohols','14240'),(2904,4,15,'Aliphatic Hydrocarbons','15350'),(2905,5,15,'Aromatic Hydrocarbons','2350'),(2906,6,15,'Acrylate Esters','730'),(2907,7,15,'Aldehydes','3470'),(2908,8,15,'Ketones','1340'),(2909,9,15,'Phenol & Derivatives','3180'),(2910,10,15,'CHAP','350'),(2911,11,15,'Unknowns & Others','3000'),(2912,12,15,'Total of All Compounds','33700'),(2913,13,15,'-','-'),(2914,14,15,'-','-'),(2915,15,15,'-','-'),(2916,16,15,'-','-'),(2917,17,15,'-','-'),(2918,18,15,'-','-'),(2919,19,15,'-','-'),(2920,20,15,'-','-'),(2921,1,16,'Siloxanes','60'),(2922,2,16,'Acetophenone','100'),(2923,3,16,'Alcohols','3810'),(2924,4,16,'Aliphatic Hydrocarbons','6760'),(2925,5,16,'Aromatic Hydrocarbons','2170'),(2926,6,16,'Acrylate Esters','320'),(2927,7,16,'Aldehydes','3470'),(2928,8,16,'Ketones','1340'),(2929,9,16,'Phenol & Derivatives','2390'),(2930,10,16,'CHAP','350'),(2931,11,16,'Unknowns & Others','3000'),(2932,12,16,'Total of All Compounds','15720'),(2933,13,16,'-','-'),(2934,14,16,'-','-'),(2935,15,16,'-','-'),(2936,16,16,'-','-'),(2937,17,16,'-','-'),(2938,18,16,'-','-'),(2939,19,16,'-','-'),(2940,20,16,'-','-'),(2941,1,17,'BHT','40'),(2942,2,17,'Ethyl Hexanol','700'),(2943,3,17,'Hydrocarbons, Unknowns, Others','2700'),(2944,4,17,'Total of All Compounds','8000'),(2945,5,17,'-','-'),(2946,6,17,'-','-'),(2947,7,17,'-','-'),(2948,8,17,'-','-'),(2949,9,17,'-','-'),(2950,10,17,'-','-'),(2951,11,17,'-','-'),(2952,12,17,'-','-'),(2953,13,17,'-','-'),(2954,14,17,'-','-'),(2955,15,17,'-','-'),(2956,16,17,'-','-'),(2957,17,17,'-','-'),(2958,18,17,'-','-'),(2959,19,17,'-','-'),(2960,20,17,'-','-'),(2961,1,18,'BHT','5'),(2962,2,18,'Ionol 2','5'),(2963,3,18,'Hydrocarbons, Unknowns, Others','80'),(2964,4,18,'Total of All Compounds','80'),(2965,5,18,'-','-'),(2966,6,18,'-','-'),(2967,7,18,'-','-'),(2968,8,18,'-','-'),(2969,9,18,'-','-'),(2970,10,18,'-','-'),(2971,11,18,'-','-'),(2972,12,18,'-','-'),(2973,13,18,'-','-'),(2974,14,18,'-','-'),(2975,15,18,'-','-'),(2976,16,18,'-','-'),(2977,17,18,'-','-'),(2978,18,18,'-','-'),(2979,19,18,'-','-'),(2980,20,18,'-','-'),(2981,1,19,'MEK','1'),(2982,2,19,'Alcohol','10'),(2983,3,19,'Toluene','5'),(2984,4,19,'Phenols & Derivatives','3'),(2985,5,19,'Silicone','1'),(2986,6,19,'Styrene & Derivatives','1'),(2987,7,19,'Aldehydes','2'),(2988,8,19,'Hydrocarbons, Others, Unknowns','111'),(2989,9,19,'Total of All Compounds','136'),(2990,10,19,'-','-'),(2991,11,19,'-','-'),(2992,12,19,'-','-'),(2993,13,19,'-','-'),(2994,14,19,'-','-'),(2995,15,19,'-','-'),(2996,16,19,'-','-'),(2997,17,19,'-','-'),(2998,18,19,'-','-'),(2999,19,19,'-','-'),(3000,20,19,'-','-'),(3001,1,20,'MEK','1'),(3002,2,20,'Alcohol','10'),(3003,3,20,'Toluene','5'),(3004,4,20,'Phenols & Derivatives','3'),(3005,5,20,'Silicone','1'),(3006,6,20,'Styrene & Derivatives','1'),(3007,7,20,'Aldehydes','2'),(3008,8,20,'Hydrocarbons, Others, Unknowns','45'),(3009,9,20,'Total of All Compounds','55'),(3010,10,20,'-','-'),(3011,11,20,'-','-'),(3012,12,20,'-','-'),(3013,13,20,'-','-'),(3014,14,20,'-','-'),(3015,15,20,'-','-'),(3016,16,20,'-','-'),(3017,17,20,'-','-'),(3018,18,20,'-','-'),(3019,19,20,'-','-'),(3020,20,20,'-','-'),(3021,1,21,'MEK','1'),(3022,2,21,'Alcohol','5'),(3023,3,21,'Toluene','5'),(3024,4,21,'Phenols & Derivatives','3'),(3025,5,21,'Silicone','1'),(3026,6,21,'Styrene & Derivatives','1'),(3027,7,21,'Aldehydes','2'),(3028,8,21,'Hydrocarbons, Others, Unknowns','40'),(3029,9,21,'Total of All Compounds','50'),(3030,10,21,'-','-'),(3031,11,21,'-','-'),(3032,12,21,'-','-'),(3033,13,21,'-','-'),(3034,14,21,'-','-'),(3035,15,21,'-','-'),(3036,16,21,'-','-'),(3037,17,21,'-','-'),(3038,18,21,'-','-'),(3039,19,21,'-','-'),(3040,20,21,'-','-'),(3041,1,22,'Phenol & Derivatives','400'),(3042,2,22,'Naphthalene','30'),(3043,3,22,'Alcohol','200'),(3044,4,22,'Aliphatic Hydrocarbon','500'),(3045,5,22,'Unknowns, Others','2100'),(3046,6,22,'Total of All Compounds','5000'),(3047,7,22,'-','-'),(3048,8,22,'-','-'),(3049,9,22,'-','-'),(3050,10,22,'-','-'),(3051,11,22,'-','-'),(3052,12,22,'-','-'),(3053,13,22,'-','-'),(3054,14,22,'-','-'),(3055,15,22,'-','-'),(3056,16,22,'-','-'),(3057,17,22,'-','-'),(3058,18,22,'-','-'),(3059,19,22,'-','-'),(3060,20,22,'-','-'),(3061,1,23,'Hydrocarbons, Unknowns, Others','20'),(3062,2,23,'Total of All Compounds','20'),(3063,3,23,'-','-'),(3064,4,23,'-','-'),(3065,5,23,'-','-'),(3066,6,23,'-','-'),(3067,7,23,'-','-'),(3068,8,23,'-','-'),(3069,9,23,'-','-'),(3070,10,23,'-','-'),(3071,11,23,'-','-'),(3072,12,23,'-','-'),(3073,13,23,'-','-'),(3074,14,23,'-','-'),(3075,15,23,'-','-'),(3076,16,23,'-','-'),(3077,17,23,'-','-'),(3078,18,23,'-','-'),(3079,19,23,'-','-'),(3080,20,23,'-','-'),(3081,1,24,'Hydrocarbons, Unknowns, Others','336'),(3082,2,24,'Total of All Compounds','336'),(3083,3,24,'-','-'),(3084,4,24,'-','-'),(3085,5,24,'-','-'),(3086,6,24,'-','-'),(3087,7,24,'-','-'),(3088,8,24,'-','-'),(3089,9,24,'-','-'),(3090,10,24,'-','-'),(3091,11,24,'-','-'),(3092,12,24,'-','-'),(3093,13,24,'-','-'),(3094,14,24,'-','-'),(3095,15,24,'-','-'),(3096,16,24,'-','-'),(3097,17,24,'-','-'),(3098,18,24,'-','-'),(3099,19,24,'-','-'),(3100,20,24,'-','-'),(3101,1,25,'Hydrocarbons, Unknowns, Others','100'),(3102,2,25,'Total of All Compounds','100'),(3103,3,25,'-','-'),(3104,4,25,'-','-'),(3105,5,25,'-','-'),(3106,6,25,'-','-'),(3107,7,25,'-','-'),(3108,8,25,'-','-'),(3109,9,25,'-','-'),(3110,10,25,'-','-'),(3111,11,25,'-','-'),(3112,12,25,'-','-'),(3113,13,25,'-','-'),(3114,14,25,'-','-'),(3115,15,25,'-','-'),(3116,16,25,'-','-'),(3117,17,25,'-','-'),(3118,18,25,'-','-'),(3119,19,25,'-','-'),(3120,20,25,'-','-'),(3121,1,26,'Hydrocarbons, Unknowns, Others','40'),(3122,2,26,'Total of All Compounds','40'),(3123,3,26,'-','-'),(3124,4,26,'-','-'),(3125,5,26,'-','-'),(3126,6,26,'-','-'),(3127,7,26,'-','-'),(3128,8,26,'-','-'),(3129,9,26,'-','-'),(3130,10,26,'-','-'),(3131,11,26,'-','-'),(3132,12,26,'-','-'),(3133,13,26,'-','-'),(3134,14,26,'-','-'),(3135,15,26,'-','-'),(3136,16,26,'-','-'),(3137,17,26,'-','-'),(3138,18,26,'-','-'),(3139,19,26,'-','-'),(3140,20,26,'-','-'),(3141,1,27,'Hydrocarbons, Unknowns, Others','250'),(3142,2,27,'Total of All Compounds','250'),(3143,3,27,'-','-'),(3144,4,27,'-','-'),(3145,5,27,'-','-'),(3146,6,27,'-','-'),(3147,7,27,'-','-'),(3148,8,27,'-','-'),(3149,9,27,'-','-'),(3150,10,27,'-','-'),(3151,11,27,'-','-'),(3152,12,27,'-','-'),(3153,13,27,'-','-'),(3154,14,27,'-','-'),(3155,15,27,'-','-'),(3156,16,27,'-','-'),(3157,17,27,'-','-'),(3158,18,27,'-','-'),(3159,19,27,'-','-'),(3160,20,27,'-','-'),(3161,1,28,'Phenol & Derivatives','400'),(3162,2,28,'Naphthalene','30'),(3163,3,28,'Alcohol','200'),(3164,4,28,'Aliphatic Hydrocarbon','500'),(3165,5,28,'Unknowns, Others','2100'),(3166,6,28,'Total of All Compounds','5000'),(3167,7,28,'-','-'),(3168,8,28,'-','-'),(3169,9,28,'-','-'),(3170,10,28,'-','-'),(3171,11,28,'-','-'),(3172,12,28,'-','-'),(3173,13,28,'-','-'),(3174,14,28,'-','-'),(3175,15,28,'-','-'),(3176,16,28,'-','-'),(3177,17,28,'-','-'),(3178,18,28,'-','-'),(3179,19,28,'-','-'),(3180,20,28,'-','-'),(3181,1,29,'Hydrocarbons, Unknowns, Others','20'),(3182,2,29,'Total of All Compounds','20'),(3183,3,29,'-','-'),(3184,4,29,'-','-'),(3185,5,29,'-','-'),(3186,6,29,'-','-'),(3187,7,29,'-','-'),(3188,8,29,'-','-'),(3189,9,29,'-','-'),(3190,10,29,'-','-'),(3191,11,29,'-','-'),(3192,12,29,'-','-'),(3193,13,29,'-','-'),(3194,14,29,'-','-'),(3195,15,29,'-','-'),(3196,16,29,'-','-'),(3197,17,29,'-','-'),(3198,18,29,'-','-'),(3199,19,29,'-','-'),(3200,20,29,'-','-'),(3201,1,30,'Acetophenone & Derivatives','-'),(3202,2,30,'Alcohols','-'),(3203,3,30,'Aliphatic Hydrocarbon','-'),(3204,4,30,'Aliphatic & Aromatic Esters','-'),(3205,5,30,'Alkylacrylate','-'),(3206,6,30,'Amines & Amides','-'),(3207,7,30,'Aromatic Hydrocarbon','-'),(3208,8,30,'Benzoic Acid & Derivatives','-'),(3209,9,30,'Organic Acid','-'),(3210,10,30,'Organic Sulfur','-'),(3211,11,30,'BHT','-'),(3212,12,30,'Halogen Containing Hydrocarbons','-'),(3213,13,30,'Phenols & Derivatievs','-'),(3214,14,30,'Siloxanes','-'),(3215,15,30,'Styrene','-'),(3216,16,30,'Ethyl Hexanol','-'),(3217,17,30,'Others & Unknowns','-'),(3218,18,30,'Trace Gimbal Assembly (TGA) Circuit','NA'),(3219,19,30,'Head Gimbal Assembly','NA'),(3220,20,30,'Total of All Compounds','-'),(3221,1,31,'Acetophenone & Derivatives','-'),(3222,2,31,'Alcohols','-'),(3223,3,31,'Aliphatic Hydrocarbon','-'),(3224,4,31,'Aliphatic & Aromatic Esters','-'),(3225,5,31,'Alkylacrylate','-'),(3226,6,31,'Amines & Amides','-'),(3227,7,31,'Aromatic Hydrocarbon','-'),(3228,8,31,'Benzoic Acid & Derivatives','-'),(3229,9,31,'Organic Acid','-'),(3230,10,31,'Organic Sulfur','-'),(3231,11,31,'BHT','-'),(3232,12,31,'Halogen Containing Hydrocarbons','-'),(3233,13,31,'Phenols & Derivatievs','-'),(3234,14,31,'Siloxanes','-'),(3235,15,31,'Styrene','-'),(3236,16,31,'Ethyl Hexanol','-'),(3237,17,31,'Others & Unknowns','-'),(3238,18,31,'Trace Gimbal Assembly (TGA) Circuit','300'),(3239,19,31,'Head Gimbal Assembly','400'),(3240,20,31,'Total of All Compounds','-'),(3241,1,32,'Acetophenone & Derivatives','1400'),(3242,2,32,'Alcohols','4500'),(3243,3,32,'Aliphatic and Aromatic Esters','900'),(3244,4,32,'Alkylacrylate','900'),(3245,5,32,'Amines & Amides','700'),(3246,6,32,'Benzoic Acid Derivatives','100'),(3247,7,32,'Butylated Hydroxyl Toluene (BHT)','300'),(3248,8,32,'Ethyl Hexanol','3600'),(3249,9,32,'Halogen Containing Hydrocarbons','500'),(3250,10,32,'Aliphatic Hydrocarbon','5200'),(3251,11,32,'Aromatic Hydrocarbon','4000'),(3252,12,32,'Organic Acid','150'),(3253,13,32,'Organic Sulfur','600'),(3254,14,32,'Others & Unknowns','5600'),(3255,15,32,'Phenols & Derivatives','2000'),(3256,16,32,'Siloxanes','330'),(3257,17,32,'Styrene','1500'),(3258,18,32,'Total of All Compounds','15000'),(3259,19,32,'-','-'),(3260,20,32,'-','-'),(3261,1,33,'Acetophenone & Derivatives','800'),(3262,2,33,'Alcohols','2000'),(3263,3,33,'Aliphatic and Aromatic Esters','1100'),(3264,4,33,'Alkylacrylate','TBD'),(3265,5,33,'Amines & Amides','200'),(3266,6,33,'Benzoic Acid Derivatives','50'),(3267,7,33,'Butylated Hydroxyl Toluene (BHT)','300'),(3268,8,33,'Ethyl Hexanol','350'),(3269,9,33,'Halogen Containing Hydrocarbons','100'),(3270,10,33,'Aliphatic Hydrocarbon','2500'),(3271,11,33,'Aromatic Hydrocarbon','1000'),(3272,12,33,'Organic Acid','150'),(3273,13,33,'Organic Sulfur','130'),(3274,14,33,'Others & Unknowns','900'),(3275,15,33,'Phenols & Derivatives','250'),(3276,16,33,'Siloxanes','50'),(3277,17,33,'Styrene','100'),(3278,18,33,'Total of All Compounds','9000'),(3279,19,33,'-','-'),(3280,20,33,'-','-'),(3281,1,34,'Acetophenone & Derivatives','800'),(3282,2,34,'Alcohols','2000'),(3283,3,34,'Aliphatic and Aromatic Esters','1100'),(3284,4,34,'Alkylacrylate','2500'),(3285,5,34,'Amines & Amides','200'),(3286,6,34,'Benzoic Acid Derivatives','50'),(3287,7,34,'Butylated Hydroxyl Toluene (BHT)','300'),(3288,8,34,'Ethyl Hexanol','350'),(3289,9,34,'Halogen Containing Hydrocarbons','100'),(3290,10,34,'Aliphatic Hydrocarbon','2500'),(3291,11,34,'Aromatic Hydrocarbon','1000'),(3292,12,34,'Organic Acid','150'),(3293,13,34,'Organic Sulfur','130'),(3294,14,34,'Others & Unknowns','900'),(3295,15,34,'Phenols & Derivatives','250'),(3296,16,34,'Siloxanes','50'),(3297,17,34,'Styrene','100'),(3298,18,34,'Total of All Compounds','9000'),(3299,19,34,'-','-'),(3300,20,34,'-','-'),(3301,1,35,'Acetophenone & Derivatives','1700'),(3302,2,35,'Alcohols','5600'),(3303,3,35,'Aliphatic and Aromatic Esters','1100'),(3304,4,35,'Alkylacrylate','900'),(3305,5,35,'Amines & Amides','1180'),(3306,6,35,'Benzoic Acid Derivatives','250'),(3307,7,35,'Butylated Hydroxyl Toluene (BHT)','800'),(3308,8,35,'Ethyl Hexanol','5000'),(3309,9,35,'Halogen Containing Hydrocarbons','4600'),(3310,10,35,'Aliphatic Hydrocarbon','10000'),(3311,11,35,'Aromatic Hydrocarbon','6100'),(3312,12,35,'Organic Acid','400'),(3313,13,35,'Organic Sulfur','580'),(3314,14,35,'Others & Unknowns','5900'),(3315,15,35,'Phenols & Derivatives','2700'),(3316,16,35,'Siloxanes','450'),(3317,17,35,'Styrene','1400'),(3318,18,35,'Total of All Compounds','26000'),(3319,19,35,'-','-'),(3320,20,35,'-','-'),(3321,1,36,'Alcohols','NA'),(3322,2,36,'Amines & Amides','NA'),(3323,3,36,'Phenols & Derivatives','3000'),(3324,4,36,'Hydrocarbons, Unknowns, Others','5000'),(3325,5,36,'Each Unspecified Compound','NA'),(3326,6,36,'Total of All Compounds','8000'),(3327,7,36,'-','-'),(3328,8,36,'-','-'),(3329,9,36,'-','-'),(3330,10,36,'-','-'),(3331,11,36,'-','-'),(3332,12,36,'-','-'),(3333,13,36,'-','-'),(3334,14,36,'-','-'),(3335,15,36,'-','-'),(3336,16,36,'-','-'),(3337,17,36,'-','-'),(3338,18,36,'-','-'),(3339,19,36,'-','-'),(3340,20,36,'-','-'),(3341,1,37,'Alcohols','1500'),(3342,2,37,'Amines & Amides','1000'),(3343,3,37,'Phenols & Derivatives','10000'),(3344,4,37,'Hydrocarbons, Unknowns, Others','4000'),(3345,5,37,'Each Unspecified Compound','1000'),(3346,6,37,'Total of All Compounds','12000'),(3347,7,37,'-','-'),(3348,8,37,'-','-'),(3349,9,37,'-','-'),(3350,10,37,'-','-'),(3351,11,37,'-','-'),(3352,12,37,'-','-'),(3353,13,37,'-','-'),(3354,14,37,'-','-'),(3355,15,37,'-','-'),(3356,16,37,'-','-'),(3357,17,37,'-','-'),(3358,18,37,'-','-'),(3359,19,37,'-','-'),(3360,20,37,'-','-'),(3361,1,38,'Acetophenone & Derivatives','1100'),(3362,2,38,'Alcohols','1500'),(3363,3,38,'Aliphatic and Aromatic Esters','200'),(3364,4,38,'Alkylacrylate','300'),(3365,5,38,'Amines & Amides','1000'),(3366,6,38,'Benzoic Acid Derivatives','20'),(3367,7,38,'BHT','180'),(3368,8,38,'Halogen Containing Hydrocarbons','400'),(3369,9,38,'Aliphatic Hydrocarbon','7300'),(3370,10,38,'Aromatic Hydrocarbon','2500'),(3371,11,38,'Others & Unknowns','1200'),(3372,12,38,'Organic Acid','80'),(3373,13,38,'Organic Sulfur','90'),(3374,14,38,'Phenols & Derivatives','200'),(3375,15,38,'Siloxanes','150'),(3376,16,38,'Styrene','200'),(3377,17,38,'Total of All Compounds','10000'),(3378,18,38,'-','-'),(3379,19,38,'-','-'),(3380,20,38,'-','-'),(3381,1,39,'Acetophenone & Derivatives','400'),(3382,2,39,'Alcohols','500'),(3383,3,39,'Aliphatic and Aromatic Esters','90'),(3384,4,39,'Alkylacrylate','150'),(3385,5,39,'Amines & Amides','500'),(3386,6,39,'Benzoic Acid Derivatives','20'),(3387,7,39,'BHT','20'),(3388,8,39,'Halogen Containing Hydrocarbons','80'),(3389,9,39,'Aliphatic Hydrocarbon','2600'),(3390,10,39,'Aromatic Hydrocarbon','1700'),(3391,11,39,'Others & Unknowns','300'),(3392,12,39,'Organic Acid','30'),(3393,13,39,'Organic Sulfur','20'),(3394,14,39,'Phenols & Derivatives','60'),(3395,15,39,'Siloxane','60'),(3396,16,39,'Styrene','20'),(3397,17,39,'Total of All Compounds','4000'),(3398,18,39,'-','-'),(3399,19,39,'-','-'),(3400,20,39,'-','-'),(3401,1,40,'Total of All Compounds','175'),(3402,2,40,'-','-'),(3403,3,40,'-','-'),(3404,4,40,'-','-'),(3405,5,40,'-','-'),(3406,6,40,'-','-'),(3407,7,40,'-','-'),(3408,8,40,'-','-'),(3409,9,40,'-','-'),(3410,10,40,'-','-'),(3411,11,40,'-','-'),(3412,12,40,'-','-'),(3413,13,40,'-','-'),(3414,14,40,'-','-'),(3415,15,40,'-','-'),(3416,16,40,'-','-'),(3417,17,40,'-','-'),(3418,18,40,'-','-'),(3419,19,40,'-','-'),(3420,20,40,'-','-'),(3421,1,41,'Alcohol','50'),(3422,2,41,'Phenols & Derivatives','31'),(3423,3,41,'Organic Acids','3'),(3424,4,41,'Silicone','34'),(3425,5,41,'Amides','7'),(3426,6,41,'Phthalates','6'),(3427,7,41,'Aromatic Hydrocarbons','44'),(3428,8,41,'Aliphatic Hydrocarbons','47'),(3429,9,41,'Hydrocarbons, Others & Unknowns','51'),(3430,10,41,'Total of All Compounds','250'),(3431,11,41,'-','-'),(3432,12,41,'-','-'),(3433,13,41,'-','-'),(3434,14,41,'-','-'),(3435,15,41,'-','-'),(3436,16,41,'-','-'),(3437,17,41,'-','-'),(3438,18,41,'-','-'),(3439,19,41,'-','-'),(3440,20,41,'-','-'),(3441,1,42,'Total of All Compounds','178'),(3442,2,42,'-','-'),(3443,3,42,'-','-'),(3444,4,42,'-','-'),(3445,5,42,'-','-'),(3446,6,42,'-','-'),(3447,7,42,'-','-'),(3448,8,42,'-','-'),(3449,9,42,'-','-'),(3450,10,42,'-','-'),(3451,11,42,'-','-'),(3452,12,42,'-','-'),(3453,13,42,'-','-'),(3454,14,42,'-','-'),(3455,15,42,'-','-'),(3456,16,42,'-','-'),(3457,17,42,'-','-'),(3458,18,42,'-','-'),(3459,19,42,'-','-'),(3460,20,42,'-','-'),(3461,1,43,'Organo Sulfur','5'),(3462,2,43,'Phenols & Derivatives','10'),(3463,3,43,'Caprolactum','15'),(3464,4,43,'Hydrocarbons, Unknowns, Others','150'),(3465,5,43,'Total of All Compounds','150'),(3466,6,43,'-','-'),(3467,7,43,'-','-'),(3468,8,43,'-','-'),(3469,9,43,'-','-'),(3470,10,43,'-','-'),(3471,11,43,'-','-'),(3472,12,43,'-','-'),(3473,13,43,'-','-'),(3474,14,43,'-','-'),(3475,15,43,'-','-'),(3476,16,43,'-','-'),(3477,17,43,'-','-'),(3478,18,43,'-','-'),(3479,19,43,'-','-'),(3480,20,43,'-','-'),(3481,1,44,'Organo Sulfur','NA'),(3482,2,44,'Phenols & Derivatives','NA'),(3483,3,44,'Caprolactum','NA'),(3484,4,44,'Hydrocarbons, Unknowns, Others','NA'),(3485,5,44,'Total of All Compounds','15000'),(3486,6,44,'-','-'),(3487,7,44,'-','-'),(3488,8,44,'-','-'),(3489,9,44,'-','-'),(3490,10,44,'-','-'),(3491,11,44,'-','-'),(3492,12,44,'-','-'),(3493,13,44,'-','-'),(3494,14,44,'-','-'),(3495,15,44,'-','-'),(3496,16,44,'-','-'),(3497,17,44,'-','-'),(3498,18,44,'-','-'),(3499,19,44,'-','-'),(3500,20,44,'-','-'),(3501,1,45,'Organo Sulfur','NA'),(3502,2,45,'Phenols & Derivatives','NA'),(3503,3,45,'Caprolactum','NA'),(3504,4,45,'Hydrocarbons, Unknowns, Others','NA'),(3505,5,45,'Total of All Compounds','1000'),(3506,6,45,'-','-'),(3507,7,45,'-','-'),(3508,8,45,'-','-'),(3509,9,45,'-','-'),(3510,10,45,'-','-'),(3511,11,45,'-','-'),(3512,12,45,'-','-'),(3513,13,45,'-','-'),(3514,14,45,'-','-'),(3515,15,45,'-','-'),(3516,16,45,'-','-'),(3517,17,45,'-','-'),(3518,18,45,'-','-'),(3519,19,45,'-','-'),(3520,20,45,'-','-'),(3521,1,46,'Organo Sulfur','5'),(3522,2,46,'Phenols & Derivatives','10'),(3523,3,46,'Caprolactum','15'),(3524,4,46,'Hydrocarbons, Unknowns, Others','150'),(3525,5,46,'Total of All Compounds','150'),(3526,6,46,'-','-'),(3527,7,46,'-','-'),(3528,8,46,'-','-'),(3529,9,46,'-','-'),(3530,10,46,'-','-'),(3531,11,46,'-','-'),(3532,12,46,'-','-'),(3533,13,46,'-','-'),(3534,14,46,'-','-'),(3535,15,46,'-','-'),(3536,16,46,'-','-'),(3537,17,46,'-','-'),(3538,18,46,'-','-'),(3539,19,46,'-','-'),(3540,20,46,'-','-'),(3541,1,47,'Phenol & Derivatives','400'),(3542,2,47,'Naphthalene','30'),(3543,3,47,'Alcohol','200'),(3544,4,47,'Aliphatic Hydrocarbon','500'),(3545,5,47,'Unknowns, Others','2100'),(3546,6,47,'Total of All Compounds','5000'),(3547,7,47,'-','-'),(3548,8,47,'-','-'),(3549,9,47,'-','-'),(3550,10,47,'-','-'),(3551,11,47,'-','-'),(3552,12,47,'-','-'),(3553,13,47,'-','-'),(3554,14,47,'-','-'),(3555,15,47,'-','-'),(3556,16,47,'-','-'),(3557,17,47,'-','-'),(3558,18,47,'-','-'),(3559,19,47,'-','-'),(3560,20,47,'-','-'),(3561,1,48,'Hydrocarbons, Unknowns, Others','20'),(3562,2,48,'Total of All Compounds','20'),(3563,3,48,'-','-'),(3564,4,48,'-','-'),(3565,5,48,'-','-'),(3566,6,48,'-','-'),(3567,7,48,'-','-'),(3568,8,48,'-','-'),(3569,9,48,'-','-'),(3570,10,48,'-','-'),(3571,11,48,'-','-'),(3572,12,48,'-','-'),(3573,13,48,'-','-'),(3574,14,48,'-','-'),(3575,15,48,'-','-'),(3576,16,48,'-','-'),(3577,17,48,'-','-'),(3578,18,48,'-','-'),(3579,19,48,'-','-'),(3580,20,48,'-','-'),(3581,1,49,'Alcohol','1500'),(3582,2,49,'Toluene','90'),(3583,3,49,'Siloxane','300'),(3584,4,49,'Octyl Acrylate','700'),(3585,5,49,'Dimethyl Benzoic Acid','200'),(3586,6,49,'Hydrocarbon & Others','2500'),(3587,7,49,'Total of All Compounds','3310'),(3588,8,49,'-','-'),(3589,9,49,'-','-'),(3590,10,49,'-','-'),(3591,11,49,'-','-'),(3592,12,49,'-','-'),(3593,13,49,'-','-'),(3594,14,49,'-','-'),(3595,15,49,'-','-'),(3596,16,49,'-','-'),(3597,17,49,'-','-'),(3598,18,49,'-','-'),(3599,19,49,'-','-'),(3600,20,49,'-','-'),(3601,1,50,'Acrylic Acid','300'),(3602,2,50,'Benzoic Acid','600'),(3603,3,50,'Methacrylic Acid','300'),(3604,4,50,'2-Ethylhexanoic Acid','100'),(3605,5,50,'Total Other Organic & Aromatic Acids','600'),(3606,6,50,'Alcohols','1500'),(3607,7,50,'Amines','300'),(3608,8,50,'Amides','300'),(3609,9,50,'BHT','600'),(3610,10,50,'Alkyl Acrylate','300'),(3611,11,50,'Phenols & Derivatives not including BHT','1500'),(3612,12,50,'Phthalates','600'),(3613,13,50,'Aliphatic & Aromatic Esters not including Phthalates','1500'),(3614,14,50,'Acetophenone & Derivatives','300'),(3615,15,50,'Siloxanes','300'),(3616,16,50,'Each Unspecified Compound','600'),(3617,17,50,'Total Hydrocarbons','7500'),(3618,18,50,'Total Outgassing','12000'),(3619,19,50,'-','-'),(3620,20,50,'-','-'),(3621,1,51,'Toluene','30'),(3622,2,51,'Butanol','10'),(3623,3,51,'Siloxane','20'),(3624,4,51,'Octyl Acrylate','10'),(3625,5,51,'Dimethyl Benzoic Acid','10'),(3626,6,51,'Hydrocarbons, Unknown & Others','920'),(3627,7,51,'Total of All Compounds','1000'),(3628,8,51,'-','-'),(3629,9,51,'-','-'),(3630,10,51,'-','-'),(3631,11,51,'-','-'),(3632,12,51,'-','-'),(3633,13,51,'-','-'),(3634,14,51,'-','-'),(3635,15,51,'-','-'),(3636,16,51,'-','-'),(3637,17,51,'-','-'),(3638,18,51,'-','-'),(3639,19,51,'-','-'),(3640,20,51,'-','-'),(3641,1,52,'Siloxanes','1000'),(3642,2,52,'Alcohols','2500'),(3643,3,52,'Aliphatic Hydrocarbons','35000'),(3644,4,52,'Aromatic Hydrocarbons','20000'),(3645,5,52,'Each Unspecified Compound','1500'),(3646,6,52,'Peak 241 Initiator','500'),(3647,7,52,'Total of All Compounds','40000'),(3648,8,52,'-','-'),(3649,9,52,'-','-'),(3650,10,52,'-','-'),(3651,11,52,'-','-'),(3652,12,52,'-','-'),(3653,13,52,'-','-'),(3654,14,52,'-','-'),(3655,15,52,'-','-'),(3656,16,52,'-','-'),(3657,17,52,'-','-'),(3658,18,52,'-','-'),(3659,19,52,'-','-'),(3660,20,52,'-','-'),(3661,1,53,'Siloxanes','460'),(3662,2,53,'Alcohols','2500'),(3663,3,53,'Aliphatic Hydrocarbons','10000'),(3664,4,53,'Aromatic Hydrocarbons','3000'),(3665,5,53,'Each Unspecified Compound','1500'),(3666,6,53,'Peak 241 Initiator','500'),(3667,7,53,'Total of All Compounds','12000'),(3668,8,53,'-','-'),(3669,9,53,'-','-'),(3670,10,53,'-','-'),(3671,11,53,'-','-'),(3672,12,53,'-','-'),(3673,13,53,'-','-'),(3674,14,53,'-','-'),(3675,15,53,'-','-'),(3676,16,53,'-','-'),(3677,17,53,'-','-'),(3678,18,53,'-','-'),(3679,19,53,'-','-'),(3680,20,53,'-','-'),(3681,1,54,'Siloxanes','800'),(3682,2,54,'Acetophenone','310'),(3683,3,54,'Alcohols','14240'),(3684,4,54,'Aliphatic Hydrocarbons','15350'),(3685,5,54,'Aromatic Hydrocarbons','2350'),(3686,6,54,'Acrylate Esters','730'),(3687,7,54,'Aldehydes','3470'),(3688,8,54,'Ketones','1340'),(3689,9,54,'Phenol & Derivatives','3180'),(3690,10,54,'CHAP','350'),(3691,11,54,'Unknowns & Others','3000'),(3692,12,54,'Total of All Compounds','33700'),(3693,13,54,'-','-'),(3694,14,54,'-','-'),(3695,15,54,'-','-'),(3696,16,54,'-','-'),(3697,17,54,'-','-'),(3698,18,54,'-','-'),(3699,19,54,'-','-'),(3700,20,54,'-','-'),(3701,1,55,'Siloxanes','60'),(3702,2,55,'Acetophenone','100'),(3703,3,55,'Alcohols','3810'),(3704,4,55,'Aliphatic Hydrocarbons','6760'),(3705,5,55,'Aromatic Hydrocarbons','2170'),(3706,6,55,'Acrylate Esters','320'),(3707,7,55,'Aldehydes','3470'),(3708,8,55,'Ketones','1340'),(3709,9,55,'Phenol & Derivatives','2390'),(3710,10,55,'CHAP','350'),(3711,11,55,'Unknowns & Others','3000'),(3712,12,55,'Total of All Compounds','15720'),(3713,13,55,'-','-'),(3714,14,55,'-','-'),(3715,15,55,'-','-'),(3716,16,55,'-','-'),(3717,17,55,'-','-'),(3718,18,55,'-','-'),(3719,19,55,'-','-'),(3720,20,55,'-','-'),(3721,1,56,'Siloxanes','1000'),(3722,2,56,'Alcohols','2500'),(3723,3,56,'Aliphatic Hydrocarbons','35000'),(3724,4,56,'Aromatic Hydrocarbons','20000'),(3725,5,56,'Each Unspecified Compound','1500'),(3726,6,56,'Peak 241 Initiator','500'),(3727,7,56,'Total of All Compounds','40000'),(3728,8,56,'-','-'),(3729,9,56,'-','-'),(3730,10,56,'-','-'),(3731,11,56,'-','-'),(3732,12,56,'-','-'),(3733,13,56,'-','-'),(3734,14,56,'-','-'),(3735,15,56,'-','-'),(3736,16,56,'-','-'),(3737,17,56,'-','-'),(3738,18,56,'-','-'),(3739,19,56,'-','-'),(3740,20,56,'-','-'),(3741,1,57,'Siloxanes','460'),(3742,2,57,'Alcohols','2500'),(3743,3,57,'Aliphatic Hydrocarbons','10000'),(3744,4,57,'Aromatic Hydrocarbons','3000'),(3745,5,57,'Each Unspecified Compound','1500'),(3746,6,57,'Peak 241 Initiator','500'),(3747,7,57,'Total of All Compounds','12000'),(3748,8,57,'-','-'),(3749,9,57,'-','-'),(3750,10,57,'-','-'),(3751,11,57,'-','-'),(3752,12,57,'-','-'),(3753,13,57,'-','-'),(3754,14,57,'-','-'),(3755,15,57,'-','-'),(3756,16,57,'-','-'),(3757,17,57,'-','-'),(3758,18,57,'-','-'),(3759,19,57,'-','-'),(3760,20,57,'-','-'),(3761,1,58,'Siloxanes','800'),(3762,2,58,'Acetophenone','310'),(3763,3,58,'Alcohols','14240'),(3764,4,58,'Aliphatic Hydrocarbons','15350'),(3765,5,58,'Aromatic Hydrocarbons','2350'),(3766,6,58,'Acrylate Esters','730'),(3767,7,58,'Aldehydes','3470'),(3768,8,58,'Ketones','1340'),(3769,9,58,'Phenol & Derivatives','3180'),(3770,10,58,'CHAP','350'),(3771,11,58,'Unknowns & Others','3000'),(3772,12,58,'Total of All Compounds','33700'),(3773,13,58,'-','-'),(3774,14,58,'-','-'),(3775,15,58,'-','-'),(3776,16,58,'-','-'),(3777,17,58,'-','-'),(3778,18,58,'-','-'),(3779,19,58,'-','-'),(3780,20,58,'-','-'),(3781,1,59,'Siloxanes','60'),(3782,2,59,'Acetophenone','100'),(3783,3,59,'Alcohols','3810'),(3784,4,59,'Aliphatic Hydrocarbons','6760'),(3785,5,59,'Aromatic Hydrocarbons','2170'),(3786,6,59,'Acrylate Esters','320'),(3787,7,59,'Aldehydes','3470'),(3788,8,59,'Ketones','1340'),(3789,9,59,'Phenol & Derivatives','2390'),(3790,10,59,'CHAP','350'),(3791,11,59,'Unknowns & Others','3000'),(3792,12,59,'Total of All Compounds','15720'),(3793,13,59,'-','-'),(3794,14,59,'-','-'),(3795,15,59,'-','-'),(3796,16,59,'-','-'),(3797,17,59,'-','-'),(3798,18,59,'-','-'),(3799,19,59,'-','-'),(3800,20,59,'-','-'),(3801,1,60,'Siloxanes','1000'),(3802,2,60,'Alcohols','2500'),(3803,3,60,'Aliphatic Hydrocarbons','35000'),(3804,4,60,'Aromatic Hydrocarbons','20000'),(3805,5,60,'Each Unspecified Compound','1500'),(3806,6,60,'Peak 241 Initiator','500'),(3807,7,60,'Total of All Compounds','40000'),(3808,8,60,'-','-'),(3809,9,60,'-','-'),(3810,10,60,'-','-'),(3811,11,60,'-','-'),(3812,12,60,'-','-'),(3813,13,60,'-','-'),(3814,14,60,'-','-'),(3815,15,60,'-','-'),(3816,16,60,'-','-'),(3817,17,60,'-','-'),(3818,18,60,'-','-'),(3819,19,60,'-','-'),(3820,20,60,'-','-'),(3821,1,61,'Siloxanes','460'),(3822,2,61,'Alcohols','2500'),(3823,3,61,'Aliphatic Hydrocarbons','10000'),(3824,4,61,'Aromatic Hydrocarbons','3000'),(3825,5,61,'Each Unspecified Compound','1500'),(3826,6,61,'Peak 241 Initiator','500'),(3827,7,61,'Total of All Compounds','12000'),(3828,8,61,'-','-'),(3829,9,61,'-','-'),(3830,10,61,'-','-'),(3831,11,61,'-','-'),(3832,12,61,'-','-'),(3833,13,61,'-','-'),(3834,14,61,'-','-'),(3835,15,61,'-','-'),(3836,16,61,'-','-'),(3837,17,61,'-','-'),(3838,18,61,'-','-'),(3839,19,61,'-','-'),(3840,20,61,'-','-'),(3841,1,62,'Siloxanes','800'),(3842,2,62,'Acetophenone','310'),(3843,3,62,'Alcohols','14240'),(3844,4,62,'Aliphatic Hydrocarbons','15350'),(3845,5,62,'Aromatic Hydrocarbons','2350'),(3846,6,62,'Acrylate Esters','730'),(3847,7,62,'Aldehydes','3470'),(3848,8,62,'Ketones','1340'),(3849,9,62,'Phenol & Derivatives','3180'),(3850,10,62,'CHAP','350'),(3851,11,62,'Unknowns & Others','3000'),(3852,12,62,'Total of All Compounds','33700'),(3853,13,62,'-','-'),(3854,14,62,'-','-'),(3855,15,62,'-','-'),(3856,16,62,'-','-'),(3857,17,62,'-','-'),(3858,18,62,'-','-'),(3859,19,62,'-','-'),(3860,20,62,'-','-'),(3861,1,63,'Aromatic HC (e.g. Benzene)','60'),(3862,2,63,'2-Ethyl Hexanol','300'),(3863,3,63,'Benzoic Acid','100'),(3864,4,63,'Octyl Acrylate','200'),(3865,5,63,'Phenyl Benzoate','230'),(3866,6,63,'Hydrocarbons (except aromatic HC), Unknown, Others','1900'),(3867,7,63,'Total of All Compounds','2100'),(3868,8,63,'-','-'),(3869,9,63,'-','-'),(3870,10,63,'-','-'),(3871,11,63,'-','-'),(3872,12,63,'-','-'),(3873,13,63,'-','-'),(3874,14,63,'-','-'),(3875,15,63,'-','-'),(3876,16,63,'-','-'),(3877,17,63,'-','-'),(3878,18,63,'-','-'),(3879,19,63,'-','-'),(3880,20,63,'-','-'),(3881,1,64,'Organophosphorus','370'),(3882,2,64,'Organosulfur','20'),(3883,3,64,'Ethylhexanoic Acid','1350'),(3884,4,64,'Amines ','1550'),(3885,5,64,'Phenol & Derivatives','230'),(3886,6,64,'Butylated Hydroxy Toluene (BHT)','80'),(3887,7,64,'Siloxanes','120'),(3888,8,64,'Total Alcohol','150'),(3889,9,64,'Silane Containing Compound (ms 119, 163)','8'),(3890,10,64,'Hydrocarbons, Others & Unknown','1800'),(3891,11,64,'Total of All Compounds','3800'),(3892,12,64,'-','-'),(3893,13,64,'-','-'),(3894,14,64,'-','-'),(3895,15,64,'-','-'),(3896,16,64,'-','-'),(3897,17,64,'-','-'),(3898,18,64,'-','-'),(3899,19,64,'-','-'),(3900,20,64,'-','-'),(3901,1,65,'Organophosphorus','20'),(3902,2,65,'Organosulfur','20'),(3903,3,65,'Ethylhexanoic Acid','20'),(3904,4,65,'Amines ','18000'),(3905,5,65,'Phenol & Derivatives','80'),(3906,6,65,'Butylated Hydroxy Toluene (BHT)','3800'),(3907,7,65,'Siloxanes','50'),(3908,8,65,'Total Alcohol','400'),(3909,9,65,'Silane Containing Compound (ms 119, 163)','330'),(3910,10,65,'Hydrocarbons, Others & Unknown','5500'),(3911,11,65,'Total of All Compounds','26000'),(3912,12,65,'',''),(3913,13,65,'-','-'),(3914,14,65,'-','-'),(3915,15,65,'-','-'),(3916,16,65,'-','-'),(3917,17,65,'-','-'),(3918,18,65,'-','-'),(3919,19,65,'-','-'),(3920,20,65,'-','-');
/*!40000 ALTER TABLE `template_16_detail_spec` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_29_coverpage`
--

DROP TABLE IF EXISTS `template_29_coverpage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_29_coverpage` (
  `ID` int(11) NOT NULL auto_increment,
  `sample_id` int(11) default NULL,
  `specification_id` int(11) default NULL,
  `b9` decimal(10,5) default NULL,
  `b10` decimal(10,5) default NULL,
  `b11` decimal(10,5) default NULL,
  `b14` decimal(10,5) default NULL,
  `b15` decimal(10,5) default NULL,
  `b16` decimal(10,5) default NULL,
  `b17` decimal(10,5) default NULL,
  `b18` decimal(10,5) default NULL,
  `b19` decimal(10,5) default NULL,
  `b20` decimal(10,5) default NULL,
  `b23` decimal(10,5) default NULL,
  `b24` decimal(10,5) default NULL,
  `b25` decimal(10,5) default NULL,
  `b26` decimal(10,5) default NULL,
  `b27` decimal(10,5) default NULL,
  `b28` decimal(10,5) default NULL,
  `c14` decimal(10,5) default NULL,
  `c15` decimal(10,5) default NULL,
  `c16` decimal(10,5) default NULL,
  `c17` decimal(10,5) default NULL,
  `c18` decimal(10,5) default NULL,
  `c19` decimal(10,5) default NULL,
  `c20` decimal(10,5) default NULL,
  `c23` decimal(10,5) default NULL,
  `c25` decimal(10,5) default NULL,
  `c26` decimal(10,5) default NULL,
  `c27` decimal(10,5) default NULL,
  `c28` decimal(10,5) default NULL,
  `c24` decimal(10,5) default NULL,
  `result_c25` decimal(10,5) default NULL,
  `result_c26` decimal(10,5) default NULL,
  `result_c27` decimal(10,5) default NULL,
  `result_c28` decimal(10,5) default NULL,
  `result_c29` decimal(10,5) default NULL,
  `result_c30` decimal(10,5) default NULL,
  `result_c31` decimal(10,5) default NULL,
  `result_c32` decimal(10,5) default NULL,
  `result_c34` decimal(10,5) default NULL,
  `result_c35` decimal(10,5) default NULL,
  `result_c36` decimal(10,5) default NULL,
  `result_c37` decimal(10,5) default NULL,
  `result_c38` decimal(10,5) default NULL,
  `result_c39` decimal(10,5) default NULL,
  `result_c40` decimal(10,5) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `fk_sample_id_idx` (`sample_id`),
  KEY `fk_specification_id_idx` (`specification_id`),
  CONSTRAINT `fk_sample_id` FOREIGN KEY (`sample_id`) REFERENCES `job_sample` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_specification_id` FOREIGN KEY (`specification_id`) REFERENCES `template_29_specification` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_29_coverpage`
--

LOCK TABLES `template_29_coverpage` WRITE;
/*!40000 ALTER TABLE `template_29_coverpage` DISABLE KEYS */;
/*!40000 ALTER TABLE `template_29_coverpage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_29_specification`
--

DROP TABLE IF EXISTS `template_29_specification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template_29_specification` (
  `ID` int(11) NOT NULL,
  `A` varchar(150) default NULL,
  `B` varchar(45) default NULL,
  `C` varchar(45) default NULL,
  `D` varchar(45) default NULL,
  `E` varchar(45) default NULL,
  `F` varchar(45) default NULL,
  `G` varchar(45) default NULL,
  `H` varchar(45) default NULL,
  `I` varchar(45) default NULL,
  `J` varchar(45) default NULL,
  `K` varchar(45) default NULL,
  `L` varchar(45) default NULL,
  `M` varchar(45) default NULL,
  `N` varchar(45) default NULL,
  `O` varchar(45) default NULL,
  `P` varchar(45) default NULL,
  `Q` varchar(45) default NULL,
  `R` varchar(45) default NULL,
  `S` varchar(45) default NULL,
  `T` varchar(45) default NULL,
  `U` varchar(45) default NULL,
  `V` varchar(45) default NULL,
  PRIMARY KEY  (`ID`)
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
-- Table structure for table `user_login`
--

DROP TABLE IF EXISTS `user_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_login` (
  `id` int(11) NOT NULL auto_increment,
  `role_id` int(11) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL default '1234',
  `latest_login` datetime default NULL,
  `email` varchar(50) NOT NULL,
  `create_by` int(11) default NULL,
  `create_date` datetime default NULL,
  `status` varchar(1) NOT NULL default 'A',
  `responsible_test` varchar(45) default NULL,
  `is_force_change_password` bit(1) default '\0',
  `personal_title` int(11) default NULL,
  `first_name` varchar(45) default NULL,
  `last_name` varchar(45) default NULL,
  `mobile_phone` varchar(45) default NULL,
  PRIMARY KEY  (`id`),
  KEY `fk_role_id_idx` (`role_id`),
  KEY `fk_title_id_2_idx` (`personal_title`),
  CONSTRAINT `fk_role_id` FOREIGN KEY (`role_id`) REFERENCES `m_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_id_2` FOREIGN KEY (`personal_title`) REFERENCES `m_title` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_login`
--

LOCK TABLES `user_login` WRITE;
/*!40000 ALTER TABLE `user_login` DISABLE KEYS */;
INSERT INTO `user_login` VALUES (1,1,'admin','827CCB0EEA8A706C4C34A16891F84E7B','2015-03-05 17:10:31','pawit1357@hotmail.com',0,'2015-02-13 15:43:58','A',NULL,'\0',1,'Pawit','Sae-eaung',NULL),(2,2,'pikul.totassa','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:34:25','pikul.totassa@alsglobal.com',0,'2015-02-14 17:34:25','A',NULL,'\0',NULL,'pikul','totassa',NULL),(3,3,'chanchira.chanprasert','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:39:30','chanchira.chanprasert@alsglobal.com',0,'2015-02-14 17:39:30','A','GCMS','',NULL,'chanchira','chanprasert',NULL),(4,3,'lampoon.srihnongwa','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:40:26','lampoon.srihnongwa@alsglobal.com',0,'2015-02-14 17:40:26','A','DHS,GCMS','',NULL,'lampoon','srihnongwa',NULL),(5,3,'nunta.thotho','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:41:08','nunta.thotho@alsglobal.com',0,'2015-02-14 17:41:08','A','LPC','',NULL,'nunta','thotho',NULL),(6,3,'onanong.pithakpong','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:41:50','onanong.pithakpong@alsglobal.com',0,'2015-02-14 17:41:50','A','IC','',NULL,'onanong','pithakpong',NULL),(7,3,'pavinee.phawaphotano','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:42:43','pavinee.phawaphotano@alsglobal.com',0,'2015-02-14 17:42:43','A','MESA','',NULL,'pavinee','phawaphotano',NULL),(8,3,'rattana.tammasin','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:43:24','rattana.tammasin@alsglobal.com',0,'2015-02-14 17:43:24','A','DHS','\0',NULL,'rattana','tammasin',NULL),(9,3,'sayan.songka','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:44:27','sayan.songka@alsglobal.com',0,'2015-02-14 17:44:27','A','FTIR','',NULL,'sayan','songka',NULL),(10,3,'jakkrit.chairasamee','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:45:15','jakkrit.chairasamee@alsglobal.com',0,'2015-02-14 17:45:15','A','GCMS','\0',NULL,'jakkrit','chairasamee',NULL),(11,3,'wattana.trachoo','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:46:07','wattana.trachoo@alsglobal.com',0,'2015-02-14 17:46:07','A','MESA','',NULL,'wattana','trachoo',NULL),(12,3,'yuwadee.kenming','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:47:06','yuwadee.kenming@alsglobal.com',0,'2015-02-14 17:47:06','A','DHS,FTIR','',NULL,'yuwadee','kenming',NULL),(13,3,'thanyaporn.vongyara','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:47:56','thanyaporn.vongyara@alsglobal.com',0,'2015-02-14 17:47:56','A','GCMS','',NULL,'thanyaporn','vongyara',NULL),(14,4,'udomlak.pattanajitpitak','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:49:08','udomlak.pattanajitpitak@alsglobal.com',0,'2015-02-14 17:49:08','A',NULL,'\0',NULL,'udomlak','pattanajitpitak',NULL),(15,4,'rossukhon.khongphuwet','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:50:06','rossukhon.khongphuwet@alsglobal.com',0,'2015-02-14 17:50:06','A',NULL,'',NULL,'rossukhon','khongphuwet',NULL),(16,5,'pornpan.wingwon','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:50:59','pornpan.wingwon@alsglobal.com',0,'2015-02-14 17:50:59','A',NULL,'',NULL,'pornpan','wingwon',NULL),(17,5,'orapin.maliwan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:51:35','orapin.maliwan@alsglobal.com',0,'2015-02-14 17:51:35','A',NULL,'',NULL,'orapin','maliwan',NULL),(18,5,'sukanya.dawan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:52:16','sukanya.dawan@alsglobal.com',0,'2015-02-14 17:52:16','A',NULL,'',NULL,'sukanya','dawan',NULL),(19,6,'Warunee.Maneesuwan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:52:53','Warunee.Maneesuwan@alsglobal.com',0,'2015-02-14 17:52:53','A',NULL,'',NULL,'Warunee','Maneesuwan',NULL),(20,6,'chayan.jutaphan','81DC9BDB52D04DC20036DBD8313ED055','2015-02-14 17:53:29','chayan.jutaphan@alsglobal.com',0,'2015-02-14 17:53:29','A',NULL,'',NULL,'chayan','jutaphan',NULL),(24,7,'acc01','81DC9BDB52D04DC20036DBD8313ED055','2015-03-05 14:45:30','acc01@alsglobal.com',1,'2015-03-05 13:20:22','A',NULL,'',1,'Pawit','แซ่อึ้ง_edit',NULL);
/*!40000 ALTER TABLE `user_login` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-03-08 23:38:16
