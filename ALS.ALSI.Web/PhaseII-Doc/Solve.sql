/*

select m_template.path_url,job_sample.id,job_sample.job_number,job_sample.job_status,m_status.name,m_type_of_test.data_group,
(select GROUP_CONCAT(username SEPARATOR ',') from users_login where responsible_test like CONCAT('%',m_type_of_test.data_group,'%') ) as chemist
from job_sample
left join m_type_of_test on job_sample.type_of_test_id = m_type_of_test.id
left join m_status on m_status.ID=job_sample.job_status
left join m_template on job_sample.template_id = m_template.id
where m_template.path_url like '%WD_LPC%' 
and m_template.status='A'
order by job_sample.job_status;

*/
-- ELP-3150-HB

-- ELP-3334-MB
select *  from m_template where id=627;

-- template_wd_ftir_coverpage

select * from template_wd_ftir_coverpage where sample_id=8515;


select * from job_sample where id=11010;


select * from template_wd_lpc_coverpage where sample_id=11010;






