-- Find template & user
select 
ms.name as jobStatus
,js.job_number as jobNumber 
,mt.id as templateId
-- ,mt.name as templateName 
,mt.path_url as template_path_url
-- mtot.data_group,
, 'pikul.totassa' as loginUsr,
(
	SELECT  username -- GROUP_CONCAT(username SEPARATOR ', ')
	FROM users_login ul  
	where ul.responsible_test like CONCAT('%', mtot.data_group, '%') limit 1
) as checkUser
, 'rossukhon.khongphuwet' as srChemUsr
, 'orapin.maliwan' as adminUsr
, 'warunee.maneesuwan' as labManagerUsr
, 'chayan.jutaphan' as managerUsr
, 'wanwisa.phasang,pornpan' as accountUsr
, 'kamonworanee.theptaranonth' as marketingUsr
from job_sample js
left join m_status ms on ms.ID = js.job_status 
left join m_template mt on mt.ID = js.template_id 
left join m_type_of_test  mtot on mtot.ID = js.type_of_test_id 
where mt.status ='A' and mt.path_url like '%WD_MESA%'