select * from tb_m_component where template_id=(SELECT max(id) FROM alsi.m_template order by id desc);
select * from tb_m_detail_spec where template_id=(SELECT max(id) FROM alsi.m_template order by id desc);
select * from tb_m_detail_spec_ref where template_id=(SELECT max(id) FROM alsi.m_template order by id desc);
select * from tb_m_specification where template_id=(SELECT max(id) FROM alsi.m_template order by id desc);

