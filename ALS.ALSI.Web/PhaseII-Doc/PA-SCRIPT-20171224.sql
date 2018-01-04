-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('621', '27', 'PA-REPORT01', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('622', '28', 'PA-REPORT01', '~/view/template/PA02.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');

-- INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');

-- CREATE TABLE `m_evaluation_of_particles` (
--  `id` int(11) NOT NULL AUTO_INCREMENT,
--  `template_id` int(11) DEFAULT NULL,
--  `seq` int(11) DEFAULT NULL,
--  `A` varchar(255) DEFAULT NULL,
--  `B` varchar(255) DEFAULT NULL,
--  `C` varchar(255) DEFAULT NULL,
--  `status` varchar(1) DEFAULT NULL,
--  PRIMARY KEY (`id`)
-- ) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- CREATE TABLE `m_microscopic_analysis` (
--  `id` int(11) NOT NULL AUTO_INCREMENT,
--  `template_id` int(11) DEFAULT NULL,
--  `seq` int(11) DEFAULT NULL,
--  `A` varchar(45) DEFAULT NULL,
--  `B` varchar(45) DEFAULT NULL,
--  `status` varchar(45) DEFAULT NULL,
--  PRIMARY KEY (`id`)
-- ) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`, `A`, `B`, `C`, `status`) VALUES ('1', '1',1, '', 'Max. permissible contaminant mass:(mg/1000cm2)', '10', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`, `A`, `B`, `C`, `status`) VALUES ('2', '1',2, '', 'Permissible particle:', 'All particles ≤ 2000', 'A');
-- 
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('3', '2',1, 'Particle', '25 - 50', 'Not to evaluate', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('4', '2',2, 'Particle', '50 - 100', '1,000', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('5', '2',3, 'Particle', '100 - 150', '140', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('6', '2',4, 'Particle', '150 - 200', '30', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('7', '2',5, 'Particle', '200 - 300', '7', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('8', '2',6, 'Particle', '300 - 400', '1', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('9', '2',7, 'Particle', '400 - 600', '0.1', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('10', '2',8, 'Particle', '600 - 800', '0.03', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('11', '2',9, 'Particle', '800 - 1000', '0.0', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('12', '2',10, 'Particle', 'X > 1000', '0.0', 'A');
-- INSERT INTO `alsi`.`m_evaluation_of_particles` (`id`, `template_id`,`seq`, `A`, `B`, `C`, `status`) VALUES ('13', '2',11, 'Fiber', 'X > 600', 'Not to evaluate', 'A');

-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('1', '1', '1', 'B', '5 ≤ X < 15', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('2', '1', '2', 'C', '15 ≤ X < 25', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('3', '1', '3', 'D', '25 ≤ X < 50', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('4', '1', '4', 'E', '50 ≤ X < 100', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('5', '1', '5', 'F', '100 ≤ X < 150', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('6', '1', '6', 'G', '150 ≤ X < 200', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('7', '1', '7', 'H', '200 ≤ X < 400', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('8', '1', '8', 'I', '400 ≤ X < 600', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('9', '1', '9', 'J', '600 ≤ X < 1000', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `A`, `B`, `status`) VALUES ('10', '1', '10', 'K', '1000 ≤ X', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('11', '2', '1', '25 ≤ X < 50', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('12', '2', '2', '50 ≤ X < 100', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('13', '2', '3', '100 ≤ X < 150', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('14', '2', '4', '150 ≤ X < 200', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('15', '2', '5', '200 ≤ X < 300', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('16', '2', '6', '300 ≤ X < 400', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('17', '2', '7', '400 ≤ X < 600', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('18', '2', '8', '600 ≤ X < 800', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('19', '2', '9', '800 ≤ X < 1000', 'A');
-- INSERT INTO `alsi`.`m_microscopic_analysis` (`id`, `template_id`, `seq`, `B`, `status`) VALUES ('20', '2', '10', '1000 ≤ X', 'A');



--ELN-0661-PAB
--ELN-0662-PAB