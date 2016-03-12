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
    public partial class Seagate_FTIR : System.Web.UI.UserControl
    {


        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_FTIR));

        #region "Property"
        public String[] MethodType = { "NVR/FTIR", "NVR", "NVR", "FTIR", "FTIR", "FTIR" };

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

        public template_seagate_ftir_coverpage Ftir
        {
            get { return (template_seagate_ftir_coverpage)Session[GetType().Name + "Ftir"]; }
            set { Session[GetType().Name + "Ftir"] = value; }
        }

        public List<ReportData> ReportData
        {
            get { return (List<ReportData>)Session[GetType().Name + "ReportData_Ftir_Seagate"]; }
            set { Session[GetType().Name + "ReportData_Ftir_Seagate"] = value; }
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

        private void initialPage()
        {

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

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

                #region "VISIBLE RESULT DATA"


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

                    btnWorkingFTIR.Visible = true;
                    btnWorkingNVR.Visible = true;

                    th1.Visible = true;
                    th1_cb1.Visible = true;
                    th1_cb2.Visible = true;
                    th1_cb3.Visible = true;
                    th1_cb4.Visible = true;
                    th1_cb5.Visible = true;
                    //Tr29.Visible = true;
                    //Tr30.Visible = true;
                    //Tr31.Visible = true;
                    //Tr32.Visible = true;

                    txtProcedureNo.Enabled = true;
                    txtNomOfPiece.Enabled = true;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolumn.Enabled = true;

                    CheckBox1.Visible = true;
                    CheckBox2.Visible = true;
                    CheckBox3.Visible = true;
                    CheckBox4.Visible = true;
                    CheckBox5.Visible = true;
                    CheckBox6.Visible = true;

                    CheckBox14.Visible = true;
                    CheckBox7.Visible = true;
                    CheckBox13.Visible = true;
                    CheckBox16.Visible = true;
                    CheckBox17.Visible = true;

                    CheckBox8.Visible = true;
                    CheckBox9.Visible = true;
                    CheckBox10.Visible = true;
                    CheckBox11.Visible = true;
                    CheckBox12.Visible = true;

                }
                else
                {
                    btnWorkingFTIR.Visible = false;
                    btnWorkingNVR.Visible = false;
                    th1.Visible = false;
                    th1_cb1.Visible = false;
                    th1_cb2.Visible = false;
                    th1_cb3.Visible = false;
                    th1_cb4.Visible = false;
                    th1_cb5.Visible = false;
                    //Tr29.Visible = false;
                    //Tr30.Visible = false;
                    //Tr31.Visible = false;
                    //Tr32.Visible = false;
                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox4.Visible = false;
                    CheckBox5.Visible = false;
                    CheckBox6.Visible = false;

                    CheckBox14.Visible = false;
                    CheckBox7.Visible = false;
                    CheckBox13.Visible = false;
                    CheckBox16.Visible = false;
                    CheckBox17.Visible = false;

                    CheckBox8.Visible = false;
                    CheckBox9.Visible = false;
                    CheckBox10.Visible = false;
                    CheckBox11.Visible = false;
                    CheckBox12.Visible = false;

                }
                #endregion
            }
            #endregion

            #region "WorkSheet"
            this.Ftir = new template_seagate_ftir_coverpage().SelectBySampleID(this.SampleID);
            if (this.Ftir != null)
            {
                this.CommandName = CommandNameEnum.Edit;


                Label1.Text = this.Ftir.furmular_1;
                Label2.Text = this.Ftir.furmular_2;
                Label3.Text = this.Ftir.furmular_3;
                Label4.Text = this.Ftir.furmular_4;


                #region "working-FTIR"
                txtWB13.Text = this.Ftir.w_b13;
                txtWB14.Text = this.Ftir.w_b14;
                txtWB15.Text = this.Ftir.w_b15;

                txtWB20.Text = this.Ftir.w_b20;
                txtWC20.Text = this.Ftir.w_c20;
                txtWD20.Text = this.Ftir.w_d20;
                txtWE20.Text = this.Ftir.w_e20;

                //txtWB21.Text = this.Ftir.w_b21;
                //txtWC21.Text = this.Ftir.w_c21;
                //txtWD21.Text = this.Ftir.w_d21;
                //txtWE21.Text = this.Ftir.w_e21;

                //txtWB22.Text = this.Ftir.w_b22;
                //txtWC22.Text = this.Ftir.w_c22;
                //txtWD22.Text = this.Ftir.w_d22;
                //txtWE22.Text = this.Ftir.w_e22;

                txtWB24.Text = this.Ftir.w_b24;
                txtWC24.Text = this.Ftir.w_c24;
                txtWD24.Text = this.Ftir.w_d24;
                txtWE24.Text = this.Ftir.w_e24;

                lbWB26.Text = this.Ftir.w_b26;
                lbWC26.Text = this.Ftir.w_c26;
                lbWD26.Text = this.Ftir.w_d26;
                lbWE26.Text = this.Ftir.w_e26;

                lbWB27.Text = this.Ftir.w_b27;
                lbWC27.Text = this.Ftir.w_c27;
                lbWD27.Text = this.Ftir.w_d27;
                lbWE27.Text = this.Ftir.w_e27;
                #endregion
                #region "NVR"
                nvrB16.Text = this.Ftir.nvrb16;
                nvrB17.Text = this.Ftir.nvrb17;
                nvrB18.Text = this.Ftir.nvrb18;
                nvrB20.Text = this.Ftir.nvrb20;
                nvrB21.Text = this.Ftir.nvrb21;
                nvrB22.Text = this.Ftir.nvrb22;
                nvrB24.Text = this.Ftir.nvrb24;
                nvrB25.Text = this.Ftir.nvrb25;
                nvrB26.Text = this.Ftir.nvrb26;
                nvrB28.Text = this.Ftir.nvrb28;
                nvrB29.Text = this.Ftir.nvrb29;
                nvrB30.Text = this.Ftir.nvrb30;
                nvrB32.Text = this.Ftir.nvrb32;
                nvrB33.Text = this.Ftir.nvrb33;
                nvrB34.Text = this.Ftir.nvrb34;
                nvrB36.Text = this.Ftir.nvrb36;

                nvrC16.Text = this.Ftir.nvrc16;
                nvrC17.Text = this.Ftir.nvrc17;
                nvrC18.Text = this.Ftir.nvrc18;
                nvrC20.Text = this.Ftir.nvrc20;
                nvrC21.Text = this.Ftir.nvrc21;
                nvrC22.Text = this.Ftir.nvrc22;
                nvrC24.Text = this.Ftir.nvrc24;
                nvrC25.Text = this.Ftir.nvrc25;
                nvrC26.Text = this.Ftir.nvrc26;
                nvrC28.Text = this.Ftir.nvrc28;
                nvrC29.Text = this.Ftir.nvrc29;
                nvrC30.Text = this.Ftir.nvrc30;
                nvrC32.Text = this.Ftir.nvrc32;
                nvrC33.Text = this.Ftir.nvrc33;
                nvrC34.Text = this.Ftir.nvrc34;
                nvrC36.Text = this.Ftir.nvrc36;
                nvrC37.Text = this.Ftir.nvrc37;
                #endregion


                txtProcedureNo.Text = this.Ftir.ProcedureNo;
                txtNomOfPiece.Text = this.Ftir.NumOfPiece;
                txtExtractionMedium.Text = this.Ftir.ExtractionMedium;
                txtExtractionVolumn.Text = this.Ftir.ExtractionVolumn;

                tb_m_specification tem = new tb_m_specification().SelectByID(Convert.ToInt32(this.Ftir.specification_id));
                if (tem != null)
                {
                    lbDocRev.Text = tem.C;
                    lbDesc.Text = tem.B;


                    lbB32.Text = String.Empty;
                    lbB33.Text = tem.E;
                    lbB34.Text = tem.F;
                    lbB35.Text = String.Empty;
                    lbB36.Text = String.Empty;

                    lbB29Spec.Text = tem.I;
                    lbB30Spec.Text = tem.J;
                    lbB31Spec.Text = String.Empty;
                    lbB32Spec.Text = String.Empty;


                    ddlSpecification.SelectedValue = tem.ID.ToString();
                }



                ddlUnit.SelectedValue = this.Ftir.selected_unit.Value.ToString();
                ddlNvrUnit.SelectedValue = this.Ftir.selected_unit_nvr.Value.ToString();


                lbUnitNvr.Text = ddlNvrUnit.SelectedItem.Text;
                lbUnitNvr_1.Text = lbUnitNvr.Text;

                lbUnitFtir.Text = ddlUnit.SelectedItem.Text;
                lbUnitFtir_1.Text = lbUnitFtir.Text;

                ShowItem(this.Ftir.item_visible);





                CheckBox1.Checked = false;
                CheckBox2.Checked = false;
                CheckBox3.Checked = false;
                CheckBox4.Checked = false;
                CheckBox5.Checked = false;
                CheckBox6.Checked = false;
                switch (this.Ftir.selected_method)
                {
                    case 0:
                        CheckBox1.Checked = true;

                        txtProcedureNo.ReadOnly = true;
                        txtNomOfPiece.ReadOnly = true;
                        txtExtractionMedium.ReadOnly = true;
                        txtExtractionVolumn.ReadOnly = true;

                        txtProcedureNo0.ReadOnly = true;
                        txtNomOfPiece0.ReadOnly = true;
                        txtExtractionMedium0.ReadOnly = true;
                        txtExtractionVolumn0.ReadOnly = true;

                        txtProcedureNo1.ReadOnly = true;
                        txtNomOfPiece1.ReadOnly = true;
                        txtExtractionMedium1.ReadOnly = true;
                        txtExtractionVolumn1.ReadOnly = true;

                        txtProcedureNo2.ReadOnly = true;
                        txtNomOfPiece2.ReadOnly = true;
                        txtExtractionMedium2.ReadOnly = true;
                        txtExtractionVolumn2.ReadOnly = true;

                        txtProcedureNo3.ReadOnly = true;
                        txtNomOfPiece3.ReadOnly = true;
                        txtExtractionMedium3.ReadOnly = true;
                        txtExtractionVolumn3.ReadOnly = true;

                        txtProcedureNo4.ReadOnly = true;
                        txtNomOfPiece4.ReadOnly = true;
                        txtExtractionMedium4.ReadOnly = true;
                        txtExtractionVolumn4.ReadOnly = true;
                        break;
                    case 1:
                        CheckBox1.Checked = true;

                        txtProcedureNo.ReadOnly = true;
                        txtNomOfPiece.ReadOnly = true;
                        txtExtractionMedium.ReadOnly = true;
                        txtExtractionVolumn.ReadOnly = true;

                        txtProcedureNo0.ReadOnly = false;
                        txtNomOfPiece0.ReadOnly = false;
                        txtExtractionMedium0.ReadOnly = false;
                        txtExtractionVolumn0.ReadOnly = false;

                        txtProcedureNo1.ReadOnly = false;
                        txtNomOfPiece1.ReadOnly = false;
                        txtExtractionMedium1.ReadOnly = false;
                        txtExtractionVolumn1.ReadOnly = false;

                        txtProcedureNo2.ReadOnly = false;
                        txtNomOfPiece2.ReadOnly = false;
                        txtExtractionMedium2.ReadOnly = false;
                        txtExtractionVolumn2.ReadOnly = false;

                        txtProcedureNo3.ReadOnly = false;
                        txtNomOfPiece3.ReadOnly = false;
                        txtExtractionMedium3.ReadOnly = false;
                        txtExtractionVolumn3.ReadOnly = false;

                        txtProcedureNo4.ReadOnly = false;
                        txtNomOfPiece4.ReadOnly = false;
                        txtExtractionMedium4.ReadOnly = false;
                        txtExtractionVolumn4.ReadOnly = false;

                        break;
                    case 2:
                        CheckBox2.Checked = true;

                        txtProcedureNo.ReadOnly = false;
                        txtNomOfPiece.ReadOnly = false;
                        txtExtractionMedium.ReadOnly = false;
                        txtExtractionVolumn.ReadOnly = false;

                        txtProcedureNo0.ReadOnly = true;
                        txtNomOfPiece0.ReadOnly = true;
                        txtExtractionMedium0.ReadOnly = true;
                        txtExtractionVolumn0.ReadOnly = true;

                        txtProcedureNo1.ReadOnly = false;
                        txtNomOfPiece1.ReadOnly = false;
                        txtExtractionMedium1.ReadOnly = false;
                        txtExtractionVolumn1.ReadOnly = false;

                        txtProcedureNo2.ReadOnly = false;
                        txtNomOfPiece2.ReadOnly = false;
                        txtExtractionMedium2.ReadOnly = false;
                        txtExtractionVolumn2.ReadOnly = false;

                        txtProcedureNo3.ReadOnly = false;
                        txtNomOfPiece3.ReadOnly = false;
                        txtExtractionMedium3.ReadOnly = false;
                        txtExtractionVolumn3.ReadOnly = false;

                        txtProcedureNo4.ReadOnly = false;
                        txtNomOfPiece4.ReadOnly = false;
                        txtExtractionMedium4.ReadOnly = false;
                        txtExtractionVolumn4.ReadOnly = false;
                        break;
                    case 3:
                        CheckBox3.Checked = true;

                        txtProcedureNo.ReadOnly = false;
                        txtNomOfPiece.ReadOnly = false;
                        txtExtractionMedium.ReadOnly = false;
                        txtExtractionVolumn.ReadOnly = false;

                        txtProcedureNo0.ReadOnly = false;
                        txtNomOfPiece0.ReadOnly = false;
                        txtExtractionMedium0.ReadOnly = false;
                        txtExtractionVolumn0.ReadOnly = false;

                        txtProcedureNo1.ReadOnly = true;
                        txtNomOfPiece1.ReadOnly = true;
                        txtExtractionMedium1.ReadOnly = true;
                        txtExtractionVolumn1.ReadOnly = true;

                        txtProcedureNo2.ReadOnly = false;
                        txtNomOfPiece2.ReadOnly = false;
                        txtExtractionMedium2.ReadOnly = false;
                        txtExtractionVolumn2.ReadOnly = false;

                        txtProcedureNo3.ReadOnly = false;
                        txtNomOfPiece3.ReadOnly = false;
                        txtExtractionMedium3.ReadOnly = false;
                        txtExtractionVolumn3.ReadOnly = false;

                        txtProcedureNo4.ReadOnly = false;
                        txtNomOfPiece4.ReadOnly = false;
                        txtExtractionMedium4.ReadOnly = false;
                        txtExtractionVolumn4.ReadOnly = false;
                        break;
                    case 4:
                        CheckBox4.Checked = true;

                        txtProcedureNo.ReadOnly = false;
                        txtNomOfPiece.ReadOnly = false;
                        txtExtractionMedium.ReadOnly = false;
                        txtExtractionVolumn.ReadOnly = false;

                        txtProcedureNo0.ReadOnly = false;
                        txtNomOfPiece0.ReadOnly = false;
                        txtExtractionMedium0.ReadOnly = false;
                        txtExtractionVolumn0.ReadOnly = false;

                        txtProcedureNo1.ReadOnly = false;
                        txtNomOfPiece1.ReadOnly = false;
                        txtExtractionMedium1.ReadOnly = false;
                        txtExtractionVolumn1.ReadOnly = false;

                        txtProcedureNo2.ReadOnly = true;
                        txtNomOfPiece2.ReadOnly = true;
                        txtExtractionMedium2.ReadOnly = true;
                        txtExtractionVolumn2.ReadOnly = true;

                        txtProcedureNo3.ReadOnly = false;
                        txtNomOfPiece3.ReadOnly = false;
                        txtExtractionMedium3.ReadOnly = false;
                        txtExtractionVolumn3.ReadOnly = false;

                        txtProcedureNo4.ReadOnly = false;
                        txtNomOfPiece4.ReadOnly = false;
                        txtExtractionMedium4.ReadOnly = false;
                        txtExtractionVolumn4.ReadOnly = false;
                        break;
                    case 5:
                        CheckBox5.Checked = true;

                        txtProcedureNo.ReadOnly = false;
                        txtNomOfPiece.ReadOnly = false;
                        txtExtractionMedium.ReadOnly = false;
                        txtExtractionVolumn.ReadOnly = false;

                        txtProcedureNo0.ReadOnly = false;
                        txtNomOfPiece0.ReadOnly = false;
                        txtExtractionMedium0.ReadOnly = false;
                        txtExtractionVolumn0.ReadOnly = false;

                        txtProcedureNo1.ReadOnly = false;
                        txtNomOfPiece1.ReadOnly = false;
                        txtExtractionMedium1.ReadOnly = false;
                        txtExtractionVolumn1.ReadOnly = false;

                        txtProcedureNo2.ReadOnly = false;
                        txtNomOfPiece2.ReadOnly = false;
                        txtExtractionMedium2.ReadOnly = false;
                        txtExtractionVolumn2.ReadOnly = false;

                        txtProcedureNo3.ReadOnly = true;
                        txtNomOfPiece3.ReadOnly = true;
                        txtExtractionMedium3.ReadOnly = true;
                        txtExtractionVolumn3.ReadOnly = true;

                        txtProcedureNo4.ReadOnly = false;
                        txtNomOfPiece4.ReadOnly = false;
                        txtExtractionMedium4.ReadOnly = false;
                        txtExtractionVolumn4.ReadOnly = false;
                        break;
                    case 6:
                        CheckBox6.Checked = true;
                        txtProcedureNo.ReadOnly = false;
                        txtNomOfPiece.ReadOnly = false;
                        txtExtractionMedium.ReadOnly = false;
                        txtExtractionVolumn.ReadOnly = false;

                        txtProcedureNo0.ReadOnly = false;
                        txtNomOfPiece0.ReadOnly = false;
                        txtExtractionMedium0.ReadOnly = false;
                        txtExtractionVolumn0.ReadOnly = false;

                        txtProcedureNo1.ReadOnly = false;
                        txtNomOfPiece1.ReadOnly = false;
                        txtExtractionMedium1.ReadOnly = false;
                        txtExtractionVolumn1.ReadOnly = false;

                        txtProcedureNo2.ReadOnly = false;
                        txtNomOfPiece2.ReadOnly = false;
                        txtExtractionMedium2.ReadOnly = false;
                        txtExtractionVolumn2.ReadOnly = false;

                        txtProcedureNo3.ReadOnly = false;
                        txtNomOfPiece3.ReadOnly = false;
                        txtExtractionMedium3.ReadOnly = false;
                        txtExtractionVolumn3.ReadOnly = false;

                        txtProcedureNo4.ReadOnly = true;
                        txtNomOfPiece4.ReadOnly = true;
                        txtExtractionMedium4.ReadOnly = true;
                        txtExtractionVolumn4.ReadOnly = true;
                        break;

                }

                if (this.Ftir.selected_method == 0)
                {
                    tab1_tr1.Visible = true;
                    tab1_tr2.Visible = true;
                    tab1_tr3.Visible = true;
                    tab1_tr4.Visible = true;
                    tab1_tr5.Visible = true;
                    tab1_tr6.Visible = true;
                }
                else
                {
                    tab1_tr1.Visible = CheckBox1.Checked;
                    tab1_tr2.Visible = CheckBox2.Checked;
                    tab1_tr3.Visible = CheckBox3.Checked;
                    tab1_tr4.Visible = CheckBox4.Checked;
                    tab1_tr5.Visible = CheckBox5.Checked;
                    tab1_tr6.Visible = CheckBox6.Checked;
                }

                CalculateCas();
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;
                this.Ftir = new template_seagate_ftir_coverpage();


            }
            #endregion

            //initial component
            btnCoverPage.CssClass = "btn green";
            btnWorkingFTIR.CssClass = "btn blue";
            btnWorkingNVR.CssClass = "btn blue";

            pCoverPage.Visible = true;
            PWorking.Visible = false;
            PNvr.Visible = false;
            pLoadFile.Visible = false;

        }

        #endregion

        List<String> errors = new List<string>();


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

        protected void btnWorkingFTIR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnCoverPage":
                    btnCoverPage.CssClass = "btn green";
                    btnWorkingFTIR.CssClass = "btn blue";
                    btnWorkingNVR.CssClass = "btn blue";
                    pCoverPage.Visible = true;
                    PWorking.Visible = false;
                    PNvr.Visible = false;
                    pLoadFile.Visible = false;

                    lbC32.Text = Constants.GetEnumDescription(ResultEnum.NOT_DETECTED);
                    lbC33.Text = lbWE26.Text;// this.Ftir.w_e26;
                    lbC34.Text = Constants.GetEnumDescription(ResultEnum.NOT_DETECTED);
                    lbC35.Text = lbWD26.Text;// this.Ftir.w_d26;
                    lbC36.Text = lbWC26.Text;// this.Ftir.w_c26;


                    CalculateCas();

                    break;
                case "btnWorkingFTIR":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkingFTIR.CssClass = "btn green";
                    btnWorkingNVR.CssClass = "btn blue";
                    pCoverPage.Visible = false;
                    PWorking.Visible = true;
                    PNvr.Visible = false;
                    pLoadFile.Visible = true;

                    break;
                case "btnWorkingNVR":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkingFTIR.CssClass = "btn blue";
                    btnWorkingNVR.CssClass = "btn green";
                    pCoverPage.Visible = false;
                    PWorking.Visible = false;
                    PNvr.Visible = true;
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
                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.Ftir.sample_id = this.SampleID;
                    this.Ftir.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                    this.Ftir.item_visible = getItemStatus();


                    if (CheckBox1.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn.Text;
                        this.Ftir.selected_method = 1;
                    }

                    if (CheckBox2.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo0.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece0.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium0.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn0.Text;
                        this.Ftir.selected_method = 2;
                    }

                    if (CheckBox3.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo1.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece1.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium1.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn1.Text;
                        this.Ftir.selected_method = 3;
                    }

                    if (CheckBox4.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo2.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece2.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium2.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn2.Text;
                        this.Ftir.selected_method = 4;
                    }

                    if (CheckBox5.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo3.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece3.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium3.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn3.Text;
                        this.Ftir.selected_method = 5;
                    }

                    if (CheckBox6.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo4.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece4.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium4.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn4.Text;
                        this.Ftir.selected_method = 6;
                    }


                    this.Ftir.selected_method = (CheckBox1.Checked && CheckBox2.Checked && CheckBox3.Checked && CheckBox4.Checked && CheckBox5.Checked && CheckBox6.Checked) ? 0 : this.Ftir.selected_method;
                    this.Ftir.selected_unit = 1;
                    this.Ftir.selected_unit_nvr = 1;
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            Ftir.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            Ftir.Update();
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    #region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_test_completed = DateTime.Now;
                    #endregion

                    if (CheckBox1.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn.Text;
                        this.Ftir.selected_method = 1;
                    }

                    if (CheckBox2.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo0.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece0.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium0.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn0.Text;
                        this.Ftir.selected_method = 2;
                    }

                    if (CheckBox3.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo1.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece1.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium1.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn1.Text;
                        this.Ftir.selected_method = 3;
                    }

                    if (CheckBox4.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo2.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece2.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium2.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn2.Text;
                        this.Ftir.selected_method = 4;
                    }

                    if (CheckBox5.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo3.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece3.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium3.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn3.Text;
                        this.Ftir.selected_method = 5;
                    }

                    if (CheckBox6.Checked)
                    {
                        this.Ftir.ProcedureNo = txtProcedureNo4.Text;
                        this.Ftir.NumOfPiece = txtNomOfPiece4.Text;
                        this.Ftir.ExtractionMedium = txtExtractionMedium4.Text;
                        this.Ftir.ExtractionVolumn = txtExtractionVolumn4.Text;
                        this.Ftir.selected_method = 6;
                    }

                    this.Ftir.selected_unit = Convert.ToInt32(ddlUnit.SelectedValue);
                    this.Ftir.selected_unit_nvr = Convert.ToInt32(ddlNvrUnit.SelectedValue);
                    this.Ftir.furmular_1 = Label1.Text;
                    this.Ftir.furmular_2 = Label2.Text;
                    this.Ftir.furmular_3 = Label3.Text;
                    this.Ftir.furmular_4 = Label4.Text;
                    #region "working-FTIR"
                    this.Ftir.w_b13 = txtWB13.Text;
                    this.Ftir.w_b14 = txtWB14.Text;
                    this.Ftir.w_b15 = txtWB15.Text;

                    this.Ftir.w_b20 = txtWB20.Text;
                    this.Ftir.w_c20 = txtWC20.Text;
                    this.Ftir.w_d20 = txtWD20.Text;
                    this.Ftir.w_e20 = txtWE20.Text;

                    this.Ftir.w_b21 = txtWB21.Text;
                    this.Ftir.w_c21 = txtWC21.Text;
                    this.Ftir.w_d21 = txtWD21.Text;
                    this.Ftir.w_e21 = txtWE21.Text;

                    this.Ftir.w_b22 = txtWB22.Text;
                    this.Ftir.w_c22 = txtWC22.Text;
                    this.Ftir.w_d22 = txtWD22.Text;
                    this.Ftir.w_e22 = txtWE22.Text;

                    this.Ftir.w_b24 = txtWB24.Text;
                    this.Ftir.w_c24 = txtWC24.Text;
                    this.Ftir.w_d24 = txtWD24.Text;
                    this.Ftir.w_e24 = txtWE24.Text;

                    this.Ftir.w_b26 = lbWB26.Text;
                    this.Ftir.w_c26 = lbWC26.Text;
                    this.Ftir.w_d26 = lbWD26.Text;
                    this.Ftir.w_e26 = lbWE26.Text;

                    this.Ftir.w_b27 = lbWB27.Text;
                    this.Ftir.w_c27 = lbWC27.Text;
                    this.Ftir.w_d27 = lbWD27.Text;
                    this.Ftir.w_e27 = lbWE27.Text;
                    #endregion

                    #region "NVR"


                    this.Ftir.nvrb16 = nvrB16.Text;
                    this.Ftir.nvrb17 = nvrB17.Text;
                    this.Ftir.nvrb18 = nvrB18.Text;
                    this.Ftir.nvrb20 = nvrB20.Text;
                    this.Ftir.nvrb21 = nvrB21.Text;
                    this.Ftir.nvrb22 = nvrB22.Text;
                    this.Ftir.nvrb24 = nvrB24.Text;
                    this.Ftir.nvrb25 = nvrB25.Text;
                    this.Ftir.nvrb26 = nvrB26.Text;
                    this.Ftir.nvrb28 = nvrB28.Text;
                    this.Ftir.nvrb29 = nvrB29.Text;
                    this.Ftir.nvrb30 = nvrB30.Text;
                    this.Ftir.nvrb32 = nvrB32.Text;
                    this.Ftir.nvrb33 = nvrB33.Text;
                    this.Ftir.nvrb34 = nvrB34.Text;
                    this.Ftir.nvrb36 = nvrB36.Text;
                    //this.Ftir.nvrb37 = nvrB37.Text;


                    this.Ftir.nvrc16 = nvrC16.Text;
                    this.Ftir.nvrc17 = nvrC17.Text;
                    this.Ftir.nvrc18 = nvrC18.Text;
                    this.Ftir.nvrc20 = nvrC20.Text;
                    this.Ftir.nvrc21 = nvrC21.Text;
                    this.Ftir.nvrc22 = nvrC22.Text;
                    this.Ftir.nvrc24 = nvrC24.Text;
                    this.Ftir.nvrc25 = nvrC25.Text;
                    this.Ftir.nvrc26 = nvrC26.Text;
                    this.Ftir.nvrc28 = nvrC28.Text;
                    this.Ftir.nvrc29 = nvrC29.Text;
                    this.Ftir.nvrc30 = nvrC30.Text;
                    this.Ftir.nvrc32 = nvrC32.Text;
                    this.Ftir.nvrc33 = nvrC33.Text;
                    this.Ftir.nvrc34 = nvrC34.Text;
                    this.Ftir.nvrc36 = nvrC36.Text;
                    this.Ftir.nvrc37 = nvrC37.Text;

                    #endregion
                    this.Ftir.item_visible = getItemStatus();
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
                            break;
                    }
                    this.jobSample.step3owner = userLogin.id;

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
                        lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.jobSample.step4owner = userLogin.id;
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
                        lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;

            }
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                //########
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

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

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (tem != null)
            {
                lbDocRev.Text = tem.C;
                lbDesc.Text = tem.B;

                lbB32.Text = String.Empty;
                lbB33.Text = tem.F;
                lbB34.Text = tem.E;
                lbB35.Text = String.Empty;
                lbB36.Text = String.Empty;

                lbB29Spec.Text = tem.I;
                lbB30Spec.Text = tem.J;
                lbB31Spec.Text = String.Empty;
                lbB32Spec.Text = String.Empty;

            }
        }

        #region "Custom method"

        private void CalculateCas()
        {

            //Map FTIR to Cover Page
            lbC32.Text = String.Empty;// ddlUnit.SelectedValue.Equals("1") ? lbWB26.Text : lbWB27.Text;//Silicone
            lbC33.Text = ddlUnit.SelectedValue.Equals("1") ? lbWE26.Text : lbWE27.Text;//xHydrocarbon
            lbC34.Text = ddlUnit.SelectedValue.Equals("1") ? lbWB26.Text : lbWB27.Text;//xSilicone Oil
            lbC35.Text = ddlUnit.SelectedValue.Equals("1") ? lbWD26.Text : lbWD27.Text;//Phthalate
            lbC36.Text = ddlUnit.SelectedValue.Equals("1") ? lbWC26.Text : lbWC27.Text;//Amide

            //Map NVR to Cover Page
            lbB29Result.Text = ddlNvrUnit.SelectedValue.Equals("1") ? nvrC36.Text : nvrC37.Text;
            lbB30Result.Text = ddlNvrUnit.SelectedValue.Equals("1") ? nvrC36.Text : nvrC37.Text;
            lbB31Result.Text = ddlNvrUnit.SelectedValue.Equals("1") ? nvrC36.Text : nvrC37.Text;
            lbB32Result.Text = ddlNvrUnit.SelectedValue.Equals("1") ? nvrC36.Text : nvrC37.Text;

            lbC32.Text = CustomUtils.isNumber(lbC32.Text) ? Convert.ToDouble(lbC32.Text).ToString("N3") : lbC32.Text;
            lbC33.Text = CustomUtils.isNumber(lbC33.Text) ? Convert.ToDouble(lbC33.Text).ToString("N3") : lbC33.Text;
            lbC34.Text = CustomUtils.isNumber(lbC34.Text) ? Convert.ToDouble(lbC34.Text).ToString("N3") : lbC34.Text;
            lbC35.Text = CustomUtils.isNumber(lbC35.Text) ? Convert.ToDouble(lbC35.Text).ToString("N3") : lbC35.Text;
            lbC36.Text = CustomUtils.isNumber(lbC36.Text) ? Convert.ToDouble(lbC36.Text).ToString("N3") : lbC36.Text;

            lbB29Result.Text = CustomUtils.isNumber(lbB29Result.Text) ? Convert.ToDouble(lbB29Result.Text).ToString("N3") : lbB29Result.Text;
            lbB30Result.Text = CustomUtils.isNumber(lbB30Result.Text) ? Convert.ToDouble(lbB30Result.Text).ToString("N3") : lbB30Result.Text;
            lbB31Result.Text = CustomUtils.isNumber(lbB31Result.Text) ? Convert.ToDouble(lbB31Result.Text).ToString("N3") : lbB31Result.Text;
            lbB32Result.Text = CustomUtils.isNumber(lbB32Result.Text) ? Convert.ToDouble(lbB32Result.Text).ToString("N3") : lbB32Result.Text;


            lbUnitNvr.Text = ddlNvrUnit.SelectedItem.Text;
            lbUnitNvr_1.Text = lbUnitNvr.Text;

            lbUnitFtir.Text = ddlUnit.SelectedItem.Text;
            lbUnitFtir_1.Text = lbUnitFtir.Text;
            if (!String.IsNullOrEmpty(txtWB24.Text) && !string.IsNullOrEmpty(lbWB25.Text))
            {
                lbA42.Text = ddlUnit.SelectedValue.Equals("1") ? Math.Round(Convert.ToDouble(txtWB24.Text), 7).ToString() : Math.Round(Convert.ToDouble(lbWB25.Text), 7).ToString();
            }
            btnSubmit.Enabled = true;
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
            row["ProcedureNo"] = this.Ftir.ProcedureNo;
            row["NumOfPiecesUsedForExtraction"] = this.Ftir.NumOfPiece;
            row["ExtractionMedium"] = this.Ftir.ExtractionMedium;
            row["ExtractionVolume"] = this.Ftir.ExtractionVolumn;
            dtHeader.Rows.Add(row);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            List<ReportData> reportNVRList = new List<ReportData>();
            List<ReportData> reportFTIRList = new List<ReportData>();
            //Create ReportData NVR
            ReportData tmp = new ReportData
            {
                A = "NVR (DI Water)",
                B = lbB29Spec.Text,
                C = lbB29Result.Text
            };
            if (item[1] == '1')
            {
                reportNVRList.Add(tmp);
            }
            tmp = new ReportData
            {
                A = "NVR (IPA/Hexane)",
                B = lbB30Spec.Text,
                C = lbB30Result.Text
            };
            if (item[2] == '1')
            {
                reportNVRList.Add(tmp);
            }
            tmp = new ReportData
            {
                A = "NVR (IPA)",
                B = lbB31Spec.Text,
                C = lbB31Result.Text
            };
            if (item[3] == '1')
            {
                reportNVRList.Add(tmp);
            }
            tmp = new ReportData
            {
                A = "NVR (Acetone)",
                B = lbB31Spec.Text,
                C = lbB31Result.Text
            };
            if (item[4] == '1')
            {
                reportNVRList.Add(tmp);
            }




            //Create ReportData FTIR
            tmp = new ReportData
            {
                A = "Silicone",
                B = lbB32.Text,
                C = lbC32.Text
            };
            if (item[6] == '1')
            {
                reportFTIRList.Add(tmp);
            }
            tmp = new ReportData
            {
                A = "Silicone Oil",
                B = lbB33.Text,
                C = lbC33.Text
            };
            if (item[7] == '1')
            {
                reportFTIRList.Add(tmp);
            } tmp = new ReportData
            {
                A = "Hydrocarbon",
                B = lbB34.Text,
                C = lbC34.Text
            };
            if (item[8] == '1')
            {
                reportFTIRList.Add(tmp);
            } tmp = new ReportData
            {
                A = "Phthalate",
                B = lbB35.Text,
                C = lbC35.Text
            };
            if (item[9] == '1')
            {
                reportFTIRList.Add(tmp);
            } tmp = new ReportData
            {
                A = "Amides",
                B = lbB36.Text,
                C = lbC36.Text
            };
            if (item[10] == '1')
            {
                reportFTIRList.Add(tmp);
            }


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", String.IsNullOrEmpty(reportHeader.cusRefNo) ? "-" : reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1 + reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", MethodType[this.Ftir.selected_method.Value - 1]));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Seagate's Doc {0} for {1}", lbDocRev.Text, lbDesc.Text)));
            reportParameters.Add(new ReportParameter("Remarks", String.Format("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is {0}", lbA42.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath(MethodType[this.Ftir.selected_method.Value - 1].EndsWith("NVR") ? "~/ReportObject/ftir_nvr_seagate.rdlc" : "~/ReportObject/ftir_seagate.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", reportNVRList.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", reportFTIRList.ToDataTable())); // Add datasource here



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

        private String getItemStatus()
        {
            String result = String.Empty;
            result =
                ((CheckBox14.Checked) ? "1" : "0") +
                ((CheckBox7.Checked) ? "1" : "0") +
                ((CheckBox13.Checked) ? "1" : "0") +
                ((CheckBox16.Checked) ? "1" : "0") +
                ((CheckBox17.Checked) ? "1" : "0") +


                ((CheckBox15.Checked) ? "1" : "0") +
                ((CheckBox8.Checked) ? "1" : "0") +
                ((CheckBox9.Checked) ? "1" : "0") +
                ((CheckBox10.Checked) ? "1" : "0") +
                ((CheckBox11.Checked) ? "1" : "0") +
                ((CheckBox12.Checked) ? "1" : "0");

            return result;
        }

        private void ShowItem(String _itemVisible)
        {
            if (_itemVisible != null)
            {
                char[] item = _itemVisible.ToCharArray();
                if (item.Length == 11)
                {

                    StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                    switch (status)
                    {
                        case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            break;
                        case StatusEnum.CHEMIST_TESTING:
                            CheckBox14.Visible = true;
                            CheckBox7.Visible = true;
                            CheckBox13.Visible = true;
                            CheckBox16.Visible = true;
                            CheckBox17.Visible = true;


                            CheckBox15.Visible = true;
                            CheckBox8.Visible = true;
                            CheckBox9.Visible = true;
                            CheckBox10.Visible = true;
                            CheckBox11.Visible = true;
                            CheckBox12.Visible = true;

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
                            CheckBox14.Visible = false;
                            CheckBox7.Visible = false;
                            CheckBox13.Visible = false;
                            CheckBox16.Visible = false;
                            CheckBox17.Visible = false;

                            CheckBox15.Visible = false;
                            CheckBox8.Visible = false;
                            CheckBox9.Visible = false;
                            CheckBox10.Visible = false;
                            CheckBox11.Visible = false;
                            CheckBox12.Visible = false;

                            break;
                    }

                    CheckBox14.Checked = item[0] == '1' ? true : false;
                    CheckBox7.Checked = item[1] == '1' ? true : false;
                    CheckBox13.Checked = item[2] == '1' ? true : false;
                    CheckBox16.Checked = item[3] == '1' ? true : false;
                    CheckBox17.Checked = item[4] == '1' ? true : false;

                    CheckBox15.Checked = item[5] == '1' ? true : false;
                    CheckBox8.Checked = item[6] == '1' ? true : false;
                    CheckBox9.Checked = item[7] == '1' ? true : false;
                    CheckBox10.Checked = item[8] == '1' ? true : false;
                    CheckBox11.Checked = item[9] == '1' ? true : false;
                    CheckBox12.Checked = item[10] == '1' ? true : false;


                    Tr28.Visible = CheckBox14.Checked;
                    Tr29.Visible = CheckBox7.Checked;
                    Tr30.Visible = CheckBox13.Checked;
                    Tr31.Visible = CheckBox16.Checked;
                    Tr32.Visible = CheckBox17.Checked;

                    tab2_tr0.Visible = CheckBox15.Checked;
                    tab2_tr1.Visible = CheckBox8.Checked;
                    tab2_tr2.Visible = CheckBox9.Checked;
                    tab2_tr3.Visible = CheckBox10.Checked;
                    tab2_tr4.Visible = CheckBox11.Checked;
                    tab2_tr5.Visible = CheckBox12.Checked;
                }
            }

        }
        #endregion


        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            String sheetName = string.Empty;

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
                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.ftir.excel.sheetname.working1"]);
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.ftir.excel.sheetname.working1"]));
                                }
                                else
                                {
                                    if (isheet != null)
                                    {

                                        sheetName = isheet.SheetName;

                                        Label1.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.B));
                                        Label2.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.C));
                                        Label3.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.D));
                                        Label4.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.E));

                                        txtWB13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B));//Surface area
                                        txtWB14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));//No of part
                                        txtWB15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));//Total surface area

                                        #region "working-FTIR"

                                        txtWB20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                        txtWC20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                        txtWD20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                        txtWE20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        txtWB20.Text = txtWB20.Text.Equals("< IDL") ? "< IDL" : txtWB20.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWB20.Text) ? "" : Math.Round(Convert.ToDecimal(txtWB20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                        txtWC20.Text = txtWC20.Text.Equals("< IDL") ? "< IDL" : txtWC20.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWC20.Text) ? "" : Math.Round(Convert.ToDecimal(txtWC20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                        txtWD20.Text = txtWD20.Text.Equals("< IDL") ? "< IDL" : txtWD20.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWD20.Text) ? "" : Math.Round(Convert.ToDecimal(txtWD20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                        txtWE20.Text = txtWE20.Text.Equals("< IDL") ? "< IDL" : txtWE20.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWE20.Text) ? "" : Math.Round(Convert.ToDecimal(txtWE20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";


                                        txtWB21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                        txtWC21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));
                                        txtWD21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.D));
                                        txtWE21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        txtWB21.Text = txtWB21.Text.Equals("< IDL") ? "< IDL" : txtWB21.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWB21.Text) ? "" : Math.Round(Convert.ToDecimal(txtWB21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                        txtWC21.Text = txtWC21.Text.Equals("< IDL") ? "< IDL" : txtWC21.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWC21.Text) ? "" : Math.Round(Convert.ToDecimal(txtWC21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                        txtWD21.Text = txtWD21.Text.Equals("< IDL") ? "< IDL" : txtWD21.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWD21.Text) ? "" : Math.Round(Convert.ToDecimal(txtWD21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                        txtWE21.Text = txtWE21.Text.Equals("< IDL") ? "< IDL" : txtWE21.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWE21.Text) ? "" : Math.Round(Convert.ToDecimal(txtWE21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";

                                        txtWB22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.B));
                                        txtWC22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.C));
                                        txtWD22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.D));
                                        txtWE22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        txtWB22.Text = txtWB22.Text.Equals("< IDL") ? "< IDL" : txtWB22.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWB22.Text) ? "" : Math.Round(Convert.ToDecimal(txtWB22.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                        txtWC22.Text = txtWC22.Text.Equals("< IDL") ? "< IDL" : txtWC22.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWC22.Text) ? "" : Math.Round(Convert.ToDecimal(txtWC22.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                        txtWD22.Text = txtWD22.Text.Equals("< IDL") ? "< IDL" : txtWD22.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWD22.Text) ? "" : Math.Round(Convert.ToDecimal(txtWD22.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                        txtWE22.Text = txtWE22.Text.Equals("< IDL") ? "< IDL" : txtWE22.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWE22.Text) ? "" : Math.Round(Convert.ToDecimal(txtWE22.Text), Convert.ToInt16(txtDecimal03.Text)) + "";

                                        lbWB23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B));
                                        lbWC23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C));
                                        lbWD23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.D));
                                        lbWE23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        lbWB23.Text = lbWB23.Text.Equals("< IDL") ? "< IDL" : lbWB23.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWB23.Text) ? "" : Math.Round(Convert.ToDecimal(lbWB23.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                        lbWC23.Text = lbWC23.Text.Equals("< IDL") ? "< IDL" : lbWC23.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWC23.Text) ? "" : Math.Round(Convert.ToDecimal(lbWC23.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                        lbWD23.Text = lbWD23.Text.Equals("< IDL") ? "< IDL" : lbWD23.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWD23.Text) ? "" : Math.Round(Convert.ToDecimal(lbWD23.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                        lbWE23.Text = lbWE23.Text.Equals("< IDL") ? "< IDL" : lbWE23.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWE23.Text) ? "" : Math.Round(Convert.ToDecimal(lbWE23.Text), Convert.ToInt16(txtDecimal04.Text)) + "";

                                        txtWB24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));
                                        txtWC24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));
                                        txtWD24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.D));
                                        txtWE24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        txtWB24.Text = txtWB24.Text.Equals("< IDL") ? "< IDL" : txtWB24.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWB24.Text) ? "" : Math.Round(Convert.ToDecimal(txtWB24.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        txtWC24.Text = txtWC24.Text.Equals("< IDL") ? "< IDL" : txtWC24.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWC24.Text) ? "" : Math.Round(Convert.ToDecimal(txtWC24.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        txtWD24.Text = txtWD24.Text.Equals("< IDL") ? "< IDL" : txtWD24.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWD24.Text) ? "" : Math.Round(Convert.ToDecimal(txtWD24.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        txtWE24.Text = txtWE24.Text.Equals("< IDL") ? "< IDL" : txtWE24.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(txtWE24.Text) ? "" : Math.Round(Convert.ToDecimal(txtWE24.Text), Convert.ToInt16(txtDecimal05.Text)) + "";

                                        lbWB25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));
                                        lbWC25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));
                                        lbWD25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.D));
                                        lbWE25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        lbWB25.Text = lbWB25.Text.Equals("< IDL") ? "< IDL" : lbWB25.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWB25.Text) ? "" : Math.Round(Convert.ToDecimal(lbWB25.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        lbWC25.Text = lbWC25.Text.Equals("< IDL") ? "< IDL" : lbWC25.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWC25.Text) ? "" : Math.Round(Convert.ToDecimal(lbWC25.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        lbWD25.Text = lbWD25.Text.Equals("< IDL") ? "< IDL" : lbWD25.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWD25.Text) ? "" : Math.Round(Convert.ToDecimal(lbWD25.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                        lbWE25.Text = lbWE25.Text.Equals("< IDL") ? "< IDL" : lbWE25.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWE25.Text) ? "" : Math.Round(Convert.ToDecimal(lbWE25.Text), Convert.ToInt16(txtDecimal05.Text)) + "";

                                        lbWB26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));
                                        lbWC26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));
                                        lbWD26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.D));
                                        lbWE26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.E));
                                        //Decimal
                                        lbWB26.Text = lbWB26.Text.Equals("< IDL") ? "< IDL" : lbWB26.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWB26.Text) ? "" : Math.Round(Convert.ToDecimal(lbWB26.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        lbWC26.Text = lbWC26.Text.Equals("< IDL") ? "< IDL" : lbWC26.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWC26.Text) ? "" : Math.Round(Convert.ToDecimal(lbWC26.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        lbWD26.Text = lbWD26.Text.Equals("< IDL") ? "< IDL" : lbWD26.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWD26.Text) ? "" : Math.Round(Convert.ToDecimal(lbWD26.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        lbWE26.Text = lbWE26.Text.Equals("< IDL") ? "< IDL" : lbWE26.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWE26.Text) ? "" : Math.Round(Convert.ToDecimal(lbWE26.Text), Convert.ToInt16(txtDecimal06.Text)) + "";

                                        lbWB27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.B));
                                        lbWC27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C));
                                        lbWD27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.D));
                                        lbWE27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.E));

                                        //lbWC27.Text = lbWC27.Text.Length > 8 ? lbWC27.Text.Substring(0, 8) : lbWC27.Text;//Split over length
                                        //Decimal
                                        lbWB27.Text = lbWB27.Text.Equals("< IDL") ? "< IDL" : lbWB27.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWB27.Text) ? "" : Math.Round(Convert.ToDecimal(lbWB27.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        try
                                        {
                                            lbWC27.Text = lbWC27.Text.Equals("< IDL") ? "< IDL" : lbWC27.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWC27.Text) ? "" : Math.Round(Convert.ToDecimal(lbWC27.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        }
                                        catch (Exception ex)
                                        {
                                            lbWC27.Text = "0.000";
                                        }

                                        lbWD27.Text = lbWD27.Text.Equals("< IDL") ? "< IDL" : lbWD27.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWD27.Text) ? "" : Math.Round(Convert.ToDecimal(lbWD27.Text), Convert.ToInt16(txtDecimal06.Text)) + "";
                                        lbWE27.Text = lbWE27.Text.Equals("< IDL") ? "< IDL" : lbWE27.Text.Equals("Not Detected") ? "Not Detected" : String.IsNullOrEmpty(lbWE27.Text) ? "" : Math.Round(Convert.ToDecimal(lbWE27.Text), Convert.ToInt16(txtDecimal06.Text)) + "";

                                        #endregion
                                    }
                                }
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.ftir.excel.sheetname.working2"]);
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.ftir.excel.sheetname.working2"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    #region "NVR (ppm)"
                                    nvrB16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.B));
                                    nvrC16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.C));

                                    nvrB17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B));
                                    nvrC17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C));

                                    nvrB18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B));
                                    nvrC18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C));

                                    nvrB20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                    nvrC20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));

                                    nvrB21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                    nvrC21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));

                                    nvrB22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.B));
                                    nvrC22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.C));

                                    nvrB24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));
                                    nvrC24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));

                                    nvrB25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));
                                    nvrC25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));

                                    nvrB26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));
                                    nvrC26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));

                                    nvrB28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.B));
                                    nvrC28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.C));

                                    nvrB29.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.B));
                                    nvrC29.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.C));

                                    nvrB30.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                    nvrC30.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.C));

                                    nvrB32.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));
                                    nvrC32.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.C));

                                    nvrB33.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.B));
                                    nvrC33.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.C));

                                    nvrB34.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.B));
                                    nvrC34.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.C));


                                    nvrB36.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.B));
                                    nvrC36.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.C));
                                    nvrC37.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.C));

                                    //Decimal

                                    nvrB16.Text = String.IsNullOrEmpty(nvrB16.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB16.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB17.Text = String.IsNullOrEmpty(nvrB17.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB17.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB18.Text = String.IsNullOrEmpty(nvrB18.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB18.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB20.Text = String.IsNullOrEmpty(nvrB20.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB20.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB21.Text = String.IsNullOrEmpty(nvrB21.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB21.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB22.Text = String.IsNullOrEmpty(nvrB22.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB22.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB24.Text = String.IsNullOrEmpty(nvrB24.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB24.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB25.Text = String.IsNullOrEmpty(nvrB25.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB25.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB26.Text = String.IsNullOrEmpty(nvrB26.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB26.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB28.Text = String.IsNullOrEmpty(nvrB28.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB28.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB29.Text = String.IsNullOrEmpty(nvrB29.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB29.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB30.Text = String.IsNullOrEmpty(nvrB30.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB30.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB32.Text = String.IsNullOrEmpty(nvrB32.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB32.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB33.Text = String.IsNullOrEmpty(nvrB33.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB33.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB34.Text = String.IsNullOrEmpty(nvrB34.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB34.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                    nvrB36.Text = String.IsNullOrEmpty(nvrB36.Text) ? "" : Math.Round(Convert.ToDecimal(nvrB36.Text), Convert.ToInt16(txtDecimal07.Text)) + "";


                                    nvrC16.Text = String.IsNullOrEmpty(nvrC16.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC16.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC17.Text = String.IsNullOrEmpty(nvrC17.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC17.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC18.Text = String.IsNullOrEmpty(nvrC18.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC18.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC20.Text = String.IsNullOrEmpty(nvrC20.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC20.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC21.Text = String.IsNullOrEmpty(nvrC21.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC21.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC22.Text = String.IsNullOrEmpty(nvrC22.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC22.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC24.Text = String.IsNullOrEmpty(nvrC24.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC24.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC25.Text = String.IsNullOrEmpty(nvrC25.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC25.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC26.Text = String.IsNullOrEmpty(nvrC26.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC26.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC28.Text = String.IsNullOrEmpty(nvrC28.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC28.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC29.Text = String.IsNullOrEmpty(nvrC29.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC29.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC30.Text = String.IsNullOrEmpty(nvrC30.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC30.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC32.Text = String.IsNullOrEmpty(nvrC32.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC32.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC33.Text = String.IsNullOrEmpty(nvrC33.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC33.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC34.Text = String.IsNullOrEmpty(nvrC34.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC34.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC36.Text = String.IsNullOrEmpty(nvrC36.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC36.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                    nvrC37.Text = String.IsNullOrEmpty(nvrC37.Text) ? "" : Math.Round(Convert.ToDecimal(nvrC37.Text), Convert.ToInt16(txtDecimal08.Text)) + "";





                                    #endregion

                                }
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

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();

        }

    }
}

