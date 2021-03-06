﻿using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.initial
{
    public partial class Maintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            Boolean bResult = MaintenanceBiz.ExecuteCommand(txtSql.Text);
            lbResult.Text = bResult ? "Success" : "Fail";
            Console.WriteLine();
        }

        protected void btnGetDs_Click(object sender, EventArgs e)
        {
            DataSet ds = MaintenanceBiz.ExecuteReturnDs(txtSql.Text);

            gvResult.DataSource = ds;
            gvResult.DataBind();

        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            // MySQLBackup mysqlBackup = new MySQLBackup();
            //HiddenField1.Value =   mysqlBackup.Backup();
            ExportToExcel(sender, e);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, HiddenField1.Value));

        }

        protected void ExportToExcel(object sender, EventArgs e)
        {


            String filePath = MaintenanceBiz.GenScript();

            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");



            String downloadUrl = Configurations.HOST + "" + String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, "Script", Path.GetFileName(filePath));



            LinkButton1.PostBackUrl = downloadUrl;
            Console.WriteLine();

            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";


            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    gvResult.AllowPaging = false;

            //    gvResult.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in gvResult.HeaderRow.Cells)
            //    {
            //        cell.BackColor = gvResult.HeaderStyle.BackColor;

            //    }

            //    foreach (GridViewRow row in gvResult.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {

            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = gvResult.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = gvResult.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";


            //        }

            //    }


            //    gvResult.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnListDir_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order", typeof(string));
            dt.Columns.Add("File Name", typeof(string));

            DirectoryInfo d = new DirectoryInfo(txtSql.Text);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files

            int index = 1;
            foreach (FileInfo file in Files)
            {
                DataRow dr = dt.NewRow();

                dr[0] = index;
                dr[1] = file.Name;
                index++;

                dt.Rows.Add(dr);//this will add the row at the end of the datatable
            }
            gvResult.DataSource = dt;
            gvResult.DataBind();
        }

        protected void btnRename_Click(object sender, EventArgs e)
        {
            File.Move(txtSql.Text+"\\"+TextBox1.Text, txtSql.Text + "\\" + TextBox2.Text);

        }
    }
}