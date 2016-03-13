using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
using Microsoft.Reporting.WebForms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_FTIR : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_FTIR));

        #region "Property"

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public template_wd_ftir_coverpage Ftir
        {
            get { return (template_wd_ftir_coverpage)Session[GetType().Name + "Ftir"]; }
            set { Session[GetType().Name + "Ftir"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int SampleID
        {
            get { return (int)Session[GetType().Name + "SampleID"]; }
            set { Session[GetType().Name + "SampleID"] = value; }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "Ftir");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        List<String> errors = new List<string>();


        private void initialPage()
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec();
            detailSpec.specification_id = this.jobSample.specification_id;
            detailSpec.template_id = this.jobSample.template_id;

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlDetailSpec.Items.Clear();
            ddlDetailSpec.DataSource = detailSpec.SelectAll();
            ddlDetailSpec.DataBind();
            ddlDetailSpec.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            #region "SAMPLE"
            this.jobSample = new job_sample().SelectByID(this.SampleID);
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            if (this.jobSample != null)
            {
                lbJobStatus.Text = Constants.GetEnumDescription(status);

                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pSpecification.Visible = false;
                pStatus.Visible = false;
                pUploadfile.Visible = false;
                pDownload.Visible = false;
                btnSubmit.Visible = false;
                //btnCalculate.Visible = false;
                switch (userRole)
                {
                    case RoleEnum.LOGIN:
                        if (status == StatusEnum.LOGIN_SELECT_SPEC)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = true;
                            pStatus.Visible = false;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                            //btnCalculate.Visible = false;
                        }
                        break;
                    case RoleEnum.CHEMIST:
                        if (status == StatusEnum.CHEMIST_TESTING)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = false;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                            //btnCalculate.Visible = true;
                        }
                        break;
                    case RoleEnum.SR_CHEMIST:
                        if (status == StatusEnum.SR_CHEMIST_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt16(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt16(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                            //btnCalculate.Visible = true;
                        }
                        break;
                    case RoleEnum.ADMIN:
                        if (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = false;
                            pUploadfile.Visible = true;
                            pDownload.Visible = true;
                            btnSubmit.Visible = true;
                            //btnCalculate.Visible = false;
                        }
                        break;
                    case RoleEnum.LABMANAGER:
                        if (status == StatusEnum.LABMANAGER_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt16(StatusEnum.LABMANAGER_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt16(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = true;
                            btnSubmit.Visible = true;
                            //btnCalculate.Visible = false;
                        }
                        break;
                }
                #region "METHOD/PROCEDURE:"
                if (status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING
            && userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST) || userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                {
                    #region ":: STAMP ANALYZED DATE ::"
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        if (this.jobSample.date_analyzed_date == null)
                        {
                            this.jobSample.date_analyzed_date = DateTime.Now;
                            this.jobSample.Update();
                        }
                    }
                    #endregion

                    txtB21.Visible = true;
                    txtB22.Visible = true;
                    txtB23.Visible = true;

                    txtC21.Visible = true;
                    txtC22.Visible = true;
                    txtC23.Visible = true;

                    txtD21_1.Visible = true;
                    txtD22.Visible = true;
                    txtD23.Visible = true;

                    txtE21.Visible = true;
                    txtE22.Visible = true;
                    txtE23.Visible = true;

                    lbB21.Visible = false;
                    lbB22.Visible = false;
                    lbB23.Visible = false;

                    lbC21.Visible = false;
                    lbC22.Visible = false;
                    lbC23.Visible = false;

                    lbD21_1.Visible = false;
                    lbD22.Visible = false;
                    lbD23.Visible = false;

                    lbE21.Visible = false;
                    lbE22.Visible = false;
                    lbE23.Visible = false;

                    CheckBox1.Visible = true;
                    CheckBox2.Visible = true;
                    CheckBox3.Visible = true;
                    CheckBox5.Visible = true;
                    CheckBox6.Visible = true;


                    btnNVRFTIR.Visible = true;

                }
                else
                {

                    txtB21.Visible = false;
                    txtB22.Visible = false;
                    txtB23.Visible = false;

                    txtC21.Visible = false;
                    txtC22.Visible = false;
                    txtC23.Visible = false;

                    txtD21_1.Visible = false;
                    txtD22.Visible = false;
                    txtD23.Visible = false;

                    txtE21.Visible = false;
                    txtE22.Visible = false;
                    txtE23.Visible = false;

                    lbB21.Visible = true;
                    lbB22.Visible = true;
                    lbB23.Visible = true;

                    lbC21.Visible = true;
                    lbC22.Visible = true;
                    lbC23.Visible = true;

                    lbD21_1.Visible = true;
                    lbD22.Visible = true;
                    lbD23.Visible = true;


                    lbE21.Visible = true;
                    lbE22.Visible = true;
                    lbE23.Visible = true;

                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox5.Visible = false;
                    CheckBox6.Visible = false;

                    btnNVRFTIR.Visible = false;

                }
                #endregion
            }
            #endregion
            #region "WorkSheet"

            this.Ftir = new template_wd_ftir_coverpage().SelectBySampleID(this.SampleID);
            if (this.Ftir != null)
            {
                #region "NVR-FTIR(Hex)"
                txtNVR_FTIR_B14.Text = this.Ftir.td_b14;//Volume of solvent used:
                txtNVR_FTIR_B15.Text = this.Ftir.td_b15;//Surface area (S):
                txtNVR_FTIR_B16.Text = this.Ftir.td_b16;//No. of parts extracted (N):
                ////
                txtB21.Text = this.Ftir.procedure_no;//Procedure No
                txtB22.Text = this.Ftir.procedure_no;//Procedure No
                txtB23.Text = this.Ftir.procedure_no;//Procedure No

                txtC21.Text = txtNVR_FTIR_B16.Text;//Number of piecesused for extraction
                txtC22.Text = txtNVR_FTIR_B16.Text;//Number of piecesused for extraction
                txtC23.Text = txtNVR_FTIR_B16.Text;//Number of piecesused for extraction

                txtD21_1.Text = this.Ftir.extraction_medium;//ExtractionMedium
                txtD22.Text = this.Ftir.extraction_medium;//ExtractionMedium
                txtD23.Text = this.Ftir.extraction_medium;//ExtractionMedium

                txtE21.Text = txtNVR_FTIR_B14.Text;//Extraction Volume
                txtE22.Text = txtNVR_FTIR_B14.Text;//Extraction Volume
                txtE23.Text = txtNVR_FTIR_B14.Text;//Extraction Volume

                lbB21.Text = txtB21.Text;
                lbB22.Text = txtB22.Text;
                lbB23.Text = txtB23.Text;
                lbC21.Text = txtC21.Text;
                lbC22.Text = txtC22.Text;
                lbC23.Text = txtC23.Text;
                lbD21_1.Text = txtD21_1.Text;
                lbD22.Text = txtD22.Text;
                lbD23.Text = txtD23.Text;
                lbE21.Text = txtE21.Text;
                lbE22.Text = txtE22.Text;
                lbE23.Text = txtE23.Text;
                ////

                txtNVR_B20.Text = this.Ftir.nvr_b20;
                txtNVR_B21.Text = this.Ftir.nvr_b21;
                txtNVR_C20.Text = this.Ftir.nvr_c20;
                txtNVR_C21.Text = this.Ftir.nvr_c21;
                lbC26.Text = this.Ftir.nvr_c26;

                txtFTIR_B30.Text = this.Ftir.ftr_b30;
                txtFTIR_B31.Text = this.Ftir.ftr_b31;
                txtFTIR_B32.Text = this.Ftir.ftr_b32;
                txtFTIR_B33.Text = this.Ftir.ftr_b33;
                txtFTIR_B35.Text = this.Ftir.ftr_b35;

                lbFTIR_C40.Text = this.Ftir.ftr_c40;

                txtFTIR_B42.Text = this.Ftir.ftr_b42;
                txtFTIR_B43.Text = this.Ftir.ftr_b43;
                txtFTIR_B44.Text = this.Ftir.ftr_b44;
                txtFTIR_B45.Text = this.Ftir.ftr_b45;

                lbFTIR_C49.Text = this.Ftir.ftr_c49;
                #endregion

                ddlDetailSpec.SelectedValue = this.Ftir.detail_spec_id.ToString();
                ddlComponent.SelectedValue = this.Ftir.component_id.ToString();
                detailSpec = new tb_m_detail_spec().SelectByID(Convert.ToInt32(this.Ftir.detail_spec_id));
                if (detailSpec != null)
                {
                    lbDocRev.Text = detailSpec.B;
                    lbDesc.Text = detailSpec.A;
                    lbC28.Text =detailSpec.E.Equals("-")? "NA": detailSpec.E.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.E);//NVR
                    lbC30.Text = detailSpec.F.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.F); ;//FTIR

                    lbE27.Text = detailSpec.D;
                    lbD27.Text = detailSpec.D;

                    lbD28.Text = this.Ftir.nvr_c26;
                    lbD30.Text = this.Ftir.ftr_c40;

                    //=IF(C28="NA","NA",IF(OR(D28="Not Detected",D28<INDEX('Detail Spec'!$A$3:$H$280,$F$1,5)),"PASS","FAIL"))
                    //if (!String.IsNullOrEmpty(lbD28.Text) && !detailSpec.E.Equals("-"))
                    //{
                    //    lbE28.Text = lbC28.Text.Equals("NA") ? "NA" : (lbD28.Text.Equals("Not Detected") || Convert.ToDouble(lbD28.Text) < Convert.ToDouble(detailSpec.E)) ? "PASS" : "FAIL";
                    //}
                    ////=IF(C30="NA","NA",IF(OR(D30="< MDL",D30<INDEX('Detail Spec'!$A$3:$H$280,$F$1,6)),"PASS","FAIL"))
                    //if (!String.IsNullOrEmpty(lbD30.Text))
                    //{
                    //    if (lbD30.Text.Equals("Not Detected"))
                    //    {
                    //        lbE30.Text = "PASS";
                    //    }
                    //    else
                    //    {
                    //        lbE30.Text = lbC30.Text.Equals("NA") ? "NA" : (lbD30.Text.Equals("MDL") || Convert.ToDouble(lbD30.Text) < Convert.ToDouble(detailSpec.F)) ? "PASS" : "FAIL";

                    //    }
                    //}

                    CalculateCas();
                }
                tb_m_component component = new tb_m_component().SelectByID(Convert.ToInt32(this.Ftir.component_id));
                if (component != null)
                {
                    lbB21.Text = component.B;
                    lbB22.Text = component.B;
                    lbB23.Text = component.B;

                    lbC21.Text = this.Ftir.td_b16;
                    lbC22.Text = this.Ftir.td_b16;
                    lbC23.Text = this.Ftir.td_b16;

                    lbD21.Text = component.F;
                    lbD22.Text = component.F;
                    lbD23.Text = component.F;

                    lbE21.Text = String.Format("{0} mL", component.G);
                    lbE22.Text = String.Format("{0} mL", component.G);
                    lbE23.Text = String.Format("{0} mL", component.G);
                }


                this.CommandName = CommandNameEnum.Edit;
                ShowItem(this.Ftir.item_visible);
            }
            else
            {
                this.Ftir = new template_wd_ftir_coverpage();
                this.CommandName = CommandNameEnum.Add;
            }
            #endregion

            //initial component
            btnNVRFTIR.CssClass = "btn green";
            btnCoverPage.CssClass = "btn blue";
            pCoverpage.Visible = true;
            PWorking.Visible = false;
            pLoadFile.Visible = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnNVRFTIR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnNVRFTIR.CssClass = "btn green";
                    btnCoverPage.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    PWorking.Visible = false;
                    pLoadFile.Visible = false;
                    txtC21.Text = txtNVR_FTIR_B16.Text;
                    txtC22.Text = txtNVR_FTIR_B16.Text;
                    txtC23.Text = txtNVR_FTIR_B16.Text;

                    txtE21.Text = String.Format("{0} mL", txtNVR_FTIR_B14.Text);
                    txtE22.Text = String.Format("{0} mL", txtNVR_FTIR_B14.Text);
                    txtE23.Text = String.Format("{0} mL", txtNVR_FTIR_B14.Text);

                    lbC21.Text = txtC21.Text;
                    lbC22.Text = txtC22.Text;
                    lbC23.Text = txtC23.Text;

                    lbE21.Text = txtE21.Text;
                    lbE22.Text = txtE22.Text;
                    lbE23.Text = txtE23.Text;
                    CalculateCas();
                    break;
                case "NVR-FTIR(Hex)":
                    btnNVRFTIR.CssClass = "btn green";
                    btnCoverPage.CssClass = "btn blue";
                    pCoverpage.Visible = false;
                    PWorking.Visible = true;
                    pLoadFile.Visible = true;

                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;

                    #region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_test_completed = DateTime.Now;
                    #endregion
                    #region "NVR-FTIR(Hex)"
                    this.Ftir.sample_id = this.SampleID;
                    this.Ftir.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                    this.Ftir.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                    this.Ftir.item_visible = getItemStatus();
                    this.Ftir.procedure_no = String.IsNullOrEmpty(txtB21.Text) ? (String.IsNullOrEmpty(txtB22.Text) ? (String.IsNullOrEmpty(txtB23.Text) ? String.Empty : txtB23.Text) : txtB22.Text) : txtB21.Text;
                    this.Ftir.extraction_medium = String.IsNullOrEmpty(txtD21_1.Text) ? (String.IsNullOrEmpty(txtD22.Text) ? (String.IsNullOrEmpty(txtD23.Text) ? String.Empty : txtD23.Text) : txtD22.Text) : txtD21_1.Text;
                    this.Ftir.td_b14 = txtNVR_FTIR_B14.Text;//Volume of solvent used:
                    this.Ftir.td_b15 = txtNVR_FTIR_B15.Text;//Surface area (S):
                    this.Ftir.td_b16 = txtNVR_FTIR_B16.Text;//No. of parts extracted (N):

                    this.Ftir.nvr_b20 = txtNVR_B20.Text;//Blank (B)-Wt.of Empty Pan (µg)
                    this.Ftir.nvr_b21 = txtNVR_B21.Text;//Sample (A)-Wt.of Empty Pan (µg)
                    this.Ftir.nvr_c20 = txtNVR_C20.Text;//Blank (B)-Wt.of Pan + Residue (µg)
                    this.Ftir.nvr_c21 = txtNVR_C21.Text;//Sample (A)-Wt.of Pan + Residue (µg)
                    this.Ftir.nvr_c26 = lbC26.Text;//Calculations:-Wt. of Residue (µg)

                    this.Ftir.ftr_b30 = txtFTIR_B30.Text;
                    this.Ftir.ftr_b31 = txtFTIR_B31.Text;
                    this.Ftir.ftr_b32 = txtFTIR_B32.Text;
                    this.Ftir.ftr_b33 = txtFTIR_B33.Text;
                    this.Ftir.ftr_b35 = txtFTIR_B35.Text;

                    this.Ftir.ftr_c40 = lbFTIR_C40.Text;

                    this.Ftir.ftr_b42 = txtFTIR_B42.Text;
                    this.Ftir.ftr_b43 = txtFTIR_B43.Text;
                    this.Ftir.ftr_b44 = txtFTIR_B44.Text;
                    this.Ftir.ftr_b45 = txtFTIR_B45.Text;

                    this.Ftir.ftr_c49 = lbFTIR_C49.Text;
                    #endregion
                    switch (CommandName)
                    {
                        case CommandNameEnum.Add:
                            this.Ftir.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            this.Ftir.Update();
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;

                    #region "NVR-FTIR(Hex)"
                    this.Ftir.sample_id = this.SampleID;
                    this.Ftir.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                    this.Ftir.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                    this.Ftir.item_visible = getItemStatus();
                    this.Ftir.procedure_no = String.IsNullOrEmpty(txtB21.Text) ? (String.IsNullOrEmpty(txtB22.Text) ? (String.IsNullOrEmpty(txtB23.Text) ? String.Empty : txtB23.Text) : txtB22.Text) : txtB21.Text;
                    this.Ftir.extraction_medium = String.IsNullOrEmpty(txtD21_1.Text) ? (String.IsNullOrEmpty(txtD22.Text) ? (String.IsNullOrEmpty(txtD23.Text) ? String.Empty : txtD23.Text) : txtD22.Text) : txtD21_1.Text;
                    this.Ftir.td_b14 = txtNVR_FTIR_B14.Text;//Volume of solvent used:
                    this.Ftir.td_b15 = txtNVR_FTIR_B15.Text;//Surface area (S):
                    this.Ftir.td_b16 = txtNVR_FTIR_B16.Text;//No. of parts extracted (N):

                    this.Ftir.nvr_b20 = txtNVR_B20.Text;//Blank (B)-Wt.of Empty Pan (µg)
                    this.Ftir.nvr_b21 = txtNVR_B21.Text;//Sample (A)-Wt.of Empty Pan (µg)
                    this.Ftir.nvr_c20 = txtNVR_C20.Text;//Blank (B)-Wt.of Pan + Residue (µg)
                    this.Ftir.nvr_c21 = txtNVR_C21.Text;//Sample (A)-Wt.of Pan + Residue (µg)
                    this.Ftir.nvr_c26 = lbC26.Text;//Calculations:-Wt. of Residue (µg)

                    this.Ftir.ftr_b30 = txtFTIR_B30.Text;
                    this.Ftir.ftr_b31 = txtFTIR_B31.Text;
                    this.Ftir.ftr_b32 = txtFTIR_B32.Text;
                    this.Ftir.ftr_b33 = txtFTIR_B33.Text;
                    this.Ftir.ftr_b35 = txtFTIR_B35.Text;

                    this.Ftir.ftr_c40 = lbFTIR_C40.Text;

                    this.Ftir.ftr_b42 = txtFTIR_B42.Text;
                    this.Ftir.ftr_b43 = txtFTIR_B43.Text;
                    this.Ftir.ftr_b44 = txtFTIR_B44.Text;
                    this.Ftir.ftr_b45 = txtFTIR_B45.Text;

                    this.Ftir.ftr_c49 = lbFTIR_C49.Text;
                    #endregion
                    this.Ftir.Update();
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"
                            this.jobSample.sr_approve_date = DateTime.Now;
                            #endregion
                            break;
                        case StatusEnum.SR_CHEMIST_DISAPPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.jobSample.ID,
                                log_title = String.Format("Sr.Chemist DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.jobSample.step4owner = userLogin.id;
                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                    StatusEnum labApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (labApproveStatus)
                    {
                        case StatusEnum.LABMANAGER_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);
                            break;
                        case StatusEnum.LABMANAGER_DISAPPROVE:
                            this.jobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.jobSample.ID,
                                log_title = String.Format("Lab Manager DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.jobSample.step5owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        btnUpload.SaveAs(source_file);
                        this.jobSample.path_word = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".pdf")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        btnUpload.SaveAs(source_file);
                        this.jobSample.path_pdf = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.jobSample.step7owner = userLogin.id;
                    break;

            }
            //########
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string sheetName = string.Empty;

            List<tb_m_gcms_cas> _cas = new List<tb_m_gcms_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);
                        #region "XLS"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);
                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["wd.ftir.excel.sheetname.working1"]);// wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_motor_oil"]);
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.ftir.excel.sheetname.working1"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    #region "Test Data"
                                    txtNVR_FTIR_B14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_FTIR_B15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_FTIR_B16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.B));
                                    #endregion
                                    #region "NVR"
                                    txtNVR_B20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_B21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_C20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                    txtNVR_C21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));

                                    lbD20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                    lbD21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.D));
                                    lbC26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));

                                    //Decimal
                                    txtNVR_B20.Text = String.IsNullOrEmpty(txtNVR_B20.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_B20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                    txtNVR_C20.Text = String.IsNullOrEmpty(txtNVR_C20.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_C20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                    lbD20.Text = String.IsNullOrEmpty(lbD20.Text) ? "" : Math.Round(Convert.ToDecimal(lbD20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";

                                    txtNVR_B21.Text = String.IsNullOrEmpty(txtNVR_B21.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_B21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                    txtNVR_C21.Text = String.IsNullOrEmpty(txtNVR_C21.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_C21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                    lbD21.Text = String.IsNullOrEmpty(lbD21.Text) ? "" : Math.Round(Convert.ToDecimal(lbD21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";

                                    lbC26.Text = String.IsNullOrEmpty(lbC26.Text) ? "" : Math.Round(Convert.ToDecimal(lbC26.Text), Convert.ToInt16(txtDecimal08.Text)) + "";

                                    #endregion
                                    #region "FTIR-Silicone"
                                    txtFTIR_B30.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B31.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B32.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B33.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B35.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.B));
                                    lbFTIR_C40.Text = CustomUtils.GetCellValue(isheet.GetRow(40 - 1).GetCell(ExcelColumn.C));



                                    txtFTIR_B30.Text = txtFTIR_B30.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B30.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B30.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                    txtFTIR_B31.Text = txtFTIR_B31.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B31.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B31.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                    txtFTIR_B32.Text = txtFTIR_B32.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B32.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B32.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                    txtFTIR_B33.Text = txtFTIR_B33.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B33.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B33.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                    txtFTIR_B35.Text = txtFTIR_B35.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B35.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B35.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    lbFTIR_C40.Text = lbFTIR_C40.Text.Equals("< MDL") ? "< MDL" : lbFTIR_C40.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbFTIR_C40.Text) ? "" : Math.Round(Convert.ToDecimal(lbFTIR_C40.Text), Convert.ToInt16(txtDecimal08.Text)) + "";


                                    #endregion
                                    #region "FTIR-Amide"
                                    txtFTIR_B42.Text = CustomUtils.GetCellValue(isheet.GetRow(42 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B43.Text = CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.B));
                                    txtFTIR_B44.Text = CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.B));

                                    txtFTIR_B45.Text = CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.B));
                                    lbFTIR_C49.Text = CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.C));

                                    txtFTIR_B42.Text = txtFTIR_B42.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B42.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B42.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                    txtFTIR_B43.Text = txtFTIR_B43.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B43.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B43.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                    txtFTIR_B44.Text = txtFTIR_B44.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B44.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B44.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                    txtFTIR_B45.Text = txtFTIR_B45.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtFTIR_B45.Text) ? "" : Math.Round(Convert.ToDecimal(txtFTIR_B45.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                    //txtFTIR_B35.Text = Math.Round(Convert.ToDecimal(txtFTIR_B35.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    lbFTIR_C49.Text = lbFTIR_C49.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbFTIR_C49.Text) ? "" : Math.Round(Convert.ToDecimal(lbFTIR_C49.Text), Convert.ToInt16(txtDecimal08.Text)) + "";



                                    #endregion
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                        }
                        #endregion


                    }
                }
                catch (Exception ex)
                {
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));

                    Console.WriteLine();
                }
            }

            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            char[] item = this.Ftir.item_visible.ToCharArray();

            DataTable dtHeader = new DataTable("MethodProcedure");

            // Define all the columns once.
            DataColumn[] cols ={ new DataColumn("ProcedureNo",typeof(String)),
                                  new DataColumn("NumOfPiecesUsedForExtraction",typeof(String)),
                                  new DataColumn("ExtractionMedium",typeof(String)),
                                  new DataColumn("ExtractionVolume",typeof(String)),
                              };
            dtHeader.Columns.AddRange(cols);
            DataRow row = dtHeader.NewRow();
            row["ProcedureNo"] = item[0] == '1' ? txtB21.Text : item[1] == '1' ? txtB22.Text : txtB23.Text;
            row["NumOfPiecesUsedForExtraction"] = item[0] == '1' ? txtC21.Text : item[1] == '1' ? txtC22.Text : txtC23.Text;
            row["ExtractionMedium"] = item[0] == '1' ? txtD21_1.Text : item[1] == '1' ? txtD22.Text : txtD23.Text;
            row["ExtractionVolume"] = item[0] == '1' ? txtE21.Text : item[1] == '1' ? txtE22.Text : txtE23.Text;
            dtHeader.Rows.Add(row);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            List<ReportData> reportList = new List<ReportData>();
            //Create ReportData NVR
            ReportData tmp = new ReportData
            {

                A = "Non-Volatile Residue (NVR)",
                B = lbC28.Text,
                C = lbD28.Text,
                D = lbE28.Text
            };
            if (item[3] == '1')
            {
                reportList.Add(tmp);
            }
            tmp = new ReportData
            {

                A = "Silicone at Wave No:2962, 1261, 1092, 1022 & 800cm-1",
                B = lbC30.Text,
                C = lbD30.Text,
                D = lbE30.Text
            };
            if (item[4] == '1')
            {
                reportList.Add(tmp);
            }


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1 + reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", item[0] == '1' ? "NVR/FTIR" : item[1] == '1' ? "NVR" : "FTIR"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Seagate's Doc {0} for {1}", lbDocRev.Text, lbDesc.Text)));
            reportParameters.Add(new ReportParameter("Remarks", String.Format("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.The instrument detection limit for silicone oil is  {0} {1}", lbA31.Text, lbB31.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", reportList.ToDataTable())); // Add datasource here



            string download = String.Empty;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                    }
                    else
                    {
                        byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                        Response.BinaryWrite(bytes); // create the file
                        Response.Flush(); // send it to the client to download
                    }
                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                case StatusEnum.LABMANAGER_APPROVE:
                case StatusEnum.LABMANAGER_DISAPPROVE:
                    if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                    }
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                    }
                    //if (!String.IsNullOrEmpty(this.jobSample.path_pdf))
                    //{
                    //    Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_pdf));
                    //}
                    //else
                    //{
                    //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    //    // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                    //    Response.Buffer = true;
                    //    Response.Clear();
                    //    Response.ContentType = mimeType;
                    //    Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                    //    Response.BinaryWrite(bytes); // create the file
                    //    Response.Flush(); // send it to the client to download
                    //}
                    break;
            }




        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue.ToString(), true);
            switch (status)
            {
                case StatusEnum.SR_CHEMIST_DISAPPROVE:
                    pRemark.Visible = true;
                    pDisapprove.Visible = false;
                    break;
                case StatusEnum.LABMANAGER_DISAPPROVE:
                    pRemark.Visible = true;
                    pDisapprove.Visible = true;
                    break;
                default:
                    pRemark.Visible = false;
                    pDisapprove.Visible = false;
                    break;
            }

        }

        protected void ddlDetailSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
            if (detailSpec != null)
            {
                lbDocRev.Text = detailSpec.B;
                lbDesc.Text = detailSpec.A;
                lbC28.Text = detailSpec.E.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.E);//NVR
                lbC30.Text = detailSpec.F.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.F); ;//FTIR

                lbE27.Text = detailSpec.D;
                lbD27.Text = detailSpec.D;
            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {

                lbB21.Text = component.B;
                lbB22.Text = component.B;
                lbB23.Text = component.B;

                lbD21.Text = component.F;
                lbD22.Text = component.F;
                lbD23.Text = component.F;

                lbE21.Text = String.Format("{0} mL", component.G);
                lbE22.Text = String.Format("{0} mL", component.G);
                lbE23.Text = String.Format("{0} mL", component.G);

                txtB21.Text = component.B;
                txtB22.Text = component.B;
                txtB23.Text = component.B;

                txtD21_1.Text = component.F;
                txtD22.Text = component.F;
                txtD23.Text = component.F;

                txtE21.Text = String.Format("{0} mL", component.G);
                txtE22.Text = String.Format("{0} mL", component.G);
                txtE23.Text = String.Format("{0} mL", component.G);

                txtNVR_FTIR_B14.Text = component.G;//Volume of solvent used:

            }
        }

        #region "Custom method"

        private void downloadWord()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            string strFileName = string.Format("{0}_{1}.doc", this.jobSample.job_number.Replace("-", "_"), DateTime.Now.ToString("yyyyMMddhhmmss"));

            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);

            StringWriter sw = new StringWriter();
            HtmlTextWriter h = new HtmlTextWriter(sw);
            invDiv.RenderControl(h);
            string strHTMLContent = sw.GetStringBuilder().ToString();
            String html = "<html><header><style>body {max-width: 800px;margin:initial;font-family: \'Arial Unicode MS\';font-size: 10px;}table {border-collapse: collapse;}th {background: #666;color: #fff;border: 1px solid #999;padding: 0.5rem;text-align: center;}td { border: 1px solid #999;padding: 0.5rem;text-align: left;}h6 {font-weight:initial;}</style></header><body>" + strHTMLContent + "</body></html>";


            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }
        private String getItemStatus()
        {
            String result = String.Empty;
            result = ((CheckBox1.Checked) ? "1" : "0") +
                ((CheckBox2.Checked) ? "1" : "0") +
                ((CheckBox3.Checked) ? "1" : "0") +
                ((CheckBox5.Checked) ? "1" : "0") +
                ((CheckBox6.Checked) ? "1" : "0");
            return result;
        }

        private void ShowItem(String _itemVisible)
        {
            if (_itemVisible != null)
            {
                char[] item = _itemVisible.ToCharArray();
                if (item.Length == 5)
                {


                    StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                    switch (status)
                    {
                        case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            break;
                        case StatusEnum.CHEMIST_TESTING:
                            CheckBox1.Checked = item[0] == '1' ? true : false;
                            CheckBox2.Checked = item[1] == '1' ? true : false;
                            CheckBox3.Checked = item[2] == '1' ? true : false;
                            CheckBox5.Checked = item[3] == '1' ? true : false;
                            CheckBox6.Checked = item[4] == '1' ? true : false;
                            break;
                        case StatusEnum.LOGIN_SELECT_SPEC:
                        case StatusEnum.SR_CHEMIST_CHECKING:
                        case StatusEnum.SR_CHEMIST_APPROVE:
                        case StatusEnum.SR_CHEMIST_DISAPPROVE:
                        case StatusEnum.ADMIN_CONVERT_WORD:
                        case StatusEnum.LABMANAGER_CHECKING:
                        case StatusEnum.LABMANAGER_APPROVE:
                        case StatusEnum.LABMANAGER_DISAPPROVE:
                        case StatusEnum.ADMIN_CONVERT_PDF:
                            CheckBox1.Visible = false;
                            CheckBox2.Visible = false;
                            CheckBox3.Visible = false;
                            CheckBox5.Visible = false;
                            CheckBox6.Visible = false;

                            tab1_tr1.Visible = item[0] == '1' ? true : false;
                            tab1_tr2.Visible = item[1] == '1' ? true : false;
                            tab1_tr3.Visible = item[2] == '1' ? true : false;

                            tab2_tr1.Visible = item[3] == '1' ? true : false;
                            tab3_tr1.Visible = item[4] == '1' ? true : false;
                            break;
                    }
                }
            }

        }

        private void CalculateCas()
        {
            if (!String.IsNullOrEmpty(lbC26.Text))
            {
                //NVR
                lbD28.Text = lbC26.Text.Equals("< MDL") ? "Not Detected" : lbC26.Text;
                if (lbD28.Text.Equals("Not Detected"))
                {
                    lbE28.Text = "PASS";
                }
                else
                {
                    decimal d28Spec = Convert.ToDecimal(CustomUtils.isNumber(lbC28.Text.Replace("<", "").Trim()) ? lbC28.Text.Replace("<", "").Trim() : "0");
                    if (d28Spec == 0)
                    {
                        lbE28.Text = "NA";
                    }
                    else
                    {
                        decimal d28Value = Convert.ToDecimal(lbD28.Text);
                        lbE28.Text = (d28Value == 0) ? "" : (d28Value < d28Spec) ? "PASS" : "FAIL";
                    }
                }
            }
            if (!String.IsNullOrEmpty(lbFTIR_C40.Text))
            {
                //FTIR
                lbD30.Text = lbFTIR_C40.Text.Equals("< MDL") ? "Not Detected" : lbFTIR_C40.Text;
                if (lbD30.Text.Equals("Not Detected"))
                {
                    lbE30.Text = "PASS";
                }
                else
                {
                    decimal d30Spec = Convert.ToDecimal(CustomUtils.isNumber(lbC30.Text.Replace("<", "").Trim()) ? lbC30.Text.Replace("<", "").Trim() : "0");
                    decimal d30Value = Convert.ToDecimal(lbD30.Text);
                    lbE30.Text = (d30Value == 0) ? "" : (d30Value < d30Spec) ? "PASS" : "FAIL";
                }
            }
            btnSubmit.Enabled = true;
        }

        #endregion

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

    }
}