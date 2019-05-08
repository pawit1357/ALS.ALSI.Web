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
using System.Linq;

namespace ALS.ALSI.Web.view.template
{
    public partial class ImportSo : System.Web.UI.Page
    {

        public IEnumerable<job_sample_group_so> searchResult
        {
            get { return (IEnumerable<job_sample_group_so>)Session[GetType().Name + "job_sample_group_so"]; }
            set { Session[GetType().Name + "job_sample_group_so"] = value; }
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
                bindingData();
                Message = "";
            }
        }

        #region "BTN"
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
                if (!File.Exists(_savefilePath))
                {
                    FileUpload1.SaveAs(_savefilePath);
                }
                ProcessUpload(_savefilePath);
            }
            else
            {
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ Text(*.txt) </div>";
            }
        }
        protected void btnBatchLoad_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>();
            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;

                if (chk != null && chk.Checked)
                {
                    HiddenField hf = row.Cells[0].FindControl("hid") as HiddenField;
                    ids.Add(Convert.ToInt32(hf.Value));
                }
            }
            batchUpload(ids);
        }
        #endregion

        #region "GRD"
        private void bindingData()
        {
            job_sample_group_so so = new job_sample_group_so();
            searchResult = so.SelectAll();
            gvJob.DataSource = searchResult;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton btnLoad = (LinkButton)e.Row.FindControl("btnLoad");
                //LinkButton btn_Edit = (LinkButton)e.Row.FindControl("btn_Edit");

                //Literal status = (Literal)e.Row.FindControl("ltStatus");
                //if (btnLoad != null)
                //{
                //    btnLoad.Visible = status.Text.Equals("I");
                //}
                //if (btn_Edit != null)
                //{
                //    btn_Edit.Visible = status.Text.Equals("I");
                //}
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
            int id = Convert.ToInt32(gvJob.DataKeys[e.RowIndex].Values[1].ToString());
            TextBox txtUnitPrice = gvJob.Rows[e.RowIndex].FindControl("txtUnitPrice") as TextBox;
            TextBox txtReportNo = gvJob.Rows[e.RowIndex].FindControl("txtReportNo") as TextBox;

            job_sample_group_so _updateCso = new job_sample_group_so().SelectByID(id);
            if (_updateCso != null)
            {
                List<Double> _UnitPrice = new List<Double>();
                String[] UnitPrice = txtUnitPrice.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String up in UnitPrice)
                {
                    if (CustomUtils.isNumber(up))
                    {
                        _UnitPrice.Add(Convert.ToDouble(up));
                    }
                }
                List<String> _ReportNo = new List<String>();
                String[] ReportNo = txtReportNo.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String rn in ReportNo)
                {
                    if (!rn.Equals(""))
                    {
                        _ReportNo.Add(rn);
                    }
                }

                _updateCso.unit_price = String.Join(Environment.NewLine, _UnitPrice);
                _updateCso.report_no = String.Join(Environment.NewLine, _ReportNo);
                _updateCso.Update();
                GeneralManager.Commit();
            }
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            gvJob.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            bindingData();
        }

        protected void gvJob_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            switch (cmd)
            {
                case CommandNameEnum.View:
                    int id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    List<int> ids = new List<int>();
                    ids.Add(id);
                    batchUpload(ids);
                    break;
            }
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
            List<job_sample_group_so> listGroupSo = new List<job_sample_group_so>();

            //StringBuilder sb = new StringBuilder();
            try
            {

                job_sample_group_so cso = null;
                int index = 0;
                string so = "";
                string[] lines = System.IO.File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line.Equals("\f"))
                    {
                        if (line.StartsWith("  SO") || line.Substring(10, 2).Equals("SO"))
                        {
                            if (index == 0)
                            {
                                so = line.Substring(10, 10).Trim();
                                cso = new job_sample_group_so { so = so };
                            }
                            if (index > 0)
                            {
                                cso.status = "I";
                                cso.create_date = DateTime.Now;
                                if (!job_sample_group_so.FindBySO(so))
                                {
                                    listGroupSo.Add(cso);
                                }
                            }
                            if (!so.Equals(line.Substring(10, 10).Trim()))
                            {
                                so = line.Substring(10, 10).Trim();
                                cso = new job_sample_group_so { so = so };
                            }
                            index++;
                        }
                        else if (line.Contains("SAMPLE"))
                        {
                            Double unitPrice = Convert.ToDouble(Regex.Replace(line.Substring(65, 15), "[A-Za-z ]", "").Replace(",", "").Trim());
                            cso.unit_price += (unitPrice) + System.Environment.NewLine;
                        }
                        else if (line.Contains("Report no."))
                        {
                            cso.report_no += (line.Replace("Report no.", "").Trim()) + System.Environment.NewLine;
                        }
                        else if (line.Contains("ELP-") || line.Contains("ELS-") || line.Contains("ELN-") || line.Contains("FA-") || line.Contains("ELWA-") || line.Contains("GRP-") || line.Contains("TRB-"))
                        {
                            cso.report_no += (line.Replace("Report no.", "").Trim()) + System.Environment.NewLine;
                        }
                    }
                }
                //add last item
                cso.status = "I";
                cso.create_date = DateTime.Now;
                if (!job_sample_group_so.FindBySO(so))
                {
                    listGroupSo.Add(cso);
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
                pSo.Visible = true;
                //insert to db
                foreach (job_sample_group_so so in listGroupSo)
                {
                    so.Insert();
                }
                GeneralManager.Commit();

                bindingData();
                //Commit
                Message = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูลเรียบแล้วทั้งหมด " + listGroupSo.Count + " รายการ</div>";
            }
            else
            {
                pSo.Visible = false;
                Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>เกิดความผิดพลาดในการโหลดข้อมูล SO กรุณาตรวจสอบไฟล์</div>";
            }
        }


        private string getNum(string num)
        {
            string number = "";
            switch (num.Length)
            {
                case 1:
                    number = string.Format("000{0}", num);
                    break;
                case 2:
                    number = string.Format("00{0}", num);
                    break;
                case 3:
                    number = string.Format("0{0}", num);
                    break;
                case 4:
                    number = string.Format("{0}", num);
                    break;
            }
            return number;
        }


        public void batchUpload(List<int> soIds = null)
        {
            if (searchResult.ToDataTable().Rows.Count > 0)
            {
                StringBuilder sbJobFail = new StringBuilder();


                IEnumerable<job_sample_group_so> soGroup = this.searchResult;
                if (soIds !=null)
                {
                    soGroup = this.searchResult.Where(x => soIds.Contains(x.id)).ToList();
                }
                soGroup = soGroup.Where(x => x.status.Equals("I") && x.report_no != null).ToList();
                int total = soGroup.Count();
                int fail = 0;
                foreach (job_sample_group_so _updateCso in soGroup)
                {
                    
                    //if (_updateCso != null && _updateCso.status.Equals("I") && _updateCso.report_no != null)
                    //{
                        String[] UnitPrice = _updateCso.unit_price.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                        String[] vals = _updateCso.report_no.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                        if (UnitPrice.Length == vals.Length)
                        {
                            Boolean isComplete = true;

                            for (int i = 0; i < vals.Length; i++)
                            {
                                if (!vals[i].Equals(""))
                                {
                                    Double amt = Convert.ToDouble(UnitPrice[i]);
                                    String[] ReportNos = vals[i].Split(new[] { "," }, StringSplitOptions.None);
                                    foreach (String job_number in ReportNos)
                                    {
                                        #region "FIND JOB NUMBER"
                                        List<string> jobNumbers = new List<string>();
                                        string[] _val = job_number.Split('-');
                                        if (_val.Length == 4)
                                        {
                                            int startJob = Convert.ToInt32(_val[1]);
                                            int endJob = Convert.ToInt32(_val[2]);
                                            for (int idx = startJob; idx <= endJob; idx++)
                                            {
                                                jobNumbers.Add(string.Format("{0}-{1}-{2}", _val[0], getNum(idx.ToString()), _val[3]));
                                            }
                                        }
                                        else
                                        {
                                            jobNumbers.Add(job_number);
                                        }
                                        #endregion

                                        foreach (var jn in jobNumbers)
                                        {
                                            job_sample js = job_sample.SelectByJobNumber(jn);
                                            if (js != null)
                                            {
                                                js.sample_invoice_amount = amt;
                                                js.sample_invoice_complete_date = DateTime.Now;
                                                js.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE);
                                                js.Update();
                                            }
                                            else
                                            {
                                                sbJobFail.Append(_updateCso.so + ",");
                                                isComplete = false;
                                            }
                                        }
                                    }
                                }
                            }
                            _updateCso.status = !isComplete ? "I" : "C";
                            _updateCso.Update();
                            if (!isComplete)
                            {
                                fail++;
                            }
                        }
                        else
                        {
                            fail++;
                            sbJobFail.Append(_updateCso.so + ",");
                        }
                    //}
                }
                GeneralManager.Commit();

                String errList = (sbJobFail.Length == 0) ? "" : "\nรายการ SO ที่โหลดไม่สำเร็จ คือ " + String.Join(",", sbJobFail.ToString().Split(','));
                Message = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูล Job\n ทั้งหมด" + total + " รายการ\n สำเร็จ " + (total - fail) + " รายการ " + ((errList.Length > 0) ? errList.Substring(0, errList.Length - 1) : errList) + " </div>";
                bindingData();
            }
        }


    }
}