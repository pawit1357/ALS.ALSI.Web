using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NotesFor.HtmlToOpenXml;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ALS.ALSI.Biz
{
    public class GenerateHtmlBiz : System.Web.UI.Page
    {
        public ReportHeader reportHeader { get; set; }
        public int PageCount { get; set; }
        public int PageTotal { get; set; }
        public Boolean ShowLogo { get; set; }
        //public Boolean ShowFooter { get; set; }
        public Boolean ShowIfFirstPage { get; set; }
        public DataTable MethodProcedure { get; set; }
        public DataTable Result { get; set; }
        public DataTable Result2 { get; set; }

        public String SampleVol { get; set; }
        public String DocumentNo { get; set; }
        public String SpecDetail { get; set; }
        public String SurfaceArea { get; set; }

        public String TotalTankVolume { get; set; }
        public String SonicationFreq { get; set; }
        public String UltrasonicPower { get; set; }


        public GenerateHtmlBiz()
        {
            PageCount = 1;
            PageTotal = 1;
            ShowLogo = true;
            ShowIfFirstPage = true;
            //ShowFooter = false;
        }

        public static void test()
        {

            List<tb_m_specification> specifications = new List<tb_m_specification>();



            FileInfo fileInfo = new FileInfo(@"D:\IC.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {

                ExcelWorksheet sConfig = package.Workbook.Worksheets["Config"];

                ExcelWorksheet sCoverPage = package.Workbook.Worksheets["Coverpage-TH"];
                var methodProcedureHeaders = sCoverPage.Cells[sConfig.Cells["C3"].Text];
                foreach (var item in methodProcedureHeaders) 
                {
                    //tb_m_specification mSpec = new tb_m_specification
                    //{
                    //    ID = Convert.ToInt16(Regex.Replace(item.Address, @"[^\d]", "")),
                    //    A = (item.Value == null) ? String.Empty : item.Value.ToString()
                    //};
                    Console.WriteLine();
                }

                //ExcelWorksheet sSpecification = package.Workbook.Worksheets["Specification"];
                //var _specifications = sSpecification.Cells[sConfig.Cells["C2"].Text];
                //foreach(var item in _specifications)
                //{
                //    tb_m_specification mSpec = new tb_m_specification
                //    {
                //        ID = Convert.ToInt16(Regex.Replace(item.Address, @"[^\d]", "")),
                //        A = (item.Value==null)? String.Empty: item.Value.ToString()
                //    };
                //    specifications.Add(mSpec);

                //    Console.WriteLine();
                //}

                Console.WriteLine();
                //Config
                //var sheet = package.Workbook.Worksheets.Add("Formula");

                //sheet.Cells["B2"].Value = "2";
                //sheet.Cells["C2"].Value = "2";

                //sheet.Cells["E2"].Formula = "B2*C2"; // quantity * price

                //sheet.Calculate();
                //String xxx = sheet.Cells["E2"].Text;
                Console.WriteLine();
            }


        }
        private string getHeaderInfo()
        {
            StringBuilder _header = new StringBuilder();
            //LOGO
            if (ShowLogo)
            {
                String url = @"http://www.alsglobal.com/ALS.Corporate.Web/styles/img/logo_icon.png";
                _header.Append(String.Format("<img src=\"{0}\" alt=\"Test picture\" />", url));
            }
            //PageOf
            _header.Append("<table width=\"100%\">");
            _header.Append("<tr><td  align=\"right\"><p style=\"font-family:arial;font-size:11pt;\">" + String.Format("Page {0} of {1}", PageCount, PageTotal) + "</p></td></tr>");
            _header.Append("</table>");
            //Header
            if (ShowIfFirstPage)
            {
                _header.Append("<table width=\"100%\">");
                _header.Append("<tr><td  align=\"center\"><p style=\"font-family:arial;font-size:15pt;font-style:blod;text-decoration:underline;\">REPORT</p></td></tr>");
                _header.Append("</table>");
            }
            //Info
            _header.Append("<table width=\"100%\">");
            if (ShowIfFirstPage)
            {
                _header.Append("<tr><td width=\"40%\">CUSTOMER PO NO.:</td><td width=\"60%\"><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.cusRefNo + "</p></td></tr>");
            }
            _header.Append("<tr><td>ALS THAILAND REF NO.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.alsRefNo + "</td></tr>");
            _header.Append("<tr><td>DATE.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.cur_date + "</td></tr>");
            _header.Append("<tr><td>COMPANY.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.addr1 + reportHeader.addr2 + "</p></td></tr>");

            if (ShowIfFirstPage)
            {
                _header.Append("<tr><td>DATE SAMPLE RECEIVED.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.dateOfDampleRecieve + "</p></td></tr>");
                _header.Append("<tr><td>DATE ANALYZED.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.dateOfAnalyze + "</p></td></tr>");
                _header.Append("<tr><td>DATE TEST COMPLETED.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.dateOfTestComplete + "</p></td></tr>");

                _header.Append("<tr><td>SAMPLE DESCRIPTION.:</td><td><p style=\"font-family:arial;font-size:11pt;\">" + reportHeader.description + "</p></td></tr>");
            }
            _header.Append("</table>");
            return _header.ToString();
        }

        private string Page1()
        {
            ShowIfFirstPage = true;
            StringBuilder html = new StringBuilder();
            //Heaer
            html.Append(getHeaderInfo());
            //
            html.Append("<p style=\"font-family:arial;font-size:11pt;\">METHOD/PROCEDURE:</p>");
            //
            html.Append("<table border=\"1\" width=\"100%\">");
            html.Append("<tr>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Test</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Procedure No</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Number of pieces <br> used for extraction</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"25%\">Extraction <br> Medium</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Extraction Volume <br> (mL)</td>");
            html.Append("</tr>");
            foreach (DataRow dr in MethodProcedure.Rows)
            {
                html.Append("<tr>");
                html.Append("<td  align=\"center\">LPC</td>");
                html.Append("<td  align=\"center\">" + dr["ProcedureNo"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["NumberOfPieces"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["ExtractionMedium"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["ExtractionVolume"] + "</td>");
                html.Append("</tr>");
            }
            html.Append("</table>");
            //Result
            html.Append("<p style=\"font-family:arial;font-size:11pt;\">Results:</p>");
            html.Append("<p style=\"font-family:arial;font-size:11pt;\">" + String.Format("The specification is based on Western Digital's document no. {0} for {1}", DocumentNo, SpecDetail) + "</p>");


            html.Append("<table border=\"1\" width=\"100%\">");
            html.Append("<tr>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Required Test</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Particle Bin Size (µm)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Specification Limits <br>(Particles / cm2)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"25%\">Average of 5 data points<br>(Particles / cm2)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">PASS / FAIL</td>");
            html.Append("</tr>");
            foreach (DataRow dr in Result.Rows)
            {
                html.Append("<tr>");
                html.Append("<td  align=\"center\">LPC</td>");
                html.Append("<td  align=\"center\">" + dr["B"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["C"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["D"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["E"] + "</td>");
                html.Append("</tr>");
            }
            html.Append("</table>");
            //------
            html.Append("<table border=\"1\" width=\"100%\">");
            html.Append("<tr>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"10%\">RUN</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Accumlative Size <br>(µm)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Blank <br>(Counts / mL)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Sample <br>(Counts / mL)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Blank-corrected <br>(Counts / part)</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Blank-corrected <br>(Counts / cm²)</td>");

            html.Append("</tr>");
            foreach (DataRow dr in Result2.Rows)
            {
                html.Append("<tr>");
                html.Append("<td  align=\"center\">" + dr["A"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["B"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["C"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["D"] + "</td>");
                html.Append("<td  align=\"center\">" + dr["E"] + "</td>");
                html.Append("</tr>");
            }
            html.Append("</table>");
            return html.ToString();
        }

        private string Page2()
        {
            ShowIfFirstPage = false;
            StringBuilder html = new StringBuilder();
            //Heaer
            html.Append(getHeaderInfo());
            //Test Method
            html.Append("<table border=\"1\" width=\"50%\">");
            html.Append("<tr>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" colspan=\"2\">" + String.Format("Test Method:{0}", MethodProcedure.Rows[0]["ProcedureNo"].ToString()) + "</td>");
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td width=\"50%\"  align=\"left\">Surface Area, cm²</td>");
            html.Append("<td width=\"50%\"  align=\"right\">" + SurfaceArea + "</td>");
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td width=\"50%\"  align=\"left\">Extraction Vol, mL</td>");
            html.Append("<td width=\"50%\"  align=\"right\">" + MethodProcedure.Rows[0]["ExtractionVolume"] + "</td>");
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td width=\"50%\"  align=\"left\">Sample Vol, mL</td>");
            html.Append("<td width=\"50%\"  align=\"right\">" + SampleVol + "</td>");
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td width=\"50%\"  align=\"left\">No. of Parts</td>");
            html.Append("<td width=\"50%\"  align=\"right\">" + MethodProcedure.Rows[0]["NumberOfPieces"] + "</td>");
            html.Append("</tr>");
            html.Append("</table>");

            //Thank And Condition
            html.Append("<table border=\"1\" width=\"100%\">");
            html.Append("<tr>");
            html.Append("<td rowspan=\"2\" align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Tank Conditions</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"15%\">Total Tank  Volume</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Sonication Freq.</td>");
            html.Append("<td align=\"center\"  bgcolor=\"#D3D3D3\" width=\"20%\">Ultrasonic Power</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td  align=\"center\">" + TotalTankVolume + "</td>");
            html.Append("<td  align=\"center\">" + SonicationFreq + "</td>");
            html.Append("<td  align=\"center\">" + UltrasonicPower + "</td>");
            html.Append("</tr>");

            html.Append("</table>");

            return html.ToString();
        }





        //private string getFooter()
        //{
        //    StringBuilder _footer = new StringBuilder();
        //    if (ShowFooter)
        //    {
        //        String url = @"http://www.alsglobal.com/ALS.Corporate.Web/styles/img/logo_icon.png";
        //        _footer.Append(String.Format("<img src=\"{0}\" alt=\"Test picture\" />", url));
        //    }
        //    return _footer.ToString();
        //}

        //public string GenerateHtml()
        //{
        //    StringBuilder html = new StringBuilder();
        //    //html.Append("<html " + "xmlns:o='urn:schemas-microsoft-com:office:office' " + "xmlns:w='urn:schemas-microsoft-com:office:word'" + "xmlns='http://www.w3.org/TR/REC-html40'>" + "<head><title>Time</title>");
        //    ////The setting specifies document's view after it is downloaded as Print
        //    ////instead of the default Web Layout
        //    //html.Append("<!--[if gte mso 9]>" + "<xml>" + "<w:WordDocument>" + "<w:View>Print</w:View>" + "<w:Zoom>90</w:Zoom>" + "<w:DoNotOptimizeForBrowser/>" + "</w:WordDocument>" + "</xml>" + "<![endif]-->");
        //    //html.Append("<style>" + "<!-- /* Style Definitions */" + "@page Section1" + "   {size:8.5in 11.0in; " + "   margin:0.2in 0.2in 0.2in 0.2in ; " + "   mso-header-margin:.5in; " + "   mso-footer-margin:.5in; mso-paper-source:0;}" + " div.Section1" + "   {page:Section1;}" + "-->" + "</style></head>");
        //    //html.Append("<body lang=EN-US style='tab-interval:.5in'>");
        //    html.Append(getHeader().ToString());
        //    html.Append(getBody().ToString());
        //    html.Append(getFooter().ToString());
        //    //html.Append("</body>");
        //    //html.Append("</html>");
        //    return html.ToString();
        //}


        public void download(HttpContext p, string _filename)
        {

            string filename = _filename + ".docx";
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //string html = File.ReadAllText(Server.MapPath("~/Template.html"));

            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    HtmlConverter converter = new HtmlConverter(mainPart);

                    //setting the page margins go here
                    //DocumentFormat.OpenXml.Wordprocessing.PageMargin pageMargins = new DocumentFormat.OpenXml.Wordprocessing.PageMargin();
                    //pageMargins.Left = 600;
                    //pageMargins.Right = 600;
                    //pageMargins.Bottom = 500;
                    //pageMargins.Top = 500;
                    ////pageMargins.Header = 1500; //not needed for now
                    ////pageMargins.Footer = 1500; //not needed for now

                    ////Important needed to access properties (sections) to set values for all elements.
                    //DocumentFormat.OpenXml.Wordprocessing.SectionProperties sectionProps = new DocumentFormat.OpenXml.Wordprocessing.SectionProperties();
                    //sectionProps.Append(pageMargins);


                    Body body = mainPart.Document.Body;
                    // ------------------ PAGE 1 ------------------
                    var page1 = converter.Parse(Page1());
                    for (int i = 0; i < page1.Count; i++)
                    {
                        body.Append(page1[i]);
                    }
                    // ------------------ PAGE 2 ------------------
                    var page2 = converter.Parse(Page2());
                    for (int i = 0; i < page2.Count; i++)
                    {
                        body.Append(page2[i]);
                    }
                    mainPart.Document.Save();
                }

                byte[] bytesInStream = generatedDocument.ToArray(); // simpler way of converting to array
                generatedDocument.Close();

                p.Response.Clear();
                p.Response.ContentType = contentType;
                p.Response.AddHeader("content-disposition", "attachment;filename=" + filename);

                //this will generate problems
                p.Response.BinaryWrite(bytesInStream);
                try
                {
                    p.Response.End();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Response.End(); generates an exception. if you don't use it, you get some errors when Word opens the file...
                }

            }

            //lblError.Visible = false;
            //lblFeedback.Visible = true;

        }

    }
}




//System.Text.StringBuilder strBody = new System.Text.StringBuilder("");

//strBody.Append("<html " + "xmlns:o='urn:schemas-microsoft-com:office:office' " + "xmlns:w='urn:schemas-microsoft-com:office:word'" + "xmlns='http://www.w3.org/TR/REC-html40'>" + "<head><title>Time</title>");

//                //The setting specifies document's view after it is downloaded as Print
//                //instead of the default Web Layout
//                strBody.Append("<!--[if gte mso 9]>" + "<xml>" + "<w:WordDocument>" + "<w:View>Print</w:View>" + "<w:Zoom>90</w:Zoom>" + "<w:DoNotOptimizeForBrowser/>" + "</w:WordDocument>" + "</xml>" + "<![endif]-->");

//                strBody.Append("<style>" + "<!-- /* Style Definitions */" + "@page Section1" + "   {size:8.5in 11.0in; " + "   margin:0.2in 0.2in 0.2in 0.2in ; " + "   mso-header-margin:.5in; " + "   mso-footer-margin:.5in; mso-paper-source:0;}" + " div.Section1" + "   {page:Section1;}" + "-->" + "</style></head>");

//                strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" + "<div class=Section1>" + "<h1>Time and tide wait for none</h1>" + "<p style='color:red'><I>" + DateTime.Now + "</I></p>" + "</div></body></html>");

//                //Force this content to be downloaded 
//                //as a Word document with the name of your choice
//                Response.AppendHeader("Content-Type", "application/msword");
//                Response.AppendHeader("Content-disposition", "attachment; filename=myword.doc");
//                Response.Write(strBody);
