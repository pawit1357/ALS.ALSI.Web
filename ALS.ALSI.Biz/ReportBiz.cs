using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;

namespace ALS.ALSI.Biz
{
    public class ReportBiz
    {
        private String fontName = "Arial";
        private int fontSize = 11;
        private Document doc;
        private Section s;
        private CharacterFormat format;
        private CharacterFormat bodyFormat;
        private job_sample jobSample;
        private HttpServerUtility server;

        public ReportBiz(job_sample _jobSample)
        {
            this.doc = new Document();
            this.s = this.doc.AddSection();
            this.jobSample = _jobSample;
            Setup();
        }

        public ReportBiz(HttpServerUtility _server, job_sample _jobSample)
        {
            this.doc = new Document();
            this.s = this.doc.AddSection();
            this.server = _server;
            this.jobSample = _jobSample;
            Setup();
        }

        public void ReportWdDhs1(List<template_wd_dhs_coverpage> listResult)
        {
            //ReportHeader();
            #region "Body"
            s.AddParagraph().AppendText("Results").ApplyCharacterFormat(bodyFormat);
            tb_m_detail_spec _detailSpec = new tb_m_detail_spec().SelectByID(listResult[0].detail_spec_id.Value);
            if (_detailSpec != null)
            {
                s.AddParagraph().AppendText(String.Format("The Specification is based on Western Digital's document no. {0} {1}", _detailSpec.B, _detailSpec.A)).ApplyCharacterFormat(bodyFormat);
            }

            Table tableResult = s.AddTable(true);
            String[] HeaderResult = { "Required Test", "Analytes", "Specification Limits(ng / part)", "Results(ng / part)", "PASS / FAIL" };
            //Add Cells
            tableResult.ResetCells(listResult.Count, HeaderResult.Length);
            //Header Row
            TableRow FRowResult = tableResult.Rows[0];
            FRowResult.IsHeader = true;

            //Row Height
            FRowResult.Height = fontSize;
            //Header Format
            FRowResult.RowFormat.BackColor = Color.LightGray;
            for (int i = 0; i < HeaderResult.Length; i++)
            {
                //Cell Alignment
                Paragraph p = FRowResult.Cells[i].AddParagraph();
                FRowResult.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                //Data Format
                p.AppendText(HeaderResult[i]).ApplyCharacterFormat(bodyFormat);

            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < listResult.Count; i++)
            {
                TableRow DataRow = tableResult.Rows[i];

                Paragraph pRow = DataRow.Cells[0].AddParagraph();
                pRow.AppendText("DHS").ApplyCharacterFormat(bodyFormat);
                pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;


                sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6}\n", i, pRow.IsComposite, pRow.IsDeleteRevision, pRow.IsEndOfDocument, pRow.IsEndOfSection, pRow.IsInCell, pRow.IsInsertRevision));

                pRow = DataRow.Cells[1].AddParagraph();
                pRow.AppendText(listResult[i].analytes).ApplyCharacterFormat(bodyFormat);
                pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;

                pRow = DataRow.Cells[2].AddParagraph();
                pRow.AppendText(listResult[i].specification_limits).ApplyCharacterFormat(bodyFormat);
                pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;

                pRow = DataRow.Cells[3].AddParagraph();
                pRow.AppendText(listResult[i].result).ApplyCharacterFormat(bodyFormat);
                pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;

                pRow = DataRow.Cells[4].AddParagraph();
                pRow.AppendText(listResult[i].result_pass_or_false).ApplyCharacterFormat(bodyFormat);
                pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
            }
            #endregion

            MergeHeaderFooter();

        }



        //private void ReportHeader()
        //{
        //    ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);



        //    #region "PAGE TITLE"
        //    Paragraph paragraph = s.AddParagraph();
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
        //    paragraph.AppendText("REPORT").ApplyCharacterFormat(format);
        //    Paragraph paragraph2 = s.AddParagraph();
        //    paragraph2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
        //    paragraph2.AppendText("");
        //    Paragraph paragraph3 = s.AddParagraph();
        //    paragraph3.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
        //    paragraph3.AppendText("");
        //    #endregion

        //    #region "Information"
        //    Table tableInfo = s.AddTable(true);
        //    //Create Header and Data
        //    String[][] data = {
        //                          new String[]{ "CUSTOMER PO NO.:", reportHeader.cusRefNo},
        //                          new String[]{ "ALS THAILAND REF NO.:", reportHeader.alsRefNo},
        //                          new String[]{ "DATE:", reportHeader.cur_date.ToString("dd MMMM yyyy")},
        //                          new String[]{ "COMPANY:", reportHeader.addr1},
        //                          new String[]{ "", reportHeader.addr2},
        //                          new String[]{ "",""},
        //                          new String[]{ "DATE SAMPLE RECEIVED:", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") },
        //                          new String[]{ "DATE ANALYZED:", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") },
        //                          new String[]{ "DATE TEST COMPLETED:", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") },
        //                          new String[]{ "","" },
        //                          new String[]{ "SAMPLE DESCRIPTION:", "One lot of sample was received with references:" },
        //                          new String[]{ "", reportHeader.description },
        //                      };


        //    //Add Cells
        //    tableInfo.ResetCells(data.Length, 2);

        //    //Data Row
        //    for (int r = 0; r < data.Length; r++)
        //    {
        //        TableRow tr = tableInfo.Rows[r];

