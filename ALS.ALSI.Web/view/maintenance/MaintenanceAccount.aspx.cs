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
using System.Net;
using System.Web;
using System.Web.UI;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Data;

namespace ALS.ALSI.Web.view.template
{
    public partial class MaintenanceAccount : System.Web.UI.Page
    {
        //private string token = string.Empty;

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
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Get an authentication access token
            string token = GetToken();

            //Create a dataset in Power BI
            //CreateDataset();

            //Get a dataset to add rows into a Power BI table
            string datasetId = GetDataset(token);

            //Add rows to a Power BI table

            DataTable dt = MaintenanceBiz.ExecuteReturnDt(String.Format("select * from {0}", rdDsBi.SelectedValue));
            String JSONresult = JsonConvert.SerializeObject(dt);
            AddRows(datasetId, rdDsBi.SelectedValue, JSONresult, token);


            Message = "<div class=\"alert alert-success\"><strong>Info!</strong>ส่งข้อมูล "+rdDsBi.SelectedItem.Text+" ไปยัง Power-BI เรียบร้อยแล้ว</div>";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(Constants.LINK_SEARCH_JOB_REQUEST);
        }

        #region Get an authentication access token
        private static string GetToken()
        {
            // TODO: Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory -Version 2.21.301221612
            // and add using Microsoft.IdentityModel.Clients.ActiveDirectory

            //The client id that Azure AD created when you registered your client app.
            string clientID = Configurations.ClientID;//APPLICATION ID

            //RedirectUri you used when you register your app.
            //For a client app, a redirect uri gives Azure AD more details on the application that it will authenticate.
            // You can use this redirect uri for your client app
            string redirectUri = "https://login.live.com/oauth20_desktop.srf";

            //Resource Uri for Power BI API
            string resourceUri = "https://analysis.windows.net/powerbi/api";

            //OAuth2 authority Uri
            string authorityUri = "https://login.windows.net/common/oauth2/authorize";

            //Get access token:
            // To call a Power BI REST operation, create an instance of AuthenticationContext and call AcquireToken
            // AuthenticationContext is part of the Active Directory Authentication Library NuGet package
            // To install the Active Directory Authentication Library NuGet package in Visual Studio,
            //  run "Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory" from the nuget Package Manager Console.

            // AcquireToken will acquire an Azure access token
            // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint
            AuthenticationContext authContext = new AuthenticationContext(authorityUri);
            string token = authContext.AcquireToken(resourceUri, clientID, new Uri(redirectUri)).AccessToken;

            Console.WriteLine(token);
            //Console.ReadLine();

            return token;
        }

        #endregion

