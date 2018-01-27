-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('621', '27', 'PA-REPORT01', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
-- INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('622', '28', 'PA-REPORT01', '~/view/template/PA02.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');

-- INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `part_no` VARCHAR(200) NULL AFTER `update_by`,
ADD COLUMN `part_name` VARCHAR(200) NULL AFTER `part_no`,
ADD COLUMN `lot_no` VARCHAR(200) NULL AFTER `part_name`;




update job_sample set job_status=11 where job_number='ELN-0664-PAB';


-- ELN-0664-PAB