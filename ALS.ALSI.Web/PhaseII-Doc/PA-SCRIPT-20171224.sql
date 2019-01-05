-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
--update job_sample set sample_prefix=SUBvarchar(45)_INDEX(job_number,'-',1);

-- ELN-0664-PAB
-- ELP-2475-HB 
-- WD_MESA_IDM.ascx/ELP-2292-MB (rattana)
-- WD_MESA_IDM.ascx/ELP-2072-MB



/*

ELP-2480-HB ***** seagate hpa swap
ELP-2824-LB ** wd lpc
ELP-3028B-HB
3128-DB
ELP-3276-DB
ELP-1942-DB

PA01 > ELN-0839-PAB
PA02 > ELN-0841-PAB
PA03 > ELN-0842-PAB,ELN-0843-PAB (result กับ max value of EOV ผิด)


fix
3151-HB

ELP-2544-DB มากกว่า 30 
- ELP-3218-DB
- ELP-3183-DB
- ELP-2545-DB

ELP-1155-DB == 9

----------
complete 
-- CLEAR - lpc seagate fix analysis แสดงชื่อซ้ำ เช่น LPC (132 KHz) แสดงเป็น LPC (LPC 132 KHz)
-- CLEAR - corrosion tank
-- CLEAR - dhs seagate (v2) เกินหน้ากระดาษ
-- CLEAR - 3063 (hpa swap ) error ELP-3063-HB
-- CLEAR - 3443-cvr CVR WD error Extraction Medium (ข้อความมีความยาวเกินไป)
-- CLEAR hpa อัพเดท script u
-- CLEAR hpa fill กระดาษแสดงไม่พอดี
-- CLEAR search SEM/EDX แล้วดึงข้อมูลออกมา มันออกมาเฉพาะ type of test ที่เป็น SEM/EDX ที่เป็น FA กับ ELN แต่พวกที่เป็น ELP   HPA & MESA Ghost พวกนี้ ไม่ออกมา
-- CLEAR เพราะเหมือนเขาว่า  เขา sort วันที่แล้วมันไม่เรียง เพราะ 1-9มันไม่มีเลข0นำหน้า มันเลยทำให้มันเรียงกันแบบไม่ถูกเช่น งานที่เข้าวันที่ 2มันจะเรียงอยู่หลังงานที่ เข้า 19
-- CLEAR- FTIR WD  IDM  ตัวที่บอกว่า พออัพแล้วมันติด น่าจะมาจากการระบุให้ดึงข้อมมูลผิดที่
-- CLEAR- ELP-3218-DB  ---WD ok (32) พอดี 2 หน้า 
-- CLEAR- ELP-3183-DB ---WD ok เกินหน้า 3 (39)
-- CLEAR- ELP-2544-DB ---Seagate
-- CLEAR- ELP-2545-DB ---Seagate
-- CLEAR- ELP-3179-DB ---WD
-- CLEAR- ELP-3158-DB ---WD
-- CLEAR- ELP-2717-DB ---WD
-- CLEAR- ELP-2614-DB ---WD

-----------------------------------
Ftir , dhs ,gcms , hpa, ic ทั้ง wd seagate ค่ะ แต่ตัว hpa ตำแหน่งที่รูปจะอยู่มันจะไม่เหมือนเพื่อน
-----------------------------------
Seagate_FTIR.ascx(546) > ELP-0812-FB
Seagate_FTIR_Adhesive.ascx(545) > ELP-0662-FB
Seagate_FTIR_Packing.ascx(638) > ELP-2947-FB,3331-FB
Seagate_FTIR_Damper.ascx(640) > ELP-2987-FB

ELP-3236-FB (test "not detect" and result has value >> must show "fail")
ELP-1248-FB (test spec is "NA" result must "NA")


-- CLEAR update template_seagate_hpa_coverpage set A = replace(A,'μ','u')
-- CLEAR update template_seagate_lpc_coverpage set LiquidParticleCount = replace(LiquidParticleCount,'μ','u');



#######-----
ALTER TABLE `alsi`.`tb_m_specification` 
ADD COLUMN `status` VARCHAR(1) NULL DEFAULT 'A' AFTER `BZ`;


CREATE TABLE `template_img` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sample_id` int(11) DEFAULT NULL,
  `seq` int(11) DEFAULT '0',
  `img_path` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=402 DEFAULT CHARSET=utf8;



ALTER TABLE `alsi`.`m_type_of_test` 
ADD COLUMN `ref_template_id` INT NULL AFTER `data_group`;


INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('927', '27', 'PA-REPORT01', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('928', '27', 'PA-REPORT02', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('929', '27', 'PA-REPORT03', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('930', '27', 'PA-REPORT04', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');
INSERT INTO `alsi`.`m_template` (`ID`, `specification_id`, `name`, `path_url`, `requestor`, `modified_by`, `verified_by`, `validated_by`, `modified_date`, `create_date`, `status`) VALUES ('931', '27', 'PA-REPORT05', '~/view/template/PA01.ascx', '2', '2', '2', '2', '2017-12-24', '2017-12-24', 'A');

UPDATE `alsi`.`m_type_of_test` SET `ref_template_id`='903' WHERE `ID`='237';
UPDATE `alsi`.`m_type_of_test` SET `ref_template_id`='904' WHERE `ID`='238';
UPDATE `alsi`.`m_type_of_test` SET `ref_template_id`='905' WHERE `ID`='234';
UPDATE `alsi`.`m_type_of_test` SET `ref_template_id`='906' WHERE `ID`='235';
UPDATE `alsi`.`m_type_of_test` SET `ref_template_id`='907' WHERE `ID`='236';

 INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
 INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
 INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');

----------------------- 2018-3-13 ---------------------------

ALTER TABLE `alsi`.`template_wd_hpa_for1_coverpage` 
CHANGE COLUMN `ParticleAnalysisBySEMEDX` `ParticleAnalysisBySEMEDX` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `TapedAreaForDriveParts` `TapedAreaForDriveParts` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `NoofTimesTaped` `NoofTimesTaped` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `SurfaceAreaAnalysed` `SurfaceAreaAnalysed` VARCHAR(150) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleRanges` `ParticleRanges` VARCHAR(150) NULL DEFAULT NULL ;

--
ALTER TABLE `alsi`.`template_wd_hpa_for3_coverpage` 
CHANGE COLUMN `TapedAreaForDriveParts` `TapedAreaForDriveParts` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `NoofTimesTaped` `NoofTimesTaped` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `SurfaceAreaAnalysed` `SurfaceAreaAnalysed` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleRanges` `ParticleRanges` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleAnalysisBySEMEDX_1` `ParticleAnalysisBySEMEDX_1` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `TapedAreaForDriveParts_1` `TapedAreaForDriveParts_1` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `NoofTimesTaped_1` `NoofTimesTaped_1` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `SurfaceAreaAnalysed_1` `SurfaceAreaAnalysed_1` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleRanges_1` `ParticleRanges_1` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleAnalysisBySEMEDX_2` `ParticleAnalysisBySEMEDX_2` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `TapedAreaForDriveParts_2` `TapedAreaForDriveParts_2` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `NoofTimesTaped_2` `NoofTimesTaped_2` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `SurfaceAreaAnalysed_2` `SurfaceAreaAnalysed_2` VARCHAR(255) NULL DEFAULT NULL ,
CHANGE COLUMN `ParticleRanges_2` `ParticleRanges_2` VARCHAR(255) NULL DEFAULT NULL ;


--------------------- 2018-03-17 ---------------------------------------------
ALTER TABLE `alsi`.`template_pa` 
ADD COLUMN `attachment_ii_01` VARCHAR(255) NULL AFTER `img05`,
ADD COLUMN `attachment_ii_02` VARCHAR(255) NULL AFTER `attachment_ii_01`,
ADD COLUMN `attachment_ii_03` VARCHAR(255) NULL AFTER `attachment_ii_02`,
ADD COLUMN `attachment_ii_04` VARCHAR(255) NULL AFTER `attachment_ii_03`,
ADD COLUMN `param_magnification_01` VARCHAR(255) NULL AFTER `attachment_ii_04`,
ADD COLUMN `param_magnification_02` VARCHAR(255) NULL AFTER `param_magnification_01`,
ADD COLUMN `param_wd_01` VARCHAR(255) NULL AFTER `param_magnification_02`,
ADD COLUMN `param_wd_02` VARCHAR(255) NULL AFTER `param_wd_01`,
ADD COLUMN `param_eht_01` VARCHAR(255) NULL AFTER `param_wd_02`,
ADD COLUMN `param_eht_02` VARCHAR(255) NULL AFTER `param_eht_01`,
ADD COLUMN `param_detector_01` VARCHAR(255) NULL AFTER `param_eht_02`,
ADD COLUMN `param_detector_02` VARCHAR(255) NULL AFTER `param_detector_01`;

-------------------- 2018-03-20 ----------------------------------------
ALTER TABLE `alsi`.`template_seagate_copperwire_coverpage` 
ADD COLUMN `seq` INT NULL DEFAULT 0 AFTER `ID`;

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `amend_or_retest` VARCHAR(1) NULL COMMENT 'A= Amend,R= Retest' AFTER `sample_prefix`,
ADD COLUMN `last_status` INT NULL AFTER `amend_or_retest`;



--------------------- 2018-03-21 ----------------------------------
select * from tb_unit where unit_group='LPC';


INSERT INTO `alsi`.`tb_unit` (`id`, `unit_group`, `name`) VALUES ('56', 'LPC', 'Counts/mL');
UPDATE `alsi`.`tb_unit` SET `value`='1' WHERE `id`='56';

ALTER TABLE `alsi`.`template_wd_lpc_coverpage` 
ADD COLUMN `unit2` INT NULL AFTER `unit`,
ADD COLUMN `unit3` INT NULL AFTER `unit2`,
ADD COLUMN `unit4` INT NULL AFTER `unit3`;

ALTER TABLE `alsi`.`template_wd_lpc_coverpage` 
CHANGE COLUMN `unit` `unit` INT(11) NULL DEFAULT 46 ,
CHANGE COLUMN `unit2` `unit2` INT(11) NULL DEFAULT 56 ,
CHANGE COLUMN `unit3` `unit3` INT(11) NULL DEFAULT 49 ,
CHANGE COLUMN `unit4` `unit4` INT(11) NULL DEFAULT 44 ;

update template_wd_lpc_coverpage set unit=46,unit2=56,unit3=49,unit4=44;

---------------------------------------------------------
ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `amend_or_retest` `amend_or_retest` VARCHAR(2) NULL DEFAULT NULL COMMENT 'AM= Amend,R= Retest' ,

ขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขขข
ELN-0854-PAB
ELN-0855-PAB > PA5x_BOSCH0442S00155 > (D:\ALS\PA_RawData_02\Template_PAB_01)
ELN-0856-PAB test 3 case.
------------------------------------- 2018-03-25 ------------------------
ALTER TABLE `alsi`.`template_pa` 
ADD COLUMN `iswashUltrasonic` VARCHAR(45) NULL AFTER `param_detector_02`,
ADD COLUMN `isUltrasonic` VARCHAR(45) NULL AFTER `iswashUltrasonic`,
ADD COLUMN `rinsing_id` INT NULL AFTER `isUltrasonic`,
ADD COLUMN `washpressurerinsing_id` INT NULL AFTER `rinsing_id`,
ADD COLUMN `per_component_total` VARCHAR(45) NULL AFTER `washpressurerinsing_id`,
ADD COLUMN `per_component_metallicshine` VARCHAR(45) NULL AFTER `per_component_total`,
ADD COLUMN `per_membrane_metallicshine` VARCHAR(45) NULL AFTER `per_component_metallicshine`;

ALTER TABLE `alsi`.`template_pa` 
CHANGE COLUMN `iswashUltrasonic` `iswashUltrasonic` TINYINT(4) NULL DEFAULT NULL ,
CHANGE COLUMN `isUltrasonic` `isUltrasonic` TINYINT(4) NULL DEFAULT NULL ;

ALTER TABLE `alsi`.`template_pa` 
ADD COLUMN `specification_no` INT NULL AFTER `per_membrane_metallicshine`,
ADD COLUMN `operater_name` INT NULL AFTER `specification_no`;

ALTER TABLE `alsi`.`template_pa` 
ADD COLUMN `per_text` VARCHAR(45) NULL AFTER `operater_name`;

---------------------------------------------------
PA5x_BOSCH0442S00155 > D:\ALS\PA_RawData_02\Template_PAB_01 > ELN-0856-PAB 
PA5x_BOSCHF00VP19194 > D:\ALS\PA_RawData_02\Template_PAB_02 >
PA5x_BOSCH0442S00155PRV  > D:\ALS\PA_RawData_02\Template_PAB_02 >



----------------
ELN-PA-TEST01 (ใช้ทดสอบที่ ALS)



---------------------------------------- 2018-4-18 ----------------------------------------
ALTER TABLE `alsi`.`template_wd_hpa_for1_coverpage` 
ADD COLUMN `note` VARCHAR(255) NULL AFTER `data_group`;

ALTER TABLE `alsi`.`template_seagate_hpa_coverpage` 
ADD COLUMN `D` VARCHAR(45) NULL AFTER `C`;


---------------------------------- 2018-4-29 ----------------------------------------------
ALTER TABLE `alsi2`.`users_login` 
DROP FOREIGN KEY `fk_role_id`;
ALTER TABLE `alsi2`.`users_login` 
ADD INDEX `fk_role_id_idx` (`role_id` ASC),
DROP INDEX `fk_role_id_idx` ;
ALTER TABLE `alsi2`.`users_login` 
ADD CONSTRAINT `fk_role_id`
  FOREIGN KEY (`role_id`)
  REFERENCES `alsi2`.`m_role` (`ID`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


  ---------------------------------- 2018-5-1 ----------------------------------------------


  INSERT INTO `alsi`.`role` (`ROLE_ID`, `ROLE_NAME`, `ROLE_DESC`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('8', 'Marketing', 'Marketing', 'SYSTEM', '2015-02-13', '2015-02-13');



  -------------------------------- 2018-05-09 -----------------------------
  ALTER TABLE `alsi`.`template_pa` 
ADD COLUMN `img06` VARCHAR(255) NULL AFTER `per_text`,
ADD COLUMN `img07` VARCHAR(255) NULL AFTER `img06`,
ADD COLUMN `img08` VARCHAR(255) NULL AFTER `img07`,
ADD COLUMN `img09` VARCHAR(255) NULL AFTER `img08`,
ADD COLUMN `img10` VARCHAR(255) NULL AFTER `img09`,
ADD COLUMN `img11` VARCHAR(255) NULL AFTER `img10`,
ADD COLUMN `attachment_ii_05` VARCHAR(255) NULL AFTER `img11`,
ADD COLUMN `lms_x_r2` VARCHAR(45) NULL AFTER `attachment_ii_05`,
ADD COLUMN `lms_y_r2` VARCHAR(45) NULL AFTER `lms_x_r2`,
ADD COLUMN `lnms_x_r2` VARCHAR(45) NULL AFTER `lms_y_r2`,
ADD COLUMN `lnms_y_r2` VARCHAR(45) NULL AFTER `lnms_x_r2`,
ADD COLUMN `lf_x_r2` VARCHAR(45) NULL AFTER `lnms_y_r2`,
ADD COLUMN `lf_y_r2` VARCHAR(45) NULL AFTER `lf_x_r2`,
ADD COLUMN `lms_x_r3` VARCHAR(45) NULL AFTER `lf_y_r2`,
ADD COLUMN `lms_y_r3` VARCHAR(45) NULL AFTER `lms_x_r3`,
ADD COLUMN `lnms_x_r3` VARCHAR(45) NULL AFTER `lms_y_r3`,
ADD COLUMN `lnms_y_r3` VARCHAR(45) NULL AFTER `lnms_x_r3`,
ADD COLUMN `lf_x_r3` VARCHAR(45) NULL AFTER `lnms_y_r3`,
ADD COLUMN `lf_y_r3` VARCHAR(45) NULL AFTER `lf_x_r3`;


--- 2018-05-14 ---

-- check num of uncomplete last physical year (2017)
select count(job_sample.job_number) -- ,job_info.date_of_receive,job_sample.job_status 
from job_sample where job_id in (select id from job_info
where job_info.date_of_receive <= '2018-03-31') and job_sample.job_status <> 3;

-- update all to job_complete
update  job_sample set job_sample.job_status=3  where job_id in (select id from job_info
where job_info.date_of_receive <= '2018-03-31') and job_sample.job_status <> 3

-- update job to "job_delete"
x INSERT INTO `alsi`.`m_status` (`ID`, `status_group_id`, `status_for_role`, `name`, `status`) VALUES ('0', '1', '2', 'JOB DELETE', 'A');
---
x select * from job_sample where job_number in ('ELN-1020-PAB','ELN-1019-PAB');
x update job_sample set job_status=0 where job_number in ('ELN-1020-PAB','ELN-1019-PAB');


*/

--XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 ---- 2018-05-26 ---
 /*16813
 select * from tb_m_specification where template_id=956;
 update tb_m_specification set E='silicone (adhesive side)',F='silicone (Facing adhesive side)' where template_id=638 and id=19378;

 ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `date_labman_complete` `date_labman_complete` DATE NULL DEFAULT NULL AFTER `date_admin_sent_to_cus`;
ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `date_chemist_analyze` `date_chemist_analyze` DATE NULL DEFAULT NULL ;
ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `date_srchemist_analyze` DATE NULL AFTER `date_chemist_complete`;
ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `date_labman_analyze` DATE NULL AFTER `date_admin_sent_to_cus`,
ADD COLUMN `job_samplecol` VARCHAR(45) NULL AFTER `last_status`;
ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `date_admin_sent_to_cus` `date_admin_sent_to_cus` DATE NULL DEFAULT NULL AFTER `date_labman_complete`;

ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `date_login_received_sample` `date_login_inprogress` DATE NULL DEFAULT NULL ;
ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `date_login_complete` VARCHAR(45) NULL AFTER `job_samplecol`;
ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `date_login_complete` `date_login_complete` DATE NULL DEFAULT NULL AFTER `date_login_inprogress`;
ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `date_admin_word_inprogress` DATE NULL AFTER `date_srchemist_complate`,
ADD COLUMN `date_admin_word_complete` DATE NULL AFTER `date_admin_word_inprogress`,
ADD COLUMN `date_admin_pdf_inprogress` DATE NULL AFTER `date_admin_sent_to_cus`,
ADD COLUMN `date_admin_pdf_complete` DATE NULL AFTER `date_admin_pdf_inprogress`;


 ---- 2018-05-31 ---
UPDATE `alsi`.`m_status` SET `status` = 'I' WHERE (`ID` = '17');
UPDATE `alsi`.`m_status` SET `status` = 'I' WHERE (`ID` = '16');
UPDATE `alsi`.`m_status` SET `status` = 'I' WHERE (`ID` = '18');


-------------- 2018-06-03 --------------------
ALTER TABLE `alsi`.`template_wd_gcms_coverpage` 
CHANGE COLUMN `pm_extraction_volumn` `pm_extraction_volumn` VARCHAR(255) NULL DEFAULT NULL ;



---------------- 2018-06-29 -----------------------
ALTER TABLE `alsi`.`template_seagate_lpc_coverpage` 
ADD COLUMN `decimal01` INT NULL AFTER `unit`,
ADD COLUMN `decimal02` INT NULL AFTER `decimal01`,
ADD COLUMN `decimal03` INT NULL AFTER `decimal02`,
ADD COLUMN `decimal04` INT NULL AFTER `decimal03`,
ADD COLUMN `decimal05` INT NULL AFTER `decimal04`;


----------------------------------------------------
CREATE TABLE `alsi`.`sample_method_procedure` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `sample_id` INT NULL,
    `row_type` INT NULL,
    `col_1` VARCHAR(200) NULL,
    `col_2` VARCHAR(200) NULL,
    `col_3` VARCHAR(200) NULL,
    `col_4` VARCHAR(200) NULL,
    `col_5` VARCHAR(200) NULL,
    `col_6` VARCHAR(200) NULL,
    `col_7` VARCHAR(200) NULL,
    `col_8` VARCHAR(200) NULL,
    `col_9` VARCHAR(200) NULL,
    `col_10` VARCHAR(200) NULL,
    `col_11` VARCHAR(200) NULL,
    `col_12` VARCHAR(200) NULL,
    `col_13` VARCHAR(200) NULL,
    `col_14` VARCHAR(200) NULL,
    `col_15` VARCHAR(200) NULL,
    `col_16` VARCHAR(200) NULL,
    `col_17` VARCHAR(200) NULL,
    `col_18` VARCHAR(200) NULL,
	`col_19` VARCHAR(200) NULL,
	`col_20` VARCHAR(200) NULL,
    PRIMARY KEY (`id`)
);


---- 2018-08-03 ------
CREATE TABLE `alsi`.`ws_hash_value` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sample_id` INT NULL,
  `key` VARCHAR(45) NULL,
  `val` VARCHAR(45) NULL,
  PRIMARY KEY (`id`));

ALTER TABLE `alsi`.`template_f_ic` 
ADD COLUMN `specification_id` INT NULL AFTER `sample_id`,
ADD COLUMN `isNoSpec` TINYINT NULL DEFAULT 0 AFTER `specification_id`;

ALTER TABLE `alsi`.`template_f_ic` 
ADD COLUMN `unit` INT NULL AFTER `isNoSpec`;

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `note` VARCHAR(200) NULL AFTER `last_status`;


ALTER TABLE `alsi`.`job_sample` 
CHANGE COLUMN `job_samplecol` `note` VARCHAR(200) NULL DEFAULT NULL ;





------------------------ 2018-09-02
select  *  from m_completion_scheduled

UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='3' WHERE `ID`='3';
UPDATE `alsi`.`m_completion_scheduled` SET `customer_due_date`='3' WHERE `ID`='3';









------------------------ 2018-09-11 ------------------------
INSERT INTO `alsi`.`m_completion_scheduled` (`ID`, `name`, `lab_due_date`, `customer_due_date`) VALUES ('4', 'Extend 1', '10', '11');
INSERT INTO `alsi`.`m_completion_scheduled` (`ID`, `name`, `lab_due_date`, `customer_due_date`) VALUES ('5', 'Extend 2', '15', '16');



------------------------ 2018-09-17 ------------------------

ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `am_retest_remark` VARCHAR(200) NULL AFTER `note_lab`;

---------------------- 2018-10-04 --------------------------------
INSERT INTO `alsi`.`menu` (`MENU_ID`, `MENU_NAME`, `URL_NAVIGATE`, `MENU_TAG`, `PREVIOUS_MENU_ID`, `DISPLAY_ORDER`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('13', 'Report', '/alis/view/request/SearchReport.aspx', 'SearchReport|', '1', '1', 'SYSTEM', '2018-10-04', '2018-10-04');
UPDATE `alsi`.`menu` SET `URL_NAVIGATE`='' WHERE `MENU_ID`='13';
INSERT INTO `alsi`.`menu` (`MENU_ID`, `MENU_ICON`, `MENU_NAME`, `URL_NAVIGATE`, `PREVIOUS_MENU_ID`, `DISPLAY_ORDER`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('14', 'icon-grid', 'Summary Income', '/alis/view/request/SumIncome.aspx', '13', '1', 'SYSTEM', '2018-10-04', '2018-10-04');

INSERT INTO `alsi`.`menu_role` (`ROLE_ID`, `MENU_ID`, `IS_REQUIRED_ACTION`, `IS_CREATE`, `IS_EDIT`, `IS_DELETE`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('7', '13', '1', '1', '1', '1', 'SYSTEM', '2015-02-13', '2015-02-13');
INSERT INTO `alsi`.`menu_role` (`ROLE_ID`, `MENU_ID`, `IS_REQUIRED_ACTION`, `IS_CREATE`, `IS_EDIT`, `IS_DELETE`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('7', '14', '1', '1', '1', '1', 'SYSTEM', '2015-02-13', '2015-02-13');
UPDATE `alsi`.`menu` SET `PREVIOUS_MENU_ID`=NULL WHERE `MENU_ID`='14';
UPDATE `alsi`.`menu` SET `MENU_ICON`='', `PREVIOUS_MENU_ID`='13' WHERE `MENU_ID`='14';
UPDATE `alsi`.`menu` SET `MENU_ICON`='icon-grid', `PREVIOUS_MENU_ID`='' WHERE `MENU_ID`='13';
 update alsi.menu set url_navigate=null,previous_menu_id=null where menu_id=13;



 ALTER TABLE `alsi`.`job_sample` 
ADD COLUMN `sample_invoice_date` DATE NULL AFTER `sample_invoice`,
ADD COLUMN `sample_invoice_amount` DOUBLE NULL AFTER `sample_invoice_date`;

==============
INSERT INTO `alsi`.`menu` (`MENU_ID`, `MENU_ICON`, `MENU_NAME`, `URL_NAVIGATE`, `MENU_TAG`, `PREVIOUS_MENU_ID`, `DISPLAY_ORDER`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('15', 'icon-settings', 'Holiday Calendar', '/alis/view/maintenance/SearchHolidayCalendar.aspx', 'SearchHolidayCalendar|', '3', '6', 'SYSTEM', '2018-10-28', '2018-10-28');




#2018-12-25

ALTER TABLE `alsi`.`holiday_calendar` 
ADD PRIMARY KEY (`DATE_HOLIDAYS`);

CREATE TABLE `alsi`.`tb_m_forcast` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `forcast_year` INT NULL,
  `forcast_month` INT NULL,
  `forcast_amt` DOUBLE NULL,
  PRIMARY KEY (`ID`));



  INSERT INTO `alsi`.`menu` (`MENU_ID`, `MENU_NAME`, `URL_NAVIGATE`, `MENU_TAG`, `PREVIOUS_MENU_ID`, `DISPLAY_ORDER`, `UPDATE_BY`, `CREATE_DATE`, `UPDATE_DATE`) VALUES ('14', 'Update BI-Report', '/alis/view/maintenance/MaintenanceAccount.aspx', 'MaintenanceAccount|', '3', '8', 'SYSTEM', '2015-02-13', '2015-02-13');


  -- 
      <add key="PATH_TMP" value="D:\Deploy\uploads\tmp\{0}" />

*/