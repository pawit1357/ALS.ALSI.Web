-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('621', '27', 'PA-REPORT01', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('622', '28', 'PA-REPORT01', '~/view/template/PA02.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');

-- INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');


-- ELN-0664-PAB
-- ELP-2475-HB 
-- WD_MESA_IDM.ascx/ELP-2292-MB (rattana)
-- WD_MESA_IDM.ascx/ELP-2072-MB


ALTER TABLE `alsi`.`template_wd_mesa_img` 
ADD COLUMN `seq` INT NULL DEFAULT 0 AFTER `sample_id`;



