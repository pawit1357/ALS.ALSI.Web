select * from m_template where path_url like '%wd_cor%' and status='A';

select id,template_id,job_number,job_status from job_sample 
where  template_id in(492); 
-- and job_status=13 
-- and id=3823;

select * from tb_m_specification where template_id=492;


-- select * from template_wd_corrosion_coverpage where sample_id=4088;



select * from template_seagate_hpa_coverpage where sample_id=3823;

-- 34843

select * from tb_m_component where template_id=480 order by id desc;

-- update tb_m_component set id



select * from template_wd_corrosion_coverpage where sample_id=3726;