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
  `contract_person_id` int(11) default NULL,
  `customer_id` int(11) default NULL,
  `specification_id` int(11) default NULL,
  `type_of_test_id` int(11) default NULL,
  `sub_type_of_test_id` int(11) default NULL,
  `customer_ref_no` varchar(45) default NULL,
  `customer_po_ref` varchar(45) default NULL,
  `s_pore_ref_no` varchar(45) default NULL,
  `company_name_to_state_in_report` varchar(45) default NULL,
  `job_number` varchar(20) default NULL,
  `invoice` varchar(45) default NULL,
  `date_of_receive` date default NULL,
  `sample_diposition` varchar(1) default NULL COMMENT '0=Discard,1=Return All',
  `type_of_test_other` varchar(45) default NULL,
  `sub_type_of_test_other` varchar(45) default NULL,
  `no_of_report` int(11) default NULL,
  `uncertainty` varchar(1) default NULL,
  `work_type` varchar(1) default NULL,
  `custody_seal_intact_status` varchar(1) default NULL,
  `number_of_sample_recived_status` varchar(1) default NULL,
  `operator_workload_status` varchar(1) default NULL,
  `instrument_test_method_status` varchar(1) default NULL,
  `create_by` int(11) default NULL,
  `update_by` int(11) default NULL,
  `create_date` date default NULL,
  `update_date` date default NULL,
  `duedate` date default NULL,
  `job_type` varchar(1) default NULL COMMENT '1=,2=,3=',
  `specification_other` varchar(45) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_info`
--

LOCK TABLES `job_info` WRITE;
/*!40000 ALTER TABLE `job_info` DISABLE KEYS */;
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
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_reiew_requistion`
--

