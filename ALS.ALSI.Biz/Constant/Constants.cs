using System;
using System.ComponentModel;
using System.Reflection;

namespace ALS.ALSI.Biz.Constant
{
    public class Constants
    {
        public const String APPNAME = "/alis";
        public const String SUM_OF_CORRECTED_AREAS = "Sum of corrected areas:";
        public const String WD_M = "WD.M";
        public const String DHS = "DHS";

        public const String PREVIOUS_PATH = "PreviousPath";
        public const String COMMAND_NAME = "CommandName";
        public const String SESSION_USER = "UserData";
        public const String SORT_DIRECTION = "SortDirection";
        public const String PLEASE_SELECT = "Please Select";
        public const String TOTAL_RECORDS = "Total {0} Records";
        public const char CHAR_COMMA = ',';
        public const char CHAR_COLON = ':';
        public const String CHAR_DASH = "-";

        public const String BASE_TEMPLATE_PATH = APPNAME + "/view/template/";
        public const String LINK_SAMPLE_INFO = APPNAME + "/UserControls/SampleInfo.ascx";

        public const String LINK_SEARCH_TEMPLATE = APPNAME + "/view/template/SearchTemplate.aspx";
        public const String LINK_TEMPLATE = APPNAME + "/view/template/Template.aspx";

        public const String LINK_LOGIN = APPNAME + "/Login.aspx";
        public const String LINK_JOB_WORK_FLOW = APPNAME + "/view/request/JobWorkFlow.aspx";
        public const String LINK_JOB_CONVERT_TEMPLATE = APPNAME + "/view/request/JobConvertTemplate.aspx";
        public const String LINK_JOB_REQUEST = APPNAME + "/view/request/JobRequest.aspx";
        public const String LINK_SEARCH_JOB_REQUEST = APPNAME + "/view/request/SearchJobRequest.aspx";
        public const String LINK_JOB_CHANGE_STATUS = APPNAME + "/view/request/ChangeJobsStatus.aspx";
        public const String LINK_JOB_CHANGE_DUEDATE = APPNAME + "/view/request/ChangeJobDueDate.aspx";
        public const String LINK_JOB_CHANGE_PO = APPNAME + "/view/request/ChangeJobPo.aspx";
        public const String LINK_REPORT_DATE = APPNAME + "/view/request/ChangeReportDate.aspx";

        public const String LINK_ADMIN_PRINT = APPNAME + "/view/request/AdminPrint.aspx";
        public const String LINK_AMEND = APPNAME + "/view/request/JobAmend.aspx";
        public const String LINK_RETEST = APPNAME + "/view/request/JobReTest.aspx";


        public const String LINK_JOB_CHANGE_INVOICE = APPNAME + "/view/request/ChangeJobInvoice.aspx";
        public const String LINK_JOB_PRINT_LABEL = APPNAME + "/view/request/PrintSticker.aspx";
        public const String LINK_SEARCH_USER = APPNAME + "/view/user/SearchUser.aspx";
        public const String LINK_USER = APPNAME + "/view/user/User.aspx";

        public const String LINK_SEARCH_ROLE = APPNAME + "/view/role/SearchRole.aspx";
        public const String LINK_ROLE = APPNAME + "/view/role/Role.aspx";

        public const String LINK_SEARCH_PERMISSION = APPNAME + "/view/permission/SearchPermission.aspx";
        public const String LINK_PERMISSION = APPNAME + "/view/permission/Permission.aspx";

        public const String LINK_SEARCH_CUSTOMER = APPNAME + "/view/customer/SearchCustomer.aspx";
        public const String LINK_CUSTOMER = APPNAME + "/view/customer/Customer.aspx";

        public const String LINK_SEARCH_CONTRACT_PERSON = APPNAME + "/view/customerContract/SearchContractPerson.aspx";
        public const String LINK_CONTRACT_PERSON = APPNAME + "/view/customerContract/ContractPerson.aspx";

        public const String LINK_SEARCH_SPECIFICATION = APPNAME + "/view/specification/SearchSpecification.aspx";
        public const String LINK_SPECIFICATION = APPNAME + "/view/specification/Specification.aspx";

