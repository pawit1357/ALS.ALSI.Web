using ALS.ALSI.Biz;
using System;
using System.Windows.Forms;

namespace ALS.ALIS.UnitTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateHtmlBiz.test();

        }
    }
}
