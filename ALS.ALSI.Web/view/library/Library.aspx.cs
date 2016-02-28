using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace ALS.ALSI.Web.view.library
{
    public partial class Library : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Library));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }
        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }
        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int PKID
        {
            get { return (int)Session[GetType().Name + "PKID"]; }
            set { Session[GetType().Name + "PKID"] = value; }
        }

        public tb_m_dhs_library obj
        {
            get
            {
                tb_m_dhs_library tmp = new tb_m_dhs_library();
                return tmp;
            }
        }

        private void initialPage()
        {
            lbCommandName.Text = CommandName.ToString();

            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    //txtPathSourceFile.ReadOnly = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();
                    //txtPathSourceFile.ReadOnly = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case CommandNameEnum.View:
                    fillinScreen();
                    //txtPathSourceFile.ReadOnly = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    break;
            }
        }

        private void fillinScreen()
        {
            //tb_m_dhs_library lib = new tb_m_dhs_library().SelectByID(this.PKID);
        }


        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
            Session.Remove(GetType().Name + "Message");

            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchLibrary prvPage = Page.PreviousPage as SearchLibrary;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_LIBRARY;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    break;
                case CommandNameEnum.Edit:
                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".xls")))
                    {
                        String _pathSourceFile = String.Format(Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                        String _phisicalPath = String.Format(System.AppDomain.CurrentDomain.BaseDirectory + Configurations.PATH_TEMPLATE, String.Empty);
                        String _savefilePath = String.Format(System.AppDomain.CurrentDomain.BaseDirectory + Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                        if (!Directory.Exists(_phisicalPath))
                        {
                            Directory.CreateDirectory(_phisicalPath);
                        }
                        hPathSourceFile.Value = _pathSourceFile;
                        FileUpload1.SaveAs(_savefilePath);
                        //::PROCESS UPLOAD
                        processUpload(_savefilePath);
                    }
                    break;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        private void processUpload(String filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Boolean bUploadSuccess = false;

                #region "UPLOAD EXCEL"
                HSSFWorkbook wd = new HSSFWorkbook(fs);
                ISheet sheet = wd.GetSheetAt(0);
                //int id = 1;
                if (sheet != null)
                {
                    tb_m_dhs_library library = new tb_m_dhs_library();
                    List<tb_m_dhs_library> lists = new List<tb_m_dhs_library>();
                    //Delete before new insert
                    library.DeleteBySpecificationID(Convert.ToInt32(this.PKID));
                    for (int row = 1; row <= sheet.LastRowNum; row++)
                    {
                        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                        {
                            tb_m_dhs_library lib = new tb_m_dhs_library();
                            lib.id = row;
                            lib.specification_id = this.PKID;
                            lib.cas = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(0));
                            lib.libraryID = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(2));
                            lib.classification = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(1));
                            lists.Add(lib);
                        }
                    }

                    library.InsertList(lists);
                    bUploadSuccess = true;
                    btnSave.Enabled = false;
                    //Commit
                    GeneralManager.Commit();
                }
                #endregion

                if (bUploadSuccess)
                {
                    Message = "<div class=\"alert alert-uccess\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Upload Success.</span></div>";
                }
                else {
                    Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Upload Fail.</span></div>";
                }
            }
        }
    }
}