using ALS.ALSI.Biz.DataAccess;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ALS.ALSI.Biz
{
    public class FreeTemplateUtil
    {
        private static String[] arrExcelColIndex = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

        public static char DELIMITER = '#';
        public static char DELIMITER_SEMI_COLON = ':';
        public static char DELIMITER_EXCLAMATION_MARK = '!';
        public static char DELIMITER_PIPE = '|';


        private FileInfo fs;
        public FreeTemplateUtil(FileInfo _fs)
        {
            this.fs = _fs;
        }

        public Hashtable getConfigValue()
        {
            Hashtable hash = new Hashtable();


            using (var package = new ExcelPackage(this.fs))
            {
                ExcelWorksheet sConfig = package.Workbook.Worksheets["Config"];
                var configItem = sConfig.Cells["A2:A400"];
                foreach (var item in configItem)
                {
                    String currentVal = (item.Value == null) ? String.Empty : item.Value.ToString();
                    int pos = Convert.ToInt16(Regex.Replace(item.Address, @"[^\d]", ""));
                    String C = sConfig.Cells["C" + pos].Text.ToLower();
                    String B = sConfig.Cells["B" + pos].Text.ToLower();
                    if (!B.Equals(""))
                    {
                        hash[currentVal] = String.Format("{0}|{1}", B, C);
                    }
                    Console.WriteLine();
                }
            }
            return hash;

        }

        public List<tb_m_specification> getSpecification()
        {
            List<tb_m_specification> specificatons = new List<tb_m_specification>();
            Hashtable configs = this.getConfigValue();
            using (var package = new ExcelPackage(this.fs))
            {
                ExcelWorksheet wSpecification = package.Workbook.Worksheets["Specification"];
                String ranges = configs["specification.range"].ToString().Split('|')[1].ToUpper();
                var specificatoinList = wSpecification.Cells[ranges];
                foreach (var _specification in specificatoinList)
                {
                    int ID = Convert.ToInt16(Regex.Replace(_specification.Address, @"[^\d]", ""));
                    String currentVal = (_specification.Value == null) ? String.Empty : _specification.Value.ToString();
                    tb_m_specification s = new tb_m_specification()
                    {
                        ID = ID,
                        A = currentVal
                    };
                    specificatons.Add(s);
                }

            }
            return specificatons;

        }


        public static String GetCellValue(ISheet isheet, String _val)
        {
            int row = GetRowIndex(_val);
            int col = GetColIndex(_val);
            return GetCellValue(isheet.GetRow(row).GetCell(col));
        }

        public static int GetColIndex(String findColName)
        {
            return Array.IndexOf(arrExcelColIndex, Regex.Replace(findColName.ToUpper(), @"[^A-Z]+", String.Empty));
        }
        public static int GetRowIndex(String findColName)
        {
            return Convert.ToInt16(Regex.Replace(findColName.ToUpper(), @"[^\d]", "")) - 1;
        }
        public static String GetColName(int colIndex)
        {
            return arrExcelColIndex[colIndex];
        }
        private static String GetCellValue(ICell _cell)
        {

            String returnValue = String.Empty;

            //if(_cell.CachedFormulaResultType == CellType.Unknown)
            //{
            //    Console.WriteLine();
            //}


            //if (_cell.CachedFormulaResultType != CellType.Error)
            //{
            if (_cell == null) return "";
            if (_cell != null)
            {
                //ErrorIndex = String.Format("Row({0}),Column({1})  Error Value = {2}", _cell.RowIndex + 1, _cell.ColumnIndex + 1, returnValue);

                switch (_cell.CellType)
                {
                    case CellType.Blank:
                        returnValue = "";
                        break;
                    case CellType.Boolean:
                        break;
                    case CellType.Error:
                        break;
                    case CellType.Formula:
                        //DataFormatter df = new DataFormatter();
                        //String asItLooksInExcel = df.FormatCellValue(_cell);
                        if (_cell.CachedFormulaResultType == CellType.Error)
                        {
                            returnValue = "";
                        }
                        else if (_cell.CachedFormulaResultType == CellType.Numeric)
                        {
                            returnValue = _cell.NumericCellValue.ToString();//.CellFormula.ToString();
                        }
                        else
                        {
                            returnValue = _cell.StringCellValue;//.CellFormula.ToString();
                        }

                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(_cell))
                        {
                            returnValue = _cell.DateCellValue.ToString();
                        }
                        else
                        {
                            returnValue = _cell.NumericCellValue.ToString();
                        }
                        break;
                    case CellType.String:
                        returnValue = _cell.StringCellValue.ToString();
                        break;
                    case CellType.Unknown:
                        break;
                }

            }
            //}
            return returnValue;//_cell.ToString();// returnValue.Length>255 ? returnValue.Substring(0,255):returnValue;
        }


        public static Hashtable GetGCMSMapping()
        {

            /*------------------------------
            1 |Workingpg - Motor Oil '!$D$33
            2 |Workingpg - Motor Oil '!$D$35
            3 |Workingpg - Extractable'!$D$54
            4 |
            5 |Workingpg - Extractable'!$B$54
            6 |Workingpg - Extractable'!$C$54
            7 |Workingpg - Extractable'!D35
            8 |
            9 |Workingpg - Extractable'!$B$35
            10|Workingpg - Extractable'!$C$35
            11|Workingpg - Extractable'!D35
            12|Workingpg - Extractable'!$B$35
            13|Workingpg - Extractable'!$C$35
            14|
            15|
            16|
            17|
            18|
            19|
            20|
            -------------------------------*/
            //1 |-
            //2 |-
            //3 |-
            //4 |-
            //5 |-
            //6 |-
            //7 |-
            //8 |-
            //9 |-
            //10|-
            //11|Repeated Hydrocarbon(C20 - C40 Alkanes)
            //12|Total Organic Compound(TOC)
            //13|Compounds with RT ≤ DOP
            //14|Compounds with RT > DOP
            //15|Compound with m / z 155, 271, 425
            //16|Compound with m / z 283, 311 & Compound with m/ z 311
            //17|Compound with m / z 138
            //18|  -
            //19|  -
            //20|  -

            Hashtable ht = new Hashtable();
            ht.Add("1", "Workingpg - Motor Oil!D33");
            ht.Add("2", "Workingpg - Motor Oil!D35");
            ht.Add("3", "Workingpg - Extractable!D54");
            ht.Add("4", "");
            ht.Add("5", "Workingpg - Extractable!B54");
            ht.Add("6", "Workingpg - Extractable!C54");
            ht.Add("7", "Workingpg - Extractable!D35");
            ht.Add("8", "");
            ht.Add("9", "Workingpg - Extractable!B35");
            ht.Add("10", "Workingpg - Extractable!C35");
            ht.Add("11", "Workingpg - Extractable!D35");
            ht.Add("12", "Workingpg - Extractable!B35");
            ht.Add("13", "Workingpg - Extractable!C35");
            ht.Add("14", "Workingpg - Extractable!C35");
            ht.Add("15", "Workingpg - Extractable!B33");
            ht.Add("16", "");
            ht.Add("17", "");
            ht.Add("18", "");
            ht.Add("19", "");
            ht.Add("20", "");
            return ht;
        }
    }



}
