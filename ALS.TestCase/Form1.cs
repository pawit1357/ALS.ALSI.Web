using ALS.ALSI.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //String[] fileToMerge = new string[1];
            ////fileToMerge[0] = @"C:\Users\icnsk\Documents\Visual Studio 2015\Projects\ALS.ALSI.Web\ALS.ALSI.Utils\BlankLetterHeadEL.dotx";
            //fileToMerge[0] = @"C:\Users\icnsk\Downloads\ELP-1362A-DB.doc";
            //MsWord.Merge(fileToMerge, "C:\\Users\\icnsk\\Downloads\\testhui"+DateTime.Now.ToString("yyyyMMddHHmm")+".doc", false);
            MsWord.AddPageHeaderFooter(@"C:\Users\icnsk\Downloads\Doc1.docx");
            System.Windows.Forms.MessageBox.Show("Finish");

        }
    }
}
