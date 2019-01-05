using System;
using System.Configuration;

namespace ALS.ALSI.Biz.Constant
{
    public class Configurations
    {

        public static String CompanyName
        {
            get { return ConfigurationManager.AppSettings["COMPANY_NAME"]; }
        }

        public static String AppTitle
        {
            get { return ConfigurationManager.AppSettings["APP_TITLE"]; }
        }

        //public static String PATH_SOURCE_DRIVE
        //{
        //    get { return ConfigurationManager.AppSettings["PATH_SOURCE_DRIVE"]; }
        //}
        public static String PATH_TEMPLATE
        {
            get { return ConfigurationManager.AppSettings["PATH_TEMPLATE"]; }
        }
        public static String PATH_TMP
        {
            get { return ConfigurationManager.AppSettings["PATH_TMP"]; }
        }
        public static String PATH_SOURCE
        {
            get { return ConfigurationManager.AppSettings["PATH_SOURCE"]; }
        }
        public static String PATH_URL
        {
            get { return ConfigurationManager.AppSettings["PATH_URL"]; }
        }
        public static String HOST
        {
            get { return ConfigurationManager.AppSettings["HOST"]; }
        }

        public static String MySQLCon
        {
            get { return ConfigurationManager.ConnectionStrings["ALSIEntities"].ConnectionString; }
        }
        //public static String PATH_DRIVE
        //{
        //    get { return ConfigurationManager.AppSettings["PATH_DRIVE"]; }
        //}
        #region "REPORT"
        public static String ReportPath
        {
            get { return ConfigurationManager.AppSettings["ReportPath"]; }
        }
        public static String PathSeagateDhs
        {
            get { return ConfigurationManager.AppSettings["PathSeagateDhs"]; }
        }
        public static String PathSeagateFtir
        {
            get { return ConfigurationManager.AppSettings["PathSeagateFtir"]; }
        }
        public static String PathSeagateGcms
        {
            get { return ConfigurationManager.AppSettings["PathSeagateGcms"]; }
        }
        public static String PathSeagateHpa
        {
            get { return ConfigurationManager.AppSettings["PathSeagateHpa"]; }
        }
        public static String PathSeagateIc
        {
            get { return ConfigurationManager.AppSettings["PathSeagateIc"]; }
        }
        public static String PathSeagateLpc
        {
            get { return ConfigurationManager.AppSettings["PathSeagateLpc"]; }
        }

        public static String PathWdDhs
        {
            get { return ConfigurationManager.AppSettings["PathWdDhs"]; }
        }
        public static String PathWdFtir
        {
            get { return ConfigurationManager.AppSettings["PathWdFtir"]; }
        }
        public static String PathWdGcms
        {
            get { return ConfigurationManager.AppSettings["PathWdGcms"]; }
        }
        public static String PathWdHpafor1
        {
            get { return ConfigurationManager.AppSettings["PathWdHpafor1"]; }
        }
        public static String PathWdHpafor3
        {
            get { return ConfigurationManager.AppSettings["PathWdHpafor3"]; }
        }
        public static String PathWdIc
        {
            get { return ConfigurationManager.AppSettings["PathWdIc"]; }
        }
        public static String PathWdLpc
        {
            get { return ConfigurationManager.AppSettings["PathWdLpc"]; }
        }
        public static String PathWdMesa
        {
            get { return ConfigurationManager.AppSettings["PathWdMesa"]; }
        }
        public static String PathWdMesaInkribbon
        {
            get { return ConfigurationManager.AppSettings["PathWdMesaInkribbon"]; }
        }
        #endregion

        #region "Power-BI"
        public static String ClientID
        {
            get { return ConfigurationManager.AppSettings["powerbi.applicationid"]; }
        }
        #endregion

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
    }
}
