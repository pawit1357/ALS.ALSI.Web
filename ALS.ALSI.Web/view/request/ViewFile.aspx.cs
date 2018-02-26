using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;

namespace ALS.ALSI.Web.view.request
{
    public partial class ViewFile : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ChangeJobPo));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "ChangeJobPo"]; }
            set { Session[GetType().Name + "ChangeJobPo"] = value; }
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

        public int JobID
        {
            get { return (int)Session[GetType().Name + "JobID"]; }
            set { Session[GetType().Name + "JobID"] = value; }
        }
        public int SampleID
        {
            get { return (int)Session[GetType().Name + "SampleID"]; }
            set { Session[GetType().Name + "SampleID"] = value; }
        }
        public job_sample jobSample
        {
            get { return (job_sample)Session[GetType().Name + "job_sample"]; }
            set { Session[GetType().Name + "job_sample"] = value; }
        }

        private void initialPage()
        {
            fillinScreen();


            if (!String.IsNullOrEmpty(this.jobSample.path_word))
            {
                hlWord.NavigateUrl = String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word);
                hlWord.Enabled = true;
                hlWord.Text = "Word (ดาวโหลดไฟล์ *.doc|*.docx)";
            }
            else
            {
                hlWord.Enabled = false;
                hlWord.Text = "Word (ไม่พบไฟล์)";

            }

            if (!String.IsNullOrEmpty(this.jobSample.path_pdf))
            {
                hlPdf.NavigateUrl = String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_pdf);
                hlPdf.Text = "Pdf (ดาวโหลดไฟล์ *.pdf)";
                hlPdf.Enabled = true;
            }
            else
            {
                hlPdf.Enabled = false;
                hlPdf.Text = "Pdf (ไม่พบไฟล์)";

            }


            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bindingData();
        }

        private void fillinScreen()
        {
            jobSample = new job_sample().SelectByID(this.SampleID);
            if (jobSample != null)
            {
            }
            else
            {
                btnCancel_Click(null, null);
            }
        }

        private void bindingData()
        {
            job_info jobInfo = new job_info();

            jobInfo.sample_id = this.SampleID;
            searchResult = jobInfo.SearchData();
            gvJob.DataSource = searchResult;
            gvJob.DataBind();

            //Show file.
            try
            {
                job_info jInfo = new job_info();
                jInfo = jInfo.SelectByID(this.jobSample.job_id);
                ///


                String rootPath = Configurations.PATH_SOURCE.Replace("{0}\\{1}\\{2}\\{3}\\{4}", "");
                String correctPath = String.Empty;

                for (int y = DateTime.Now.Year; y > DateTime.Now.Year-2; y--)
                {
                    for(int m = 1; m <= 12; m++)
                    {
                        for(int dd = 1; dd <= DateTime.DaysInMonth(y,m); dd++)
                        {
                            String checkPath = String.Format("{0}{1}\\{2}\\{3}\\{4}", rootPath, y, m.ToString("00"),dd.ToString("00"), this.jobSample.job_number);
                            if (Directory.Exists(checkPath))
                            {
                                correctPath = String.Format(Configurations.PATH_SOURCE, y, m.ToString("00"), dd.ToString("00"), this.jobSample.job_number, String.Empty);
                                break;
                            }
                        }
  
                    }
                }

                List<FileList> listOfFile = new List<FileList>();
                //String year = jInfo.create_date.Value.Year.ToString();
                //String month = jInfo.create_date.Value.Month.ToString("00");
                //String day = jInfo.create_date.Value.Day.ToString("00");
                //String job = this.jobSample.job_number;

                DirectoryInfo d = new DirectoryInfo(correctPath);//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
                string str = "";
                int index = 1;
                List<String> keepExtension = new List<String>();
                keepExtension.Add(".xls");
                keepExtension.Add(".jpg");
                keepExtension.Add(".xlt");
                keepExtension.Add(".csv");

                foreach (FileInfo file in Files)
                {
                    FileList fl = new FileList();
                    fl.order = index;
                    fl.name = Path.GetFileName(file.FullName);
                    fl.url = String.Concat(Configurations.HOST,"/uploads/", file.FullName.Replace(rootPath, "").Replace("\\","/"));
                    //str = str + ", " + file.Name;
                    index++;
                    if (keepExtension.Contains(Path.GetExtension(file.FullName)))
                    {
                        listOfFile.Add(fl);
                    }
                }

                gvFileList.DataSource = listOfFile;
                gvFileList.DataBind();
                lbShowListText.Text = "";

            }
            catch (Exception ex)
            {
                lbShowListText.Text = "-";

            }
        }




        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "JobID");
            Session.Remove(GetType().Name + "objInfo");

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.JobID = (prvPage == null) ? this.JobID : prvPage.JobID;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;

            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }




    }
}