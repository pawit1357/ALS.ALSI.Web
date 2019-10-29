
1. Open Services (services.msc) and restart MySQL57 service.
2. Execute the following commands in MySQL.
   use <<database name>>;
   set global optimizer_switch='derived_merge=off';
3. Update the .edmx.

====== ย้ายเครื่องใหม่
1. ลบ folder .vs ออก
2. ติดตั้ง iis เลือก asp.net .......
3. Install mysql-connector-net-6.8.8





ALTER TABLE `alsi`.`job_sample_group_so` 
CHANGE COLUMN `unit_price` `unit_price` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `report_no` `report_no` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `quantity` `quantity` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `so_desc` `so_desc` TEXT NULL DEFAULT NULL ,
CHANGE COLUMN `so_company` `so_company` TEXT NULL DEFAULT NULL ;