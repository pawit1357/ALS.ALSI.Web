-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************

-- ELN-0664-PAB
-- ELP-2475-HB 
-- WD_MESA_IDM.ascx/ELP-2292-MB (rattana)
-- WD_MESA_IDM.ascx/ELP-2072-MB



/*

ELP-2480-HB ***** seagate hpa swap


ALTER TABLE `alsi`.`template_seagate_hpa_coverpage` 
ADD COLUMN `note_pzt` VARCHAR(255) NULL AFTER `unit3`,
ADD COLUMN `show_note_pzt` TINYINT NULL DEFAULT 1 AFTER `note_pzt`;

*/