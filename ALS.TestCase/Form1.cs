using Spire.Doc;
using System;
using System.Windows.Forms;

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

            Document sourceDoc = new Document(@"C:\Users\icnsk\Downloads\ELP-1362A-DB1.doc");
            Document destinationDoc = new Document(@"C:\Users\icnsk\Downloads\Blank Letter Head - EL.doc");
            foreach (Section sec in sourceDoc.Sections)
            {
                foreach (DocumentObject obj in sec.Body.ChildObjects)
                {
                    destinationDoc.Sections[0].Body.ChildObjects.Add(obj.Clone());
                }
            }
            destinationDoc.SaveToFile("target.docx");
            //System.Diagnostics.Process.Start("target.docx");

            Document document = new Document();
            document.LoadFromFile(@"target.docx");

            //Convert Word to PDF
            document.SaveToFile("toPDF.PDF", FileFormat.PDF);

            //Launch Document
            System.Diagnostics.Process.Start("toPDF.PDF");





            ////Load Document1 and Document2
            //Document DocOne = new Document();
            //DocOne.LoadFromFile(@"C:\Users\icnsk\Downloads\ELP-1362A-DB1.doc", FileFormat.Doc);
            //Document DocTwo = new Document();
            //DocTwo.LoadFromFile(@"C:\Users\icnsk\Downloads\Blank Letter Head - EL.doc", FileFormat.Doc);

            ////Merge 
            //foreach (Section sec in DocOne.Sections)
            //{
            //    DocTwo.Sections.Add(sec.Clone());
            //}

            ////Save and Launch
            //DocOne.SaveToFile("Merge.docx", FileFormat.Docx);
            //System.Diagnostics.Process.Start("Merge.docx");


            ////Load Document
            //Document document = new Document();
            //document.LoadFromFile(@"C:\Users\icnsk\Downloads\ELP-1362A-DB1.doc");

            ////Initialize a Header Instance
            //HeaderFooter header = document.Sections[0].HeadersFooters.Header;
            ////Add Header Paragraph and Format 
            //Paragraph paragraph = header.AddParagraph();
            //paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
            ////Append Picture for Header Paragraph and Format
            //DocPicture headerimage = paragraph.AppendPicture(Image.FromFile(@"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Web\images\images.png"));
            //headerimage.VerticalAlignment = ShapeVerticalAlignment.Bottom;

            ////Initialize a Footer Instance
            //HeaderFooter footer = document.Sections[0].HeadersFooters.Footer;
            ////Add Footer Paragraph and Format
            //Paragraph paragraph2 = footer.AddParagraph();
            //paragraph2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            ////Append Picture and Text for Footer Paragraph
            //DocPicture footerimage = paragraph2.AppendPicture(Image.FromFile(@"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Web\images\images.png"));
            //TextRange TR = paragraph2.AppendText("Copyright © 2013 e-iceblue. All Rights Reserved.");
            //TR.CharacterFormat.FontName = "Arial";
            //TR.CharacterFormat.FontSize = 10;
            //TR.CharacterFormat.TextColor = Color.Black;

            ////Save and Launch
            //document.SaveToFile("ImageHeaderFooter.docx", FileFormat.Docx);
            //System.Diagnostics.Process.Start("ImageHeaderFooter.docx");


        }
        }
    }
