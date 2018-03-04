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
-  search SEM/EDX แล้วดึงข้อมูลออกมา มันออกมาเฉพาะ type of test ที่เป็น SEM/EDX ที่เป็น FA กับ ELN แต่พวกที่เป็น ELP   HPA & MESA Ghost พวกนี้ ไม่ออกมา
complete 
-- CLEAR - lpc seagate fix analysis แสดงชื่อซ้ำ เช่น LPC (132 KHz) แสดงเป็น LPC (LPC 132 KHz)
-- CLEAR - corrosion tank
-- CLEAR - dhs seagate (v2) เกินหน้ากระดาษ
-- CLEAR - 3063 (hpa swap ) error ELP-3063-HB
-- CLEAR - 3443-cvr CVR WD error Extraction Medium (ข้อความมีความยาวเกินไป)

-FTIR WD  IDM  ตัวที่บอกว่า พออัพแล้วมันติด น่าจะมาจากการระบุให้ดึงข้อมมูลผิดที่
- ELP-3218-DB  ---WD ok (32) พอดี 2 หน้า 
- ELP-3183-DB ---WD ok เกินหน้า 3 (39)
- ELP-2544-DB ---Seagate
- ELP-2545-DB ---Seagate
- ELP-3179-DB ---WD
- ELP-3158-DB ---WD
- ELP-2717-DB ---WD
- ELP-2614-DB ---WD
-----------------------------------
Ftir , dhs ,gcms , hpa, ic ทั้ง wd seagate ค่ะ แต่ตัว hpa ตำแหน่งที่รูปจะอยู่มันจะไม่เหมือนเพื่อน
-----------------------------------
Seagate_FTIR.ascx(546) > ELP-0812-FB
Seagate_FTIR_Adhesive.ascx(545) > ELP-0662-FB
Seagate_FTIR_Packing.ascx(638) > ELP-2947-FB
Seagate_FTIR_Damper.ascx(640) > ELP-2987-FB




update template_seagate_hpa_coverpage set A = replace(A,'μ','u')
update template_seagate_lpc_coverpage set LiquidParticleCount = replace(LiquidParticleCount,'μ','u');



#######-----
ALTER TABLE `alsi`.`tb_m_specification` 
ADD COLUMN `status` VARCHAR(1) NULL DEFAULT 'A' AFTER `BZ`;


CREATE TABLE `template_img` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sample_id` int(11) DEFAULT NULL,
  `seq` int(11) DEFAULT '0',
  `img_path` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=402 DEFAULT CHARSET=utf8;


*/