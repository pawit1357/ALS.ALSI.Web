/*

--ALTER TABLE `alsi`.`job_sample` 
--ADD COLUMN `update_date` DATE NULL AFTER `is_no_spec`;],
--ADD COLUMN `is_hold` VARCHAR(1) NULL DEFAULT 0 AFTER `update_date`;
--ADD COLUMN `due_date_customer` DATE NULL AFTER `due_date`,
--ADD COLUMN `due_date_lab` DATE NULL AFTER `due_date_customer`,
--ADD COLUMN `amend_count` INT NULL DEFAULT 0 AFTER `is_hold`,
--ADD COLUMN `retest_count` INT NULL DEFAULT 0 AFTER `amend_count`;
--ADD COLUMN `update_by` INT NULL DEFAULT 1 AFTER `retest_count`;

--CHANGE COLUMN `update_date` `update_date` DATETIME NULL DEFAULT NULL ;

--update job_sample set is_hold=1 where job_status=2;
--update job_sample set is_hold=0 where job_status <> 2;

--update job_sample  set
--job_sample.due_date_customer = DATE_ADD((select job_info.date_of_receive from job_info where id=job_sample.job_id), INTERVAL (select customer_due_date from m_completion_scheduled where m_completion_scheduled.ID=job_sample.status_completion_scheduled) DAY),
--job_sample.due_date_lab = DATE_ADD((select job_info.date_of_receive from job_info where id=job_sample.job_id), INTERVAL (select lab_due_date from m_completion_scheduled where m_completion_scheduled.ID=job_sample.status_completion_scheduled) DAY)


--ALTER TABLE `alsi`.`m_completion_scheduled` 
--ADD COLUMN `value2` INT NULL AFTER `value`;

------------------------------------------------------------------------------------------------------
--INSERT INTO `alsi`.`m_status` (`ID`, `status_group_id`, `status_for_role`, `name`, `status`) VALUES ('15', '1', '2', 'JOB_UNHOLD', 'A');
--INSERT INTO `alsi`.`m_status` (`ID`, `status_group_id`, `status_for_role`, `name`, `status`) VALUES ('16', '1', '2', 'JOB RETEST', 'A');
--INSERT INTO `alsi`.`m_status` (`ID`, `status_group_id`, `status_for_role`, `name`, `status`) VALUES ('17', '1', '2', 'JOB AMEND', 'A');

--ALTER TABLE `alsi`.`m_status` 
--ADD COLUMN `status` VARCHAR(1) NULL DEFAULT 'A' AFTER `name`;
--UPDATE `alsi`.`m_status` SET `status`='I' WHERE `ID`='2';
--UPDATE `alsi`.`m_status` SET `status`='I' WHERE `ID`='15';


------------------------------------------------------------------------------------------------------
--ALTER TABLE `alsi`.`m_completion_scheduled` 
--CHANGE COLUMN `value2` `lab_due_date` INT(11) NULL DEFAULT NULL ,
--ADD COLUMN `customer_due_date` INT NULL AFTER `lab_due_date`;
--UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='8', `customer_due_date`='8' WHERE `ID`='1';
--UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='4', `customer_due_date`='4' WHERE `ID`='2';
--UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='3', `customer_due_date`='3' WHERE `ID`='3';
------------------------------------------------------------------------------------------------------


*/

--select job_sample.job_number,job_info.date_of_receive,
--job_sample.due_date,
--job_sample.due_date_customer,
--job_sample.due_date_lab
---- DATE_ADD(job_info.date_of_receive, INTERVAL m_completion_scheduled.customer_due_date DAY) as due_date_customer,
---- DATE_ADD(job_info.date_of_receive, INTERVAL m_completion_scheduled.lab_due_date DAY) as due_date_lab
--from job_sample 
--left join job_info on job_info.id = job_sample.job_id
--left join m_completion_scheduled on m_completion_scheduled.id = job_sample.status_completion_scheduled


