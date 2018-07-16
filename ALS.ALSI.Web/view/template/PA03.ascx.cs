using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
using Microsoft.Reporting.WebForms;
using Spire.Doc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class PA03 : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_DHS));
        private String PA_SPECIFICATION = "PA5x_BOSCH0442S00155";
        private const String PA_MICROPIC_DATA = "Micropic Data:";

        private const String PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION = "Description of process and extraction:";
        private const String PA_DISSOLVING = "Dissolving";
        private const String PA_AGITATION = "Agitation";
        private const String PA_ULTRASONIC = "Ultrasonic";
        private const String PA_WASHING = "Washing";
        private const String PA_PRESURE_RINSING = "Pressure rinsing";
        private const String PA_INTERNAL_RINSING = "Internal rinsing";
        private const String PA_REFLECTIVE = "reflective";
        private const String PA_NON_REFLECTIVE = "non-reflective";
        private const String PA_FIBROUS = "fibrous";

        //
        private const String PA_DDL_EVALUATION_OF_PARTICLE = "Evaluation of Particle:";
        private const String PA_DDL_TEST_ARRANGEMENT_ENV = "Test arrangement / Environment:";
        private const String PA_DDL_CONTAINER = "Container";
        private const String PA_DDL_FLUID1 = "Fluid 1";
        private const String PA_DDL_FLUID2 = "Fluid 2";
        private const String PA_DDL_FLUID3 = "Fluid 3";
        private const String PA_DDL_ANALYSIS_MEMBRANE_USED = "Analysis membrane used:";
        private const String PA_DDL_MANUFACTURER = "Manufacturer";
        private const String PA_DDL_MATERIAL = "Material";

        private const String PA_DDL_GRAVIMETRIC_ANALYSIS = "Gravimetric analysis:";
        private const String PA_DDL_LAB_BALANCE = "Lab Balance";

        private const String PA_DDL_SPECIFICATION_NO = "SpecificationNo";
        private const String PA_DDL_OPERATOR_NAME = "Operator Name";
        private const String PA_PER_LIST = "Per";


        #region "Property"
        public users_login UserLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public job_sample JobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public template_pa Pa
        {
            get { return (template_pa)Session[GetType().Name + "pa"]; }
            set { Session[GetType().Name + "pa"] = value; }
        }

        public List<template_pa_detail> PaDetail
        {
            get { return (List<template_pa_detail>)Session[GetType().Name + "paDetail"]; }
            set { Session[GetType().Name + "paDetail"] = value; }
        }

        public List<tb_m_specification> tbMSpecifications
        {
            get { return (List<tb_m_specification>)Session[GetType().Name + "tbMSpecifications"]; }
            set { Session[GetType().Name + "tbMSpecifications"] = value; }
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

        public List<String> DissolvingHeaders
        {
            get { return (List<String>)Session[GetType().Name + "DissolvingHeaders"]; }
            set { Session[GetType().Name + "DissolvingHeaders"] = value; }
        }
        public List<String> WashingHeaders
        {
            get { return (List<String>)Session[GetType().Name + "WashingHeaders"]; }
            set { Session[GetType().Name + "WashingHeaders"] = value; }
        }

        List<String> errors = new List<string>();
        #endregion

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "tbCas");
            Session.Remove(GetType().Name + "coverpages");
            Session.Remove(GetType().Name + "libs");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
            Session.Remove(GetType().Name + "DirectInject");
            Session.Remove(GetType().Name + "SampleSize");
            Session.Remove(GetType().Name + "Unit");
            Session.Remove(GetType().Name + "PercentRecovery");
            Session.Remove(GetType().Name + "HumID");

        }

        private void initialPage()
        {
            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), UserLogin.role_id.ToString(), true);

            this.CommandName = CommandNameEnum.Add;
            this.WashingHeaders = new List<string>();
            this.DissolvingHeaders = new List<string>();


            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            #region "SAMPLE"
            if (this.JobSample != null)
            {

                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
                lbJobStatus.Text = Constants.GetEnumDescription(status);
                ddlStatus.Items.Clear();

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pStatus.Visible = (status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.LABMANAGER_CHECKING);
                pUploadfile.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD);
                pDownload.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnSubmit.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);

                pPage01.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage02.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage03.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage04.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage05.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage06.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage07.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage08.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);


                //pEop.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);

                if (status == StatusEnum.LABMANAGER_CHECKING)
                {
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));

                }
                else if (status == StatusEnum.SR_CHEMIST_CHECKING)
                {
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                    pRemark.Visible = false;
                }

                txtDateAnalyzed.Text = (this.JobSample.date_chemist_analyze != null) ? this.JobSample.date_chemist_analyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;



                //txtAlsReferenceNo.Text = String.Format("ATT/ELN/{0}/{1}", DateTime.Now.Year.ToString().Substring(2, 2), this.jobSample.job_number);
                //txtPartDescription.Text = this.jobSample.description;
                //txtLotNo.Text = String.Empty;
            }
            #endregion

            template_pa_detail pad = new template_pa_detail();
            this.tbMSpecifications = new tb_m_specification().SelectBySpecificationID(this.JobSample.specification_id, this.JobSample.template_id);
            List<template_pa_detail> listPaDetail = pad.SelectBySampleID(this.SampleID);
            if (listPaDetail.Count > 0)
            {
                this.PaDetail = listPaDetail;
            }
            else
            {
                PaDetail = new List<template_pa_detail>();
            }

            #region "Initial component"

            ddlContainer.Items.Clear();
            ddlContainer.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_TEST_ARRANGEMENT_ENV) && x.C.Equals(PA_DDL_CONTAINER));
            ddlContainer.DataBind();
            //ddlContainer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid1.Items.Clear();
            ddlFluid1.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_TEST_ARRANGEMENT_ENV) && x.C.Equals(PA_DDL_FLUID1));
            ddlFluid1.DataBind();
            //ddlFluid1.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid2.Items.Clear();
            ddlFluid2.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_TEST_ARRANGEMENT_ENV) && x.C.Equals(PA_DDL_FLUID2));
            ddlFluid2.DataBind();
            //ddlFluid2.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid3.Items.Clear();
            ddlFluid3.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_TEST_ARRANGEMENT_ENV) && x.C.Equals(PA_DDL_FLUID3));
            ddlFluid3.DataBind();
            //ddlFluid3.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlManufacturer.Items.Clear();
            ddlManufacturer.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_ANALYSIS_MEMBRANE_USED) && x.C.Equals(PA_DDL_MANUFACTURER));
            ddlManufacturer.DataBind();
            //ddlManufacturer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlMaterial.Items.Clear();
            ddlMaterial.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_ANALYSIS_MEMBRANE_USED) && x.C.Equals(PA_DDL_MATERIAL));
            ddlMaterial.DataBind();
            //ddlMaterial.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlGravimetricAlalysis.Items.Clear();
            ddlGravimetricAlalysis.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_GRAVIMETRIC_ANALYSIS) && x.C.Equals(PA_DDL_LAB_BALANCE));
            ddlGravimetricAlalysis.DataBind();
            //ddlGravimetricAlalysis.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_SPECIFICATION_NO));
            ddlSpecification.DataBind();


            ddlOperatorName.Items.Clear();
            ddlOperatorName.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_OPERATOR_NAME));
            ddlOperatorName.DataBind();

            ddlPer.Items.Clear();
            ddlPer.DataSource = this.tbMSpecifications.Where(x => x.A.Equals(PA_PER_LIST));
            ddlPer.DataBind();



            #endregion

            this.Pa = new template_pa();
            this.Pa = Pa.SelectByID(this.SampleID);
            if (this.Pa != null)
            {
                ddlResult.SelectedValue = this.Pa.result.ToString();
                txtPIRTDC.Text = this.Pa.pirtd;
                ddlSpecification.SelectedValue = this.Pa.specification_no.ToString();
                this.PA_SPECIFICATION = ddlSpecification.SelectedItem.Text;

                #region "PAGE01"
                txtDoec.Text = this.Pa.doec;
                txtDos.Text = this.Pa.dos;
                txtCustomerLimit.Text = this.Pa.customerlimit;
                txtGravimetry.Text = this.Pa.gravimetry;
                txtLmsp.Text = this.Pa.lmsp;
                txtExtractionValue.Text = this.Pa.extractionvalue;
                txtLnmsp.Text = this.Pa.lnmsp;
                txtEop_G.Text = this.Pa.eop_g;
                txtEop_Lmsp.Text = this.Pa.eop_lmsp;
                txtEop_Lnmsp.Text = this.Pa.eop_lnmsp;
                txtEop_pt.Text = this.Pa.eop_pt;
                txtEop_size.Text = this.Pa.eop_size;
                txtEop_value.Text = this.Pa.eop_value;
                txtEopRemark.Text = this.Pa.remark;
                #endregion

                #region "PAGE02"
                CustomUtils.setCheckBoxListValue(ref cbCsa, this.Pa.iscsa);
                txtWspc.Text = this.Pa.wspc;
                txtWvpc.Text = this.Pa.wvpc;
                txtTls.Text = this.Pa.tls;
                cbPreTreatmentConditioning.Checked = Convert.ToBoolean(this.Pa.ispretreatmentconditioning);
                txtPreTreatmentConditioning.Text = this.Pa.pretreatmentconditioning;
                CustomUtils.setCheckBoxListValue(ref cbPackingToBeTested, this.Pa.ispackingtobetested);

                cbContainer.Checked = Convert.ToBoolean(this.Pa.iscontainer);
                ddlContainer.SelectedValue = this.Pa.container_id.ToString();

                cbFluid1.Checked = Convert.ToBoolean(this.Pa.isfluid1);
                ddlFluid1.SelectedValue = this.Pa.fluid1_id.ToString();
                cbFluid2.Checked = Convert.ToBoolean(this.Pa.isfluid2);
                ddlFluid2.SelectedValue = this.Pa.fluid2_id.ToString();
                cbFluid3.Checked = Convert.ToBoolean(this.Pa.isfluid3);
                ddlFluid3.SelectedValue = this.Pa.fluid3_id.ToString();

                txtTradeName.Text = this.Pa.tradename;
                txtManufacturer.Text = this.Pa.manufacturer;
                txtTotalQuantity.Text = this.Pa.totalquantity;
                cbTshb01.Checked = Convert.ToBoolean(this.Pa.istshb01);
                cbTshb02.Checked = Convert.ToBoolean(this.Pa.istshb02);
                cbTshb03.Checked = Convert.ToBoolean(this.Pa.istshb03);

                txtTshb03.Text = this.Pa.tshb03;
                cbPots01.Checked = Convert.ToBoolean(this.Pa.ispots01);
                txtPots01.Text = this.Pa.pots01;
                #endregion

                #region "PAGE03"
                cbDissolving.Checked = Convert.ToBoolean(this.Pa.isdissolving);
                txtDissolving.Text = this.Pa.dissolving;
                txtDissolvingTime.Text = this.Pa.dissolvingtime;
                cbPressureRinsing.Checked = Convert.ToBoolean(this.Pa.ispressurerinsing);
                cbAgitation.Checked = Convert.ToBoolean(this.Pa.isagitation);
                cbUntrasonic.Checked = Convert.ToBoolean(this.Pa.isUltrasonic);
                ddlRinsing.SelectedValue = this.Pa.rinsing_id.ToString();
                cbWashQuantity.Checked = Convert.ToBoolean(this.Pa.iswashquantity);
                txtWashQuantity.Text = this.Pa.washquantity;
                cbRewashingQuantity.Checked = Convert.ToBoolean(this.Pa.isrewashingquantity);
                txtRewashingQuantity.Text = this.Pa.rewashingquantity;
                cbWashPressureRinsing.Checked = Convert.ToBoolean(this.Pa.iswashpressurerinsing);
                cbWashAgitation.Checked = Convert.ToBoolean(this.Pa.iswashagitation);
                cbWashUltrasonic.Checked = Convert.ToBoolean(this.Pa.iswashUltrasonic);
                dllWashPressureRinsing.SelectedValue = this.Pa.washpressurerinsing_id.ToString();
                CustomUtils.setCheckBoxListValue(ref cbFiltrationMethod, this.Pa.isfiltrationmethod);

                ddlManufacturer.SelectedValue = this.Pa.manufacturer_id.ToString();
                ddlMaterial.SelectedValue = this.Pa.material_id.ToString();
                txtPoreSize.Text = this.Pa.poresize;
                txtDiameter.Text = this.Pa.diameter;
                cbOven.Checked = Convert.ToBoolean(this.Pa.isoven);
                cbDesiccator.Checked = Convert.ToBoolean(this.Pa.isdesiccator);
                cbAmbientAir.Checked = Convert.ToBoolean(this.Pa.isambientair);
                cbEasyDry.Checked = Convert.ToBoolean(this.Pa.iseasydry);
                txtDryTime.Text = this.Pa.drytime;
                txtTemperature.Text = this.Pa.temperature;
                ddlGravimetricAlalysis.SelectedValue = this.Pa.gravimetricalalysis_id.ToString();
                txtModel.Text = this.Pa.model;
                txtBalanceResolution.Text = this.Pa.balanceresolution;
                txtLastCalibration.Text = this.Pa.lastcalibration;
                cbZEISSAxioImager2.Checked = Convert.ToBoolean(this.Pa.iszeissaxioimager2);
                cbMeasuringSoftware.Checked = Convert.ToBoolean(this.Pa.ismeasuringsoftware);
                cbAutomated.Checked = Convert.ToBoolean(this.Pa.isautomated);
                txtAutomated.Text = this.Pa.automated;
                txtTotalextractionVolume.Text = this.Pa.totalextractionvolume;
                lbExtractionMethod.Text = this.Pa.lbextractionmethod;
                txtNumberOfComponents.Text = this.Pa.numberofcomponents;
                lbExtractionTime.Text = this.Pa.lbextractiontime;
                lbMembraneType.Text = this.Pa.measureddiameter;
                lbX.Text = this.Pa.lbx;
                lbY.Text = this.Pa.lby;
                txtMeasuredDiameter.Text = this.Pa.measureddiameter;
                txtFeretLmsp.Text = this.Pa.feretlmsp;
                txtFeretLnms.Text = this.Pa.feretlnms;
                txtFeretFb.Text = this.Pa.feretfb;

                txtLms.Text = this.Pa.feretlmsp;
                txtLnmp.Text = this.Pa.feretlnms;
                txtLf.Text = this.Pa.feretfb;
                #endregion

                img1.ImageUrl = this.Pa.img01;
                img2.ImageUrl = this.Pa.img02;
                img3.ImageUrl = this.Pa.img03;
                img4.ImageUrl = this.Pa.img04;
                img5.ImageUrl = this.Pa.img05;

                img6.ImageUrl = this.Pa.img06;
                img7.ImageUrl = this.Pa.img07;
                img8.ImageUrl = this.Pa.img08;
                img9.ImageUrl = this.Pa.img09;
                img10.ImageUrl = this.Pa.img10;
                img11.ImageUrl = this.Pa.img11;



                Image1.ImageUrl = this.Pa.attachment_ii_01;
                Image2.ImageUrl = this.Pa.attachment_ii_02;
                Image3.ImageUrl = this.Pa.attachment_ii_03;
                Image4.ImageUrl = this.Pa.attachment_ii_04;



                txtLms_X.Text = this.Pa.lms_x;
                txtLms_Y.Text = this.Pa.lms_y;

                txtLnms_X.Text = this.Pa.lnms_x;
                txtLnms_Y.Text = this.Pa.lnms_y;

                txtLf_X.Text = this.Pa.lf_x;
                txtLf_Y.Text = this.Pa.lf_y;

                txtLms_X_R2.Text = this.Pa.lms_x_r2;
                txtLms_Y_R2.Text = this.Pa.lms_y_r2;
                txtLnms_X_R2.Text = this.Pa.lnms_x_r2;
                txtLnms_Y_R2.Text = this.Pa.lnms_y_r2;
                txtLf_X_R2.Text = this.Pa.lf_x_r2;
                txtLf_Y_R2.Text = this.Pa.lf_y_r2;

                txtLms_X_R3.Text = this.Pa.lms_x_r3;
                txtLms_Y_R3.Text = this.Pa.lms_y_r3;
                txtLnms_X_R3.Text = this.Pa.lnms_x_r3;
                txtLnms_Y_R3.Text = this.Pa.lnms_y_r3;
                txtLf_X_R3.Text = this.Pa.lf_x_r3;
                txtLf_Y_R3.Text = this.Pa.lf_y_r3;


                lbX.Text = txtAutomated.Text;
                lbY.Text = txtAutomated.Text;


                txtParamMagnification1.Text = this.Pa.param_magnification_01;
                txtParamMagnification2.Text = this.Pa.param_magnification_02;
                txtParamWd1.Text = this.Pa.param_wd_01;
                txtParamWd2.Text = this.Pa.param_wd_02;
                txtParamEht1.Text = this.Pa.param_eht_01;
                txtParamEht2.Text = this.Pa.param_eht_02;
                txtParamDetector1.Text = this.Pa.param_detector_01;
                txtParamDetector2.Text = this.Pa.param_detector_02;

                ddlSpecification.SelectedValue = this.Pa.specification_no.ToString();
                ddlOperatorName.SelectedValue = this.Pa.operater_name.ToString();


                txtPerComponentTotal.Text = this.Pa.per_component_total;
                txtPerComponentMetallicShine.Text = this.Pa.per_component_metallicshine;
                txtPermembraneMetallicShine.Text = this.Pa.per_membrane_metallicshine;

                lbPer.Text = this.Pa.per_text;


                #region "COLUMN HEADER"
                tb_m_specification selectValue = new tb_m_specification();
                #region "gvEop"
                List<tb_m_specification> listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_EVALUATION_OF_PARTICLE) && x.B.Equals(PA_SPECIFICATION)).ToList();
                if (listOfSpec.Count > 0)
                {
                    tb_m_specification item = listOfSpec.FirstOrDefault();
                    //
                    gvEop.Columns[0].HeaderText = item.C;
                    gvEop.Columns[1].HeaderText = item.D;
                    gvEop.Columns[2].HeaderText = item.E;
                    gvEop.Columns[3].HeaderText = item.F;
                    gvEop.Columns[4].HeaderText = item.G;
                    gvEop.Columns[5].HeaderText = item.H;
                    gvEop.Columns[6].HeaderText = item.I;
                    gvEop.Columns[7].HeaderText = item.J;
                    gvEop.Columns[8].HeaderText = item.K;
                    gvEop.Columns[9].HeaderText = item.L;
                    gvEop.Columns[10].HeaderText = item.M;
                    gvEop.Columns[11].HeaderText = item.N;


                    gvEop.Columns[0].Visible = !String.IsNullOrEmpty(gvEop.Columns[0].HeaderText);
                    gvEop.Columns[1].Visible = !String.IsNullOrEmpty(gvEop.Columns[1].HeaderText);
                    gvEop.Columns[2].Visible = !String.IsNullOrEmpty(gvEop.Columns[2].HeaderText);
                    gvEop.Columns[3].Visible = !String.IsNullOrEmpty(gvEop.Columns[3].HeaderText);
                    gvEop.Columns[4].Visible = !String.IsNullOrEmpty(gvEop.Columns[4].HeaderText);
                    gvEop.Columns[5].Visible = !String.IsNullOrEmpty(gvEop.Columns[5].HeaderText);
                    gvEop.Columns[6].Visible = !String.IsNullOrEmpty(gvEop.Columns[6].HeaderText);
                    gvEop.Columns[7].Visible = !String.IsNullOrEmpty(gvEop.Columns[7].HeaderText);
                    gvEop.Columns[8].Visible = !String.IsNullOrEmpty(gvEop.Columns[8].HeaderText);
                    gvEop.Columns[9].Visible = !String.IsNullOrEmpty(gvEop.Columns[9].HeaderText);
                    gvEop.Columns[10].Visible = !String.IsNullOrEmpty(gvEop.Columns[10].HeaderText);
                    gvEop.Columns[11].Visible = !String.IsNullOrEmpty(gvEop.Columns[11].HeaderText);
                    //

                }
                #endregion
                #region "gvDissolving"

                if (cbPressureRinsing.Checked)
                {
                    switch (ddlRinsing.SelectedValue)
                    {
                        case "0":
                            selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                            break;
                        case "1":
                            selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_INTERNAL_RINSING)).FirstOrDefault();
                            break;
                    }

                }
                if (cbAgitation.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                }
                if (cbUntrasonic.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_ULTRASONIC)).FirstOrDefault();
                }



                this.DissolvingHeaders = tb_m_specification.findColumnCount(selectValue);
                for (int i = 0; i < this.DissolvingHeaders.Count; i++)
                {
                    gvDissolving.Columns[i].HeaderText = this.DissolvingHeaders[i];
                    gvDissolving.Columns[i].Visible = true;
                }
                #endregion
                #region "gvWashing"
                if (cbWashPressureRinsing.Checked)
                {
                    switch (dllWashPressureRinsing.SelectedValue)
                    {
                        case "0":
                            selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                            break;
                        case "1":
                            selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_INTERNAL_RINSING)).FirstOrDefault();
                            break;
                    }
                }

                if (cbWashAgitation.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                }
                if (cbWashUltrasonic.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_ULTRASONIC)).FirstOrDefault();
                }

                this.WashingHeaders = tb_m_specification.findColumnCount(selectValue);
                for (int i = 0; i < this.WashingHeaders.Count; i++)
                {
                    gvWashing.Columns[i].HeaderText = this.WashingHeaders[i];
                    gvWashing.Columns[i].Visible = true;
                }
                #endregion

                #endregion
                membraneType();

            }
            else
            {
                this.Pa = new template_pa();

                this.PA_SPECIFICATION = ddlSpecification.SelectedItem.Text;

                tb_m_specification selectedSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_SPECIFICATION_NO) && x.C.Equals(this.PA_SPECIFICATION)).FirstOrDefault();
                if (selectedSpec != null)
                {
                    lbP1Note.Text = selectedSpec.D ?? String.Empty;
                    lbP1Remark.Text = selectedSpec.E ?? String.Empty;
                }

                #region "gvEop"
                List<tb_m_specification> listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_EVALUATION_OF_PARTICLE) && x.B.Equals(PA_SPECIFICATION)).ToList();
                if (listOfSpec.Count > 0)
                {

                    int seq = 1;
                    foreach (tb_m_specification item in listOfSpec)
                    {
                        if (seq > 1)
                        {
                            template_pa_detail tmp = new template_pa_detail
                            {
                                id = CustomUtils.GetRandomNumberID(),
                                seq = seq,
                                col_c = item.C,
                                col_d = item.D,
                                col_e = item.E,
                                col_f = item.F,
                                col_g = item.G,
                                col_h = item.H,
                                col_i = item.I,
                                col_j = item.J,
                                col_k = item.K,
                                col_l = item.L,
                                col_m = item.M,
                                col_n = item.N,
                                row_status = Convert.ToInt16(RowTypeEnum.Normal),
                                row_type = Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)
                            };
                            PaDetail.Add(tmp);

                        }
                        else
                        {
                            //
                            gvEop.Columns[0].HeaderText = item.C;
                            gvEop.Columns[1].HeaderText = item.D;
                            gvEop.Columns[2].HeaderText = item.E;
                            gvEop.Columns[3].HeaderText = item.F;
                            gvEop.Columns[4].HeaderText = item.G;
                            gvEop.Columns[5].HeaderText = item.H;
                            gvEop.Columns[6].HeaderText = item.I;
                            gvEop.Columns[7].HeaderText = item.J;
                            gvEop.Columns[8].HeaderText = item.K;
                            gvEop.Columns[9].HeaderText = item.L;
                            gvEop.Columns[10].HeaderText = item.M;
                            gvEop.Columns[11].HeaderText = item.N;


                            gvEop.Columns[0].Visible = !String.IsNullOrEmpty(gvEop.Columns[0].HeaderText);
                            gvEop.Columns[1].Visible = !String.IsNullOrEmpty(gvEop.Columns[1].HeaderText);
                            gvEop.Columns[2].Visible = !String.IsNullOrEmpty(gvEop.Columns[2].HeaderText);
                            gvEop.Columns[3].Visible = !String.IsNullOrEmpty(gvEop.Columns[3].HeaderText);
                            gvEop.Columns[4].Visible = !String.IsNullOrEmpty(gvEop.Columns[4].HeaderText);
                            gvEop.Columns[5].Visible = !String.IsNullOrEmpty(gvEop.Columns[5].HeaderText);
                            gvEop.Columns[6].Visible = !String.IsNullOrEmpty(gvEop.Columns[6].HeaderText);
                            gvEop.Columns[7].Visible = !String.IsNullOrEmpty(gvEop.Columns[7].HeaderText);
                            gvEop.Columns[8].Visible = !String.IsNullOrEmpty(gvEop.Columns[8].HeaderText);
                            gvEop.Columns[9].Visible = !String.IsNullOrEmpty(gvEop.Columns[9].HeaderText);
                            gvEop.Columns[10].Visible = !String.IsNullOrEmpty(gvEop.Columns[10].HeaderText);
                            gvEop.Columns[11].Visible = !String.IsNullOrEmpty(gvEop.Columns[11].HeaderText);
                            //
                        }
                        seq++;
                    }

                }
                #endregion
                #region "Microscopic Analysis"
                listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_MICROPIC_DATA) && x.B.Equals(PA_SPECIFICATION)).ToList();
                if (listOfSpec.Count > 1)
                {

                    int row = 1;
                    foreach (var item in listOfSpec)
                    {
                        template_pa_detail tmp = new template_pa_detail
                        {
                            id = CustomUtils.GetRandomNumberID(),
                            seq = row,
                            col_a = item.A,
                            col_b = item.B,
                            col_c = item.C,
                            col_d = item.D,
                            col_e = item.E,
                            col_f = item.G,
                            col_g = item.G,
                            col_h = item.H,
                            col_i = item.I,
                            col_j = item.J,
                            col_k = item.K,
                            col_l = item.L,
                            col_m = item.M,
                            col_n = item.N,
                            col_o = item.O,
                            col_p = item.P,
                            col_q = item.Q,
                            col_r = item.R,

                            col_s = item.S,
                            col_t = item.T,
                            col_u = item.U,
                            col_v = item.V,
                            col_w = item.W,
                            col_x = item.X,
                            col_y = item.Y,
                            col_z = item.Z,

                            row_status = Convert.ToInt16(RowTypeEnum.Normal),
                            row_type = Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)
                        };
                        PaDetail.Add(tmp);
                        row++;
                    }
                }
                #endregion

                //default:Agitation
                lbExtractionMethod.Text = PA_AGITATION;
                tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }


                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvDissolving.Columns[i].HeaderText = cols[i];
                        gvDissolving.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail
                    {
                        id = CustomUtils.GetRandomNumberID(),
                        col_d = "42 KHz",
                        col_e = "24 W/L",
                        col_f = "8 mins",
                        col_g = "room temperature",
                        col_h = "No",
                        row_status = Convert.ToInt16(RowTypeEnum.Normal),
                        row_type = Convert.ToInt16(PAEnum.DISSOLVING)
                    };
                    PaDetail.Add(tmp);

                }

                selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }


                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvWashing.Columns[i].HeaderText = cols[i];
                        gvWashing.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail
                    {
                        id = CustomUtils.GetRandomNumberID(),
                        col_d = "Flat type",
                        col_e = "40",
                        col_f = "0.5 L/min",
                        col_g = "1 bar",
                        col_h = "-",
                        row_status = Convert.ToInt16(RowTypeEnum.Normal),
                        row_type = Convert.ToInt16(PAEnum.WASHING)
                    };
                    PaDetail.Add(tmp);

                }
                ////
                selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddlFluid1.SelectedValue)).FirstOrDefault();
                if (null != selectValue)
                {
                    txtTradeName.Text = selectValue.E;
                    txtManufacturer.Text = selectValue.F;
                }
            }

            pPage01.Visible = true;
            pPage02.Visible = false;
            pPage03.Visible = false;
            pPage04.Visible = false;
            pPage05.Visible = false;
            pPage06.Visible = false;
            pPage07.Visible = false;
            pPage08.Visible = false;
            pUploadWorkSheet.Visible = false;
            btnSubmit.Enabled = false;

            ReportHeader reportHeader = ReportHeader.getReportHeder(this.JobSample);
            pdCustomer.Text = reportHeader.addr1;
            pdAlsRefNo.Text = reportHeader.alsRefNo;
            pdPartName.Text = this.JobSample.part_name;
            pdAnalysisDate.Text = reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy");
            paPartNo.Text = this.JobSample.part_no;
            pdLotNo.Text = this.JobSample.lot_no;

            btnSrChemistTest.Visible = userRole == RoleEnum.SR_CHEMIST;
            calculate();

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

        private void calculate()
        {
            lbPermembraneTotal.Text = String.Empty;
            txtPermembraneMetallicShine.Text = String.Empty;

            List<template_pa_detail> listMicroPicData = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).OrderBy(x => x.seq).ToList();
            List<template_pa_detail> listPaDetail = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).OrderBy(x => x.seq).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                Boolean isPrv = ddlSpecification.SelectedItem.Text.Equals("PA5x_BOSCH0442S00155PRV") ? true : false;
                template_pa_detail val = null;
                if (gvEop.Columns[1].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[1].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_d = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[2].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[2].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_e = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[3].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[3].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_f = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[4].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[4].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_g = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[5].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[5].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_h = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[6].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[6].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_i = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[7].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[7].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_j = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[8].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[8].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_k = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[9].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[9].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_l = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[10].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[10].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_m = isPrv ? val.col_i : val.col_e;
                    }
                }
                if (gvEop.Columns[11].Visible)
                {
                    val = listMicroPicData.Where(x => x.col_c.Equals(mappingRawData(gvEop.Columns[11].HeaderText))).FirstOrDefault();
                    if (val != null)
                    {
                        listPaDetail[1].col_n = isPrv ? val.col_i : val.col_e;
                    }
                }





                //headerRow                                                  
                gvEop.DataSource = listPaDetail;
                gvEop.DataBind();
            }
            if (null != listMicroPicData && listMicroPicData.Count > 0)
            {
                int numberOfComponents = !String.IsNullOrEmpty(txtNumberOfComponents.Text) ? Convert.ToInt32(txtNumberOfComponents.Text) : 0;
                //Cal:Particles on membrane(Total)
                foreach (var pad in listMicroPicData)
                {
                    if (CustomUtils.isNumber(pad.col_f) && CustomUtils.isNumber(pad.col_g) && CustomUtils.isNumber(pad.col_h))
                    {
                        pad.col_e = (Convert.ToDouble(pad.col_f) + Convert.ToDouble(pad.col_g)).ToString("");
                        if (numberOfComponents > 0)
                        {
                            double _col_e = !CustomUtils.isNumber(pad.col_e) ? 0 : Convert.ToDouble(pad.col_e);
                            double _col_f = !CustomUtils.isNumber(pad.col_f) ? 0 : Convert.ToDouble(pad.col_f);
                            double _col_g = !CustomUtils.isNumber(pad.col_g) ? 0 : Convert.ToDouble(pad.col_g);
                            double _col_h = !CustomUtils.isNumber(pad.col_h) ? 0 : Convert.ToDouble(pad.col_h);

                            pad.col_i = (Convert.ToDouble(_col_e) / numberOfComponents).ToString("N" + txtDecimal01.Text);
                            pad.col_j = (Convert.ToDouble(_col_f) / numberOfComponents).ToString("N" + txtDecimal02.Text);
                            pad.col_k = (Convert.ToDouble(_col_g) / numberOfComponents).ToString("N" + txtDecimal03.Text);
                            pad.col_l = (Convert.ToDouble(_col_h) / numberOfComponents).ToString("N" + txtDecimal04.Text);
                        }

                        if (!pad.col_d.Equals("-"))
                        {
                            lbPermembraneTotal.Text += String.Format("{0}{1}/", pad.col_d, Convert.ToDouble(pad.col_i).ToString("N0"));
                            txtPermembraneMetallicShine.Text += String.Format("{0}{1}/", pad.col_d, Convert.ToDouble(pad.col_k).ToString("N0"));
                        }

                    }
                    else
                    {
                        pad.col_e = String.IsNullOrEmpty(pad.col_e) ? "Not to evaluate" : pad.col_e;
                        pad.col_f = String.IsNullOrEmpty(pad.col_f) ? "Not to evaluate" : pad.col_f;
                        pad.col_g = String.IsNullOrEmpty(pad.col_g) ? "Not to evaluate" : pad.col_g;
                        pad.col_h = String.IsNullOrEmpty(pad.col_h) ? "Not to evaluate" : pad.col_h;
                        pad.col_i = String.IsNullOrEmpty(pad.col_i) ? "Not to evaluate" : pad.col_i;
                        pad.col_j = String.IsNullOrEmpty(pad.col_j) ? "Not to evaluate" : pad.col_j;
                        pad.col_k = String.IsNullOrEmpty(pad.col_k) ? "Not to evaluate" : pad.col_k;
                        pad.col_l = String.IsNullOrEmpty(pad.col_l) ? "Not to evaluate" : pad.col_l;
                        pad.col_m = String.IsNullOrEmpty(pad.col_m) ? "Not to evaluate" : pad.col_m;
                        pad.col_n = String.IsNullOrEmpty(pad.col_n) ? "Not to evaluate" : pad.col_n;
                    }
                }


                if (!String.IsNullOrEmpty(lbPermembraneTotal.Text))
                {
                    lbPermembraneTotal.Text = String.Format("N({0})", lbPermembraneTotal.Text.Substring(0, lbPermembraneTotal.Text.Length - 1));
                }
                else
                {
                    lbPermembraneTotal.Text = "-";
                }
                if (!String.IsNullOrEmpty(txtPermembraneMetallicShine.Text))
                {
                    txtPermembraneMetallicShine.Text = String.Format("N({0})", txtPermembraneMetallicShine.Text.Substring(0, txtPermembraneMetallicShine.Text.Length - 1));
                }
                else
                {
                    txtPermembraneMetallicShine.Text = "-";
                }
                gvMicroscopicAnalysis.Visible = true;
                gvMicroscopicAnalysis.DataSource = listMicroPicData;
                gvMicroscopicAnalysis.DataBind();
            }
            listPaDetail = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {

                gvDissolving.DataSource = listPaDetail;
                gvDissolving.DataBind();
                //lbExtractionTime.Text = listPaDetail[0].col_f;
            }
            listPaDetail = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvWashing.DataSource = listPaDetail;
                gvWashing.DataBind();
            }
            listPaDetail = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("1")).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvCompositionElement.DataSource = listPaDetail;
                gvCompositionElement.DataBind();
            }
            listPaDetail = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("2")).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvCompositionElement2.DataSource = listPaDetail;
                gvCompositionElement2.DataBind();
            }






            //fil data "LargestRegionsTable"
            txtLms.Text = txtFeretLmsp.Text;
            txtLnmp.Text = txtFeretLnms.Text;
            txtLf.Text = txtFeretFb.Text;
            txtEop_Lmsp.Text = txtFeretLmsp.Text;
            txtEop_Lnmsp.Text = txtFeretLnms.Text;
            lbLf.Text = txtFeretFb.Text;
            lbTotalResidueWeight.Text = txtEop_G.Text;
            lbExtractionTime.Text = txtDissolvingTime.Text;

            pdSpecification.Text = ddlSpecification.SelectedItem.Text;

            if (CustomUtils.isNumber(txtDissolving.Text) && CustomUtils.isNumber(txtWashQuantity.Text) && CustomUtils.isNumber(txtRewashingQuantity.Text))
            {
                txtTotalQuantity.Text = (Convert.ToDouble(txtDissolving.Text) + Convert.ToDouble(txtWashQuantity.Text) + Convert.ToDouble(txtRewashingQuantity.Text)) + "";
            }
            txtTotalextractionVolume.Text = txtTotalQuantity.Text;
            lbX.Text = txtAutomated.Text;
            lbY.Text = txtAutomated.Text;
            lbPer.Text = ddlPer.SelectedItem.Text;
        }

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

        #region "Button"

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Boolean isValid = true;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.JobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);

                    break;
                case StatusEnum.CHEMIST_TESTING:

                    this.JobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.JobSample.step2owner = UserLogin.id;
                    this.JobSample.path_word = String.Empty;
                    this.JobSample.path_pdf = String.Empty;
                    #region ":: STAMP COMPLETE DATE"
                    this.JobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    this.JobSample.date_chemist_complete = DateTime.Now;
                    this.JobSample.date_srchemist_analyze = DateTime.Now;
                    #endregion
                    this.Pa.sample_id = this.SampleID;
                    this.Pa.result = Convert.ToInt32(ddlResult.SelectedValue);
                    this.Pa.pirtd = txtPIRTDC.Text;

                    #region "PAGE01"
                    this.Pa.lms = txtLms.Text;
                    this.Pa.lnmp = txtLnmp.Text;
                    this.Pa.lf = txtLf.Text;
                    this.Pa.doec = txtDoec.Text;
                    this.Pa.dos = txtDos.Text;
                    this.Pa.customerlimit = txtCustomerLimit.Text;
                    this.Pa.gravimetry = txtGravimetry.Text;
                    this.Pa.lmsp = txtLmsp.Text;
                    this.Pa.extractionvalue = txtExtractionValue.Text;
                    this.Pa.lnmsp = txtLnmsp.Text;
                    this.Pa.eop_g = txtEop_G.Text;
                    this.Pa.eop_lmsp = txtEop_Lmsp.Text;
                    this.Pa.eop_lnmsp = txtEop_Lnmsp.Text;
                    this.Pa.eop_pt = txtEop_pt.Text;
                    this.Pa.eop_size = txtEop_size.Text;
                    this.Pa.eop_value = txtEop_value.Text;
                    this.Pa.remark = txtEopRemark.Text;
                    #endregion
                    #region "PAGE02"
                    this.Pa.iscsa = CustomUtils.getCheckBoxListValue(cbCsa);
                    this.Pa.wspc = txtWspc.Text;
                    this.Pa.wvpc = txtWvpc.Text;
                    this.Pa.tls = txtTls.Text;
                    this.Pa.ispretreatmentconditioning = (cbPreTreatmentConditioning.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.pretreatmentconditioning = txtPreTreatmentConditioning.Text;
                    this.Pa.ispackingtobetested = CustomUtils.getCheckBoxListValue(cbPackingToBeTested);
                    this.Pa.iscontainer = (cbContainer.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.container_id = Convert.ToInt32(ddlContainer.SelectedValue);
                    this.Pa.isfluid1 = (cbFluid1.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.fluid1_id = Convert.ToInt32(ddlFluid1.SelectedValue);
                    this.Pa.isfluid2 = (cbFluid2.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.fluid2_id = Convert.ToInt32(ddlFluid2.SelectedValue);
                    this.Pa.isfluid3 = (cbFluid3.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.fluid3_id = Convert.ToInt32(ddlFluid3.SelectedValue);
                    this.Pa.tradename = txtTradeName.Text;
                    this.Pa.manufacturer = txtManufacturer.Text;
                    this.Pa.totalquantity = txtTotalQuantity.Text;
                    this.Pa.istshb01 = (cbTshb01.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.istshb02 = (cbTshb02.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.istshb03 = (cbTshb03.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.tshb03 = txtTshb03.Text;
                    this.Pa.ispots01 = (cbPots01.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.pots01 = txtPots01.Text;
                    #endregion

                    #region "PAGE03"
                    this.Pa.isdissolving = (cbDissolving.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.dissolving = txtDissolving.Text;
                    this.Pa.dissolvingtime = txtDissolvingTime.Text;
                    this.Pa.ispressurerinsing = (cbPressureRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.isagitation = (cbAgitation.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.isUltrasonic = (cbUntrasonic.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.rinsing_id = Convert.ToInt16(ddlRinsing.SelectedValue);
                    this.Pa.iswashquantity = (cbWashQuantity.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.washquantity = txtWashQuantity.Text;
                    this.Pa.isrewashingquantity = (cbRewashingQuantity.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.rewashingquantity = txtRewashingQuantity.Text;
                    this.Pa.iswashpressurerinsing = (cbWashPressureRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.iswashagitation = (cbWashAgitation.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.iswashUltrasonic = (cbWashUltrasonic.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.washpressurerinsing_id = Convert.ToInt16(dllWashPressureRinsing.SelectedValue);
                    this.Pa.isfiltrationmethod = CustomUtils.getCheckBoxListValue(cbFiltrationMethod);


                    this.Pa.manufacturer_id = Convert.ToInt32(ddlManufacturer.SelectedValue);
                    this.Pa.material_id = Convert.ToInt32(ddlMaterial.SelectedValue);
                    this.Pa.poresize = txtPoreSize.Text;
                    this.Pa.diameter = txtDiameter.Text;
                    this.Pa.isoven = (cbOven.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.isdesiccator = (cbDesiccator.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.isambientair = (cbAmbientAir.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.iseasydry = (cbEasyDry.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.drytime = txtDryTime.Text;
                    this.Pa.temperature = txtTemperature.Text;
                    this.Pa.gravimetricalalysis_id = Convert.ToInt32(ddlGravimetricAlalysis.SelectedValue);
                    this.Pa.model = txtModel.Text;
                    this.Pa.balanceresolution = txtBalanceResolution.Text;
                    this.Pa.lastcalibration = txtLastCalibration.Text;
                    this.Pa.iszeissaxioimager2 = (cbZEISSAxioImager2.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.ismeasuringsoftware = (cbMeasuringSoftware.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.isautomated = (cbAutomated.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.Pa.automated = txtAutomated.Text;
                    this.Pa.totalextractionvolume = txtTotalextractionVolume.Text;
                    this.Pa.lbextractionmethod = lbExtractionMethod.Text;
                    this.Pa.numberofcomponents = txtNumberOfComponents.Text;
                    this.Pa.lbextractiontime = lbExtractionTime.Text;
                    this.Pa.measureddiameter = lbMembraneType.Text;
                    this.Pa.lbx = lbX.Text;
                    this.Pa.lby = lbY.Text;
                    this.Pa.measureddiameter = txtMeasuredDiameter.Text;
                    this.Pa.feretlmsp = txtFeretLmsp.Text;
                    this.Pa.feretlnms = txtFeretLnms.Text;
                    this.Pa.feretfb = txtFeretFb.Text;
                    #endregion

                    this.Pa.img01 = img1.ImageUrl;
                    this.Pa.img02 = img2.ImageUrl;
                    this.Pa.img03 = img3.ImageUrl;
                    this.Pa.img04 = img4.ImageUrl;
                    this.Pa.img05 = img5.ImageUrl;

                    this.Pa.lms_x = txtLms_X.Text;
                    this.Pa.lms_y = txtLms_Y.Text;
                    this.Pa.lnms_x = txtLnms_X.Text;
                    this.Pa.lnms_y = txtLnms_Y.Text;
                    this.Pa.lf_x = txtLf_X.Text;
                    this.Pa.lf_y = txtLf_Y.Text;

                    this.Pa.lms_x_r2 = txtLms_X_R2.Text;
                    this.Pa.lms_y_r2 = txtLms_Y_R2.Text;
                    this.Pa.lnms_x_r2 = txtLnms_X_R2.Text;
                    this.Pa.lnms_y_r2 = txtLnms_Y_R2.Text;
                    this.Pa.lf_x_r2 = txtLf_X_R2.Text;
                    this.Pa.lf_y_r2 = txtLf_Y_R2.Text;

                    this.Pa.lms_x_r3 = txtLms_X_R3.Text;
                    this.Pa.lms_y_r3 = txtLms_Y_R3.Text;
                    this.Pa.lnms_x_r3 = txtLnms_X_R3.Text;
                    this.Pa.lnms_y_r3 = txtLnms_Y_R3.Text;
                    this.Pa.lf_x_r3 = txtLf_X_R3.Text;
                    this.Pa.lf_y_r3 = txtLf_Y_R3.Text;






                    this.Pa.param_magnification_01 = txtParamMagnification1.Text;
                    this.Pa.param_magnification_02 = txtParamMagnification2.Text;
                    this.Pa.param_wd_01 = txtParamWd1.Text;
                    this.Pa.param_wd_02 = txtParamWd2.Text;
                    this.Pa.param_eht_01 = txtParamEht1.Text;
                    this.Pa.param_eht_02 = txtParamEht2.Text;
                    this.Pa.param_detector_01 = txtParamDetector1.Text;
                    this.Pa.param_detector_02 = txtParamDetector2.Text;

                    this.Pa.per_component_total = txtPerComponentTotal.Text;
                    this.Pa.per_component_metallicshine = txtPerComponentMetallicShine.Text;
                    this.Pa.per_membrane_metallicshine = txtPermembraneMetallicShine.Text;

                    this.Pa.specification_no = Convert.ToInt32(ddlSpecification.SelectedValue);
                    this.Pa.operater_name = Convert.ToInt32(ddlOperatorName.SelectedValue);

                    this.Pa.per_text = lbPer.Text;
                    //Delete old
                    template_pa.DeleteBySampleID(this.SampleID);
                    this.Pa.Insert();
                    template_pa_detail.DeleteBySampleID(this.SampleID);
                    foreach (template_pa_detail item in this.PaDetail)
                    {
                        item.sample_id = this.SampleID;
                    }
                    template_pa_detail.InsertList(this.PaDetail);

                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"
                            this.JobSample.date_srchemist_complate = DateTime.Now;
                            this.JobSample.date_admin_word_inprogress = DateTime.Now;
                            #endregion
                            break;
                        case StatusEnum.SR_CHEMIST_DISAPPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.JobSample.ID,
                                log_title = String.Format("Sr.Chemist DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.JobSample.step4owner = UserLogin.id;
                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                    StatusEnum labApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (labApproveStatus)
                    {
                        case StatusEnum.LABMANAGER_APPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);

                            this.JobSample.date_labman_complete = DateTime.Now;
                            this.JobSample.date_admin_pdf_inprogress = DateTime.Now;
                            break;
                        case StatusEnum.LABMANAGER_DISAPPROVE:
                            this.JobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.JobSample.ID,
                                log_title = String.Format("Lab Manager DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.JobSample.step5owner = UserLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (FileUpload1.HasFile)// && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(FileUpload1.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload1.SaveAs(source_file);
                        this.JobSample.path_word = source_file_url;
                        this.JobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        this.JobSample.date_admin_word_complete = DateTime.Now;
                        this.JobSample.date_labman_analyze = DateTime.Now;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                    }
                    this.JobSample.step6owner = UserLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (FileUpload1.HasFile) // && (Path.GetExtension(FileUpload1.FileName).Equals(".pdf")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(FileUpload1.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload1.SaveAs(source_file);
                        this.JobSample.path_pdf = source_file_url;
                        this.JobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        this.JobSample.step7owner = UserLogin.id;
                        this.JobSample.date_admin_pdf_complete = DateTime.Now;
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
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
                this.JobSample.Update();

                //Commit
                GeneralManager.Commit();

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }




        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.PreviousPath);
        }

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            pUploadWorkSheet.Visible = false;
            btnSubmit.Enabled = false;
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnPage01":
                    btnPage01.CssClass = "btn red-sunglo btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = true;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage02":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn red-sunglo btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = true;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage03":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn red-sunglo btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = true;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage04":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn red-sunglo btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = true;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage05":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn red-sunglo btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = true;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage06":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn red-sunglo btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = true;
                    pPage07.Visible = false;
                    pPage08.Visible = false;
                    break;
                case "btnPage07":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn red-sunglo btn-sm";
                    btnPage08.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = true;
                    pPage08.Visible = false;
                    pUploadWorkSheet.Visible = true;
                    btnSubmit.Enabled = true;
                    break;
                case "btnPage08":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    btnPage04.CssClass = "btn btn-default btn-sm";
                    btnPage05.CssClass = "btn btn-default btn-sm";
                    btnPage06.CssClass = "btn btn-default btn-sm";
                    btnPage07.CssClass = "btn btn-default btn-sm";
                    btnPage08.CssClass = "btn red-sunglo btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    pPage04.Visible = false;
                    pPage05.Visible = false;
                    pPage06.Visible = false;
                    pPage07.Visible = false;
                    pPage08.Visible = true;
                    break;
            }
            calculate();
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            String[] filterList = { PA_REFLECTIVE, PA_NON_REFLECTIVE, PA_FIBROUS };
            Double largestMetallicShine = 0;
            Double largestNonMetallicShine = 0;
            Double longestFiber = 0;
            string sheetName = string.Empty;
            List<template_pa_detail> paList = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();

            List<tb_m_dhs_cas> _cas = new List<tb_m_dhs_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < FileUpload2.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload2.PostedFiles[i];
                //try
                //{
                if (_postedFile.ContentLength > 0)
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(_postedFile.FileName));

                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    _postedFile.SaveAs(source_file);


                    if ((Path.GetExtension(_postedFile.FileName).Equals(".xml")) || (Path.GetExtension(_postedFile.FileName).Equals(".txt")) || (Path.GetExtension(_postedFile.FileName).Equals(".csv")))
                    {
                        #region "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx"


                        int index = 0;
                        Double value = 0;
                        using (var reader = new StreamReader(source_file))
                        {
                            int row = 0;
                            int colCount = 0;
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();

                                if (Path.GetFileNameWithoutExtension(_postedFile.FileName).ToLower().StartsWith("LargestRegionsTable".ToLower()))
                                {
                                    if (line.IndexOf("<Cell>") != -1)
                                    {
                                        String[] tmp = line.Replace("ss:Type=\"String\"", String.Empty).Replace("ss:Type=\"Number\"", String.Empty).Replace(" ", String.Empty).Replace("</Data>", "#").Replace("<Data>", String.Empty).Replace("<Cell>", String.Empty).Replace("</Cell>", String.Empty).Split('#');
                                        if (tmp.Length == 13)
                                        {
                                            value = (!CustomUtils.isNumber(tmp[1])) ? Convert.ToDouble(0) : Convert.ToDouble(tmp[1]);
                                            String filter = Regex.Replace(tmp[4], @"(\s+|@|&|'|\(|\)|<|>|#|\"")", "").Replace(" ", String.Empty);
                                            if (null != filter && filterList.Contains(filter))
                                            {
                                                switch (filter.Trim())
                                                {
                                                    case PA_REFLECTIVE:
                                                        if (value > largestMetallicShine)
                                                        {
                                                            largestMetallicShine = value;
                                                        }
                                                        break;
                                                    case PA_NON_REFLECTIVE:
                                                        if (value > largestNonMetallicShine)
                                                        {
                                                            largestNonMetallicShine = value;
                                                        }
                                                        break;
                                                    case PA_FIBROUS:
                                                        if (value > longestFiber)
                                                        {
                                                            longestFiber = value;
                                                        }
                                                        break;
                                                }

                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                }
                                if (Path.GetFileNameWithoutExtension(_postedFile.FileName).ToLower().StartsWith("reflectiveTable".ToLower()))
                                {
                                    if (line.IndexOf("ExpandedColumnCount") != -1)
                                    {
                                        colCount = CustomUtils.isNumber(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) ? Convert.ToInt16(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) : 0;
                                        Console.WriteLine();
                                    }
                                    if (line.IndexOf("<Cell>") != -1)
                                    {
                                        String[] tmp = line.Replace("ss:Type=\"String\"", String.Empty).Replace("ss:Type=\"Number\"", String.Empty).Replace(" ", String.Empty).Replace("</Data>", "#").Replace("<Data>", String.Empty).Replace("<Cell>", String.Empty).Replace("</Cell>", String.Empty).Split('#');
                                        if (tmp.Length == (colCount + 1))
                                        {
                                            double x = Convert.ToDouble(tmp[1]);
                                            foreach (var item in paList)
                                            {
                                                String val = Regex.Match(item.col_c, @"\d+").Value;
                                                item.col_z = val;
                                            }


                                            template_pa_detail _tmp = paList.Where(o => o.col_z.StartsWith(x.ToString())).FirstOrDefault();
                                            if (_tmp != null)
                                            {
                                                value = (!CustomUtils.isNumber(tmp[3])) ? Convert.ToDouble(0) : Convert.ToDouble(tmp[3]);
                                                _tmp.col_g = value.ToString();
                                            }
                                            row++;
                                        }
                                    }
                                }
                                if (Path.GetFileNameWithoutExtension(_postedFile.FileName).ToLower().StartsWith("non-reflectiveTable".ToLower()))
                                {
                                    if (line.IndexOf("ExpandedColumnCount") != -1)
                                    {
                                        colCount = CustomUtils.isNumber(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) ? Convert.ToInt16(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) : 0;
                                        Console.WriteLine();
                                    }
                                    if (line.IndexOf("<Cell>") != -1)
                                    {
                                        String[] tmp = line.Replace("ss:Type=\"String\"", String.Empty).Replace("ss:Type=\"Number\"", String.Empty).Replace(" ", String.Empty).Replace("</Data>", "#").Replace("<Data>", String.Empty).Replace("<Cell>", String.Empty).Replace("</Cell>", String.Empty).Split('#');
                                        if (tmp.Length == (colCount + 1))
                                        {
                                            double x = Convert.ToDouble(tmp[1]);
                                            foreach (var item in paList)
                                            {
                                                String val = Regex.Match(item.col_c, @"\d+").Value;
                                                item.col_z = val;
                                            }


                                            template_pa_detail _tmp = paList.Where(o => o.col_z.StartsWith(x.ToString())).FirstOrDefault();
                                            if (_tmp != null)
                                            {
                                                value = (!CustomUtils.isNumber(tmp[3])) ? Convert.ToDouble(0) : Convert.ToDouble(tmp[3]);
                                                _tmp.col_f = value.ToString();
                                            }
                                            row++;
                                        }
                                    }
                                }
                                if (Path.GetFileNameWithoutExtension(_postedFile.FileName).ToLower().StartsWith("fibrousTable".ToLower()))
                                {
                                    if (line.IndexOf("ExpandedColumnCount") != -1)
                                    {
                                        colCount = CustomUtils.isNumber(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) ? Convert.ToInt16(Regex.Match(line.Split('=')[1].Split(' ')[0], @"\d+").Value) : 0;
                                        Console.WriteLine();
                                    }
                                    if (line.IndexOf("<Cell>") != -1)
                                    {
                                        String[] tmp = line.Replace("ss:Type=\"String\"", String.Empty).Replace("ss:Type=\"Number\"", String.Empty).Replace(" ", String.Empty).Replace("</Data>", "#").Replace("<Data>", String.Empty).Replace("<Cell>", String.Empty).Replace("</Cell>", String.Empty).Split('#');
                                        if (tmp.Length == (colCount + 1))
                                        {
                                            double x = Convert.ToDouble(tmp[1]);
                                            foreach (var item in paList)
                                            {
                                                String val = Regex.Match(item.col_c, @"\d+").Value;
                                                item.col_z = val;
                                            }


                                            template_pa_detail _tmp = paList.Where(o => o.col_z.StartsWith(x.ToString())).FirstOrDefault();
                                            if (_tmp != null)
                                            {
                                                value = (!CustomUtils.isNumber(tmp[3])) ? Convert.ToDouble(0) : Convert.ToDouble(tmp[3]);
                                                _tmp.col_h = value.ToString();
                                            }
                                            row++;
                                        }
                                    }

                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        //errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.csv"));
                    }
                }
                //}
                //catch (Exception ex)
                //{
                //    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));

                //    Console.WriteLine();
                //}
            }


            txtFeretLmsp.Text = largestMetallicShine.ToString("N2");
            txtFeretLnms.Text = largestNonMetallicShine.ToString("N2");
            txtFeretFb.Text = longestFiber.ToString("N2");

            txtLms_Y.Text = txtFeretLmsp.Text;
            txtLnms_Y.Text = txtFeretLnms.Text;
            txtLf_Y.Text = txtFeretFb.Text;

            txtLms_Y_R2.Text = txtFeretLmsp.Text;
            txtLnms_Y_R2.Text = txtFeretLnms.Text;
            txtLf_Y_R2.Text = txtFeretFb.Text;

            txtLms_Y_R3.Text = txtFeretLmsp.Text;
            txtLnms_Y_R3.Text = txtFeretLnms.Text;
            txtLf_Y_R3.Text = txtFeretFb.Text;
            //if (!FileUpload2.HasFile)
            //{
            //    errors.Add(String.Format("ไม่พบไฟล์ *.csv ที่ใช้โหลดข้อมูล (Ex. ClassTable_FromNumber_FeretMaximum_A01316.csv)"));
            //}

            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                calculate();
            }
        }

        protected void btnLoadImg1_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;

            if ((Path.GetExtension(fileUploadImg01.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg01.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg01.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg01.SaveAs(source_file_jpg);
                }

                this.Pa.img01 = source_file_url;
                img1.ImageUrl = source_file_url;



            }

            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                calculate();
            }
        }

        protected void btnLoadImg_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;

            if ((Path.GetExtension(fileUploadImg02.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg02.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg02.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg02.SaveAs(source_file_jpg);
                }
                this.Pa.img02 = source_file_url;
                img2.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg03.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg03.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg03.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg03.SaveAs(source_file_jpg);
                }
                this.Pa.img03 = source_file_url;
                img3.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg04.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg04.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg04.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg04.SaveAs(source_file_jpg);
                }
                this.Pa.img04 = source_file_url;
                img4.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg05.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg05.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg05.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg05.SaveAs(source_file_jpg);
                }
                this.Pa.img05 = source_file_url;
                img5.ImageUrl = source_file_url;
            }
            //ROW2
            if ((Path.GetExtension(fileUploadImg03_R2.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg03_R2.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg03_R2.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg03_R2.SaveAs(source_file_jpg);
                }
                this.Pa.img06 = source_file_url;
                img6.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg04_R2.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg04_R2.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg04_R2.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg04_R2.SaveAs(source_file_jpg);
                }
                this.Pa.img07 = source_file_url;
                img7.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg05_R2.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg05_R2.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg05_R2.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg04_R2.SaveAs(source_file_jpg);
                }
                this.Pa.img08 = source_file_url;
                img8.ImageUrl = source_file_url;
            }
            //ROW3
            if ((Path.GetExtension(fileUploadImg03_R3.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg03_R3.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg03_R3.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg03_R3.SaveAs(source_file_jpg);
                }
                this.Pa.img09 = source_file_url;
                img9.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg04_R3.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg04_R3.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg04_R3.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg04_R3.SaveAs(source_file_jpg);
                }
                this.Pa.img10 = source_file_url;
                img10.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg05_R3.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUploadImg05_R3.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUploadImg05_R3.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUploadImg05_R3.SaveAs(source_file_jpg);
                }
                this.Pa.img11 = source_file_url;
                img11.ImageUrl = source_file_url;
            }


            if ((Path.GetExtension(fileAttachedDoc.FileName).ToUpper().Equals(".DOC")) || (Path.GetExtension(fileAttachedDoc.FileName).ToUpper().Equals(".DOCX")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileAttachedDoc.FileName));

                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file_jpg)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file_jpg));
                }

                fileAttachedDoc.SaveAs(source_file_jpg);

                this.Pa.attachment_ii_05 = source_file_url;
            }

            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                calculate();
            }
        }
        #endregion

        #region "Event"

        #region "EOP"
        protected void gvEop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_cov != null)
                    {
                        RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), _cov.row_type.ToString(), true);
                        switch (cmd)
                        {
                            case RowTypeEnum.Hide:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Hide);
                                break;
                            case RowTypeEnum.Normal:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Normal);
                                break;
                        }

                        gvEop.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
                        gvEop.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvEop_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvEop.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }


            }
        }

        protected void gvEop_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvEop_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEop.EditIndex = e.NewEditIndex;
            gvEop.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).OrderBy(x => x.seq).ToList();
            gvEop.DataBind();
        }

        protected void gvEop_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvEop.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtC = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtC");
            TextBox txtD = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtD");
            TextBox txtE = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtE");
            TextBox txtF = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtF");
            TextBox txtG = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtG");
            TextBox txtH = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtH");
            TextBox txtI = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtI");
            TextBox txtJ = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtJ");
            TextBox txtK = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtK");
            TextBox txtL = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtL");
            TextBox txtM = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtM");
            TextBox txtN = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtN");


            template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                if (txtC != null)
                {
                    _cov.col_c = txtC.Text;
                }
                if (txtD != null)
                {
                    _cov.col_d = txtD.Text;
                }
                if (txtE != null)
                {
                    _cov.col_e = txtE.Text;
                }
                if (txtF != null)
                {
                    _cov.col_f = txtF.Text;
                }
                if (txtG != null)
                {
                    _cov.col_g = txtG.Text;
                }
                if (txtH != null)
                {
                    _cov.col_h = txtH.Text;
                }
                if (txtI != null)
                {
                    _cov.col_i = txtI.Text;
                }
                if (txtJ != null)
                {
                    _cov.col_j = txtJ.Text;
                }
                if (txtK != null)
                {
                    _cov.col_k = txtK.Text;
                }
                if (txtL != null)
                {
                    _cov.col_l = txtL.Text;
                }
                if (txtM != null)
                {
                    _cov.col_m = txtM.Text;
                }
                if (txtN != null)
                {
                    _cov.col_n = txtN.Text;
                }
            }
            gvEop.EditIndex = -1;
            gvEop.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).OrderBy(x => x.seq).ToList();
            gvEop.DataBind();
        }

        protected void gvEop_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEop.EditIndex = -1;
            gvEop.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).OrderBy(x => x.seq).ToList();
            gvEop.DataBind();
        }
        #endregion
        #region "MicroscopicAnalysis"
        protected void gvMicroscopicAnalysis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_cov != null)
                    {
                        RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), _cov.row_type.ToString(), true);
                        switch (cmd)
                        {
                            case RowTypeEnum.Hide:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Hide);
                                break;
                            case RowTypeEnum.Normal:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Normal);
                                break;
                        }

                        gvMicroscopicAnalysis.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
                        gvMicroscopicAnalysis.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvMicroscopicAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMicroscopicAnalysis.DataKeys[e.Row.RowIndex].Values[1]);
                    LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                    LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                    if (_btnHide != null && _btnUndo != null)
                    {
                        switch (cmd)
                        {
                            case RowTypeEnum.Hide:
                                _btnHide.Visible = false;
                                _btnUndo.Visible = true;
                                e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                                break;
                            default:
                                _btnHide.Visible = true;
                                _btnUndo.Visible = false;
                                e.Row.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                    e.Row.Cells[2].BackColor = Color.LightSteelBlue;
                    e.Row.Cells[6].BackColor = Color.LightSteelBlue;

                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[2].BackColor = Color.LightSteelBlue;
                    e.Row.Cells[6].BackColor = Color.LightSteelBlue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvMicroscopicAnalysis_OnDataBound(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell
                {
                    Text = "",
                    RowSpan = 1
                };
                row.Controls.Add(cell);

                cell = new TableHeaderCell
                {
                    RowSpan = 1,
                    Text = ""
                };
                row.Controls.Add(cell);

                cell = new TableHeaderCell
                {
                    ColumnSpan = 4,
                    Text = "Particles on membrane",
                    HorizontalAlign = HorizontalAlign.Center
                };
                row.Controls.Add(cell);

                cell = new TableHeaderCell
                {
                    ColumnSpan = 4,
                    Text = "3Particles per component",
                    HorizontalAlign = HorizontalAlign.Center
                };
                row.Controls.Add(cell);

                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 2;
                //cell.Text = "Particles on per 1000 cm2";
                //cell.HorizontalAlign = HorizontalAlign.Center;
                //row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 1;
                cell.Text = "";
                row.Controls.Add(cell);

                //gvMicroscopicAnalysis.HeaderRow.Parent.Controls.AddAt(0, row);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }


        protected void gvMicroscopicAnalysis_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMicroscopicAnalysis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvMicroscopicAnalysis.EditIndex = e.NewEditIndex;
                gvMicroscopicAnalysis.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
                gvMicroscopicAnalysis.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvMicroscopicAnalysis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvMicroscopicAnalysis.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtA = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtA");
            TextBox txtB = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtB");
            TextBox txtC = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtC");
            TextBox txtD = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtD");
            TextBox txtE = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtE");
            TextBox txtF = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtF");
            TextBox txtG = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtG");
            TextBox txtH = (TextBox)gvMicroscopicAnalysis.Rows[e.RowIndex].FindControl("txtH");


            //template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            //if (_cov != null)
            //{
            //    _cov.col_a = txtA.Text;
            //    _cov.col_b = txtB.Text;
            //    _cov.col_c = txtC.Text;
            //    _cov.col_d = txtD.Text;
            //    _cov.col_e = txtE.Text;
            //    _cov.col_f = txtF.Text;
            //    _cov.col_g = txtG.Text;
            //    _cov.col_h = txtH.Text;
            //}


            gvMicroscopicAnalysis.EditIndex = -1;
            gvMicroscopicAnalysis.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            gvMicroscopicAnalysis.DataBind();
        }

        protected void gvMicroscopicAnalysis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMicroscopicAnalysis.EditIndex = -1;
            gvMicroscopicAnalysis.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            gvMicroscopicAnalysis.DataBind();
        }
        #endregion
        #region PA_DISSOLVING
        protected void gvDissolving_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_cov != null)
                    {
                        RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), _cov.row_type.ToString(), true);
                        switch (cmd)
                        {
                            case RowTypeEnum.Hide:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Hide);
                                break;
                            case RowTypeEnum.Normal:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Normal);
                                break;
                        }

                        gvDissolving.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING)).ToList();
                        gvDissolving.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvDissolving_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvDissolving.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }

            }
        }

        protected void gvDissolving_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvDissolving_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDissolving.EditIndex = e.NewEditIndex;
            gvDissolving.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            gvDissolving.DataBind();
        }

        protected void gvDissolving_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvDissolving.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtD = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtD");
            TextBox txtE = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtE");
            TextBox txtF = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtF");
            TextBox txtG = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtG");
            TextBox txtH = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtH");
            TextBox txtI = (TextBox)gvDissolving.Rows[e.RowIndex].FindControl("txtI");

            template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                if (txtD != null)
                {
                    _cov.col_d = txtD.Text;
                }
                if (txtE != null)
                {
                    _cov.col_e = txtE.Text;
                }
                if (txtF != null)
                {
                    _cov.col_f = txtF.Text;
                }
                if (txtG != null)
                {
                    _cov.col_g = txtG.Text;
                }
                if (txtH != null)
                {
                    _cov.col_h = txtH.Text;
                }
                if (txtI != null)
                {
                    _cov.col_i = txtI.Text;
                }
            }
            gvDissolving.EditIndex = -1;
            gvDissolving.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            gvDissolving.DataBind();
        }

        protected void gvDissolving_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDissolving.EditIndex = -1;
            gvDissolving.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            gvDissolving.DataBind();
        }

        #endregion
        #region PA_WASHING
        protected void gvWashing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_cov != null)
                    {
                        RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), _cov.row_type.ToString(), true);
                        switch (cmd)
                        {
                            case RowTypeEnum.Hide:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Hide);
                                break;
                            case RowTypeEnum.Normal:
                                _cov.row_status = Convert.ToInt32(RowTypeEnum.Normal);
                                break;
                        }

                        gvWashing.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING)).ToList();
                        gvWashing.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvWashing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvWashing.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }

            }
        }

        protected void gvWashing_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvWashing_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWashing.EditIndex = e.NewEditIndex;
            gvWashing.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            gvWashing.DataBind();
        }

        protected void gvWashing_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvWashing.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtD = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtD");
            TextBox txtE = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtE");
            TextBox txtF = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtF");
            TextBox txtG = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtG");
            TextBox txtH = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtH");
            TextBox txtI = (TextBox)gvWashing.Rows[e.RowIndex].FindControl("txtI");

            template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                if (txtD != null)
                {
                    _cov.col_d = txtD.Text;
                }
                if (txtE != null)
                {
                    _cov.col_e = txtE.Text;
                }
                if (txtF != null)
                {
                    _cov.col_f = txtF.Text;
                }
                if (txtG != null)
                {
                    _cov.col_g = txtG.Text;
                }
                if (txtH != null)
                {
                    _cov.col_h = txtH.Text;
                }
                if (txtI != null)
                {
                    _cov.col_i = txtI.Text;
                }

            }
            gvWashing.EditIndex = -1;
            gvWashing.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            gvWashing.DataBind();
        }

        protected void gvWashing_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWashing.EditIndex = -1;
            gvWashing.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            gvWashing.DataBind();
        }

        #endregion


        protected void ddlMa_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void txtParticleSize01_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtParticleSize02.Text = (Convert.ToDouble(txtParticleSize03.Text) / Convert.ToDouble(txtParticleSize01.Text)).ToString("N2");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void txtParticleSize03_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtParticleSize02.Text = (Convert.ToDouble(txtParticleSize03.Text) / Convert.ToDouble(txtParticleSize01.Text)).ToString("N2");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();// Extenders.ObjectToDataTable(this.coverpages[0]);
            ReportHeader reportHeader = ReportHeader.getReportHeder(this.JobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));

            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", " "));
            reportParameters.Add(new ReportParameter("partNo", String.IsNullOrEmpty(this.JobSample.part_no) ? " " : this.JobSample.part_no));
            reportParameters.Add(new ReportParameter("partName", String.IsNullOrEmpty(this.JobSample.part_name) ? " " : this.JobSample.part_name));
            reportParameters.Add(new ReportParameter("lotNo", String.IsNullOrEmpty(this.JobSample.lot_no) ? " " : this.JobSample.lot_no));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", " "));

            reportParameters.Add(new ReportParameter("dh_col1", this.DissolvingHeaders[0]));
            reportParameters.Add(new ReportParameter("dh_col2", this.DissolvingHeaders[1]));
            reportParameters.Add(new ReportParameter("dh_col3", this.DissolvingHeaders[2]));
            reportParameters.Add(new ReportParameter("dh_col4", this.DissolvingHeaders[3]));
            reportParameters.Add(new ReportParameter("dh_col5", this.DissolvingHeaders[4]));

            reportParameters.Add(new ReportParameter("wh_col1", this.WashingHeaders[0]));
            reportParameters.Add(new ReportParameter("wh_col2", this.WashingHeaders[1]));
            reportParameters.Add(new ReportParameter("wh_col3", this.WashingHeaders[2]));
            reportParameters.Add(new ReportParameter("wh_col4", this.WashingHeaders[3]));
            reportParameters.Add(new ReportParameter("wh_col5", this.WashingHeaders[4]));
            reportParameters.Add(new ReportParameter("specification_no", ddlSpecification.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("operator_name", ddlOperatorName.SelectedItem.Text));

            reportParameters.Add(new ReportParameter("TestResult", (ddlResult.SelectedItem.Text.Equals("-NONE-") ? " " : ddlResult.SelectedItem.Text)));

            reportParameters.Add(new ReportParameter("dissolving_rinsing", ddlRinsing.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("wash_rinsing", dllWashPressureRinsing.SelectedItem.Text));

            reportParameters.Add(new ReportParameter("showAttachedII", (this.Pa.img06 == null)? "HIDDEN":""));
            //reportParameters.Add(new ReportParameter("SupplementToReportNo", reportHeader.supplementToReportNo));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/pa_03.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);

            List<template_pa_detail> eops = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).OrderBy(x => x.seq).ToList();
            List<template_pa_detail> dissolvings = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            List<template_pa_detail> washings = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            List<template_pa_detail> mas = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).OrderBy(x => x.seq).ToList();
            List<template_pa_detail> ec1 = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("1")).ToList();
            List<template_pa_detail> ec2 = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("2")).ToList();


            List<template_pa> pas = new List<template_pa>();
            this.Pa.img1 = CustomUtils.GetBytesFromImage(this.Pa.img01);
            this.Pa.img2 = CustomUtils.GetBytesFromImage(this.Pa.img02);
            this.Pa.img3 = CustomUtils.GetBytesFromImage(this.Pa.img03);
            this.Pa.img4 = CustomUtils.GetBytesFromImage(this.Pa.img04);
            this.Pa.img5 = CustomUtils.GetBytesFromImage(this.Pa.img05);

            this.Pa.img6 = CustomUtils.GetBytesFromImage(this.Pa.attachment_ii_01);
            this.Pa.img7 = CustomUtils.GetBytesFromImage(this.Pa.attachment_ii_02);
            this.Pa.img8 = CustomUtils.GetBytesFromImage(this.Pa.attachment_ii_03);
            this.Pa.img9 = CustomUtils.GetBytesFromImage(this.Pa.attachment_ii_01);

            this.Pa.img3R2 = CustomUtils.GetBytesFromImage(this.Pa.img06);
            this.Pa.img4R2 = CustomUtils.GetBytesFromImage(this.Pa.img07);
            this.Pa.img5R2 = CustomUtils.GetBytesFromImage(this.Pa.img08);

            this.Pa.img3R3 = CustomUtils.GetBytesFromImage(this.Pa.img09);
            this.Pa.img4R3 = CustomUtils.GetBytesFromImage(this.Pa.img10);
            this.Pa.img5R3 = CustomUtils.GetBytesFromImage(this.Pa.img11);

            this.Pa.iscontainer_text = cbContainer.Checked.ToString();
            this.Pa.container_id_text = ddlContainer.SelectedItem.Text;
            this.Pa.isfluid1_text = cbFluid1.Checked.ToString();
            this.Pa.fluid1_id_text = ddlFluid1.SelectedItem.Text;
            this.Pa.isfluid2_text = cbFluid2.Checked.ToString();
            this.Pa.fluid2_id_text = ddlFluid2.SelectedItem.Text;
            this.Pa.isfluid3_text = cbFluid3.Checked.ToString();
            this.Pa.fluid3_id_text = ddlFluid3.SelectedItem.Text;
            this.Pa.istshb01_text = cbTshb01.Checked.ToString();
            this.Pa.istshb02_text = cbTshb02.Checked.ToString();
            this.Pa.istshb03_text = cbTshb03.Checked.ToString();
            this.Pa.ispots01_text = cbPots01.Checked.ToString();
            this.Pa.isdissolving_text = cbDissolving.Checked.ToString();

            this.Pa.isagitation_text = cbAgitation.Checked.ToString();
            this.Pa.ispressurerinsing_text = cbPressureRinsing.Checked.ToString();
            this.Pa.isultrasonic_text = cbUntrasonic.Checked.ToString();



            this.Pa.iswashquantity_text = cbWashQuantity.Checked.ToString();
            this.Pa.isrewashingquantity_text = cbRewashingQuantity.Checked.ToString();


            this.Pa.iswashagitation_text = cbWashAgitation.Checked.ToString();
            this.Pa.iswashpressurerinsing_text = cbWashPressureRinsing.Checked.ToString();
            this.Pa.iswashultrasonic_text = cbWashUltrasonic.Checked.ToString();


            this.Pa.isoven_text = cbOven.Checked.ToString();
            this.Pa.isdesiccator_text = cbDesiccator.Checked.ToString();
            this.Pa.gravimetricalalysis_id_text = ddlGravimetricAlalysis.SelectedItem.Text;
            this.Pa.iseasydry_text = cbEasyDry.Checked.ToString();
            this.Pa.isambientair_text = cbAmbientAir.Checked.ToString();
            this.Pa.iszeissaxioimager2_text = cbZEISSAxioImager2.Checked.ToString();
            this.Pa.ismeasuringsoftware_text = cbMeasuringSoftware.Checked.ToString();
            this.Pa.isautomated_text = cbAutomated.Checked.ToString();
            this.Pa.material_id_text = ddlMaterial.SelectedItem.Text;
            this.Pa.manufacturer_id_text = ddlManufacturer.SelectedItem.Text;
            this.Pa.lbmembranetype = lbMembraneType.Text;
            this.Pa.lbPermembrane_text = lbPermembraneTotal.Text;
            this.Pa.totalResidueWeight = lbTotalResidueWeight.Text;


            this.Pa.dh_col1 = this.DissolvingHeaders[0];
            this.Pa.dh_col2 = this.DissolvingHeaders[1];
            this.Pa.dh_col3 = this.DissolvingHeaders[2];
            this.Pa.dh_col4 = this.DissolvingHeaders[3];
            this.Pa.dh_col5 = this.DissolvingHeaders[4];

            this.Pa.wh_col1 = this.WashingHeaders[0];
            this.Pa.wh_col2 = this.WashingHeaders[1];
            this.Pa.wh_col3 = this.WashingHeaders[2];
            this.Pa.wh_col4 = this.WashingHeaders[3];
            this.Pa.wh_col5 = this.WashingHeaders[4];

            pas.Add(this.Pa);


            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dissolvings.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", washings.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", eops.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", mas.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", pas.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", ec1.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", ec2.ToDataTable())); // Add datasource here



            //xxxx


            //if (anionic.Count == 0)
            //{
            //    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", new DataTable())); // Add datasource here
            //    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here
            //    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", new DataTable())); // Add datasource here
            //}


            string download = String.Empty;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.JobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                    }
                    else
                    {
                        byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                        if (!Directory.Exists(Server.MapPath("~/Report/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Report/"));
                        }
                        using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                        }

                        #region "Insert Footer & Header from template"
                        Document doc1 = new Document();
                        //doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - PA.doc");
                        doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
                        Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                        Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                        Document doc2 = new Document(Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension);
                        foreach (Section section in doc2.Sections)
                        {
                            foreach (DocumentObject obj in header.ChildObjects)
                            {
                                section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                            }
                            foreach (DocumentObject obj in footer.ChildObjects)
                            {
                                section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
                            }
                        }



                        doc2.SaveToFile(Server.MapPath("~/Report/") + this.JobSample.job_number + "." + extension);
                        #endregion
                        Response.ContentType = mimeType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + this.JobSample.job_number + "." + extension);
                        Response.WriteFile(Server.MapPath("~/Report/" + this.JobSample.job_number + "." + extension));
                        Response.Flush();

                        #region "Delete After Download"
                        String deleteFile1 = Server.MapPath("~/Report/") + this.JobSample.job_number + "." + extension;
                        String deleteFile2 = Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension;

                        if (File.Exists(deleteFile1))
                        {
                            File.Delete(deleteFile1);
                        }
                        if (File.Exists(deleteFile2))
                        {
                            File.Delete(deleteFile2);
                        }
                        #endregion
                    }
                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                case StatusEnum.LABMANAGER_APPROVE:
                case StatusEnum.LABMANAGER_DISAPPROVE:
                    if (!String.IsNullOrEmpty(this.JobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                    }
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (!String.IsNullOrEmpty(this.JobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                    }
                    break;
            }
        }

        protected void ddlFluid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.E;
                txtManufacturer.Text = selectValue.F;
                cbFluid1.Checked = true;
                cbFluid2.Checked = false;
                cbFluid3.Checked = false;
            }
        }

        protected void ddlFluid2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.E;
                txtManufacturer.Text = selectValue.F;
                cbFluid1.Checked = false;
                cbFluid2.Checked = true;
                cbFluid3.Checked = false;
            }
        }

        protected void ddlFluid3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.E;
                txtManufacturer.Text = selectValue.F;
                cbFluid1.Checked = false;
                cbFluid2.Checked = false;
                cbFluid3.Checked = true;
            }
        }

        protected void cbFluid1_CheckedChanged(object sender, EventArgs e)
        {
            cbFluid1.Checked = true;
            cbFluid2.Checked = false;
            cbFluid3.Checked = false;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
        }

        protected void cbFluid2_CheckedChanged(object sender, EventArgs e)
        {
            cbFluid1.Checked = false;
            cbFluid2.Checked = true;
            cbFluid3.Checked = false;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
        }

        protected void cbFluid3_CheckedChanged(object sender, EventArgs e)
        {
            cbFluid1.Checked = false;
            cbFluid2.Checked = false;
            cbFluid3.Checked = true;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
            ddlFluid1.SelectedIndex = 0;
        }

        protected void cbDissolving_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            tb_m_specification selectValue = null;
            ddlRinsing.SelectedIndex = 0;

            if (ddl.Checked)
            {
                switch (ddl.ID)
                {
                    case "cbPressureRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                        cbPressureRinsing.Checked = true;
                        cbAgitation.Checked = false;
                        cbUntrasonic.Checked = false;
                        lbExtractionMethod.Text = PA_PRESURE_RINSING;
                        break;
                    case "cbAgitation":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                        cbPressureRinsing.Checked = false;
                        cbAgitation.Checked = true;
                        cbUntrasonic.Checked = false;
                        lbExtractionMethod.Text = PA_AGITATION;
                        break;
                    case "cbUntrasonic":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_ULTRASONIC)).FirstOrDefault();
                        cbPressureRinsing.Checked = false;
                        cbAgitation.Checked = false;
                        cbUntrasonic.Checked = true;
                        lbExtractionMethod.Text = PA_ULTRASONIC;
                        break;
                }
                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }

                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvDissolving.Columns[i].HeaderText = cols[i];
                        gvDissolving.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail();
                    tmp.id = CustomUtils.GetRandomNumberID();
                    tmp.col_d = "42 KHz";
                    tmp.col_e = "24 W/L";
                    tmp.col_f = "8 mins";
                    tmp.col_g = "room temperature";
                    tmp.col_h = "No";
                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                    tmp.row_type = Convert.ToInt16(PAEnum.DISSOLVING);
                    PaDetail.Add(tmp);

                }
            }
            else
            {
                foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                {
                    PaDetail.Remove(pd);
                }
            }
            calculate();
        }

        protected void cbWashQuantity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            tb_m_specification selectValue = null;
            dllWashPressureRinsing.SelectedIndex = 0;
            if (ddl.Checked)
            {
                switch (ddl.ID)
                {
                    case "cbWashPressureRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                        cbWashPressureRinsing.Checked = true;
                        cbWashAgitation.Checked = false;
                        cbWashUltrasonic.Checked = false;
                        break;
                    case "cbWashAgitation":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_AGITATION)).FirstOrDefault();
                        cbWashPressureRinsing.Checked = false;
                        cbWashAgitation.Checked = true;
                        cbWashUltrasonic.Checked = false;
                        break;
                    case "cbWashUltrasonic":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_ULTRASONIC)).FirstOrDefault();
                        cbWashPressureRinsing.Checked = false;
                        cbWashAgitation.Checked = false;
                        cbWashUltrasonic.Checked = true;
                        break;
                }

                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }


                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvWashing.Columns[i].HeaderText = cols[i];
                        gvWashing.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail();
                    tmp.id = CustomUtils.GetRandomNumberID();
                    tmp.col_d = "Flat type";
                    tmp.col_e = "40";
                    tmp.col_f = "0.5 L/min";
                    tmp.col_g = "1 bar";
                    tmp.col_h = "-";
                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                    tmp.row_type = Convert.ToInt16(PAEnum.WASHING);
                    PaDetail.Add(tmp);

                }
            }
            else
            {
                foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                {
                    PaDetail.Remove(pd);
                }
            }
            calculate();


        }

        #endregion

        protected void ddlGravimetricAlalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (selectValue != null)
            {
                txtModel.Text = String.IsNullOrEmpty(selectValue.D) ? String.Empty : selectValue.E;
                txtBalanceResolution.Text = String.IsNullOrEmpty(selectValue.F) ? String.Empty : selectValue.F;
                //txtLastCalibration.Text = String.IsNullOrEmpty(selectValue.F) ? String.Empty : selectValue.F;
            }
        }

        protected void txtFeretLmsp_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            txtEop_Lmsp.Text = tb.Text;
            txtLms.Text = tb.Text;
        }

        protected void txtFeretLnms_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            txtEop_Lnmsp.Text = tb.Text;
            txtLnmp.Text = tb.Text;
        }

        protected void txtFeretFb_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            lbLf.Text = tb.Text;
            txtLf.Text = tb.Text;

        }

        protected void txtAutomated_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            lbX.Text = tb.Text;
            lbY.Text = tb.Text;

        }

        protected void ddlManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            membraneType();

        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            membraneType();

        }

        protected void txtPoreSize_TextChanged(object sender, EventArgs e)
        {
            membraneType();
        }

        protected void txtDiameter_TextChanged(object sender, EventArgs e)
        {
            membraneType();
        }

        private void membraneType()
        {
            lbMembraneType.Text = String.Format("{0} / {1} um, {2} mm Dia.", ddlMaterial.SelectedItem.Text, txtPoreSize.Text, txtDiameter.Text);

        }

        protected void txtEop_G_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            lbTotalResidueWeight.Text = tb.Text;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            template_pa_detail tmp = new template_pa_detail();
            tmp.id = CustomUtils.GetRandomNumberID();
            tmp.col_d = "1";
            tmp.col_e = txtEC01.Text;
            tmp.col_f = txtEC01_SHOT.Text;
            tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
            tmp.row_type = Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION);
            PaDetail.Add(tmp);

            List<template_pa_detail> ecs = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("1")).ToList();
            gvCompositionElement.DataSource = ecs;
            gvCompositionElement.DataBind();
            txtEC01.Text = String.Empty;
            txtEC01_SHOT.Text = String.Empty;

        }

        protected void gvCompositionElement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvCompositionElement.DataKeys[e.RowIndex].Values[0].ToString());

            template_pa_detail pa = PaDetail.Find(x => x.id == id);
            if (pa != null)
            {
                PaDetail.Remove(pa);
                List<template_pa_detail> ecs = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("1")).ToList();
                gvCompositionElement.DataSource = ecs;
                gvCompositionElement.DataBind();
            }
        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            template_pa_detail tmp = new template_pa_detail();
            tmp.id = CustomUtils.GetRandomNumberID();
            tmp.col_d = "2";
            tmp.col_e = txtEC02.Text;
            tmp.col_f = txtEC02_SHOT.Text;
            tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
            tmp.row_type = Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION);
            PaDetail.Add(tmp);

            List<template_pa_detail> ecs = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("2")).ToList();
            gvCompositionElement2.DataSource = ecs;
            gvCompositionElement2.DataBind();
            txtEC02.Text = String.Empty;
            txtEC02_SHOT.Text = String.Empty;
        }

        protected void gvCompositionElement2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvCompositionElement2.DataKeys[e.RowIndex].Values[0].ToString());

            template_pa_detail pa = PaDetail.Find(x => x.id == id);
            if (pa != null)
            {
                PaDetail.Remove(pa);
                List<template_pa_detail> ecs = PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.ELEMENT_COMPOSITION) && x.col_d.Equals("2")).ToList();
                gvCompositionElement2.DataSource = ecs;
                gvCompositionElement2.DataBind();
            }
        }

        #region "LOAD IMG"
        protected void btnLoadParamImg1_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;
            if ((Path.GetExtension(fileUpload3.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUpload3.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUpload3.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUpload3.SaveAs(source_file_jpg);
                }
                ///
                this.Pa.attachment_ii_01 = source_file_url;
                Image1.ImageUrl = source_file_url;
            }
        }

        protected void btnLoadParamImg2_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;
            if ((Path.GetExtension(fileUpload4.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUpload4.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUpload4.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUpload4.SaveAs(source_file_jpg);
                }
                ///
                this.Pa.attachment_ii_02 = source_file_url;
                Image2.ImageUrl = source_file_url;
            }
        }

        protected void btnLoadParamImg3_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;
            if ((Path.GetExtension(fileUpload5.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUpload5.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUpload5.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUpload5.SaveAs(source_file_jpg);
                }
                ///
                this.Pa.attachment_ii_03 = source_file_url;
                Image3.ImageUrl = source_file_url;
            }
        }

        protected void btnLoadParamImg4_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String jpgName = String.Empty;
            String tifName = String.Empty;

            String source_file = String.Empty;
            String source_file_jpg = String.Empty;
            String source_file_url = String.Empty;
            if ((Path.GetExtension(fileUpload6.FileName).ToUpper().Equals(".JPG")) || (Path.GetExtension(fileUpload6.FileName).ToUpper().Equals(".TIF")))
            {
                jpgName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".jpg");
                tifName = String.Format("{0}_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), ".tif");

                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, tifName);
                source_file_jpg = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, jpgName);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, jpgName));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                if (Path.GetExtension(source_file).ToUpper().Equals(".TIF"))
                {
                    fileUpload6.SaveAs(source_file);
                    PictureUtils.convertTifToJpg(source_file, source_file_jpg);

                }
                else
                {
                    fileUpload6.SaveAs(source_file_jpg);
                }
                ///
                this.Pa.attachment_ii_04 = source_file_url;
                Image4.ImageUrl = source_file_url;
            }
        }
        #endregion
        protected void txtDissolving_TextChanged(object sender, EventArgs e)
        {
            Double val1 = CustomUtils.isNumber(txtDissolving.Text) ? Convert.ToDouble(txtDissolving.Text) : 0;
            Double val2 = CustomUtils.isNumber(txtWashQuantity.Text) ? Convert.ToDouble(txtWashQuantity.Text) : 0;
            Double val3 = CustomUtils.isNumber(txtRewashingQuantity.Text) ? Convert.ToDouble(txtRewashingQuantity.Text) : 0;
            txtTotalQuantity.Text = (val1 + val2 + val3) + "";
        }

        protected void ddlRinsing_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = null;
            if (cbPressureRinsing.Checked)
            {
                switch (ddl.SelectedValue)
                {
                    case "0":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                        lbExtractionMethod.Text = PA_PRESURE_RINSING;
                        break;
                    case "1":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_DISSOLVING) && x.D.Equals(PA_INTERNAL_RINSING)).FirstOrDefault();
                        lbExtractionMethod.Text = PA_INTERNAL_RINSING;
                        break;
                }

                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }


                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvWashing.Columns[i].HeaderText = cols[i];
                        gvWashing.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail();
                    tmp.id = CustomUtils.GetRandomNumberID();
                    tmp.col_d = "Flat type";
                    tmp.col_e = "40";
                    tmp.col_f = "0.5 L/min";
                    tmp.col_g = "1 bar";
                    tmp.col_h = "-";
                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                    tmp.row_type = Convert.ToInt16(PAEnum.DISSOLVING);
                    PaDetail.Add(tmp);

                }

                calculate();
            }
        }

        protected void dllWashPressureRinsing_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = null;
            if (cbWashPressureRinsing.Checked)
            {
                switch (ddl.SelectedValue)
                {
                    case "0":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_PRESURE_RINSING)).FirstOrDefault();
                        break;
                    case "1":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals(PA_DESCRIPTION_OF_PROCESS_AND_EXTRACTION) && x.C.Equals(PA_WASHING) && x.D.Equals(PA_INTERNAL_RINSING)).FirstOrDefault();
                        break;
                }

                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                    {
                        PaDetail.Remove(pd);
                    }


                    List<String> cols = tb_m_specification.findColumnCount(selectValue);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvWashing.Columns[i].HeaderText = cols[i];
                        gvWashing.Columns[i].Visible = true;
                    }
                    template_pa_detail tmp = new template_pa_detail();
                    tmp.id = CustomUtils.GetRandomNumberID();
                    tmp.col_d = "Flat type";
                    tmp.col_e = "40";
                    tmp.col_f = "0.5 L/min";
                    tmp.col_g = "1 bar";
                    tmp.col_h = "-";
                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                    tmp.row_type = Convert.ToInt16(PAEnum.WASHING);
                    PaDetail.Add(tmp);

                }

                calculate();
            }
        }

        protected void ddlPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbPer.Text = ddlPer.SelectedItem.Text;
        }

        protected void btnSrChemistTest_Click(object sender, EventArgs e)
        {
            calculate();
        }

        protected void btnShowUnit_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();

        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PA_SPECIFICATION = ddlSpecification.SelectedItem.Text;

            tb_m_specification selectedSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_SPECIFICATION_NO) && x.C.Equals(this.PA_SPECIFICATION)).FirstOrDefault();
            if (selectedSpec != null)
            {
                lbP1Note.Text = (selectedSpec.D == null) ? String.Empty : selectedSpec.D;
                lbP1Remark.Text = (selectedSpec.E == null) ? String.Empty : selectedSpec.E;
            }

            #region "gvEop"
            List<tb_m_specification> listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_DDL_EVALUATION_OF_PARTICLE) && x.B.Equals(PA_SPECIFICATION)).ToList();
            if (listOfSpec.Count > 0)
            {

                foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList())
                {
                    PaDetail.Remove(pd);
                }

                int seq = 1;
                foreach (tb_m_specification item in listOfSpec)
                {
                    if (seq > 1)
                    {
                        template_pa_detail tmp = new template_pa_detail();
                        tmp.id = CustomUtils.GetRandomNumberID();
                        tmp.seq = seq;
                        tmp.col_c = item.C;
                        tmp.col_d = item.D;
                        tmp.col_e = item.E;
                        tmp.col_f = item.F;
                        tmp.col_g = item.G;
                        tmp.col_h = item.H;
                        tmp.col_i = item.I;
                        tmp.col_j = item.J;
                        tmp.col_k = item.K;
                        tmp.col_l = item.L;
                        tmp.col_m = item.M;
                        tmp.col_n = item.N;
                        tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                        tmp.row_type = Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES);
                        PaDetail.Add(tmp);

                    }
                    else
                    {
                        //
                        gvEop.Columns[0].HeaderText = item.C;
                        gvEop.Columns[1].HeaderText = item.D;
                        gvEop.Columns[2].HeaderText = item.E;
                        gvEop.Columns[3].HeaderText = item.F;
                        gvEop.Columns[4].HeaderText = item.G;
                        gvEop.Columns[5].HeaderText = item.H;
                        gvEop.Columns[6].HeaderText = item.I;
                        gvEop.Columns[7].HeaderText = item.J;
                        gvEop.Columns[8].HeaderText = item.K;
                        gvEop.Columns[9].HeaderText = item.L;
                        gvEop.Columns[10].HeaderText = item.M;
                        gvEop.Columns[11].HeaderText = item.N;


                        gvEop.Columns[0].Visible = !String.IsNullOrEmpty(gvEop.Columns[0].HeaderText);
                        gvEop.Columns[1].Visible = !String.IsNullOrEmpty(gvEop.Columns[1].HeaderText);
                        gvEop.Columns[2].Visible = !String.IsNullOrEmpty(gvEop.Columns[2].HeaderText);
                        gvEop.Columns[3].Visible = !String.IsNullOrEmpty(gvEop.Columns[3].HeaderText);
                        gvEop.Columns[4].Visible = !String.IsNullOrEmpty(gvEop.Columns[4].HeaderText);
                        gvEop.Columns[5].Visible = !String.IsNullOrEmpty(gvEop.Columns[5].HeaderText);
                        gvEop.Columns[6].Visible = !String.IsNullOrEmpty(gvEop.Columns[6].HeaderText);
                        gvEop.Columns[7].Visible = !String.IsNullOrEmpty(gvEop.Columns[7].HeaderText);
                        gvEop.Columns[8].Visible = !String.IsNullOrEmpty(gvEop.Columns[8].HeaderText);
                        gvEop.Columns[9].Visible = !String.IsNullOrEmpty(gvEop.Columns[9].HeaderText);
                        gvEop.Columns[10].Visible = !String.IsNullOrEmpty(gvEop.Columns[10].HeaderText);
                        gvEop.Columns[11].Visible = !String.IsNullOrEmpty(gvEop.Columns[11].HeaderText);
                        //
                    }
                    seq++;
                }

            }
            #endregion
            #region "Microscopic Analysis"
            listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals(PA_MICROPIC_DATA) && x.B.Equals(PA_SPECIFICATION)).ToList();
            if (listOfSpec.Count > 1)
            {
                foreach (template_pa_detail pd in PaDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList())
                {
                    PaDetail.Remove(pd);
                }
                int row = 1;
                foreach (var item in listOfSpec)
                {
                    template_pa_detail tmp = new template_pa_detail();
                    tmp.id = CustomUtils.GetRandomNumberID();
                    tmp.seq = row;
                    tmp.col_a = item.A;
                    tmp.col_b = item.B;
                    tmp.col_c = item.C;
                    tmp.col_d = item.D;
                    tmp.col_e = item.E;
                    tmp.col_f = item.G;
                    tmp.col_g = item.G;
                    tmp.col_h = item.H;
                    tmp.col_i = item.I;
                    tmp.col_j = item.J;
                    tmp.col_k = item.K;
                    tmp.col_l = item.L;
                    tmp.col_m = item.M;
                    tmp.col_n = item.N;
                    tmp.col_o = item.O;
                    tmp.col_p = item.P;
                    tmp.col_q = item.Q;
                    tmp.col_r = item.R;

                    tmp.col_s = item.S;
                    tmp.col_t = item.T;
                    tmp.col_u = item.U;
                    tmp.col_v = item.V;
                    tmp.col_w = item.W;
                    tmp.col_x = item.X;
                    tmp.col_y = item.Y;
                    tmp.col_z = item.Z;

                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                    tmp.row_type = Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS);
                    PaDetail.Add(tmp);
                    row++;
                }
            }
            #endregion
        }

        private String mappingRawData(String _val)
        {
            String result = Regex.Replace(_val, @"\t|\n|\r|^\s+|\s+$", String.Empty);
            //HEADER | DB DATA

            Hashtable mappingValues = new Hashtable();
            mappingValues["X ≥ 400"] = "400 ≥ X";
            //-----------------

            mappingValues["25-50"] = "25 ≤ X < 50";
            mappingValues["50-100"] = "50 ≤ X < 100";
            mappingValues["100-150"] = "100 ≤ X < 150";
            mappingValues["150-200"] = "150 ≤ X < 200";
            mappingValues["200-300"] = "200 ≤ X < 300";
            mappingValues["300-400"] = "300 ≤ X < 400";
            mappingValues["400-600"] = "400 ≤ X < 600";
            mappingValues["600-800"] = "600 ≤ X < 800";
            mappingValues["800-1000"] = "800 ≤X < 1000";
            mappingValues[">1000"] = "X > 1000";



            //SST400s(Fe / Cr)



            foreach (DictionaryEntry entry in mappingValues)
            {
                if (Regex.Replace(entry.Key.ToString(), @"\t|\n|\r|^\s+|\s+$", String.Empty).Equals(Regex.Replace(_val, @"\t|\n|\r|^\s+|\s+$", String.Empty)))
                {
                    result = entry.Value.ToString();
                    break;
                }
            }
            return result;
        }

        protected void lbDownloadAtt_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}", this.Pa.attachment_ii_05));

        }
    }
}