        public const String LINK_SEARCH_LIBRARY = APPNAME + "/view/library/SearchLibrary.aspx";
        public const String LINK_LIBRARY = APPNAME + "/view/library/Library.aspx";

        public const String LINK_SEARCH_TYPE_OF_TEST = APPNAME + "/view/type_of_test/SearchTypeOfTest.aspx";
        public const String LINK_TYPE_OF_TEST = APPNAME + "/view/type_of_test/TypeOfTest.aspx";





        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string[] WD_DHS = {
                        "Total Acrylate and Methacrylate",
                        "Total Acrylate",
                        "Total Methacrylate",
                        "Total Siloxane",
                        "Total Silane",//new
                        "Total Unknown",
                        "Total Sulfur Compound",
                        "Total Phthalate",
                        "Total Phenol",
                        "Total Alcohol",//Change position
                        "Total Others",
                        "Total Initiator",
                        "Total Antioxidant",
                        "Total Hydrocarbon",
                        "Total Aromatic Hydrocarbon",
                        "Total Aliphatic Hydrocarbon",
                        "Hydrocarbon Hump",
                        "Total Outgassing",
                        //"Total Outgassing (no of arms: >= 4)",
                        //"Total Outgassing (no of arms: <= 3)",
                        //"Total Outgassing (no of arms: >= 4)"
        };

    }

