-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('621', '27', 'PA-REPORT01', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('622', '28', 'PA-REPORT01', '~/view/template/PA02.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');

-- INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('221', '27', 'PAB', 'PA_REPORT3', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('224', '27', 'PAB', 'PA_REPORT4', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('223', '27', 'PAB', 'PA_REPORT5', '1', 'A', 'PA');

-- ELN-0664-PAB
-- ELP-2475-HB 
-- WD_MESA_IDM.ascx/ELP-2292-MB (rattana)
-- WD_MESA_IDM.ascx/ELP-2072-MB




/*
ELP-2478-MB

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `singapore_ref_no` VARCHAR(200) NULL AFTER `other_ref_no`;

------
CREATE TABLE `template_seagate_mesa_coverpage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `sample_id` int(11) DEFAULT NULL,
  `component_id` int(11) DEFAULT NULL,
  `location_of_parts` varchar(255) DEFAULT NULL,
  `specification` varchar(255) DEFAULT NULL,
  `result` varchar(45) DEFAULT NULL,
  `pass_fail` varchar(45) DEFAULT NULL,
  `ProcedureNo_Extraction` varchar(45) DEFAULT NULL,
  `ExtractionMedium_Extraction` varchar(45) DEFAULT NULL,
  `SampleSize_Extraction` varchar(45) DEFAULT NULL,
  `OvenCondition_Extraction` varchar(200) DEFAULT NULL,
  `ProcedureNo_IndirectMaterials` varchar(200) DEFAULT NULL,
  `SampleSize_IndirectMaterials` varchar(45) DEFAULT NULL,
  `OvenCondition_IndirectMaterials` varchar(45) DEFAULT NULL,
  `row_type` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_sample_id_idx_5_idx` (`sample_id`),
  KEY `fk_component_idx_5_idx` (`component_id`)
) ENGINE=InnoDB AUTO_INCREMENT=700 DEFAULT CHARSET=utf8;

-------
CREATE TABLE `template_seagate_mesa_img` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sample_id` int(11) DEFAULT NULL,
  `seq` int(11) DEFAULT '0',
  `area` int(11) DEFAULT NULL,
  `descripton` varchar(255) DEFAULT NULL,
  `path_sem_image_at_250x` varchar(255) DEFAULT NULL,
  `path_sem_image_at_500x` varchar(255) DEFAULT NULL,
  `path_sem_image_at_2000x` varchar(255) DEFAULT NULL,
  `path_edx_spectrum` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=322 DEFAULT CHARSET=utf8;

*/