LOCK TABLES `job_reiew_requistion` WRITE;
/*!40000 ALTER TABLE `job_reiew_requistion` DISABLE KEYS */;
/*!40000 ALTER TABLE `job_reiew_requistion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_sample`
--

DROP TABLE IF EXISTS `job_sample`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `job_sample` (
  `ID` int(11) NOT NULL auto_increment,
  `job_id` int(11) default NULL,
  `sn` varchar(45) default NULL,
  `description` varchar(45) default NULL,
  `model` varchar(45) default NULL,
  `surface_area` varchar(45) default NULL,
  `remarks` varchar(45) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_sample`
--

LOCK TABLES `job_sample` WRITE;
/*!40000 ALTER TABLE `job_sample` DISABLE KEYS */;
/*!40000 ALTER TABLE `job_sample` ENABLE KEYS */;
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
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=179 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_customer`
--

LOCK TABLES `m_customer` WRITE;
/*!40000 ALTER TABLE `m_customer` DISABLE KEYS */;
INSERT INTO `m_customer` VALUES (4,'','3M Thailand Limited','150 Soi Chalongkrung 31','Ladkrabang','+66 85 441 9193','achakkaew@mmm.com','','Ladkrabang','2287','Customer Technical Center','Bangkok','10520','+66 2326 0025',0,'2015-01-27',0,'2015-01-27'),(3,'CDG3MTH001','3M Thailand Limited','159 Asoke-Montri Road','Klongtoey-Nua','+66 81 170 7109','ctangtreerat@mmm.com','Head Office','Wattana','2240','Customer Technical Center','Bangkok','10110','+66 2326 0025',0,'2015-01-27',0,'2015-01-27'),(5,'CDGAAFIN01','Aaf International (Thailand) Co.,Ltd.','100 Moo 4 Bangkaew','Bangplee-Yai','','witoon.pon@aafthailand.com','Head Office','Bangplee','','Sales','Samutprakarn','10540','+66 2738 7788',0,'2015-01-27',0,'2015-01-27'),(6,'CDGAAPIC01','Aapico Hitech Pasts Co., Ltd.','99/2 Moo 1 Hi-tech Industrial Estate','Ban-Lane','+66 81 321 4264','Atthakorn.t@aapico.com','Head Office','Bang Pa-In','','QA Engineer','Ayutthaya','13160','+66 3535 0880',0,'2015-01-27',0,'2015-01-27'),(7,'CDGADAMP01','Adampak (Thailand) Limited','700/686 Moo 1 Amata Nakorn Industrial Estate','Phanthong','','waraporn@adampak.co.th','Head Office','Phanthong','144','QA Engineer','Chonburi','20160','+66 3807 9850',0,'2015-01-27',0,'2015-01-27'),(8,'CDGADVAN01','Advance Packaging Co., Ltd.','57 Moo 9 Rojana Industrial Park','Thanu','','qa@advance-pack.co.th','Head Office','U-Thai','','','Ayutthaya','13210','+66 3533 0568',0,'2015-01-27',0,'2015-01-27'),(9,'CDGAGRUT01','Agru Technology (Thailand) Co., Ltd.','420/51 Kanchanapisek Road','Dokmai','+66 83 135 8998','Pisit@agru.co.th','Head Office','Pravet ','','Sales Engineer Manager','Bangkok','10250','+66 2728 4477',0,'2015-01-27',0,'2015-01-27'),(10,'CDGALLEN01','Allen Enineering Co.,Ltd. ','226 Moo 6 Rama 2 Road','Smaedam','+66 86 688 8341','ittthikorn@allenthai.com','Head Office','Bangkhuntien','','Sales Engineer','Bangkok','10150','',0,'2015-01-27',0,'2015-01-27'),(11,'CDGALMON01','Almond (Thailand) Limited ','31 Moo 14, Bangchan Industrial Estate Seri Thai Road','Minburi','+66 86 565 7231','almondbkk@almond.co.th','Head Office','Minburi','','Quality Assurance ','Bangkok','10510','+66 2517 0042',0,'2015-01-27',0,'2015-01-27'),(12,'CDGALTUM01','Altum Precision Co., Ltd.','133 Moo 1 Hi-Tech Industrial Estate Asia-Nakornsawan Rd.,','Ban-Lane','+66 86 105 8722','lab.apt@altum.co.th','Head Office','Bang Pa-In','','QA Lab','Ayutthaya   ','13160','+66 3572 9100',0,'2015-01-27',0,'2015-01-27'),(13,'CDGAPCB001','Apcb Electronics (Thailand) Co., Ltd.','139/2 Moo 2 BangPa-In Industrial Estate Udomsorayuth Road,','Klong-Jig','','waleepun@apcb.co.th','Head Office','Bang Pa-In','1320','QC','Ayutthaya  ','13160','+66 3525 8222',0,'2015-01-27',0,'2015-01-27'),(14,'CDGASIAN01','Asian Micro (Thailand) Co., Ltd.','130/171-2 Moo 3 Factory Land','Wangchula','','jeeranan@asianmicro.co.th','Head Office','Wangnoi','','Quality Assurance ','Ayutthaya','13170','+66 3572 2022',0,'2015-01-27',0,'2015-01-27'),(15,'CDGASTCL01','A.S.T. Clean Label Co., Ltd.','242 Soi Soonvijai 4, Rama 9 Rd.,','Bangkapi','','onnisara_a@astcleanlabel.com','Head Office','Huai Khwang','30','Quality Assurance','Bangkok','10310','+66 2719 6377-9',0,'2015-01-27',0,'2015-01-27'),(16,'CDGATMCO01','A.T.M. Corporation Ltd','179/5 Moo 7','Lamtasao','','atm.corporation@yahoo.com','Head Office','Wangnoi','','','Ayutthaya ','13170','+66 3579 8134',0,'2015-01-27',0,'2015-01-27'),(17,'CDGBELLS01','Bell Survey Ltd.','87/77-81 5th Flr. Modern Town Bldd.,Ekamai Soi 3, Sukh. 63','Klongtoey-Nua','','Martin.covell@bellsurveybkk.th.com','Head Office','Wattana','','','Bangkok','10110','+66 2381 7291',0,'2015-01-27',0,'2015-01-27'),(18,'CDGBELTO01','Belton Industrial (Thailand) Ltd.','101/110 Moo 20 , Phaholyothin Rd.,','Klongneung','','puntharee.s@beltontechnology.com','Head Office','Klong Luang','1905','QA / M&P Lab','Pathumthani ','12120','+66 2529 7300',0,'2015-01-27',0,'2015-01-27'),(19,'CDGBENCH01','Benchmark Electronics (Thailand) Pcl.','94 Moo 1, Hi-Tech Industrial Estate','Ban-Lane','','Runglawan.Seeburth@bench.com','Head Office','Bang Pa-In','6770','Quality Assurance ','Ayutthaya','13160','+66 3527 6300',0,'2015-01-27',0,'2015-01-27'),(20,'CDGBESCO01','Besco & C Co.,Ltd','39 Moo 10','Nongkainam ','','walaipon@bandc.co.th','Head Office','Nong-Khae','','Accounting','Saraburi','18140','+66 3638 0927-8',0,'2015-01-27',0,'2015-01-27'),(21,' CDGBESTR01','Bestrade Precision Limited.','160 Moo 4','Makhamkou','+66 83 899 6346','patpong@bestradegroup.com','Head Office','Nikompattana','','Quality  Control  Supervisor','Rayong','21180','+66 3889 3800-4',0,'2015-01-27',0,'2015-01-27'),(22,'CDGBKKRO01','Bangkok Royal Express Co.,Ltd','57/214 Moo 3','Sam Ruen','','panachonp@gmail.com','Head Office','Bang Pa-In','','Quality Control','Ayutthaya','13170','+66 2531 5818',0,'2015-01-27',0,'2015-01-27'),(23,'CDGBOYD001 ','Boyd Technologies (Thailand) Co., Ltd.','555 Rasa Tower ll, Unit 1403, the 14th Floor. Phaholyothin Road','Chatuchuk','+66 89 920 3494','sukanya.prasertsung@boydcorp.com','Head Office','Chatuchuk','','Lab & Contamination Control ','Bangkok','10900','+66 2793 9200',0,'2015-01-27',0,'2015-01-27'),(24,'CDGBPCAS01','Bp - Castrol (Thailand) Limited.','23rd Floor, Rajanakarn Building 183 South Sathorn Road','Yannawa ','','sukanya.chantaworakit@se1.bp.com','Head Office','Yannawa ','','Technical Manager ','Bangkok','10120','+66 2684 3555',0,'2015-01-27',0,'2015-01-27'),(25,'CDGBUSIN01','Bissiness Logistic (Thailand) Co., Ltd.','1168/32 Lumpini Tower, 16th Fl., Rama IV Rd.,','Thungmahamek','','charnchai@blt.th.com','Head Office','Sathorn','','Packaging','Bangkok','10120','+66 2285 5998',0,'2015-01-27',0,'2015-01-27'),(26,'CDGCAMFI01','Camfil Farr (Thailand) Ltd.','A3 Fl.,Le-Concorde Tower, Room No.A305,202,Ratchadapisek Rd','Huai Khwang','+66 85 111 6710','Bantita.Takkhapiwat@camfil.com','Head Office','Huai Khwang','','Product Development Engineer ','Bangkok','10310','+66 2694 1480-84',0,'2015-01-27',0,'2015-01-27'),(27,'CDGCANON01','Canon Hi-Tech (Thailand) Ltd ','789 Moo 1','Naklang','+66 86 873 8006','chairat_l@cht.canon.co.th','Branch 0006','Soongnuen','','Products Quality Assurance ','Nakhonratchasima','30380','',0,'2015-01-27',0,'2015-01-27'),(28,'CDGCANON02','Canon Hi-Tech (Thailand) Ltd.','Hi-Tech Industrial Estate 89 Moo 1,','Bhan-Lain','','jiraporn@cht.canon.co.th','Head Office','Bang Pa-In','2521','PRQA','Ayutthaya','13160','+66 3535 0080',0,'2015-01-27',0,'2015-01-27'),(29,'CDGCELES01','Celestica (Thailand) Ltd','49/18 Moo 5,Leam Chabang Industrial Estate,','Tungsukhla','+66 81 840 5083','dnatapor@celestica.com','Head Office','Sriracha','3476','Lead Failure Analysis Engineer','Chonburi','20230','+66 3849 3561',0,'2015-01-27',0,'2015-01-27'),(30,'CDGCENT001','Cent - Engineering (Thailand) Co., Ltd.','70/3 M.9, Rojana Industraial Park, Rojana Road','Thanu','','iso-network-coordinator@cent-eng.com','Head Office','U-Thai','','Quality Assurance ','Ayutthaya','13210','+66 3533 1246',0,'2015-01-27',0,'2015-01-27'),(31,'CDGCLEAN01','Cleanstat (Thailand) Co., Ltd.','207 Moo 1','Ban-Lane','','Kanuengnit@cleanstat.com','Head Office','Bang Pa-In','','QA/Lab','Ayutthaya','13160','+66 3535 0555',0,'2015-01-27',0,'2015-01-27'),(32,'CDGCOLIB01','Colibri Assembly (Thailand) Co., Ltd.','150/82 Moo 9, Pinthong Industrial Estate2','Nongkham','+66 89 066 3125','sunanta.s@colibri-assembly.com ','Head Office','Sriracha','','QS Officer ','Chonburi','20110','+66 3300 5118',0,'2015-01-27',0,'2015-01-27'),(33,'CDGCOMPA01','Compart Precision (Thailand) Co., Ltd.','135 Moo 1 Hi-Tech Industrial Estate','Banpo','+66 84 094 4633','patchara.to@compart-grp.com','Head Office','Bang Pa-In','','M&P Lab Asst. Manager.','Ayutthaya','13160','+66 3531 5600',0,'2015-01-27',0,'2015-01-27'),(34,'CDGCONTI01','Continental Automotive (Thailand) Co.,Ltd.','7/259 Moo6 Amata City Industrial Estate','Mabyangporn','','Rungnapa.pasuriwong@continental-corporation.c','Head Office','Pluakdaeng','6347','Laboratory Manager / Quality','Rayong','21140','+66 3892 6299',0,'2015-01-27',0,'2015-01-27'),(35,'CDGDDK0001','Ddk (Thailand) Ltd','55/25 Moo 13, Navanakorn Industrial Estate,','Klong Nueng','','thiraporn@ddk.fujikura.co.th','Head Office','Klong Luang','1117','Quality Assurance ','Pathumthani','12120','+66 2529 1428',0,'2015-01-27',0,'2015-01-27'),(36,'CDGDEESI01','Deesiri Trading Co., Ltd','89/37  Moo 4','Bangtalard','+66 94 651 4664','deesiri.sales@hotmail.com','Head Office','Pakkret','','Sales manager','Nonthaburi ','11120','',0,'2015-01-27',0,'2015-01-27'),(37,'CDGDISKP01','Disk Precision Industries(Thailand) Co., Ltd.','193 Moo. 1 Hi Tech Industrial Estate,','Ban-Lane','+66 86 125 5152','warut@diskprecision.co.th','Head Office','Bang Pa-In','','NPD&QA Engineer','Ayutthaya','13160','+66 3531 4501-5',0,'2015-01-27',0,'2015-01-27'),(38,'CDGDONAL01','Donaldson (Thailand)  Ltd.','Amata City Industrial Estate 7/217 Moo 6,Soi Pornprapa,','Mabyangpron','','natnapat.methanorapat@donaldson.com','Head Office','Pluakdaeng','2504','Senior Chemist','Rayong','21140','+66 3865 0280',0,'2015-01-27',0,'2015-01-27'),(39,'CDGDOUYE01','Dou Yee Enterprises (Thailand) Co., Ltd. ','75/27 Moo 11, Phaholyothin Road,','Klongnueng','','bksales3@douyee.co.th','Head Office','Klong Luang','','','Pathumthani','12120','',0,'2015-01-27',0,'2015-01-27'),(40,'CDGDRUBB01','D-Rubber Products Co.,Ltd. ','38 MOO 4','Bua-Loi','','purchase@d-rubber.co.th','Head Office','Nong-Khae','','Purchasing','Saraburi','18140','+66 3637 3234-5',0,'2015-01-27',0,'2015-01-27'),(41,'CDGECOGS01','Eco Green Solutions Co.,Ltd','1/292','Bangpood','+66 81 988 3996','Sirikitjarak996@yahoo.com','Head Office','Pakkret','','','Nonthaburi','11120','',0,'2015-01-27',0,'2015-01-27'),(42,'CDGEMERS0','Emerson Electric (Thailand) Ltd.','24 Moo 4, Eastern Seaboard Industrial Estate,','Pluakdaeng','+66 81 945 5833','Yeamyuth.M@Emercon.com','Head Office','Pluakdaeng','','Facility','Rayong','21140','+66 3895 7390',0,'2015-01-27',0,'2015-01-27'),(43,'CDGEMPOR01','Emporio Controls Co., Ltd. ','205 Soi Charansanitwong 40, Charansanitwong Rd.','Bangyeekhan','+66 86 993 2026','wannuttee@emporiocontrols.com','Head Office','Bangplad','','Senior Sales Engineer','Bangkok','10700','+66 2433 6990',0,'2015-01-27',0,'2015-01-27'),(44,'CDGENPRO01','Enpro Products (Thailand) Co., Ltd.','101/47/18 M.20, Navanakorn Industrial Estate,Phaholyothin Rd.','Klong Nueng','+66 81-639 0394','yuthtaphoom_s@enpro.co.th','Head Office','Klong Luang','','Sale & CSM','Pathumthani','12120','+66 2529 1380-82',0,'2015-01-27',0,'2015-01-27'),(45,'CDGEXACT01','Exact Design & Tooling. Co., Ltd.','19/17 Moo 8','Klongha','+66 89 814 2276','aree_s@exact-thailand.com ','Head Office','Klong Luang','','','Pathumthani','12120','+66 2904 6177  ',0,'2015-01-27',0,'2015-01-27'),(46,'CDGEXCEL01','Excellent Product Manufacturing Co.,Ltd.','10 Moo 2 Phaholyotin Road, KM. 57,','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya','13210','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27'),(47,'CDGFAGOR01','Fagor Electronics (Thailand) Ltd.','Wellgrow Industrial Estate, Bangna-Trad KM.36, 82 Moo 5','Bangsamak','+66 89 767 0202','clyde_magsino@fagorelectronics.co.th','Head Office','Bangpakong','','Purchasing Manager','Chachoengsao ','24180','+66 3857 0087',0,'2015-01-27',0,'2015-01-27'),(48,'CDGFOAMT01','Foamtec International Co., Ltd. ','111/1 Moo 2 K.M.56 of Phaholyothin Road,','Lamsai','','pornpitp@foamtecintl.com  ','Branch 0003','Wangnoi ','','Quality Assurance ','Ayutthaya','13170','+66 3574 0717',0,'2015-01-27',0,'2015-01-27'),(49,'CDGFOAMT02','Foamtec International Co., Ltd.','700/50,52,54 Moo 6 Amata-Nakorn Industrial Estate,57 KM of Bangna-Trad Rd.','Nongmaidang','+66 81 555 8659','netnapay@foamtecintl.com ','Branch 0002','Muang','319','QA Supervisor','Chonburi ','20000','+66 3846 5795-800',0,'2015-01-27',0,'2015-01-27'),(50,' CDGFOAMT03','Foamtec International Co., Ltd. ','Free Trade Zone 259/1 Moo 3,','Toongsukla','+66 86 972 6138 ','namphungf@foamtecintl.com','Branch 0006','Sriracha','','Senior Data Controller','Chonburi ','20230','+66 3367 8877',0,'2015-01-27',0,'2015-01-27'),(51,'CDGFOAMT05','Foamtec International Co., Ltd.','259 Moo 3, Leam Chabang Industrial Estate, Export Processing Zone 1    ','Toongsukla','','daraneep@foamtecintl.com','Branch 0004','Sriracha','832','R&D','Chonburi ','20230','+66 3840 1888',0,'2015-01-27',0,'2015-01-27'),(52,'CDGFORTU01','Fortune And Star Technology Co., Ltd.','1597 Soi Ladprao 94 (Phanchamit), Ladprao Rd.,','Wangthonglang','+66 86 030 0398','surasak@fnstechnology.com','Head Office','Wangthonglang','','Business Development Manager','Bangkok','10310','+66 2516 1407',0,'2015-01-27',0,'2015-01-27'),(53,'CDGFUJIK01','Fujikura Electronics (Thailand) Ltd. ','118/2 Moo 11 Suwannasorn Road ','Banpra','','uthen.w@th.fujikura.com','Branch 0004','Muang','3123','QA Engineer','Prachinburi ','25230','+66 3721 3323',0,'2015-01-27',0,'2015-01-27'),(54,'CDGFUJIK02','Fujikura Electronics (Thailand) Ltd.','68/1 Moo 4,Northern Region Industrial Estate  ','Ban Klang','','pattamon.k@th.fujikura.com','Branch 0006','Muang','2936','Laboratory Quality Assurance','Lumphun','51000','+66 5358 1002',0,'2015-01-27',0,'2015-01-27'),(55,'CDGFUJIT01','Fujitsu Ten (Thailand) Co., Ltd.',' 253 M.11 Rojana Industrial Estate                                                                                                                                                                      ','Bankhai','','supansa@fttl.ten.fujitsu.com','Head Office','Nongbua','652','Purchasing officer ','Rayong      ','21120','+66 3896 2025-30',0,'2015-01-27',0,'2015-01-27'),(56,'CDGGREEN01','Green Pack Industries Co., Ltd.','40/8 Moo 5','Chiangraknoi ','+66 86 345 6553','Amornchai_t@g-p-industries.com','Head Office','Bang Pa-In ','','Production','Ayutthaya   ','13180','+66 3574 6005-6',0,'2015-01-27',0,'2015-01-27'),(57,'CDGGREEN02','Greendii Co.,Ltd.  ','55/157 Moo 2','Lumphakkood','+66 89 900 9413','Sudarat@greendii.com','Head Office','Thanyaburi','','','Pathumthani  ','12110','+66 2150 7694 - 7   ',0,'2015-01-27',0,'2015-01-27'),(58,'CDGHAGEM01','Hagemeyer - Pps (Thailand) Ltd.  ','21 Tower, 16 th Fl, 805 Srinakarin Rd.','Suanluang','+66 81 931 2166 ','nitjawan.inthajakr@hagemeyerasia.com','Head Office','Suanluang','','Sourcing Engineer','Bangkok','10250','+66 2529 5910-3 ',0,'2015-01-27',0,'2015-01-27'),(59,'CDGHANAS01','Hana Semiconductor (Ayutthaya) Co., Ltd.','Asia Road, K.M. 59, 100 Moo 1','Baan-Lane','','DoungdaoP@ayt.hanabk.th.com','Head Office','Bang Pa-In','','Environmental Officer / Facility','Ayutthaya   ','13160','+66 2209 8000',0,'2015-01-27',0,'2015-01-27'),(60,'CDGHARAD01','Harada Corporation (Thailand) Co.,Ltd ','147,9th Floor Unit 903 Rangsit-Pathumthani Road','Prachatipat','+66 81 840 5389','sakchai@haradacorp.co.jp ','Head Office','Thanyaburi','','Sale','Pathumthani  ','12130','+66 2959 2188    ',0,'2015-01-27',0,'2015-01-27'),(61,'CDGHENKE01','Henkel (Thailand) Ltd.  ','The Offices at Centralworld, 35th Floor,.999/9 Rama 1 Rd.,','Pathumwan','+66 81 939 2808','Tula.Chankana@henkel.com','Head Office','Pathumwan','','Senior Sales Engineer ','Bangkok ','10330','+66 2209 8000',0,'2015-01-27',0,'2015-01-27'),(62,'CDGHIP0001','Hi-P (Thailand) Co., Ltd.    ','7/132 Moo 4,','Mabyangporn','','Patinya.p@hi-p.com','Head Office','Pluakdeang','','Customer Quality Engineer','Rayong ','21140','+66 3865 0432-3 ',0,'2015-01-27',0,'2015-01-27'),(63,'CDGHUTCH02 ','Hutchinson Technology Operations (Thailand) C','50 Moo 4, Rojana Ind.park, Ph 8 (FZ), ','U-Thai','+66 87 201 9195 ','sukanya.yujan@hti.htch.com','Head Office','U-Thai','7309','Chemical Lab Engineer ','Ayutthaya ','13210','+66 3533 4800  ',0,'2015-01-27',0,'2015-01-27'),(64,'CDGIME0001','I.M.E. (Thailand) Co., Ltd.    ',' 1/64 Moo 5 Rojana Industrial Park, Rojana Road,','Khan-Ham','','sittiporn@ime.co.th','Head Office','U-Thai','','QA','Ayutthaya ','13210','+66 3533 0801',0,'2015-01-27',0,'2015-01-27'),(65,'CDGINNOV02','Innovalues Precision (Thailand) Ltd.   ','83 Moo 2 Hi-Tech Industrial  Estate,   ','Bann-Len','+66 90 961 3636','warunya@innovalues.com','Head Office','Bang Pa-In','1513','CQE/ QA&QC ','Ayutthaya   ','13160','+66 3536 1701-5 ',0,'2015-01-27',0,'2015-01-27'),(66,'CDGINOUT01','Inout Enterprise (Thailand) Co., Ltd. ','310/89 Moo 2   ','Sam Ruen  ','','aniruth@inoutthai.com','Head Office','Bang Pa-In','','Sales','Ayutthaya   ','13160','+66 3570 9801-2 ',0,'2015-01-27',0,'2015-01-27'),(67,'CDGINTEG01 ','Integrated Metal Finishing (Thailand) Co., Lt','129/18-19 Moo 3 ,Phaholyothin Rd., ','Wangchula','+66 86 523 6555','suriam@itmf.co.th','Head Office','Wangnoi','','Material Control','Ayutthaya   ','13170','+66 3572 1126-8 ',0,'2015-01-27',0,'2015-01-27'),(68,'CDGINTRI01','Intriplex (Thailand) Ltd.  ','158-160 Moo 1, Hi-Tech Industrial Estate, ','Baan-Lane','+66 81 754 9808','Supaktra.Sanguanrat@mmi.com.sg','Head Office','Bang Pa-In','','Sr.Chemical and LAB Engineer','Ayutthaya   ','13160','+66 3572 9183',0,'2015-01-27',0,'2015-01-27'),(69,'CDGISCM001','Iscm Technology (Thailand) Co., Ltd.      ','70/5 Moo 9,   ','Thanu','','mana@iscmthai.com','Head Office','U-Thai','','Quality Assurance Manager ','Ayutthaya ','13210','+66 3580 0116-9 ',0,'2015-01-27',0,'2015-01-27'),(70,'CDGISCM002','Iscm Technology (Thailand) Co., Ltd. ','70/6 Moo 9,   ','Thanu','','mana@iscmthai.com','Branch 0001','U-Thai','','Quality Assurance Manager ','Ayutthaya ','13210','+66 3580 0116-9',0,'2015-01-27',0,'2015-01-27'),(71,'CDGJCY0001','Jcy Hdd Technology Co., Ltd    ','70 Moo. 4 SIL Industrial,   ','Bua-Loi','','srirahayu@jcyinternational.com','Head Office','Nong-Khae','132','QA / Lab','Saraburi   ','18140','+66 3637 3992',0,'2015-01-27',0,'2015-01-27'),(72,'CDGJETHO01','Je Tho Vision Co., Ltd.  ','37/4 Moo 1,','Ban-Chang','','panachonp@gmail.com','Head Office','U-Thai','','Quality Control','Ayutthaya ','13210','+66 3525 5262',0,'2015-01-27',0,'2015-01-27'),(73,'CDGKATAY01','Katayama Advanced Precision (Thailand) Ltd.  ',' 42/6-7 Moo 4  ','Ban-Chang','','sompong@katayama-ap.co.th','Branch 0001','U-Thai','504','Quality Assurance Engineer','Ayutthaya ','13210','+66 3574 6608-11 ',0,'2015-01-27',0,'2015-01-27'),(74,'CDGKTEC001','K-Tech Industrial (Thailand) Co., Ltd      ','7/297 Moo 6 ','Mabyangporn  ','+66 86 320 6456','Saleela.w@ktech-th.co.th ','Head Office','Pluakdeang   ','8002','Quality Assurance Department','Rayong       ','21140','+66 3803 6210-16',0,'2015-01-27',0,'2015-01-27'),(75,'CDGLASER01','Laser Printing (Thailand) Co.,Ltd      ','Amata Nakorn Industrial Estate, 700/394 Moo 6,    ','Don Hau Roh','+66 85 071 1626','qclaserthi@csloxinfo.com','Head Office','Muang','','Quality Assurance Engineer','Chonburi ','20000','+66 3846 5222  ',0,'2015-01-27',0,'2015-01-27'),(76,'CDGLEADE01','Leader Industries Ltd., Part    ','150/78 Moo 3 ,Teparuk Road,   ','Bangplee-Yai','','bancha_r@leaderindustries.co.th','Head Office','Bangplee','','QA','Samutprakarn','10540','+66 2385 5585 ',0,'2015-01-27',0,'2015-01-27'),(77,'CDGLINDE01','Linde (Thailand) Public Company Limited ',' 15th Floor, Bangna Tower A, 2/3 M. 14, Bangna Trad Rd','Bangkaew','+66 85 109 5682 ','Sutthipong.Itthipalangkul@linde.com','Head Office','Bangplee','','Engineer','Samutprakarn','10540','+66 2338 0800 ',0,'2015-01-27',0,'2015-01-27'),(78,'CDGLINTE01','Lintec Bkk Pte Ltd.',' 11 Q. House Sathorn Bldg., 8th Fl., South Sathorn','Thungmahamek','','dusit@lintec.com.sg','Head Office','Sathorn    ','','Sales','Bangkok ','10120','+66 2287 0660   ',0,'2015-01-27',0,'2015-01-27'),(79,'CDGLINTEC2','Lintec (Thailand) Co., Ltd.   ','128/2 Moo 5 Bangna-Trad Road,','Bangsamak','','dusit@lintec.com.sg','Head Office','Bangpakong','','Sales','Chachoengsao  ','24130','+66 3857 1195 ',0,'2015-01-27',0,'2015-01-27'),(80,'CDGMACSY01','Macsys Industries (Thailand) Co., Ltd.    ','789/23 Moo 1, Soi 3 Pinthong Industrial Estate  ','Nongkham','+66 87 859 0620','jariya@macsth.co.th','Head Office','Sriracha','','Production Engineer','Chonburi        ','20230','+66 3834 8411-3',0,'2015-01-27',0,'2015-01-27'),(81,'CDGMAGNE01','Magnecomp Precision Technology Public Co., Lt','162 Moo 5, Phaholyothin Road, ','Lamsai','','kittichaip@magnecomp.com','Head Office','Wangnoi','2835','SQE','Ayutthaya ','13170','+66 3521 5225',0,'2015-01-27',0,'2015-01-27'),(82,' CDGMARUA01','Maruai (Asia) Co., Ltd.   ','Hi-Tech Industrial Estate 135 Moo.1   ','Ban-Lane','','i.teerawat@maruai-asia.co.th','Head Office','Bang Pa-In','','Quality Assurance ','Ayutthaya   ','13160','+66 3535 0663-5 ',0,'2015-01-27',0,'2015-01-27'),(83,'CDGMATER01','Material Expertise Co., Ltd.',' 50 Moo.1','Klong Pra Udom','','adul@malugo.co.th','Head Office','Latlumkaew ','','','Pathumthani ','12140','+66 2194 5904-9',0,'2015-01-27',0,'2015-01-27'),(84,'CDGMEKTE01','Mektec Manufacuring Corporation (Thailand) Lt','560 Moo2, Bangpa-in Industrial Estate, Udomsorayuth Rd','Klong-Jik','','pallapar@mektec.co.th','Head Office','Bang Pa-In','4552','Sr. Lab Chemistry officer','Ayutthaya   ','13160','+66 3525 8888',0,'2015-01-27',0,'2015-01-27'),(85,'CDGMIMAN01 ','Mi Manufacturing (Thailand) Ltd.  ','7/239 Moo 6,    ','Mabyangporn','','daungrutai.boonyen@mi-st-group.com','Head Office','Pluakdaeng','','QA','Rayong       ','21140','+66 3865 0587-8',0,'2015-01-27',0,'2015-01-27'),(86,'CDGMITTA01','Mittapab Plastic Industry Co.,Ltd. ','10/25 Moo 5 Soi Subpaisan 3, Leabklongsewapasawat Rd     ','Khokkrabue','+66 81 854 5843','suphoj@mittapab.com','Head Office','Muang ','','','Samutsakorn  ','74000','',0,'2015-01-27',0,'2015-01-27'),(87,'CDGMIYOS01','Miyoshi Hi-Tech Co., Ltd. ','38 MOO1, Hi-Tech Industrial Estate ','Banpo','','wivornchai.mht@th.miyoshi.biz','Head Office','Bang Pa-In','','Quality Assurance','Ayutthaya   ','13160','+66 3531 4031-4  ',0,'2015-01-27',0,'2015-01-27'),(88,'CDGMKL0001','Mkl Tool Limited Partnership','26/273-275 Moo 18     ','Klong-Nueng','','purchase_mkl@hotmail.com','Head Office','Klong Luang','','','Pathumthani  ','12120','+66 2908-1107',0,'2015-01-27',0,'2015-01-27'),(89,'CDGMMIPA01','Mmi Precision Assembly (Thailand) Co., Ltd.','888 Moo 1, Mittraphap Road','Naklang','','daungnapa@mmi.com.sg','Head Office','Sungnoen','1202','QA Contamination','Nakhonratchasima ','30380','+66 4429 1579 ',0,'2015-01-27',0,'2015-01-27'),(90,'CDGMMIPF01  ','Mmi Precision Forming (Thailand) Limited. ','70/8-11 Moo 9 Rojana Industrial Park','Thanu','+66 89 666 3097','noppawan@mmi.com.sg','Head Office','U-Thai','','Senior Engineer, MSL Lab','Ayutthaya   ','13210','+66 3571 9339',0,'2015-01-27',0,'2015-01-27'),(91,'CDGMODUL01','Modular Engineered Products Supply Co., Ltd. ',' 39/13-16 Unit 4D., 4th Floor, Soi Suanplu, Sathorn Rd.','Thungmahamek','+66 81 694 3804','nuantip@mepscosystems.com','Head Office','Sathorn','','Purchasing Manager','Bangkok','10120','+66 2755 0779',0,'2015-01-27',0,'2015-01-27'),(92,'CDGMPM0001','Mpm Technology (Thailand) Limited','101/79, Moo 20 Navanakorn Industrial Estate','Klong Nung','+66 87 517 8490','nithima@mmi.com.sg','Head Office','Klong Luang','','QA Engineer','Pathumthani ','12120','+66 2909 0909',0,'2015-01-27',0,'2015-01-27'),(93,'CDGMTEC001','National Metal And Meterials Technology','114 Thailand Science Park Paholyothin Rd.','Klong Nung','','daruneea@mtec.or.th','Head Office','Klong Luang','4757','Plastics Technology Lab','Pathumthani','12120','+66 2564 6500',0,'2015-01-27',0,'2015-01-27'),(94,'CDGMYLER01','Myler Company Limited ','25/11 Moo 8','Chimplee','','pornpimol@myler.co.th','Head Office','Talingchan','','','Bangkok','10170','+66 2422 3975   ',0,'2015-01-27',0,'2015-01-27'),(95,'CDGNEXAS01','Nexas Elechemic Co., Ltd.  ','115/10 M. 4, Saharatananakorn Industrial Estate','Bangphrakru','+66 89 923 3156','cqa1@nexasthai.com','Head Office','Nakornluang','','QA','Ayutthaya   ','13260','+66 3571 6576-7',0,'2015-01-27',0,'2015-01-27'),(96,'CDGNHK0001','Nhk Spring (Thailand) Co., Ltd.','115 M. 5, Bangna Trad Rd, ','Bangsamak','','piyachat.mab@nhkspg.co.th','Branch 0003','Bangpakong ','5242','Quality Assurance  Engineer','Chachoengsao','24180','+66 3884 2830',0,'2015-01-27',0,'2015-01-27'),(97,'CDGNIDEC01 ','Nidec Component Technology (Thailand) Co., Lt','38 Moo 1','Bua-Loi','+66 90 429 5861','put.saelew@nidec.com','Head Office','Nong-Khae','304','Lab Sr.Engineer','Saraburi ','18140','+66 3637 3741-4',0,'2015-01-27',0,'2015-01-27'),(98,'CDGNIDEC02','Nidec Precision (Thailand) Co., Ltd. ','(NPT R/F) 29 Moo 2, U-Thai-Pachi Rd., ','Ban-Chang','','NPTA_CAL@notes.nidec.co.jp','Head Office','U-Thai','307','NPTA_Calibration','Ayutthaya ','13210','+66 3574 6683-6',0,'2015-01-27',0,'2015-01-27'),(99,'CDGNIDEC03','Nidec Electronics (Thailand) Co., Ltd. ','199/12 Moo 3, Thunyaburi-Lumlookka Road, ','Rangsit','','Kitisak.Pandaranantaka@nidec.com','Head Office','Thanyaburi','2214','QA','Pathumthani  ','12110','+66 2577 5077  ',0,'2015-01-27',0,'2015-01-27'),(100,'CDGNIDEC05','Nidec Electronics (Thailand) Co., Ltd.  ','Rojana Factory 44 Moo 9, Rojana Industrial Park','Thanu','','NETR_QA-LAB@notes.nidec.co.jp','Branch 0002','U-Thai','3901','QA-LAB','Ayutthaya   ','13210','+66 3533 0742',0,'2015-01-27',0,'2015-01-27'),(101,'CDGNIDEC06','Nidec Precision (Thailand) Co., Ltd.     ','NPT Ayutthaya Factory 118 Moo 5, Phaholyothin Rd.','Lamsai','','SO_SIRANEE@notes.nidec.co.jp','Branch 0004','Wangnoi','','QA-Base / LAB','Ayutthaya     ','13170','+66 3521 5318 ',0,'2015-01-27',0,'2015-01-27'),(102,'CDGNIPPO01','Nippon Paint (Thailand) Co., Ltd.    ','700/29,31 Moo 6, 700/33 Moo 5','Klong Tam Rhu','','qc_waterbase@nipponpaint.co.th','Head Office','Muang','363','QC (WATER BASE)','Chonburi   ','20000','+66 3821 3701-5',0,'2015-01-27',0,'2015-01-27'),(103,'CDGNISSH01','Nissho Seiko (Thailand) Ltd.  ','7/225 Amata City Industrial Estate Moo 6','Mabyangporn','+66 86 001 7870','nstqa-mattika@nisshoseiko.com','Head Office','Pluakdaeng  ','145','QA','Rayong ','21140','+66 3865 0175-8',0,'2015-01-27',0,'2015-01-27'),(104,'CDGNITT01','Nitto Matex (Thailand) Co., Ltd   ','700/611 Moo 7 Bangna-Trad Rd. Km.57    ','Don Hau Roh','','jirachaya_sei-lim@gg.nitto.co.jp','Head Office','Muang ','431','Quality Assurance ','Chonburi   ','20000','+66 3804 7015   ',0,'2015-01-27',0,'2015-01-27'),(105,'CDGNITTO01','Nitto Denko Material (Thailand) Co., Ltd.  ','Rojana Industrial Park, 1/75 Moo 5, Rojana Rd.','Khan-Ham','','rungthiwa_buatoom@gg.nitto.co.jp','Head Office','U-Thai','420','Purchasing Section','Ayutthaya   ','13210','+66 3522 6750 ',0,'2015-01-27',0,'2015-01-27'),(106,'CDGNMB0001','Nmb-Minebea Thai Limited    ','1/14 MOO 5, ROJANA ROAD','Khan-Ham','','naruemon.p@minebea.co.th','Branch 0003','U-Thai','2449','Spindle Motor','Ayutthaya   ','13210','+66 3533 0506-9',0,'2015-01-27',0,'2015-01-27'),(107,'CDGNMB0002 ','Nmb-Minebea Thai Ltd.    ','1 Moo 7, Phaholyothin Road, Km. 51  ','Chiangraknoi','+66 87 916 5343','somkid.p@minebea.co.th','Head Office','Bang Pa-In','','Purchasing ( Spindle Motor Div )','Ayutthaya   ','13180','+66 3523 7268-75',0,'2015-01-27',0,'2015-01-27'),(108,'CDGNOK0001','Nok Precision Component (Thailand) Ltd. ','189, 198, 296 Moo 16 Bangpa-in Industrial Estate, Udomsorayath Rd.',' Bangkrasan','','alisad@nokpct.com','Head Office','Bang Pa-In','','QA','Ayutthaya   ','13160','+66 3525 8666 ',0,'2015-01-27',0,'2015-01-27'),(109,'CDGNTN0001','Ntn Manufacturing (Thailand) Co., Ltd.  ','64/89 Moo 4,    ','Pluakdaeng','','nipaporn@nmt.th.com','Branch 0002','Pluakdaeng','207','QA NMT','Rayong ','21140','+66 3895 5935-8',0,'2015-01-27',0,'2015-01-27'),(110,'CDGOKIPR01  ','Oki Proserve (Thailand) Co., Ltd. ','1168/32 Lumpini Tower, 16th Fl., Rama IV Rd., ','Thungmahamek','+66 89 922 8459','Charnchai@blt.co.th','Head Office','Sathorn','402','','Bangkok','10120','+66 2285 5998',0,'2015-01-27',0,'2015-01-27'),(111,'CDGONOFL01','O.N.O. Flow Co., Ltd.    ','48/53 Floor 4 Moo 7 Boonkum Rd.,','Kukot','','janeth@connols.co.th','Head Office','Lamlukka','','Purchase ','Pathumthani  ','12130','+66 2900 6900-5',0,'2015-01-27',0,'2015-01-27'),(112,'CDGP&NIN01','P&N Intelligent Provision Co.,Ltd.','316/47 , Ladprow87 Road','Klongchaokhunsing','+66 81 172 3247','chaipiwong@yahoo.com','Head Office','Wangthonglang','','Managing Director','Bangkok ','10310','+66 2932 1524   ',0,'2015-01-27',0,'2015-01-27'),(113,'CDGPACIF01','Pacific Cleanroom Services Limited','269/2 P.K.Building 2nd Floor,  S.Chockchaijongchamroen, Rama3 Rd.','Bangpongpang','','logistics@pcst.th.com','Head Office','Yannawa','','Purchase ','Bangkok ','10120','+66 2284 2616-8',0,'2015-01-27',0,'2015-01-27'),(114,'CDGPHOLD01','Pholdhanya Public Company Limited','1/11 MOO3 , Lamlukka Rd.,','Ladsawai','+66 91 557 8690','ammarin@pdgth.com','Head Office','Lamlukka','','','Pathumthani  ','12150','+66 2791 0230',0,'2015-01-27',0,'2015-01-27'),(115,'CDGPMCLA01','Pmc Label Materials Co., Ltd. ','30/28 Moo 2','Khokkham','','numtip@pmclabel.com','Head Office','Muang','223','QA/QC','Samutsakorn','74000','+66 3445 2000    ',0,'2015-01-27',0,'2015-01-27'),(116,'CDGPOOMJ02','Poomjai Engineering Co., Ltd.','136 Soi Lasalle 42','Bangna','+66 81 811 6057','Somsak_j@poomjai.co.th','Head Office','Bangna','','Quality Manager','Bangkok ','10260','+66 2752 5570-4',0,'2015-01-27',0,'2015-01-27'),(117,'CDGPOSCO01','Posco-Thainox Public Company Limited','324 Moo 8, Highway No 3191 Road','Mabkha','','hattaya@poscothainox.com','Branch 0002','Nikompattana','211','Products Technical Service Team','Rayong','21180','+66 3863 6125-32',0,'2015-01-27',0,'2015-01-27'),(118,'CDGPRIMA01','Prima Kleen Ltd. ','733/401-3 Moo 8 Phaholyothin Rd.','Kookod','','primakl@loxinfo.co.th','Head Office','Lamlukka','','','Pathumthani  ','12130','+66 2998 9308-11',0,'2015-01-27',0,'2015-01-27'),(119,'CDGPSC0001 ','Psc Technology (Ayutthaya) Co., Ltd.','593 Moo 2 Bang Pa-in Industrial Estate','Klong-Jig','','purchase.psca@psc-tech.com','Head Office','Bang Pa-In','201','Purchase ','Ayutthaya   ','13160','+66 3525 8152-5',0,'2015-01-27',0,'2015-01-27'),(120,'CDGRCI0001','Rci Labscan Limited ','24 Rama 1 Rd.','Rong Muang','','archara.c@rcilabscan.com','Head Office','Pathumwan','','','Bangkok','10330','+66 2613 7911-4',0,'2015-01-27',0,'2015-01-27'),(121,'CDGROJAN02','Rojanapat Engineering Co., Ltd.       ','60/9 Moo 4, Soi Klong 4 Tawan-OK 17,  Leab Klong 4 Road','Klong Si','','enquiry@rojanapatfilter.com','Head Office','Klong Luang ','201',' Admin','Pathumthani ','12120','+66 2908 1928 ',0,'2015-01-27',0,'2015-01-27'),(122,'CDGROYCE01','Royce Universal Co., Ltd.   ','86 Moo 9 Lang Wat Tha-Pood Rd.,','Raiking','','daw@royceuniversal.net','Head Office','Sampran','','QA','Nakronpathom ','73210','+66 2810 2533-5 ',0,'2015-01-27',0,'2015-01-27'),(123,'CDGRTSUP01','R T Supply Co.,Ltd.','116/5  Prayasurain Rd. ','Samwatawantok','+66 95 535 1448','purchase2@rt-supply.com','Head Office','Klongsamwa   ','','Purchasing','Bangkok','10510','+66 2914 3041',0,'2015-01-27',0,'2015-01-27'),(124,'CDGSAKUR01','Sakura Tech (Thailand) Ltd.','64/146 MOO 4. ESTERN SEABOARD INDUSTRIAL ESTATE ','Pluakdaeng ','+66 86 361  0647','kamolsaya16@gmail.com','Head Office','Pluakdaeng ','120','Project & Procurement Engineer ','Rayong','21140','+66 3895 9016-8   ',0,'2015-01-27',0,'2015-01-27'),(125,'CDGSAMSU01','Samsung Electro-Mechanics Nakhonratchasima Co','Suranaree Industrial Zone 555 Moo 6 ','Nong Rawieng','','jutima.p@samsung.com','Head Office','Muang','2170','Chemical test part','Nakhonratchasima','30000','+66 4421 2905-12',0,'2015-01-27',0,'2015-01-27'),(126,'CDGSCHAF01','Schaffner Emc Co. Ltd.','67 Moo 4','Ban Klang','','sakpaiboon.tajumpa@schaffner.com','Head Office','Muang ','','QA Engineer','Lumphun  ','51000','+66 5358 1104',0,'2015-01-27',0,'2015-01-27'),(127,'CDGSEAGA01','Seagate Technology (Thailand) Ltd.   ','1627 Moo 7, Teparuk Road','Teparuk','','suparat.yangsanthia@seagate.com ','Head Office','Muang ','2408','Indirect Material Purchasing','Samutprakarn  ','10270','+66 2715 2273 ',0,'2015-01-27',0,'2015-01-27'),(128,'','Seagate Technology (Thailand) Ltd.   ','90 Moo 15 ','Sungnoen','','kitti.kaewrattanapattama@seagate.com','Branch 0007','Sungnoen ','','Facility Engineer','Nakhonratchasima','30170','+66 4470 4315',0,'2015-01-27',0,'2015-01-27'),(129,'CDGSEIIN01','Sei Interconnect Products (Thailand) Ltd.','700/128 Moo 5 Bangna-Trad Rd.','Klong Tam Roo','+66 86 677 7697','hunsa-opanuruk@sept.sei.co.jp','Head Office','Muang','1143','QA/Lab','Chonburi   ','20000','+66 3846 5804-10',0,'2015-01-27',0,'2015-01-27'),(130,'CDGSEIKO01 ','Seiko Instruments (Thailand) Ltd.','60/83 Moo 19, Nava-nakorn Industrial Estate Zone 3 ','Klong Nueng','','chiraporn.s@sit.co.th','Head Office','Klong Luang ','','Ass t Dept Manager Chemical ','Pathumthani ','12120','+66 2529 2420-5',0,'2015-01-27',0,'2015-01-27'),(131,'CDGSEIKO02','Seikou Communication Co., Ltd','29/4 Moo 2, Putthabucha 36','Bangmod','+66 81 648 5545 ','chanukid@seikou.co.th','Head Office','Thongkru ','','','Bangkok','10140','+66 2426 3402',0,'2015-01-27',0,'2015-01-27'),(132,'CDGSEKSU01','Seksun Technology (Thailand) Co., Ltd.    ','99 Moo 9,','Thanu','+66 89 734 3620','pinanong@seksun.co.th','Head Office','U-Thai','244','QA/Lab','Ayutthaya   ','13210','+66 3580 0100',0,'2015-01-27',0,'2015-01-27'),(133,'CDGSHINE01','Shin-Etsu Magnetics (Thailand) Ltd. ','56/26 Moo 20','Klong Nueng','+66 86 977 7416','Kantinunt.s@set.co.th','Head Office','Klong Luang ','','QA Department','Pathumthani ','12120','+66 2520 4293-8',0,'2015-01-27',0,'2015-01-27'),(134,'CDGSHINE02 ','Shin-Etsu Magnetics (Thailand) Ltd. ','60/120, 122,123 Moo 19','Klong Nueng','+66 86 977 7416','Kantinunt.s@set.co.th','Head Office','Klong Luang ','','QA Department','Pathumthani ','12120','+66 2529 6230-1',0,'2015-01-27',0,'2015-01-27'),(135,'CDGSHINS01','Shinsei (Thailand) Co., Ltd.   ','40/18-19 Moo 5, Rojana Industrial park','U-Thai','','rattana@shinsei.co.th','Head Office','U-Thai','','QA','Ayutthaya   ','13210','+66 3574 1741',0,'2015-01-27',0,'2015-01-27'),(136,'CDGSIMAT01','Simat Label Company Limited  ','123, Soi Chalongkrung 31, Ladkrabang Industrial Estate, Chalongkrung Road','Lamplatew','+66 81 913 2684','somchoke.s@simat.co.th','Head Office','Ladkrabang','','','Bangkok','10520','+66 2326 0999',0,'2015-01-27',0,'2015-01-27'),(137,'CDGSKILL01','Skill Development Co.,Ltd   ','109 Moo 7, Kingkaew-Bangplee Rd., ','Bangplee-Yai','+66 90 198 7801 ','sirichai.eng@skill1999.com','Head Office','Bangplee','18','Mechanical Eng. Manager','Samutprakarn','10540','+66 2751 1256 ',0,'2015-01-27',0,'2015-01-27'),(138,'CDGSOLAR01','Solarlens Co., Ltd. ',' 2999Bang pa-in Industrial Estate,Export Processing Zone 4/2, Udomsorayuth Rd','Klong-Gik','+66 81 916 5824','wothiwunnarak@intercast-group.com','Head Office','Bang Pa-In','','Engineering','Ayutthaya   ','13160','+66 3526 8219-26  ',0,'2015-01-27',0,'2015-01-27'),(139,'CDGSOODE01','Soode Nagano (Thailand) Co., Ltd. ','130/129 Moo 3 Factoryland wangnoi','Wangchula','+66 86 753 2179','sayfon.w@soode.co.th','Head Office','Wangnoi','105','Quality & Environment Management System','Ayutthaya   ','13170','+66 3872 1711-5 ',0,'2015-01-27',0,'2015-01-27'),(140,'CDGSPC0001 ','Siam Precision Components Ltd.  ','19/29 Moo 10 Paholyothin Rd.','Klong Nueng','+66 81 812 3967','pairogew@siamprecision.com','Head Office','Klong Luang ','','Quality  Assurance','Pathumthani ','12120','+66 2529 6242',0,'2015-01-27',0,'2015-01-27'),(141,'CDGSPECI01','Specialty Tech Corporation Limited ',' 1/8 Moo 1','Klong Nueng','','nuntaporn@specialty.co.th','Head Office','Klong Luang ','','Purchasing','Pathumthani ','12120','+66 2833 3999',0,'2015-01-27',0,'2015-01-27'),(142,'CDGSUMIT01','Sumitomo Electric (Thailand) Ltd  ','54 B.B. Building, 15th Floor, Sukhumvit 21 Road','North Klongtoey','','angsana-phummee@gr.sei.co.jp','Head Office','Wattana','','Sales Department ','Bangkok ','10110','+66 2260 7231-5 ',0,'2015-01-27',0,'2015-01-27'),(143,'CDGSUNLI01','Sunlit (Thailand) Co., Ltd. ','Eastern Seaboard Industrial Estate , 64/155 Moo 4 ','Pluakdaeng ','+66 82 120 5685     ','pro_2@sunlit.co.th','Head Office','Pluakdaeng ','','Maintenance Engineer','Rayong','21140','+66 3895 9383',0,'2015-01-27',0,'2015-01-27'),(144,'CDGTAIKI01','Taikisha (Thailand) Co.,Ltd.','62 SILOM ROAD','Suriyawong','+66 92 617 7789','tkc_nantagan@hotmail.com','Head Office','Bangrak','','Senior Engineer','Bangkok','10500','+66 2236 8055-9',0,'2015-01-27',0,'2015-01-27'),(145,'CDGTAIYO02','Taiyo Technology Industry (Thailand) Co., Ltd','55/1,3,5,9,11  Moo.15','Bangsaotong','+66 85 482 9426','Wanyupa@taiyotech.th.com','Head Office','Bangsaotong','','','Samutprakarn','10540','+66 2182 5228-9',0,'2015-01-27',0,'2015-01-27'),(146,'CDGTANAB01','Tanabe (Thailand) Co., Ltd ','304 Industrial Park, 229 Moo. 7','Thatoom','+66 89 244 7006','niramol@tanabe.co.th','Head Office','Srimahaphot','29','QA','Prachinburi ','25140','+66 3720 8591-3',0,'2015-01-27',0,'2015-01-27'),(147,'CDGTDI0001','Thai Dai-Ichi Seiko Co., Ltd. ','700/390 Moo 6','Don Hau Roh','+66 81 999 5913','Qa3@thai-daiichi.co.th','Head Office','Muangchonburi','706','QA-TDI','Chonburi ','20000','+66 3846 8316',0,'2015-01-27',0,'2015-01-27'),(148,'CDGTDK0001','Tdk (Thailand) Co., Ltd. ','1/62 Moo 5, Rojana Industrial Park, Rojana Rd.','Khan-Ham','','RungrapeeB@th.tdk.com','Head Office','U-Thai','3323','QA MSL Analysis Lab','Ayutthaya   ','13210','+66 3533 0614-24  ',0,'2015-01-27',0,'2015-01-27'),(149,'CDGTDK0002','Tdk (Thailand) Co., Ltd.','149 Moo 5, Phaholyothin Rd.','Lamsai','+66 83 123 4684','Rattiyat@th.tdk.com','Head Office','Wangnoi ','5331','Sr.QA-Analysis engineer','Ayutthaya','13170','+66 3521 5299',0,'2015-01-27',0,'2015-01-27'),(150,'CDGTECH02','Techno Gateway (Asia) Co., Ltd.','99/9 Central Tower office Building Chaengwattana, Fl.12 Room 1202 M.2 Chaeng Wattana Rd','Bangtala','+66 80 603 2417','noboruhayakawa@techno-gateway.asia','Head Office','Pakkret','','','Nonthaburi ','11120','',0,'2015-01-27',0,'2015-01-27'),(151,'CDGTECHN01','Techno Packaging Industries Co.,Ltd ','69 Rojana Industrial moo9','Thanu','','sophie@technopackaging.asia','Head Office','U-Thai','','','Ayutthaya   ','13210','+66 3580 0233',0,'2015-01-27',0,'2015-01-27'),(152,'CDGTEXCH01','Texchem - Pack (Thailand) Co., Ltd. ','234 Moo 2, Bangpa-in Industrial Estate','Klong-Jig','+66 81 714 9685','qa_engineer@th.texchem-pack.com','Head Office','Bang Pa-In','4701','QA engineer','Ayutthaya   ','13160','+66 3525 8428',0,'2015-01-27',0,'2015-01-27'),(153,'CDGTHAIK01','Thai Kajima Co., Ltd. ','952 19th Fl.,Ramaland Bldg., Rama IV Rd.','Suriyawong','','thawatchai@kajima.co.th','Head Office','Bangrak','','','Bangkok','10500','+66 2632 9300',0,'2015-01-27',0,'2015-01-27'),(154,'CDGTHERM01','Thermal Pack Co., Ltd.   ',' 88/5  Moo 1, Bangbuathong-Pathumthani Rd.','Bangtanai','','kittisak@thermalpack.net','Head Office','Pakkret','','','Nonthaburi ','11120','+66 2598 6291-3',0,'2015-01-27',0,'2015-01-27'),(155,'CDGTHHOU01','Thai Houghton 1993 Co., Ltd.','77/105-106, 25th Flr., Sinn Sathorn Tower',' Krungthonburi','+66 89 403 9293 ','Udaporn.Phookduang@houghtonintl.com','Branch 0001','Klongsarn','','Senior Executive Administrator ','Bangkok ','10600','+66 2440 1262 ',0,'2015-01-27',0,'2015-01-27'),(156,'CDGTHJUR01','Thai Jurong Engineering Limited  ','75/43 Ocean Tower 2, 22 Floor, Sukumvit 19','North Klongtoey','','wendywee@tjel.co.th','Head Office','Wattana','','Commissioning','Bangkok  ','10110','+66 2260 5181-4',0,'2015-01-27',0,'2015-01-27'),(157,'CDGTHKOK01 ','Thai Kokoku Rubber Co., Ltd.','36 Moo 9 Rojana Industrial Park, Rojana Road','U-Thai','','achari@kokoku.co.th','Head Office','U-Thai','','Quality Control','Ayutthaya   ','13210','+66 3533 0781',0,'2015-01-27',0,'2015-01-27'),(158,'CDGTHMEK01 ','Thai Mekki Company Limited ','113 Soi Bangkradi 8, Bangkradi Rd.','Smaedam','+66 81 786 1639','patcharapong@thaimekki.com','Head Office','Bangkhuntien','','QA','Bangkok  ','10150','+66 2896 9008',0,'2015-01-27',0,'2015-01-27'),(159,'CDGTHMEK02 ','Thai Mekki (2012) Company Limited','113 Soi Bangkradi 8, Bangkradi Rd.','Smaedam','+66 81 786 1639','patcharapong@thaimekki.com','Head Office','Bangkhuntien','','QA','Bangkok  ','10150','+66 2452 1290 ',0,'2015-01-27',0,'2015-01-27'),(160,'CDGTHREE01','Three Bond Manufacturing (Thailand) Co., Ltd.','700/432 M. 7, Bangna-Trad Rd','Don Hau Roh','','gusmar@tbmt.co.th','Head Office','Muangchonburi','46','Development Department','Chonburi ','20000','+66 3845 4251-3',0,'2015-01-27',0,'2015-01-27'),(161,'CDGTHREE02','Three Bond Technology (Thailand) Co., Ltd.','153 Moo 1  ','Ban-Lane','+66 86 546 3205','wassana@tbtt.co.th','Head Office','Bang Pa-In','','QC Section','Ayutthaya   ','13160','+66 3572 9259-61',0,'2015-01-27',0,'2015-01-27'),(162,'CDGTHTAK01','Thai Takasago Co., Ltd.',' Bangna TowersC16th Fl., 40/14 M.12, Bangna-Trad Rd, K.M. 6.5','Bangkaew','+66 81 825 3667','theeraphan.j@thaitakasago.co.th','Head Office','Bangplee','',' Engineering','Samutprakarn ','10540','+66 2751 9695-99',0,'2015-01-27',0,'2015-01-27'),(163,'CDGTODAP01','Toda Pipe (Thailand) Co., Ltd',' 208 Moo 2 Phaholyothin Rd.','Chamab','','qa_dcc@toda.co.th','Head Office','Wangnoi ','','Document Control Center','Ayutthaya   ','13170','+66 3574 4200-4 ',0,'2015-01-27',0,'2015-01-27'),(164,'CDGTOKYO01','Tokyo Seimitsu(Thailand)Co.,Ltd.','93/11-13 Kensington Place 4th FL.,Chaeng Wattana13','Thung Song Hong','+66 81 910 2013 ','Yutthana@accretech.com.my','Branch 0001','Lak Si','','Sr. Sales Engineer','Bangkok  ','10210','+66 2617 9497-8 ',0,'2015-01-27',0,'2015-01-27'),(165,'CDGTOLI001','Toli Packaging (Thailand) Co., Ltd.','58-59 Moo 3 Phahinyothin Rd.','Lamsai','','piyapong@tolipackaging.com','Head Office','Wangnoi ','200','QA','Ayutthaya','13170','+66 3528 7597',0,'2015-01-27',0,'2015-01-27'),(166,'CDGTRUE001','True Internet Data Center Co., Ltd.  ','18 True Tower, 14th Floor  , Ratchadaphisek Road','Huai Khwang','+66 84 075 3668','Thanawat_Taw@truecorp.co.th','Head Office','Huai Khwang','','','Bangkok ','10310','+66 2643 1111  ',0,'2015-01-27',0,'2015-01-27'),(167,'CDGTSCAN01','Thai Scan Tube Co., Ltd.  ','143 Moo 1  ','Bo-Kwangthong','','somporn@scantube.com','Head Office','Bothong','','General Manager','Chonburi  ','20270','+66 3836 3221-2',0,'2015-01-27',0,'2015-01-27'),(168,'','Valtech Corporation   ','1/21 Soi Sarmmit,','Klongtoey','+66 86 754 5268','c.sakuna@valtechcorp.com','','Klongtoey','','Manager-South East Asia','Bangkok','10110','',0,'2015-01-27',0,'2015-01-27'),(169,'CDGWDB0001','Western Digital (Thailand) Co., Ltd. ',' 140 Moo 2','Klong-Jig','','Kingfa.kitchainugool@wdc.com','Head Office','Bang Pa-In','','TSE/AS Lab','Ayutthaya   ','13160','+66 3527 8764',0,'2015-01-27',0,'2015-01-27'),(170,'CDGWEMA001','Wema Environmental Technologies Limited','999/18 Moo 15, Bangna-Trad KM.23 Road','Bangsaotong','+66 81 863 3660','chsu@wema.com','Head Office','Bangsaotong','','','Samutprakarn','10540','+66 2182 5291',0,'2015-01-27',0,'2015-01-27'),(171,'CDGWINGF01','Wing Fung Adhesive Manufacturing (Thailand) C','10 Moo 2 Paholyotin Road, KM. 57','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya   ','13170','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27'),(172,'CDGWINGF02 ','Wing Fung Packaging Co.,Ltd    ','10 Moo 2 Paholyotin Road, KM. 57','Lamsai','+66 87 9711959','usani@wingfungthai.com','Head Office','Wangnoi','51','Quality Assurance ','Ayutthaya   ','13170','+66 3528 7491-4',0,'2015-01-27',0,'2015-01-27'),(173,'CDGWINNE01','Winner Export (Thailand) Co., Ltd. ','16 Soi Panitchayakarn Thonbui 9, Yak 10 ','Wat Thaphra','+66 86 774 3233','chitrapat.winner@gmail.com','','Bangkok Yai','','Control Production','Bangkok ','10600','+66 2411 6097',0,'2015-01-27',0,'2015-01-27'),(174,'CDGWORLD01','World Hitech Marketing Co.,Ltd    ','85/34-36 Moo5 ','Wat-Tum','','wht_tape@hotmail.com','','Ayutthaya','','Admin','Ayutthaya','13000','+66 3571 3543',0,'2015-01-27',0,'2015-01-27'),(175,'CDGYUSHI01','Yushiro (Thailand) Co., Ltd.   ','700/533 Moo 7 Amata Nakorn Industrial Estate','Don Hau Roh','','takahashi@yushiro.co.th','','Muangchonburi','','Sales','Chonburi','20000','+66 3845 4873',0,'2015-01-27',0,'2015-01-27'),(176,'CDGZOSEN01','Zos Engineering Co., Ltd  ','53/501 Moo 9','Bangpood','+66 86 309 3330','kongsak.s@zos-engineering.com','','Pakkret','','Business Development Manager','Nonthaburi','11120','+66 2960 1038',0,'2015-01-27',0,'2015-01-27'),(177,'CDGZKURO01','Z.Kuroda (Thailand) Co., Ltd','1/24  Moo. 5 Rojana Industrial Park,','Khan-Ham','+66 96 416 2469','korawan@zkurodath.com','','U-Thai','159','Laboratory Engineer','Ayutthaya','13210','+66 3533 0066',0,'2015-01-27',0,'2015-01-27'),(178,'CDGไฟฟ้า01','Egat Mae Moh','800 Moo.6','Mae Moh ','+66 86 654 7601','sawek.c@egat.co.th','','Mae Moh ','','แผนกเคมี กองเชื้อเพลิงถ่านและน้ำ','Lumpang','52220','+66 5425 2331',0,'2015-01-27',0,'2015-01-27');
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
  `company_id` int(11) default NULL,
  `name` varchar(45) default NULL,
  `phone_number` varchar(45) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=177 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_customer_contract_person`
