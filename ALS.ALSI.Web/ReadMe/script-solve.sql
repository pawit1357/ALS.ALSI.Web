
select s.id,s.template_id,s.job_number,ms.name,sp.name as specification,(
SELECT  GROUP_CONCAT(username SEPARATOR ', ') as userlogin
FROM users_login where responsible_test is not null and responsible_test like tot.data_group GROUP BY responsible_test) as username
from job_sample s 
left join m_specification sp on s.specification_id = sp.id
left join m_status ms on s.job_status = ms.id
left join m_type_of_test tot on s.type_of_test_id = tot.id
where  template_id in(select id from m_template where path_url like '%wd_cor%' and status='A'); 



