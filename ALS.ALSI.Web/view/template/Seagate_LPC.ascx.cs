using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using WordToPDF;
using ALSALSI.Biz;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_LPC : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_LPC));

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
        public List<template_seagate_lpc_coverpage> Lpcs
        {
            get { return (List<template_seagate_lpc_coverpage>)Session[GetType().Name + "Lpcs"]; }
            set { Session[GetType().Name + "Lpcs"] = value; }
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
            //Session.Remove(GetType().Name + "tbCas");
            //Session.Remove(GetType().Name + "coverpages");
            Session.Remove(GetType().Name + "Lpcs");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        private void initialPage()
        {

            this.CommandName = CommandNameEnum.Add;

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));




            #region "SAMPLE"
            if (this.jobSample != null)
            {
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                lbJobStatus.Text = Constants.GetEnumDescription(status);
                ddlStatus.Items.Clear();

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pSpecification.Visible = false;
                pStatus.Visible = false;
                pUploadfile.Visible = false;
                pDownload.Visible = false;
                btnSubmit.Visible = false;
                //gvCoverPages.Columns[3].Visible = false;
                //gvResult.Columns[8].Visible = false;

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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
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
                            //gvCoverPages.Columns[3].Visible = true;
                            //gvResult.Columns[8].Visible = true;
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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = true;
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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
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
                        if (this.jobSample.date_chemist_alalyze == null)
                        {
                            this.jobSample.date_chemist_alalyze = DateTime.Now;
                            this.jobSample.Update();
                        }
                    }
                    #endregion


                    ddlA19.Enabled = true;
                    txtB19.Enabled = true;
                    txtCVP_C19.Enabled = true;
                    txtD19.Enabled = true;
                    txtCVP_E19.Enabled = true;


                    //td5.Visible = true;

                    //th2.Visible = true;
                    //td6.Visible = true;
                    //td7.Visible = true;
                    //td8.Visible = true;
                    //td9.Visible = true;
                    //td10.Visible = true;

                    //th3.Visible = true;
                    //td11.Visible = true;
                    //td12.Visible = true;
                    //td13.Visible = true;
                    //td14.Visible = true;
                    //td15.Visible = true;

                    //th4.Visible = true;
                    //td16.Visible = true;
                    //td17.Visible = true;
                    //td18.Visible = true;
                    //td19.Visible = true;
                    //td20.Visible = true;

                    btnCoverPage.Visible = true;
                    btnWorkSheet.Visible = true;
                }
                else
                {
                    ddlA19.Enabled = false;
                    txtB19.Enabled = false;
                    txtCVP_C19.Enabled = false;
                    txtD19.Enabled = false;
                    txtCVP_E19.Enabled = false;

                    //th1.Visible = false;
                    //td1.Visible = false;
                    //td2.Visible = false;
                    //td3.Visible = false;
                    //td4.Visible = false;
                    //td5.Visible = false;

                    //th2.Visible = false;
                    //td6.Visible = false;
                    //td7.Visible = false;
                    //td8.Visible = false;
                    //td9.Visible = false;
                    //td10.Visible = false;

                    //th3.Visible = false;
                    //td11.Visible = false;
                    //td12.Visible = false;
                    //td13.Visible = false;
                    //td14.Visible = false;
                    //td15.Visible = false;

                    //th4.Visible = false;
                    //td16.Visible = false;
                    //td17.Visible = false;
                    //td18.Visible = false;
                    //td19.Visible = false;
                    //td20.Visible = false;

                    btnCoverPage.Visible = false;
                    btnWorkSheet.Visible = false;

                }
                #endregion

            }

            #endregion
            #region "WorkingSheet"
            this.Lpcs = template_seagate_lpc_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Lpcs != null && this.Lpcs.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                ddlSpecification.SelectedValue = this.Lpcs[0].specification_id.ToString();
                tb_m_specification tem = new tb_m_specification().SelectByID(Convert.ToInt32(Lpcs[0].specification_id));
                if (tem != null)
                {
                    //#region "HEADER"
                    //lbDocNo.Text = tem.C;
                    //lbDocRev.Text = tem.D;
                    //lbCommodity.Text = tem.B;
                    //lbUnit1.Text = tem.E;//unit
                    //lbUnit2.Text = tem.E;//unit
                    //lbUnit3.Text = tem.E;//unit
                    //lbUnit4.Text = tem.E;//unit
                    //lbUnit5.Text = tem.E;//unit
                    //lbUnit6.Text = tem.E;//unit
                    //lbUnit7.Text = tem.E;//unit
                    //lbUnit8.Text = tem.E;//unit
                    //#endregion
                    //#region "Liquid Particle Count (68 KHz) 0.3"
                    //lbB28.Text = tem.F;
                    //#endregion
                    //#region "Liquid Particle Count (68 KHz) 0.6"
                    //lbB35.Text = tem.G;
                    //#endregion
                    //#region "Liquid Particle Count (132 KHz) 0.3 "
                    //lbB42.Text = tem.H;
                    //#endregion
                    //#region "Liquid Particle Count (132 KHz) 0.6"
                    //lbB49.Text = tem.I;
                    //#endregion
                }

                LPCTypeEnum lpcType = (LPCTypeEnum)Enum.ToObject(typeof(LPCTypeEnum), Convert.ToInt32(this.Lpcs[0].lpc_type));
                ddlA19.SelectedValue = lpcType.ToString();

                #region "US-LPC(0.3)"
                template_seagate_lpc_coverpage khz68_03 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_03).ToString());
                if (khz68_03 != null)
                {
                    //txt_UsLPC03_B14.Text = khz68_03.b14;
                    //txt_UsLPC03_B15.Text = khz68_03.b15;
                    //txt_UsLPC03_B16.Text = khz68_03.b16;
                    //txt_UsLPC03_B17.Text = khz68_03.b17;
                    //txt_UsLPC03_B18.Text = khz68_03.b18;

                    //txt_UsLPC03_C14.Text = khz68_03.c14;
                    //txt_UsLPC03_C15.Text = khz68_03.c15;
                    //txt_UsLPC03_C16.Text = khz68_03.c16;
                    //txt_UsLPC03_C17.Text = khz68_03.c17;
                    //txt_UsLPC03_C18.Text = khz68_03.c18;

                    //txt_UsLPC03_D14.Text = khz68_03.d14;
                    //txt_UsLPC03_D15.Text = khz68_03.d15;
                    //txt_UsLPC03_D16.Text = khz68_03.d16;
                    //txt_UsLPC03_D17.Text = khz68_03.d17;
                    //txt_UsLPC03_D18.Text = khz68_03.d18;

                    //txt_UsLPC03_E14.Text = khz68_03.e14;
                    //txt_UsLPC03_E15.Text = khz68_03.e15;
                    //txt_UsLPC03_E16.Text = khz68_03.e16;
                    //txt_UsLPC03_E17.Text = khz68_03.e17;
                    //txt_UsLPC03_E18.Text = khz68_03.e18;

                    //txt_UsLPC03_F14.Text = khz68_03.f14;
                    //txt_UsLPC03_F15.Text = khz68_03.f15;
                    //txt_UsLPC03_F16.Text = khz68_03.f16;
                    //txt_UsLPC03_F17.Text = khz68_03.f17;
                    //txt_UsLPC03_F18.Text = khz68_03.f18;

                    //txt_UsLPC03_G14.Text = khz68_03.g14;
                    //txt_UsLPC03_G15.Text = khz68_03.g15;
                    //txt_UsLPC03_G16.Text = khz68_03.g16;
                    //txt_UsLPC03_G17.Text = khz68_03.g17;
                    //txt_UsLPC03_G18.Text = khz68_03.g18;

                    //txt_UsLPC03_B26.Text = khz68_03.b26;

                    ////txt_UsLPC03_B20.Text = Lpcs[0].cvp_c19;
                    //txt_UsLPC03_B21.Text = khz68_03.b21;
                    ////txt_UsLPC03_B22.Text = Lpcs[0].cvp_e19;

                    //txt_UsLPC03_B25.Text = khz68_03.b25;
                    //txt_UsLPC03_D25.Text = khz68_03.d25;
                    //txt_UsLPC03_F25.Text = khz68_03.f25;

                }
                #endregion
                #region "US-LPC(0.6)"
                template_seagate_lpc_coverpage khz68_06 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06).ToString());
                if (khz68_06 != null)
                {
                    //txt_UsLPC06_B14.Text = khz68_06.b14;
                    //txt_UsLPC06_B15.Text = khz68_06.b15;
                    //txt_UsLPC06_B16.Text = khz68_06.b16;
                    //txt_UsLPC06_B17.Text = khz68_06.b17;
                    //txt_UsLPC06_B18.Text = khz68_06.b18;

                    //txt_UsLPC06_C14.Text = khz68_06.c14;
                    //txt_UsLPC06_C15.Text = khz68_06.c15;
                    //txt_UsLPC06_C16.Text = khz68_06.c16;
                    //txt_UsLPC06_C17.Text = khz68_06.c17;
                    //txt_UsLPC06_C18.Text = khz68_06.c18;

                    //txt_UsLPC06_D14.Text = khz68_06.d14;
                    //txt_UsLPC06_D15.Text = khz68_06.d15;
                    //txt_UsLPC06_D16.Text = khz68_06.d16;
                    //txt_UsLPC06_D17.Text = khz68_06.d17;
                    //txt_UsLPC06_D18.Text = khz68_06.d18;

                    //txt_UsLPC06_E14.Text = khz68_06.e14;
                    //txt_UsLPC06_E15.Text = khz68_06.e15;
                    //txt_UsLPC06_E16.Text = khz68_06.e16;
                    //txt_UsLPC06_E17.Text = khz68_06.e17;
                    //txt_UsLPC06_E18.Text = khz68_06.e18;

                    //txt_UsLPC06_F14.Text = khz68_06.f14;
                    //txt_UsLPC06_F15.Text = khz68_06.f15;
                    //txt_UsLPC06_F16.Text = khz68_06.f16;
                    //txt_UsLPC06_F17.Text = khz68_06.f17;
                    //txt_UsLPC06_F18.Text = khz68_06.f18;

                    //txt_UsLPC06_G14.Text = khz68_06.g14;
                    //txt_UsLPC06_G15.Text = khz68_06.g15;
                    //txt_UsLPC06_G16.Text = khz68_06.g16;
                    //txt_UsLPC06_G17.Text = khz68_06.g17;
                    //txt_UsLPC06_G18.Text = khz68_06.g18;

                    //txt_UsLPC06_B26.Text = khz68_06.b26;

                    ////txt_UsLPC06_B20.Text = Lpcs[0].cvp_c19;
                    //txt_UsLPC06_B21.Text = khz68_06.b21;
                    ////txt_UsLPC06_B22.Text = Lpcs[0].cvp_e19;

                    //txt_UsLPC06_B25.Text = khz68_06.b25;
                    //txt_UsLPC06_D25.Text = khz68_06.d25;
                    //txt_UsLPC06_F25.Text = khz68_06.f25;
                }
                #endregion



                //FORM COVER PAGE
                txtB19.Text = this.Lpcs[0].ProcedureNo;
                txtCVP_C19.Text = this.Lpcs[0].NumberOfPieces;
                txtD19.Text = this.Lpcs[0].ExtractionMedium;
                txtCVP_E19.Text = this.Lpcs[0].ExtractionVolume;



                LPCTypeEnum lpcType1 = (LPCTypeEnum)Enum.ToObject(typeof(LPCTypeEnum), Convert.ToInt32(this.Lpcs[0].lpc_type));
                switch (lpcType1)
                {
                    case LPCTypeEnum.KHz_68:
                        //tb1_1.Visible = true;
                        //tb2.Visible = true;
                        //tb3.Visible = false;
                        //tb4.Visible = false;
                        //pUS_LPC03.Visible = true;
                        //pUS_LPC06.Visible = false;
                        break;
                    case LPCTypeEnum.KHz_132:
                        //tb1_1.Visible = false;
                        //tb2.Visible = false;
                        //tb3.Visible = true;
                        //tb4.Visible = true;
                        //pUS_LPC03.Visible = false;
                        //pUS_LPC06.Visible = true;
                        break;
                }
                //
                CalculateCas();
            }
            else
            {
                #region "Initial coverpage value"
                this.Lpcs = new List<template_seagate_lpc_coverpage>();
                template_seagate_lpc_coverpage lpc = new template_seagate_lpc_coverpage();
                lpc.sample_id = this.SampleID;
                lpc.specification_id = Convert.ToInt16(ddlSpecification.SelectedValue);
                lpc.lpc_type = Convert.ToInt16(LPCTypeEnum.KHz_68).ToString();
                lpc.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03).ToString();
                lpc.item_visible = getItemStatus();
                Lpcs.Add(lpc);
                lpc = new template_seagate_lpc_coverpage();
                lpc.sample_id = this.SampleID;
                lpc.specification_id = Convert.ToInt16(ddlSpecification.SelectedValue);
                lpc.lpc_type = Convert.ToInt16(LPCTypeEnum.KHz_68).ToString();
                lpc.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06).ToString();
                lpc.item_visible = getItemStatus();
                Lpcs.Add(lpc);


                #endregion
            }

            #endregion



            //initial component
            btnCoverPage.CssClass = "btn blue";
            btnWorkSheet.CssClass = "btn green";

            pCoverpage.Visible = true;
            pDSH.Visible = false;
            //pUS_LPC03.Visible = false;
            //pUS_LPC06.Visible = false;
            switch (lbJobStatus.Text)
            {
                case "CONVERT_PDF":
                    litDownloadIcon.Text = "<i class=\"fa fa-file-pdf-o\"></i>";
                    break;
                default:
                    litDownloadIcon.Text = "<i class=\"fa fa-file-word-o\"></i>";
                    break;
            }




        }

        #endregion

        List<String> errors = new List<String>();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;
            template_seagate_lpc_coverpage objWork = new template_seagate_lpc_coverpage();

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;

                    foreach (template_seagate_lpc_coverpage _tmp in this.Lpcs)
                    {
                        _tmp.sample_id = this.jobSample.ID;
                        _tmp.lpc_type = ddlA19.SelectedValue;
                        _tmp.specification_id = Convert.ToInt16(ddlSpecification.SelectedValue);
                        _tmp.ProcedureNo = txtB19.Text;
                        _tmp.NumberOfPieces = txtCVP_C19.Text;
                        _tmp.ExtractionMedium = txtD19.Text;
                        _tmp.ExtractionVolume = txtCVP_E19.Text;
                        _tmp.item_visible = getItemStatus();
                    }
                    if (this.Lpcs.Count > 0)
                    {
                        switch (this.CommandName)
                        {
                            case CommandNameEnum.Add:
                                objWork.InsertList(this.Lpcs);
                                break;
                            case CommandNameEnum.Edit:

                                objWork.UpdateList(this.Lpcs);
                                break;
                        }
                    }

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.Lpcs.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        //#region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        //#endregion
                        foreach (template_seagate_lpc_coverpage _tmp in this.Lpcs)
                        {
                            _tmp.sample_id = this.jobSample.ID;
                            _tmp.lpc_type = ddlA19.SelectedValue;
                            _tmp.specification_id = Convert.ToInt16(ddlSpecification.SelectedValue);
                            _tmp.ProcedureNo = txtB19.Text;

                            switch (ddlA19.SelectedValue)
                            {
                                case "1":
                                    //Extraction Vol. (ml) & No. of Parts Used For (64KHz)
                                    //_tmp.NumberOfPieces = txt_UsLPC03_B20.Text;
                                    //_tmp.ExtractionVolume = txt_UsLPC03_B22.Text;
                                    break;
                                case "2":
                                    //Extraction Vol. (ml) & No. of Parts Used For (132KHz)
                                    //_tmp.NumberOfPieces = txt_UsLPC06_B20.Text;
                                    //_tmp.ExtractionVolume = txt_UsLPC06_B22.Text;
                                    break;
                            }

                            _tmp.ExtractionMedium = txtD19.Text;
                            _tmp.item_visible = getItemStatus();
                        }
                        #region "US-LPC(0.3)"
                        template_seagate_lpc_coverpage khz68_03 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_03).ToString());
                        if (khz68_03 != null)
                        {
                            //khz68_03.lpc_type = (cbLPCType68.Checked) ? "1" : "2";
                            //khz68_03.b14 = txt_UsLPC03_B14.Text;
                            //khz68_03.b15 = txt_UsLPC03_B15.Text;
                            //khz68_03.b16 = txt_UsLPC03_B16.Text;
                            //khz68_03.b17 = txt_UsLPC03_B17.Text;

                            //khz68_03.c14 = txt_UsLPC03_C14.Text;
                            //khz68_03.c15 = txt_UsLPC03_C15.Text;
                            //khz68_03.c16 = txt_UsLPC03_C16.Text;
                            //khz68_03.c17 = txt_UsLPC03_C17.Text;

                            //khz68_03.d14 = txt_UsLPC03_D14.Text;
                            //khz68_03.d15 = txt_UsLPC03_D15.Text;
                            //khz68_03.d16 = txt_UsLPC03_D16.Text;
                            //khz68_03.d17 = txt_UsLPC03_D17.Text;

                            //khz68_03.e14 = txt_UsLPC03_E14.Text;
                            //khz68_03.e15 = txt_UsLPC03_E15.Text;
                            //khz68_03.e16 = txt_UsLPC03_E16.Text;
                            //khz68_03.e17 = txt_UsLPC03_E17.Text;

                            //khz68_03.f14 = txt_UsLPC03_F14.Text;
                            //khz68_03.f15 = txt_UsLPC03_F15.Text;
                            //khz68_03.f16 = txt_UsLPC03_F16.Text;
                            //khz68_03.f17 = txt_UsLPC03_F17.Text;

                            //khz68_03.g14 = txt_UsLPC03_G14.Text;
                            //khz68_03.g15 = txt_UsLPC03_G15.Text;
                            //khz68_03.g16 = txt_UsLPC03_G16.Text;
                            //khz68_03.g17 = txt_UsLPC03_G17.Text;

                            //khz68_03.b21 = txt_UsLPC03_B21.Text;
                            //khz68_03.b25 = txt_UsLPC03_B25.Text;
                            //khz68_03.d25 = txt_UsLPC03_D25.Text;
                            //khz68_03.f25 = txt_UsLPC03_F25.Text;


                        }
                        #endregion
                        #region "US-LPC(0.6)"
                        template_seagate_lpc_coverpage khz68_06 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06).ToString());
                        if (khz68_06 != null)
                        {
                            //khz68_06.b14 = txt_UsLPC06_B14.Text;
                            //khz68_06.b15 = txt_UsLPC06_B15.Text;
                            //khz68_06.b16 = txt_UsLPC06_B16.Text;
                            //khz68_06.b17 = txt_UsLPC06_B17.Text;

                            //khz68_06.c14 = txt_UsLPC06_C14.Text;
                            //khz68_06.c15 = txt_UsLPC06_C15.Text;
                            //khz68_06.c16 = txt_UsLPC06_C16.Text;
                            //khz68_06.c17 = txt_UsLPC06_C17.Text;

                            //khz68_06.d14 = txt_UsLPC06_D14.Text;
                            //khz68_06.d15 = txt_UsLPC06_D15.Text;
                            //khz68_06.d16 = txt_UsLPC06_D16.Text;
                            //khz68_06.d17 = txt_UsLPC06_D17.Text;

                            //khz68_06.e14 = txt_UsLPC06_E14.Text;
                            //khz68_06.e15 = txt_UsLPC06_E15.Text;
                            //khz68_06.e16 = txt_UsLPC06_E16.Text;
                            //khz68_06.e17 = txt_UsLPC06_E17.Text;

                            //khz68_06.f14 = txt_UsLPC06_F14.Text;
                            //khz68_06.f15 = txt_UsLPC06_F15.Text;
                            //khz68_06.f16 = txt_UsLPC06_F16.Text;
                            //khz68_06.f17 = txt_UsLPC06_F17.Text;

                            //khz68_06.g14 = txt_UsLPC06_G14.Text;
                            //khz68_06.g15 = txt_UsLPC06_G15.Text;
                            //khz68_06.g16 = txt_UsLPC06_G16.Text;
                            //khz68_06.g17 = txt_UsLPC06_G17.Text;

                            //khz68_06.b21 = txt_UsLPC06_B21.Text;
                            //khz68_06.b25 = txt_UsLPC06_B25.Text;
                            //khz68_06.d25 = txt_UsLPC06_D25.Text;
                            //khz68_06.f25 = txt_UsLPC06_F25.Text;
                        }
                        #endregion
                        khz68_03.UpdateList(this.Lpcs);
                    }
                    else
                    {
                        errors.Add("ไม่พบข้อมูล WorkSheet");
                    }
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"


                            this.jobSample.date_srchemist_complate = DateTime.Now;
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

                            this.jobSample.date_labman_complete = DateTime.Now;
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
                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload1.SaveAs(source_file);
                        this.jobSample.path_word = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        isValid = false;
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    //if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".pdf")))
                    //{
                    //    string yyyy = DateTime.Now.ToString("yyyy");
                    //    string MM = DateTime.Now.ToString("MM");
                    //    string dd = DateTime.Now.ToString("dd");

                    //    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                    //    String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));


                    //    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    //    {
                    //        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    //    }
                    //    btnUpload.SaveAs(source_file);
                    //    this.jobSample.path_pdf = source_file_url;
                    //    this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    //    //lbMessage.Text = string.Empty;
                    //}
                    //else
                    //{
                    //    errors.Add("Invalid File. Please upload a File with extension .pdf");
                    //    //lbMessage.Attributes["class"] = "alert alert-error";
                    //    //isValid = false;
                    //}
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    this.jobSample.step7owner = userLogin.id;
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

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //removeSession();
            Response.Redirect(this.PreviousPath);

        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            #region "LOAD"
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            List<template_seagate_lpc_coverpage> listSeagateLpc = new List<template_seagate_lpc_coverpage>();

            List<LPC> lpcs = new List<LPC>();

            for (int i = 0; i < btnUpload.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = btnUpload.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            string yyyy = DateTime.Now.ToString("yyyy");
                            string MM = DateTime.Now.ToString("MM");
                            string dd = DateTime.Now.ToString("dd");

                            String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));
                            String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));


                            if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                            }

                            _postedFile.SaveAs(source_file);

                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wd = new HSSFWorkbook(fs);
                                ISheet isheet = wd.GetSheet("Sheet1");
                                int run = 0;
                                for (int row = 17; row < 120; row++)
                                {
                                    if (isheet.GetRow(row) != null) //null is when the row only contains empty cells 
                                    {
                                        if (CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.F)).Equals("Sample #"))
                                        {
                                            run++;
                                            Console.WriteLine();
                                        }
                                        if (String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.C))))
                                        {
                                            //switch (CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J)))
                                            //{
                                            //    case "0.200":
                                            //    case "0.300":
                                            //    case "0.400":
                                            //    case "0.500":
                                            //    case "0.600":
                                            //    case "0.700":
                                            //    case "0.800":
                                            //    case "0.900":
                                            //    case "1.000":
                                            //    case "1.500":
                                            //case "2.000":
                                            if (ddlChannel.SelectedValue.Equals(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J))))
                                            {
                                                LPC lpc = new LPC();
                                                lpc.RunNumber = run;
                                                lpc.Run = run + "";
                                                lpc.type = Path.GetFileNameWithoutExtension(_postedFile.FileName);
                                                lpc.ChannelSize = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J));
                                                lpc.Value = String.Format("{0:0.00}", Math.Round(Convert.ToDouble(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.S))), _postedFile.FileName.StartsWith("B") ? Convert.ToInt16(txtDecimal01.Text) : Convert.ToInt16(txtDecimal02.Text)));
                                                lpcs.Add(lpc);
                                            }
                                                    //break;
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                    }
                    Console.WriteLine();
                }
                catch (Exception Ex)
                {
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
                    //logger.Error(Ex.Message);
                    Console.WriteLine();
                }
            }
            #endregion



            //New Order
            if (lpcs.Count > 0)
            {
                string lastSampleCount = lpcs.Max(x => x.type);
                lastSampleCount = lastSampleCount.Substring(1, lastSampleCount.Length - 1);
                List<LPC> newLpcs = new List<LPC>();
                for (int i = 1; i <= Convert.ToInt16(lastSampleCount); i++)
                {
                    newLpcs.AddRange(lpcs.Where(x => x.type.Equals("B" + i)).ToList());
                    newLpcs.AddRange(lpcs.Where(x => x.type.Equals("S" + i)).ToList());

                }

                foreach (LPC _lpc in newLpcs)
                {
                    _lpc.type = _lpc.type.Replace("B", "Blank ").Replace("S", "Sample ");
                }

                var lpcTypeGroup =
                       from lpc in newLpcs
                       group lpc by lpc.type into newGroup
                       orderby newGroup.Key
                       select newGroup;



                List<LPC> tmp = new List<LPC>();
                foreach (var item in lpcTypeGroup)
                {
                    LPC lpc = new LPC();
                    //Average of last 3
                    lpc.RunNumber = 5;
                    lpc.Run = "Average of last 3";
                    lpc.type = item.Key;
                    lpc.ChannelSize = ddlChannel.SelectedValue;
                    double _value = newLpcs.Where(x => x.type.Equals(item.Key) && x.RunNumber > 1 && x.ChannelSize.Equals(ddlChannel.SelectedValue)).Average(x => Convert.ToDouble(x.Value));
                    lpc.Value = String.Format("{0:0.000}", Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                    tmp.Add(lpc);
                }

                newLpcs.AddRange(tmp);

                DataTable dt = PivotTable.GetInversedDataTable(newLpcs.Where(x => x.ChannelSize.Equals(ddlChannel.SelectedValue)).ToDataTable(), "Type", "Run", "Value", "-", false);

                gvWorkSheet.DataSource = dt;
                gvWorkSheet.DataBind();


                if (String.IsNullOrEmpty(txtSurfaceArea.Text))
                {
                    errors.Add("Surface Area (cm2) is empty.");
                }
                if (String.IsNullOrEmpty(txtDilutionFactor.Text))
                {
                    errors.Add("Dilution Factor (time) is empty.");
                }

                if (errors.Count == 0)
                {
                    List<LPC> listAverages = new List<LPC>();
                    var lpcTypeGroup1 = from lpc in newLpcs where lpc.type.StartsWith("Blank") group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                    int index = 1;
                    foreach (var item in lpcTypeGroup1)
                    {
                        LPC lpc = new LPC();
                        //Average of last 3
                        lpc.RunNumber = 6;
                        lpc.Run = "";
                        lpc.Sample = "";
                        lpc.type = item.Key.Replace("Blank", "");
                        lpc.ChannelSize = ddlChannel.SelectedValue;
                        LPC lpcBlank = newLpcs.Where(x => x.RunNumber == 5 && x.type.Equals("Blank " + index)).FirstOrDefault();
                        LPC lpcSaple = newLpcs.Where(x => x.RunNumber == 5 && x.type.Equals("Sample " + index)).FirstOrDefault();


                        double _value = (Convert.ToDouble(lpcSaple.Value) - Convert.ToDouble(lpcBlank.Value)) * Convert.ToDouble(lbExtractionVol.Text) / (Convert.ToDouble(txtSurfaceArea.Text) * Convert.ToDouble(lbNoOfPartsUsed.Text)) * Convert.ToDouble(txtDilutionFactor.Text);

                        lpc.Value = String.Format("{0:0}", Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                        listAverages.Add(lpc);
                        index++;
                    }
                    DataTable dtAverages = PivotTable.GetInversedDataTable(listAverages.Where(x => x.ChannelSize.Equals(ddlChannel.SelectedValue)).ToDataTable(), "Type", "Sample", "Value", ddlChannel.SelectedItem.Text, false);
                    gvWorkSheetAverage.DataSource = dtAverages;
                    gvWorkSheetAverage.DataBind();
                    Console.WriteLine();
                    double average = listAverages.Average(x => Convert.ToDouble(x.Value));
                    lbAverage.Text = average.ToString().Split('.')[0];

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
            //CalculateCas();
            Console.WriteLine("-END-");

        }

        protected void btnUsLPC_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";
                    pCoverpage.Visible = true;
                    pDSH.Visible = false;
                    break;
                case "WorkSheet":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pDSH.Visible = true;
                    lbExtractionVol.Text = txtCVP_E19.Text;
                    lbNoOfPartsUsed.Text = txtCVP_C19.Text;

                    break;
            }



            CalculateCas();
        }

        #region "Custom method"

        private String validateDSHFile(IList<HttpPostedFile> _files)
        {
            Boolean isFound_b1 = false;
            Boolean isFound_b2 = false;
            Boolean isFound_b3 = false;

            Boolean isFound_s1 = false;
            Boolean isFound_s2 = false;
            Boolean isFound_s3 = false;

            Boolean isFoundWrongExtension = false;

            String result = String.Empty;

            String[] files = new String[_files.Count];
            if (files.Length == 6)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    files[i] = _files[i].FileName;
                    if (!Path.GetExtension(_files[i].FileName).Trim().Equals(".xls"))
                    {
                        isFoundWrongExtension = true;
                        break;
                    }
                }
                if (!isFoundWrongExtension)
                {

                    //Find B1
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B1"))
                        {
                            isFound_b1 = true;
                            break;
                        }
                    }

                    //Find B2
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B2"))
                        {
                            isFound_b2 = true;
                            break;
                        }
                    }
                    //Find B3
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B3"))
                        {
                            isFound_b3 = true;
                            break;
                        }
                    }

                    //Find S1
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S1"))
                        {
                            isFound_s1 = true;
                            break;
                        }
                    }

                    //Find S2
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S2"))
                        {
                            isFound_s2 = true;
                            break;
                        }
                    }
                    //Find S3
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S3"))
                        {
                            isFound_s3 = true;
                            break;
                        }
                    }

                    result = (!isFound_b1) ? result += "File not found B1.xls" :
                                (!isFound_b2) ? result += "File not found B2.xls" :
                                (!isFound_b3) ? result += "File not found B3.xls" :
                                (!isFound_s1) ? result += "File not found S1.xls" :
                                (!isFound_s2) ? result += "File not found S2.xls" :
                                (!isFound_s3) ? result += "File not found S3.xls" : String.Empty;
                }
                else
                {
                    result = "File extension must be *.txt";
                }
            }
            else
            {
                result = "You must to select 6 files for upload.";
            }
            return result;
        }

        private void CalculateCas()
        {

            #region "US-LPC(0.3)"
            template_seagate_lpc_coverpage khz68_03 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_03).ToString());
            if (khz68_03 != null)
            {
                //khz68_03.lpc_type = (cbLPCType68.Checked) ? "1" : "2";
                //khz68_03.b14 = txt_UsLPC03_B14.Text;
                //khz68_03.b15 = txt_UsLPC03_B15.Text;
                //khz68_03.b16 = txt_UsLPC03_B16.Text;
                //khz68_03.b17 = txt_UsLPC03_B17.Text;
                //khz68_03.b18 = txt_UsLPC03_B18.Text;

                //khz68_03.c14 = txt_UsLPC03_C14.Text;
                //khz68_03.c15 = txt_UsLPC03_C15.Text;
                //khz68_03.c16 = txt_UsLPC03_C16.Text;
                //khz68_03.c17 = txt_UsLPC03_C17.Text;
                //khz68_03.c18 = txt_UsLPC03_C18.Text;

                //khz68_03.d14 = txt_UsLPC03_D14.Text;
                //khz68_03.d15 = txt_UsLPC03_D15.Text;
                //khz68_03.d16 = txt_UsLPC03_D16.Text;
                //khz68_03.d17 = txt_UsLPC03_D17.Text;
                //khz68_03.d18 = txt_UsLPC03_D18.Text;


                //khz68_03.e14 = txt_UsLPC03_E14.Text;
                //khz68_03.e15 = txt_UsLPC03_E15.Text;
                //khz68_03.e16 = txt_UsLPC03_E16.Text;
                //khz68_03.e17 = txt_UsLPC03_E17.Text;
                //khz68_03.e18 = txt_UsLPC03_E18.Text;

                //khz68_03.f14 = txt_UsLPC03_F14.Text;
                //khz68_03.f15 = txt_UsLPC03_F15.Text;
                //khz68_03.f16 = txt_UsLPC03_F16.Text;
                //khz68_03.f17 = txt_UsLPC03_F17.Text;
                //khz68_03.f18 = txt_UsLPC03_F18.Text;

                //khz68_03.g14 = txt_UsLPC03_G14.Text;
                //khz68_03.g15 = txt_UsLPC03_G15.Text;
                //khz68_03.g16 = txt_UsLPC03_G16.Text;
                //khz68_03.g17 = txt_UsLPC03_G17.Text;
                //khz68_03.g18 = txt_UsLPC03_G18.Text;

                //khz68_03.b21 = txt_UsLPC03_B21.Text;
                //khz68_03.b25 = txt_UsLPC03_B25.Text;
                //khz68_03.d25 = txt_UsLPC03_D25.Text;
                //khz68_03.f25 = txt_UsLPC03_F25.Text;
                //khz68_03.b26 = txt_UsLPC03_B26.Text;


            }
            #endregion
            #region "US-LPC(0.6)"
            template_seagate_lpc_coverpage khz68_06 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06).ToString());
            if (khz68_06 != null)
            {
                //khz68_06.b14 = txt_UsLPC06_B14.Text;
                //khz68_06.b15 = txt_UsLPC06_B15.Text;
                //khz68_06.b16 = txt_UsLPC06_B16.Text;
                //khz68_06.b17 = txt_UsLPC06_B17.Text;
                //khz68_06.b18 = txt_UsLPC06_B18.Text;

                //khz68_06.c14 = txt_UsLPC06_C14.Text;
                //khz68_06.c15 = txt_UsLPC06_C15.Text;
                //khz68_06.c16 = txt_UsLPC06_C16.Text;
                //khz68_06.c17 = txt_UsLPC06_C17.Text;
                //khz68_06.c18 = txt_UsLPC06_C18.Text;


                //khz68_06.d14 = txt_UsLPC06_D14.Text;
                //khz68_06.d15 = txt_UsLPC06_D15.Text;
                //khz68_06.d16 = txt_UsLPC06_D16.Text;
                //khz68_06.d17 = txt_UsLPC06_D17.Text;
                //khz68_06.d18 = txt_UsLPC06_D18.Text;

                //khz68_06.e14 = txt_UsLPC06_E14.Text;
                //khz68_06.e15 = txt_UsLPC06_E15.Text;
                //khz68_06.e16 = txt_UsLPC06_E16.Text;
                //khz68_06.e17 = txt_UsLPC06_E17.Text;
                //khz68_06.e18 = txt_UsLPC06_E18.Text;

                //khz68_06.f14 = txt_UsLPC06_F14.Text;
                //khz68_06.f15 = txt_UsLPC06_F15.Text;
                //khz68_06.f16 = txt_UsLPC06_F16.Text;
                //khz68_06.f17 = txt_UsLPC06_F17.Text;
                //khz68_06.f18 = txt_UsLPC06_F18.Text;

                //khz68_06.g14 = txt_UsLPC06_G14.Text;
                //khz68_06.g15 = txt_UsLPC06_G15.Text;
                //khz68_06.g16 = txt_UsLPC06_G16.Text;
                //khz68_06.g17 = txt_UsLPC06_G17.Text;
                //khz68_06.g18 = txt_UsLPC06_G18.Text;

                //khz68_06.b21 = txt_UsLPC06_B21.Text;
                //khz68_06.b25 = txt_UsLPC06_B25.Text;
                //khz68_06.d25 = txt_UsLPC06_D25.Text;
                //khz68_06.f25 = txt_UsLPC06_F25.Text;
                //khz68_06.b26 = txt_UsLPC06_B26.Text;

            }
            #endregion

            //Calculate Result
            #region "US-LPC(0.3)"
            khz68_03 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_03).ToString());
            if (khz68_03 != null)
            {
                if (!String.IsNullOrEmpty(khz68_03.b25))
                {
                    //lbC25.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_03.b25)));//='US-LPC(0.3)'!B25
                    //lbC26.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_03.d25)));//='US-LPC(0.3)'!D25
                    //lbC27.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_03.f25)));//='US-LPC(0.3)'!F25
                    //lbC28.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(((Convert.ToDecimal(khz68_03.b25) +
                    //               Convert.ToDecimal(khz68_03.d25) +
                    //               Convert.ToDecimal(khz68_03.f25)) / 3).ToString())));

                    ////132 KHz
                    //lbC39.Text = lbC25.Text;
                    //lbC40.Text = lbC26.Text;
                    //lbC41.Text = lbC27.Text;
                    //lbC42.Text = lbC28.Text;
                }

                LPCTypeEnum lpcType = (LPCTypeEnum)Enum.ToObject(typeof(LPCTypeEnum), Convert.ToInt32(khz68_03.lpc_type));

            }
            #endregion

            #region "US-LPC(0.6)"
            khz68_06 = Lpcs.Find(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06).ToString());
            if (khz68_06 != null)
            {
                if (!String.IsNullOrEmpty(khz68_06.b25))
                {
                    //lbC32.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_06.b25)));//='US-LPC(0.6)'!B25
                    //lbC33.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_06.d25)));//='US-LPC(0.6)'!D25
                    //lbC34.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(khz68_06.f25)));//='US-LPC(0.6)'!F25
                    //lbC35.Text = String.Format("{0:n0}", Math.Round(Convert.ToDecimal(((Convert.ToDecimal(khz68_06.b25) +
                    //               Convert.ToDecimal(khz68_06.d25) +
                    //               Convert.ToDecimal(khz68_06.f25)) / 3).ToString())));

                    ////132 KHz
                    //lbC46.Text = lbC32.Text;
                    //lbC47.Text = lbC33.Text;
                    //lbC48.Text = lbC34.Text;
                    //lbC49.Text = lbC35.Text;
                }
            }
            #endregion

        }
        #endregion

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            template_seagate_lpc_coverpage objCoverPage = new template_seagate_lpc_coverpage();
            DataTable dt = Extenders.ObjectToDataTable(this.Lpcs[0]);
            List<ReportLPC> reportLpcs = objCoverPage.generateReport(this.Lpcs);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            //GenerateHtmlBiz genHtmlBiz = new GenerateHtmlBiz();
            //genHtmlBiz.reportHeader = reportHeader;
            //genHtmlBiz.download();



            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2)); reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", ddlA19.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Seagate's Doc {0} for {1}", lbDocNo.Text, lbCommodity.Text)));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/lpc_seagate.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", reportLpcs.Where(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_03)).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", reportLpcs.Where(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06)).ToDataTable())); // Add datasource here




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

                    //if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    //{
                    //    Word2Pdf objWorPdf = new Word2Pdf();
                    //    objWorPdf.InputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word);
                    //    objWorPdf.OutputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word).Replace("doc", "pdf");
                    //    try
                    //    {
                    //        objWorPdf.Word2PdfCOnversion();
                    //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word).Replace("doc", "pdf"));

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine();
                    //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));

                    //    }
                    //}
                    break;
            }


        }


        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (tem != null)
            {
                //#region "HEADER"
                //lbDocNo.Text = tem.C;
                //lbDocRev.Text = tem.D;
                //lbCommodity.Text = tem.B;
                //lbUnit1.Text = tem.E;//unit
                //lbUnit2.Text = tem.E;//unit
                //lbUnit3.Text = tem.E;//unit
                //lbUnit4.Text = tem.E;//unit
                //lbUnit5.Text = tem.E;//unit
                //lbUnit6.Text = tem.E;//unit
                //lbUnit7.Text = tem.E;//unit
                //lbUnit8.Text = tem.E;//unit
                //#endregion
                //#region "Liquid Particle Count (68 KHz) 0.3"
                //lbB28.Text = tem.F;
                //#endregion
                //#region "Liquid Particle Count (68 KHz) 0.6"
                //lbB35.Text = tem.G;
                //#endregion
                //#region "Liquid Particle Count (132 KHz) 0.3 "
                //lbB42.Text = tem.H;
                //#endregion
                //#region "Liquid Particle Count (132 KHz) 0.6"
                //lbB49.Text = tem.I;
                //#endregion

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

        private String getItemStatus()
        {
            String result = String.Empty;
            //result = ((CheckBox1.Checked) ? "1" : "0") +
            //            ((CheckBox2.Checked) ? "1" : "0") +
            //            ((CheckBox3.Checked) ? "1" : "0") +
            //            ((CheckBox4.Checked) ? "1" : "0");
            return result;
        }

        private void ShowItem(String _itemVisible)
        {
            //if (_itemVisible != null)
            //{
            //    char[] item = _itemVisible.ToCharArray();
            //    if (item.Length == 4)
            //    {
            //        StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            //        switch (status)
            //        {
            //            case StatusEnum.LOGIN_CONVERT_TEMPLATE:
            //                break;
            //            case StatusEnum.LOGIN_SELECT_SPEC:
            //                CheckBox1.Checked = item[0] == '1' ? true : false;
            //                CheckBox2.Checked = item[1] == '1' ? true : false;
            //                CheckBox3.Checked = item[2] == '1' ? true : false;
            //                CheckBox4.Checked = item[3] == '1' ? true : false;
            //                txtCVP_C19.Visible = true;
            //                txtCVP_E19.Visible = true;
            //                break;
            //            case StatusEnum.CHEMIST_TESTING:
            //            case StatusEnum.SR_CHEMIST_CHECKING:
            //            case StatusEnum.SR_CHEMIST_APPROVE:
            //            case StatusEnum.SR_CHEMIST_DISAPPROVE:
            //            case StatusEnum.ADMIN_CONVERT_WORD:
            //            case StatusEnum.LABMANAGER_CHECKING:
            //            case StatusEnum.LABMANAGER_APPROVE:
            //            case StatusEnum.LABMANAGER_DISAPPROVE:
            //            case StatusEnum.ADMIN_CONVERT_PDF:
            //                tb1.Visible = item[0] == '1' ? true : false;
            //                tb2.Visible = item[1] == '1' ? true : false;
            //                tb3.Visible = item[2] == '1' ? true : false;
            //                tb4.Visible = item[3] == '1' ? true : false;
            //                txtCVP_C19.Visible = false;
            //                txtCVP_E19.Visible = false;
            //                CheckBox1.Visible = false;
            //                CheckBox2.Visible = false;
            //                CheckBox3.Visible = false;
            //                CheckBox4.Visible = false;
            //                break;
            //        }
            //    }
            //}

        }

        protected void ddlA19_SelectedIndexChanged(object sender, EventArgs e)
        {
            LPCTypeEnum lpcType = (LPCTypeEnum)Enum.ToObject(typeof(LPCTypeEnum), Convert.ToInt32(ddlA19.SelectedValue));
            switch (lpcType)
            {
                case LPCTypeEnum.KHz_68:
                    //tb1_1.Visible = true;
                    //tb2.Visible = true;
                    //tb3.Visible = false;
                    //tb4.Visible = false;
                    break;
                case LPCTypeEnum.KHz_132:
                    //tb1_1.Visible = false;
                    //tb2.Visible = false;
                    //tb3.Visible = true;
                    //tb4.Visible = true;
                    break;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbParticle.Text = ddlChannel.SelectedItem.Text;
        }

        protected void txtSurfaceArea_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class LPC
    {
        public string type { get; set; }
        public string Run { get; set; }
        public int RunNumber { get; set; }
        public string Sample { get; set; }
        public string ChannelSize { get; set; }
        public string Value { get; set; }
        //public int Sample { get; set; }
    }

}