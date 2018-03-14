/*

select job_sample.job_number,job_sample.job_status,m_status.name,m_type_of_test.data_group,
(select GROUP_CONCAT(username SEPARATOR ',') from users_login where responsible_test like CONCAT('%',m_type_of_test.data_group,'%') ) as chemist
from job_sample
left join m_type_of_test on job_sample.type_of_test_id = m_type_of_test.id
left join m_status on m_status.ID=job_sample.job_status
where template_id in(
select id from m_template where path_url like '%WD_MESA_IDM.ascx' and status='A') order by job_status;

*/
