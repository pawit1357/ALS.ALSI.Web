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
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_GCMS_5 : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_GCMS));

        #region "Property"
        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }
        public List<tb_m_gcms_cas> tbCas
        {
            get { return (List<tb_m_gcms_cas>)Session[GetType().Name + "tbCas"]; }
            set { Session[GetType().Name + "tbCas"] = value; }
        }

        public List<template_seagate_gcms_coverpage> coverpages
        {
            get { return (List<template_seagate_gcms_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
        }
        public List<template_img> refImg
        {
            get { return (List<template_img>)Session[GetType().Name + "refImg"]; }
            set { Session[GetType().Name + "refImg"] = value; }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public List<ws_hash_value> WsHashValue
        {
            get { return (List<ws_hash_value>)Session[GetType().Name + "WsHashValue"]; }
            set { Session[GetType().Name + "WsHashValue"] = value; }
        }

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
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

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));


            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();

            ddlUnitCompound.Items.Clear();
            ddlUnitCompound.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitCompound.DataBind();
            ddlUnitCompound.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitCompoundSub.Items.Clear();
            ddlUnitCompoundSub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitCompoundSub.DataBind();
            ddlUnitCompoundSub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorBaseSub.Items.Clear();
            ddlUnitMotorBaseSub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorBaseSub.DataBind();
            ddlUnitMotorBaseSub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorBase.Items.Clear();
            ddlUnitMotorBase.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorBase.DataBind();
            ddlUnitMotorBase.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorHubSub.Items.Clear();
            ddlUnitMotorHubSub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorHubSub.DataBind();
            ddlUnitMotorHubSub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorHub.Items.Clear();
            ddlUnitMotorHub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorHub.DataBind();
            ddlUnitMotorHub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorOilContamination.Items.Clear();
            ddlUnitMotorOilContamination.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorOilContamination.DataBind();
            ddlUnitMotorOilContamination.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));




            /*INFO*/
            if (this.WsHashValue != null)
            {
                try
                {
                    this.WsHashValue = new ws_hash_value().SelectBySampleID(this.SampleID);
                    txtFloatResult01.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult01")).FirstOrDefault().val;
                    txtFloatResult02.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult02")).FirstOrDefault().val;
                    txtFloatResult03.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult03")).FirstOrDefault().val;
                    txtFloatResult04.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult04")).FirstOrDefault().val;
                    txtFloatResult05.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult05")).FirstOrDefault().val;
                    txtFloatResult06.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult06")).FirstOrDefault().val;
                    txtFloatResult07.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult07")).FirstOrDefault().val;
                    txtFloatResult08.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult08")).FirstOrDefault().val;
                    txtFloatResult09.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult09")).FirstOrDefault().val;
                    txtFloatResult10.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult10")).FirstOrDefault().val;
                    txtFloatResult11.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult11")).FirstOrDefault().val;
                    txtFloatResult12.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult12")).FirstOrDefault().val;
                    txtFloatResult13.Text = this.WsHashValue.Where(x => x.key.Equals("txtFloatResult13")).FirstOrDefault().val;
                    txtDecimal01.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal01")).FirstOrDefault().val;
                    txtDecimal02.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal02")).FirstOrDefault().val;
                    txtDecimal06.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal06")).FirstOrDefault().val;
                    txtDecimal07.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal07")).FirstOrDefault().val;
                    txtDecimal08.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal08")).FirstOrDefault().val;
                    txtDecimal09.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal09")).FirstOrDefault().val;
                    txtDecimal10.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal10")).FirstOrDefault().val;
                    txtDecimal11.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal11")).FirstOrDefault().val;
                    txtDecimal12.Text = this.WsHashValue.Where(x => x.key.Equals("txtDecimal12")).FirstOrDefault().val;
                }
                catch (Exception ) { }
            }
            ///////
            this.jobSample = new job_sample().SelectByID(this.SampleID);
            this.coverpages = template_seagate_gcms_coverpage.FindAllBySampleID(this.SampleID);

            #region "CAS"
            this.tbCas = tb_m_gcms_cas.FindAllBySampleID(this.SampleID);
            if (this.tbCas != null && this.tbCas.Count > 0 && this.coverpages != null && this.coverpages.Count > 0)
            {
                this.coverpages = this.coverpages.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal));

                gvRHCBase.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_BASE) && !x.library_id.Equals("0")).ToList();
                gvRHCBase.DataBind();
                gvRHCHub.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_HUB) && !x.library_id.Equals("0")).ToList();
                gvRHCHub.DataBind();
            }

            #endregion

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
                        }
                        break;
                    case RoleEnum.SR_CHEMIST:
                        if (status == StatusEnum.SR_CHEMIST_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
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
                        }
                        break;
                    case RoleEnum.LABMANAGER:
                        if (status == StatusEnum.LABMANAGER_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = true;
                            btnSubmit.Visible = true;
                        }
                        break;
                }
                txtDateAnalyzed.Text = (this.jobSample.date_chemist_analyze != null) ? this.jobSample.date_chemist_analyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

                #region "VISIBLE RESULT DATA"
                if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {
                    //#region ":: STAMP ANALYZED DATE ::"
                    //if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    //{
                    //    if (this.jobSample.date_chemist_analyze == null)
                    //    {
                    //        this.jobSample.date_chemist_analyze = DateTime.Now;
                    //        this.jobSample.Update();
                    //    }
                    //}
                    //#endregion


                    txtProcedure.Enabled = true;
                    txtSampleSize.Enabled = true;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolumn.Enabled = true;
                    btnCoverPage.Visible = true;
                    btnRH.Visible = true;
                    btnExtractable.Visible = true;
                    btnMotorOil.Visible = true;
                }
                else
                {
                    txtProcedure.Enabled = false;
                    txtSampleSize.Enabled = false;
                    txtExtractionMedium.Enabled = false;
                    txtExtractionVolumn.Enabled = false;
                    btnCoverPage.Visible = false;
                    btnRH.Visible = false;
                    btnExtractable.Visible = false;
                    btnMotorOil.Visible = false;


                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnCoverPage.Visible = true;
                        btnRH.Visible = true;
                        btnExtractable.Visible = true;
                        btnMotorOil.Visible = true;
                    }
                }
                #endregion
                #region "COVERPAGE"
                if (this.coverpages != null && this.coverpages.Count > 0)
                {

                    this.CommandName = CommandNameEnum.Edit;
                    //Result Description
                    template_seagate_gcms_coverpage cov = this.coverpages[0];
                    tb_m_component component = new tb_m_component().SelectByID(this.coverpages[0].component_id.Value);// this.coverpages[0].tb_m_component;
                    if (component != null)
                    {
                        ddlComponent.SelectedValue = component.ID.ToString();
                        ddlUnitMotorOilContamination.SelectedValue = this.coverpages[0].UnitMotorOilContamination == null ? "0" : this.coverpages[0].UnitMotorOilContamination.ToString();
                        ddlUnitMotorHub.SelectedValue = this.coverpages[0].UnitMotorHub == null ? "0" : this.coverpages[0].UnitMotorHub.ToString();
                        ddlUnitMotorHubSub.SelectedValue = this.coverpages[0].UnitMotorHubSub == null ? "0" : this.coverpages[0].UnitMotorHubSub.ToString();
                        ddlUnitMotorBase.SelectedValue = this.coverpages[0].UnitMotorBase == null ? "0" : this.coverpages[0].UnitMotorBase.ToString();
                        ddlUnitMotorBaseSub.SelectedValue = this.coverpages[0].UnitMotorBaseSub == null ? "0" : this.coverpages[0].UnitMotorBaseSub.ToString();
                        ddlUnitCompound.SelectedValue = this.coverpages[0].UnitCompound == null ? "0" : this.coverpages[0].UnitCompound.ToString();
                        ddlUnitCompoundSub.SelectedValue = this.coverpages[0].UnitCompoundSub == null ? "0" : this.coverpages[0].UnitCompoundSub.ToString();



                        cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                        if (cbCheckBox.Checked)
                        {
                            lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
                        }
                        else
                        {
                            lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);

                        }
                    }
                    txtProcedure.Text = cov.procedure_no;
                    txtSampleSize.Text = cov.sample_size;
                    txtExtractionMedium.Text = cov.extraction_medium;
                    txtExtractionVolumn.Text = cov.extraction_volumn;
                    //
                    txtB13.Text = cov.B13;
                    txtB14.Text = cov.B14;
                    txtB15.Text = cov.B15;
                    txtB16.Text = cov.B16;
                    txtB17.Text = cov.B17;
                    txtB18.Text = cov.B18;
                    txtB19.Text = cov.B19;

                    txtC13.Text = cov.C13;
                    txtC14.Text = cov.C14;
                    txtC15.Text = cov.C15;
                    txtC16.Text = cov.C16;
                    txtC17.Text = cov.C17;
                    txtC18.Text = cov.C18;
                    txtC19.Text = cov.C19;

                    txtB20.Text = cov.B20;
                    txtB21.Text = cov.B21;
                    txtB22.Text = cov.B22;
                    txtB23.Text = cov.B23;
                    txtB24.Text = cov.B24;
                    txtB25.Text = cov.B25;
                    txtB26.Text = cov.B26;
                    txtB27.Text = cov.B27;
                    txtB28.Text = cov.B28;
                    txtB29.Text = cov.B29;
                    txtB30.Text = cov.B30;
                    txtB31.Text = cov.B31;
                    txtB32.Text = cov.B32;
                    txtB33.Text = cov.B33;
                    txtB34.Text = cov.B34;


                    txtC20.Text = cov.C20;
                    txtC21.Text = cov.C21;
                    txtC22.Text = cov.C22;
                    txtC23.Text = cov.C23;
                    txtC24.Text = cov.C24;
                    txtC25.Text = cov.C25;
                    txtC26.Text = cov.C26;
                    txtC27.Text = cov.C27;
                    txtC28.Text = cov.C28;
                    txtC29.Text = cov.C29;
                    txtC30.Text = cov.C30;
                    txtC31.Text = cov.C31;
                    txtC32.Text = cov.C32;
                    txtC33.Text = cov.C33;
                    txtC34.Text = cov.C34;


                    txtD20.Text = cov.D20;
                    txtD21.Text = cov.D21;
                    txtD22.Text = cov.D22;
                    txtD23.Text = cov.D23;
                    txtD24.Text = cov.D24;
                    txtD25.Text = cov.D25;
                    txtD26.Text = cov.D26;
                    txtD27.Text = cov.D27;
                    txtD28.Text = cov.D28;
                    txtD29.Text = cov.D29;
                    txtD30.Text = cov.D30;
                    txtD31.Text = cov.D31;
                    txtD32.Text = cov.D32;
                    txtD33.Text = cov.D33;
                    txtD34.Text = cov.D34;


                    txtE20.Text = cov.E20;
                    txtE21.Text = cov.E21;
                    txtE22.Text = cov.E22;
                    txtE23.Text = cov.E23;
                    txtE24.Text = cov.E24;
                    txtE25.Text = cov.E25;
                    txtE26.Text = cov.E26;
                    txtE27.Text = cov.E27;
                    txtE28.Text = cov.E28;
                    txtE29.Text = cov.E29;
                    txtE30.Text = cov.E30;
                    txtE31.Text = cov.E31;
                    txtE32.Text = cov.E32;
                    txtE33.Text = cov.E33;
                    txtE34.Text = cov.E34;


                    txtF20.Text = cov.F20;
                    txtF21.Text = cov.F21;
                    txtF22.Text = cov.F22;
                    txtF23.Text = cov.F23;
                    txtF24.Text = cov.F24;
                    txtF25.Text = cov.F25;
                    txtF26.Text = cov.F26;
                    txtF27.Text = cov.F27;
                    txtF28.Text = cov.F28;
                    txtF29.Text = cov.F29;
                    txtF30.Text = cov.F30;
                    txtF31.Text = cov.F31;
                    txtF32.Text = cov.F32;
                    txtF33.Text = cov.F33;
                    txtF34.Text = cov.F34;

                    txtB40.Text = cov.B40;
                    txtB41.Text = cov.B41;
                    txtB42.Text = cov.B42;
                    txtB43.Text = cov.B43;
                    txtB44.Text = cov.B44;
                    txtB45.Text = cov.B45;
                    txtB46.Text = cov.B46;
                    txtB47.Text = cov.B47;
                    txtB48.Text = cov.B48;
                    txtB49.Text = cov.B49;
                    txtB50.Text = cov.B50;
                    txtB51.Text = cov.B51;
                    txtB52.Text = cov.B52;
                    txtB53.Text = cov.B53;
                    txtB54.Text = cov.B54;



                    txtC40.Text = cov.C40;
                    txtC41.Text = cov.C41;
                    txtC42.Text = cov.C42;
                    txtC43.Text = cov.C43;
                    txtC44.Text = cov.C44;
                    txtC45.Text = cov.C45;
                    txtC46.Text = cov.C46;
                    txtC47.Text = cov.C47;
                    txtC48.Text = cov.C48;
                    txtC49.Text = cov.C49;
                    txtC50.Text = cov.C50;
                    txtC51.Text = cov.C51;
                    txtC52.Text = cov.C52;
                    txtC53.Text = cov.C53;
                    txtC54.Text = cov.C54;


                    txtD40.Text = cov.D40;
                    txtD41.Text = cov.D41;
                    txtD42.Text = cov.D42;
                    txtD43.Text = cov.D43;
                    txtD44.Text = cov.D44;
                    txtD45.Text = cov.D45;
                    txtD46.Text = cov.D46;
                    txtD47.Text = cov.D47;
                    txtD48.Text = cov.D48;
                    txtD49.Text = cov.D49;
                    txtD50.Text = cov.D50;
                    txtD51.Text = cov.D51;
                    txtD52.Text = cov.D52;
                    txtD53.Text = cov.D53;
                    txtD54.Text = cov.D54;


                    lbRemark1.Text = cov.remark1;
                    lbRemark2.Text = cov.remark2;
                    lbRemark3.Text = cov.remark3;
                    lbRemark4.Text = cov.remark4;
                    lbRemark5.Text = cov.remark5;


                    //
                    txtD13.Text=cov.D13;
                    txtD14.Text=cov.D14;
                    txtD15.Text=cov.D15;
                    txtD16.Text=cov.D16;
                    txtD17.Text=cov.D17;
                    txtD18.Text=cov.D18;
                    txtD19.Text= cov.D19;
                    //
                    txtE13.Text=cov.E13;
                    txtE14.Text=cov.E14;
                    txtE15.Text=cov.E15;
                    txtE16.Text=cov.E16;
                    txtE17.Text=cov.E17;
                    txtE18.Text=cov.E18;
                    txtE19.Text= cov.E19;
                    //

                    template_seagate_gcms_coverpage_2 cov2 = template_seagate_gcms_coverpage_2.FindBySampleID(this.SampleID);
                    if(cov2 != null)
                    {
                        txtG20.Text = cov2.G20;
                        txtG21.Text = cov2.G21;
                        txtG22.Text = cov2.G22;
                        txtG23.Text = cov2.G23;
                        txtG24.Text = cov2.G24;
                        txtG25.Text = cov2.G25;
                        txtG26.Text = cov2.G26;
                        txtG27.Text = cov2.G27;
                        txtG28.Text = cov2.G28;
                        txtG29.Text = cov2.G29;
                        txtG30.Text = cov2.G30;
                        txtG31.Text = cov2.G31;
                        txtG32.Text = cov2.G32;
                        txtG33.Text = cov2.G33;
                        txtG34.Text = cov2.G34;

                        txtE40.Text = cov2.E40;
                        txtE41.Text = cov2.E41;
                        txtE42.Text = cov2.E42;
                        txtE43.Text = cov2.E43;
                        txtE44.Text = cov2.E44;
                        txtE45.Text = cov2.E45;
                        txtE46.Text = cov2.E46;
                        txtE47.Text = cov2.E47;
                        txtE48.Text = cov2.E48;
                        txtE49.Text = cov2.E49;
                        txtE50.Text = cov2.E50;
                        txtE51.Text = cov2.E51;
                        txtE52.Text = cov2.E52;
                        txtE53.Text = cov2.E53;
                        txtE54.Text = cov2.E54;
                    }


                    #region "Unit"
                    gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                    gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                    gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                    gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                    gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBase.SelectedItem.Text);
                    gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                    gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                    gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                    gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHub.SelectedItem.Text);
                    gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                    gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompound.SelectedItem.Text);
                    gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                    gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompoundSub.SelectedItem.Text);
                    gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompoundSub.SelectedItem.Text);
                    #endregion
                    GenerrateCoverPage();

                }
                #endregion
            }

            #endregion



            //initial button.
            btnCoverPage.CssClass = "btn blue";
            btnRH.CssClass = "btn green";
            btnExtractable.CssClass = "btn green";
            btnMotorOil.CssClass = "btn green";

            pCoverpage.Visible = true;
            pRH.Visible = false;
            pExtractable.Visible = false;
            pLoadFile.Visible = false;
            pMotorOil.Visible = false;

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

        List<String> errors = new List<string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
                #region "IMG"
                this.refImg = template_img.FindAllBySampleID(this.SampleID);
                if (this.refImg != null && this.refImg.Count > 0)
                {
                    gvRefImages.DataSource = this.refImg;
                    gvRefImages.DataBind();
                }
                #endregion
            }
        }

        #region "Button"

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //Boolean isValid = true;
            //DateTime dtStart = DateTime.Now;
            //StringBuilder sbLog = new StringBuilder();

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;
                    template_seagate_gcms_coverpage.DeleteBySampleID(this.SampleID);
                    tb_m_gcms_cas.DeleteBySampleID(this.SampleID);
                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    #region "Cover Page#"
                    foreach (template_seagate_gcms_coverpage cov in this.coverpages)
                    {
                        cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        cov.procedure_no = txtProcedure.Text;
                        cov.sample_size = txtSampleSize.Text;
                        cov.extraction_medium = txtExtractionMedium.Text;
                        cov.extraction_volumn = txtExtractionVolumn.Text;

                        //cov.UnitMotorOilContamination = Convert.ToInt32(ddlUnitMotorOilContamination.SelectedValue);
                        //cov.UnitMotorHub = Convert.ToInt32(ddlUnitMotorHub.SelectedValue);
                        //cov.UnitMotorHubSub = Convert.ToInt32(ddlUnitMotorHubSub.SelectedValue);
                        cov.UnitMotorBase = Convert.ToInt32(ddlUnitMotorBase.SelectedValue);
                        cov.UnitMotorBaseSub = Convert.ToInt32(ddlUnitMotorBaseSub.SelectedValue);
                        //cov.UnitCompound = Convert.ToInt32(ddlUnitCompound.SelectedValue);


                    }
                    //template_seagate_gcms_coverpage.DeleteBySampleID(this.SampleID);
                    MaintenanceBiz.ExecuteReturnDt(string.Format("delete from template_seagate_gcms_coverpage where sample_id={0}", this.SampleID));
                    template_seagate_gcms_coverpage.InsertList(this.coverpages);


                    #endregion

                    this.jobSample.date_login_complete = DateTime.Now;
                    this.jobSample.date_chemist_analyze = DateTime.Now;
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.tbCas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                        this.jobSample.path_word = String.Empty;
                        this.jobSample.path_pdf = String.Empty;
                        #region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        this.jobSample.date_srchemist_analyze = DateTime.Now;
                        #endregion
                        #region "CAS#"

                        MaintenanceBiz.ExecuteReturnDt(string.Format("delete from tb_m_gcms_cas where sample_id={0}", this.SampleID));

                        //tb_m_gcms_cas.DeleteBySampleID(this.SampleID);
                        //sbLog.Append("tb_m_gcms_cas time(del):" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");
                        tb_m_gcms_cas.InsertList(this.tbCas);
                        //sbLog.Append("tb_m_gcms_cas time:" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");

                        #endregion
                        #region "Cover Page#"
                        foreach (template_seagate_gcms_coverpage cov in this.coverpages)
                        {
                            cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                            cov.procedure_no = txtProcedure.Text;
                            cov.sample_size = txtSampleSize.Text;
                            cov.extraction_medium = txtExtractionMedium.Text;
                            cov.extraction_volumn = txtExtractionVolumn.Text;
                            //
                            cov.B13 = txtB13.Text;
                            cov.B14 = txtB14.Text;
                            cov.B15 = txtB15.Text;
                            cov.B16 = txtB16.Text;
                            cov.B17 = txtB17.Text;
                            cov.B18 = txtB18.Text;
                            cov.B19 = txtB19.Text;

                            cov.C13 = txtC13.Text;
                            cov.C14 = txtC14.Text;
                            cov.C15 = txtC15.Text;
                            cov.C16 = txtC16.Text;
                            cov.C17 = txtC17.Text;
                            cov.C18 = txtC18.Text;
                            cov.C19 = txtC19.Text;

                            cov.B20 = txtB20.Text;
                            cov.B21 = txtB21.Text;
                            cov.B22 = txtB22.Text;
                            cov.B23 = txtB23.Text;
                            cov.B24 = txtB24.Text;
                            cov.B25 = txtB25.Text;
                            cov.B26 = txtB26.Text;
                            cov.B27 = txtB27.Text;
                            cov.B28 = txtB28.Text;
                            cov.B29 = txtB29.Text;
                            cov.B30 = txtB30.Text;
                            cov.B31 = txtB31.Text;
                            cov.B32 = txtB32.Text;
                            cov.B33=txtB33.Text ;
                            cov.B34= txtB34.Text;




                            cov.C20 = txtC20.Text;
                            cov.C21 = txtC21.Text;
                            cov.C22 = txtC22.Text;
                            cov.C23 = txtC23.Text;
                            cov.C24 = txtC24.Text;
                            cov.C25 = txtC25.Text;
                            cov.C26 = txtC26.Text;
                            cov.C27 = txtC27.Text;
                            cov.C28 = txtC28.Text;
                            cov.C29 = txtC29.Text;
                            cov.C30 = txtC30.Text;
                            cov.C31 = txtC31.Text;
                            cov.C32 = txtC32.Text;
                            cov.C33=txtC33.Text;
                            cov.C34= txtC34.Text;


                            cov.D20 = txtD20.Text;
                            cov.D21 = txtD21.Text;
                            cov.D22 = txtD22.Text;
                            cov.D23 = txtD23.Text;
                            cov.D24 = txtD24.Text;
                            cov.D25 = txtD25.Text;
                            cov.D26 = txtD26.Text;
                            cov.D27 = txtD27.Text;
                            cov.D28 = txtD28.Text;
                            cov.D29 = txtD29.Text;
                            cov.D30 = txtD30.Text;
                            cov.D31 = txtD31.Text;
                            cov.D32 = txtD32.Text;
                            cov.D33=txtD33.Text;
                            cov.D34= txtD34.Text;


                            cov.E20 = txtE20.Text;
                            cov.E21 = txtE21.Text;
                            cov.E22 = txtE22.Text;
                            cov.E23 = txtE23.Text;
                            cov.E24 = txtE24.Text;
                            cov.E25 = txtE25.Text;
                            cov.E26 = txtE26.Text;
                            cov.E27 = txtE27.Text;
                            cov.E28 = txtE28.Text;
                            cov.E29 = txtE29.Text;
                            cov.E30 = txtE30.Text;
                            cov.E31 = txtE31.Text;
                            cov.E32 = txtE32.Text;
                            cov.E33 = txtE33.Text;
                            cov.E34 = txtE34.Text;


                            cov.F20 = txtF20.Text;
                            cov.F21 = txtF21.Text;
                            cov.F22 = txtF22.Text;
                            cov.F23 = txtF23.Text;
                            cov.F24 = txtF24.Text;
                            cov.F25 = txtF25.Text;
                            cov.F26 = txtF26.Text;
                            cov.F27 = txtF27.Text;
                            cov.F28 = txtF28.Text;
                            cov.F29 = txtF29.Text;
                            cov.F30 = txtF30.Text;
                            cov.F31 = txtF31.Text;
                            cov.F32 = txtF32.Text;
                            cov.F33=txtF33.Text;
                            cov.F34= txtF34.Text;


                            cov.B40 = txtB40.Text;
                            cov.B41 = txtB41.Text;
                            cov.B42 = txtB42.Text;
                            cov.B43 = txtB43.Text;
                            cov.B44 = txtB44.Text;
                            cov.B45 = txtB45.Text;
                            cov.B46 = txtB46.Text;
                            cov.B47 = txtB47.Text;
                            cov.B48 = txtB48.Text;
                            cov.B49 = txtB49.Text;
                            cov.B40 = txtB40.Text;
                            cov.B50 = txtB50.Text;
                            cov.B51 = txtB51.Text;
                            cov.B52 = txtB52.Text;
                            cov.B53 = txtB53.Text;
                            cov.B54 = txtB54.Text;

                            cov.C40 = txtC40.Text;
                            cov.C41 = txtC41.Text;
                            cov.C42 = txtC42.Text;
                            cov.C43 = txtC43.Text;
                            cov.C44 = txtC44.Text;
                            cov.C45 = txtC45.Text;
                            cov.C46 = txtC46.Text;
                            cov.C47 = txtC47.Text;
                            cov.C48 = txtC48.Text;
                            cov.C49 = txtC49.Text;
                            cov.C40 = txtC40.Text;
                            cov.C50 = txtC50.Text;
                            cov.C51 = txtC51.Text;
                            cov.C52 = txtC52.Text;
                            cov.C53 = txtC53.Text;
                            cov.C54 = txtC54.Text;

                            cov.D40 = (txtD40.Text.Length > 20) ? "" : txtD40.Text;
                            cov.D41 = (txtD41.Text.Length > 20) ? "" : txtD41.Text;
                            cov.D42 = (txtD42.Text.Length > 20) ? "" : txtD42.Text;
                            cov.D43 = (txtD43.Text.Length > 20) ? "" : txtD43.Text;
                            cov.D44 = (txtD44.Text.Length > 20) ? "" : txtD44.Text;
                            cov.D45 = (txtD45.Text.Length > 20) ? "" : txtD45.Text;
                            cov.D46 = (txtD46.Text.Length > 20) ? "" : txtD46.Text;
                            cov.D47 = (txtD47.Text.Length > 20) ? "" : txtD47.Text;
                            cov.D48 = (txtD48.Text.Length > 20) ? "" : txtD48.Text;
                            cov.D49 = (txtD49.Text.Length > 20) ? "" : txtD49.Text;
                            cov.D50 = (txtD50.Text.Length > 20) ? "" : txtD50.Text;
                            cov.D51 = (txtD51.Text.Length > 20) ? "" : txtD51.Text;
                            cov.D52 = (txtD52.Text.Length > 20) ? "" : txtD52.Text;
                            cov.D53 = (txtD53.Text.Length > 20) ? "" : txtD53.Text;
                            cov.D54 = (txtD54.Text.Length > 20) ? "" : txtD54.Text;


                            //
                            cov.D13 = (String.IsNullOrEmpty(txtD13.Text)) ? "" : txtD13.Text;
                            cov.D14 = (String.IsNullOrEmpty(txtD14.Text)) ? "" : txtD14.Text;
                            cov.D15 = (String.IsNullOrEmpty(txtD15.Text)) ? "" : txtD15.Text;
                            cov.D16 = (String.IsNullOrEmpty(txtD16.Text)) ? "" : txtD16.Text;
                            cov.D17 = (String.IsNullOrEmpty(txtD17.Text)) ? "" : txtD17.Text;
                            cov.D18 = (String.IsNullOrEmpty(txtD18.Text)) ? "" : txtD18.Text;
                            cov.D19 = (String.IsNullOrEmpty(txtD19.Text)) ? "" : txtD19.Text;
                            //
                            cov.E13 = (String.IsNullOrEmpty(txtE13.Text)) ? "" : txtE13.Text;
                            cov.E14 = (String.IsNullOrEmpty(txtE14.Text)) ? "" : txtE14.Text;
                            cov.E15 = (String.IsNullOrEmpty(txtE15.Text)) ? "" : txtE15.Text;
                            cov.E16 = (String.IsNullOrEmpty(txtE16.Text)) ? "" : txtE16.Text;
                            cov.E17 = (String.IsNullOrEmpty(txtE17.Text)) ? "" : txtE17.Text;
                            cov.E18 = (String.IsNullOrEmpty(txtE18.Text)) ? "" : txtE18.Text;
                            cov.E19 = (String.IsNullOrEmpty(txtE19.Text)) ? "" : txtE19.Text;
                            //



                            cov.UnitMotorOilContamination = Convert.ToInt32(ddlUnitMotorOilContamination.SelectedValue);
                            cov.UnitMotorHub = Convert.ToInt32(ddlUnitMotorHub.SelectedValue);
                            cov.UnitMotorHubSub = Convert.ToInt32(ddlUnitMotorHubSub.SelectedValue);
                            cov.UnitMotorBase = Convert.ToInt32(ddlUnitMotorBase.SelectedValue);
                            cov.UnitMotorBaseSub = Convert.ToInt32(ddlUnitMotorBaseSub.SelectedValue);
                            cov.UnitCompound = Convert.ToInt32(ddlUnitCompound.SelectedValue);
                            cov.UnitCompoundSub = Convert.ToInt32(ddlUnitCompoundSub.SelectedValue);

                            cov.remark1 = lbRemark1.Text;
                            cov.remark2 = lbRemark2.Text;
                            cov.remark3 = lbRemark3.Text;
                            cov.remark4 = lbRemark4.Text;
                            cov.remark5 = lbRemark5.Text;
                        }
                        MaintenanceBiz.ExecuteReturnDt(string.Format("delete from template_seagate_gcms_coverpage where sample_id={0}", this.SampleID));
                        MaintenanceBiz.ExecuteReturnDt(string.Format("delete from template_seagate_gcms_coverpage_2 where sample_id={0}", this.SampleID));

                        //template_seagate_gcms_coverpage.DeleteBySampleID(this.SampleID);
                        //sbLog.Append("template_seagate_gcms_coverpage time(del):" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");
                        template_seagate_gcms_coverpage.InsertList(this.coverpages);
                        //sbLog.Append("template_seagate_gcms_coverpage time:" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");

                        template_seagate_gcms_coverpage_2 cov2 = new template_seagate_gcms_coverpage_2();
                        cov2.sample_id = this.SampleID;
                        cov2.component_id= Convert.ToInt32(ddlComponent.SelectedValue);
                        cov2.G20 = txtG20.Text;
                            cov2.G21 = txtG21.Text;
                            cov2.G22 = txtG22.Text;
                            cov2.G23 = txtG23.Text;
                            cov2.G24 = txtG24.Text;
                            cov2.G25 = txtG25.Text;
                            cov2.G26 = txtG26.Text;
                            cov2.G27 = txtG27.Text;
                            cov2.G28 = txtG28.Text;
                            cov2.G29 = txtG29.Text;
                            cov2.G30 = txtG30.Text;
                            cov2.G31 = txtG31.Text;
                            cov2.G32 = txtG32.Text;
                            cov2.G33 = txtG33.Text;
                            cov2.G34 = txtG34.Text;

                            cov2.E40 = txtE40.Text;
                            cov2.E41 = txtE41.Text;
                            cov2.E42 = txtE42.Text;
                            cov2.E43 = txtE43.Text;
                            cov2.E44 = txtE44.Text;
                            cov2.E45 = txtE45.Text;
                            cov2.E46 = txtE46.Text;
                            cov2.E47 = txtE47.Text;
                            cov2.E48 = txtE48.Text;
                            cov2.E49 = txtE49.Text;
                            cov2.E50 = txtE50.Text;
                            cov2.E51 = txtE51.Text;
                            cov2.E52 = txtE52.Text;
                            cov2.E53 = txtE53.Text;
                            cov2.E54 = txtE54.Text;
                        
                        List<template_seagate_gcms_coverpage_2> cov2s = new List<template_seagate_gcms_coverpage_2>();
                        cov2s.Add(cov2);
                        template_seagate_gcms_coverpage_2.InsertList(cov2s);



                        //template_seagate_gcms_coverpage.UpdateList(this.coverpages);
                        this.WsHashValue = new List<ws_hash_value>();
                        ws_hash_value ws = new ws_hash_value { id = 1, sample_id = this.SampleID, key = "txtFloatResult01", val = txtFloatResult01.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 2, sample_id = this.SampleID, key = "txtFloatResult02", val = txtFloatResult02.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 3, sample_id = this.SampleID, key = "txtFloatResult03", val = txtFloatResult03.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 4, sample_id = this.SampleID, key = "txtFloatResult04", val = txtFloatResult04.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 5, sample_id = this.SampleID, key = "txtFloatResult05", val = txtFloatResult05.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 6, sample_id = this.SampleID, key = "txtFloatResult06", val = txtFloatResult06.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 7, sample_id = this.SampleID, key = "txtFloatResult07", val = txtFloatResult07.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 8, sample_id = this.SampleID, key = "txtFloatResult08", val = txtFloatResult08.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 9, sample_id = this.SampleID, key = "txtFloatResult09", val = txtFloatResult09.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 10, sample_id = this.SampleID, key = "txtFloatResult10", val = txtFloatResult10.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 11, sample_id = this.SampleID, key = "txtFloatResult11", val = txtFloatResult11.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 12, sample_id = this.SampleID, key = "txtFloatResult12", val = txtFloatResult12.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 13, sample_id = this.SampleID, key = "txtFloatResult13", val = txtFloatResult13.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 14, sample_id = this.SampleID, key = "txtDecimal01", val = txtDecimal01.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 15, sample_id = this.SampleID, key = "txtDecimal02", val = txtDecimal02.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 16, sample_id = this.SampleID, key = "txtDecimal06", val = txtDecimal06.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 17, sample_id = this.SampleID, key = "txtDecimal07", val = txtDecimal07.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 18, sample_id = this.SampleID, key = "txtDecimal08", val = txtDecimal08.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 19, sample_id = this.SampleID, key = "txtDecimal09", val = txtDecimal09.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 20, sample_id = this.SampleID, key = "txtDecimal10", val = txtDecimal10.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 21, sample_id = this.SampleID, key = "txtDecimal11", val = txtDecimal11.Text };
                        this.WsHashValue.Add(ws);
                        ws = new ws_hash_value { id = 22, sample_id = this.SampleID, key = "txtDecimal12", val = txtDecimal12.Text };
                        this.WsHashValue.Add(ws);

                        MaintenanceBiz.ExecuteReturnDt(string.Format("delete from ws_hash_value where sample_id={0}", this.SampleID));

                        //ws_hash_value.DeleteBySampleID(this.SampleID);
                        //sbLog.Append("ws_hash_value time(del):" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");
                        ws_hash_value.InsertList(this.WsHashValue);
                        //sbLog.Append("ws_hash_value time:" + DateTime.Now.Subtract(dtStart).TotalMinutes + "\r\n");

                        Console.WriteLine();
                        #endregion
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
                            this.jobSample.date_admin_word_inprogress = DateTime.Now;
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
                            this.jobSample.date_admin_pdf_inprogress = DateTime.Now;
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
                    if (FileUpload1.HasFile)
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
                        FileUpload1.SaveAs(source_file);
                        this.jobSample.path_word = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        this.jobSample.date_admin_word_complete = DateTime.Now;
                        this.jobSample.date_labman_analyze = DateTime.Now;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:

                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".pdf")))
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
                        //lbMessage.Text = string.Empty;
                        this.jobSample.date_admin_pdf_complete = DateTime.Now;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    //this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
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
                this.jobSample.update_date = DateTime.Now;
                this.jobSample.update_by = userLogin.id;
                this.jobSample.Update();

                //Commit
                GeneralManager.Commit();

                removeSession();
                //Console.WriteLine(sbLog.ToString());
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            String sheetName = string.Empty;

            List<tb_m_gcms_cas> _cas = new List<tb_m_gcms_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < btnUpload.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = btnUpload.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName)));

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
                                #region "RHC"
                                ISheet isheet = wb.GetSheet("RHC");
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;
                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_BASE);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "RHC-BASE"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;
                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_BASE);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "RHC-HUB"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhchub"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhchub"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_HUB);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "Workingpg - Extractable"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_extractable"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_extractable"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    #region "Results: Surface Extraction Method (MBA, Baseplate)"
                                    txtB13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B));
                                    txtB14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));
                                    txtB15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                    txtB16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.B));
                                    txtB17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B));
                                    txtB18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B));
                                    txtB19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.B));

                                    txtC13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.C));
                                    txtC14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.C));
                                    txtC15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.C));
                                    txtC16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.C));
                                    txtC17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C));
                                    txtC18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C));
                                    txtC19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.C));


                                    txtD13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.D));
                                    txtD14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.D));
                                    txtD15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.D));
                                    txtD16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.D));
                                    txtD17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D));
                                    txtD18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D));
                                    txtD19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.D));

                                    txtE13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.E));
                                    txtE14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.E));
                                    txtE15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.E));
                                    txtE16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.E));
                                    txtE17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.E));
                                    txtE18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.E));
                                    txtE19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.E));

                                    #endregion
                                    #region "Motor Base / Baseplate"
                                    txtB20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B));
                                    txtB21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));
                                    txtB22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));
                                    txtB23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));
                                    txtB24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.B));
                                    txtB25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.B));
                                    txtB26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.B));
                                    txtB27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                    txtB28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));
                                    txtB29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));
                                    txtB30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.B));
                                    txtB31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.B));
                                    txtB32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.B));
                                    txtB33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.B));
                                    txtB34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.B));




                                    txtC20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C));
                                    txtC21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));
                                    txtC22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));
                                    txtC23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));
                                    txtC24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C));
                                    txtC25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.C));
                                    txtC26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.C));
                                    txtC27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.C));
                                    txtC28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.C));
                                    txtC29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.C));
                                    txtC30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.C));
                                    txtC31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.C));
                                    txtC32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.C));
                                    txtC33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.C));
                                    txtC34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.C));

                                    txtD20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.D));
                                    txtD21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.D));
                                    txtD22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.D));
                                    txtD23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.D));
                                    txtD24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.D));
                                    txtD25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.D));
                                    txtD26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.D));
                                    txtD27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.D));
                                    txtD28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.D));
                                    txtD29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.D));
                                    txtD30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.D));
                                    txtD31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.D));
                                    txtD32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.D));
                                    txtD33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.D));
                                    txtD34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.D));

                                    txtE20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.E));
                                    txtE21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.E));
                                    txtE22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.E));
                                    txtE23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.E));
                                    txtE24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.E));
                                    txtE25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.E));
                                    txtE26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.E));
                                    txtE27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.E));
                                    txtE28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.E));
                                    txtE29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.E));
                                    txtE30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.E));
                                    txtE31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.E));
                                    txtE32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.E));
                                    txtE33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.E));
                                    txtE34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.E));

                                    txtF20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.F));
                                    txtF21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.F));
                                    txtF22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.F));
                                    txtF23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.F));
                                    txtF24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.F));
                                    txtF25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.F));
                                    txtF26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.F));
                                    txtF27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.F));
                                    txtF28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.F));
                                    txtF29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.F));
                                    txtF30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.F));
                                    txtF31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.F));
                                    txtF32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.F));
                                    txtF33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.F));
                                    txtF34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.F));


                                    //
                                    txtD13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.D));
                                    txtD14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.D));
                                    txtD15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.D));
                                    txtD16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.D));
                                    txtD17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D));
                                    txtD18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D));
                                    txtD19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.D));
                                    //
                                    txtE13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.E));
                                    txtE14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.E));
                                    txtE15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.E));
                                    txtE16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.E));
                                    txtE17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.E));
                                    txtE18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.E));
                                    txtE19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.E));
                                    //
                                    txtG20.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.E));
                                    txtG21.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.E));
                                    txtG22.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.E));
                                    txtG23.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.E));
                                    txtG24.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.E));
                                    txtG25.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.E));
                                    txtG26.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.E));
                                    txtG27.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.E));
                                    txtG28.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.E));
                                    txtG29.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.E));
                                    txtG30.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.E));
                                    txtG31.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.E));
                                    txtG32.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.E));
                                    txtG33.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.E));
                                    txtG34.Text = CustomUtils.GetCellValue(isheet.GetRow(37 - 1).GetCell(ExcelColumn.E));

                                    txtE40.Text = CustomUtils.GetCellValue(isheet.GetRow(42 - 1).GetCell(ExcelColumn.E));
                                    txtE41.Text = CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.E));
                                    txtE42.Text = CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.E));
                                    txtE43.Text = CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.E));
                                    txtE44.Text = CustomUtils.GetCellValue(isheet.GetRow(46 - 1).GetCell(ExcelColumn.E));
                                    txtE45.Text = CustomUtils.GetCellValue(isheet.GetRow(47 - 1).GetCell(ExcelColumn.E));
                                    txtE46.Text = CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.E));
                                    txtE47.Text = CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.E));
                                    txtE48.Text = CustomUtils.GetCellValue(isheet.GetRow(50 - 1).GetCell(ExcelColumn.E));
                                    txtE49.Text = CustomUtils.GetCellValue(isheet.GetRow(51 - 1).GetCell(ExcelColumn.E));
                                    txtE50.Text = CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.E));
                                    txtE51.Text = CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.E));
                                    txtE52.Text = CustomUtils.GetCellValue(isheet.GetRow(54 - 1).GetCell(ExcelColumn.E));
                                    txtE53.Text = CustomUtils.GetCellValue(isheet.GetRow(55 - 1).GetCell(ExcelColumn.E));
                                    txtE54.Text = CustomUtils.GetCellValue(isheet.GetRow(56 - 1).GetCell(ExcelColumn.E));

                                    #endregion
                                    #region "Motor Hub"
                                    txtB40.Text = isheet.GetRow(42 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(42 - 1).GetCell(ExcelColumn.B));
                                    txtB41.Text = isheet.GetRow(43 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.B));
                                    txtB42.Text = isheet.GetRow(44 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.B));
                                    txtB43.Text = isheet.GetRow(45 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.B));
                                    txtB44.Text = isheet.GetRow(46 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(46 - 1).GetCell(ExcelColumn.B));
                                    txtB45.Text = isheet.GetRow(47 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(47 - 1).GetCell(ExcelColumn.B));
                                    txtB46.Text = isheet.GetRow(48 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.B));
                                    txtB47.Text = isheet.GetRow(49 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.B));
                                    txtB48.Text = isheet.GetRow(50 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(50 - 1).GetCell(ExcelColumn.B));
                                    txtB49.Text = isheet.GetRow(51 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(51 - 1).GetCell(ExcelColumn.B));
                                    txtB50.Text = isheet.GetRow(52 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.B));
                                    txtB51.Text = isheet.GetRow(53 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.B));
                                    txtB52.Text = isheet.GetRow(54 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(54 - 1).GetCell(ExcelColumn.B));
                                    txtB53.Text = isheet.GetRow(55 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(55 - 1).GetCell(ExcelColumn.B));
                                    txtB54.Text = isheet.GetRow(56 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(56 - 1).GetCell(ExcelColumn.B));

                                    txtC40.Text = isheet.GetRow(42 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(42 - 1).GetCell(ExcelColumn.C));
                                    txtC41.Text = isheet.GetRow(43 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.C));
                                    txtC42.Text = isheet.GetRow(44 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.C));
                                    txtC43.Text = isheet.GetRow(45 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.C));
                                    txtC44.Text = isheet.GetRow(46 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(46 - 1).GetCell(ExcelColumn.C));
                                    txtC45.Text = isheet.GetRow(47 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(47 - 1).GetCell(ExcelColumn.C));
                                    txtC46.Text = isheet.GetRow(48 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.C));
                                    txtC47.Text = isheet.GetRow(49 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.C));
                                    txtC48.Text = isheet.GetRow(50 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(50 - 1).GetCell(ExcelColumn.C));
                                    txtC49.Text = isheet.GetRow(51 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(51 - 1).GetCell(ExcelColumn.C));
                                    txtC50.Text = isheet.GetRow(52 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.C));
                                    txtC51.Text = isheet.GetRow(53 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.C));
                                    txtC52.Text = isheet.GetRow(54 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(54 - 1).GetCell(ExcelColumn.C));
                                    txtC53.Text = isheet.GetRow(55 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(55 - 1).GetCell(ExcelColumn.C));
                                    txtC54.Text = isheet.GetRow(56 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(56 - 1).GetCell(ExcelColumn.C));

                                    txtD40.Text = isheet.GetRow(42 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(42 - 1).GetCell(ExcelColumn.D));
                                    txtD41.Text = isheet.GetRow(43 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.D));
                                    txtD42.Text = isheet.GetRow(44 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.D));
                                    txtD43.Text = isheet.GetRow(45 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.D));
                                    txtD44.Text = isheet.GetRow(46 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(46 - 1).GetCell(ExcelColumn.D));
                                    txtD45.Text = isheet.GetRow(47 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(47 - 1).GetCell(ExcelColumn.D));
                                    txtD46.Text = isheet.GetRow(48 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.D));
                                    txtD47.Text = isheet.GetRow(49 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.D));
                                    txtD48.Text = isheet.GetRow(50 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(50 - 1).GetCell(ExcelColumn.D));
                                    txtD49.Text = isheet.GetRow(51 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(51 - 1).GetCell(ExcelColumn.D));
                                    txtD50.Text = isheet.GetRow(52 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.D));
                                    txtD51.Text = isheet.GetRow(53 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.D));
                                    txtD52.Text = isheet.GetRow(54 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(54 - 1).GetCell(ExcelColumn.D));
                                    txtD53.Text = isheet.GetRow(55 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(55 - 1).GetCell(ExcelColumn.D));
                                    txtD54.Text = isheet.GetRow(56 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(56 - 1).GetCell(ExcelColumn.D));

                                    #endregion
                                    //--------
                                }
                                #endregion
                                #region "Workingpg - Motor Oil "
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_motor_oil"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_motor_oil"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;


                                    String _txtMotorOilHub = isheet.GetRow(33 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.D));
                                    String _txtMotorOilBase25 = isheet.GetRow(34 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.D));
                                    String _txtMotorOilBase35 = isheet.GetRow(35 - 1) == null ? "" : CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.D));

                                    txtMotorOilHub.Text = CustomUtils.showOnCoverPageValue(_txtMotorOilHub, Convert.ToInt16(txtFloatResult01.Text));// String.IsNullOrEmpty(txtMotorOilHub)|| txtMotorOilHub.Equals("0.00") ? "" : txtMotorOilHub.Equals("Not Detected") ? "Not Detected" : "<MDL".Equals(txtMotorOilHub) ? txtMotorOilHub : Convert.ToDouble(txtMotorOilHub).ToString("N2");
                                    txtMotorOilBase25.Text = CustomUtils.showOnCoverPageValue(_txtMotorOilBase25, Convert.ToInt16(txtFloatResult01.Text));//String.IsNullOrEmpty(txtMotorOilBase25)|| txtMotorOilBase25.Equals("0.00") ? "" : txtMotorOilBase25.Equals("Not Detected") ? "Not Detected" : "<MDL".Equals(txtMotorOilBase25) ? txtMotorOilBase25 : Convert.ToDouble(txtMotorOilBase25).ToString("N2");
                                    txtMotorOilBase35.Text = CustomUtils.showOnCoverPageValue(_txtMotorOilBase35, Convert.ToInt16(txtFloatResult01.Text));//String.IsNullOrEmpty(txtMotorOilBase35) || txtMotorOilBase35.Equals("0.00") ? "":"";// : txtMotorOilBase35.Equals("Not Detected") ? "Not Detected" : "<MDL".Equals(txtMotorOilBase35) ? txtMotorOilBase35 : Convert.ToDouble(txtMotorOilBase35).ToString("N2");

                                    List<template_seagate_gcms_coverpage> motorOilsUpdate = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-")).ToList();
                                    if (motorOilsUpdate.Count > 0)
                                    {
                                        if (!String.IsNullOrEmpty(_txtMotorOilHub))
                                        {
                                            motorOilsUpdate[0].C = _txtMotorOilHub.Replace("<", "&lt;");
                                        }
        
                                        if(motorOilsUpdate.Count >= 2)
                                        {
                                            if (!String.IsNullOrEmpty(_txtMotorOilBase25))
                                            {
                                                motorOilsUpdate[1].C = _txtMotorOilBase25.Replace("<", "&lt;");
                                            }
                                            if (!String.IsNullOrEmpty(_txtMotorOilBase35))
                                            {
                                                motorOilsUpdate[1].C = _txtMotorOilBase35.Replace("<", "&lt;");
                                            }
                                        }


                                    }
                                }
                                #endregion
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                        }
                        #endregion

                        #region "IMG"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".png")) || (Path.GetExtension(_postedFile.FileName).Equals(".jpg")))
                        {
                            template_img _img = new template_img();
                            _img.id = CustomUtils.GetRandomNumberID();
                            _img.sample_id = this.SampleID;
                            _img.img_path = source_file_url;
                            this.refImg.Add(_img);

                            gvRefImages.DataSource = this.refImg;
                            gvRefImages.DataBind();
                        }
                        #endregion

                    }
                }
                catch (Exception )
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
                this.tbCas = _cas;
                gvRHCBase.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_BASE) && !x.library_id.Equals("0")).ToList();
                gvRHCBase.DataBind();
                gvRHCHub.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_HUB) && !x.library_id.Equals("0")).ToList();
                gvRHCHub.DataBind();

                //Note: This report was performed test by ALS Singapore.
                if (!string.IsNullOrEmpty(txtD33.Text) && String.IsNullOrEmpty(lbRemark1.Text))
                {
                    lbRemark1.Text = String.Format("1.) Minimum RHC Detection Limit is {0} {1}", Convert.ToDecimal(txtD33.Text).ToString("N3"), txtD34.Text);
                }

                if (!string.IsNullOrEmpty(txtD53.Text) && String.IsNullOrEmpty(lbRemark2.Text))
                {
                    lbRemark2.Text = String.Format("2.) Minimum RHC Detection Limit of Base is {0} {1}", Math.Round(Convert.ToDecimal(txtD53.Text), 3), txtD54.Text);
                }

                //if (!string.IsNullOrEmpty(txtD51.Text) && String.IsNullOrEmpty(lbRemark3.Text))
                //{
                //    if (CustomUtils.isNumber(txtD51.Text))
                //    {
                //        lbRemark3.Text = String.Format("3.) Minimum RHC Detection Limit of Base is {0} {1}", Math.Round(Convert.ToDecimal(txtD51.Text), 3), txtD52.Text);
                //    }
                //}
                //if (!string.IsNullOrEmpty(txtC51.Text) && String.IsNullOrEmpty(lbRemark4.Text))
                //{
                //    lbRemark4.Text = String.Format("4.) Minimum RHC Detection Limit is Hub {0} {1}", Math.Round(Convert.ToDecimal(txtC51.Text), 3), txtC52.Text);
                //}
                //if (!string.IsNullOrEmpty(txtC51.Text) && String.IsNullOrEmpty(lbRemark5.Text))
                //{
                //    lbRemark5.Text = String.Format("5.) Minimum RHC Detection Limit of Hub is {0} {1}", Math.Round(Convert.ToDecimal(txtC51.Text), 3), txtC52.Text);
                //}

                gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", txtB34.Text);
                gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", txtB34.Text);
                gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", txtB34.Text);
                gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", txtB34.Text);
                gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBase.SelectedItem.Text);
                gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHub.SelectedItem.Text);
                gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);


                GenerrateCoverPage();
            }
        }


        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnCoverPage":
                    btnCoverPage.CssClass = "btn blue";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = true;
                    pRH.Visible = false;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = false;

                    GenerrateCoverPage();
                    break;
                case "btnRH":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn blue";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pRH.Visible = true;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = false;
                    pLoadFile.Visible = true;

                    break;
                case "btnExtractable":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn blue";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pRH.Visible = false;
                    pExtractable.Visible = true;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = true;

                    break;
                case "btnMotorOil":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn blue";

                    pCoverpage.Visible = false;
                    pRH.Visible = false;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = true;
                    pLoadFile.Visible = true;

                    break;

            }
        }

        #endregion

        #region "COVERPAGES GRID."

        protected void gvMotorOil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorOil.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorOil.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMotorOil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorOil.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL)).ToList();
                    gvMotorOil.DataBind();
                }
            }
        }

        protected void gvMotorHub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorHub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorHub.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMotorHub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorHub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB)).ToList();
                    gvMotorHub.DataBind();
                }
            }
        }

        protected void gvMotorHubSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorHubSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorHubSub.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMotorHubSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorHubSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB)).ToList();
                    gvMotorHubSub.DataBind();
                }
            }
        }

        protected void gvMotorBase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorBase.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorBase.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMotorBase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorBase.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE)).ToList();
                    gvMotorBase.DataBind();
                }
            }
        }

        protected void gvMotorBaseSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorBaseSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorBaseSub.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMotorBaseSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorBaseSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB)).ToList();
                    gvMotorBaseSub.DataBind();
                }
            }
        }


        protected void gvCompound_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCompound.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCompound.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvCompound_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvCompound.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND)).ToList();
                    gvCompound.DataBind();
                }
            }
        }


        protected void gvCompoundSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCompoundSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCompoundSub.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvCompoundSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }

                    gvCompoundSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-")).ToList();
                    gvCompoundSub.DataBind();
                }
            }
        }
        #endregion

        #region "DHS GRID."
        protected void gvRHCBase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvRHCBase.DataKeys[e.Row.RowIndex].Values[0].ToString());

                tb_m_gcms_cas _cas = this.tbCas.Find(x => x.ID == _id);
                if (_cas != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)_cas.row_type);
                    switch (cmd)
                    {
                        case RowTypeEnum.Normal:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                        case RowTypeEnum.TotalRow:
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Font.Bold = true;
                            break;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }
        protected void gvRHCHub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvRHCHub.DataKeys[e.Row.RowIndex].Values[0].ToString());

                tb_m_gcms_cas _cas = this.tbCas.Find(x => x.ID == _id);
                if (_cas != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)_cas.row_type);
                    switch (cmd)
                    {
                        case RowTypeEnum.Normal:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                        case RowTypeEnum.TotalRow:
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Font.Bold = true;
                            break;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        #endregion

        #region "Custom method"

        private void GenerrateCoverPage()
        {
            //set float


            txtB25.Text = String.IsNullOrEmpty(txtB25.Text) ? "" : Convert.ToDouble(txtB25.Text).ToString("N3");
            txtC25.Text = String.IsNullOrEmpty(txtC25.Text) ? "" : Convert.ToDouble(txtC25.Text).ToString("N3");
            txtD25.Text = String.IsNullOrEmpty(txtD25.Text) ? "" : Convert.ToDouble(txtD25.Text).ToString("N3");
            txtB30.Text = String.IsNullOrEmpty(txtB30.Text) ? "" : Convert.ToDouble(txtB30.Text).ToString("N3");
            txtC30.Text = String.IsNullOrEmpty(txtC30.Text) ? "" : Convert.ToDouble(txtC30.Text).ToString("N3");
            txtD30.Text = String.IsNullOrEmpty(txtD30.Text) ? "" : Convert.ToDouble(txtD30.Text).ToString("N3");

            txtE30.Text = String.IsNullOrEmpty(txtE30.Text) ? "" : Convert.ToDouble(txtE30.Text).ToString("N3");
            txtF30.Text = String.IsNullOrEmpty(txtF30.Text) ? "" : Convert.ToDouble(txtF30.Text).ToString("N3");

            txtB31.Text = String.IsNullOrEmpty(txtB31.Text) ? "" : Convert.ToDouble(txtB31.Text).ToString("N3");
            txtC31.Text = String.IsNullOrEmpty(txtC31.Text) ? "" : Convert.ToDouble(txtC31.Text).ToString("N3");
            txtD31.Text = String.IsNullOrEmpty(txtD31.Text) ? "" : Convert.ToDouble(txtD31.Text).ToString("N3");




            #region "Binding"

            #region "MOTOR OILS"
            List<template_seagate_gcms_coverpage> motorOils = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-")).ToList();
            if (motorOils.Count > 0)
            {
                foreach (var item in motorOils)
                {

                    if (CustomUtils.isNumber(item.C))
                    {
                        item.C = Convert.ToDouble(item.C).ToString("N" + txtFloatResult01.Text);
                    }
                    else
                    {
                        item.C = String.IsNullOrEmpty(item.C) ? "Not Detected" : item.C;

                    }
                }
                gvMotorOil.DataSource = motorOils;
                gvMotorOil.DataBind();
                gvMotorOil.Visible = true;
            }
            else
            {
                gvMotorOil.Visible = false;
            }
            #endregion

            #region "MOTOR HUBS"
            List<template_seagate_gcms_coverpage> motorHubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB) && !x.A.Equals("-")).ToList();
            if (motorHubs.Count > 0)
            {
                if (!String.IsNullOrEmpty(txtD52.Text) && !String.IsNullOrEmpty(txtD52.Text)&&!String.IsNullOrEmpty(txtE52.Text) && !String.IsNullOrEmpty(txtE52.Text))
                {
                    motorHubs[0].C = (Convert.ToDecimal(txtD52.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtD52.Text), Convert.ToInt16(txtFloatResult02.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                    motorHubs[1].C = (Convert.ToDecimal(txtE52.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtE52.Text), Convert.ToInt16(txtFloatResult02.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)

                }
                gvMotorHub.DataSource = motorHubs;
                gvMotorHub.DataBind();
                gvMotorHub.Visible = true;
            }
            else
            {
                gvMotorHub.Visible = false;
            }

            List<template_seagate_gcms_coverpage> motorHubSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB) && !x.A.Equals("-")).ToList();
            if (motorHubSubs.Count > 0)
            {
                motorHubSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtB52.Text) ? "0" : txtB52.Text), Convert.ToInt16(txtFloatResult03.Text)) + "";//Compounds with RT ≤ DOP
                motorHubSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtC52.Text) ? "0" : txtC52.Text), Convert.ToInt16(txtFloatResult04.Text)) + "";//Compounds with RT > DOP
                motorHubSubs[0].C = Math.Round(Convert.ToDecimal((Convert.ToDecimal(motorHubSubs[1].C) + Convert.ToDecimal(motorHubSubs[2].C)) + ""), Convert.ToInt16(txtFloatResult05.Text)) + "";//Total Organic Compound (TOC)

                motorHubSubs[1].C = (Convert.ToDouble(motorHubSubs[1].C) == 0) ? "Not Detected" : motorHubSubs[1].C;
                motorHubSubs[2].C = (Convert.ToDouble(motorHubSubs[2].C) == 0) ? "Not Detected" : motorHubSubs[2].C;
                motorHubSubs[0].C = (Convert.ToDouble(motorHubSubs[0].C) == 0) ? "Not Detected" : motorHubSubs[0].C;

                gvMotorHubSub.DataSource = motorHubSubs;
                gvMotorHubSub.DataBind();


                gvMotorHubSub.Visible = true;
            }
            else
            {
                gvMotorHubSub.Visible = false;
            }
            #endregion

            #region "MOTOR BASES"
            List<template_seagate_gcms_coverpage> motorBases = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE) && !x.A.Equals("-")).ToList();
            if (motorBases.Count > 0)
            {
                if (!String.IsNullOrEmpty(txtD32.Text))
                {
                    motorBases[0].C = (Convert.ToDecimal(txtD32.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtD32.Text), Convert.ToInt16(txtFloatResult06.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                    motorBases[1].C = (Convert.ToDecimal(txtE32.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtE32.Text), Convert.ToInt16(txtFloatResult06.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)

                }
                gvMotorBase.DataSource = motorBases;
                gvMotorBase.DataBind();
                gvMotorBase.Visible = true;
            }
            else
            {
                gvMotorBase.Visible = false;
            }


            List<template_seagate_gcms_coverpage> motorBaseSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB) && !x.A.Equals("-")).ToList();
            if (motorBaseSubs.Count > 0)
            {
                motorBaseSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtB32.Text) ? "0" : txtB32.Text), Convert.ToInt16(txtFloatResult07.Text)) + "";//Compounds with RT ≤ DOP
                motorBaseSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtC32.Text) ? "0" : txtC32.Text), Convert.ToInt16(txtFloatResult08.Text)) + "";//Compounds with RT > DOP
                motorBaseSubs[0].C = Math.Round(Convert.ToDecimal((Convert.ToDecimal(motorBaseSubs[1].C) + Convert.ToDecimal(motorBaseSubs[2].C)) + ""), Convert.ToInt16(txtFloatResult09.Text)) + "";//Total Organic Compound (TOC)

                motorBaseSubs[1].C = (Convert.ToDouble(motorBaseSubs[1].C) == 0) ? "Not Detected" : motorBaseSubs[1].C;
                motorBaseSubs[2].C = (Convert.ToDouble(motorBaseSubs[2].C) == 0) ? "Not Detected" : motorBaseSubs[2].C;
                motorBaseSubs[0].C = (Convert.ToDouble(motorBaseSubs[0].C) == 0) ? "Not Detected" : motorBaseSubs[0].C;

                gvMotorBaseSub.DataSource = motorBaseSubs;
                gvMotorBaseSub.DataBind();
                gvMotorBaseSub.Visible = true;
            }
            else
            {
                gvMotorBaseSub.Visible = false;
            }
            #endregion

            #region "COMPOUNDS"
            List<template_seagate_gcms_coverpage> compounds = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND) && !x.A.Equals("-")).ToList();
            if (compounds.Count > 0)
            {
                if (!String.IsNullOrEmpty(txtD32.Text))
                {
                    compounds[0].C = (Convert.ToDecimal(txtD32.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtD32.Text), Convert.ToInt32(txtFloatResult10.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                    compounds[1].C = (Convert.ToDecimal(txtE32.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(txtE32.Text), Convert.ToInt16(txtFloatResult06.Text)) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)

                }
                gvCompound.DataSource = compounds;
                gvCompound.DataBind();

                gvCompound.Visible = true;
            }
            else
            {
                gvCompound.Visible = false;
            }

            List<template_seagate_gcms_coverpage> compoundSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-")).ToList();
            if (compoundSubs.Count > 0&& compoundSubs.Count >=3)
            {
                compoundSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtB32.Text) ? "0" : txtB32.Text), Convert.ToInt32(txtFloatResult11.Text)) + "";//Compounds with RT > DOP
                compoundSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtC32.Text) ? "0" : txtC32.Text), Convert.ToInt32(txtFloatResult12.Text)) + "";//Compounds with RT > DOP
                compoundSubs[0].C = (Convert.ToDecimal(compoundSubs[1].C) + Convert.ToDecimal(compoundSubs[2].C)) + "";
                if (compoundSubs.Count > 3)
                {
                    compoundSubs[3].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(txtF32.Text) ? "0" : txtF32.Text), Convert.ToInt32(txtFloatResult13.Text)) + "";//Compounds with RT > DOP
                }




                compoundSubs[1].C = (Convert.ToDouble(compoundSubs[1].C) == 0) ? "Not Detected" : compoundSubs[1].C;
                compoundSubs[2].C = (Convert.ToDouble(compoundSubs[2].C) == 0) ? "Not Detected" : compoundSubs[2].C;
                if (compoundSubs.Count > 3)
                {
                    compoundSubs[3].C = (Convert.ToDouble(compoundSubs[3].C) == 0) ? "Not Detected" : compoundSubs[3].C;
                }
                compoundSubs[0].C = (Convert.ToDouble(compoundSubs[0].C) == 0) ? "Not Detected" : compoundSubs[0].C;

                gvCompoundSub.DataSource = compoundSubs;
                gvCompoundSub.DataBind();
                gvCompoundSub.Visible = true;
            }
            else
            {
                gvCompoundSub.Visible = false;
            }
            #endregion
            #endregion
        }

        #endregion

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {
                if (!String.IsNullOrEmpty(component.E))
                {
                    tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref();
                    detailSpecRef.spec_ref = Convert.ToInt32(component.E);
                    detailSpecRef.template_id = this.jobSample.template_id;


                    List<tb_m_detail_spec_ref> detailSpecRefs = (List<tb_m_detail_spec_ref>)detailSpecRef.SelectAll();

                    List<template_seagate_gcms_coverpage> newCoverPage = new List<template_seagate_gcms_coverpage>();
                    int index = 0;
                    foreach (tb_m_detail_spec_ref spec in detailSpecRefs)
                    {
                        template_seagate_gcms_coverpage work = new template_seagate_gcms_coverpage();
                        work.ID = spec.ID;// (this.CommandName == CommandNameEnum.Add) ? index : this.coverpages[index].ID;
                        work.sample_id = this.SampleID;
                        work.component_id = component.ID;
                        work.A = spec.B;
                        work.B = spec.C;
                        work.C = string.Empty;
                        work.RowState = this.CommandName;
                        work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                        newCoverPage.Add(work);
                        index++;
                    }


                    /*
                0|Motor Oil - Hub
                1|Motor Oil - Base
                -----------------------------------------------
                2|Repeated Hydrocarbon (C20-C40 Alkanes)
                3|n-Alkanes
                4|Total Organic Compound (TOC)
                5|Compounds with RT ≤ DOP
                6|Compounds with RT > DOP
                -----------------------------------------------
                7|Repeated Hydrocarbon (C20-C40 Alkanes)
                8|n-Alkanes
                9|Total Organic Compound (TOC)
                10|Compounds with RT ≤ DOP
                11|Compounds with RT > DOP
                12|Total Organic Compound (TOC)
                -----------------------------------------------
                13|Compounds with RT ≤ DOP
                14|Compounds with RT > DOP
                15|Compound with m/z 155, 271, 425
                16|Compound with m/z 283, 311& Compound with m/z 311
                17|Compound with m/z 138
                    */

                    int[] fixLayout = { 1, 3, 5, 7, 9, 12, 23, 26, 28, 31, 33, 35, 41 };//FILE: Seagate-GCMS_component (FM510.01.09) BN.xlt
                    int[] fixLayoutSpindle = { 14, 16, 18, 20, 37, 39 };//FILE: Seagate-GCMS_component (FM510.01.09) BN.xlt
                    int[] fixLayoutComponent2 = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 26, 28, 30, 32, 35, 37, 39, 41, 43, 45, 48, 50, 52, 54, 56, 58, 60, 62, 65, 67, 69, 71, 73, 75, 77, 80, 82, 84, 86 };
                    if (fixLayout.Contains(Convert.ToInt16(component.E)))
                    {
                        newCoverPage[0].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        newCoverPage[1].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        //-----------------------------------------------
                        newCoverPage[2].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[3].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[4].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[5].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[6].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        //-----------------------------------------------
                        newCoverPage[7].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[8].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[9].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        newCoverPage[10].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        newCoverPage[11].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        //-----------------------------------------------
                        newCoverPage[12].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[13].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[14].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[15].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[16].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[17].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[18].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[19].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[20].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[22].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[23].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);

                    }
                    else if (fixLayoutSpindle.Contains(Convert.ToInt16(component.E)))
                    {

                        newCoverPage[0].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        newCoverPage[1].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        //-----------------------------------------------
                        newCoverPage[2].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[3].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[4].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[5].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[6].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        //-----------------------------------------------
                        newCoverPage[7].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[8].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[9].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        newCoverPage[10].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        newCoverPage[11].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        //-----------------------------------------------
                        newCoverPage[12].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[13].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[14].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[15].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[16].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[17].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[18].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[19].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[20].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[22].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[23].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    }
                    else if (fixLayoutComponent2.Contains(Convert.ToInt16(component.E)))
                    {
                        newCoverPage[0].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        newCoverPage[1].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        //-----------------------------------------------
                        newCoverPage[2].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[3].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[4].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[5].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[6].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        //-----------------------------------------------
                        newCoverPage[7].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[8].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[9].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        //-----------------------------------------------
                        newCoverPage[10].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        newCoverPage[11].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);

                        newCoverPage[12].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[13].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[14].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[15].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[16].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[17].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[18].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[19].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[20].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[22].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[23].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    }
                    else
                    {

                        newCoverPage[0].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        newCoverPage[1].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                        //-----------------------------------------------
                        newCoverPage[2].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[3].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                        newCoverPage[4].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[5].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        newCoverPage[6].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                        //-----------------------------------------------
                        newCoverPage[7].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[8].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                        newCoverPage[9].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                        //-----------------------------------------------
                        newCoverPage[10].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[11].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                        newCoverPage[12].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[13].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[14].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[15].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[16].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[17].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[18].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        newCoverPage[19].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[20].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[22].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                        //newCoverPage[23].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    }


                    Console.WriteLine();


                    this.coverpages = newCoverPage;

                    //#region "Binding"
                    gvMotorOil.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-"));
                    gvMotorOil.DataBind();
                    if (gvMotorOil.Rows.Count > 0)
                    {

                        gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                        gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                        gvMotorOil.Visible = true;
                    }
                    else
                    {
                        gvMotorOil.Visible = false;
                    }
                    gvMotorHub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB) && !x.A.Equals("-"));
                    gvMotorHub.DataBind();
                    if (gvMotorHub.Rows.Count > 0)
                    {
                        gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHub.SelectedItem.Text);
                        gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                        gvMotorHub.Visible = true;
                    }
                    else
                    {
                        gvMotorHub.Visible = false;
                    }
                    gvMotorHubSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB) && !x.A.Equals("-"));
                    gvMotorHubSub.DataBind();
                    if (gvMotorHubSub.Rows.Count > 0)
                    {
                        gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                        gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                        gvMotorHubSub.Visible = true;
                    }
                    else
                    {
                        gvMotorHubSub.Visible = false;
                    }
                    gvMotorBase.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE) && !x.A.Equals("-"));
                    gvMotorBase.DataBind();
                    if (gvMotorBase.Rows.Count > 0)
                    {
                        gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBase.SelectedItem.Text);
                        gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                        gvMotorBase.Visible = true;
                    }
                    else
                    {
                        gvMotorBase.Visible = false;
                    }
                    gvMotorBaseSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB) && !x.A.Equals("-"));
                    gvMotorBaseSub.DataBind();
                    if (gvMotorBaseSub.Rows.Count > 0)
                    {
                        gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                        gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                        gvMotorBaseSub.Visible = true;
                    }
                    else
                    {
                        gvMotorBaseSub.Visible = false;
                    }
                    gvCompound.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND) && !x.A.Equals("-"));
                    gvCompound.DataBind();
                    if (gvCompound.Rows.Count > 0)
                    {
                        gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompound.Visible = true;
                    }
                    else
                    {
                        gvCompound.Visible = false;
                    }
                    gvCompoundSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-"));
                    gvCompoundSub.DataBind();
                    if (gvCompoundSub.Rows.Count > 0)
                    {
                        gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompoundSub.SelectedItem.Text);
                        gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompoundSub.SelectedItem.Text);
                        gvCompoundSub.Visible = true;
                    }
                    else
                    {
                        gvCompoundSub.Visible = false;
                    }


                    //#endregion




                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);
                    txtProcedure.Text = component.F;
                    txtSampleSize.Text = component.G;
                    //lbExtractionMedium.Text;
                    txtExtractionVolumn.Text = component.H;


                    //gvMotorOil.DataSource = this.coverpages;
                    //gvMotorOil.DataBind();
                    //}
                }
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            try
            {

                int noteSeq = 0;
                noteSeq += (string.IsNullOrEmpty(lbRemark1.Text) ? 0 : 1);
                noteSeq += (string.IsNullOrEmpty(lbRemark2.Text) ? 0 : 1);
                noteSeq += (string.IsNullOrEmpty(lbRemark3.Text) ? 0 : 1);
                noteSeq += (string.IsNullOrEmpty(lbRemark4.Text) ? 0 : 1);
                noteSeq += (string.IsNullOrEmpty(lbRemark5.Text) ? 0 : 1);


                DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);

                ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample, noteSeq);



                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("RemarkAmendRetest", reportHeader.remarkAmendRetestRaw));
                reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo + " "));
                reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
                reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
                reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
                reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));

                reportParameters.Add(new ReportParameter("rpt_unit1", ddlUnitMotorOilContamination.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit2", ddlUnitMotorHub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit3", ddlUnitMotorHubSub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit4", ddlUnitMotorBase.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit5", ddlUnitMotorBaseSub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit6", ddlUnitCompound.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit7", ddlUnitCompoundSub.SelectedItem.Text));



                reportParameters.Add(new ReportParameter("Test", "GCMS Extractable"));
                reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
                reportParameters.Add(new ReportParameter("Remark1", String.IsNullOrEmpty(lbRemark1.Text) ? " " : lbRemark1.Text));
                reportParameters.Add(new ReportParameter("Remark2", String.IsNullOrEmpty(lbRemark2.Text) ? " " : lbRemark2.Text));
                reportParameters.Add(new ReportParameter("Remark3", String.IsNullOrEmpty(lbRemark3.Text) ? " " : lbRemark3.Text));
                reportParameters.Add(new ReportParameter("Remark4", String.IsNullOrEmpty(lbRemark4.Text) ? " " : lbRemark4.Text));
                reportParameters.Add(new ReportParameter("Remark5", String.IsNullOrEmpty(lbRemark5.Text) ? " " : lbRemark5.Text));
                reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));
                reportParameters.Add(new ReportParameter("SupplementToReportNo", reportHeader.supplementToReportNo));
                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/gcms_seagate.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);

                foreach (var item in coverpages)
                {
                    item.C = item.C.Replace("&lt;", "<");
                }

                DataTable dt2 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt3 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt4 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt5 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt6 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt7 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND) && !x.A.Equals("-")).ToList().ToDataTable();
                DataTable dt8 = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-")).ToList().ToDataTable();
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt3));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dt4));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", dt5));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", dt6));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", dt7));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet8", dt8));

                if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0 && dt4.Rows.Count > 0 && dt5.Rows.Count > 0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet9", dt));
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet9", new DataTable()));

                }
                List<template_img> dat = this.refImg.OrderBy(x => x.seq).ToList();
                if (dat.Count >= 1)
                {
                    List<template_img> datImg1 = new List<template_img>();
                    datImg1.Add(new template_img { img1 = CustomUtils.GetBytesFromImage(dat[0].img_path) });
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet10", datImg1.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet10", new DataTable())); // Add datasource here
                }
                if (dat.Count >= 2)
                {
                    List<template_img> datImg2 = new List<template_img>();
                    datImg2.Add(new template_img { img1 = CustomUtils.GetBytesFromImage(dat[1].img_path) });
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet11", datImg2.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet11", new DataTable())); // Add datasource here
                }
                if (dat.Count >= 3)
                {
                    List<template_img> datImg3 = new List<template_img>();
                    datImg3.Add(new template_img { img1 = CustomUtils.GetBytesFromImage(dat[2].img_path) });
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet12", datImg3.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet12", new DataTable())); // Add datasource here
                }
                if (dat.Count >= 4)
                {
                    List<template_img> datImg4 = new List<template_img>();
                    datImg4.Add(new template_img { img1 = CustomUtils.GetBytesFromImage(dat[3].img_path) });
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet13", datImg4.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet13", new DataTable())); // Add datasource here
                }
                //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND)).ToList().ToDataTable()));



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

                            if (!Directory.Exists(Server.MapPath("~/Report/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Report/"));
                            }
                            using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                            }


                            #region "Insert Footer & Header from template"
                            Document doc1 = new Document();
                            doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
                            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                            Document doc2 = new Document(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension);
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



                            doc2.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);
                            #endregion
                            Response.ContentType = mimeType;
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                            Response.WriteFile(Server.MapPath("~/Report/" + this.jobSample.job_number + "." + extension));
                            Response.Flush();


                            #region "Delete After Download"
                            String deleteFile1 = Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension;
                            String deleteFile2 = Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension;

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
            catch (Exception )
            {
                Console.WriteLine();
            }

        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
            }
            else
            {
                tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
                if (component != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", component.B, component.A);
                }
            }

        }



        protected void ddlUnitMotorOilContamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
            gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorBaseSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
            gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBase.SelectedItem.Text);
            gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorHubSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHubSub.SelectedItem.Text);
            gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorHub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHub.SelectedItem.Text);
            gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitCompound_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompound.SelectedItem.Text);
            gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }

        protected void ddlUnitCompoundSub_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompoundSub.SelectedItem.Text);
            gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompoundSub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }


        #region "IMG"

        protected void gvRefImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_img _mesa = this.refImg.Find(x => x.id == PKID);
                if (_mesa != null)
                {
                    switch (cmd)
                    {
                        case CommandNameEnum.Delete:
                            this.refImg.Remove(_mesa);
                            break;

                    }
                    gvRefImages.DataSource = this.refImg;
                    gvRefImages.DataBind();
                }
            }
        }

        protected void gvRefImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvRefImages_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvRefImages.EditIndex = e.NewEditIndex;
            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();

        }

        protected void gvRefImages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRefImages.EditIndex = -1;
            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();
        }

        protected void gvRefImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField _hImgType = (HiddenField)e.Row.FindControl("hImgType");
                //Literal _litImgType = (Literal)e.Row.FindControl("litImgType");
                //if (!String.IsNullOrEmpty(_hImgType.Value) && _litImgType != null)
                //{
                //    IMAGE_ORDER_TYPE imgOrderType = (IMAGE_ORDER_TYPE)Enum.ToObject(typeof(IMAGE_ORDER_TYPE), Convert.ToInt32(_hImgType.Value));
                //    _litImgType.Text = imgOrderType.ToString();
                //}
            }
        }
        protected void gvRefImages_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvRefImages.DataKeys[e.RowIndex].Values[0].ToString());
            //DropDownList _ddlImgType = (DropDownList)gvRefImages.Rows[e.RowIndex].FindControl("ddlImgType");
            //if (_ddlImgType != null)
            //{
            //    template_img _tmp = this.refImg.Find(x => x.id == _id);
            //    if (_tmp != null)
            //    {
            //        _tmp.img_type = Convert.ToInt32(_ddlImgType.SelectedValue);
            //    }
            //}

            //gvRefImages.EditIndex = -1;
            //gvRefImages.DataSource = this.refImg;
            //gvRefImages.DataBind();
        }
        #endregion

    }
}



