using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.ALSI.Biz
{
    public class MaintenanceBiz
    {
        public static Boolean ExecuteCommand(String sql)
        {

            try
            {
                //ConfigurationManager.ConnectionStrings["MySqlCon"];
                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlCon"].ToString()))
                {
                    connection.Open();
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();

                    //Assign the query using CommandText
                    cmd.CommandText = sql;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static DataSet ExecuteReturnDs(String sql)
        {

            try
            {
                //ConfigurationManager.ConnectionStrings["MySqlCon"];
                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlCon"].ToString()))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(sql,connection);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //Execute query
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
