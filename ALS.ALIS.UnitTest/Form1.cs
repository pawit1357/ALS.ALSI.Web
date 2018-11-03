using ALS.ALSI.Biz;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            //GenerateHtmlBiz.test();


            //DateTime _date = new DateTime(2018, 9, 12);//yyyyMMdd
            //holiday_calendar h = new holiday_calendar();
            //DateTime dt = h.GetWorkingDay(_date, 10);
            //Console.WriteLine();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\SO.txt");

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    if (line.Contains("SAMPLE"))
                    {
                        Console.WriteLine(line);

                    }

                    if (line.Contains("Report no."))
                    {
                        Console.WriteLine(line);
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