        #region Create a dataset in a Power BI
        private void CreateDataset(string token)
        {
            //TODO: Add using System.Net and using System.IO

            string powerBIDatasetsApiUrl = "https://api.powerbi.com/v1.0/myorg/datasets";
            //POST web request to create a dataset.
            //To create a Dataset in a group, use the Groups uri: https://api.PowerBI.com/v1.0/myorg/groups/{group_id}/datasets
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIDatasetsApiUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            //Create dataset JSON for POST request

            //Create dataset JSON for POST request
            string datasetJson = "{\"name\": \"ALS_ALIS_BI_DS\", \"tables\": " +
                "[" +
                /* =============== vw_revenue_actual =============== */
                "{\"name\": \"vw_revenue_actual\", \"columns\": " +
                "[{ \"name\": \"rev_year\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"rev_month\", \"dataType\": \"string\"}, " +
                "{ \"name\": \"rev_type\", \"dataType\": \"string\"}," +
                "{ \"name\": \"rev_amt\", \"dataType\": \"Int64\"}]" +
                "}," +
                /* =============== vw_forcast_budget =============== */
                "{\"name\": \"vw_forcast_budget\", \"columns\": " +
                "[{ \"name\": \"rev_date\", \"dataType\": \"DateTime\"}, " +
                "{ \"name\": \"rev_amt\", \"dataType\": \"Int64\"}]" +
                "}," +
                /* =============== vw_outstanding_balance =============== */
                "{\"name\": \"vw_outstanding_balance\", \"columns\": " +
                "[{ \"name\": \"company_id\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"company_name\", \"dataType\": \"string\"}, " +
                "{ \"name\": \"sample_invoice\", \"dataType\": \"string\"}, " +
                "{ \"name\": \"overdue_date\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"outstanding_balance\", \"dataType\": \"Int64\"}]" +
                "}," +
                /* =============== vw_dinv =============== */
                "{\"name\": \"vw_dinv\", \"columns\": " +
                "[{ \"name\": \"sample_invoice_date\", \"dataType\": \"DateTime\"}, " +
                "{ \"name\": \"countInv\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"outstanding_balance\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"overdue_date\", \"dataType\": \"Int64\"}," +
                "{ \"name\": \"inv_status\", \"dataType\": \"string\"}]" +
                "}," +
                /* =============== vw_summary_inv_status =============== */
                "{\"name\": \"vw_summary_inv_status\", \"columns\": " +
                "[{ \"name\": \"inv_status\", \"dataType\": \"string\"}, " +
                "{ \"name\": \"countInv\", \"dataType\": \"Int64\"}, " +
                "{ \"name\": \"outstanding_balance\", \"dataType\": \"Int64\"}]" +
                "}" +
                "]}";

            //POST web request
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(datasetJson);
            request.ContentLength = byteArray.Length;

            //Write JSON byte[] into a Stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);

                var response = (HttpWebResponse)request.GetResponse();

                Console.WriteLine(string.Format("Dataset {0}", response.StatusCode.ToString()));

                //Console.ReadLine();
            }
        }
        #endregion

        #region Get a dataset to add rows into a Power BI table
        private string GetDataset(string token)
        {
            string powerBIDatasetsApiUrl = "https://api.powerbi.com/v1.0/myorg/datasets";
            //POST web request to create a dataset.
            //To create a Dataset in a group, use the Groups uri: https://api.PowerBI.com/v1.0/myorg/groups/{group_id}/datasets
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIDatasetsApiUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            string datasetId = string.Empty;
            //Get HttpWebResponse from GET request
            using (HttpWebResponse httpResponse = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get StreamReader that holds the response stream
                using (StreamReader reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();

                    //TODO: Install NuGet Newtonsoft.Json package: Install-Package Newtonsoft.Json
                    //and add using Newtonsoft.Json
                    var results = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    //Get the first id
                    datasetId = results["value"][0]["id"];

                    Console.WriteLine(String.Format("Dataset ID: {0}", datasetId));
                    //Console.ReadLine();

                    return datasetId;
                }
            }
        }
        #endregion

        #region Add rows to a Power BI table
        private void AddRows(string datasetId, string tableName, string jsonData,string token)
        {
            string powerBIApiAddRowsUrl = String.Format("https://api.powerbi.com/v1.0/myorg/datasets/{0}/tables/{1}/rows", datasetId, tableName);

            //POST web request to add rows.
            //To add rows to a dataset in a group, use the Groups uri: https://api.powerbi.com/v1.0/myorg/groups/{group_id}/datasets/{dataset_id}/tables/{table_name}/rows
            //Change request method to "POST"
            HttpWebRequest request = System.Net.WebRequest.Create(powerBIApiAddRowsUrl) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = rdDsBiPostType.SelectedValue;// "POST";// "DELETE";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            //Add token to the request header
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            //JSON content for product row
            string rowsJson = "{\"rows\":" + jsonData + "}";

            //POST web request
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(rowsJson);
            request.ContentLength = byteArray.Length;

            //Write JSON byte[] into a Stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);
                var response = (HttpWebResponse)request.GetResponse();
            }

        }
        #endregion

    }
}



