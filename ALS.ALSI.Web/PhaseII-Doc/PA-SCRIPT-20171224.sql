-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************
--update job_sample set sample_prefix=SUBSTRING_INDEX(job_number,'-',1);

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
--ALTER TABLE `alsi`.`tb_m_specification` 
--ADD COLUMN `status` VARCHAR(1) NULL DEFAULT 'A' AFTER `BZ`;


--CREATE TABLE `template_img` (
--  `id` int(11) NOT NULL AUTO_INCREMENT,
--  `sample_id` int(11) DEFAULT NULL,
--  `seq` int(11) DEFAULT '0',
--  `img_path` varchar(255) DEFAULT NULL,
--  PRIMARY KEY (`id`)
--) ENGINE=InnoDB AUTO_INCREMENT=402 DEFAULT CHARSET=utf8;



--ALTER TABLE `alsi`.`m_type_of_test` 
--ADD COLUMN `ref_template_id` INT NULL AFTER `data_group`;


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

-- INSERT INTO `alsi`.`m_specification` (`ID`, `name`, `status`) VALUES ('27', 'PA(REPORT)', 'A');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('219', '27', 'PAB', 'PA_REPORT1', '1', 'A', 'PA');
-- INSERT INTO `alsi`.`m_type_of_test` (`ID`, `specification_id`, `prefix`, `name`, `parent`, `status`, `data_group`) VALUES ('220', '27', 'PAB', 'PA_REPORT2', '1', 'A', 'PA');


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

*/