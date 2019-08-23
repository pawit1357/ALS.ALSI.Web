﻿using ALS.ALSI.Biz;
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
            TextBox txtQuantity = gvJob.Rows[e.RowIndex].FindControl("txtQuantity") as TextBox;

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
                    //if (!rn.Equals(""))
                    //{
                        _ReportNo.Add(rn);
                    //}
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
                List<String> _Quantity = new List<String>();
                String[] Quantity = txtQuantity.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                foreach (String rn in Quantity)
                {
                    if (!rn.Equals(""))
                    {
                        _Quantity.Add(rn);
                    }
                }
                _updateCso.unit_price = String.Join(Environment.NewLine, _UnitPrice);
                _updateCso.report_no = String.Join("|", _ReportNo);
                _updateCso.so_desc = String.Join(Environment.NewLine, _SoDesc);
                _updateCso.quantity = String.Join(Environment.NewLine, _Quantity);


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
                case CommandNameEnum.Delete:
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    job_sample_group_so jobSampleGS = this.searchResult.Where(x => x.id == _id).FirstOrDefault();
                    if (jobSampleGS != null)
                    {
                        List<string> soList = new List<string>();
                        soList.Add("'"+jobSampleGS.so+"'");

                        string clearSql = "update job_sample set sample_so = '', sample_invoice = '' where sample_so in (" + string.Join(",", soList) + ")";
                        MaintenanceBiz.ExecuteReturnDt(clearSql);
                        //delete batch jobSample by condition
                        string sqlDelJobInfo = "delete from job_info where id in (select job_id from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + "))";
                        MaintenanceBiz.ExecuteReturnDt(sqlDelJobInfo);
                        string sqlDelSample = "delete from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + ")";
                        MaintenanceBiz.ExecuteReturnDt(sqlDelSample);


                        jobSampleGS.Delete();
                        GeneralManager.Commit();
                        bindingData();
                    }
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
            List<string> ignoreSoCode = new job_sample_group_so_ignore_code().SelectInvAll().Select(x => x.code).ToList<string>();

            try
            {
                List<string> soList = new List<string>();

                job_sample_group_so cso = null;
                int index = 0;
                string so = "";
                string[] lines = System.IO.File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line.Equals("\f"))
                    {
                        if (line.StartsWith("  SO") || (line.Length < 10 ? "" : line.Substring(10, 2)).Equals("SO"))
                        {
                            string _so = line.Substring(10, 10).Trim();
                            soList.Add("'"+_so+"'");
                        }
                    }
                }
                //delete batch jobSample by condition
                string sqlDelJobInfo = "delete from job_info where id in (select job_id from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + "))";
                MaintenanceBiz.ExecuteReturnDt(sqlDelJobInfo);
                string sqlDelSample = "delete from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + ")";
                MaintenanceBiz.ExecuteReturnDt(sqlDelSample);

                Console.WriteLine();
                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line.Equals("\f"))
                    {
                        if (line.StartsWith("  SO") || (line.Length < 10 ? "" : line.Substring(10, 2)).Equals("SO"))
                        {
                            if (index == 0)
                            {
                                so = line.Substring(10, 10).Trim();
                                cso = new job_sample_group_so { so = so, so_company = line.Substring(23, 30) };
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
                                cso.quantity = cso.quantity.TrimEnd('\n').TrimEnd('\r');
                                cso.unit_price = cso.unit_price.TrimEnd('\n').TrimEnd('\r');
                                cso.so_desc = cso.so_desc.TrimEnd('\n').TrimEnd('\r');

                                cso.status = "I";
                                cso.create_date = DateTime.Now;
                                listGroupSo.Add(cso);
                            }
                            if (!so.Equals(line.Substring(10, 10).Trim()))
                            {
                                so = line.Substring(10, 10).Trim();
                                cso = new job_sample_group_so { so = so, so_company = line.Substring(23, 30) };

                            }
                            index++;
                        }
                        else if (line.Contains("SAMPLE") || line.Contains("Each") || line.Contains("Pack"))
                        {
                            string soCode = Regex.Replace(line.Substring(7, 12), @"[^0-9a-zA-Z\._-]", string.Empty);
                            Double _quantity = Convert.ToDouble(Regex.Replace(line.Substring(54, 5), "[A-Za-z ]", "").Replace(",", "").Trim());
                            Double unitPrice = Convert.ToDouble(Regex.Replace(line.Substring(65, 15), "[A-Za-z ]", "").Replace(",", "").Trim());
                            cso.unit_price += (unitPrice) + System.Environment.NewLine;
                            cso.so_desc += soCode + System.Environment.NewLine;
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

                cso.quantity = cso.quantity.TrimEnd('\n').TrimEnd('\r');
                cso.unit_price = cso.unit_price.TrimEnd('\n').TrimEnd('\r');
                cso.so_desc = cso.so_desc.TrimEnd('\n').TrimEnd('\r');

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
                        updateSo.so_company = so.so_company;
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
                List<string> soList = new List<string>();

                if (soIds != null)
                {
                    soGroup = this.searchResult.Where(x => soIds.Contains(x.id)).ToList();
                }
                soGroup = soGroup.Where(x => (x.unit_price != null)).ToList();
                soList = soGroup.Select(x => "'" + x.so + "'").ToList<string>();

                int total = soGroup.Count();
                int fail = 0;
                logs.Append("<br>--------------------------------------------------------------------------------");
                logs.Append("<br>############ Load 'SO' List ############");
                logs.Append("<br>--------------------------------------------------------------------------------");


                string clearSql = "update job_sample set sample_so = '', sample_invoice = '' where sample_so in (" + string.Join(",", soList) + ")";
                MaintenanceBiz.ExecuteReturnDt(clearSql);
                //delete batch jobSample by condition
                string sqlDelJobInfo = "delete from job_info where id in (select job_id from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + "))";
                MaintenanceBiz.ExecuteReturnDt(sqlDelJobInfo);
                string sqlDelSample = "delete from job_sample where template_id=-1 and job_status=3 and sample_so in (" + string.Join(",", soList) + ")";
                MaintenanceBiz.ExecuteReturnDt(sqlDelSample);
                Console.WriteLine();


                foreach (job_sample_group_so _updateCso in soGroup)
                {
                    Boolean isComplete = true;

                    logs.Append("<br>#SO = " + _updateCso.so + "<br>");
                    String[] Quantitys = _updateCso.quantity.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    String[] UnitPrices = _updateCso.unit_price.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    String[] ReportNoLists = createEmptyArray(Quantitys.Length, _updateCso);// _updateCso.report_no == null ? createEmptyArray(Quantitys.Length) : _updateCso.report_no.Split(new[] { "|" }, StringSplitOptions.None);
                    String[] descs = _updateCso.so_desc.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                    Console.WriteLine();

                    if (ReportNoLists.Count() != Quantitys.Count())
                    {
                        //ReportNoLists = createEmptyArray(Quantitys.Length, _updateCso);
                        isComplete = false;
                    }
                    else
                    {

                        //_updateCso.report_no = "";//clear
                        for (int i = 0; i < Quantitys.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(Quantitys[i]))
                            {
                                string soDesc = descs[i];
                                string rptNo = ReportNoLists[i];
                                double amt = Convert.ToDouble(UnitPrices[i]);
                                int quantity = Convert.ToInt16(Quantitys[i]);
                                bool isPackageCost = rptNo.EndsWith("*");
                                if (isPackageCost)
                                {
                                    rptNo = rptNo.Substring(0, rptNo.Length - 1);
                                }
                                List<string> jobNumbers = getJobNumFromList(rptNo, quantity, _updateCso, soDesc, amt);
                                string jns = "";
                                foreach (string s in jobNumbers)
                                {
                                    jns += "'" + s + "',";
                                }
                                jns = jns.EndsWith(",") ? jns.Substring(0, jns.Length - 1) : jns;

                                string sqlUploadBySo = "update job_sample set sample_so='{1}',sample_invoice_amount={2},sample_invoice_package='{3}' where   job_number in ({0});";

                                int result = MaintenanceBiz.ExecuteCommandReturnResult(string.Format(sqlUploadBySo, jns, _updateCso.so, amt, (isPackageCost) ? "Y" : "N"));
                                Console.WriteLine();


                                if (result < jobNumbers.Count())
                                {
                                    //string sqlNotIn = "select * from job_sample where   job_number not in ({0});";
                                    //DataTable dtNotExists = MaintenanceBiz.ExecuteReturnDt(string.Format(sqlNotIn, jns));
                                    logs.Append(" - ");
                                    sbJobFail.Append(_updateCso.so+"["+ jns + "],");
                                    fail++;
                                    isComplete = false;
                                }
                                else
                                {

                                    logs.Append(" - " + jns + "[x],");
                                }

                                //List<job_sample> listUpdates = job_sample.FindAllByJobNumbers(jobNumbers);
                                //foreach (job_sample js in listUpdates)
                                //{
                                //    js.sample_so = _updateCso.so;
                                //    js.sample_invoice_amount = amt;
                                //    js.sample_invoice_package = (isPackageCost) ? "Y" : "N";
                                //    js.Update();
                                //    logs.Append(" - " + js.job_number + "[x],");
                                //}

                                //if (listUpdates.Count() < jobNumbers.Count())
                                //{
                                //    logs.Append(" - ");
                                //    sbJobFail.Append(_updateCso.so + ",");

                                //    List<string> updated = listUpdates.Select(x => x.job_number).ToList<string>();

                                //    List<string> xxx = new List<string>();
                                //    foreach (string o in jobNumbers)
                                //    {
                                //        if (!updated.Contains(o))
                                //        {
                                //            xxx.Add(o);
                                //            logs.Append("," + o + "[ ]");
                                //        }
                                //    }
                                //    fail++;
                                //    isComplete = false;
                                //}

                                //_updateCso.report_no += string.Join(",", jobNumbers) + "|";
                            }
                            else
                            {
                                //quantiy is empty.
                                Console.WriteLine();
                            }
                        }
                    }

                    _updateCso.status = !isComplete ? "I" : "C";
                    _updateCso.inv_status = _updateCso.status.Equals("C") ? "I" : null;
                    _updateCso.Update();
                    logs.Append("<br>#Status :" + _updateCso.status + "<br>");
                    logs.Append("--------------------------------------------------------------------------------<br>");
                }




                GeneralManager.Commit();
                String errList = (sbJobFail.Length == 0) ? "" : "<br>รายการ SO ที่โหลดไม่สำเร็จ คือ " + String.Join(",", sbJobFail.ToString().Split(','));

                Message = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูล ทั้งหมด " + total + " รายการ สำเร็จ " + (total - fail) + " รายการ" + ((fail == 0) ? "" : (" ไม่สำเร็จ " + fail + " รายการ")) + " </div>";
                if (sbJobFail.Length > 0)
                {
                    MsgLogs = errList;

                }
                bindingData();
            }
        }

        public job_info makeTempJob(string jn, double amt, string remark, string so)
        {

            List<job_sample> js = new List<job_sample>();
            job_sample jobSample = new job_sample
            {
                ID = CustomUtils.GetRandomNumberID(),
                template_id = -1,
                job_number = jn,
                RowState = CommandNameEnum.Add,
                job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE),
                job_role = userLogin.role_id,
                date_login_inprogress = DateTime.Now,
                update_date = DateTime.Now,
                update_by = userLogin.id,
                is_hold = "0",//0=Unhold
                sample_invoice_amount = amt,
                sample_so = so,
                remarks = remark,
                specification_id= 29,
                type_of_test_id = 324
            };
            js.Add(jobSample);

            job_info job = new job_info
            {
                ID = 0,
                contract_person_id = 16,// <----------------#1#

                customer_id = 4,// <----------------#2#
                date_of_request = DateTime.Now,
                customer_ref_no = "",
                company_name_to_state_in_report = "",
                job_number = 0,
                job_prefix = 1,// <----------------#3#
                date_of_receive = DateTime.Now,
                spec_ref_rev_no = "0",

                create_by = userLogin.id,
                update_by = userLogin.id,
                create_date = DateTime.Now,
                update_date = DateTime.Now,
                document_type = "1",

                
                jobSample = js
            };
            return job;


        }

        public String[] createEmptyArray(int size, job_sample_group_so _updateCso = null)
        {
            String[] data = new String[size];

            if (_updateCso != null)
            {
                if (_updateCso.report_no == null)
                {
                    for (int i = 0; i < size; i++)
                    {
                        data[i] = string.Empty;
                    }
                }
                else
                {
                    String[] ReportNoLists = _updateCso.report_no.Split(new[] { "|" }, StringSplitOptions.None);
                    for (int i = 0; i < size; i++)
                    {
                        data[i] = (i < ReportNoLists.Length) ? ReportNoLists[i] : string.Empty;
                    }
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
                    if (jn.Length == 2)
                    {
                        _val[i] = _val[i] + "-";
                        jn = _val[i].Split('-');
                    }


                    if (jn.Length == 4 || (jn[0].Equals("GRP") && CustomUtils.isNumber(jn[2])))
                    {
                        int startJob = Convert.ToInt32(jn[1]);
                        int endJob = Convert.ToInt32(jn[2]);
                        for (int idx = startJob; idx <= endJob; idx++)
                        {
                            results.Add(string.Format("{0}-{1}-{2}", jn[0], getNum(idx.ToString()), jn.Length == 4 ? jn[3].ToUpper() : ""));
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
                    makeTempJob(jobNumber, amt, remark, jobSampleGroupSo.so).Insert(false);
                    results.Add(jobNumber);
                }
            }
            GeneralManager.Commit();

            return results;

        }

        protected void gvJob_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}




