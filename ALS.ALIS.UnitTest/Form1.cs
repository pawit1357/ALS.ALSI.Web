using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
            List<CSo> csos = new List<CSo>();
            String line;
            try
            {

                //SO,Date,Qty,Unit,ReportNo

                //String[] prefix = { "ELP-", "ELS-", "ELN-", "FA-", "ELWA-", "GRP-", "TRB-"};
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\SO.txt");

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                CSo cso = null;
                int index = 0;
                while (line != null)
                {


                    //write the lie to console window
                    if (line.StartsWith("  SO"))
                    {
                        cso = new CSo { SO = line.Substring(0, 11).Trim(), _Qty = new List<double>(), _UnitPrice = new List<double>(), _ReportNo = new List<string>() };
                        if (index > 0)
                        {
                            csos.Add(cso);
                        }
                        Console.WriteLine(line);
                    }
                    else if (line.Contains("SAMPLE"))
                    {
                        Double qty = Convert.ToDouble(Regex.Replace(line.Substring(50, 15), "[A-Za-z ]", ""));
                        Double unitPrice = Convert.ToDouble(Regex.Replace(line.Substring(65, 15), "[A-Za-z ]", "").Replace(",", "").Trim());
                        cso._Qty.Add(qty);
                        cso._UnitPrice.Add(unitPrice);
                        Console.WriteLine(line);
                    }
                    else if (line.Contains("Report no."))
                    {
                        Console.WriteLine(line);
                        cso._ReportNo.Add(line.Replace("Report no.","").Trim());
                    }
                    else if (line.Contains("ELP-") || line.Contains("ELS-") || line.Contains("ELN-") || line.Contains("FA-") || line.Contains("ELWA-") || line.Contains("GRP-") || line.Contains("TRB-"))
                    {
                        Console.WriteLine(line);
                        cso._ReportNo.Add(line.Replace("Report no.", "").Trim());
                    }

                    //Read the next line
                    line = sr.ReadLine();
                    index++;
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
    //public class CSo
    //{
    //    public String SO { get; set; }
    //    public String PO { get; set; }
    //    public String Date { get; set; }
    //    public List<Double> Qty { get; set; }
    //    public List<Double> UnitPrice { get; set; }
    //    public List<String> ReportNo { get; set; }

    //}
}
