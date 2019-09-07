
1. Open Services (services.msc) and restart MySQL57 service.
2. Execute the following commands in MySQL.
   use <<database name>>;
   set global optimizer_switch='derived_merge=off';
3. Update the .edmx.

====== ย้ายเครื่องใหม่
1. ลบ folder .vs ออก
2. ติดตั้ง iis เลือก asp.net .......
3. Install mysql-connector-net-6.8.8