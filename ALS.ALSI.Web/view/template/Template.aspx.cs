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
        public users_login UserLogin
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

        public m_template Obj
        {
            get
            {
                m_template tmp = new m_template
                {
                    //tmp.ID = PKID;
                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                    name = txtName.Text,
                    path_source_file = hPathSourceFile.Value,
                    path_url = txtPathUrl.Text,
                    description = txtDescription.Text,
                    requestor = UserLogin.id,
                    modified_by = UserLogin.id,
                    verified_by = UserLogin.id,
                    validated_by = UserLogin.id,
                    modified_date = DateTime.Now,
                    create_date = DateTime.Now,
                    version = txtVersion.Text,
                    status = "A"
                };
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
                m_template msp = new m_template
                {
                    ID = index,
                    path_url = String.Format("~/view/template/{0}", Path.GetFileName(s))
                };
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

                //::PROCESS UPLOAD
                switch (CommandName)
                {
                    case CommandNameEnum.Add:
                        try
                        {
                            this.PKID = Obj.InsertGetLastID();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine();
                        }
                        break;
                    case CommandNameEnum.Edit:
                        break;
                }
            if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).ToLower().Equals(".xls") || Path.GetExtension(FileUpload1.FileName).ToLower().Equals(".xlt") || Path.GetExtension(FileUpload1.FileName).ToLower().Equals(".xlsx")))
            {
                String _pathSourceFile = String.Format(Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                String _phisicalPath = String.Format(Configurations.PATH_TEMPLATE, String.Empty);
                String _savefilePath = String.Format(Configurations.PATH_TEMPLATE, FileUpload1.FileName);
                if (!Directory.Exists(_phisicalPath))
                {
                    Directory.CreateDirectory(_phisicalPath);
                }
                hPathSourceFile.Value = _pathSourceFile;
                FileUpload1.SaveAs(_savefilePath);
                ProcessUpload(this.PKID, _savefilePath);
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

        private void ProcessUpload(int template_id, String filePath)
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

                            for (int row = 2; row <= isComponent.LastRowNum; row++)
                            {
                                if (isComponent.GetRow(row) != null) //null is when the row only contains empty cells 
                                {

                                tb_m_component component = new tb_m_component
                                {
                                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                    template_id = template_id,
                                    A = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(0)),
                                    B = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(1)),
                                    C = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(2)),
                                    D = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(3)),
                                    E = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(4)),
                                    F = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(5)),
                                    G = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(6)),
                                    H = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(7)),
                                    I = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(8)),
                                    J = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(9)),
                                    K = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(10)),
                                    L = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(11)),
                                    M = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(12)),
                                    N = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(13)),
                                    O = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(14)),
                                    P = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(15)),
                                    Q = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(16)),
                                    R = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(17)),
                                    S = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(18)),
                                    T = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(19)),
                                    U = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(20)),
                                    V = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(21)),
                                    W = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(22)),
                                    X = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(23)),
                                    Y = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(24)),
                                    Z = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(25)),
                                    RowState = CommandNameEnum.Add
                                };

                                #region "Add Detail"
                                if (isDetailSpecRef != null && !String.IsNullOrEmpty(txtSpecRef.Text) && !String.IsNullOrEmpty(component.B) && !component.B.Equals("Spec / Rev"))
                                    {
                                        String _spectRefId = CustomUtils.GetCellValue(isComponent.GetRow(row).GetCell(Convert.ToInt32(txtSpecRef.Text) - 1));
                                        if (!String.IsNullOrEmpty(_spectRefId))
                                        {

                                            int spectRefId = Convert.ToInt32(_spectRefId);
                                            int index = 1;

                                            for (int row1 = 2; row1 <= isDetailSpecRef.LastRowNum; row1++)
                                            {
                                                if (isDetailSpecRef.GetRow(row1) != null)
                                                {
                                                tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref
                                                {
                                                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                                    template_id = template_id,
                                                    spec_ref = spectRefId,
                                                    A = index,
                                                    B = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId)),
                                                    C = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId + 1)),
                                                    D = CustomUtils.GetCellValue(isDetailSpecRef.GetRow(row1).GetCell(spectRefId + 2)),

                                                    RowState = CommandNameEnum.Add
                                                };

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
                                    tb_m_detail_spec detailSpec = new tb_m_detail_spec
                                    {
                                        specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                        template_id = template_id,
                                        A = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(0)),
                                        B = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(1)),
                                        C = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(2)),
                                        D = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(3)),
                                        E = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(4)),
                                        F = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(5)),
                                        G = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(6)),
                                        H = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(7)),
                                        I = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(8)),
                                        J = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(9)),
                                        K = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(10)),
                                        L = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(11)),
                                        M = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(12)),
                                        N = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(13)),
                                        O = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(14)),
                                        P = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(15)),
                                        Q = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(16)),
                                        R = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(17)),
                                        S = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(18)),
                                        T = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(19)),
                                        U = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(20)),
                                        V = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(21)),
                                        W = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(22)),
                                        X = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(23)),
                                        Y = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(24)),
                                        Z = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(25)),
                                        AA = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(26)),
                                        AB = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(27)),
                                        AC = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(28)),
                                        AD = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(29)),
                                        AE = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(30)),
                                        AF = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(31)),
                                        AG = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(32)),
                                        AH = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(33)),
                                        AI = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(34)),
                                        AJ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(35)),
                                        AK = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(36)),
                                        AL = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(37)),
                                        AM = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(38)),
                                        AN = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(39)),
                                        AO = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(40)),
                                        AP = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(41)),
                                        AQ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(42)),
                                        AR = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(43)),
                                        AS = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(44)),
                                        AT = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(45)),
                                        AU = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(46)),
                                        AV = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(47)),
                                        AW = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(48)),
                                        AX = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(49)),
                                        AY = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(50)),
                                        AZ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(51)),
                                        BA = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(52)),
                                        BB = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(53)),
                                        BC = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(54)),
                                        BD = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(55)),
                                        BE = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(56)),
                                        BF = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(57)),
                                        BG = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(58)),
                                        BH = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(59)),
                                        BI = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(60)),
                                        BJ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(61)),
                                        BK = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(62)),
                                        BL = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(63)),
                                        BM = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(64)),
                                        BN = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(65)),
                                        BO = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(66)),
                                        BP = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(67)),
                                        BQ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(68)),
                                        BR = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(69)),
                                        BS = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(70)),
                                        BT = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(71)),
                                        BU = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(72)),
                                        BV = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(73)),
                                        BW = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(74)),
                                        BX = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(75)),
                                        BY = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(76)),
                                        BZ = CustomUtils.GetCellValue(isDetailSpec.GetRow(row).GetCell(77))
                                    };
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
                                tb_m_specification specification = new tb_m_specification
                                {
                                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                    template_id = template_id,
                                    A = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(0)),
                                    B = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(1)),
                                    C = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(2)),
                                    D = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(3)),
                                    E = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(4)),
                                    F = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(5)),
                                    G = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(6)),
                                    H = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(7)),
                                    I = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(8)),
                                    J = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(9)),
                                    K = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(10)),
                                    L = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(11)),
                                    M = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(12)),
                                    N = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(13)),
                                    O = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(14)),
                                    P = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(15)),
                                    Q = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(16)),
                                    R = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(17)),
                                    S = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(18)),
                                    T = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(19)),
                                    U = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(20)),
                                    V = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(21)),
                                    W = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(22)),
                                    X = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(23)),
                                    Y = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(24)),
                                    Z = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(25)),

                                    AA = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(26)),
                                    AB = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(27)),
                                    AC = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(28)),
                                    AD = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(29)),
                                    AE = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(30)),
                                    AF = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(31)),
                                    AG = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(32)),
                                    AH = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(33)),
                                    AI = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(34)),
                                    AJ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(35)),

                                    AK = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(36)),
                                    AL = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(37)),
                                    AM = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(38)),
                                    AN = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(39)),
                                    AO = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(40)),
                                    AP = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(41)),
                                    AQ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(42)),
                                    AR = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(43)),
                                    AS = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(44)),
                                    AT = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(45)),
                                    AU = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(46)),
                                    AV = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(47)),
                                    AW = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(48)),
                                    AX = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(49)),
                                    AY = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(50)),
                                    AZ = CustomUtils.GetCellValue(isSpecification.GetRow(row).GetCell(51)),

                                    status = "A",
                                    RowState = CommandNameEnum.Add
                                };
                                specifications.Add(specification);
                                }
                            }
                        //Delete
                        new tb_m_specification().DeleteByTemplateID(template_id);
                        new tb_m_specification().InsertList(specifications);
                        }
                    #endregion

                    #region "Specification No."
                    ISheet isSpecificationNo = wd.GetSheet("Specification No.");
                    if (isSpecificationNo != null)
                    {
                        List<tb_m_specification> specifications = new List<tb_m_specification>();
                        for (int row = 0; row <= isSpecificationNo.LastRowNum; row++)
                        {
                            if (isSpecificationNo.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                tb_m_specification specification = new tb_m_specification
                                {
                                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                    template_id = template_id,
                                    A = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(0)),
                                    B = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(1)),
                                    C = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(2)),
                                    D = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(3)),
                                    E = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(4)),
                                    F = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(5)),
                                    G = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(6)),
                                    H = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(7)),
                                    I = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(8)),
                                    J = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(9)),
                                    K = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(10)),
                                    L = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(11)),
                                    M = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(12)),
                                    N = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(13)),
                                    O = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(14)),
                                    P = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(15)),
                                    Q = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(16)),
                                    R = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(17)),
                                    S = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(18)),
                                    T = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(19)),
                                    U = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(20)),
                                    V = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(21)),
                                    W = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(22)),
                                    X = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(23)),
                                    Y = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(24)),
                                    Z = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(25)),

                                    AA = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(26)),
                                    AB = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(27)),
                                    AC = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(28)),
                                    AD = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(29)),
                                    AE = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(30)),
                                    AF = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(31)),
                                    AG = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(32)),
                                    AH = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(33)),
                                    AI = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(34)),
                                    AJ = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(35)),

                                    AK = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(36)),
                                    AL = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(37)),
                                    AM = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(38)),
                                    AN = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(39)),
                                    AO = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(40)),
                                    AP = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(41)),
                                    AQ = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(42)),
                                    AR = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(43)),
                                    AS = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(44)),
                                    AT = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(45)),
                                    AU = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(46)),
                                    AV = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(47)),
                                    AW = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(48)),
                                    AX = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(49)),
                                    AY = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(50)),
                                    AZ = CustomUtils.GetCellValue(isSpecificationNo.GetRow(row).GetCell(51)),

                                    status = "A",
                                    RowState = CommandNameEnum.Add
                                };
                                specifications.Add(specification);
                            }
                        }
                        //Delete
                        new tb_m_specification().DeleteByTemplateID(template_id);
                        new tb_m_specification().InsertList(specifications);
                    }
                    #endregion
                    #region "PA-DROPDOWN"
                    ISheet isPaDropdown = wd.GetSheet("Drop down");
                    if (isPaDropdown != null)
                    {
                        List<tb_m_specification> specifications = new List<tb_m_specification>();
                        for (int row = 0; row <= isPaDropdown.LastRowNum; row++)
                        {
                            if (isPaDropdown.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                tb_m_specification specification = new tb_m_specification
                                {
                                    specification_id = Convert.ToInt32(ddlSpecification.SelectedValue),
                                    template_id = template_id,
                                    A = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(0)),
                                    B = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(1)),
                                    C = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(2)),
                                    D = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(3)),
                                    E = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(4)),
                                    F = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(5)),
                                    G = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(6)),
                                    H = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(7)),
                                    I = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(8)),
                                    J = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(9)),
                                    K = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(10)),
                                    L = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(11)),
                                    M = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(12)),
                                    N = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(13)),
                                    O = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(14)),
                                    P = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(15)),
                                    Q = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(16)),
                                    R = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(17)),
                                    S = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(18)),
                                    T = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(19)),
                                    U = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(20)),
                                    V = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(21)),
                                    W = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(22)),
                                    X = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(23)),
                                    Y = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(24)),
                                    Z = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(25)),

                                    AA = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(26)),
                                    AB = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(27)),
                                    AC = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(28)),
                                    AD = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(29)),
                                    AE = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(30)),
                                    AF = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(31)),
                                    AG = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(32)),
                                    AH = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(33)),
                                    AI = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(34)),
                                    AJ = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(35)),

                                    AK = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(36)),
                                    AL = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(37)),
                                    AM = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(38)),
                                    AN = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(39)),
                                    AO = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(40)),
                                    AP = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(41)),
                                    AQ = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(42)),
                                    AR = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(43)),
                                    AS = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(44)),
                                    AT = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(45)),
                                    AU = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(46)),
                                    AV = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(47)),
                                    AW = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(48)),
                                    AX = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(49)),
                                    AY = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(50)),
                                    AZ = CustomUtils.GetCellValue(isPaDropdown.GetRow(row).GetCell(51)),

                                    status = "A",
                                    RowState = CommandNameEnum.Add
                                };
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



