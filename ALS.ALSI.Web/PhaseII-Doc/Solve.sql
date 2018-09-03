/*

select m_template.id,m_template.path_url,m_template.name,job_sample.id,job_sample.job_number,job_sample.job_status,m_status.name,m_type_of_test.data_group,
(select GROUP_CONCAT(username SEPARATOR ',') from users_login where responsible_test like CONCAT('%',m_type_of_test.data_group,'%') ) as chemist
from job_sample
left join m_type_of_test on job_sample.type_of_test_id = m_type_of_test.id
left join m_status on m_status.ID=job_sample.job_status
left join m_template on job_sample.template_id = m_template.id
where m_template.path_url like '%Seagate_GCMS_3%' 
and m_template.status='A'
order by job_sample.job_status;

*/

select * from m_template where path_url like '%Seagate_FTIR_Adhesive%';

select * from m_template where id=648;
select * from template_wd_ftir_coverpage where sample_id=8515;
select * from job_sample where id=13728;
select * from template_wd_lpc_coverpage where sample_id=11010;

select * from job_sample where job_number='ELP-3160-LB';
select * from template_seagate_lpc_coverpage where sample_id=11175 ;
select * from template_seagate_hpa_coverpage where hpa_type=7 and sample_id=10731 and b like '%NiP with AlMgSi%';

select * from tb_m_specification where template_id=633;
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


-- XXXXXXXXXXXXXXXXXXXXXXXXXXX SOLVE CASE AM/RETEST XXXXXXXXXXXXXXXXXXXXXX --
UPDATE `alsi`.`job_sample` SET `date_chemist_analyze` = '2017-09-25', `date_chemist_complete` = '2017-09-20', `date_srchemist_complate` = '2017-09-27' WHERE (`ID` = '13833');


select job_number,`date_chemist_analyze`,`date_chemist_complete`,date_srchemist_complate 
from job_sample where job_number = 'ELP-1687-FB' and job_status=3 ;




select * from tb_m_component where template_id=648;




