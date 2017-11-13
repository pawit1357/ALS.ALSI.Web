using ALS.ALSI.Biz;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button5_Click(object sender, EventArgs e)
        {
            int test = Convert.ToInt32("34843");
            Console.WriteLine();
            //job_sample job = new job_sample();
            //job.SelectByID(5939);

            //ReportBiz reportBiz = new ReportBiz(new System.Web.HttpServerUtility(),job);
                
        }
        
    }
}
