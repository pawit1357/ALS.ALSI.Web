
delete from template_seagate_dhs_coverpage;
delete from template_seagate_ftir_coverpage;
delete from template_seagate_hpa_coverpage;
delete from template_seagate_ic_coverpage;
delete from template_seagate_lpc_coverpage;
delete from template_wd_dhs_coverpage;
delete from template_wd_ftir_coverpage;
delete from template_wd_gcms_coverpage;
delete from template_wd_hpa_for1_coverpage;
delete from template_wd_hpa_for3_coverpage;
delete from template_wd_ic_coverpage;
delete from template_seagate_ic_coverpage;
delete from template_wd_ir_coverpage;
delete from template_wd_lpc_coverpage;
delete from template_wd_mesa_coverpage;
delete from template_wd_mesa_img;

delete from tb_m_dhs_cas;
-- delete from tb_m_gcms_cas;
-- delete from job_sample_logs;

update job_sample set job_status=11; -- 10>Convert Template,11>Select Spec
-- delete from job_sample;
-- delete from job_info;