--

LOCK TABLES `m_customer_contract_person` WRITE;
/*!40000 ALTER TABLE `m_customer_contract_person` DISABLE KEYS */;
INSERT INTO `m_customer_contract_person` VALUES (1,4,'Choltinee Tangtreerat ','+66 81 170 7109'),(2,3,'Anusorn Chakkaew','+66 85 441 9193'),(3,15,'Witoon Ponglaohapun',''),(4,16,'Atthakorn Termpittayapaisit','+66 81 321 4264'),(5,5,'Waraporn Pholyiem',''),(6,6,'Kanokwan',''),(7,7,'Pisit Rujipichapornkul','+66 83 135 8998'),(8,8,'Ittthikorn Jinuntuya','+66 86 688 8341'),(9,9,'Yutthana','+66 86 565 7231'),(10,10,'Nongluk','+66 86 105 8722'),(11,11,'Waleepun Kaewpatch',''),(12,12,'Jeeranan Pimta',''),(13,13,'Onnisara Archariya',''),(14,14,'Saengtean',''),(15,22,'Martin Covell',''),(16,17,'Puntharee Sirojtantikorn',''),(17,18,'Runglawan Seeburth',''),(18,19,'Walaipon',''),(19,20,'Patpong  Boonraksah','+66 83 899 6346'),(20,21,'Panachon',''),(21,25,'Sukanya Prasertsung ','+66 89 920 3494'),(22,23,'Sukanya Chantaworakit ',''),(23,24,'Charnchai Jamrasfuangfoo',''),(24,26,'Bantita Takkhapiwat (Kate)','+66 85 111 6710'),(25,27,'Chairat Luepongpattana','+66 86 873 8006'),(26,28,'Jiraporn Somnuam',''),(27,29,'Nataporn  Phunnarungsi','+66 81 840 5083'),(28,30,'Vanida Keswong',''),(29,31,'Kanuengnit',''),(30,32,'Sunanta Sukpool','+66 89 066 3125'),(31,33,'Patchara Toprated','+66 84 094 4633'),(32,34,'Rungnapa Pasuriwong',''),(33,40,'Thiraporn Yaiying (Pu)',''),(34,35,'Sakranta Tonsanguan','+66 94 651 4664'),(35,36,'Warut Sutjaritjun','+66 86 125 5152'),(36,37,'Natnapat Methanorapat ',''),(37,38,'Kampol',''),(38,39,'Pacharathon Chueateaw',''),(39,41,'Patipat','+66 81 988 3996'),(40,178,'Yeamyuth  Manitras','+66 81 945 5833'),(41,42,'Wannuttee Malithong ','+66 86 993 2026'),(42,43,'Yuthtaphoom Silachan','+66 81-639 0394'),(43,44,'Aree Krataitong','+66 89 814 2276'),(44,45,'Usani','+66 87 9711959'),(45,46,'Kristoffer Clyde G. Magsino (Clyde)','+66 89 767 0202'),(46,47,'Pornpit Phetrak',''),(47,49,'Netnapa Y. ( Yui)','+66 81 555 8659'),(48,51,'Namphung Feepakprao','+66 86 972 6138 '),(49,48,'Daranee Phoyu',''),(50,50,'Surasak Kornnitikul','+66 86 030 0398'),(51,52,'Uthen Wiriyakasikon',''),(52,54,'Patthamon   Kittisophon ',''),(53,53,'Supansa  Noinamkam',''),(54,55,'Amornchai','+66 86 345 6553'),(55,56,'Sudarat (Art)','+66 89 900 9413'),(56,57,'Nitjawan Inthajakr ','+66 81 931 2166 '),(57,58,'Doungdao Phanthong',''),(58,59,'Sakchai','+66 81 840 5389'),(59,60,'Tula Chankana','+66 81 939 2808'),(60,61,'Patinya  Pansuk ',''),(61,62,'Sukanya Yujan (Su)','+66 87 201 9195 '),(62,63,'Sittiporn',''),(63,64,'Warunya','+66 90 961 3636'),(64,65,'Aniruth Uthaisawek',''),(65,66,'Suriam','+66 86 523 6555'),(66,67,'Supaktra Sa-Nguanrat','+66 81 754 9808'),(67,68,'Mana',''),(68,70,'Mana',''),(69,69,'Sarisanach Iamsripeng',''),(70,71,'Panachon',''),(71,72,'Sompong Suphap',''),(72,74,'Saleela','+66 86 320 6456'),(73,73,'Wuttichai  Chamnan','+66 85 071 1626'),(74,75,'Bancha',''),(75,76,'Sutthipong Itthipalangkul ','+66 85 109 5682 '),(76,77,'Dusit Suratham',''),(77,79,'Dusit Suratham',''),(78,78,'Jariya  Makan  (Sai)','+66 87 859 0620'),(79,80,'Kittichai Pinwiset',''),(80,81,'Teerawat',''),(81,82,'Charida',''),(82,83,'Pallapa Rattanawaraporn ',''),(83,84,'Daungrutai Boonyen',''),(84,85,'Suphoj Cholkujanan','+66 81 854 5843'),(85,86,'Wivornchai  Boonyon',''),(86,87,'Sutheera',''),(87,88,'Daungnapa',''),(88,89,'Noppawan Jomkamsing','+66 89 666 3097'),(89,90,'Nuantip Tanya','+66 81 694 3804'),(90,91,'Nithima Pimpijit','+66 87 517 8490'),(91,92,'Darunee Aussawasathien',''),(92,94,'Pornpimol',''),(93,93,'Sasipapha  Ruang U-Rai (Rin)','+66 89 923 3156'),(94,95,'Piyachat',''),(95,96,'Put Sae-Lew','+66 90 429 5861'),(96,97,'Jutharat   Phonkaew',''),(97,99,'Kittisak Pandaranandaka',''),(98,100,'Suwimon',''),(99,98,'Siranee Songsawas',''),(100,101,'Pratchapol Marutapun',''),(101,102,'Mattika Sri-On','+66 86 001 7870'),(102,103,'Jiratchaya Sae-Lim',''),(103,105,'Rungthiwa  Buatoom (Daeng)',''),(104,104,'Naruemon Pinthong',''),(105,106,'Somkid Pichi  ( Aoy ) ','+66 87 916 5343'),(106,107,'Alisa  Dararung',''),(107,108,'Nipaporn Muangnak ',''),(108,109,'Charnchai Jamrasfuangfoo','+66 89 922 8459'),(109,111,'Janeth A. Mallorca',''),(110,110,'Bongkot Chaipiwong (Pom)','+66 81 172 3247'),(111,112,'Aktana',''),(112,113,'Ammarin','+66 91 557 8690'),(113,114,'Numtip Sangchai',''),(114,115,'Somsak','+66 81 811 6057'),(115,116,'Hattaya Aranyanarth',''),(116,117,'Kittipong',''),(117,118,'Wilaiporn',''),(118,119,'Achara',''),(119,123,'Praphaporn  K. (อ้วน ) ',''),(120,120,'Daw Petchsri',''),(121,121,'Wijittra Mokchai(Angie)','+66 95 535 1448'),(122,122,'Kanok','+66 86 361  0647'),(123,124,'Jutima Phonchalard',''),(124,125,'Sakpaiboon Tajumpa',''),(125,126,'Suparat Yangsanthia',''),(126,127,'Kitti Kaewrattanapattama',''),(127,128,'Hunsa Opanuruk','+66 86 677 7697'),(128,129,'Chiraporn',''),(129,130,'Chanukid Siripakornchai','+66 81 648 5545 '),(130,131,'Pinanong','+66 89 734 3620'),(131,132,'Kantinunt Supsermpol','+66 86 977 7416'),(132,133,'Kantinunt Supsermpol','+66 86 977 7416'),(133,134,'Rattana  Bandasak',''),(134,135,'Somchok','+66 81 913 2684'),(135,140,'Sirichai Ratthanaphuripat','+66 90 198 7801 '),(136,136,'Worawun Thiwunnarak','+66 81 916 5824'),(137,137,'Sayfon  Wangkeeree ','+66 86 753 2179'),(138,138,'Pairoge Wongsakulchuen','+66 81 812 3967'),(139,139,'Nuntaporn Krasaesut',''),(140,141,'Angsana Phummee',''),(141,142,'Yhutapol  Tommahon','+66 82 120 5685     '),(142,143,'Nantakan Klongduangjit','+66 92 617 7789'),(143,144,'Wanyupa  Waikulpech','+66 85 482 9426'),(144,145,'Niramol','+66 89 244 7006'),(145,146,'Tassanee','+66 81 999 5913'),(146,149,'Rungrapee Boonmee',''),(147,148,'Rattiya Tangyoo ','+66 83 123 4684'),(148,150,'Noboru Hayakawa ','+66 80 603 2417'),(149,151,'Sophie Zhou',''),(150,152,'Aree  Boonpa','+66 81 714 9685'),(151,147,'Thawatchai',''),(152,155,'Kittisak',''),(153,156,'Udaporn  Phookduang (Gap)','+66 89 403 9293 '),(154,153,'Wendy Wee',''),(155,157,'Achari Saardiem',''),(156,159,'Patcharapong','+66 81 786 1639'),(157,158,'Patcharapong','+66 81 786 1639'),(158,167,'Gusmar Thungsupanich',''),(159,162,'Wassana','+66 86 546 3205'),(160,154,'Teeraphan','+66 81 825 3667'),(161,160,'Nattamon',''),(162,161,'Yutthana','+66 81 910 2013 '),(163,163,'Piyapong Inthaphophan',''),(164,164,'Thanawat Taweeprecharat ','+66 84 075 3668'),(165,165,'Somporn',''),(166,166,'Sakuna Chongkittisakul','+66 86 754 5268'),(167,168,'Kingfa Kitchainugool',''),(168,170,'Chonchalerm Suwannapho ','+66 81 863 3660'),(169,169,'Usani','+66 87 9711959'),(170,171,'Usani','+66 87 9711959'),(171,172,'Chitlrapat','+66 86 774 3233'),(172,173,'Thitima',''),(173,174,'Shousuke Takahashi',''),(174,175,'Kongsak Sanguanpong','+66 86 309 3330'),(175,177,'Korawan  Sowichai','+66 96 416 2469'),(176,176,'Sawek Chaisalee','+66 86 654 7601');
/*!40000 ALTER TABLE `m_customer_contract_person` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_role`
--

DROP TABLE IF EXISTS `m_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_role` (
  `ID` int(11) NOT NULL,
  `name` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_role`
--

LOCK TABLES `m_role` WRITE;
/*!40000 ALTER TABLE `m_role` DISABLE KEYS */;
INSERT INTO `m_role` VALUES (0,'Root','H'),(1,'Login','A'),(2,'Chemist','A'),(3,'SrChemist','A'),(4,'Admin','A'),(5,'LabManager','A');
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
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
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
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
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
-- Table structure for table `m_type_of_test`
--

