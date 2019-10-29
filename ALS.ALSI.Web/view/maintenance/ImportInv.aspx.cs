using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Collections;
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

        public IEnumerable<job_sample_group_invoice> searchResult
        {
            get { return (IEnumerable<job_sample_group_invoice>)Session[GetType().Name + "job_sample_group_so"]; }
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
        protected String MsgLogs
        {
            get { return (String)Session[GetType().Name + "MsgLogs"]; }
            set { Session[GetType().Name + "MsgLogs"] = value; }
        }
        //protected Hashtable ListMoreOneInv
        //{
        //    get { return (Hashtable)Session[GetType().Name + "ListMoreOneInv"]; }
        //    set { Session[GetType().Name + "ListMoreOneInv"] = value; }
        //}
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
                MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ Text(*.txt) </div>";
            }
        }

        protected void btnBatchLoad_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();
            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;

                if (chk != null && chk.Checked)
                {
                    HiddenField hf = row.Cells[0].FindControl("hid") as HiddenField;
                    ids.Add(hf.Value);
                }
            }
            batchUpload(ids);
        }
        #endregion

        #region "GRD"
        private void bindingData()
        {

            job_sample_group_invoice soLists = new job_sample_group_invoice();
            searchResult = soLists.SelectAll(ddlStatus.SelectedValue, txtSoCode.Text, txtInvoice.Text);

            #region "Check One SO Has More One Inovice"
            //if (this.searchResult.Count() > 0)
            //{
            //    List<string> soList = new List<string>();
            //    ListMoreOneInv = new Hashtable();

            //    IEnumerable<job_sample_group_invoice> invGroups = this.searchResult.ToList();
            //    soList = invGroups.Select(x => "'" + x.so + "'").ToList<string>();
            //    string sqlCheckOneSOHasMoreOneInovice = "select sample_so, count(sample_so) as iCount from(select sample_so from job_sample where sample_so in (" + string.Join(",", soList) + ") group by sample_invoice) x group by x.sample_so";

            //    DataTable dt = MaintenanceBiz.ExecuteReturnDt(sqlCheckOneSOHasMoreOneInovice);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (!ListMoreOneInv.ContainsKey(dr["sample_so"].ToString()))
            //        {
            //            ListMoreOneInv.Add(dr["sample_so"].ToString(), Convert.ToInt32(dr["iCount"].ToString()));
            //        }
            //    }
            //}
            #endregion


            gvJob.DataSource = searchResult;
            gvJob.DataBind();


            Console.WriteLine();
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
                //string _sampleSo = gvJob.DataKeys[e.Row.RowIndex][0].ToString();
                //CheckBox cbSelect = (CheckBox)e.Row.FindControl("cbSelect");
                //LinkButton btnLoad = (LinkButton)e.Row.FindControl("btnLoad");

                //if (null != this.ListMoreOneInv && this.ListMoreOneInv[_sampleSo] != null)
                //{
                //    int count = Convert.ToInt32(this.ListMoreOneInv[_sampleSo]);
                //    cbSelect.Enabled = (count == 1);
                //    btnLoad.Visible = (count > 1);
                //}
                //else
                //{
                //    btnLoad.Visible = false;

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
            //CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            //switch (cmd)
            //{
            //    case CommandNameEnum.View:
            //        int id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            //        List<int> ids = new List<int>();
            //        ids.Add(id);
            //        batchUpload(ids);
            //        break;
            //}
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
            //try
            //{
                List<job_sample_group_invoice> groupInv = new List<job_sample_group_invoice>();
                int index = 0;
                string[] lines = System.IO.File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line.Equals("\f"))
                    {
                        if (line.IndexOf("SO") != -1)
                        {
                            string so = line.Substring(line.IndexOf("SO"), 9);
                            string company = line.Substring(24, 49).Trim();

                            string inv_no = line.Substring(line.IndexOf("IV"), 9);
                            string inv_date = line.Substring(15, 8);
                            string inv_duedate = line.Substring(128, 9);
                            double inv_amt = double.Parse(line.Substring(85, 15));

                            job_sample_group_invoice tmp = new job_sample_group_invoice();
                            tmp.so = so;
                            tmp.inv_no = inv_no;
                            tmp.inv_date = parseDateFromStr(inv_date);
                            tmp.inv_duedate = parseDateFromStr(inv_duedate);
                            tmp.inv_amt = inv_amt;
                            tmp.inv_status = "I";
                            tmp.update_date = DateTime.Now;
                            tmp.company = company;
                            groupInv.Add(tmp);

                            index++;
                        }
                    }
                }
                bUploadSuccess = true;

                if (bUploadSuccess)
                {
                    pSo.Visible = true;
                    //delete old before
                    string inSOs = string.Join(",", groupInv.Select(x => "'" + x.so + "'"));
                    string delOld = "delete from job_sample_group_invoice where so in (" + inSOs + ")";
                    MaintenanceBiz.ExecuteReturnDt(delOld);
                    //insert to db
                    foreach (job_sample_group_invoice sgInv in groupInv)
                    {
                        sgInv.filename = Path.GetFileName(filePath);
                        sgInv.update_date = DateTime.Now;
                        sgInv.Insert();
                    }

                        //insert to db
                        //foreach (job_sample_group_invoice sgInv in groupInv)
                        //{

                        //    job_sample_group_invoice tmp = job_sample_group_invoice.getBySo(sgInv.so);
                        //    if (tmp != null)
                        //    {
                        //        tmp.filename = Path.GetFileName(filePath);
                        //        tmp.inv_status = "I";
                        //        tmp.report_no = "";
                        //        tmp.company = sgInv.company;
                        //        tmp.update_date = DateTime.Now;
                        //        tmp.Update();
                        //    }
                        //    else
                        //    {
                        //        sgInv.report_no = "";
                        //        sgInv.filename = Path.GetFileName(filePath);
                        //        sgInv.create_date = DateTime.Now;
                        //        sgInv.Insert();
                        //    }
                        //}

                        //Console.WriteLine();
                        //string inSOs = string.Join(",", groupInv.Select(x => "'" + x.so + "'"));
                        //string clearSql = "update job_sample set sample_invoice = '' where sample_so in (" + inSOs + ")";
                        //MaintenanceBiz.ExecuteReturnDt(clearSql);


                        GeneralManager.Commit();


                    //transfer
                    //bindingData();
                    this.searchResult = groupInv;
                    #region "Check One SO Has More One Inovice"
                    //if (this.searchResult.Count() > 0)
                    //{
                    //    List<string> soList = new List<string>();
                    //    ListMoreOneInv = new Hashtable();

                    //    IEnumerable<job_sample_group_invoice> invGroups = this.searchResult.ToList();
                    //    soList = invGroups.Select(x => "'" + x.so + "'").ToList<string>();
                    //    string sqlCheckOneSOHasMoreOneInovice = "select sample_so, count(sample_so) as iCount from(select sample_so from job_sample where sample_so in (" + string.Join(",", soList) + ") group by sample_invoice) x group by x.sample_so";

                    //    DataTable dt = MaintenanceBiz.ExecuteReturnDt(sqlCheckOneSOHasMoreOneInovice);
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        if (!ListMoreOneInv.ContainsKey(dr["sample_so"].ToString()))
                    //        {
                    //            ListMoreOneInv.Add(dr["sample_so"].ToString(), Convert.ToInt32(dr["iCount"].ToString()));
                    //        }
                    //    }
                    //}
                    #endregion

                    gvJob.DataSource = this.searchResult;
                    gvJob.DataBind();

                    //Commit
                    MessageINv = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูลเรียบแล้วทั้งหมด " + groupInv.Count() + " รายการ</div>";
                }
                else
                {
                    pSo.Visible = false;
                    MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>เกิดความผิดพลาดในการโหลดข้อมูล SO กรุณาตรวจสอบไฟล์</div>";
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageINv = "<div class=\"alert alert-danger\"><strong>Error!</strong>" + ex.InnerException + "</div>";

            //    Console.WriteLine(ex.Message);
            //}
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

        public void batchUpload(List<string> soIds = null)
        {
            MsgLogs = "";
            StringBuilder logs = new StringBuilder();

            //Boolean isComplete = true;
            StringBuilder sbJobFail = new StringBuilder();

            if (searchResult.ToDataTable().Rows.Count > 0)
            {

                //List<job_sample_group_invoice> listInvUpdates = new List<job_sample_group_invoice>();

                IEnumerable<job_sample_group_invoice> invGroups = this.searchResult.Where(x => soIds.Contains(x.so)).ToList();

                logs.Append("<br>--------------------------------------------------------------------------------");
                logs.Append("<br>############ Load 'Invoice' List ############");
                logs.Append("<br>--------------------------------------------------------------------------------");
                logs.Append("<br>Total INV: " + invGroups.Count());
                int total = invGroups.Count();
                int fail = 0;



                foreach (job_sample_group_invoice invData in invGroups)
                {
                    //update job_sample
                    string sqlUploadBySo = "update job_sample set sample_invoice='{0}',sample_invoice_date='{1}',sample_invoice_complete_date='{2}',sample_invoice_amount_rpt='{3}',sample_invoice_status={4} where sample_so='{5}' and sample_invoice='';";
                    bool result = MaintenanceBiz.ExecuteCommand(string.Format(sqlUploadBySo, invData.inv_no, invData.inv_date.Value.ToString("yyyy-MM-dd"), invData.inv_duedate.Value.ToString("yyyy-MM-dd HH:mm:ss"), invData.inv_amt, Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE), invData.so));
                    //update job_sample_group_invoice

                    string sqlUploadInvG = "update job_sample_group_invoice set inv_status='{0}' where so='{1}'";
                    MaintenanceBiz.ExecuteCommand(string.Format(sqlUploadInvG, result ? "C" : "I", invData.so));
                    if (result)
                    {
                        logs.Append(invData.so + "[X],");
                    }
                    else
                    {
                        sbJobFail.Append("" + invData.so + ",");
                        logs.Append(invData.so + "[ ],");
                        fail++;
                    }
                }

                //GeneralManager.Commit();
                String errList = (sbJobFail.Length == 0) ? "" : "<br>ไม่พบหมาย Job_number ที่ถูกผูกด้วย So ดังนี้ " + String.Join(",", sbJobFail.ToString().Split(','));
                MessageINv = "<div class=\"alert alert-info\"><strong>Info!</strong>โหลดข้อมูล ทั้งหมด " + total + " รายการ สำเร็จ " + (total - fail) + " รายการ" + ((fail == 0) ? "" : (" ไม่สำเร็จ " + fail + " รายการ")) + " </div>";
                if (sbJobFail.Length > 0)
                {
                    MsgLogs = errList;
                }

            }
            bindingData();

        }


        public DateTime parseDateFromStr(string date)
        {
            // : 03/04/19
            if (date.Length == 8)
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

        protected void chkAllSign_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                chkcheck.Checked = cb.Checked;
            }
        }
    }
}

                                        
//<asp:TemplateField HeaderText = "Force Load" ItemStyle-HorizontalAlign="Center">
//    <ItemTemplate>
//        <asp:LinkButton ID = "btnLoad" runat="server" CommandArgument='<%# String.Concat(Eval("so")) %>' CommandName="View" ToolTip="Force Load"><i class="fa fa-rocket"  onclick='return confirm("คุณต้องการที่จะโหลดข้อมูล SO/Invoice ใหม่ จะทำให้ข้อมูลใน SO ที่มีมากว่า 1 Invoice หาย ?");'></i></asp:LinkButton>
//    </ItemTemplate>
//    <HeaderStyle HorizontalAlign = "Center" />
//    < ItemStyle HorizontalAlign="Center" />
//</asp:TemplateField>
                                  