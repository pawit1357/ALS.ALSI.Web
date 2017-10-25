using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;

namespace ALS.ALSI.Utils
{
    public class CustomUtils
    {

        public static String ErrorIndex;

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        public static int GetRandomNumberID()
        {
            return GetRandomNumber(1, 1000); //Convert.ToInt32(DateTime.Now.Ticks);

        }
        public static int GenerateRandom(int min, int max)
        {
            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(seed).Next(min, max);
        }

        public static Boolean isNumber(String _value)
        {
            //int n;
            //return int.TryParse(_value, out n);
            try
            {
                Double.Parse(_value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static byte[] GetBytesFromImage(String imageFile)
        {
           
            //string path = HttpContext.Current.Server.MapPath(imageFile);
            //byte[] photo = File.ReadAllBytes(path);

            var webClient = new WebClient();

            byte[] photo = webClient.DownloadData(imageFile);


            return photo;
        }

        //public static byte[] GetBytesFromPhisicalPath(String imageFile)
        //{
        //    byte[] photo = null;
        //    string path = String.Format("{0}/{1}", Configurations.PATH_DRIVE, imageFile);

        //    photo = File.ReadAllBytes(path);


        //    return photo;
        //}
        //private static Image resizeImage(Image imgToResize, Size size)
        //{
        //    int sourceWidth = imgToResize.Width;
        //    int sourceHeight = imgToResize.Height;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)size.Width / (float)sourceWidth);
        //    nPercentH = ((float)size.Height / (float)sourceHeight);

        //    if (nPercentH < nPercentW)
        //        nPercent = nPercentH;
        //    else
        //        nPercent = nPercentW;

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap b = new Bitmap(destWidth, destHeight);
        //    Graphics g = Graphics.FromImage((Image)b);
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        //    g.Dispose();

        //    return (Image)b;
        //}
        public static String EncodeMD5(String password)
        {


            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }

        public static String GetCellValue(ICell _cell)
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
                ErrorIndex = String.Format("Row({0}),Column({1})  Error Value = {2}", _cell.RowIndex + 1, _cell.ColumnIndex + 1, returnValue);

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
                    if(_cell.CachedFormulaResultType == CellType.Error)
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


        public static Double GetMax(Double _value)
        {
            return (_value > 0) ? _value : 0;
        }

        public static Double GetDefaultZero(String _value)
        {
            return String.IsNullOrEmpty(_value) ? 0 : Convert.ToDouble(_value);
        }

        public static double Average(List<Double> valueList)
        {
            double result = 0.0;
            foreach (double value in valueList)
            {
                result += value;
            }
            return result / valueList.Count;
        }

        public static double StandardDeviation(List<Double> valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        public static double Sum(String[] valueList)
        {
            double result = 0.0;
            foreach (String value in valueList)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    result += isNumber(value) ? Convert.ToDouble(value) : 0;
                }
            }
            return result;
        }

        public static double ValueIfNullToZero(String value)
        {
            double result = 0.0;

            if (!String.IsNullOrEmpty(value))
            {
                result += Convert.ToDouble(value);
            }

            return result;
        }

        public static DateTime converFromDDMMYYYY(String _val)
        {
            //26/01/2015
            String[] data = _val.Split('/');
            if (data.Length == 2)
            {
                return Convert.ToDateTime(String.Format("{0}/{1}/{2}", data[2], DateTime.DaysInMonth(Convert.ToInt32(data[2]), Convert.ToInt32(data[1])), data[0]));
            }
            else
            {
                return Convert.ToDateTime(String.Format("{0}/{1}/{2}", data[2], data[1], data[0]));
            }


        }

        public static String removeSpacialCharacter(String originalStr)
        {
            String returnStr = originalStr;
            String[] spcialChar = { "!","@","#","$","%","^","&","*","(",")","-","+" };
            foreach(String s in spcialChar)
            {
                returnStr = returnStr.Replace(s, "_");
            }
            return returnStr.Trim();
        }



        public static int getUnitByName(String _name)
        {
            int result = 0;
            switch (_name)
            {
                case "ng/part":
                    result = 100;
                    break;
                case "ng/g":
                    result = 1000;
                    break;
                case "ng/cm2":
                    result = 0;
                    break;
                case "Cartridge ng/part":
                    result = 0;
                    break;
            }
            return result;
        }

        public static String getUnitText(int unit)
        {
            String result = String.Empty;

            switch (unit)
            {
                case 1: result = "ug/sq cm";
                    break;
                case 1000: result = "ng/cm2";
                    break;
                default: result = "mg";
                    break;
            }

            return result;
        }

        public static String showOnCoverPageValue(String val,int digit)
        {
            String returnResult = String.Empty;
            switch (val)
            {
                case "":
                case "0":
                case "0.00":
                    returnResult = "";
                    break;
                case " <MDL":
                case "<MDL":
                case "Not Detected":
                    returnResult = val;
                    break;
                default:
                    returnResult = Convert.ToDouble(val).ToString("N"+digit);
                    break;
            }
            return returnResult;

        }


        public static List<DetailSpecComponent> GetComponent(List<tb_m_detail_spec> _data, tb_m_detail_spec _value,List<int> ignoreOrder)
        {
            List<DetailSpecComponent> specs = new List<DetailSpecComponent>();

            List<tb_m_detail_spec> listOrder = new List<tb_m_detail_spec>();
            listOrder.Add(_data[0]);
            List<tb_m_detail_spec> listHeader = new List<tb_m_detail_spec>();
            listHeader.Add(_data[1]);
            List<tb_m_detail_spec> listValue = new List<tb_m_detail_spec>();
            listValue.Add(_value);

            DataTable dtOrder = listOrder.ToDataTable();
            DataTable dtHeader = listHeader.ToDataTable();
            DataTable dtValue = listValue.ToDataTable();

            foreach (DataRow dtRow in dtOrder.Rows)
            {
                foreach (DataColumn dcOrder in dtOrder.Columns)
                {
                    DetailSpecComponent spec = new DetailSpecComponent();
                    if (ExcelColumn.dataIndex.Contains(dcOrder.ToString()))
                    {
                        String order = dtRow[dcOrder].ToString();
                        if (String.IsNullOrEmpty(order)) continue;

                        spec.order = Convert.ToInt16(order);
                        //----
                        foreach (DataRow dtRowHeader in dtHeader.Rows)
                        {
                            foreach (DataColumn dcHeader in dtHeader.Columns)
                            {
                                if (dcOrder.ToString().Equals(dcHeader.ToString()))
                                {
                                    String specName = dtRowHeader[dcHeader].ToString();
                                    if (specName.Equals("Total Silanes (ng/cm2)"))
                                    {
                                        spec.name = specName;
                                    }
                                    else {
                                        spec.name = specName.Substring(0, specName.IndexOf('(') == -1 ? specName.Length : specName.IndexOf('(')).Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        //----
                        //----
                        foreach (DataRow dtRowValue in dtValue.Rows)
                        {
                            foreach (DataColumn dcValue in dtValue.Columns)
                            {
                                if (dcOrder.ToString().Equals(dcValue.ToString()))
                                {
                                    String value = dtRowValue[dcValue].ToString();
                                    spec.value = value;
                                    break;
                                }
                            }
                        }
                        //----
                    }
                    specs.Add(spec);
                }
            }
            return specs.Where(x=> !ignoreOrder.Contains(x.order)).OrderBy(x => x.order).ToList();
        }


    }
}




