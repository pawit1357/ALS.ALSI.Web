/*

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `update_date` DATE NULL AFTER `is_no_spec`;],
ADD COLUMN `is_hold` VARCHAR(1) NULL DEFAULT 0 AFTER `update_date`;

update job_sample set is_hold=1 where job_status=2;
update job_sample set is_hold=0 where job_status <> 2;

ALTER TABLE `alsi`.`m_completion_scheduled` 
ADD COLUMN `value2` INT NULL AFTER `value`;

------------------------------------------------------------------------------------------------------
INSERT INTO `alsi`.`m_status` (`ID`, `status_group_id`, `status_for_role`, `name`, `status`) VALUES ('15', '1', '2', 'JOB_UNHOLD', 'A');

ALTER TABLE `alsi`.`m_status` 
ADD COLUMN `status` VARCHAR(1) NULL DEFAULT 'A' AFTER `name`;
UPDATE `alsi`.`m_status` SET `status`='I' WHERE `ID`='2';
UPDATE `alsi`.`m_status` SET `status`='I' WHERE `ID`='15';


------------------------------------------------------------------------------------------------------
ALTER TABLE `alsi`.`m_completion_scheduled` 
CHANGE COLUMN `value2` `lab_due_date` INT(11) NULL DEFAULT NULL ,
ADD COLUMN `customer_due_date` INT NULL AFTER `lab_due_date`;

UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='8', `customer_due_date`='8' WHERE `ID`='1';
UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='4', `customer_due_date`='4' WHERE `ID`='2';
UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='3', `customer_due_date`='3' WHERE `ID`='3';
------------------------------------------------------------------------------------------------------


*/





