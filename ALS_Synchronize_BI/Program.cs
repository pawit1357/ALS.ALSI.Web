using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ALS_Synchronize_BI
{
    class Program
    {
        // Use class-level Random for best results
        static Random random = new Random();

        // Use class-level HttpClient as a best practice
        static HttpClient client = new HttpClient();
        //static List<SampleRevenue> weatherStation;
        static string powerBiPostUrl = "";


        static void Main(string[] args)
        {

            powerBiPostUrl = ConfigurationManager.AppSettings["powerBiPostUrl"];



            const int duration = 60; // Length of time in minutes to push data
            const int pauseInterval = 2; // Frequency in seconds that data will be pushed
            const string timeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"; // Time format required by Power BI

            //weatherStation = new List<SampleRevenue>();
            //weatherStation.Add(new SampleRevenue { sample_date = "Jan", sample_amount = 500, sample_amount_actual = 450 });
            //weatherStation.Add(new SampleRevenue { sample_date = "Feb", sample_amount = 600, sample_amount_actual = 600 });
            //weatherStation.Add(new SampleRevenue { sample_date = "Mar", sample_amount = 400, sample_amount_actual = 450 });
            //weatherStation.Add(new SampleRevenue { sample_date = "May", sample_amount = 450, sample_amount_actual = 450 });
            //weatherStation.Add(new SampleRevenue { sample_date = "Jun", sample_amount = 600, sample_amount_actual = 600 });
            //weatherStation.Add(new SampleRevenue { sample_date = "Jul", sample_amount = 400, sample_amount_actual = 450 });
            //weatherStation.Add(new SampleRevenue { sample_date = "Aug", sample_amount = 200, sample_amount_actual = 800 });

            GenerateObservations(duration, pauseInterval, timeFormat);


            Console.WriteLine();

        }



        public static void GenerateObservations(int duration, int pauseInterval, string timeFormat)
        {
            DateTime startTime = GetDateTimeUtc();
            DateTime currentTime;
            String sql = "select " +
            " year(sample_invoice_complete_date) as y," +
            " month(sample_invoice_complete_date) as m," +
            " sum(sample_invoice_amount) as revenue " +
            " from alsi.job_sample" +
            " where sample_invoice_status = 2" +
            " group by year(sample_invoice_complete_date),month(sample_invoice_complete_date);";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
            Console.WriteLine();

            SampleRevenue sampleRevenue = new SampleRevenue { y = "2018", m = "Jan", revenue = 450 };

            var jsonString = JsonConvert.SerializeObject(sampleRevenue);

            //string jsonString = JsonConvert.SerializeObject(dt, Formatting.Indented);

            do
            {
                jsonString = "[{\"y\":\"2018\",\"m\":\"Jan\",\"revenue\":1},{\"y\":\"2018\",\"m\":\"Feb\",\"revenue\":11},{\"y\":\"2018\",\"m\":\"Mar\",\"revenue\":111},{\"y\":\"2018\",\"m\":\"May\",\"revenue\":1111}s]";
                //foreach (var station in weatherStation)
                //{
                //    SampleRevenue sampleRevenue = new SampleRevenue
                //    {
                //        sample_date = station.sample_date,
                //        sample_amount = station.sample_amount,
                //        sample_amount_actual = station.sample_amount_actual,
                //    };

                //    var jsonString = JsonConvert.SerializeObject(sampleRevenue);
                //for(int i = 0; i < 10; i++)
                //{
                    var postToPowerBi = HttpPostAsync(powerBiPostUrl, "[" + jsonString + "]"); // Add brackets for Power BI
                    Console.WriteLine(jsonString);
                //}

                //}


                Thread.Sleep(pauseInterval * 1000); // Pause for n seconds. Not highly accurate.
                currentTime = GetDateTimeUtc();
            } while ((currentTime - startTime).TotalMinutes <= duration);
        }
        public class SampleRevenue
        {
            public string y { get; set; }
            public string m { get; set; }
            public int revenue { get; set; }

        }


        static async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            // Construct an HttpContent object from StringContent
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        static DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }


    }
}
