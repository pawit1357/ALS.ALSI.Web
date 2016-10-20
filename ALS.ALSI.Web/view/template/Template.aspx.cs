using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
namespace ALS.ALSI.Web.view.template
{
    public partial class Template : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Template));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int PKID
        {
            get { return (int)Session[GetType().Name + "PKID"]; }
            set { Session[GetType().Name + "PKID"] = value; }
        }

        public m_template obj
        {
            get
            {
                m_template tmp = new m_template();
                //tmp.ID = PKID;
                tmp.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                tmp.name = txtName.Text;
                tmp.path_source_file = hPathSourceFile.Value;
                tmp.path_url = txtPathUrl.Text;
                tmp.description = txtDescription.Text;
                tmp.requestor = userLogin.id;
                tmp.modified_by = userLogin.id;
                tmp.verified_by = userLogin.id;
                tmp.validated_by = userLogin.id;
                tmp.modified_date = DateTime.Now;
                tmp.create_date = DateTime.Now;
                tmp.version = txtVersion.Text;
                tmp.status = "A";
                return tmp;
            }
        }

        private void initialPage()
        {
            lbCommandName.Text = CommandName.ToString();

            ddlSpecification.DataSource = new m_specification().SelectAll();
            ddlSpecification.DataBind();

            string APP_PATH = HttpRuntime.AppDomainAppPath;
            string[] array1 = Directory.GetFiles(String.Concat(APP_PATH,"view\\template"));
            List<m_template> spec = new List<m_template>();
            int index = 1;
            foreach (string s in array1)
            {
                m_template msp = new m_template();
                msp.ID = index;
                msp.path_url = String.Format("~/view/template/{0}", Path.GetFileName(s));
                index++;
                if (Path.GetExtension(s).Equals(".ascx"))
                {
                    spec.Add(msp);
                }
            }

            ddlPathUrl.DataSource = spec;
            ddlPathUrl.DataBind();

            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    txtName.ReadOnly = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();
                    txtName.ReadOnly = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case CommandNameEnum.View:
                    fillinScreen();
                    txtName.ReadOnly = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    break;
            }
        }

        private void fillinScreen()
        {
            m_template tmp = new m_template().SelectByID(this.PKID);
            if (tmp != null)
            {
                txtName.Text = tmp.name;
                ddlSpecification.SelectedValue = tmp.specification_id.ToString();
                txtDescription.Text = tmp.description;
                txtVersion.Text = tmp.version;
                txtPathUrl.Text = tmp.path_url;
                hPathSourceFile.Value = tmp.path_source_file;
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchTemplate prvPage = Page.PreviousPage as SearchTemplate;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_TEMPLATE;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            txtPathUrl.Text = ddlPathUrl.SelectedItem.Text;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".xls")  || Path.GetExtension(FileUpload1.FileName).Equals(".xlt")))
            {
                String _pathSourceFile = String.Format(Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                String _phisicalPath = String.Format(System.AppDomain.CurrentDomain.BaseDirectory + Configurations.PATH_TEMPLATE, String.Empty);
                String _savefilePath = String.Format(System.AppDomain.CurrentDomain.BaseDirectory + Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                if (!Directory.Exists(_phisicalPath))
                {
                    Directory.CreateDirectory(_phisicalPath);
                }
                hPathSourceFile.Value = _pathSourceFile;
                FileUpload1.SaveAs(_savefilePath);
                //::PROCESS UPLOAD
                switch (CommandName)
                {
                    case CommandNameEnum.Add:
                        this.PKID = obj.InsertGetLastID();
                        break;
                    case CommandNameEnum.Edit:
                        break;
                }
                processUpload(this.PKID, _savefilePath);
            }
            removeSession();
            MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }

        private void processUpload(int template_id, String filePath)
        {
            Boolean bUploadSuccess = false;
            try
            {
                List<int> listOfSpec = new List<int>();

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook wd = new HSSFWorkbook(fs);
                    #region "Component"
                    ISheet isComponent = wd.GetSheet("Component");
                    if (isComponent == null)
                    {
                        isComponent = wd.GetSheet("Components");
                    }
                    ISheet isDetailSpecRef = wd.GetSheet("Detail Spec");
                    if (isComponent != null)
                    {
                        List<tb_m_component> components = new List<tb_m_component>();
                        List<tb_m_detail_spec_ref> detailSpecRefs = new List<tb_m_detail_spec_ref>();

                        for (int row = 4; row <= isComponent.LastRowNum; row++)
                        {
                            if (isComponent.GetRow(row) != null) //null is when the row only contains empty cells 
                            {

                                tb_m_component component = new tb_m_component();
                                component.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                                component.template_id = template_id;
                                component.A = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(0));
                                component.B = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(1));
                                component.C = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(2));
                                component.D = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(3));
                                component.E = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(4));
                                component.F = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(5));
                                component.G = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(6));
                                component.H = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(7));
                                component.I = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(8));
                                component.J = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(9));
                                component.K = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(10));
                                component.L = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(11));
                                component.M = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(12));
                                component.N = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(13));
                                component.O = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(14));
                                component.P = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(15));
                                component.Q = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(16));
                                component.R = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(17));
                                component.S = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(18));
                                component.T = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(19));
                                component.U = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(20));
                                component.V = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(21));
                                component.W = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(22));
                                component.X = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(23));
                                component.Y = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(24));
                                component.Z = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(25));
                                component.RowState = CommandNameEnum.Add;

                                #region "Add Detail"
                                if (isDetailSpecRef != null && !String.IsNullOrEmpty(txtSpecRef.Text))
                                {
                                    String _spectRefId = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(Convert.ToInt32(txtSpecRef.Text)-1));
                                    if (!String.IsNullOrEmpty(_spectRefId))
                                    {

                                        int spectRefId = Convert.ToInt32(_spectRefId);
                                        int index = 1;

                                        for (int row1 = 2; row1 <= isDetailSpecRef.LastRowNum; row1++)
                                        {
                                            if (isDetailSpecRef.GetRow(row1) != null)
                                            {
                                                tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref();
                                                detailSpecRef.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                                                detailSpecRef.template_id = template_id;
                                                detailSpecRef.spec_ref = spectRefId;
                                                detailSpecRef.A = index;
                                                detailSpecRef.B = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId));
                                                detailSpecRef.C = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId + 1));
                                                detailSpecRef.D = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId + 2));

                                                detailSpecRef.RowState = CommandNameEnum.Add;

                                                if (!detailSpecRef.B.Equals(""))// && !detailSpecRef.B.Equals("-"))
                                                {
                                                    detailSpecRefs.Add(detailSpecRef);
                                                }
                                                index++;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                components.Add(component);
                            }
                        }
                        //Delete
                        //new tb_m_component().DeleteByTemplateID(template_id);
                        //new tb_m_detail_spec_ref().DeleteByTemplateID(template_id);
                        //
                        new tb_m_component().InsertList(components);
                        new tb_m_detail_spec_ref().InsertList(detailSpecRefs);
                    }
                    #endregion
                    #region "Detail Spec"
                    if (String.IsNullOrEmpty(txtSpecRef.Text))
                    {
                        ISheet isDetailSpec = wd.GetSheet("Detail Spec");
                        if (isDetailSpec != null)
                        {
                            List<tb_m_detail_spec> detailSpecs = new List<tb_m_detail_spec>();
                            for (int row = 0; row <= isDetailSpec.LastRowNum; row++)
                            {
                                if (isDetailSpec.GetRow(row) != null) //null is when the row only contains empty cells 
                                {
                                    tb_m_detail_spec detailSpec = new tb_m_detail_spec();
                                    detailSpec.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                                    detailSpec.template_id = template_id;
                                    detailSpec.A = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(0));
                                    detailSpec.B = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(1));
                                    detailSpec.C = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(2));
                                    detailSpec.D = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(3));
                                    detailSpec.E = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(4));
                                    detailSpec.F = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(5));
                                    detailSpec.G = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(6));
                                    detailSpec.H = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(7));
                                    detailSpec.I = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(8));
                                    detailSpec.J = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(9));
                                    detailSpec.K = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(10));
                                    detailSpec.L = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(11));
                                    detailSpec.M = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(12));
                                    detailSpec.N = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(13));
                                    detailSpec.O = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(14));
                                    detailSpec.P = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(15));
                                    detailSpec.Q = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(16));
                                    detailSpec.R = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(17));
                                    detailSpec.S = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(18));
                                    detailSpec.T = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(19));
                                    detailSpec.U = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(20));
                                    detailSpec.V = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(21));
                                    detailSpec.W = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(22));
                                    detailSpec.X = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(23));
                                    detailSpec.Y = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(24));
                                    detailSpec.Z = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(25));
                                    detailSpec.AA = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(26));
                                    detailSpec.AB = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(27));
                                    detailSpec.AC = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(28));
                                    detailSpec.AD = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(29));
                                    detailSpec.AE = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(30));
                                    detailSpec.AF = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(31));
                                    detailSpec.AG = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(32));
                                    detailSpec.AH = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(33));
                                    detailSpec.AI = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(34));
                                    detailSpec.AJ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(35));
                                    detailSpec.AK = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(36));
                                    detailSpec.AL = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(37));
                                    detailSpec.AM = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(38));
                                    detailSpec.AN = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(39));
                                    detailSpec.AO = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(40));
                                    detailSpec.AP = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(41));
                                    detailSpec.AQ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(42));
                                    detailSpec.AR = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(43));
                                    detailSpec.AS = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(44));
                                    detailSpec.AT = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(45));
                                    detailSpec.AU = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(46));
                                    detailSpec.AV = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(47));
                                    detailSpec.AW = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(48));
                                    detailSpec.AX = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(49));
                                    detailSpec.AY = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(50));
                                    detailSpec.AZ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(51));
                                    detailSpec.BA = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(52));
                                    detailSpec.BB = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(53));
                                    detailSpec.BC = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(54));
                                    detailSpec.BD = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(55));
                                    detailSpec.BE = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(56));
                                    detailSpec.BF = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(57));
                                    detailSpec.BG = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(58));
                                    detailSpec.BH = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(59));
                                    detailSpec.BI = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(60));
                                    detailSpec.BJ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(61));
                                    detailSpec.BK = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(62));
                                    detailSpec.BL = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(63));
                                    detailSpec.BM = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(64));
                                    detailSpec.BN = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(65));
                                    detailSpec.BO = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(66));
                                    detailSpec.BP = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(67));
                                    detailSpec.BQ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(68));
                                    detailSpec.BR = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(69));
                                    detailSpec.BS = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(70));
                                    detailSpec.BT = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(71));
                                    detailSpec.BU = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(72));
                                    detailSpec.BV = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(73));
                                    detailSpec.BW = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(74));
                                    detailSpec.BX = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(75));
                                    detailSpec.BY = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(76));
                                    detailSpec.BZ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(77));
                                    detailSpec.BA = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(78));
                                    detailSpec.BB = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(79));
                                    detailSpec.BC = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(80));
                                    detailSpec.BD = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(81));
                                    detailSpec.BE = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(82));
                                    detailSpec.BF = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(83));
                                    detailSpec.BG = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(84));
                                    detailSpec.BH = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(85));
                                    detailSpec.BI = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(86));
                                    detailSpec.BJ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(87));
                                    detailSpec.BK = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(88));
                                    detailSpec.BL = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(89));
                                    detailSpec.BM = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(90));
                                    detailSpec.BN = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(91));
                                    detailSpec.BO = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(92));
                                    detailSpec.BP = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(93));
                                    detailSpec.BQ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(94));
                                    detailSpec.BR = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(95));
                                    detailSpec.BS = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(96));
                                    detailSpec.BT = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(97));
                                    detailSpec.BU = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(98));
                                    detailSpec.BV = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(99));
                                    detailSpec.BW = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(100));
                                    detailSpec.BX = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(101));
                                    detailSpec.BY = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(102));
                                    detailSpec.BZ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(103));

                                    detailSpec.RowState = CommandNameEnum.Add;
                                    detailSpecs.Add(detailSpec);
                                }
                            }
                            //Delete
                            //new tb_m_detail_spec().DeleteByTemplateID(template_id);
                            //
                            new tb_m_detail_spec().InsertList(detailSpecs);
                        }
                    }
                    #endregion
                    #region "Specification"
                    ISheet isSpecification = wd.GetSheet("Specification");
                    if (isSpecification != null)
                    {
                        List<tb_m_specification> specifications = new List<tb_m_specification>();
                        for (int row = 0; row <= isSpecification.LastRowNum; row++)
                        {
                            if (isSpecification.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                tb_m_specification specification = new tb_m_specification();
                                specification.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                                specification.template_id = template_id;
                                specification.A = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(0));
                                specification.B = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(1));
                                specification.C = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(2));
                                specification.D = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(3));
                                specification.E = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(4));
                                specification.F = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(5));
                                specification.G = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(6));
                                specification.H = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(7));
                                specification.I = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(8));
                                specification.J = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(9));
                                specification.K = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(10));
                                specification.L = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(11));
                                specification.M = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(12));
                                specification.N = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(13));
                                specification.O = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(14));
                                specification.P = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(15));
                                specification.Q = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(16));
                                specification.R = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(17));
                                specification.S = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(18));
                                specification.T = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(19));
                                specification.U = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(20));
                                specification.V = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(21));
                                specification.W = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(22));
                                specification.X = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(23));
                                specification.Y = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(24));
                                specification.Z = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(25));

                                specification.AA = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(26));
                                specification.AB = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(27));
                                specification.AC = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(28));
                                specification.AD = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(29));
                                specification.AE = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(30));
                                specification.AF = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(31));
                                specification.AG = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(32));
                                specification.AH = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(33));
                                specification.AI = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(34));
                                specification.AJ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(35));

                                specification.AK = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(36));
                                specification.AL = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(37));
                                specification.AM = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(38));
                                specification.AN = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(39));
                                specification.AO = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(40));
                                specification.AP = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(41));
                                specification.AQ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(42));
                                specification.AR = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(43));
                                specification.AS = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(44));
                                specification.AT = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(45));
                                specification.AU = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(46));
                                specification.AV = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(47));
                                specification.AW = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(48));
                                specification.AX = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(49));
                                specification.AY = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(50));
                                specification.AZ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(51));


                                specification.RowState = CommandNameEnum.Add;
                                specifications.Add(specification);
                            }
                        }
                        //Delete
                        //new tb_m_specification().DeleteByTemplateID(template_id);
                        new tb_m_specification().InsertList(specifications);
                    }
                    #endregion
                }
                bUploadSuccess = true;

            }
            catch (Exception ex)
            {
                Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>"+ex.InnerException+"/span></div>";
                Console.WriteLine(ex.Message);
            }
            if (bUploadSuccess)
            {
                //Commit
                GeneralManager.Commit();
                Message = "<div class=\"alert alert-uccess\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Upload Success.</span></div>";
            }
            else
            {
                Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Upload Fail.</span></div>";
            }
        }

    }
}



