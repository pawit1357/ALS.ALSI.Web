using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace ALS.ALSI.Biz
{
    public class ReportBiz
    {
        public static void ReportMesa(job_sample jobSample)
        {
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(jobSample);

            //List<template_wd_mesa_img> imgList = this.refImg.OrderBy(x => x.area).OrderBy(x => x.descripton).ToList();
            //List<template_wd_mesa_img> tmpImg1 = new List<template_wd_mesa_img>();

            Document doc = new Document();
            Section s = doc.AddSection();

            #region "PAGE SETUP"
            s.PageSetup.PageSize = PageSize.A4;
            s.PageSetup.Orientation = PageOrientation.Portrait;
            s.PageSetup.Margins.Top = 100.0f;
            s.PageSetup.Margins.Bottom = 72.0f;
            //s.PageSetup.Margins.Left = 89.85f;
            //s.PageSetup.Margins.Right = 89.85f;
            #endregion

            #region "FONT FORMAT"
            CharacterFormat format = new CharacterFormat(doc);
            format.FontName = "Arial";
            format.FontSize = 15;
            format.Bold = false;
            format.UnderlineStyle = UnderlineStyle.Single;
            CharacterFormat format2 = new CharacterFormat(doc);
            format2.FontName = "Arial";
            format2.FontSize = 11;
            format2.Bold = false;
            format2.UnderlineStyle = UnderlineStyle.Single;
            #endregion

            #region "PAGE TITLE"
            Paragraph paragraph = s.AddParagraph();
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            paragraph.AppendText("REPORT").ApplyCharacterFormat(format);
            Paragraph paragraph2 = s.AddParagraph();
            paragraph2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            paragraph2.AppendText("");
            Paragraph paragraph3 = s.AddParagraph();
            paragraph3.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            paragraph3.AppendText("");
            #endregion

            #region "Information"
            Table table = s.AddTable(true);
            //Create Header and Data
            String[] Header = { "", "", };
            String[][] data = {
                                  new String[]{ "CUSTOMER PO NO.:", reportHeader.cusRefNo},
                                  new String[]{ "ALS THAILAND REF NO.:", reportHeader.alsRefNo},
                                  new String[]{ "DATE:", reportHeader.cur_date.ToString("dd MMMM yyyy")},
                                  new String[]{ "COMPANY:", reportHeader.addr1},
                                  new String[]{ "", reportHeader.addr2},
                                  new String[]{ "",""},
                                  new String[]{ "DATE SAMPLE RECEIVED:", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") },
                                  new String[]{ "DATE ANALYZED:", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") },
                                  new String[]{ "DATE TEST COMPLETED:", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") },
                                  new String[]{ "","" },
                                  new String[]{ "SAMPLE DESCRIPTION:", "One lot of sample was received with references:" },
                                  new String[]{ "", reportHeader.description },
                              };

            //Add Cells
            table.ResetCells(data.Length, Header.Length);

            //Data Row
            for (int r = 0; r < data.Length; r++)
            {
                TableRow DataRow = table.Rows[r];

                //Row Height
                DataRow.Height = 11;
                DataRow.Cells[0].SetCellWidth(30, CellWidthType.Percentage);
                DataRow.Cells[1].SetCellWidth(70, CellWidthType.Percentage);
                //C Represents Column.
                for (int c = 0; c < data[r].Length; c++)
                {
                    //Cell Alignment
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //Fill Data in Rows
                    Paragraph p2 = DataRow.Cells[c].AddParagraph();
                    TextRange TR2 = p2.AppendText(data[r][c]);
                    //Format Cells
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                    TR2.CharacterFormat.FontName = "Arial";
                    TR2.CharacterFormat.FontSize = 11;
                    TR2.CharacterFormat.TextColor = Color.Black;
                    DataRow.Cells[c].CellFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
                    DataRow.Cells[c].CellFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
                    DataRow.Cells[c].CellFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
                    DataRow.Cells[c].CellFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
                }
            }

            table.TableFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
            table.TableFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
            table.TableFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
            table.TableFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
            #endregion

            #region "Method/Procedure"
            Paragraph pBlank = s.AddParagraph();
            pBlank.AppendText("");
            Paragraph pMethodProcedure = s.AddParagraph();
            pMethodProcedure.AppendText("METHOD/PROCEDURE:").ApplyCharacterFormat(format2);
            Table tableMethodProcedure = s.AddTable(true);
            //Create Header and Data

            //String[] HeaderMethodProcedure = { "Test", "Procedure No", "Sample Size", "Oven Condition" };
            //String[][] dataMethodProcedure = {
            //                      new String[]{ "","","",""},
            //                  };

            ////Add Cells
            //tableMethodProcedure.ResetCells(HeaderMethodProcedure.Length, dataMethodProcedure.Length);
            ////Header Row
            //TableRow FRow = tableMethodProcedure.Rows[0];
            //FRow.IsHeader = false;
            ////Row Height
            //FRow.Height = 23;
            ////Header Format
            //FRow.RowFormat.BackColor = Color.AliceBlue;
            //for (int i = 0; i < Header.Length; i++)
            //{
            //    //Cell Alignment
            //    Paragraph p = FRow.Cells[i].AddParagraph();
            //    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //    //Data Format
            //    TextRange TR = p.AppendText(Header[i]);
            //    TR.CharacterFormat.FontName = "Arial";
            //    TR.CharacterFormat.FontSize = 11;
            //    TR.CharacterFormat.TextColor = Color.Gray;
            //    TR.CharacterFormat.Bold = true;
            //}
            ////Data Row
            //for (int r = 0; r < data.Length; r++)
            //{
            //    TableRow DataRow = table.Rows[r];

            //    //Row Height
            //    DataRow.Height = 11;
            //    DataRow.Cells[0].SetCellWidth(30, CellWidthType.Percentage);
            //    DataRow.Cells[1].SetCellWidth(70, CellWidthType.Percentage);
            //    //C Represents Column.
            //    for (int c = 0; c < data[r].Length; c++)
            //    {
            //        //Cell Alignment
            //        DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //        //Fill Data in Rows
            //        Paragraph p2 = DataRow.Cells[c].AddParagraph();
            //        TextRange TR2 = p2.AppendText(data[r][c]);
            //        //Format Cells
            //        p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            //        TR2.CharacterFormat.FontName = "Arial";
            //        TR2.CharacterFormat.FontSize = 11;
            //        TR2.CharacterFormat.TextColor = Color.Black;
            //        DataRow.Cells[c].CellFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //        DataRow.Cells[c].CellFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //        DataRow.Cells[c].CellFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //        DataRow.Cells[c].CellFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //    }
            //}

            //table.TableFormat.Borders.Bottom.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //table.TableFormat.Borders.Top.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //table.TableFormat.Borders.Left.BorderType = Spire.Doc.Documents.BorderStyle.None;
            //table.TableFormat.Borders.Right.BorderType = Spire.Doc.Documents.BorderStyle.None;
            #endregion


            //Save and Launch
            doc.SaveToFile(@"E:\ALIS\Template\" + jobSample.job_number + "_test.doc");

            Document destinationDoc = new Document(@"E:\ALIS\Template\Blank Letter Head - EL.doc");
            foreach (Section sec in doc.Sections)
            {
                foreach (DocumentObject obj in sec.Body.ChildObjects)
                {
                    destinationDoc.Sections[0].Body.ChildObjects.Add(obj.Clone());
                }
            }
            destinationDoc.SaveToFile(@"E:\ALIS\Template\"+jobSample.job_number+".doc");
            //destinationDoc.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);
        }
    }
}
