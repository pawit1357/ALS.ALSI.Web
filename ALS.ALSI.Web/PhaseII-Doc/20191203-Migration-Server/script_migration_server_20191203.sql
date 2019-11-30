update alsi.template_img set img_path = replace(img_path,'172.18.105.6','172.18.145.6');
update alsi.template_seagate_copperwire_img set path_img1 = replace(path_img1,'172.18.105.6','172.18.145.6');
update alsi.template_seagate_corrosion_img set path_img1 = replace(path_img1,'172.18.105.6','172.18.145.6');
update alsi.template_seagate_gcms_coverpage_img set path_img1 = replace(path_img1,'172.18.105.6','172.18.145.6');
update alsi.template_seagate_mesa_img set path_sem_image_at_250x = replace(path_sem_image_at_250x,'172.18.105.6','172.18.145.6'),path_sem_image_at_500x = replace(path_sem_image_at_500x,'172.18.105.6','172.18.145.6'),path_sem_image_at_2000x = replace(path_sem_image_at_2000x,'172.18.105.6','172.18.145.6'),path_edx_spectrum = replace(path_edx_spectrum,'172.18.105.6','172.18.145.6');
update alsi.template_wd_corrosion_img set path_img1 = replace(path_img1,'172.18.105.6','172.18.145.6');
update alsi.template_wd_mesa_img set path_sem_image_at_250x = replace(path_sem_image_at_250x,'172.18.105.6','172.18.145.6'),path_sem_image_at_500x = replace(path_sem_image_at_500x,'172.18.105.6','172.18.145.6'),path_sem_image_at_2000x = replace(path_sem_image_at_2000x,'172.18.105.6','172.18.145.6'),path_edx_spectrum = replace(path_edx_spectrum,'172.18.105.6','172.18.145.6');

ALTER TABLE alsi.template_wd_dhs_coverpage
CHANGE COLUMN `analytes` `analytes` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `specification_limits` `specification_limits` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `result` `result` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `result_pass_or_false` `result_pass_or_false` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `pm_procedure_no` `pm_procedure_no` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `pm_number_of_pieces_used_for_extraction` `pm_number_of_pieces_used_for_extraction` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `pm_extraction_medium` `pm_extraction_medium` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `pm_extraction_volume` `pm_extraction_volume` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `sample_size` `sample_size` VARCHAR(500) NULL DEFAULT NULL ;

ALTER TABLE alsi.job_sample 
CHANGE COLUMN `path_word` `path_word` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `path_pdf` `path_pdf` VARCHAR(500) NULL DEFAULT NULL ,
CHANGE COLUMN `ad_hoc_tempalte_path` `ad_hoc_tempalte_path` VARCHAR(500) NULL DEFAULT NULL ;


