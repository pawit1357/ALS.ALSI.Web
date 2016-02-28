using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace ALS.ALSI.Utils
{
    public static class MessageBox
    {
        public static void Show(this Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
        public static void Show(this Page Page, String Message, String Location)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');window.location.href='" + Location + "';</script>"
            );
        }



        public static String GenWarnning(List<String> errorsList)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"note note-danger\">");
            sb.Append("<h4 class=\"block\">เกิดข้อผิดพลาด! กรุณาตรวจสอบตามรายละเอียดด่านล่าง</h4>");
            int index = 1;
            foreach (String error in errorsList)
            {
                sb.Append("<p>" + index + ". " + error + "</p>");
                index++;
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
