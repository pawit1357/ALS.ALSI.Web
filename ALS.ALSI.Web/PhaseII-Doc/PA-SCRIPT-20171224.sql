-- *******************************************************************************************************************************
-- 2017-12-24 (PA REPORT)
-- *******************************************************************************************************************************

-- ELN-0664-PAB
-- ELP-2475-HB 
-- WD_MESA_IDM.ascx/ELP-2292-MB (rattana)
-- WD_MESA_IDM.ascx/ELP-2072-MB



/*

ELP-2480-HB ***** seagate hpa swap
ELP-2824-LB ** wd lpc
ELP-3028B-HB
3128-DB
ELP-3276-DB
ELP-1942-DB


fix
3151-HB

ELP-2544-DB มากกว่า 30 
- ELP-3218-DB
- ELP-3183-DB
- ELP-2545-DB


----------
- เพราะเหมือนเขาว่า  เขา sort วันที่แล้วมันไม่เรียง เพราะ 1-9มันไม่มีเลข0นำหน้า มันเลยทำให้มันเรียงกันแบบไม่ถูกเช่น งานที่เข้าวันที่ 2มันจะเรียงอยู่หลังงานที่ เข้า 19

- hpa อัพเดท script u
- hpa fill กระดาษแสดงไม่พอดี
-  search SEM/EDX แล้วดึงข้อมูลออกมา มันออกมาเฉพาะ type of test ที่เป็น SEM/EDXที่เป็น FA กับ ELN แต่พวกที่เป็น ELP   HPA & MESA Ghost พวกนี้ ไม่ออกมา
complete 
- lpc seagate fix analysis แสดงชื่อซ้ำ เช่น LPC (132 KHz) แสดงเป็น LPC (LPC 132 KHz)
- corrosion tank
- dhs seagate (v2) เกินหน้ากระดาษ
- 3063 (hpa swap ) error ELP-3063-HB
- 3443-cvr CVR WD error Extraction Medium (ข้อความมีความยาวเกินไป)


update template_seagate_hpa_coverpage set A = replace(A,'μ','u')
update template_seagate_lpc_coverpage set LiquidParticleCount = replace(LiquidParticleCount,'μ','u');


ALTER TABLE `alsi`.`template_wd_gcms_coverpage` 
CHANGE COLUMN `pm_extraction_medium` `pm_extraction_medium` VARCHAR(255) NULL DEFAULT NULL ;

*/