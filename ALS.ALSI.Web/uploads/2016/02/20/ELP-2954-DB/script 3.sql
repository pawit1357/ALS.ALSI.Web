select * from request_borrow b 
left join request_borrow_equipment_type bet on bet.request_borrow_id = b.id
left join request_borrow_equipment_type_item beti on bet.equipment_type_list_id = beti.request_borrow_equipment_type_id
left join equipment e on e.id = beti.equipment_id
-- where e.barcode='404000010590'