    public enum CommandNameEnum : int
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        View = 4,
        Workflow = 5,
        Print = 6,
        Page = 7,
        ConvertTemplate = 8,
        ChangeStatus = 9,
        ChangeDueDate = 10,
        ChangePo = 11,
        ChangeInvoice = 12,
        Update = 14,
        Cancel = 15,
        Undo = 16,
        Hum = 17,
        D34 = 18,
        D10 = 19,
        HydrocarbonHump = 20,
        TotalHydrocarbonHump = 21,
        TotalOutGas = 22,
        TotalRow = 23,
        Hide = 24,
        SampleSize = 25,
        UnHide = 26,
        MajorCompounds = 27,
        ChangeReportDate = 28,
        Amend = 29,
        Retest = 30,
        Normal = 31,
        Inactive = 32
    }

    public enum RowTypeEnum : int
    {
        Hide = 0,
        Normal = 1,
        Hum = 2,
        D34 = 3,
        D10 = 4,
        HydrocarbonHump = 5,
        TotalHydrocarbonHump = 6,
        TotalOutGas = 7,
        TotalRow = 8,
        SampleSize = 10,
        //AD10Directinject =11,
        MajorCompounds = 12,
        Standard = 13,
        CoverPageValue = 14,
        c31 = 15
    }

    public enum RoleEnum
    {
        ROOT = 1,
        LOGIN = 2,
        CHEMIST = 3,
        SR_CHEMIST = 4,
        ADMIN = 5,
        LABMANAGER = 6,
        ACCOUNT = 7
    }

    public enum ResultEnum
    {
        [Description("-")]
        DASH = 1,
        [Description("NA")]
        NA = 2,
        [Description("ND")]
        ND = 3,
        [Description("Not Detected")]
        NOT_DETECTED = 4,
        [Description("PASS")]
        PASS = 5,
        [Description("FAIL")]
        FAIL = 6,
    }

    public enum StatusEnum
    {
        [Description("CANCEL")]
        JOB_CANCEL = 1,
        [Description("HOLD")]
        JOB_HOLD = 2,
        [Description("COMPLETE")]
        JOB_COMPLETE = 3,


        [Description("SR_CHEMIST_CHECKING")]
        SR_CHEMIST_CHECKING = 4,
        [Description("APPROVE")]
        SR_CHEMIST_APPROVE = 5,
        [Description("DISAPPROVE")]
        SR_CHEMIST_DISAPPROVE = 6,



        [Description("APPROVE")]
        LABMANAGER_APPROVE = 7,
        [Description("DISAPPROVE")]
        LABMANAGER_DISAPPROVE = 8,
        [Description("LABMANAGER_CHECKING")]
        LABMANAGER_CHECKING = 9,

        [Description("CONVERT_TEMPLATE")]
        LOGIN_CONVERT_TEMPLATE = 10,
        [Description("SELECT_SPEC")]
        LOGIN_SELECT_SPEC = 11,

        [Description("CHEMIST_TESTING")]
        CHEMIST_TESTING = 12,


        [Description("CONVERT_WORD")]
        ADMIN_CONVERT_WORD = 13,
        [Description("CONVERT_PDF")]
        ADMIN_CONVERT_PDF = 14,


    }

    public enum CompletionScheduledEnum
    {
        NORMAL = 1,
        URGENT = 2,
        EXPRESS = 3
    }

    public enum SpecificationEnum
    {
        [Description("Seagate")]
        SEAGATE = 1,
        [Description("WD")]
        WD = 2
    }
    public enum ICTypeEnum
    {
        [Description("Anionic")]
        ANIONIC = 1,
        [Description("Cationic")]
        CATIONIC = 2
    }
    public enum GVTypeEnum
    {
        LPC03 = 1,
        LPC06 = 2,
        HPA = 3,
        CLASSIFICATION = 4,
        ANALYSIS_DETAILS = 5,
        CLASSIFICATION_HEAD = 6,
        CLASSIFICATION_ITEM = 7,
        CLASSIFICATION_TOTAL = 8,
        CLASSIFICATION_GRAND_TOTAL = 9,
        CLASSIFICATION_SUB_TOTAL = 10

    }
    public enum LPCTypeEnum
    {
        [Description("68 KHz")]
        KHz_68 = 1,
        [Description("132 KHz")]
        KHz_132 = 2
    }
    public enum ParticleTypeEnum
    {
        [Description("0.3")]
        PAR_03 = 1,
        [Description("0.6")]
        PAR_06 = 2
    }

    public enum MenuRoleActionEnum : int
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
    }
    public enum HPAType : int
    {
        LPC03 = 1,
        LPC06 = 2,
        HPA = 3,
        CLASSIFICATION = 4,
        ANALYSIS_DETAILS = 5,
        CLASSIFICATION_HEAD = 6,
        CLASSIFICATION_ITEM = 7,
        CLASSIFICATION_TOTAL = 8,
        CLASSIFICATION_GRAND_TOTAL = 9,
        CLASSIFICATION_SUB_TOTAL = 10,
        LPC =11,
        LPC05 = 12,
    }
    public enum HPAFor3Group : int
    {
        [Description("Result on Arm")]
        RESULT_ON_ARM = 1,
        [Description("Result on Pivot ")]
        RESULT_ON_PIVOT = 2,
        [Description("Result on Swage")]
        RESULT_ON_SWAGE = 3,
        [Description("Average result")]
        AVERAGE_RESULT = 4,
        [Description("Specification")]
        SPECIFICATION = 5,
        [Description("PASS / FAIL")]
        PASS_SLASH_FAIL = 6,
        RAWDATA_ARM = 7,
        RAWDATA_PIVOT = 8,
        RAWDATA_SWAGE = 9,
        ARM_SUB_TOTAL = 10,
        ARM_GRAND_TOTAL = 11,
        PIVOT_SUB_TOTAL = 12,
        PIVOT_GRAND_TOTAL = 13,
        SWAGE_SUB_TOTAL = 14,
        SWAGE_GRAND_TOTAL = 15
    }
    public enum WDLpcDataType : int
    {
        SPEC = 1,
        DATA_VALUE = 2,
        SUMMARY = 3
    }

    public enum IMAGE_ORDER_TYPE : int
    {
        PHOTO_BEFORE = 1,
        PHOTO_AFTER = 2
    }
    public enum GcmsSeagateEnum : int
    {
        RHC_BASE = 1,
        RHC_HUB = 2
    }

    public enum FtirNvrEnum : int
    {
        METHOD_PROCEDURE = 1,
        FTIR_SPEC = 2,
        FTIR_RAW_DATA = 3,
        NVR_RAW_DATA = 4,
        NVR_RAW_DATA_DI = 7,
        NVR_SPEC = 5,
        NVR_RAW_DATA_IPA = 6
    }

    public enum SeagateGcmsEnum : int
    {
        MOTOR_OIL = 1,
        MOTOR_HUB = 2,
        MOTOR_HUB_SUB = 3,
        MOTOR_BASE = 4,
        MOTOR_BASE_SUB = 5,
        COMPOUND = 6,
        COMPOUND_SUB = 7
    }
}