DROP TABLE IF EXISTS `m_type_of_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_type_of_test` (
  `ID` int(11) NOT NULL auto_increment,
  `name` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_type_of_test`
--

LOCK TABLES `m_type_of_test` WRITE;
/*!40000 ALTER TABLE `m_type_of_test` DISABLE KEYS */;
INSERT INTO `m_type_of_test` VALUES (1,'NVR only','A'),(2,'FTIR only','A'),(3,'NVR & FTIR','A'),(4,'CVR (WD only)','A'),(5,'IC','A'),(6,'LPC','A'),(7,'Outgassing Test (DHS)','A'),(8,'GCMS','A'),(9,'MESA (NCST):(WD only)','A'),(10,'SEM/EDX','A'),(11,'Corrosion by Humidity Chamber','A'),(12,'Copper Wire (Seagate only)','A');
/*!40000 ALTER TABLE `m_type_of_test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_type_of_test_sub`
--

DROP TABLE IF EXISTS `m_type_of_test_sub`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_type_of_test_sub` (
  `ID` int(11) NOT NULL auto_increment,
  `type_of_test_id` int(11) default NULL,
  `name` varchar(45) default NULL,
  `status` varchar(1) default 'A',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_type_of_test_sub`
--

LOCK TABLES `m_type_of_test_sub` WRITE;
/*!40000 ALTER TABLE `m_type_of_test_sub` DISABLE KEYS */;
INSERT INTO `m_type_of_test_sub` VALUES (1,5,'Anion+NH4','A'),(2,5,'Anion','A'),(3,5,'Cation','A'),(4,6,'0.3','A'),(5,6,'0.5','A'),(6,6,'0.6','A'),(7,8,'4X Rinse (Seagate)','A'),(8,8,'Hydro Oil (Seagate)','A'),(9,8,'Organic residue (WD)','A'),(10,10,'HPA Tape Test','A'),(11,10,'HPA Filtration','A'),(12,10,'Swap','A'),(13,10,'Talc','A'),(14,10,'Contaminate','A');
/*!40000 ALTER TABLE `m_type_of_test_sub` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject`
--

DROP TABLE IF EXISTS `subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `subject` (
  `id` int(11) NOT NULL auto_increment,
  `subj_code` varchar(255) default NULL,
  `sbj_name` varchar(255) default NULL,
  `teacher_user_id` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject`
--

LOCK TABLES `subject` WRITE;
/*!40000 ALTER TABLE `subject` DISABLE KEYS */;
INSERT INTO `subject` VALUES (7,'1','1',1);
/*!40000 ALTER TABLE `subject` ENABLE KEYS */;
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
  `username` varchar(20) NOT NULL,
  `password` varchar(100) NOT NULL default '1234',
  `latest_login` datetime NOT NULL,
  `email` varchar(50) NOT NULL,
  `status` varchar(1) NOT NULL default 'A',
  `create_by` int(11) default NULL,
  `create_date` datetime default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_login`
--

LOCK TABLES `user_login` WRITE;
/*!40000 ALTER TABLE `user_login` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_profile`
--

DROP TABLE IF EXISTS `user_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_profile` (
  `id` int(11) NOT NULL auto_increment,
  `personal_card_id` varchar(13) NOT NULL,
  `personal_title` varchar(255) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `gender` varchar(5) NOT NULL,
  `birth_date` date NOT NULL,
  `address1` varchar(255) NOT NULL,
  `address2` varchar(255) NOT NULL,
  `sub_district` varchar(255) NOT NULL,
  `district` varchar(255) NOT NULL,
  `province` varchar(255) NOT NULL,
  `postal_code` varchar(5) NOT NULL,
  `phone` varchar(50) NOT NULL,
  `mobile` varchar(50) NOT NULL,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `personal_card_id` (`personal_card_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_profile`
--

LOCK TABLES `user_profile` WRITE;
/*!40000 ALTER TABLE `user_profile` DISABLE KEYS */;
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

-- Dump completed on 2015-01-27 23:37:38
