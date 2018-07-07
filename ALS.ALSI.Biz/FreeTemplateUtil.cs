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
                    String columnType = sConfig.Cells["C" + pos].Text.ToLower();
                    String mappingValue = sConfig.Cells["B" + pos].Text.ToLower();

                    hash[currentVal] = String.Format("{0}", mappingValue);

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
                var specificatoinList = wSpecification.Cells[configs["specification.range"].ToString()];
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
    }



}
