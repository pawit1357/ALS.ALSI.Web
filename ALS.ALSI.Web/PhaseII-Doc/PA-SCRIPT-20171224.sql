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


$("#req-department_id")

 var txt_pathogen_name = $('#txt_pathogen_name');
 var txt_pathogen_code = $('#txt_pathogen_code');
 var txt_pathogen_volume = $('#txt_pathogen_volume');
 var txt_supervisor = $('#txt_supervisor');
 var txt_manufacture_plant = $('#txt_manufacture_plant');
 var txt_manufacture_fuse = $('#txt_manufacture_fuse');
 var txt_manufacture_prepare = $('#txt_manufacture_prepare');
 var txt_manufacture_transform = $('#txt_manufacture_transform');
 var txt_manufacture_packing = $('#txt_manufacture_packing');
 var txt_manufacture_total_packing = $('#txt_manufacture_total_packing');
 var txt_distribute_sell = $('#txt_distribute_sell');
 var txt_distribute_pay = $('#txt_distribute_pay');
 var txt_distribute_give = $('#txt_distribute_give');
 var txt_distribute_exchange = $('#txt_distribute_exchange');
 var txt_distribute_donate = $('#txt_distribute_donate');
 var txt_distribute_lost = $('#txt_distribute_lost');
 var txt_distribute_discard = $('#txt_distribute_discard');
 var txt_distribute_destroy = $('#txt_distribute_destroy');
 var txt_import = $('#txt_import');
 var txt_export = $('#txt_export');
 var txt_import_to_other = $('#txt_import_to_other');
 ----------------------

'<td><input style="width : 100px !important;" id="pathogen_name" type="text" value="'+txt_pathogen_name+'"class="form-control" name="pathogen_name[]"></td>'+
'<td><input style="width : 100px !important;" id="pathogen_code" type="text" value="'+txt_pathogen_code+'"class="form-control" name="pathogen_code[]"></td>'+
'<td><input style="width : 100px !important;" id="pathogen_volume" type="text" value="'+txt_pathogen_volume+'"class="form-control" name="pathogen_volume[]"></td>'+
'<td><input style="width : 100px !important;" id="supervisor" type="text" value="'+txt_supervisor+'"class="form-control" name="supervisor[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_plant" type="text" value="'+txt_manufacture_plant+'"class="form-control" name="manufacture_plant[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_fuse" type="text" value="'+txt_manufacture_fuse+'"class="form-control" name="manufacture_fuse[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_prepare" type="text" value="'+txt_manufacture_prepare+'"class="form-control" name="manufacture_prepare[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_transform" type="text" value="'+txt_manufacture_transform+'"class="form-control" name="manufacture_transform[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_packing" type="text" value="'+txt_manufacture_packing+'"class="form-control" name="manufacture_packing[]"></td>'+
'<td><input style="width : 60px !important;" id="manufacture_total_packing" type="text" value="'+txt_manufacture_total_packing+'"class="form-control" name="manufacture_total_packing[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_sell" type="text" value="'+txt_distribute_sell+'"class="form-control" name="distribute_sell[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_pay" type="text" value="'+txt_distribute_pay+'"class="form-control" name="distribute_pay[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_give" type="text" value="'+txt_distribute_give+'"class="form-control" name="distribute_give[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_exchange" type="text" value="'+txt_distribute_exchange+'"class="form-control" name="distribute_exchange[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_donate" type="text" value="'+txt_distribute_donate+'"class="form-control" name="distribute_donate[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_lost" type="text" value="'+txt_distribute_lost+'"class="form-control" name="distribute_lost[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_discard" type="text" value="'+txt_distribute_discard+'"class="form-control" name="distribute_discard[]"></td>'+
'<td><input style="width : 60px !important;" id="distribute_destroy" type="text" value="'+txt_distribute_destroy+'"class="form-control" name="distribute_destroy[]"></td>'+
'<td><input style="width : 60px !important;" id="import" type="text" value="'+txt_import+'"class="form-control" name="import[]"></td>'+
'<td><input style="width : 60px !important;" id="export" type="text" value="'+txt_export+'"class="form-control" name="export[]"></td>'+
'<td><input style="width : 60px !important;" id="import_to_other" type="text" value="'+txt_import_to_other+'"class="form-control" name="import_to_other[]"></td>'+

