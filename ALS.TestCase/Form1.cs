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
            String[] fileToMerge = new string[1];
            fileToMerge[0] = "C:\\Users\\icnsk\\Downloads\\ELP-1249-DB (1).doc";
            MsWord.Merge(fileToMerge, "C:\\Users\\icnsk\\Downloads\\testhui.doc", false);

        }
    }
}
