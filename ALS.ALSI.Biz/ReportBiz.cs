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
            table.ResetCells(data.Length, 2);

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
            table = s.AddTable(true);
            Paragraph pBlank = s.AddParagraph();
            pBlank.AppendText("");
            Paragraph pMethodProcedure = s.AddParagraph();
            pMethodProcedure.AppendText("METHOD/PROCEDURE:").ApplyCharacterFormat(format2);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////







            //Create Header and Data
            //String[] Header1 = { "Item", "Description", "Qty", "Unit Price", "Price" };
            //String[][] data1 = {
            //                      new String[]{ "Spire.Doc for .NET",".NET Word Component","1","$799.00","$799.00"},
            //                      new String[]{"Spire.XLS for .NET",".NET Excel Component","2","$799.00","$1,598.00"},
            //                      new String[]{"Spire.Office for .NET",".NET Office Component","1","$1,899.00","$1,899.00"},
            //                      new String[]{"Spire.PDF for .NET",".NET PDFComponent","2","$599.00","$1,198.00"},
            //                  };
            ////Add Cells
            //tableMethodProcedure.ResetCells(data1.Length + 1, Header1.Length);

            ////Header Row
            //TableRow FRow = table.Rows[0];
            //FRow.IsHeader = true;
            ////Row Height
            //FRow.Height = 23;
            ////Header Format
            //FRow.RowFormat.BackColor = Color.AliceBlue;
            //for (int i = 0; i < Header1.Length; i++)
            //{
            //    //Cell Alignment
            //    Paragraph p = FRow.Cells[i].AddParagraph();
            //    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //    p.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //    //Data Format
            //    TextRange TR = p.AppendText(Header1[i]);
            //    TR.CharacterFormat.FontName = "Calibri";
            //    TR.CharacterFormat.FontSize = 14;
            //    TR.CharacterFormat.TextColor = Color.Teal;
            //    TR.CharacterFormat.Bold = true;
            //}

            ////Data Row
            //for (int r = 0; r < data1.Length; r++)
            //{
            //    TableRow DataRow = table.Rows[r + 1];

            //    //Row Height
            //    DataRow.Height = 20;

            //    //C Represents Column.
            //    for (int c = 0; c < data1[r].Length; c++)
            //    {
            //        //Cell Alignment
            //        DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //        //Fill Data in Rows
            //        Paragraph p2 = DataRow.Cells[c].AddParagraph();
            //        TextRange TR2 = p2.AppendText(data1[r][c]);
            //        //Format Cells
            //        p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //        TR2.CharacterFormat.FontName = "Calibri";
            //        TR2.CharacterFormat.FontSize = 12;
            //        TR2.CharacterFormat.TextColor = Color.Brown;
            //    }
            //}



            //Create Header and Data
            //wd_dhs
            //String[] HeaderMethodProcedure = { "Test", "Procedure No", "Number of piecesused for extraction", "ExtractionMedium", "Extraction Volume" };
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


            Document doc1 = new Document();
            doc1.LoadFromFile(@"D:\Work\Outsource\ALS.ALSI.Web\ALS.ALSI.Web\template\Blank Letter Head - EL.doc");
            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
            foreach (Section section in doc.Sections)
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
            doc.SaveToFile(@"D:\" + jobSample.job_number + ".doc");


        }
    }
}