<td><input style="width : 100px !important;" id="txt_pathogen_name" type="text" value=""class="form-control"></td>
<td><input style="width : 100px !important;" id="txt_pathogen_code" type="text" value=""class="form-control"></td>
<td><input style="width : 100px !important;" id="txt_pathogen_volume" type="text" value=""class="form-control"></td>
<td><input style="width : 100px !important;" id="txt_supervisor" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_plant" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_fuse" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_prepare" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_transform" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_packing" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_manufacture_total_packing" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_sell" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_pay" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_give" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_exchange" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_donate" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_lost" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_discard" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_distribute_destroy" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_import" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_export" type="text" value=""class="form-control"></td>
<td><input style="width : 60px !important;" id="txt_import_to_other" type="text" value=""class="form-control"></td>

 -----------------------

 	    	    	'<td>'+txt_pathogen_name+'</td>'+
	    	    	'<td>'+txt_pathogen_code+'</td>'+
	    	    	'<td>'+txt_pathogen_volume +'</td>'+
	    	    	'<td>'+txt_supervisor+'</td>'+
	    	    	'<td>'+txt_manufacture_plant+'</td>'+
	    	    	'<td>'+txt_manufacture_fuse+'</td>'+
	    	    	'<td>'+txt_manufacture_prepare+'</td>'+
	    	    	'<td>'+txt_manufacture_transform+'</td>'+
	    	    	'<td>'+txt_manufacture_packing+'</td>'+
	    	    	'<td>'+txt_manufacture_total_packing+'</td>'+
	    	    	'<td>'+txt_distribute_sell+'</td>'+
	    	    	'<td>'+txt_distribute_pay+'</td>'+
	    	    	'<td>'+txt_distribute_give+'</td>'+
	    	    	'<td>'+txt_distribute_exchange+'</td>'+
	    	    	'<td>'+txt_distribute_donate+'</td>'+
	    	    	'<td>'+txt_distribute_lost+'</td>'+
	    	    	'<td>'+txt_distribute_discard+'</td>'+
	    	    	'<td>'+txt_distribute_destroy+'</td>'+
	    	    	'<td>'+txt_import+'</td>'+
	    	    	'<td>'+txt_export+'</td>'+
	    	    	'<td>'+txt_import_to_other+'</td>'+
	    	    	'<td>'++'</td>'+


					pathogen_name[]
					pathogen_code[]
					pathogen_volume[]
					supervisor[]
					manufacture_plant[]
					manufacture_fuse[]
					manufacture_prepare[]
					manufacture_transform[]
					manufacture_packing[]
					manufacture_total_packing[]
					distribute_sell[]
					distribute_pay[]
					distribute_give[]
					distribute_exchange[]
					distribute_donate[]
					distribute_lost[]
					distribute_discard[]
					distribute_destroy[]
					import[]
					export[]
					import_to_other[]

					txt_pathogen_name
					txt_pathogen_code
					txt_pathogen_volume
					txt_supervisor
					txt_manufacture_plant
					txt_manufacture_fuse
					txt_manufacture_prepare
					txt_manufacture_transform
					txt_manufacture_packing
					txt_manufacture_total_packing
					txt_distribute_sell
					txt_distribute_pay
					txt_distribute_give
					txt_distribute_exchange
					txt_distribute_donate
					txt_distribute_lost
					txt_distribute_discard
					txt_distribute_destroy
					txt_import
					txt_export
					txt_import_to_other





					----------------
					pathogen_name
					pathogen_code
					pathogen_volume
					supervisor
					manufacture_plant
					manufacture_fuse
					manufacture_prepare
					manufacture_transform
					manufacture_packing
					manufacture_total_packing
					distribute_sell
					distribute_pay
					distribute_give
					distribute_exchange
					distribute_donate
					distribute_lost
					distribute_discard
					distribute_destroy
					import
					export
					import_to_other

					--
					$pathogen_name = $_POST['pathogen_name']
					$pathogen_code = $_POST['pathogen_code']
					$pathogen_volume = $_POST['pathogen_volume']
					$supervisor = $_POST['supervisor']
					$manufacture_plant = $_POST['manufacture_plant']
					$manufacture_fuse = $_POST['manufacture_fuse']
					$manufacture_prepare = $_POST['manufacture_prepare']
					$manufacture_transform = $_POST['manufacture_transform']
					$manufacture_packing = $_POST['manufacture_packing']
					$manufacture_total_packing = $_POST['manufacture_total_packing']
					$distribute_sell = $_POST['distribute_sell']
					$distribute_pay = $_POST['distribute_pay']
					$distribute_give = $_POST['distribute_give']
					$distribute_exchange = $_POST['distribute_exchange']
					$distribute_donate = $_POST['distribute_donate']
					$distribute_lost = $_POST['distribute_lost']
					$distribute_discard = $_POST['distribute_discard']
					$distribute_destroy = $_POST['distribute_destroy']
					$import = $_POST['import']
					$export = $_POST['export']
					$import_to_other = $_POST['import_to_other']

					------
					$model->pathogen_name
					$model->pathogen_code
					$model->pathogen_volume
					$model->supervisor
					$model->manufacture_plant
					$model->manufacture_fuse
					$model->manufacture_prepare
					$model->manufacture_transform
					$model->manufacture_packing
					$model->manufacture_total_packing
					$model->distribute_sell
					$model->distribute_pay
					$model->distribute_give
					$model->distribute_exchange
					$model->distribute_donate
					$model->distribute_lost
					$model->distribute_discard
					$model->distribute_destroy
					$model->import
					$model->export
					$model->import_to_other



					 
					----------------------
					UPDATE `alsi`.`m_completion_scheduled` SET `lab_due_date`='2', `customer_due_date`='2' WHERE `ID`='3';
					Description: An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code. 

Exception Details: System.IO.FileNotFoundException: Could not find file 'c:\windows\system32\inetsrv\reflectiveTable_A03359.xml'.

Source Error: 

An unhandled exception was generated during the execution of the current web request. Information regarding the origin and location of the exception can be identified using the exception stack trace below.

Stack Trace: 


[FileNotFoundException: Could not find file 'c:\windows\system32\inetsrv\reflectiveTable_A03359.xml'.]
   System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath) +1346
   System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost) +1132
   System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost) +146
   System.IO.StreamReader..ctor(String path, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean checkHost) +179
   System.IO.StreamReader..ctor(String path) +53
   ALS.ALSI.Web.view.template.PA03.btnLoadFile_Click(Object sender, EventArgs e) in D:\Work\Outsource\ALS.ALSI.Web\ALS.ALSI.Web\view\template\PA03.ascx.cs:1399
   System.Web.UI.WebControls.Button.OnClick(EventArgs e) +11773973
   System.Web.UI.WebControls.Button.RaisePostBackEvent(String eventArgument) +150

----------------
ELN-PA-TEST01 (ใช้ทดสอบที่ ALS)
*/