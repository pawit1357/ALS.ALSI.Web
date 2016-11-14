
using System;
using Word = Microsoft.Office.Interop.Word;

namespace ALS.ALSI.Utils
{
    public class MsWord
    {
        /// <summary>
        /// This is the default Word Document Template file
        /// </summary>
        private const string defaultWordDocumentTemplate = @"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Utils\BlankLetterHeadEL.dotx";
        //private const string defaultWordDocumentTemplate = @"C:\Users\icnsk\Downloads\ELP-1362A-DB.doc";


        /// <summary>
        /// A function that merges Microsoft Word Documents that uses the default template
        /// </summary>
        /// <param name="filesToMerge">An array of files that we want to merge</param>
        /// <param name="outputFilename">The filename of the merged document</param>
        /// <param name="insertPageBreaks">Set to true if you want to have page breaks inserted after each document</param>
        public static void Merge(string[] filesToMerge, string outputFilename, bool insertPageBreaks)
        {
            Merge(filesToMerge, outputFilename, insertPageBreaks, defaultWordDocumentTemplate);
        }

        /// <summary>
        /// A function that merges Microsoft Word Documents that uses a template specified by the user
        /// </summary>
        /// <param name="filesToMerge">An array of files that we want to merge</param>
        /// <param name="outputFilename">The filename of the merged document</param>
        /// <param name="insertPageBreaks">Set to true if you want to have page breaks inserted after each document</param>
        /// <param name="documentTemplate">The word document you want to use to serve as the template</param>
        public static void Merge(string[] filesToMerge, string outputFilename, bool insertPageBreaks, string documentTemplate)
        {
            object defaultTemplate = documentTemplate;
            object missing = System.Type.Missing;
            object pageBreak = Word.WdBreakType.wdPageBreak;
            object outputFile = outputFilename;

            // Create  a new Word application
            Word._Application wordApplication = new Word.Application();

            try
            {
                // Create a new file based on our template
                Word._Document wordDocument = wordApplication.Documents.Add(
                                              ref defaultTemplate
                                            , ref missing
                                            , ref missing
                                            , ref missing);

                // Make a Word selection object.
                Word.Selection selection = wordApplication.Selection;

                // Loop thru each of the Word documents
                foreach (string file in filesToMerge)
                {
                    // Insert the files to our template
                    selection.InsertFile(
                                                file
                                            , ref missing
                                            , ref missing
                                            , ref missing
                                            , ref missing);

                    //Do we want page breaks added after each documents?
                    if (insertPageBreaks)
                    {
                        selection.InsertBreak(ref pageBreak);
                    }
                }

                // Save the document to it's output file.
                wordDocument.SaveAs(
                                ref outputFile
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing
                            , ref missing);

                // Clean up!
                wordDocument = null;
            }
            catch (Exception ex)
            {
                //I didn't include a default error handler so i'm just throwing the error
                throw ex;
            }
            finally
            {
                // Finally, Close our Word application
                wordApplication.Quit(ref missing, ref missing, ref missing);
            }
        }

        public static void AddPageHeaderFooter(object fileName)
        {
            object missing = System.Reflection.Missing.Value;
            // Create an object for filename, which is the file to be opened
            //object fileName =@”C:\MySecond.doc”;
            // Create an object of application class
            Microsoft.Office.Interop.Word._Application WordApp = new Word.Application();
            // open the document specified in the fileName variable
            Word.Document adoc = WordApp.Documents.Open(ref fileName, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);
            // We can insert the picture using Range objects AddPicture method
            // To insert a picture at a particular location in the word document
            // insert a table over there and then refer that location through range object
            Word.Range rngPic = adoc.Tables[1].Range;
            // we can even select a particular cell in the table
            //Range rngPic = rng.Tables[1].Cell(2, 3).Range;
            rngPic.InlineShapes.AddPicture(@"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Web\images\images.png", ref missing, ref missing, ref missing);
            WordApp.Visible = false;

  
        }

        //public static void InsertPageHeader()
        //{
        //    string strDocName = @"C:\Users\icnsk\Downloads\ELP-1362A-DB.doc";
        //    object missing = System.Type.Missing;
        //    //object pageBreak = Word.WdBreakType.wdPageBreak;
        //    string outputFile = "C:\\Users\\icnsk\\Downloads\\mygod" + DateTime.Now.ToString("yyyyMMddHHmm") + ".doc";


        //    // first we are creating application of word.
        //    Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
        //    // now creating new document.
        //    WordApp.Documents.Add();
        //    // see word file behind your program
        //    WordApp.Visible = true;
        //    // get the reference of active document
        //    Microsoft.Office.Interop.Word.Document doc = WordApp.ActiveDocument;

        //            // now add the picture in active document reference
        //            doc.InlineShapes.AddPicture(strDocName, Type.Missing, Type.Missing, Type.Missing);

        //    // file is saved.
        //    doc.SaveAs(outputFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    // application is now quit.
        //    WordApp.Quit(Type.Missing, Type.Missing, Type.Missing);

        //}
    }
}