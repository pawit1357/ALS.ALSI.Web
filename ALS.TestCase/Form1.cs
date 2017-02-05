using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Windows.Forms;
using Spire.Doc.Formatting;
using System.Collections.Generic;
using ALS.ALSI.Biz.DataAccess;

namespace ALS.TestCase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            ////List<template_wd_mesa_coverpage> coverpages = template_wd_mesa_coverpage.FindAllBySampleID(2726);
            ////List<template_wd_mesa_img> refImg = template_wd_mesa_img.FindAllBySampleID(2726);
            //Create Table
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
            format.FontSize =  15;
            format.Bold = false;
            format.UnderlineStyle = UnderlineStyle.Single;
            CharacterFormat format2 = new CharacterFormat(doc);
            format2.FontName = "Arial";
            format2.FontSize = 11;
            format2.Bold = false;
            format2.UnderlineStyle = UnderlineStyle.Single;
            #endregion

            //Initialize a Header Instance
            HeaderFooter header = doc.Sections[0].HeadersFooters.Header;
            //Add Header Paragraph and Format
            Paragraph paragraph1 = header.AddParagraph();
            paragraph1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
            //Append Picture for Header Paragraph and Format
            DocPicture headerimage = paragraph1.AppendPicture(Image.FromFile(@"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Web\images\images.png"));
            headerimage.VerticalAlignment = ShapeVerticalAlignment.Bottom;



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
                                  new String[]{ "CUSTOMER PO NO.:", ""},
                                  new String[]{ "ALS THAILAND REF NO.:", "ATT/ELP/16/3148-MB"},
                                  new String[]{ "DATE:", "13 January 2017"},
                                  new String[]{ "COMPANY:", "Thai Dai-Ichi Seiko Co., Ltd."},
                                  new String[]{ "", "700/390 Moo.6 T.Donhuaroh, A.Muang Chonburi"},
                                  new String[]{ "","Chonburi 20000 Thailand" },
                                  new String[]{ "","" },
                                  new String[]{ "DATE SAMPLE RECEIVED:", "13 January 2017" },
                                  new String[]{ "DATE ANALYZED:", "13 January 2017" },
                                  new String[]{ "DATE TEST COMPLETED:", "13 January 2017" },
                                  new String[]{ "","" },
                                  new String[]{ "SAMPLE DESCRIPTION:", "One lot of sample was received with references:" },
                                  new String[]{ "","Description: Ramp Shrek (2063-778130-000) M.2" },
                                  new String[]{ "","Lot No.7D2063778130000AASDI0200006Z1400100" },
                                  new String[]{ "","Surface Area: 7.73 sq.cm" },
                              };

            //Add Cells
            table.ResetCells(data.Length, Header.Length);

            //Header Row
            //TableRow FRow = table.Rows[0];
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
            //    TR.CharacterFormat.FontName = "Calibri";
            //    TR.CharacterFormat.FontSize = 14;
            //    TR.CharacterFormat.TextColor = Color.Teal;
            //    TR.CharacterFormat.Bold = true;
            //}

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

            #endregion









            //Save and Launch
            doc.SaveToFile("WordTable.docx");
            System.Diagnostics.Process.Start("WordTable.docx");




            //string[] lines;
            //var list = new List<string>();
            //var fileStream = new FileStream(@"C:\Users\icnsk\Downloads\sky_room_ad\JSON_MUIC_Room_List.txt FileMode.Open, FileAccess.Read);
            //using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            //{
            //    string line;
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        if (line.IndexOf("room_number") != -1)
            //        {
            //            Console.WriteLine(line);
            //            list.Add(line);
            //        }
            //    }
            //}
            //lines = list.ToArray();
            //Document sourceDoc = new Document(@"C:\Users\icnsk\Downloads\ELP-1362A-DB1.doc");
            //Document destinationDoc = new Document(@"C:\Users\icnsk\Downloads\Blank Letter Head - EL.doc");
            //foreach (Section sec in sourceDoc.Sections)
            //{
            //    foreach (DocumentObject obj in sec.Body.ChildObjects)
            //    {
            //        destinationDoc.Sections[0].Body.ChildObjects.Add(obj.Clone());
            //    }
            //}
            //destinationDoc.SaveToFile("target.docx");
            ////System.Diagnostics.Process.Start("target.docx");

            //Document document = new Document();
            //document.LoadFromFile(@"target.docx");

            ////Convert Word to PDF
            //document.SaveToFile("toPDF.PDF FileFormat.PDF);

            ////Launch Document
            //System.Diagnostics.Process.Start("toPDF.PDF");




        }
    }
}

//6272
//E3209
//1312
//3
//E3204
//E3210
//E3212
//E3213
//E3214
//E3211
//P315
//P316"
//P320
//P322
//P318
//1514/1
//1514
//6273
//5208
//5209
//5210
//5211
//5212
//5301
//5307
//5308
//5309
//5310
//5311
//5312
//5313
//1108
//3302
//3303
//3304
//3305
//3316
//3410
//3409
//3411
//5207
//3421
//3422
//3315
//2303
//2308
//2120
//1302
//1306
//1407
//1404
//1405
//1417
//1502
//1503
//1504
//1303
//1304
//1305
//1314
//1315
//1307
//3408
//6275
//1408/1
//SC3
//3504
//3508
//1506
//3502
//3501
//1418
//1419
//1506
//1308
//3308
//1402
//1403
//1406
//6276
//4114
//6277
//Closed
//2306
//2302
//3306
//5206
//3317
//P318*
//P314
//3307
//2207
//3407
//204
//3420
//3415
//1309
//390/1
//1515
//1210
//1318
//1409
//4117
//3415
//contact Division
//P323
//1408
//Closed
//01
//Contact Division
//1516
//4119
//1516/1
//3414
//4111/4
//3501
//1211
//1214
//1211
//3301
//6274
//3412
//Contact BBA   Division 
//4116
//4111/1
//Ground Floor Zone C
//3507
//p317
//2307
//Ground Floor Zone A
//Ground Floor Zone D
//1417 (ICBI261)
//Ground Floor Zone B
//1512
//1513


