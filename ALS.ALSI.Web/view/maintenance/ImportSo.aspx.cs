using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class ImportSo : System.Web.UI.Page
    {

        public List<CSo> searchResult
        {
            get { return (List<CSo>)Session[GetType().Name + "MaintenanceAccount"]; }
            set { Session[GetType().Name + "MaintenanceAccount"] = value; }
        }

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
        private void initialPage()
        {
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "Message");
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                searchResult = new List<CSo>();
                pSo.Visible = false;
            }
        }

        #region "BTN"
        protected void btnSaveSo_Click(object sender, EventArgs e)
        {
            int total = 0;
            int success = 0;
            foreach(CSo cso in searchResult)
            {
                String[] vals = cso.ReportNo.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                for (int i = 0; i < vals.Length; i++)
                {
                    Double amt = cso._UnitPrice[i];
                    String[] ReportNos = vals[i].Split(new[] { "," }, StringSplitOptions.None);
                    foreach (String job_number in ReportNos)
                    {
                        job_sample js = job_sample.SelectByJobNumber(job_number);
                        if (js != null)
                        {
                            js.sample_invoice_amount = amt;
                            js.sample_invoice_complete_date = DateTime.Now;
                            js.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE);
                            success++;
                        }
                        total++;
                    }
                }
            }
            GeneralManager.Commit();
            Message = "<div class=\"alert alert-success\"><strong></strong>Job Number ทั้งหมด "+total+" นำเข้าได้ "+success+" นำเข้าไม่ได้ "+(total-success)+"</div>";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(Constants.LINK_SEARCH_JOB_REQUEST);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            String _phisicalPath = String.Format(Configurations.PATH_TMP, String.Empty);
            String _savefilePath = String.Format(Configurations.PATH_TMP, FileUpload1.FileName);
            //::PROCESS UPLOAD

            if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).ToLower().Equals(".txt")))
            {
                if (!Directory.Exists(_phisicalPath))
                {
                    Directory.CreateDirectory(_phisicalPath);
                }
                FileUpload1.SaveAs(_savefilePath);
                ProcessUpload(_savefilePath);
            }
            else
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ Text(*.txt) </div>";
            }
        }
        #endregion

        #region "GRD"
        private void bindingData()
        {
            gvJob.DataSource = this.searchResult;
            gvJob.DataBind();
        }
        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
            }
        }

        protected void gvJob_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            gvJob.EditIndex = e.NewEditIndex;
            bindingData();
        }
        protected void gvJob_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            ////Finding the controls from Gridview for the row which is going to update  
            string SO = gvJob.DataKeys[e.RowIndex].Values[0].ToString();
            TextBox txtUnitPrice = gvJob.Rows[e.RowIndex].FindControl("txtUnitPrice") as TextBox;
            TextBox txtReportNo = gvJob.Rows[e.RowIndex].FindControl("txtReportNo") as TextBox;
            CSo _updateCso = this.searchResult.Find(x => x.SO.Equals(SO));
            if (_updateCso != null)
            {
                List<Double> _UnitPrice = new List<Double>();
                String[] UnitPrice = txtUnitPrice.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String up in UnitPrice)
                {
                    _UnitPrice.Add(Convert.ToDouble(up));
                }
                List<String> _ReportNo = new List<String>();
                String[] ReportNo = txtReportNo.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String rn in ReportNo)
                {
                    _ReportNo.Add(rn);
                }

                _updateCso._UnitPrice = _UnitPrice;
                _updateCso._ReportNo = _ReportNo;
            }
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            gvJob.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            bindingData();
        }
        protected void gvJob_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            gvJob.EditIndex = -1;
            bindingData();
        }


        #endregion


        private void ProcessUpload(String filePath)
        {
            Boolean bUploadSuccess = false;
            String line;
            StringBuilder sb = new StringBuilder();
            try
            {

                StreamReader sr = new StreamReader(filePath);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                CSo cso = null;
                int index = 0;
                string poNo = "";
                while (line != null)
                {
                    //write the lie to console window
                    if (line.StartsWith("  SO"))
                    {
                        cso = new CSo { SO = line.Substring(0, 11).Trim(), _Qty = new List<double>(), _UnitPrice = new List<double>(), _ReportNo = new List<string>() };
                        if (index > 0)
                        {
                            searchResult.Add(cso);
                        }
                    }
                    else if (line.Contains("PO NO."))
                    {
                        poNo = line.Replace("PO NO.", "").Trim();
                    }
                    else if (line.Contains("SAMPLE"))
                    {
                        Double qty = Convert.ToDouble(Regex.Replace(line.Substring(50, 15), "[A-Za-z ]", ""));
                        Double unitPrice = Convert.ToDouble(Regex.Replace(line.Substring(65, 15), "[A-Za-z ]", "").Replace(",", "").Trim());
                        cso._Qty.Add(qty);
                        cso._UnitPrice.Add(unitPrice);
                    }
                    else if (line.Contains("Report no."))
                    {
                        cso._ReportNo.Add(line.Replace("Report no.", "").Trim());
                        //cso._ReportNo.Add(poNo + ":" + line.Replace("Report no.", "").Trim());
                    }
                    else if (line.Contains("ELP-") || line.Contains("ELS-") || line.Contains("ELN-") || line.Contains("FA-") || line.Contains("ELWA-") || line.Contains("GRP-") || line.Contains("TRB-"))
                    {
                        cso._ReportNo.Add(line.Replace("Report no.", "").Trim());
                        //cso._ReportNo.Add(poNo + ":" + line.Replace("Report no.", "").Trim());
                    }

                    //Read the next line
                    line = sr.ReadLine();
                    index++;
                }

                //close the file
                sr.Close();
                bUploadSuccess = true;
            }
            catch (Exception ex)
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>" + ex.InnerException + "</div>";

                Console.WriteLine(ex.Message);
            }
            if (bUploadSuccess)
            {
                pSo.Visible = true;
                bindingData();
                //Commit
                Message = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูลเรียบแล้วทั้งหมด " + this.searchResult.Count + " รายการ</div>";
            }
            else
            {
                pSo.Visible = false;
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>เกิดความผิดพลาดในการโหลดข้อมูล SO กรุณาตรวจสอบไฟล์</div>";
            }
        }

    }
}



