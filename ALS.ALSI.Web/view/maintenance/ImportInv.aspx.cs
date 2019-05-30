using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class ImportInv : System.Web.UI.Page
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

        protected String MessageINv
        {
            get { return (String)Session[GetType().Name + "MessageINv"]; }
            set { Session[GetType().Name + "MessageINv"] = value; }
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
                MessageINv = "";
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
                else
                {
                    File.Delete(_savefilePath);
                    FileUpload1.SaveAs(_savefilePath);
                }
                ProcessUpload(_savefilePath);
            }
            else
            {
                MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ Text(*.txt) </div>";
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
            searchResult = so.SelectAllInv(ddlStatus.SelectedValue);
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
            try
            {
                List<job_sample_group_so> groupInv = new List<job_sample_group_so>();
                int index = 0;
                string[] lines = System.IO.File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line.Equals("\f"))
                    {
                        if (line.IndexOf("SO") != -1)
                        {
                            string so = line.Substring(line.IndexOf("SO"), 9);
                            //Console.WriteLine("::"+so);
                            job_sample_group_so tmp = this.searchResult.Where(x => x.so.Equals(so)).FirstOrDefault();
                            if (tmp != null)
                            {
                                string inv_no = line.Substring(line.IndexOf("IV"), 9);
                                string inv_date = line.Substring(15, 8);
                                string inv_duedate = line.Substring(128, 9);
                                double inv_amt = double.Parse(line.Substring(85, 15));

                                tmp.inv_no = inv_no;
                                tmp.inv_date = parseDateFromStr(inv_date);
                                tmp.inv_duedate = parseDateFromStr(inv_duedate);
                                tmp.inv_amt = inv_amt;
                                tmp.inv_status = "I";
                                tmp.update_date = DateTime.Now;
                                groupInv.Add(tmp);
                            }
                            index++;
                        }
                    }
                }
                bUploadSuccess = true;

                if (bUploadSuccess)
                {
                    pSo.Visible = true;
                    //insert to db
                    foreach (job_sample_group_so so in groupInv)
                    {
                        so.Update();
                    }
                    GeneralManager.Commit();

                    bindingData();
                    //Commit
                    MessageINv = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูลเรียบแล้วทั้งหมด " + groupInv.Count() + " รายการ</div>";
                }
                else
                {
                    pSo.Visible = false;
                    MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>เกิดความผิดพลาดในการโหลดข้อมูล SO กรุณาตรวจสอบไฟล์</div>";
                }
            }
            catch (Exception ex)
            {
                MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>" + ex.InnerException + "</div>";

                Console.WriteLine(ex.Message);
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
                if (soIds != null)
                {
                    soGroup = this.searchResult.Where(x => soIds.Contains(x.id)).ToList();
                }
                soGroup = soGroup.Where(x => x.inv_status.Equals("I") && x.inv_no != null).ToList();

                int total = soGroup.Count();
                int fail = 0;
                foreach (job_sample_group_so _updateCso in soGroup)
                {

                    if (_updateCso.report_no == null)
                    {
                        fail++;
                        sbJobFail.Append("ไม่พบ job สำหรับอัพเดท invoice,");

                        continue;
                    }
                    String[] UnitPrice = _updateCso.unit_price.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    String[] vals = _updateCso.report_no.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                    //if (UnitPrice.Length == vals.Length)
                    //{
                    Boolean isComplete = true;

                    for (int i = 0; i < UnitPrice.Length; i++)
                    {
                        if (!UnitPrice[i].Equals(""))
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
                                        
                                        js.sample_invoice = _updateCso.inv_no;
                                        js.sample_invoice_complete_date = _updateCso.inv_date;
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
                    _updateCso.inv_status = !isComplete ? "I" : "C";
                    _updateCso.Update();
                    if (!isComplete)
                    {
                        fail++;
                    }
                    //}
                    //else
                    //{
                    //    fail++;
                    //    sbJobFail.Append(_updateCso.so + ",");
                    //}
                }

                GeneralManager.Commit();

                String errList = (sbJobFail.Length == 0) ? "" : "\nรายการ Invoice ที่โหลดไม่สำเร็จ คือ " + String.Join(",", sbJobFail.ToString().Split(','));
                MessageINv = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูล Job\n ทั้งหมด" + total + " รายการ\n สำเร็จ " + (total - fail) + " รายการ " + ((errList.Length > 0) ? errList.Substring(0, errList.Length - 1) : errList) + " </div>";
                bindingData();
            }
        }


        public DateTime parseDateFromStr(string date)
        {
            // : 03/04/19
            if(date.Length == 8)
            {
                int day = Convert.ToInt16(date.Split('/')[0]);
                int month = Convert.ToInt16(date.Split('/')[1]);
                int year = Convert.ToInt16("20" + date.Split('/')[2]);
                return new DateTime(year, month, day);
            }
            else
            {
                return DateTime.Now;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }
    }
}