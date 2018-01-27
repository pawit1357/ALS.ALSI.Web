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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class PA01 : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_DHS));
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        #region "Property"
        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }
        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }


        public template_pa pa
        {
            get { return (template_pa)Session[GetType().Name + "pa"]; }
            set { Session[GetType().Name + "pa"] = value; }
        }

        public List<template_pa_detail> paDetail
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

        List<String> errors = new List<string>();

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

            this.CommandName = CommandNameEnum.Add;

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
                //pCoverPage.Visible = true;
                //pSpecification.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC);
                pStatus.Visible = (status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.LABMANAGER_CHECKING);
                pUploadfile.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD);
                pDownload.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnSubmit.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                //btnPage01.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                //btnPage02.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));

                pPage01.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage02.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);
                pPage03.Enabled = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING);


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

                #region ":: STAMP ANALYZED DATE ::"
                if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {
                    if (this.jobSample.date_chemist_alalyze == null)
                    {
                        txtDateAnalyzed.Text = DateTime.Now.ToString("dd MMMM yyy");

                        this.jobSample.date_chemist_alalyze = DateTime.Now;
                        this.jobSample.Update();
                    }
                    txtDateTestComplete.Text = this.jobSample.due_date_lab.Value.ToString("dd MMMM yyy");
                }
                #endregion

                txtAlsReferenceNo.Text = String.Format("ATT/ELN/{0}/{1}", DateTime.Now.Year.ToString().Substring(2, 2), this.jobSample.job_number);
                txtPartDescription.Text = this.jobSample.description;
                txtLotNo.Text = String.Empty;
            }
            #endregion

            template_pa_detail pad = new template_pa_detail();
            this.tbMSpecifications = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            #region "Initial component"
            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = tb_m_specification.getDistinctValue(this.tbMSpecifications.Where(x => x.A.Equals("Evaluation of Particle:")).ToList());
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlContainer.Items.Clear();
            ddlContainer.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Test arrangement / Environment:") && x.B.Equals("Container"));
            ddlContainer.DataBind();
            ddlContainer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid1.Items.Clear();
            ddlFluid1.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Test arrangement / Environment:") && x.B.Equals("Fluid 1"));
            ddlFluid1.DataBind();
            ddlFluid1.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid2.Items.Clear();
            ddlFluid2.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Test arrangement / Environment:") && x.B.Equals("Fluid 2"));
            ddlFluid2.DataBind();
            ddlFluid2.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            ddlFluid3.Items.Clear();
            ddlFluid3.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Test arrangement / Environment:") && x.B.Equals("Fluid 3"));
            ddlFluid3.DataBind();
            ddlFluid3.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlManufacturer.Items.Clear();
            ddlManufacturer.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Analysis membrane used:") && x.B.Equals("Manufacturer"));
            ddlManufacturer.DataBind();
            ddlManufacturer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlMaterial.Items.Clear();
            ddlMaterial.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Analysis membrane used:") && x.B.Equals("Material"));
            ddlMaterial.DataBind();
            ddlMaterial.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlGravimetricAlalysis.Items.Clear();
            ddlGravimetricAlalysis.DataSource = this.tbMSpecifications.Where(x => x.A.Equals("Gravimetric analysis:") && x.B.Equals("Lab Balance"));
            ddlGravimetricAlalysis.DataBind();
            ddlGravimetricAlalysis.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            #endregion

            List<template_pa_detail> listPaDetail = pad.SelectBySampleID(this.SampleID);
            if (listPaDetail.Count > 0)
            {
                this.paDetail = listPaDetail;
            }
            else
            {
                paDetail = new List<template_pa_detail>();

            }



            this.pa = pa.SelectByID(this.SampleID);
            if (this.pa != null)
            {

                ddlSpecification.SelectedValue = this.pa.specification_no.ToString();
                ddlResult.SelectedValue = this.pa.result.ToString();
                txtPIRTDC.Text = this.pa.pirtd;
                #region "PAGE01"
                txtLms.Text = this.pa.lms;
                txtLnmp.Text = this.pa.lnmp;
                txtLf.Text = this.pa.lf;
                txtDoec.Text = this.pa.doec;
                txtDos.Text = this.pa.dos;
                txtCustomerLimit.Text = this.pa.customerlimit;
                txtGravimetry.Text = this.pa.gravimetry;
                txtLmsp.Text = this.pa.lmsp;
                txtExtractionValue.Text = this.pa.extractionvalue;
                txtLnmsp.Text = this.pa.lnmsp;
                txtEop_G.Text = this.pa.eop_g;
                txtEop_Lmsp.Text = this.pa.eop_lmsp;
                txtEop_Lnmsp.Text = this.pa.eop_lnmsp;
                txtEop_pt.Text = this.pa.eop_pt;
                txtEop_size.Text = this.pa.eop_size;
                txtEop_value.Text = this.pa.eop_value;
                txtEopRemark.Text = this.pa.remark;
                #endregion

                #region "PAGE02"
                CustomUtils.setCheckBoxListValue(ref cbCsa, this.pa.iscsa);
                txtWspc.Text = this.pa.wspc;
                txtWvpc.Text = this.pa.wvpc;
                txtTls.Text = this.pa.tls;
                cbPreTreatmentConditioning.Checked = Convert.ToBoolean(this.pa.ispretreatmentconditioning);
                txtPreTreatmentConditioning.Text = this.pa.pretreatmentconditioning;
                CustomUtils.setCheckBoxListValue(ref cbPackingToBeTested, this.pa.ispackingtobetested);

                cbContainer.Checked = Convert.ToBoolean(this.pa.iscontainer);
                ddlContainer.SelectedValue = this.pa.container_id.ToString();

                cbFluid1.Checked = Convert.ToBoolean(this.pa.isfluid1);
                ddlFluid1.SelectedValue = this.pa.fluid1_id.ToString();
                cbFluid2.Checked = Convert.ToBoolean(this.pa.isfluid2);
                ddlFluid2.SelectedValue = this.pa.fluid2_id.ToString();
                cbFluid3.Checked = Convert.ToBoolean(this.pa.isfluid3);
                ddlFluid3.SelectedValue = this.pa.fluid3_id.ToString();

                txtTradeName.Text = this.pa.tradename;
                txtManufacturer.Text = this.pa.manufacturer;
                txtTotalQuantity.Text = this.pa.totalquantity;
                cbTshb01.Checked = Convert.ToBoolean(this.pa.istshb01);
                cbTshb02.Checked = Convert.ToBoolean(this.pa.istshb02);
                cbTshb03.Checked = Convert.ToBoolean(this.pa.istshb03);

                txtTshb03.Text = this.pa.tshb03;
                cbPots01.Checked = Convert.ToBoolean(this.pa.ispots01);
                txtPots01.Text = this.pa.pots01;
                #endregion

                #region "PAGE03"
                cbDissolving.Checked = Convert.ToBoolean(this.pa.isdissolving);
                txtDissolving.Text = this.pa.dissolving;
                txtDissolvingTime.Text = this.pa.dissolvingtime;
                cbPressureRinsing.Checked = Convert.ToBoolean(this.pa.ispressurerinsing);
                cbInternalRinsing.Checked = Convert.ToBoolean(this.pa.isinternalrinsing);
                cbAgitation.Checked = Convert.ToBoolean(this.pa.isagitation);
                cbWashQuantity.Checked = Convert.ToBoolean(this.pa.iswashquantity);
                txtWashQuantity.Text = this.pa.washquantity;
                cbRewashingQuantity.Checked = Convert.ToBoolean(this.pa.isrewashingquantity);
                txtRewashingQuantity.Text = this.pa.rewashingquantity;
                cbWashPressureRinsing.Checked = Convert.ToBoolean(this.pa.iswashpressurerinsing);
                cbWashInternalRinsing.Checked = Convert.ToBoolean(this.pa.iswashinternalrinsing);
                cbWashAgitation.Checked = Convert.ToBoolean(this.pa.iswashagitation);
                CustomUtils.setCheckBoxListValue(ref cbFiltrationMethod, this.pa.isfiltrationmethod);

                ddlManufacturer.SelectedValue = this.pa.manufacturer_id.ToString();
                ddlMaterial.SelectedValue = this.pa.material_id.ToString();
                txtPoreSize.Text = this.pa.poresize;
                txtDiameter.Text = this.pa.diameter;
                cbOven.Checked = Convert.ToBoolean(this.pa.isoven);
                cbDesiccator.Checked = Convert.ToBoolean(this.pa.isdesiccator);
                cbAmbientAir.Checked = Convert.ToBoolean(this.pa.isambientair);
                cbEasyDry.Checked = Convert.ToBoolean(this.pa.iseasydry);
                txtDryTime.Text = this.pa.drytime;
                txtTemperature.Text = this.pa.temperature;
                ddlGravimetricAlalysis.SelectedValue = this.pa.gravimetricalalysis_id.ToString();
                txtModel.Text = this.pa.model;
                txtBalanceResolution.Text = this.pa.balanceresolution;
                txtLastCalibration.Text = this.pa.lastcalibration;
                cbZEISSAxioImager2.Checked = Convert.ToBoolean(this.pa.iszeissaxioimager2);
                cbMeasuringSoftware.Checked = Convert.ToBoolean(this.pa.ismeasuringsoftware);
                cbAutomated.Checked = Convert.ToBoolean(this.pa.isautomated);
                txtAutomated.Text = this.pa.automated;
                txtTotalextractionVolume.Text = this.pa.totalextractionvolume;
                //this.pa.lbextractionmethod = lbExtractionMethod.Text;
                txtNumberOfComponents.Text = this.pa.numberofcomponents;
                lbExtractionTime.Text = this.pa.lbextractiontime;
                lbMembraneType.Text = this.pa.measureddiameter;
                lbX.Text = this.pa.lbx;
                lbY.Text = this.pa.lby;
                txtMeasuredDiameter.Text = this.pa.measureddiameter;
                txtFeretLmsp.Text = this.pa.feretlmsp;
                txtFeretLnms.Text = this.pa.feretlnms;
                txtFeretFb.Text = this.pa.feretfb;
                #endregion


                img1.ImageUrl = this.pa.img01;
                img2.ImageUrl = this.pa.img02;
                img3.ImageUrl = this.pa.img03;
                img4.ImageUrl = this.pa.img04;

                txtLms_X.Text = this.pa.lms_x;
                txtLms_Y.Text = this.pa.lms_y;

                txtLnms_X.Text = this.pa.lnms_x;
                txtLnms_Y.Text = this.pa.lnms_y;

                txtLf_X.Text = this.pa.lf_x;
                txtLf_Y.Text = this.pa.lf_y;

                #region "COLUMN HEADER"

                List<String> cols = new List<string>();
                tb_m_specification selectValue = new tb_m_specification();

                #region "gvEop"
                List<tb_m_specification> listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals("Evaluation of Particle:") && x.B.Equals(ddlSpecification.SelectedItem.Text)).ToList();
                if (listOfSpec.Count > 1)
                {
                    cols = tb_m_specification.findColumnCount(listOfSpec[0]);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvEop.Columns[i].HeaderText = cols[i];
                        gvEop.Columns[i].Visible = true;
                    }
                    for (int i = cols.Count; i < 13; i++)
                    {
                        gvEop.Columns[i].Visible = false;
                    }
                    gvEop.Visible = true;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        gvEop.Columns[i].Visible = false;
                    }
                    gvEop.Visible = false;
                }
                #endregion
                #region "gvMicroscopicAnalysis"
                listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals("Micropic Data:") && x.B.Equals(ddlSpecification.SelectedItem.Text)).ToList();
                if (listOfSpec.Count > 1)
                {
                    cols = tb_m_specification.findColumnCount(listOfSpec[1]);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvMicroscopicAnalysis.Columns[i].HeaderText = cols[i];
                        gvMicroscopicAnalysis.Columns[i].Visible = true;
                    }
                    gvMicroscopicAnalysis.Visible = true;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        gvMicroscopicAnalysis.Columns[i].Visible = false;
                    }
                    gvMicroscopicAnalysis.Visible = false;
                }
                #endregion
                #region "gvDissolving"

                if (cbPressureRinsing.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Pressure rinsing")).FirstOrDefault();
                }
                if (cbInternalRinsing.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Internal rinsing")).FirstOrDefault();
                }
                if (cbWashAgitation.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Agitation")).FirstOrDefault();

                }


                cols = tb_m_specification.findColumnCount(selectValue);
                for (int i = 0; i < cols.Count; i++)
                {
                    gvDissolving.Columns[i].HeaderText = cols[i];
                    gvDissolving.Columns[i].Visible = true;
                }
                #endregion
                #region "gvWashing"
                if (cbWashPressureRinsing.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Pressure rinsing")).FirstOrDefault();
                }
                if (cbWashInternalRinsing.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Internal rinsing")).FirstOrDefault();
                }
                if (cbWashAgitation.Checked)
                {
                    selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Agitation")).FirstOrDefault();
                }

                cols = tb_m_specification.findColumnCount(selectValue);
                for (int i = 0; i < cols.Count; i++)
                {
                    gvWashing.Columns[i].HeaderText = cols[i];
                    gvWashing.Columns[i].Visible = true;
                }
                #endregion


                #endregion


            }
            else
            {
                this.pa = new template_pa();
            }

            pPage01.Visible = true;
            pPage02.Visible = false;
            pPage03.Visible = false;
            pCcc.Visible = false;

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

            List<template_pa_detail> listPaDetail = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvEop.DataSource = listPaDetail;
                gvEop.DataBind();
            }
            listPaDetail = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                //gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
                //gvGravimetry.DataBind();
            }
            listPaDetail = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvMicroscopicAnalysis.DataSource = listPaDetail;
                gvMicroscopicAnalysis.DataBind();
            }
            listPaDetail = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvDissolving.DataSource = listPaDetail;
                gvDissolving.DataBind();
            }
            listPaDetail = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            if (null != listPaDetail && listPaDetail.Count > 0)
            {
                gvWashing.DataSource = listPaDetail;
                gvWashing.DataBind();
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;
            this.pa = new template_pa();

            if (!Page.IsPostBack)
            {
                initialPage();
            }

        }

        #region "Button"

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Boolean isValid = true;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);

                    this.pa.sample_id = this.SampleID;
                    this.pa.specification_no = Convert.ToInt32(ddlSpecification.SelectedValue);
                    this.pa.result = Convert.ToInt32(ddlResult.SelectedValue);
                    this.pa.pirtd = txtPIRTDC.Text;
                    this.jobSample.lot_no = txtLotNo.Text;

                    #region "PAGE01"
                    this.pa.lms = txtLms.Text;
                    this.pa.lnmp = txtLnmp.Text;
                    this.pa.lf = txtLf.Text;
                    this.pa.doec = txtDoec.Text;
                    this.pa.dos = txtDos.Text;
                    this.pa.customerlimit = txtCustomerLimit.Text;
                    this.pa.gravimetry = txtGravimetry.Text;
                    this.pa.lmsp = txtLmsp.Text;
                    this.pa.extractionvalue = txtExtractionValue.Text;
                    this.pa.lnmsp = txtLnmsp.Text;
                    this.pa.eop_g = txtEop_G.Text;
                    this.pa.eop_lmsp = txtEop_Lmsp.Text;
                    this.pa.eop_lnmsp = txtEop_Lnmsp.Text;
                    this.pa.eop_pt = txtEop_pt.Text;
                    this.pa.eop_size = txtEop_size.Text;
                    this.pa.eop_value = txtEop_value.Text;
                    this.pa.remark = txtEopRemark.Text;
                    #endregion
                    #region "PAGE02"
                    this.pa.iscsa = CustomUtils.getCheckBoxListValue(cbCsa);
                    this.pa.wspc = txtWspc.Text;
                    this.pa.wvpc = txtWvpc.Text;
                    this.pa.tls = txtTls.Text;
                    this.pa.ispretreatmentconditioning = (cbPreTreatmentConditioning.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.pretreatmentconditioning = txtPreTreatmentConditioning.Text;
                    this.pa.ispackingtobetested = CustomUtils.getCheckBoxListValue(cbPackingToBeTested);
                    this.pa.iscontainer = (cbContainer.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.container_id = Convert.ToInt32(ddlContainer.SelectedValue);
                    this.pa.isfluid1 = (cbFluid1.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.fluid1_id = Convert.ToInt32(ddlFluid1.SelectedValue);
                    this.pa.isfluid2 = (cbFluid2.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.fluid2_id = Convert.ToInt32(ddlFluid2.SelectedValue);
                    this.pa.isfluid3 = (cbFluid3.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.fluid3_id = Convert.ToInt32(ddlFluid3.SelectedValue);
                    this.pa.tradename = txtTradeName.Text;
                    this.pa.manufacturer = txtManufacturer.Text;
                    this.pa.totalquantity = txtTotalQuantity.Text;
                    this.pa.istshb01 = (cbTshb01.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.istshb02 = (cbTshb02.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.istshb03 = (cbTshb03.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.tshb03 = txtTshb03.Text;
                    this.pa.ispots01 = (cbPots01.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.pots01 = txtPots01.Text;
                    #endregion

                    #region "PAGE03"
                    this.pa.isdissolving = (cbDissolving.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.dissolving = txtDissolving.Text;
                    this.pa.dissolvingtime = txtDissolvingTime.Text;
                    this.pa.ispressurerinsing = (cbPressureRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isinternalrinsing = (cbInternalRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isagitation = (cbAgitation.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.iswashquantity = (cbWashQuantity.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.washquantity = txtWashQuantity.Text;
                    this.pa.isrewashingquantity = (cbRewashingQuantity.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.rewashingquantity = txtRewashingQuantity.Text;
                    this.pa.iswashpressurerinsing = (cbWashPressureRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.iswashinternalrinsing = (cbWashInternalRinsing.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.iswashagitation = (cbWashAgitation.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isfiltrationmethod = CustomUtils.getCheckBoxListValue(cbFiltrationMethod);


                    this.pa.manufacturer_id = Convert.ToInt32(ddlManufacturer.SelectedValue);
                    this.pa.material_id = Convert.ToInt32(ddlMaterial.SelectedValue);
                    this.pa.poresize = txtPoreSize.Text;
                    this.pa.diameter = txtDiameter.Text;
                    this.pa.isoven = (cbOven.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isdesiccator = (cbDesiccator.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isambientair = (cbAmbientAir.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.iseasydry = (cbEasyDry.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.drytime = txtDryTime.Text;
                    this.pa.temperature = txtTemperature.Text;
                    this.pa.gravimetricalalysis_id = Convert.ToInt32(ddlGravimetricAlalysis.SelectedValue);
                    this.pa.model = txtModel.Text;
                    this.pa.balanceresolution = txtBalanceResolution.Text;
                    this.pa.lastcalibration = txtLastCalibration.Text;
                    this.pa.iszeissaxioimager2 = (cbZEISSAxioImager2.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.ismeasuringsoftware = (cbMeasuringSoftware.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.isautomated = (cbAutomated.Checked) ? Convert.ToSByte(1) : Convert.ToSByte(0);
                    this.pa.automated = txtAutomated.Text;
                    this.pa.totalextractionvolume = txtTotalextractionVolume.Text;
                    //this.pa.lbextractionmethod = lbExtractionMethod.Text;
                    this.pa.numberofcomponents = txtNumberOfComponents.Text;
                    this.pa.lbextractiontime = lbExtractionTime.Text;
                    this.pa.measureddiameter = lbMembraneType.Text;
                    this.pa.lbx = lbX.Text;
                    this.pa.lby = lbY.Text;
                    this.pa.measureddiameter = txtMeasuredDiameter.Text;
                    this.pa.feretlmsp = txtFeretLmsp.Text;
                    this.pa.feretlnms = txtFeretLnms.Text;
                    this.pa.feretfb = txtFeretFb.Text;
                    #endregion

                    this.pa.img01 = img1.ImageUrl;
                    this.pa.img02 = img2.ImageUrl;
                    this.pa.img03 = img3.ImageUrl;
                    this.pa.img04 = img4.ImageUrl;

                    this.pa.lms_x = txtLms_X.Text;
                    this.pa.lms_y = txtLms_Y.Text;

                    this.pa.lnms_x = txtLnms_X.Text;
                    this.pa.lnms_y = txtLnms_Y.Text;

                    this.pa.lf_x = txtLf.Text;
                    this.pa.lf_y = txtLf_Y.Text;

                    //Delete old
                    template_pa.DeleteBySampleID(this.SampleID);
                    this.pa.Insert();
                    template_pa_detail.DeleteBySampleID(this.SampleID);
                    foreach (template_pa_detail item in this.paDetail)
                    {
                        item.sample_id = this.SampleID;
                    }
                    template_pa_detail.InsertList(this.paDetail);
                    break;
                case StatusEnum.CHEMIST_TESTING:

                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step2owner = userLogin.id;

                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.pa.sample_id = this.SampleID;
                    //this.pa.largest_metallic_particle = txtLms.Text;
                    //this.pa.largest_non_metallic_particle = txtLnmp.Text;
                    //this.pa.largest_fiber = txtLf.Text;

                    //this.pa.em_extraction_medium = txtExtractionMedium.Text;
                    //this.pa.em_shaking = txtShkingRewashQty.Text;
                    //this.pa.em_wetted_surface_per_component = txtWettedSurfacePerComponent.Text;
                    //this.pa.em_total_tested_size = txtTotalTestedSize.Text;
                    //this.pa.extraction_procedure = txtExtractionProcedure.Text;

                    //this.pa.type_of_method = CustomUtils.getCheckBoxListValue(cbTypeOfMethod);
                    //this.pa.filtration_method = CustomUtils.getCheckBoxListValue(cbFiltrationMethod);
                    //this.pa.type_of_drying = CustomUtils.getCheckBoxListValue(cbTypeOfDrying);
                    //this.pa.particle_sizing_counting_det1 = CustomUtils.getCheckBoxListValue(cbParticleSizingCoungtingDetermination);
                    //this.pa.particle_sizing_counting_det2 = CustomUtils.getCheckBoxListValue(cbParticleSizingCoungtingDetermination2);

                    //this.pa.analysis_membrane_used = txtAnalysisMembraneUsed.Text;
                    //this.pa.particle_sizing_counting_det_a = txtPixelScaling.Text;
                    //this.pa.particle_sizing_counting_det_b = txtCameraResolution.Text;

                    //this.pa.pirtdc = txtPIRTDC.Text;
                    //this.pa.specification_no = ddlSpecification.SelectedItem.Text;
                    //Delete old
                    this.pa.Update();
                    template_pa_detail.DeleteBySampleID(this.SampleID);
                    foreach (template_pa_detail item in this.paDetail)
                    {
                        item.sample_id = this.SampleID;
                    }
                    template_pa_detail.InsertList(this.paDetail);

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
                    if (FileUpload1.HasFile)// && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
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
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (FileUpload1.HasFile) // && (Path.GetExtension(FileUpload1.FileName).Equals(".pdf")))
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
                        this.jobSample.path_pdf = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        this.jobSample.step7owner = userLogin.id;

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
                this.jobSample.Update();

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
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnPage01":
                    btnPage01.CssClass = "btn red-sunglo btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = true;
                    pPage02.Visible = false;
                    pPage03.Visible = false;
                    break;
                case "btnPage02":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn red-sunglo btn-sm";
                    btnPage03.CssClass = "btn btn-default btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = true;
                    pPage03.Visible = false;
                    break;
                case "btnPage03":
                    btnPage01.CssClass = "btn btn-default btn-sm";
                    btnPage02.CssClass = "btn btn-default btn-sm";
                    btnPage03.CssClass = "btn red-sunglo btn-sm";
                    pPage01.Visible = false;
                    pPage02.Visible = false;
                    pPage03.Visible = true;
                    break;
            }
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            List<tb_m_dhs_cas> _cas = new List<tb_m_dhs_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < FileUpload2.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload2.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);

                        #region "Microscopic Analysis"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".csv")))
                        {
                            if (Path.GetFileNameWithoutExtension(_postedFile.FileName).StartsWith("ClassTable_FromNumber_FeretMaximum"))
                            {
                                lbPermembrane.Text = String.Empty;
                                foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList())
                                {
                                    paDetail.Remove(pd);
                                }

                                List<char> cols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
                                DataTable table = new DataTable();
                                foreach (char c in cols)
                                {
                                    table.Columns.Add(c.ToString(), typeof(string));
                                }

                                using (var reader = new StreamReader(source_file))
                                {
                                    int row = 0;
                                    while (!reader.EndOfStream)
                                    {
                                        var line = reader.ReadLine();
                                        var values = line.Split(',');
                                        if (!String.IsNullOrEmpty(values[0]))
                                        {
                                            table.Rows.Add(
                                             (row == 0) ? values[0].Split(':')[0] : values[0],
                                             (row == 0) ? values[1].Split(':')[0] : values[1],
                                             (row == 0) ? values[2].Split(':')[0] : values[2],
                                             (row == 0) ? values[3].Split(':')[0] : values[3],
                                             (row == 0) ? values[4].Split(':')[0] : values[4],
                                             (row == 0) ? values[5].Split(':')[0] : values[5],
                                             (row == 0) ? values[6].Split(':')[0] : values[6],
                                             (row == 0) ? values[7].Split(':')[0] : values[7],
                                             (row == 0) ? values[8].Split(':')[0] : values[8],
                                             (row == 0) ? values[9].Split(':')[0] : values[9],
                                             (row == 0) ? values[10].Split(':')[0] : values[10]
                                             );
                                        }
                                        row++;
                                    }
                                }
                                for (int r = 1; r < table.Columns.Count; r++)
                                {

                                    template_pa_detail tmp = new template_pa_detail();
                                    tmp.id = CustomUtils.GetRandomNumberID();
                                    tmp.col_a = table.Rows[0][r].ToString().Replace("\"", "");
                                    tmp.col_b = table.Rows[1][r].ToString().Replace("\"", "");
                                    tmp.col_c = table.Rows[2][r].ToString().Replace("\"", "");
                                    tmp.col_d = table.Rows[3][r].ToString().Replace("\"", "");

                                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                                    tmp.row_type = Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS);
                                    if (!tmp.col_a.Equals(""))
                                    {
                                        //tmp.col_e = (Convert.ToDouble(tmp.col_c) / Convert.ToDouble(txtParticleSize01.Text)).ToString("N2");
                                        //tmp.col_f = (Convert.ToDouble(tmp.col_d) / Convert.ToDouble(txtParticleSize01.Text)).ToString("N2");
                                        //tmp.col_g = ((Convert.ToDouble(tmp.col_c) / Convert.ToDouble(txtParticleSize02.Text)) * Convert.ToDouble(txtParticleSize03.Text)).ToString("N0");
                                        //tmp.col_h = ((Convert.ToDouble(tmp.col_d) / Convert.ToDouble(txtParticleSize02.Text)) * Convert.ToDouble(txtParticleSize03.Text)).ToString("N0");
                                        lbPermembrane.Text += string.Format("{0}{1}/", tmp.col_a, tmp.col_c);
                                        paDetail.Add(tmp);
                                    }
                                }
                                lbPermembrane.Text = lbPermembrane.Text.Substring(0, lbPermembrane.Text.Length - 1);
                            }
                        }
                        else
                        {
                            //errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.csv"));
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
            if (!FileUpload2.HasFile)
            {
                errors.Add(String.Format("ไม่พบไฟล์ *.csv ที่ใช้โหลดข้อมูล (Ex. ClassTable_FromNumber_FeretMaximum_A01316.csv)"));
            }
            //if (txtParticleSize01.Text.Equals("") || txtParticleSize02.Text.Equals("") || txtParticleSize03.Text.Equals(""))
            //{
            //    errors.Add(String.Format("โปรดระบุข้อมูล Particle Size ที่ใช้สำหรับคำนวณ)"));
            //}

            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                //this.tbCas = _cas;
                //gvResult.DataSource = this.tbCas;
                //gvResult.DataBind();
                calculate();
            }
        }

        protected void btnLoadImg1_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String randNumber = String.Empty;
            String source_file = String.Empty;
            String source_file_url = String.Empty;

            if ((Path.GetExtension(fileUploadImg01.FileName).ToUpper().Equals(".JPG")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg01.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg01.SaveAs(source_file);
                this.pa.img01 = source_file_url;
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

            String randNumber = String.Empty;
            String source_file = String.Empty;
            String source_file_url = String.Empty;

            if ((Path.GetExtension(fileUploadImg02.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg02.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg02.SaveAs(source_file);
                this.pa.img02 = source_file_url;
                img2.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg03.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg03.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg03.SaveAs(source_file);
                this.pa.img03 = source_file_url;
                img3.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg04.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg04.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg04.SaveAs(source_file);
                this.pa.img04 = source_file_url;
                img4.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg05.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg05.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg05.SaveAs(source_file);
                //this.pa.img05 = source_file_url;
                img5.ImageUrl = source_file_url;
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
                    template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
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

                        gvEop.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
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
            gvEop.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
            gvEop.DataBind();
        }

        protected void gvEop_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvEop.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtB = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtB");
            TextBox txtC = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtC");
            TextBox txtD = (TextBox)gvEop.Rows[e.RowIndex].FindControl("txtD");


            template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                _cov.col_b = txtB.Text;
                _cov.col_c = txtC.Text;
                _cov.col_d = txtD.Text;
            }
            gvEop.EditIndex = -1;
            gvEop.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
            gvEop.DataBind();
        }

        protected void gvEop_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEop.EditIndex = -1;
            gvEop.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
            gvEop.DataBind();
        }
        #endregion
        #region "Gravimetry"
        protected void gvGravimetry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.GRAVIMETRY) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
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

                        //gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.GRAVIMETRY)).ToList();
                        //gvGravimetry.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvGravimetry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvGravimetry.DataKeys[e.Row.RowIndex].Values[1]);
                //LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                //LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                //if (_btnHide != null && _btnUndo != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            _btnHide.Visible = false;
                //            _btnUndo.Visible = true;
                //            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                //            break;
                //        default:
                //            _btnHide.Visible = true;
                //            _btnUndo.Visible = false;
                //            e.Row.ForeColor = System.Drawing.Color.Black;
                //            break;
                //    }
                //}

            }
        }

        protected void gvGravimetry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvGravimetry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvGravimetry.EditIndex = e.NewEditIndex;
            //gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
            //gvGravimetry.DataBind();
        }

        protected void gvGravimetry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int _id = Convert.ToInt32(gvGravimetry.DataKeys[e.RowIndex].Values[0].ToString());
            //TextBox txtA = (TextBox)gvGravimetry.Rows[e.RowIndex].FindControl("txtA");
            //TextBox txtB = (TextBox)gvGravimetry.Rows[e.RowIndex].FindControl("txtB");
            //TextBox txtC = (TextBox)gvGravimetry.Rows[e.RowIndex].FindControl("txtC");


            //template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.GRAVIMETRY) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            //if (_cov != null)
            //{
            //    _cov.col_a = txtA.Text;
            //    _cov.col_b = txtB.Text;
            //    _cov.col_c = txtC.Text;
            //}
            //gvGravimetry.EditIndex = -1;
            //gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
            //gvGravimetry.DataBind();
        }

        protected void gvGravimetry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvGravimetry.EditIndex = -1;
            //gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
            //gvGravimetry.DataBind();
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
                    template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
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

                        gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
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
                //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                //TableHeaderCell cell = new TableHeaderCell();
                //cell.Text = "";
                //cell.ColumnSpan = 1;
                //row.Controls.Add(cell);

                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 1;
                //cell.Text = "";
                //row.Controls.Add(cell);

                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 2;
                //cell.Text = "Particle counton membrane";
                //cell.HorizontalAlign = HorizontalAlign.Center;
                //row.Controls.Add(cell);

                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 2;
                //cell.Text = "Particle count /component";
                //cell.HorizontalAlign = HorizontalAlign.Center;
                //row.Controls.Add(cell);

                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 2;
                //cell.Text = "Particle count /1000cm2";
                //cell.HorizontalAlign = HorizontalAlign.Center;
                //row.Controls.Add(cell);
                //cell = new TableHeaderCell();
                //cell.ColumnSpan = 1;
                //cell.Text = "";
                //row.Controls.Add(cell);

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
                gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
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
            gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            gvMicroscopicAnalysis.DataBind();
        }

        protected void gvMicroscopicAnalysis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMicroscopicAnalysis.EditIndex = -1;
            gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            gvMicroscopicAnalysis.DataBind();
        }
        #endregion

        #region "Dissolving"
        protected void gvDissolving_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
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

                        gvDissolving.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING)).ToList();
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
            gvDissolving.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
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

            template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.DISSOLVING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                _cov.col_d = txtD.Text;
                _cov.col_e = txtE.Text;
                _cov.col_f = txtF.Text;
                _cov.col_g = txtG.Text;
                _cov.col_h = txtH.Text;
                _cov.col_i = txtI.Text;
            }
            gvDissolving.EditIndex = -1;
            gvDissolving.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            gvDissolving.DataBind();
        }

        protected void gvDissolving_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDissolving.EditIndex = -1;
            gvDissolving.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList();
            gvDissolving.DataBind();
        }

        #endregion
        #region "Washing"
        protected void gvWashing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
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

                        gvWashing.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING)).ToList();
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
            gvWashing.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
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

            template_pa_detail _cov = paDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.WASHING) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_cov != null)
            {
                _cov.col_d = txtD.Text;
                _cov.col_e = txtE.Text;
                _cov.col_f = txtF.Text;
                _cov.col_g = txtG.Text;
                _cov.col_h = txtH.Text;
                _cov.col_i = txtI.Text;
            }
            gvWashing.EditIndex = -1;
            gvWashing.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            gvWashing.DataBind();
        }

        protected void gvWashing_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWashing.EditIndex = -1;
            gvWashing.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList();
            gvWashing.DataBind();
        }

        #endregion

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                DropDownList ddl = (DropDownList)sender;

                int row = 0;
                #region "gvEop"
                List<tb_m_specification> listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals("Evaluation of Particle:") && x.B.Equals(ddl.SelectedItem.Text)).ToList();
                if (listOfSpec.Count > 1)
                {
                    foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList())
                    {
                        paDetail.Remove(pd);
                    }

                    foreach (var item in listOfSpec)
                    {
                        if (row > 0)
                        {
                            template_pa_detail tmp = new template_pa_detail();
                            tmp.id = CustomUtils.GetRandomNumberID();

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
                            tmp.row_type = Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES);
                            paDetail.Add(tmp);
                        }
                        row++;
                    }


                    List<String> cols = tb_m_specification.findColumnCount(listOfSpec[0]);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvEop.Columns[i].HeaderText = cols[i];
                        gvEop.Columns[i].Visible = true;
                    }
                    for (int i = cols.Count; i < 13; i++)
                    {
                        gvEop.Columns[i].Visible = false;
                    }

                    calculate();
                    gvEop.Visible = true;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        gvEop.Columns[i].Visible = false;
                    }
                    gvEop.Visible = false;
                }
                #endregion
                #region "gvMicroscopicAnalysis"
                row = 0;
                listOfSpec = this.tbMSpecifications.Where(x => x.A.Equals("Micropic Data:") && x.B.Equals(ddl.SelectedItem.Text)).ToList();
                if (listOfSpec.Count > 1)
                {
                    foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList())
                    {
                        paDetail.Remove(pd);
                    }

                    foreach (var item in listOfSpec)
                    {
                        if (row > 0)
                        {
                            template_pa_detail tmp = new template_pa_detail();
                            tmp.id = CustomUtils.GetRandomNumberID();

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
                            paDetail.Add(tmp);
                        }
                        row++;
                    }


                    List<String> cols = tb_m_specification.findColumnCount(listOfSpec[1]);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        gvMicroscopicAnalysis.Columns[i].HeaderText = cols[i];
                        gvMicroscopicAnalysis.Columns[i].Visible = true;
                    }


                    calculate();
                    gvMicroscopicAnalysis.Visible = true;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        gvMicroscopicAnalysis.Columns[i].Visible = false;
                    }
                    gvMicroscopicAnalysis.Visible = false;
                }
                #endregion


            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void ddlMa_SelectedIndexChanged(object sender, EventArgs e)
        {

            //m_microscopic_analysis tem = new m_microscopic_analysis().SelectByID(int.Parse(ddlEop.SelectedValue));

            //if (tem != null)
            //{

            //}
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


            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.SR_CHEMIST_CHECKING:
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.jobSample.ad_hoc_tempalte_path))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.ad_hoc_tempalte_path));
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
                    break;
            }
        }

        protected void ddlFluid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.D;
                txtManufacturer.Text = selectValue.E;
            }
            switch (ddl.SelectedIndex)
            {
                case 0:
                    cbFluid1.Checked = false;
                    break;
                default:
                    cbFluid1.Checked = true;
                    break;
            }
        }

        protected void ddlFluid2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.D;
                txtManufacturer.Text = selectValue.E;
            }
            switch (ddl.SelectedIndex)
            {
                case 0:
                    cbFluid2.Checked = false;
                    break;
                default:
                    cbFluid2.Checked = true;
                    break;
            }
        }

        protected void ddlFluid3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            tb_m_specification selectValue = this.tbMSpecifications.Where(x => x.ID == Convert.ToInt32(ddl.SelectedValue)).FirstOrDefault();
            if (null != selectValue)
            {
                txtTradeName.Text = selectValue.D;
                txtManufacturer.Text = selectValue.E;
            }
            switch (ddl.SelectedIndex)
            {
                case 0:
                    cbFluid3.Checked = false;
                    break;
                default:
                    cbFluid3.Checked = true;
                    break;
            }
        }

        protected void cbFluid1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            switch (ddl.Checked)
            {
                case false:
                    ddlFluid1.SelectedIndex = 0;
                    break;
            }
        }

        protected void cbFluid2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            switch (ddl.Checked)
            {
                case false:
                    ddlFluid2.SelectedIndex = 0;
                    break;
            }
        }

        protected void cbFluid3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            switch (ddl.Checked)
            {
                case false:
                    ddlFluid3.SelectedIndex = 0;
                    break;
            }
        }

        protected void cbDissolving_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            tb_m_specification selectValue = null;
            if (ddl.Checked)
            {
                switch (ddl.ID)
                {
                    case "cbPressureRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Pressure rinsing")).FirstOrDefault();
                        cbPressureRinsing.Checked = true;
                        cbInternalRinsing.Checked = false;
                        cbAgitation.Checked = false;
                        break;
                    case "cbInternalRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Internal rinsing")).FirstOrDefault();
                        cbPressureRinsing.Checked = false;
                        cbInternalRinsing.Checked = true;
                        cbAgitation.Checked = false;
                        break;
                    case "cbAgitation":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Dissolving") && x.C.Equals("Agitation")).FirstOrDefault();
                        cbPressureRinsing.Checked = false;
                        cbInternalRinsing.Checked = false;
                        cbAgitation.Checked = true;
                        break;
                }

                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                    {
                        paDetail.Remove(pd);
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
                    paDetail.Add(tmp);

                }
            }
            else
            {
                foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.DISSOLVING)).ToList())
                {
                    paDetail.Remove(pd);
                }
            }
            calculate();
        }

        protected void cbWashQuantity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ddl = (CheckBox)sender;
            tb_m_specification selectValue = null;
            if (ddl.Checked)
            {
                switch (ddl.ID)
                {
                    case "cbWashPressureRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Pressure rinsing")).FirstOrDefault();
                        cbWashPressureRinsing.Checked = true;
                        cbWashInternalRinsing.Checked = false;
                        cbWashAgitation.Checked = false;
                        break;
                    case "cbWashInternalRinsing":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Internal rinsing")).FirstOrDefault();
                        cbWashPressureRinsing.Checked = false;
                        cbWashInternalRinsing.Checked = true;
                        cbWashAgitation.Checked = false;
                        break;
                    case "cbWashAgitation":
                        selectValue = this.tbMSpecifications.Where(x => x.A.Equals("Description of process and extraction:") && x.B.Equals("Washing") && x.C.Equals("Agitation")).FirstOrDefault();
                        cbWashPressureRinsing.Checked = false;
                        cbWashInternalRinsing.Checked = false;
                        cbWashAgitation.Checked = true;
                        break;
                }

                if (null != selectValue)
                {
                    foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                    {
                        paDetail.Remove(pd);
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
                    paDetail.Add(tmp);

                }
            }
            else
            {
                foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.WASHING)).ToList())
                {
                    paDetail.Remove(pd);
                }
            }
            calculate();


        }

        #endregion

        protected void ddlGravimetricAlalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            ////txtModel;
            ////txtBalanceResolution;
            ////txtLastCalibration;
        }
    }
}

