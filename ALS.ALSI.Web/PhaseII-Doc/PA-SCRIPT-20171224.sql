-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************

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

#
ALTER TABLE `alsi`.`template_wd_ftir_coverpage` 
ADD COLUMN `ftr_b53` VARCHAR(45) NULL AFTER `ftir_unit`,
ADD COLUMN `ftr_b54` VARCHAR(45) NULL AFTER `ftr_b53`,
ADD COLUMN `ftr_b55` VARCHAR(45) NULL AFTER `ftr_b54`,
ADD COLUMN `ftr_b56` VARCHAR(45) NULL AFTER `ftr_b55`,
ADD COLUMN `ftr_b58` VARCHAR(45) NULL AFTER `ftr_b56`,
ADD COLUMN `ftr_c62` VARCHAR(45) NULL AFTER `ftr_b58`,
ADD COLUMN `ftr_c63` VARCHAR(45) NULL AFTER `ftr_c62`;

#
ALTER TABLE `alsi`.`template_wd_ftir_coverpage` 
ADD COLUMN `dop_unit` VARCHAR(45) NULL AFTER `silicone_unit`;


#
update template_seagate_copperwire_coverpage set result=replace(result,'Pass Level 0','Pass (Level 0)'),result2=replace(result2,'Pass Level 0','Pass (Level 0)'),result3=replace(result3,'Pass Level 0','Pass (Level 0)');

#
update template_seagate_lpc_coverpage set LiquidParticleCount=replace(LiquidParticleCount,'μ','u')



*/