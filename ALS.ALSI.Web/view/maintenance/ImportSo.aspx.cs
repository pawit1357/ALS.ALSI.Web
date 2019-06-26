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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class ImportSo : System.Web.UI.Page
    {
        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }
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

        protected String MsgLogs
        {
            get { return (String)Session[GetType().Name + "MsgLogs"]; }
            set { Session[GetType().Name + "MsgLogs"] = value; }
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
                MsgLogs = "";
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            bindingData();
        }
        #endregion

        #region "GRD"
        private void bindingData()
        {
            job_sample_group_so so = new job_sample_group_so();
            searchResult = so.SelectAll(ddlStatus.SelectedValue, txtSoCode.Text);
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
            TextBox txtSoDesc = gvJob.Rows[e.RowIndex].FindControl("txtSoDesc") as TextBox;

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
                String[] ReportNo = txtReportNo.Text.Split(new[] { "|" }, StringSplitOptions.None);
                foreach (String rn in ReportNo)
                {
                    if (!rn.Equals(""))
                    {
                        _ReportNo.Add(rn);
                    }
                }
                List<String> _SoDesc = new List<String>();
                String[] SoDesc = txtSoDesc.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String rn in SoDesc)
                {
                    if (!rn.Equals(""))
                    {
                        _SoDesc.Add(rn);
                    }
                }
                _updateCso.unit_price = String.Join(Environment.NewLine, _UnitPrice);
                _updateCso.report_no = String.Join("|", _ReportNo);
                _updateCso.so_desc = String.Join(Environment.NewLine, _SoDesc);


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


        protected void chkAllSign_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                chkcheck.Checked = cb.Checked;
            }
        }

        #endregion

        private void ProcessUpload(String filePath)
        {
            Boolean bUploadSuccess = false;
            List<job_sample_group_so> listGroupSo = new List<job_sample_group_so>();

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
                                if (cso.report_no == null && cso.report_no == null && cso.report_no == null)
                                {
                                    Console.WriteLine();
                                }
                                else
                                {
                                    cso.unit_price = cso.unit_price.TrimEnd(Environment.NewLine.ToCharArray());
                                    cso.so_desc = cso.so_desc.TrimEnd(Environment.NewLine.ToCharArray());
                                    cso.report_no = cso.report_no.TrimEnd(Environment.NewLine.ToCharArray());
                                }
                                cso.status = "I";
                                cso.create_date = DateTime.Now;
                                listGroupSo.Add(cso);
                            }
                            if (!so.Equals(line.Substring(10, 10).Trim()))
                            {
                                so = line.Substring(10, 10).Trim();
                                cso = new job_sample_group_so { so = so };

                            }
                            index++;
                        }
                        else if (line.Contains("SAMPLE") || line.Contains("Each") || line.Contains("Pack"))
                        {
                            Double _quantity = Convert.ToDouble(Regex.Replace(line.Substring(54, 5), "[A-Za-z ]", "").Replace(",", "").Trim());
                            Double unitPrice = Convert.ToDouble(Regex.Replace(line.Substring(65, 15), "[A-Za-z ]", "").Replace(",", "").Trim());
                            cso.unit_price += (unitPrice) + System.Environment.NewLine;
                            cso.so_desc += Regex.Replace(line.Substring(7, 12), @"[^0-9a-zA-Z\._-]", string.Empty) + System.Environment.NewLine;
                            cso.quantity += Convert.ToInt16(_quantity) + System.Environment.NewLine;
                        }
                        else if (line.Contains("Report no."))
                        {
                            cso.report_no += (line.Replace("Report no.", "").Trim());// + System.Environment.NewLine;
                        }
                        else if (line.Contains("ELP-") || line.Contains("ELS-") || line.Contains("ELN-") || line.Contains("FA-") || line.Contains("ELWA-") || line.Contains("GRP-") || line.Contains("TRB-"))
                        {
                            cso.report_no += (line.Replace("Report no.", "").Trim());// + System.Environment.NewLine;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
                //add last item
                cso.status = "I";
                cso.create_date = DateTime.Now;
                listGroupSo.Add(cso);



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
                    job_sample_group_so updateSo = job_sample_group_so.getBySo(so.so);
                    if (updateSo == null)
                    {
                        so.filename = Path.GetFileName(filePath);
                        so.Insert();
                    }
                    else
                    {
                        updateSo.filename = Path.GetFileName(filePath);
                        updateSo.unit_price = so.unit_price;
                        updateSo.so_desc = so.so_desc;
                        updateSo.report_no = so.report_no;
                        updateSo.quantity = so.quantity;
                        updateSo.status = so.status = "I";
                        updateSo.inv_status = "I";
                        updateSo.inv_no = null;
                        updateSo.inv_date = null;
                        updateSo.inv_duedate = null;
                        updateSo.inv_amt = null;
                        updateSo.update_date = DateTime.Now;
                        updateSo.Update();
                    }
                }

                //delete batch jobSample by condition
                string ids = string.Join(",", listGroupSo.Select(x => "'" + x.so + "'"));
                string sqlDelJobInfo = "delete from job_info where id in (select job_id from job_sample where template_id=-1 and job_status=3 and sample_so in (" + ids + "))";
                MaintenanceBiz.ExecuteReturnDt(sqlDelJobInfo);
                string sqlDelSample = "delete from job_sample where template_id=-1 and job_status=3 and sample_so in (" + ids + ")";
                MaintenanceBiz.ExecuteReturnDt(sqlDelSample);

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
            Message = "";
            MsgLogs = "";

            StringBuilder logs = new StringBuilder();

            if (searchResult.ToDataTable().Rows.Count > 0)
            {
                StringBuilder sbJobFail = new StringBuilder();


                IEnumerable<job_sample_group_so> soGroup = this.searchResult;

                List<job_sample_group_so_ignore_code> ignoreCode = new job_sample_group_so_ignore_code().SelectInvAll();
                if (soIds != null)
                {
                    soGroup = this.searchResult.Where(x => soIds.Contains(x.id)).ToList();
                }
                soGroup = soGroup.Where(x => (x.unit_price != null)).ToList();
                int total = soGroup.Count();
                int fail = 0;
                logs.Append("<br>--------------------------------------------------------------------------------");
                logs.Append("<br>############ Load 'SO' List ############");
                logs.Append("<br>--------------------------------------------------------------------------------");



                Console.WriteLine();


                foreach (job_sample_group_so _updateCso in soGroup)
                {
                    Boolean isComplete = true;

                    logs.Append("<br>#SO = " + _updateCso.so + "<br>");
                    String[] Quantitys = _updateCso.quantity.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    String[] UnitPrices = _updateCso.unit_price.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    String[] ReportNoLists = _updateCso.report_no == null ? createEmptyArray(Quantitys.Length) : _updateCso.report_no.Split(new[] { "|" }, StringSplitOptions.None);
                    String[] descs = _updateCso.so_desc.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                    if (ReportNoLists.Count() != Quantitys.Count())
                    {
                        ReportNoLists = createEmptyArray(Quantitys.Length, _updateCso);
                    }

                    _updateCso.report_no = "";//clear
                    for (int i = 0; i < Quantitys.Length - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(Quantitys[i]))
                        {
                            string soDesc = descs[i];
                            string rptNo = ReportNoLists[i];
                            double amt = Convert.ToDouble(UnitPrices[i]);
                            int quantity = Convert.ToInt16(Quantitys[i]);

                            List<string> jobNumbers = getJobNumFromList(rptNo, quantity, _updateCso, soDesc, amt);

                            List<job_sample> listUpdates = job_sample.FindAllByJobNumbers(jobNumbers);
                            foreach(job_sample js in listUpdates)
                            {
                                js.sample_so = _updateCso.so;
                                js.sample_invoice_amount = amt;
                                js.Update();
                                logs.Append(" - " + js.job_number + "[x],");
                            }

                            if (listUpdates.Count() < jobNumbers.Count())
                            {
                                logs.Append(" - ");
                                sbJobFail.Append(_updateCso.so + ",");

                                List<string> updated = listUpdates.Select(x => x.job_number).ToList<string>();

                                List<string> xxx = new List<string>();
                                foreach(string o in jobNumbers)
                                {
                                    if (!updated.Contains(o))
                                    {
                                        xxx.Add(o);
                                        logs.Append(","+o+"[ ]");
                                    }
                                }
                                isComplete = false;
                            }

                            _updateCso.report_no += string.Join(",", jobNumbers) + "|";
                        }
                        else
                        {
                            //quantiy is empty.
                        }
                        Console.WriteLine();
                    }

                    _updateCso.status = !isComplete ? "I" : "C";
                    _updateCso.inv_status = _updateCso.status.Equals("C") ? "I" : null;
                    _updateCso.Update();
                    logs.Append("<br>#Status :" + _updateCso.status + "<br>");
                    logs.Append("--------------------------------------------------------------------------------<br>");
                }




                GeneralManager.Commit();
                MsgLogs = logs.ToString();
                String errList = (sbJobFail.Length == 0) ? "" : "\nรายการ SO ที่โหลดไม่สำเร็จ คือ " + String.Join(",", sbJobFail.ToString().Split(','));
                Message = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูล Job\n ทั้งหมด" + total + " รายการ\n สำเร็จ " + (total - fail) + " รายการ " + ((errList.Length > 0) ? errList.Substring(0, errList.Length - 1) : errList) + " </div>";
                bindingData();
            }
        }

        public job_info makeTempJob(string jn, double amt, string remark, string so)
        {

            List<job_sample> js = new List<job_sample>();
            job_sample jobSample = new job_sample();

            jobSample.ID = CustomUtils.GetRandomNumberID();
            jobSample.template_id = -1;
            jobSample.job_number = jn;
            jobSample.RowState = CommandNameEnum.Add;
            jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
            jobSample.job_role = userLogin.role_id;
            jobSample.date_login_inprogress = DateTime.Now;
            jobSample.update_date = DateTime.Now;
            jobSample.update_by = userLogin.id;
            jobSample.is_hold = "0";//0=Unhold
            jobSample.sample_invoice_amount = amt;
            jobSample.sample_so = so;
            jobSample.remarks = remark;
            js.Add(jobSample);

            job_info job = new job_info();
            job.ID = 0;
            job.contract_person_id = 16;// <----------------#1#

            job.customer_id = 4;// <----------------#2#
            job.date_of_request = DateTime.Now;
            job.customer_ref_no = "";
            job.company_name_to_state_in_report = "";
            job.job_number = 0;
            job.job_prefix = 1;// <----------------#3#
            job.date_of_receive = DateTime.Now;
            job.spec_ref_rev_no = "0";

            job.create_by = userLogin.id;
            job.update_by = userLogin.id;
            job.create_date = DateTime.Now;
            job.update_date = DateTime.Now;
            job.document_type = "1";

            job.jobSample = js;
            return job;


        }

        public String[] createEmptyArray(int size, job_sample_group_so _updateCso = null)
        {
            String[] data = new String[size];

            if (_updateCso != null)
            {
                String[] ReportNoLists = _updateCso.report_no.Split(new[] { "|" }, StringSplitOptions.None);
                for (int i = 0; i < size; i++)
                {
                    data[i] = (i < ReportNoLists.Length) ? ReportNoLists[i] : string.Empty;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    data[i] = String.Empty;
                }
            }

            return data;
        }

        public List<string> getJobNumFromList(string job_number, int quantity, job_sample_group_so jobSampleGroupSo, string soDesc, double amt)
        {
            List<string> results = new List<string>();
            if (!String.IsNullOrEmpty(job_number))
            {
                //Regex regex = new Regex(@"^\d$");

                string[] _val = job_number.Split(',');

                for (int i = 0; i < _val.Length; i++)
                {
                    string[] jn = _val[i].Split('-');


                    if (jn.Length == 4 || (jn[0].Equals("GRP") && CustomUtils.isNumber(jn[2])))
                    {
                        int startJob = Convert.ToInt32(jn[1]);
                        int endJob = Convert.ToInt32(jn[2]);
                        for (int idx = startJob; idx <= endJob; idx++)
                        {
                            results.Add(string.Format("{0}-{1}-{2}", jn[0], getNum(idx.ToString()), jn.Length == 4 ? jn[3] : ""));
                        }
                    }
                    else
                    {
                        results.Add(_val[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < quantity; i++)
                {
                    string jobNumber = "#" + DateTime.Now.Ticks;
                    string remark = jobSampleGroupSo.so + "," + soDesc;
                    makeTempJob(jobNumber, amt, remark, jobSampleGroupSo.so).Insert();
                    results.Add(jobNumber);
                }
            }
            GeneralManager.Commit();

            return results;

        }
    }
}