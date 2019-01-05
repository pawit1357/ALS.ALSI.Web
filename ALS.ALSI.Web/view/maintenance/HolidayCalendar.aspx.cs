using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
namespace ALS.ALSI.Web.view.template
{
    public partial class HolidayCalendar : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Template));

        #region "Property"
        public users_login UserLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        private void initialPage()
        {
           
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "Message");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchTemplate prvPage = Page.PreviousPage as SearchTemplate;
            this.PreviousPath = Constants.LINK_SEARCH_HILIDAY_CALENDAR;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            String _pathSourceFile = String.Format(Configurations.PATH_TMP, FileUpload1.FileName);
            String _phisicalPath = String.Format(Configurations.PATH_TMP, String.Empty);
            String _savefilePath = String.Format(Configurations.PATH_TMP, FileUpload1.FileName);
            //::PROCESS UPLOAD
        
            if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).ToLower().Equals(".xls")))
            {
                if (!Directory.Exists(_phisicalPath))
                {
                    Directory.CreateDirectory(_phisicalPath);
                }
                hPathSourceFile.Value = _pathSourceFile;
                FileUpload1.SaveAs(_savefilePath);
                ProcessUpload(_savefilePath);
            }
            else
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ Excel(*.xls) </div>";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        private void ProcessUpload(String filePath)
        {
            Boolean bUploadSuccess = false;
            try
            {

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook wd = new HSSFWorkbook(fs);
   
                    ISheet isComponent = wd.GetSheet("Sheet1");
                    if (isComponent != null)
                    {
                        List<holiday_calendar> hcs = new List<holiday_calendar>();

                        for (int row = 1; row <= isComponent.LastRowNum; row++)
                        {
                            if (isComponent.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                if (!CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(0)).Equals(""))
                                {
                                    //String[] date = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(0)).Split(' ')[0].Split('/');
                                    //int year = Convert.ToInt16(date[2]);
                                    //int day = Convert.ToInt16(date[1]);
                                    //int month = Convert.ToInt16(date[0]);
                                    DateTime dh = isComponent.GetRow(row).GetCell(0).DateCellValue;
                                    holiday_calendar tmp = new holiday_calendar
                                    {
                                        DATE_HOLIDAYS = dh,
                                        YEAR_HOLIDAYS = dh.Year.ToString(),
                                        DESCRIPTION_SUMMARY = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(1))
                                    };
                                    String sql = "insert into holiday_calendar(date_holidays,year_holidays,description_summary) values('{0}','{1}','{2}');";

                                    Boolean bResult = MaintenanceBiz.ExecuteCommand(String.Format(sql,tmp.DATE_HOLIDAYS.ToString("yyyy-MM-dd"), tmp.YEAR_HOLIDAYS,tmp.DESCRIPTION_SUMMARY));
                                    hcs.Add(tmp);
                                }
                            }
                        }
                        //Delete
                        //holiday_calendar.deleteByYear(Convert.ToInt16(hcs[0].YEAR_HOLIDAYS));

                        //holiday_calendar.InsertList(hcs);
                    }
                }
                bUploadSuccess = true;
            }
            catch (Exception ex)
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>" + ex.InnerException + "</div>";

                Console.WriteLine(ex.Message);
            }
            if (bUploadSuccess)
            {
                //Commit
                //GeneralManager.Commit();

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);

            }
            else
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>Upload Fail.</div>";

            }
        }

    }
}



