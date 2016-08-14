-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: faadb
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
-- Table structure for table `equipment`
--

DROP TABLE IF EXISTS `equipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipment_group_id` int(11) NOT NULL,
  `room_id` int(11) DEFAULT NULL,
  `name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `barcode` varchar(200) NOT NULL,
  `img_path` varchar(255) NOT NULL,
  `status` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `equipment_type_id` (`equipment_group_id`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `fk3_e2eg` FOREIGN KEY (`equipment_group_id`) REFERENCES `equipment_group` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=755 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment`
--

LOCK TABLES `equipment` WRITE;
/*!40000 ALTER TABLE `equipment` DISABLE KEYS */;
INSERT INTO `equipment` VALUES (378,1,5314,'1.7xTele Conversion Lens *for Sony VIDEO Camera','','404000004167','/images_equipment/1081.png','A'),(379,1,5314,'1.7xTele Conversion Lens *for Sony VIDEO Camera','','404000004168','/images_equipment/1081.png','A'),(380,2,5314,'7 inch LCD HDMI/HD-SD Monitor','','404000008709','/images_equipment/1132.png','A'),(381,2,5314,'7 inch LCD HDMI/HD-SD Monitor','','404000008710','/images_equipment/1132.png','A'),(382,2,5314,'7 inch LCD HDMI/HD-SD Monitor','','404000008711','/images_equipment/1132.png','A'),(383,3,5314,'8 inch LCD HDMI/HD-SD Monitor','','404000004716','','D'),(384,4,5314,'Arm','','904000003532','/images_equipment/1106.png','A'),(385,4,5314,'Arm','','904000003533','/images_equipment/1106.png','A'),(386,4,5314,'Arm','','904000003534','/images_equipment/1106.png','A'),(387,4,5314,'Arm','','904000003535','/images_equipment/1106.png','A'),(388,4,5314,'Arm','','904000003536','/images_equipment/1106.png','A'),(389,4,5314,'Arm','','904000003537','/images_equipment/1106.png','A'),(390,4,5314,'Arm','','904000003538','/images_equipment/1106.png','A'),(391,4,5314,'Arm','','904000003539','/images_equipment/1106.png','A'),(392,4,5314,'Arm','','904000003540','/images_equipment/1106.png','A'),(393,4,5314,'Arm','','904000003541','/images_equipment/1106.png','A'),(394,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008434','/images_equipment/1109.png','A'),(395,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008435','/images_equipment/1109.png','A'),(396,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008436','/images_equipment/1109.png','A'),(397,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008437','/images_equipment/1109.png','A'),(398,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008438','/images_equipment/1109.png','A'),(399,5,5314,'ARRi 300W PLUS PRESNEL + Light stand','','404000008439','/images_equipment/1109.png','A'),(400,6,5314,'ARRi 650W + Light stand','','404000008440','/images_equipment/1110.png','A'),(401,6,5314,'ARRi 650W + Light stand','','404000008441','/images_equipment/1110.png','A'),(402,6,5314,'ARRi 650W + Light stand','','404000008442','/images_equipment/1110.png','A'),(403,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005129','/upload/files/1453781898/File.jpg','A'),(404,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005130','/files/1453781958/File.jpg','A'),(405,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005131','/files/1453782052/File.jpg','A'),(406,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005132','/files/1453782177/File.jpg','A'),(407,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005133','/files/1453782229/File.jpg','A'),(408,7,5314,'ARRI 800W Set','ARRI 800W Set','404000005134','/files/1453782298/File.jpg','A'),(409,8,5314,'Audio Interface','','403000002365','/images_equipment/1131.png','A'),(410,9,5314,'Bag for Canon 5D.7D','','012310','/upload/files/1453688762/File.jpg','A'),(411,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012320','/upload/files/1453688789/File.jpg','A'),(412,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012330','/upload/files/1453688801/File.jpg','A'),(413,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012340','/upload/files/1453688812/File.jpg','A'),(414,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012350','','A'),(415,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012360','/upload/files/1453688835/File.jpg','A'),(416,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012370','/upload/files/1453688846/File.jpg','A'),(417,9,5314,'Bag for Canon 5D.7D','Bag for Canon 5D.7D','012380','/upload/files/1453688855/File.jpg','A'),(418,10,5314,'Black Flag 12x18 inch','','904000003520','/images_equipment/1122.png','A'),(419,10,5314,'Black Flag 12x18 inch','','904000003521','/images_equipment/1122.png','A'),(420,10,5314,'Black Flag 12x18 inch','','904000003522','/images_equipment/1122.png','A'),(421,10,5314,'Black Flag 12x18 inch','','904000003523','/images_equipment/1122.png','A'),(422,10,5314,'Black Flag 12x18 inch','','904000003524','/images_equipment/1122.png','A'),(423,10,5314,'Black Flag 12x18 inch','','904000003525','/images_equipment/1122.png','A'),(424,10,5314,'Black Flag 12x18 inch','','904000003526','/images_equipment/1122.png','A'),(425,10,5314,'Black Flag 12x18 inch','','904000003527','/images_equipment/1122.png','A'),(426,10,5314,'Black Flag 12x18 inch','','904000003528','/images_equipment/1122.png','A'),(427,10,5314,'Black Flag 12x18 inch','','904000003529','/images_equipment/1122.png','A'),(428,10,5314,'Black Flag 12x18 inch','','904000003530','/images_equipment/1122.png','A'),(429,10,5314,'Black Flag 12x18 inch','','904000003531','/images_equipment/1122.png','A'),(430,11,5314,'Black Flag 18x24 inch','','904000003508','/images_equipment/1121.png','A'),(431,11,5314,'Black Flag 18x24 inch','','904000003509','/images_equipment/1121.png','A'),(432,11,5314,'Black Flag 18x24 inch','','904000003510','/images_equipment/1121.png','A'),(433,11,5314,'Black Flag 18x24 inch','','904000003511','/images_equipment/1121.png','A'),(434,11,5314,'Black Flag 18x24 inch','','904000003512','/images_equipment/1121.png','A'),(435,11,5314,'Black Flag 18x24 inch','','904000003513','/images_equipment/1121.png','A'),(436,11,5314,'Black Flag 18x24 inch','','904000003514','/images_equipment/1121.png','A'),(437,11,5314,'Black Flag 18x24 inch','','904000003515','/images_equipment/1121.png','A'),(438,11,5314,'Black Flag 18x24 inch','','904000003516','/images_equipment/1121.png','A'),(439,11,5314,'Black Flag 18x24 inch','','904000003517','/images_equipment/1121.png','A'),(440,11,5314,'Black Flag 18x24 inch','','904000003518','/images_equipment/1121.png','A'),(441,11,5314,'Black Flag 18x24 inch','','904000003519','/images_equipment/1121.png','A'),(442,12,5314,'Black Flag 24x30 inch','','904000003488','/images_equipment/1119.png','A'),(443,12,5314,'Black Flag 24x30 inch','','904000003489','/images_equipment/1119.png','A'),(444,12,5314,'Black Flag 24x30 inch','','904000003490','/images_equipment/1119.png','A'),(445,12,5314,'Black Flag 24x30 inch','','904000003491','/images_equipment/1119.png','A'),(446,12,5314,'Black Flag 24x30 inch','','904000003492','/images_equipment/1119.png','A'),(447,12,5314,'Black Flag 24x30 inch','','904000003493','/images_equipment/1119.png','A'),(448,12,5314,'Black Flag 24x30 inch','','904000003494','/images_equipment/1119.png','A'),(449,12,5314,'Black Flag 24x30 inch','','904000003495','/images_equipment/1119.png','A'),(450,13,5314,'Black Flag 24x36 inch','','904000003496','/images_equipment/1120.png','A'),(451,13,5314,'Black Flag 24x36 inch','','904000003497','/images_equipment/1120.png','A'),(452,13,5314,'Black Flag 24x36 inch','','904000003498','/images_equipment/1120.png','A'),(453,13,5314,'Black Flag 24x36 inch','','904000003499','/images_equipment/1120.png','A'),(454,13,5314,'Black Flag 24x36 inch','','904000003500','/images_equipment/1120.png','A'),(455,13,5314,'Black Flag 24x36 inch','','904000003501','/images_equipment/1120.png','A'),(456,13,5314,'Black Flag 24x36 inch','','904000003502','/images_equipment/1120.png','A'),(457,13,5314,'Black Flag 24x36 inch','','904000003503','/images_equipment/1120.png','A'),(458,13,5314,'Black Flag 24x36 inch','','904000003504','/images_equipment/1120.png','A'),(459,13,5314,'Black Flag 24x36 inch','','904000003505','/images_equipment/1120.png','A'),(460,13,5314,'Black Flag 24x36 inch','','904000003506','/images_equipment/1120.png','A'),(461,13,5314,'Black Flag 24x36 inch','','904000003507','/images_equipment/1120.png','A'),(462,14,5314,'Black Flag 30x36 inch','','404000004557','/images_equipment/1116.png','A'),(463,14,5314,'Black Flag 30x36 inch','','404000004558','/images_equipment/1116.png','A'),(464,14,5314,'Black Flag 30x36 inch','','404000004559','/images_equipment/1116.png','A'),(465,14,5314,'Black Flag 30x36 inch','','404000004560','/images_equipment/1116.png','A'),(466,14,5314,'Black Flag 30x36 inch','','404000004561','/images_equipment/1116.png','A'),(467,14,5314,'Black Flag 30x36 inch','','404000004562','/images_equipment/1116.png','A'),(468,14,5314,'Black Flag 30x36 inch','','404000004563','/images_equipment/1116.png','A'),(469,14,5314,'Black Flag 30x36 inch','','404000004564','/images_equipment/1116.png','A'),(470,14,5314,'Black Flag 30x36 inch','','404000004565','/images_equipment/1116.png','A'),(471,14,5314,'Black Flag 30x36 inch','','404000004566','/images_equipment/1116.png','A'),(472,14,5314,'Black Flag 30x36 inch','','404000004567','/images_equipment/1116.png','A'),(473,14,5314,'Black Flag 30x36 inch','','404000004568','/images_equipment/1116.png','A'),(474,15,5314,'Boom Microphone with case','','404000009090','/upload/files/1454550943/File.jpg','A'),(475,15,5314,'Boom Microphone with case','','404000005111','/upload/files/1454550969/File.jpg','A'),(476,16,5314,'C-stand','','404000004547','/images_equipment/1105.png','A'),(477,16,5314,'C-stand','','404000004548','/images_equipment/1105.png','A'),(478,16,5314,'C-stand','','404000004549','/images_equipment/1105.png','A'),(479,16,5314,'C-stand','','404000004550','/images_equipment/1105.png','A'),(480,16,5314,'C-stand','','404000004551','/images_equipment/1105.png','A'),(481,16,5314,'C-stand','','404000004552','/images_equipment/1105.png','A'),(482,16,5314,'C-stand','','404000004553','/images_equipment/1105.png','A'),(483,16,5314,'C-stand','','404000004554','/images_equipment/1105.png','A'),(484,16,5314,'C-stand','','404000004555','/images_equipment/1105.png','A'),(485,16,5314,'C-stand','','404000004556','/images_equipment/1105.png','A'),(486,17,5314,'Canon  5D Mark III(BODY)','','404000008529','/images_equipment/1075.png','A'),(487,17,5314,'Canon  5D Mark III(BODY)','','404000008530','/images_equipment/1075.png','A'),(488,17,5314,'Canon  5D Mark III(BODY)','','404000010586','','A'),(489,17,5314,'Canon  5D Mark III(BODY)','','404000010587','','A'),(490,17,5314,'Canon  5D Mark III(BODY)','','404000010588','','A'),(491,17,5314,'Canon  5D Mark III(BODY)','','404000010589','','A'),(492,17,5314,'Canon  5D Mark III(BODY)','','404000010590','','A'),(493,18,5314,'Canon EOS 550D(BODY)','','404000004699','/images_equipment/1079.png','A'),(494,18,5314,'Canon EOS 550D(BODY)','','404000004700','/images_equipment/1079.png','A'),(495,19,5314,'Canon EOS 5D Mark III + 24-105mm. F4','','404000008820','/upload/files/1452823184/File.png','A'),(496,19,5314,'Canon EOS 5D Mark III + 24-105mm. F4','','404000007619','/upload/files/1452823194/File.png','A'),(497,19,5314,'Canon EOS 5D Mark III + 24-105mm. F4','','404000007620','/upload/files/1452823206/File.png','A'),(498,20,5314,'Canon EOS 5D MKII(BODY)','','404000005136','/images_equipment/1076.png','A'),(499,20,5314,'Canon EOS 5D MKII(BODY)','','404000004697','','D'),(500,21,5314,'Canon EOS 6D + 24-105mm. F4','','404000010591','/upload/files/1452823027/File.png','A'),(501,21,5314,'Canon EOS 6D + 24-105mm. F4','','404000010592','/upload/files/1452823040/File.png','A'),(502,21,5314,'Canon EOS 6D + 24-105mm. F4','','404000010593','/upload/files/1452823051/File.png','A'),(503,21,5314,'Canon EOS 6D + 24-105mm. F4','','404000010594','/upload/files/1452823062/File.png','A'),(504,21,5314,'Canon EOS 6D + 24-105mm. F4','','404000010595','/upload/files/1452823072/File.png','A'),(505,22,5314,'Canon EOS 7D(BODY)','','404000004698','/images_equipment/1078.png','A'),(506,22,5314,'Canon EOS 7D(BODY)','','404000005137','/images_equipment/1078.png','A'),(507,23,5314,'Car mount Tripod','','404000007248','/images_equipment/1099.png','A'),(508,24,5314,'Condenser Microphone','','404000004529','/images_equipment/1127.png','A'),(509,25,5314,'Converter Lens-30/37mm. *for Sony VIDEO Camera','','404000004163','/images_equipment/1080.png','A'),(510,25,5314,'Converter Lens-30/37mm. *for Sony VIDEO Camera','','404000004164','/images_equipment/1080.png','A'),(511,25,5314,'Converter Lens-30/37mm. *for Sony VIDEO Camera','','404000004165','/images_equipment/1080.png','A'),(512,25,5314,'Converter Lens-30/37mm. *for Sony VIDEO Camera','','404000004166','/images_equipment/1080.png','A'),(513,26,5314,'Dedo light 150 W + Light stand','','404000008431','/images_equipment/1108.png','A'),(514,26,5314,'Dedo light 150 W + Light stand','','404000008432','/images_equipment/1108.png','A'),(515,26,5314,'Dedo light 150 W + Light stand','','404000008433','/images_equipment/1108.png','A'),(516,27,5314,'Director Viewfinder','','404000004595','/images_equipment/1142.png','A'),(517,28,5314,'Dolly 75 *Use with Tripod spreader','','02111','/upload/files/1454922368/File.jpg','A'),(518,28,5314,'Dolly 75 *Use with Tripod spreader','','02112','/upload/files/1454922382/File.jpg','A'),(519,28,5314,'Dolly 75 *Use with Tripod spreader','','02113','/upload/files/1454922400/File.jpg','A'),(520,29,5314,'Door away Dolly','','404000004533','/images_equipment/1134.png','A'),(521,30,5314,'Flag Roadrags 18x24 inch','','904000003542','','D'),(522,30,5314,'Flag Roadrags 18x24 inch','','904000003543','','D'),(523,30,5314,'Flag Roadrags 18x24 inch','','904000003544','','D'),(524,30,5314,'Flag Roadrags 18x24 inch','','904000003545','','D'),(525,31,5314,'Gasoline Generator 220 V','','403000003726','/images_equipment/1141.png','A'),(526,32,5314,'Headphone ','','404000004523','/images_equipment/1126.png','A'),(527,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004573','/images_equipment/1118.png','A'),(528,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004574','/images_equipment/1118.png','A'),(529,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004575','/images_equipment/1118.png','A'),(530,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004576','/images_equipment/1118.png','A'),(531,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004577','/images_equipment/1118.png','A'),(532,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004578','/images_equipment/1118.png','A'),(533,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004579','/images_equipment/1118.png','A'),(534,33,5314,'High Temperature Metal Black Flag 24x36 inch','','404000004580','/images_equipment/1118.png','A'),(535,34,5314,'Hot button for tracks 4PSC/SET','','404000004534','/images_equipment/1135.png','A'),(536,35,5314,'I Track Dock','I Track Dock','403000005688','/upload/files/1452825213/File.jpg','A'),(537,36,5314,'Jib Crane 14 feet','','404000007242','/images_equipment/1103.png','A'),(538,36,5314,'Jib Crane 14 feet','','404000007243','/images_equipment/1103.png','A'),(539,37,5314,'Jib Crane 9 feet','','404000007244','/images_equipment/1104.png','A'),(540,37,5314,'Jib Crane 9 feet','','404000007245','/images_equipment/1104.png','A'),(541,38,5314,'KINO FLO GRAFFERKIT','','404000004726','/images_equipment/1111.png','A'),(542,38,5314,'KINO FLO GRAFFERKIT','','404000004727','/images_equipment/1111.png','A'),(543,39,5314,'KINO FLO Interview KIT','','404000004728','/images_equipment/1112.png','A'),(544,40,5314,'LED LIGHT + Light Stand','','404000008419','/images_equipment/1107.png','A'),(545,40,5314,'LED LIGHT + Light Stand','','404000008420','/images_equipment/1107.png','A'),(546,40,5314,'LED LIGHT + Light Stand','','404000008421','/images_equipment/1107.png','A'),(547,40,5314,'LED LIGHT + Light Stand','','404000008422','/images_equipment/1107.png','A'),(548,40,5314,'LED LIGHT + Light Stand','','404000008423','/images_equipment/1107.png','A'),(549,40,5314,'LED LIGHT + Light Stand','','404000008424','/images_equipment/1107.png','A'),(550,40,5314,'LED LIGHT + Light Stand','','404000008425','/images_equipment/1107.png','A'),(551,40,5314,'LED LIGHT + Light Stand','','404000008426','/images_equipment/1107.png','A'),(552,40,5314,'LED LIGHT + Light Stand','','404000008427','/images_equipment/1107.png','A'),(553,40,5314,'LED LIGHT + Light Stand','','404000008428','/images_equipment/1107.png','A'),(554,40,5314,'LED LIGHT + Light Stand','','404000008429','/images_equipment/1107.png','A'),(555,40,5314,'LED LIGHT + Light Stand','','404000008430','/images_equipment/1107.png','A'),(556,41,5314,'Lens 14-45 mm. F/3.5-F5.6 ASPH for Panasonic Video Camera','','404000009115','/images_equipment/1088.png','A'),(557,42,5314,'Lens 35 mm.F1.4 DG HSM','','404000009116','/upload/files/1452823350/File.jpg','A'),(558,43,5314,'Lens 45-200 mm. F/4.0-F5.6 for Panasonic Video Camera','','404000009114','/images_equipment/1087.png','A'),(559,44,5314,'Lens Adapter EOS-Micro4/3 W/IRIS for Panasonic Video Camera','','404000005068','/images_equipment/1093.png','A'),(560,45,5314,'Lens Canon EF 24-105mm. F4','','404000010596','/images_equipment/1084.png','A'),(561,45,5314,'Lens Canon EF 24-105mm. F4','','404000010597','/images_equipment/1084.png','A'),(562,45,5314,'Lens Canon EF 24-105mm. F4','','404000010598','/images_equipment/1084.png','A'),(563,45,5314,'Lens Canon EF 24-105mm. F4','','404000010599','/images_equipment/1084.png','A'),(564,45,5314,'Lens Canon EF 24-105mm. F4','','404000010600','/images_equipment/1084.png','A'),(565,45,5314,'Lens Canon EF 24-105mm. F4','','404000010601','/images_equipment/1084.png','A'),(566,45,5314,'Lens Canon EF 24-105mm. F4','','404000010602','/images_equipment/1084.png','A'),(567,45,5314,'Lens Canon EF 24-105mm. F4','','404000010603','/images_equipment/1084.png','A'),(568,45,5314,'Lens Canon EF 24-105mm. F4','','404000010604','/images_equipment/1084.png','A'),(569,45,5314,'Lens Canon EF 24-105mm. F4','','404000010605','/images_equipment/1084.png','A'),(570,45,5314,'Lens Canon EF 24-105mm. F4','','404000010606','/images_equipment/1084.png','A'),(571,46,5314,'Lens Canon EF 24-70mm. F2.8L IS2USM','','404000004709','/images_equipment/1083.png','A'),(572,46,5314,'Lens Canon EF 24-70mm. F2.8L IS2USM','','404000004710','/images_equipment/1083.png','A'),(573,46,5314,'Lens Canon EF 24-70mm. F2.8L IS2USM','','404000004712','/images_equipment/1083.png','A'),(574,46,5314,'Lens Canon EF 24-70mm. F2.8L IS2USM','','404000009124','/images_equipment/1083.png','A'),(575,47,5314,'Lens Canon EF 50mm. F1.8','','904000003741','/images_equipment/1086.png','A'),(576,47,5314,'Lens Canon EF 50mm. F1.8','','904000003742','/images_equipment/1086.png','A'),(577,48,5314,'Lens Canon EF 70-200mm. F2.8L IS2USM','','404000004705','/images_equipment/1082.png','A'),(578,48,5314,'Lens Canon EF 70-200mm. F2.8L IS2USM','','404000004706','/images_equipment/1082.png','A'),(579,48,5314,'Lens Canon EF 70-200mm. F2.8L IS2USM','','404000004708','/images_equipment/1082.png','A'),(580,48,5314,'Lens Canon EF 70-200mm. F2.8L IS2USM','','404000009125','/images_equipment/1082.png','A'),(581,49,5314,'Lens EF 100 mm. F2.8L Macro IS USM','','404000009122','/images_equipment/1092.png','A'),(582,49,5314,'Lens EF 100 mm. F2.8L Macro IS USM','','404000009123','/images_equipment/1092.png','A'),(583,50,5314,'Lens EF 50 mm. F1.4 USM','','404000009118','/images_equipment/1090.png','A'),(584,50,5314,'Lens EF 50 mm. F1.4 USM','','404000009119','/images_equipment/1090.png','A'),(585,51,5314,'Lens EF 85 mm. F1.2LII USM','','404000009120','/images_equipment/1091.png','A'),(586,51,5314,'Lens EF 85 mm. F1.2LII USM','','404000009121','/images_equipment/1091.png','A'),(587,52,5314,'Lens TOKINA 11-16mm. F2.8IF For Canon Mount','','404000009117','/images_equipment/1085.png','A'),(588,52,5314,'Lens TOKINA 11-16mm. F2.8IF For Canon Mount','','404000004713','/images_equipment/1085.png','A'),(589,52,5314,'Lens TOKINA 11-16mm. F2.8IF For Canon Mount','','404000004714','/images_equipment/1085.png','A'),(590,53,5314,'Long-gun Microphone','','404000004583','/upload/files/1454551038/File.jpg','A'),(591,54,5314,'Meat Axe Black Flag 24x48 inch','','404000004569','/images_equipment/1117.png','A'),(592,54,5314,'Meat Axe Black Flag 24x48 inch','','404000004570','/images_equipment/1117.png','A'),(593,54,5314,'Meat Axe Black Flag 24x48 inch','','404000004571','/images_equipment/1117.png','A'),(594,54,5314,'Meat Axe Black Flag 24x48 inch','','404000004572','/images_equipment/1117.png','A'),(595,55,5314,'Nikon FM 10 Film Camera ','','404000004092','/upload/files/1453078539/File.JPG','A'),(596,55,5314,'Nikon FM 10 Film Camera ','','404000004093','/images_equipment/1114.png','A'),(597,55,5314,'Nikon FM 10 Film Camera ','','404000004094','/images_equipment/1114.png','A'),(598,55,5314,'Nikon FM 10 Film Camera ','','404000004095','/images_equipment/1114.png','A'),(599,55,5314,'Nikon FM 10 Film Camera ','','404000004096','/images_equipment/1114.png','A'),(600,55,5314,'Nikon FM 10 Film Camera ','','404000004097','/images_equipment/1114.png','A'),(601,55,5314,'Nikon FM 10 Film Camera ','','404000004098','/images_equipment/1114.png','A'),(602,55,5314,'Nikon FM 10 Film Camera ','','404000004099','/images_equipment/1114.png','A'),(603,55,5314,'Nikon FM 10 Film Camera ','','404000004100','/images_equipment/1114.png','A'),(604,55,5314,'Nikon FM 10 Film Camera ','','404000004101','/images_equipment/1114.png','A'),(605,55,5314,'Nikon FM 10 Film Camera ','','404000004102','/images_equipment/1114.png','A'),(606,55,5314,'Nikon FM 10 Film Camera ','','404000004103','/images_equipment/1114.png','A'),(607,55,5314,'Nikon FM 10 Film Camera ','','404000004104','/images_equipment/1114.png','A'),(608,55,5314,'Nikon FM 10 Film Camera ','','404000004105','/images_equipment/1114.png','A'),(609,55,5314,'Nikon FM 10 Film Camera ','','404000004106','/images_equipment/1114.png','A'),(610,55,5314,'Nikon FM 10 Film Camera ','','404000004107','/images_equipment/1114.png','A'),(611,55,5314,'Nikon FM 10 Film Camera ','','404000004108','/images_equipment/1114.png','A'),(612,55,5314,'Nikon FM 10 Film Camera ','','404000004109','/images_equipment/1114.png','A'),(613,55,5314,'Nikon FM 10 Film Camera ','','404000004110','/images_equipment/1114.png','A'),(614,55,5314,'Nikon FM 10 Film Camera ','','404000004111','/images_equipment/1114.png','A'),(615,55,5314,'Nikon FM 10 Film Camera ','','404000004112','/images_equipment/1114.png','A'),(616,55,5314,'Nikon FM 10 Film Camera ','','404000004113','/images_equipment/1114.png','A'),(617,55,5314,'Nikon FM 10 Film Camera ','','404000004114','/images_equipment/1114.png','A'),(618,55,5314,'Nikon FM 10 Film Camera ','','404000004115','/images_equipment/1114.png','A'),(619,55,5314,'Nikon FM 10 Film Camera ','','404000004116','/images_equipment/1114.png','A'),(620,55,5314,'Nikon FM 10 Film Camera ','','404000004117','/images_equipment/1114.png','A'),(621,55,5314,'Nikon FM 10 Film Camera ','','404000004118','/images_equipment/1114.png','A'),(622,55,5314,'Nikon FM 10 Film Camera ','','404000004119','/images_equipment/1114.png','A'),(623,56,5314,'Panasonic AG-AF102EN Camcorder','','404000009113','/images_equipment/1096.png','A'),(624,56,5314,'Panasonic AG-AF102EN Camcorder','','404000005066','/images_equipment/1096.png','A'),(625,57,5314,'Pen Tablet','','410000026525','/upload/files/1452825823/File.jpg','A'),(626,57,5314,'Pen Tablet','','410000026526','','A'),(627,57,5314,'Pen Tablet','','410000026527','','A'),(628,57,5314,'Pen Tablet','','410000026528','','A'),(629,57,5314,'Pen Tablet','','410000026529','','A'),(630,57,5314,'Pen Tablet','','410000026530','','A'),(631,57,5314,'Pen Tablet','','410000026531','','A'),(632,57,5314,'Pen Tablet','','410000026532','','A'),(633,57,5314,'Pen Tablet','','410000026533','','A'),(634,57,5314,'Pen Tablet','','410000026534','','A'),(635,57,5314,'Pen Tablet','','410000026535','','A'),(636,57,5314,'Pen Tablet','','410000026536','','A'),(637,57,5314,'Pen Tablet','','410000026537','','A'),(638,57,5314,'Pen Tablet','','410000026538','','A'),(639,57,5314,'Pen Tablet','','410000026539','','A'),(640,57,5314,'Pen Tablet','','410000026540','','A'),(641,57,5314,'Pen Tablet','','410000026541','','A'),(642,57,5314,'Pen Tablet','','410000026542','','A'),(643,57,5314,'Pen Tablet','','410000026543','','A'),(644,57,5314,'Pen Tablet','','410000026544','','A'),(645,57,5314,'Pen Tablet','','410000026545','','A'),(646,57,5314,'Pen Tablet','','410000026546','','A'),(647,57,5314,'Pen Tablet','','410000026547','','A'),(648,57,5314,'Pen Tablet','','410000026548','','A'),(649,58,5314,'Recorder Sony GV-HD700E','','404000004454','/images_equipment/1140.png','A'),(650,59,5314,'Sachtler FSB 4 TRIPOD','แทนของเดิมที่หาย','404000010634','/images_equipment/0.png','A'),(651,60,5314,'Screen Motor','','404000004510','/upload/files/1452825895/File.jpg','A'),(652,61,5314,'Shot-gun Microphone Set With Fabric BAG','Shot-gun Microphone Set','404000013726','/upload/files/1456890138/File.jpg','A'),(653,62,5314,'Shoulder Brace','','404000004042','/images_equipment/1100.png','A'),(654,62,5314,'Shoulder Brace','','404000004043','/images_equipment/1100.png','A'),(655,63,5314,'Shoulder Stabilizer','','404000007246','/images_equipment/1098.png','A'),(656,63,5314,'Shoulder Stabilizer','','404000007247','/images_equipment/1098.png','A'),(657,64,5314,'Silk for Flag 18x12','Silk for Flag 18x12','5404200500066','/upload/files/1452824066/File.jpg','A'),(658,64,5314,'Silk for Flag 18x12','Silk for Flag 18x12','5404200500056','/upload/files/1452824089/File.jpg','A'),(659,65,5314,'Silk for Flag 24x18','Silk for Flag 24x18','5404200500046','/upload/files/1452824098/File.jpg','A'),(660,65,5314,'Silk for Flag 24x18','Silk for Flag 24x18','5404200500036','/upload/files/1452824110/File.jpg','A'),(661,66,5314,'Silk for Flag 36x24','Silk for Flag 36x24','5404200500026','/upload/files/1452824191/File.jpg','A'),(662,66,5314,'Silk for Flag 36x24','Silk for Flag 36x24','5404200500016','/upload/files/1452824202/File.jpg','A'),(663,67,5314,'Sony HVR-DR60 Hard Disk Units','','404000004161','/images_equipment/1139.png','A'),(664,67,5314,'Sony HVR-DR60 Hard Disk Units','','404000004162','/images_equipment/1139.png','A'),(665,68,5314,'Sony HVR-HD1000P Camcorder','','404000004051','/images_equipment/1095.png','A'),(666,68,5314,'Sony HVR-HD1000P Camcorder','','404000004052','/images_equipment/1095.png','A'),(667,68,5314,'Sony HVR-HD1000P Camcorder','','404000004053','/images_equipment/1095.png','A'),(668,68,5314,'Sony HVR-HD1000P Camcorder','','404000004054','/images_equipment/1095.png','A'),(669,68,5314,'Sony HVR-HD1000P Camcorder','','404000004055','/images_equipment/1095.png','A'),(670,68,5314,'Sony HVR-HD1000P Camcorder','','404000004056','/images_equipment/1095.png','A'),(671,68,5314,'Sony HVR-HD1000P Camcorder','','404000004057','/images_equipment/1095.png','A'),(672,68,5314,'Sony HVR-HD1000P Camcorder','','404000004058','/images_equipment/1095.png','A'),(673,68,5314,'Sony HVR-HD1000P Camcorder','','404000004059','/images_equipment/1095.png','A'),(674,68,5314,'Sony HVR-HD1000P Camcorder','','404000004060','/images_equipment/1095.png','A'),(675,68,5314,'Sony HVR-HD1000P Camcorder','','404000004061','/images_equipment/1095.png','A'),(676,69,5314,'Sony HVR-MRC1K CF Memory Recording Unit','','404000004159','/images_equipment/1138.png','A'),(677,69,5314,'Sony HVR-MRC1K CF Memory Recording Unit','','404000004160','/images_equipment/1138.png','A'),(678,70,5314,'Sony HVR-Z7P HDV Camcorder','','404000004047','/images_equipment/1094.png','A'),(679,70,5314,'Sony HVR-Z7P HDV Camcorder','','404000004048','/images_equipment/1094.png','A'),(680,70,5314,'Sony HVR-Z7P HDV Camcorder','','404000004049','/images_equipment/1094.png','A'),(681,70,5314,'Sony HVR-Z7P HDV Camcorder','','404000004050','/images_equipment/1094.png','A'),(682,71,5314,'Sony PD-170P(3CCD Camera DVCAM) + Remote','','404000004041','/upload/files/1452824646/File.jpg','A'),(683,72,5314,'Studio flash Set','','404000005135','/upload/files/1452847885/File.jpg','A'),(684,73,5314,'Track Curve for 20 FT Diameter','','404000004587','/images_equipment/1137.png','A'),(685,73,5314,'Track Curve for 20 FT Diameter','','404000004588','/images_equipment/1137.png','A'),(686,73,5314,'Track Curve for 20 FT Diameter','','404000004589','/images_equipment/1137.png','A'),(687,73,5314,'Track Curve for 20 FT Diameter','','404000004590','/images_equipment/1137.png','A'),(688,73,5314,'Track Curve for 20 FT Diameter','','404000004591','/images_equipment/1137.png','A'),(689,73,5314,'Track Curve for 20 FT Diameter','','404000004592','/images_equipment/1137.png','A'),(690,73,5314,'Track Curve for 20 FT Diameter','','404000004593','/images_equipment/1137.png','A'),(691,73,5314,'Track Curve for 20 FT Diameter','','404000004594','/images_equipment/1137.png','A'),(692,74,5314,'Track Straight 4 ft','','404000004535','/images_equipment/1136.png','A'),(693,74,5314,'Track Straight 4 ft','','404000004536','/images_equipment/1136.png','A'),(694,74,5314,'Track Straight 4 ft','','404000004537','/images_equipment/1136.png','A'),(695,74,5314,'Track Straight 4 ft','','404000004538','/images_equipment/1136.png','A'),(696,74,5314,'Track Straight 4 ft','','404000004539','/images_equipment/1136.png','A'),(697,74,5314,'Track Straight 4 ft','','404000004540','/images_equipment/1136.png','A'),(698,74,5314,'Track Straight 4 ft','','404000004541','/images_equipment/1136.png','A'),(699,74,5314,'Track Straight 4 ft','','404000004542','/images_equipment/1136.png','A'),(700,74,5314,'Track Straight 4 ft','','404000004543','/images_equipment/1136.png','A'),(701,74,5314,'Track Straight 4 ft','','404000004544','/images_equipment/1136.png','A'),(702,74,5314,'Track Straight 4 ft','','404000004545','/images_equipment/1136.png','A'),(703,74,5314,'Track Straight 4 ft','','404000004546','/images_equipment/1136.png','A'),(704,75,5314,'Tripod ','','404000009092','/images_equipment/1101.png','A'),(705,75,5314,'Tripod ','','404000009093','/images_equipment/1101.png','A'),(706,75,5314,'Tripod ','','404000009094','/images_equipment/1101.png','A'),(707,75,5314,'Tripod ','','404000009095','/images_equipment/1101.png','A'),(708,76,5314,'Tripod with Spreader ','','404000005112','/images_equipment/1102.png','A'),(709,76,5314,'Tripod with Spreader ','','404000005113','/images_equipment/1102.png','A'),(710,76,5314,'Tripod with Spreader ','','404000005114','/images_equipment/1102.png','A'),(711,76,5314,'Tripod with Spreader ','','404000005115','/images_equipment/1102.png','A'),(712,76,5314,'Tripod with Spreader ','','404000005116','/images_equipment/1102.png','A'),(713,76,5314,'Tripod with Spreader ','','404000005117','/images_equipment/1102.png','A'),(714,76,5314,'Tripod with Spreader ','','404000005118','/images_equipment/1102.png','A'),(715,76,5314,'Tripod with Spreader ','','404000005119','/images_equipment/1102.png','A'),(716,76,5314,'Tripod with Spreader ','','404000005120','/images_equipment/1102.png','A'),(717,76,5314,'Tripod with Spreader ','','404000005121','/images_equipment/1102.png','A'),(718,77,5314,'Visualizer LUMENS PS','','404000004344','/upload/files/1452825868/File.jpg','A'),(719,78,5314,'Wireless MIC for Studio','Wireless MIC for Studio','01111','/upload/files/1452824967/File.jpg','A'),(720,78,5314,'Wireless MIC for Studio','Wireless MIC for Studio','01112','/upload/files/1452825027/File.jpg','A'),(721,78,5314,'Wireless MIC for Studio','Wireless MIC for Studio','01113','/upload/files/1452825040/File.jpg','A'),(722,78,5314,'Wireless MIC for Studio','Wireless MIC for Studio','01114','/upload/files/1452825052/File.jpg','A'),(723,79,5314,'Wireless Microphone','Wireless Microphone','0001','/upload/files/1452825103/File.jpg','A'),(724,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0002','','A'),(725,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0003','','A'),(726,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0004','','A'),(727,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0005','','A'),(728,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0006','','A'),(729,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0007','','A'),(730,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0008','','A'),(731,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0009','','A'),(732,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0010','','A'),(733,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0011','','A'),(734,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0012','','A'),(735,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0013','','A'),(736,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0014','','A'),(737,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0015','','A'),(738,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0016','','A'),(739,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0017','','A'),(740,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0018','','A'),(741,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0019','','A'),(742,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0020','','A'),(743,79,5314,'Wireless Microphone','Wireless Microphone\r\n','0021','','A'),(744,79,5314,'Wireless Microphone','Wireless Microphone','0022','','A'),(745,79,5314,'Wireless Microphone','Wireless Microphone','0023','','A'),(746,79,5314,'Wireless Microphone','Wireless Microphone','0024','','A'),(747,79,5314,'Wireless Microphone','Wireless Microphone','0025','','A'),(748,80,5314,'XLR Microphone for Panasonic Video Camera','','404000005067','/images_equipment/1130.png','A'),(749,81,5314,'ZOOM 4 Track Digital Recorder','','403000002463','/images_equipment/1124.png','A'),(750,81,5314,'ZOOM 4 Track Digital Recorder','','403000002480','/images_equipment/1124.png','A'),(751,82,5314,'ZOOM 6 Track Digital Recorder','','403000004325','/images_equipment/1125.png','A'),(752,82,5314,'ZOOM 6 Track Digital Recorder','','403000004326','/images_equipment/1125.png','A'),(753,82,5314,'ZOOM 6 Track Digital Recorder','','403000004327','/images_equipment/1125.png','A'),(754,82,5314,'ZOOM 6 Track Digital Recorder','','403000004328','/images_equipment/1125.png','A');
/*!40000 ALTER TABLE `equipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipment_group`
--

DROP TABLE IF EXISTS `equipment_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipment_group` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipment_type_id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `img_path` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_eg2ey_idx` (`equipment_type_id`),
  CONSTRAINT `fk_eg2ey` FOREIGN KEY (`equipment_type_id`) REFERENCES `equipment_type` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=83 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment_group`
--

LOCK TABLES `equipment_group` WRITE;
/*!40000 ALTER TABLE `equipment_group` DISABLE KEYS */;
INSERT INTO `equipment_group` VALUES (1,12,'1.7xTele Conversion Lens *for Sony VIDEO Camera','/images_equipment/1081.png'),(2,30,'7 inch LCD HDMI/HD-SD Monitor','/images_equipment/1132.png'),(3,30,'8 inch LCD HDMI/HD-SD Monitor',''),(4,15,'Arm','/images_equipment/1106.png'),(5,16,'ARRi 300W PLUS PRESNEL + Light stand','/images_equipment/1109.png'),(6,16,'ARRi 650W + Light stand','/images_equipment/1110.png'),(7,16,'ARRI 800W Set','/upload/files/1453781898/File.jpg'),(8,29,'Audio Interface','/images_equipment/1131.png'),(9,32,'Bag for Canon 5D.7D','/upload/files/1453688762/File.jpg'),(10,18,'Black Flag 12x18 inch','/images_equipment/1122.png'),(11,18,'Black Flag 18x24 inch','/images_equipment/1121.png'),(12,18,'Black Flag 24x30 inch','/images_equipment/1119.png'),(13,18,'Black Flag 24x36 inch','/images_equipment/1120.png'),(14,18,'Black Flag 30x36 inch','/images_equipment/1116.png'),(15,29,'Boom Microphone with case','/upload/files/1454550943/File.jpg'),(16,15,'C-stand','/images_equipment/1105.png'),(17,11,'Canon  5D Mark III(BODY)','/images_equipment/1075.png'),(18,11,'Canon EOS 550D(BODY)','/images_equipment/1079.png'),(19,11,'Canon EOS 5D Mark III + 24-105mm. F4','/upload/files/1452823184/File.png'),(20,11,'Canon EOS 5D MKII(BODY)','/images_equipment/1076.png'),(21,11,'Canon EOS 6D + 24-105mm. F4','/upload/files/1452823027/File.png'),(22,11,'Canon EOS 7D(BODY)','/images_equipment/1078.png'),(23,14,'Car mount Tripod','/images_equipment/1099.png'),(24,29,'Condenser Microphone','/images_equipment/1127.png'),(25,12,'Converter Lens-30/37mm. *for Sony VIDEO Camera','/images_equipment/1080.png'),(26,16,'Dedo light 150 W + Light stand','/images_equipment/1108.png'),(27,32,'Director Viewfinder','/images_equipment/1142.png'),(28,21,'Dolly 75 *Use with Tripod spreader','/upload/files/1454922368/File.jpg'),(29,21,'Door away Dolly','/images_equipment/1134.png'),(30,18,'Flag Roadrags 18x24 inch',''),(31,32,'Gasoline Generator 220 V','/images_equipment/1141.png'),(32,29,'Headphone ','/images_equipment/1126.png'),(33,18,'High Temperature Metal Black Flag 24x36 inch','/images_equipment/1118.png'),(34,21,'Hot button for tracks 4PSC/SET','/images_equipment/1135.png'),(35,29,'I Track Dock','/upload/files/1452825213/File.jpg'),(36,14,'Jib Crane 14 feet','/images_equipment/1103.png'),(37,14,'Jib Crane 9 feet','/images_equipment/1104.png'),(38,16,'KINO FLO GRAFFERKIT','/images_equipment/1111.png'),(39,16,'KINO FLO Interview KIT','/images_equipment/1112.png'),(40,16,'LED LIGHT + Light Stand','/images_equipment/1107.png'),(41,12,'Lens 14-45 mm. F/3.5-F5.6 ASPH for Panasonic Video Camera','/images_equipment/1088.png'),(42,12,'Lens 35 mm.F1.4 DG HSM','/upload/files/1452823350/File.jpg'),(43,12,'Lens 45-200 mm. F/4.0-F5.6 for Panasonic Video Camera','/images_equipment/1087.png'),(44,12,'Lens Adapter EOS-Micro4/3 W/IRIS for Panasonic Video Camera','/images_equipment/1093.png'),(45,12,'Lens Canon EF 24-105mm. F4','/images_equipment/1084.png'),(46,12,'Lens Canon EF 24-70mm. F2.8L IS2USM','/images_equipment/1083.png'),(47,12,'Lens Canon EF 50mm. F1.8','/images_equipment/1086.png'),(48,12,'Lens Canon EF 70-200mm. F2.8L IS2USM','/images_equipment/1082.png'),(49,12,'Lens EF 100 mm. F2.8L Macro IS USM','/images_equipment/1092.png'),(50,12,'Lens EF 50 mm. F1.4 USM','/images_equipment/1090.png'),(51,12,'Lens EF 85 mm. F1.2LII USM','/images_equipment/1091.png'),(52,12,'Lens TOKINA 11-16mm. F2.8IF For Canon Mount','/images_equipment/1085.png'),(53,29,'Long-gun Microphone','/upload/files/1454551038/File.jpg'),(54,18,'Meat Axe Black Flag 24x48 inch','/images_equipment/1117.png'),(55,17,'Nikon FM 10 Film Camera ','/upload/files/1453078539/File.JPG'),(56,28,'Panasonic AG-AF102EN Camcorder','/images_equipment/1096.png'),(57,32,'Pen Tablet','/upload/files/1452825823/File.jpg'),(58,31,'Recorder Sony GV-HD700E','/images_equipment/1140.png'),(59,14,'Sachtler FSB 4 TRIPOD','/images_equipment/0.png'),(60,32,'Screen Motor','/upload/files/1452825895/File.jpg'),(61,29,'Shot-gun Microphone Set With Fabric BAG','/upload/files/1456890138/File.jpg'),(62,14,'Shoulder Brace','/images_equipment/1100.png'),(63,14,'Shoulder Stabilizer','/images_equipment/1098.png'),(64,18,'Silk for Flag 18x12','/upload/files/1452824066/File.jpg'),(65,18,'Silk for Flag 24x18','/upload/files/1452824098/File.jpg'),(66,18,'Silk for Flag 36x24','/upload/files/1452824191/File.jpg'),(67,31,'Sony HVR-DR60 Hard Disk Units','/images_equipment/1139.png'),(68,28,'Sony HVR-HD1000P Camcorder','/images_equipment/1095.png'),(69,31,'Sony HVR-MRC1K CF Memory Recording Unit','/images_equipment/1138.png'),(70,28,'Sony HVR-Z7P HDV Camcorder','/images_equipment/1094.png'),(71,28,'Sony PD-170P(3CCD Camera DVCAM) + Remote','/upload/files/1452824646/File.jpg'),(72,32,'Studio flash Set','/upload/files/1452847885/File.jpg'),(73,21,'Track Curve for 20 FT Diameter','/images_equipment/1137.png'),(74,21,'Track Straight 4 ft','/images_equipment/1136.png'),(75,14,'Tripod ','/images_equipment/1101.png'),(76,14,'Tripod with Spreader ','/images_equipment/1102.png'),(77,32,'Visualizer LUMENS PS','/upload/files/1452825868/File.jpg'),(78,29,'Wireless MIC for Studio','/upload/files/1452824967/File.jpg'),(79,29,'Wireless Microphone','/upload/files/1452825103/File.jpg'),(80,29,'XLR Microphone for Panasonic Video Camera','/images_equipment/1130.png'),(81,29,'ZOOM 4 Track Digital Recorder','/images_equipment/1124.png'),(82,29,'ZOOM 6 Track Digital Recorder','/images_equipment/1125.png');
/*!40000 ALTER TABLE `equipment_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipment_type`
--

DROP TABLE IF EXISTS `equipment_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipment_type` (
  `id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment_type`
--

LOCK TABLES `equipment_type` WRITE;
/*!40000 ALTER TABLE `equipment_type` DISABLE KEYS */;
INSERT INTO `equipment_type` VALUES (11,'DSLR'),(12,'Lens'),(13,'VIDEO CAM'),(14,'Tripod'),(15,'C-Stand'),(16,'Lighting'),(17,'Film camera'),(18,'Flag'),(19,'Audio'),(20,'Monitor'),(21,'Dolly'),(22,'Player'),(24,'Recorder Media'),(27,'Etc'),(28,'Video Camera'),(29,'Audio Devices'),(30,'Monitor'),(31,'Recorder Media for Video Camera'),(32,'Other Devices');
/*!40000 ALTER TABLE `equipment_type` ENABLE KEYS */;
UNLOCK TABLES;

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
INSERT INTO `menu` VALUES (1,'icon-screen-desktop','Borrow',NULL,NULL,-1,1,'SYSTEM','2016-03-20','2015-02-13'),(2,'icon-grid','Account',NULL,NULL,-1,2,'SYSTEM','2015-02-13','2015-02-13'),(3,'icon-settings','Master',NULL,NULL,-1,3,'SYSTEM','2015-02-13','2015-02-13'),(4,NULL,'Check Status','/index.php/Borrow','SearchUser|',1,1,'SYSTEM','2015-02-13','2015-02-13'),(5,NULL,'Borrow','/index.php/Borrow','SearchRole|',1,2,'SYSTEM','2015-02-13','2015-02-13'),(6,NULL,'Remain','/index.php/Remain','Specification|',1,3,'SYSTEM','2015-02-13','2015-02-13'),(7,NULL,'Subject','/index.php/Subject','TypeOfTest|',3,1,'SYSTEM','2015-02-13','2015-02-13'),(8,NULL,'Equipment','/index.php/Equipment','SearchTemplate|',3,2,'SYSTEM','2015-02-13','2015-02-13'),(9,NULL,'Room','/index.php/Room','SearchLibrary|',3,3,'SYSTEM','2015-02-13','2015-02-13'),(15,NULL,'User','/index.php/Users',NULL,2,1,'SYSTEM','2015-02-13','2015-02-13'),(16,NULL,'Role','/index.php/UsersRole',NULL,2,2,'SYSTEM','2015-02-13','2015-02-13'),(17,'icon-bar-chart','Report',NULL,NULL,-1,4,'SYSTEM','2015-02-13','2015-02-13'),(18,NULL,'รายงาน 1','/index.php/Report01','SearchRecord1|',17,1,'SYSTEM','2015-02-13','2015-02-13'),(19,NULL,'ชนิดปิดผนึก','/index.php/Report02',NULL,33,1,'SYSTEM','2015-02-13','2015-02-13'),(33,'fa fa-edit','วัตถุกัมมันตรังสี','#',NULL,1,4,'SYSTEM','2016-07-24','2016-07-24');
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
  `IS_ACTIVE` varchar(1) NOT NULL,
  `IS_REQUIRED_ACTION` varchar(1) DEFAULT NULL,
  `IS_CREATE` varchar(1) DEFAULT NULL,
  `IS_EDIT` varchar(1) DEFAULT NULL,
  `IS_DELETE` varchar(1) DEFAULT NULL,
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
INSERT INTO `menu_role` VALUES (1,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,2,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,3,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,6,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,7,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,8,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,9,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,15,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,16,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,17,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,18,'1','','1','1','1','1','2016-06-18','2016-06-18'),(1,19,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,2,'','','','','','1','2016-06-18','2016-06-18'),(2,3,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,6,'','','','','','1','2016-06-18','2016-06-18'),(2,7,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,8,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,9,'1','','1','1','1','1','2016-06-18','2016-06-18'),(2,15,'','','','','','1','2016-06-18','2016-06-18'),(2,16,'','','','','','1','2016-06-18','2016-06-18'),(2,17,'','','','','','1','2016-06-18','2016-06-18'),(2,18,'','','','','','1','2016-06-18','2016-06-18'),(2,19,'','','','','','1','2016-06-18','2016-06-18'),(3,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(3,2,'','','','','','1','2016-06-18','2016-06-18'),(3,3,'','','','','','1','2016-06-18','2016-06-18'),(3,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(3,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(3,6,'1','','1','1','1','1','2016-06-18','2016-06-18'),(3,7,'','','','','','1','2016-06-18','2016-06-18'),(3,8,'','','','','','1','2016-06-18','2016-06-18'),(3,9,'','','','','','1','2016-06-18','2016-06-18'),(3,15,'','','','','','1','2016-06-18','2016-06-18'),(3,16,'','','','','','1','2016-06-18','2016-06-18'),(3,17,'','','','','','1','2016-06-18','2016-06-18'),(3,18,'','','','','','1','2016-06-18','2016-06-18'),(3,19,'','','','','','1','2016-06-18','2016-06-18'),(4,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(4,2,'','','','','','1','2016-06-18','2016-06-18'),(4,3,'','','','','','1','2016-06-18','2016-06-18'),(4,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(4,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(4,6,'1','','1','1','1','1','2016-06-18','2016-06-18'),(4,7,'','','','','','1','2016-06-18','2016-06-18'),(4,8,'','','','','','1','2016-06-18','2016-06-18'),(4,9,'','','','','','1','2016-06-18','2016-06-18'),(4,15,'','','','','','1','2016-06-18','2016-06-18'),(4,16,'','','','','','1','2016-06-18','2016-06-18'),(4,17,'','','','','','1','2016-06-18','2016-06-18'),(4,18,'','','','','','1','2016-06-18','2016-06-18'),(4,19,'','','','','','1','2016-06-18','2016-06-18'),(5,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(5,2,'','','','','','1','2016-06-18','2016-06-18'),(5,3,'','','','','','1','2016-06-18','2016-06-18'),(5,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(5,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(5,6,'1','','1','1','1','1','2016-06-18','2016-06-18'),(5,7,'','','','','','1','2016-06-18','2016-06-18'),(5,8,'','','','','','1','2016-06-18','2016-06-18'),(5,9,'','','','','','1','2016-06-18','2016-06-18'),(5,15,'','','','','','1','2016-06-18','2016-06-18'),(5,16,'','','','','','1','2016-06-18','2016-06-18'),(5,17,'','','','','','1','2016-06-18','2016-06-18'),(5,18,'','','','','','1','2016-06-18','2016-06-18'),(5,19,'','','','','','1','2016-06-18','2016-06-18'),(6,1,'1','','1','1','1','1','2016-06-18','2016-06-18'),(6,2,'','','','','','1','2016-06-18','2016-06-18'),(6,3,'','','','','','1','2016-06-18','2016-06-18'),(6,4,'1','','1','1','1','1','2016-06-18','2016-06-18'),(6,5,'1','','1','1','1','1','2016-06-18','2016-06-18'),(6,6,'1','','1','1','1','1','2016-06-18','2016-06-18'),(6,7,'','','','','','1','2016-06-18','2016-06-18'),(6,8,'','','','','','1','2016-06-18','2016-06-18'),(6,9,'','','','','','1','2016-06-18','2016-06-18'),(6,15,'','','','','','1','2016-06-18','2016-06-18'),(6,16,'','','','','','1','2016-06-18','2016-06-18'),(6,17,'','','','','','1','2016-06-18','2016-06-18'),(6,18,'','','','','','1','2016-06-18','2016-06-18'),(6,19,'','','','','','1','2016-06-18','2016-06-18');
/*!40000 ALTER TABLE `menu_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_borrow`
--

DROP TABLE IF EXISTS `request_borrow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_borrow` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `DocumentNo` varchar(20) NOT NULL,
  `location` varchar(255) NOT NULL,
  `description` text,
  `from_date` date NOT NULL,
  `to_date` date NOT NULL,
  `status_code` varchar(255) NOT NULL,
  `otherDevice` varchar(255) DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `create_date` datetime NOT NULL,
  `create_by` int(11) NOT NULL,
  `approver_1` int(11) DEFAULT NULL,
  `approver_2` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk01_idx` (`create_by`),
  CONSTRAINT `fk01` FOREIGN KEY (`create_by`) REFERENCES `users_login` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=787 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_borrow`
--

LOCK TABLES `request_borrow` WRITE;
/*!40000 ALTER TABLE `request_borrow` DISABLE KEYS */;
INSERT INTO `request_borrow` VALUES (784,'1600000001','1',NULL,'2016-05-08','2016-05-12','READY','ถ่านตรากบสักก้อน','ยืมอุปกรณ์ไปถ่ายทำละคร','2016-05-05 08:30:50',1,NULL,NULL),(785,'1600000002','1',NULL,'2016-05-01','2016-05-11','PREPARE_EQUIPMENT','test2','test','2016-05-17 09:55:11',1,NULL,NULL),(786,'1600000003','1',NULL,'2016-06-02','2016-02-10','PREPARE_EQUIPMENT','dddd','ddsfsdfds','2016-05-30 15:47:58',6,3,4);
/*!40000 ALTER TABLE `request_borrow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_borrow_detail`
--

DROP TABLE IF EXISTS `request_borrow_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_borrow_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `request_borrow_id` int(11) NOT NULL,
  `request_borrow_quantity_id` int(11) NOT NULL,
  `equipment_id` int(11) NOT NULL,
  `return_date` varchar(255) DEFAULT NULL,
  `return_price` double DEFAULT NULL,
  `broken_price` double DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_rbd_2_e_idx` (`equipment_id`),
  CONSTRAINT `fk_rbd_2_e` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6359 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_borrow_detail`
--

LOCK TABLES `request_borrow_detail` WRITE;
/*!40000 ALTER TABLE `request_borrow_detail` DISABLE KEYS */;
INSERT INTO `request_borrow_detail` VALUES (6354,784,23,507,NULL,NULL,NULL,NULL),(6355,784,25,509,NULL,NULL,NULL,NULL),(6356,784,25,510,NULL,NULL,NULL,NULL),(6357,784,17,486,NULL,NULL,NULL,NULL),(6358,784,30,521,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `request_borrow_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_borrow_quantity`
--

DROP TABLE IF EXISTS `request_borrow_quantity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_borrow_quantity` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `seq` int(11) DEFAULT NULL,
  `request_borrow_id` int(11) NOT NULL,
  `equipment_group_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_rbq2_et_idx` (`equipment_group_id`),
  CONSTRAINT `fk_rbq2_et` FOREIGN KEY (`equipment_group_id`) REFERENCES `equipment_group` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_borrow_quantity`
--

LOCK TABLES `request_borrow_quantity` WRITE;
/*!40000 ALTER TABLE `request_borrow_quantity` DISABLE KEYS */;
INSERT INTO `request_borrow_quantity` VALUES (18,1,784,23,1),(19,1,784,25,1),(20,1,784,17,1),(21,2,784,23,1),(22,2,784,25,2),(23,2,784,17,1),(24,2,784,30,1),(25,1,785,6,1),(26,1,786,17,1);
/*!40000 ALTER TABLE `request_borrow_quantity` ENABLE KEYS */;
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
  `building_id` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_department`
--

LOCK TABLES `tb_m_department` WRITE;
/*!40000 ALTER TABLE `tb_m_department` DISABLE KEYS */;
INSERT INTO `tb_m_department` VALUES (1,'FAA','1','1');
/*!40000 ALTER TABLE `tb_m_department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_m_title`
--

DROP TABLE IF EXISTS `tb_m_title`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_m_title` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_m_title`
--

LOCK TABLES `tb_m_title` WRITE;
/*!40000 ALTER TABLE `tb_m_title` DISABLE KEYS */;
INSERT INTO `tb_m_title` VALUES (1,'Mr.','A'),(2,'Mrs.','A'),(3,'Miss.','A'),(4,'Ms.','A');
/*!40000 ALTER TABLE `tb_m_title` ENABLE KEYS */;
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
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL DEFAULT '1234',
  `latest_login` datetime DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `create_by` int(11) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `status` varchar(1) NOT NULL DEFAULT 'A',
  `is_force_change_password` bit(1) DEFAULT b'0',
  `first_name` varchar(45) DEFAULT NULL,
  `last_name` varchar(45) DEFAULT NULL,
  `mobile_phone` varchar(45) DEFAULT NULL,
  `department_id` int(11) DEFAULT NULL,
  `title_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_role_id_idx` (`role_id`),
  KEY `fk_title_id_2_idx` (`title_id`),
  KEY `fk_department_id_idx` (`department_id`),
  CONSTRAINT `fk_department_id` FOREIGN KEY (`department_id`) REFERENCES `tb_m_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_role_id` FOREIGN KEY (`role_id`) REFERENCES `users_role` (`ROLE_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_id` FOREIGN KEY (`title_id`) REFERENCES `tb_m_title` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_login`
--

LOCK TABLES `users_login` WRITE;
/*!40000 ALTER TABLE `users_login` DISABLE KEYS */;
INSERT INTO `users_login` VALUES (1,1,'admin','81DC9BDB52D04DC20036DBD8313ED055','2015-03-05 17:10:31','pawit1357@hotmail.com',1,'2015-02-13 15:43:58','A','\0','Pawit','Sae-eaung','1',NULL,NULL),(2,2,'staff','81DC9BDB52D04DC20036DBD8313ED055','2016-03-21 21:25:13','pawit1357@hotmail.com',1,'2016-03-21 21:25:13','A','\0','staff','-','2',NULL,NULL),(3,3,'approver1','81DC9BDB52D04DC20036DBD8313ED055','2016-03-21 21:25:13','pawit1357@hotmail.com',1,'2016-03-21 21:25:13','A','\0','approver 1','-','3',NULL,NULL),(4,4,'approver2','81DC9BDB52D04DC20036DBD8313ED055','2016-03-21 21:25:13','pawit1357@hotmail.com',1,'2016-03-21 21:25:13','A','\0','approver 2','-','4',NULL,NULL),(5,5,'lecturer','81DC9BDB52D04DC20036DBD8313ED055','2016-03-21 21:25:13','pawit1357@hotmail.com',1,'2016-03-21 21:25:13','A','\0','lecturer','-','5',NULL,NULL),(6,6,'student','81DC9BDB52D04DC20036DBD8313ED055','2016-03-21 21:25:13','pawit1357@hotmail.com',1,'2016-03-21 21:25:13','A','\0','student','-','6',NULL,NULL);
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
  `UPDATE_BY` int(11) NOT NULL,
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
INSERT INTO `users_role` VALUES (1,'ADMIN','Admin',1,'2016-06-18','2016-06-18'),(2,'STAFF','Staff',1,'2016-06-18','2016-06-18'),(3,'LECTURER_APPROVER_1','Lecturer (Approver 1)',1,'2016-06-18','2016-06-18'),(4,'LECTURER_APPROVER_2','Lecturer (Approver 2)',1,'2016-06-18','2016-06-18'),(5,'LECTURER','Lecturer',1,'2016-06-18','2016-06-18'),(6,'STUDENT','Student',1,'2016-06-18','2016-06-18');
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

-- Dump completed on 2016-08-14 15:21:59
