/*

select m_template.id,m_template.path_url,m_template.name,job_sample.id,job_sample.job_number,job_sample.job_status,m_status.name,m_type_of_test.data_group,
(select GROUP_CONCAT(username SEPARATOR ',') from users_login where responsible_test like CONCAT('%',m_type_of_test.data_group,'%') ) as chemist
from job_sample
left join m_type_of_test on job_sample.type_of_test_id = m_type_of_test.id
left join m_status on m_status.ID=job_sample.job_status
left join m_template on job_sample.template_id = m_template.id
where m_template.path_url like '%ELP-0562-G%' 
and m_template.status='A'
order by job_sample.job_status;

*/
select * from m_template where id=908;
select * from template_wd_ftir_coverpage where sample_id=8515;
select * from job_sample where id=10820;
select * from template_wd_lpc_coverpage where sample_id=11010;

select * from job_sample where job_number='ELN-0856-PAB';
select * from template_seagate_lpc_coverpage where sample_id=11175 ;
select * from template_seagate_hpa_coverpage where hpa_type=7 and sample_id=10731 and b like '%NiP with AlMgSi%';

select * from tb_m_specification where template_id=908;
select * from tb_m_component where template_id=908;
select * from tb_m_detail_spec where template_id=908;



-- check num of uncomplete last physical year (2017)
select count(job_sample.job_number) -- ,job_info.date_of_receive,job_sample.job_status 
from job_sample where job_id in (select id from job_info
where job_info.date_of_receive <= '2018-03-31') and job_sample.job_status <> 3;

-- update all to job_complete
update  job_sample set job_sample.job_status=3  where job_id in (select id from job_info
where job_info.date_of_receive <= '2018-03-31') and job_sample.job_status <> 3;


-- update job to "job_delete"
select * from job_sample where job_number in ('ELN-1020-PAB','ELN-1019-PAB');
update job_sample set job_status=0 where job_number in ('ELN-1020-PAB','ELN-1019-PAB');


update job_sample set job_status=12 where job_number='ELN-0002-PAB';