        //        //Row Height
        //        tr.Height = fontSize;
        //        tr.Cells[0].SetCellWidth(30, CellWidthType.Percentage);
        //        tr.Cells[1].SetCellWidth(70, CellWidthType.Percentage);
        //        //C Represents Column.
        //        for (int c = 0; c < data[r].Length; c++)
        //        {
        //            //Cell Alignment
        //            tr.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
        //            //Fill Data in Rows
        //            Paragraph p2 = tr.Cells[c].AddParagraph();
        //            p2.AppendText(data[r][c]).ApplyCharacterFormat(bodyFormat); ;
        //            tr.Cells[c].CellFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //            tr.Cells[c].CellFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //            tr.Cells[c].CellFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //            tr.Cells[c].CellFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //        }
        //    }
        //    tableInfo.TableFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //    tableInfo.TableFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //    tableInfo.TableFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //    tableInfo.TableFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //    #endregion

        //    #region "Method/Procedure"
        //    s.AddParagraph().AppendText("");
        //    s.AddParagraph().AppendText("METHOD/PROCEDURE:").ApplyCharacterFormat(bodyFormat);

        //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    Table table = s.AddTable(true);
        //    String[] Header = { "Test", "Procedure No ", "Number of pieces used for extraction ", " Extraction Medium  ", "Extraction Volume" };
        //    List<template_wd_dhs_coverpage> dataList = template_wd_dhs_coverpage.FindAllBySampleID(jobSample.ID);
        //    //Add Cells
        //    table.ResetCells(2, Header.Length);
        //    //Header Row
        //    TableRow FRow = table.Rows[0];
        //    FRow.IsHeader = true;
        //    //Row Height
        //    FRow.Height = fontSize;
        //    //Header Format
        //    FRow.RowFormat.BackColor = Color.LightGray;
        //    for (int i = 0; i < Header.Length; i++)
        //    {
        //        //Cell Alignment
        //        Paragraph p = FRow.Cells[i].AddParagraph();
        //        FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
        //        p.Format.HorizontalAlignment = HorizontalAlignment.Center;
        //        //Data Format
        //        p.AppendText(Header[i]).ApplyCharacterFormat(bodyFormat); ;
        //    }

        //    TableRow DataRow = table.Rows[1];
        //    Paragraph pRow = DataRow.Cells[0].AddParagraph();

        //    pRow.AppendText("DHS").ApplyCharacterFormat(bodyFormat);
        //    pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
        //    pRow = DataRow.Cells[1].AddParagraph();
        //    pRow.AppendText(dataList[0].pm_procedure_no).ApplyCharacterFormat(bodyFormat);
        //    pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
        //    pRow = DataRow.Cells[2].AddParagraph();
        //    pRow.AppendText(dataList[0].pm_number_of_pieces_used_for_extraction).ApplyCharacterFormat(bodyFormat);
        //    pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
        //    pRow = DataRow.Cells[3].AddParagraph();
        //    pRow.AppendText(dataList[0].pm_extraction_medium).ApplyCharacterFormat(bodyFormat);
        //    pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
        //    pRow = DataRow.Cells[4].AddParagraph();
        //    pRow.AppendText(dataList[0].pm_extraction_volume).ApplyCharacterFormat(bodyFormat);
        //    pRow.Format.HorizontalAlignment = HorizontalAlignment.Center;

        //    #endregion








        //}

        private void MergeHeaderFooter()
        {
            Document doc1 = new Document();
            doc1.LoadFromFile(this.server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;


            //Paragraph paraInserted = new Paragraph(this.doc);
            //TextRange textRange1 = paraInserted.AppendText("======================= xxx ============================");
            //textRange1.CharacterFormat.TextColor = Color.Blue;
            //textRange1.CharacterFormat.FontSize = 15;
            //textRange1.CharacterFormat.UnderlineStyle = UnderlineStyle.Dash;

            //this.doc.Sections[0].Paragraphs.Insert(0, paraInserted);
            //this.doc.Sections[0].Paragraphs.Insert(1, paraInserted);
            //document.Sections[0].Paragraphs.Insert(0, paraInserted);

            //            int number_of_pages = this.doc.BuiltinDocumentProperties.PageCount;

            //]            Paragraph paragraph = range.OwnerParagraph;
            //Body body = paragraph.OwnerTextBody;
            //int index = body.ChildObjects.IndexOf(paragraph);


            foreach (Section section in this.doc.Sections)
            {

                foreach (DocumentObject obj in header.ChildObjects)
                {
                    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                }



                //section.Body.AddParagraph().AppendText("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                //foreach (DocumentObject obj in header.ChildObjects)
                //{
                //    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                //}

                foreach (DocumentObject obj in footer.ChildObjects)
                {
                    section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
                }
            }
            this.doc.SaveToFile(this.server.MapPath("~/Report/") + this.jobSample.job_number + ".doc");
        }


        private void Setup()
        {
            #region "PAGE SETUP"
            s.PageSetup.PageSize = PageSize.A4;
            s.PageSetup.Orientation = PageOrientation.Portrait;
            s.PageSetup.Margins.Top = 100.0f;
            s.PageSetup.Margins.Bottom = 72.0f;
            //s.PageSetup.Margins.Left = 89.85f;
            //s.PageSetup.Margins.Right = 89.85f;
            #endregion

            #region "FONT FORMAT"
            format = new CharacterFormat(doc);
            format.FontName = fontName;
            format.FontSize = 15;
            format.Bold = false;
            format.UnderlineStyle = UnderlineStyle.Single;
            bodyFormat = new CharacterFormat(doc);
            bodyFormat.FontName = fontName;
            bodyFormat.FontSize = fontSize;
            bodyFormat.Bold = false;
            //format2.UnderlineStyle = UnderlineStyle.Single;
            #endregion
        }
    }
}
