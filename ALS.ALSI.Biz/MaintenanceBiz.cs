using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
                    MySqlCommand cmd = new MySqlCommand
                    {

                        //Assign the query using CommandText
                        CommandText = sql,
                        //Assign the connection using Connection
                        Connection = connection
                    };

                    //Execute query
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public static DataSet ExecuteReturnDs(String sql)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlCon"].ToString()))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(ds);
            }
            //Execute query
            return ds;
        }
        public static DataTable ExecuteReturnDt(String sql)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlCon"].ToString()))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(dt);
            }
            //Execute query
            return dt;
        }

        public static String GenScript()
        {

            List<String> sqlList = new List<string>();
            Hashtable hashtable = new Hashtable();
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlCon"].ToString()))
            {
                connection.Open();

                List<String> sqlSelectTables = Table();

                foreach (String sql in sqlSelectTables)
                {
                    try
                    {


                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        String tableName = sql.Split(' ')[3].Replace(';', ' ').Trim();

                        String sqlSchema = "SELECT COLUMN_NAME AS ColumnName, DATA_TYPE AS DataType FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'";
                        MySqlCommand cmdSchema = new MySqlCommand(sqlSchema, connection);
                        MySqlDataAdapter daSchema = new MySqlDataAdapter(cmdSchema);
                        DataTable dtSchema = new DataTable();
                        daSchema.Fill(dtSchema);

                        for (int r = 0; r < dtSchema.Rows.Count; r++)
                        {
                            hashtable.Add(tableName + "|" + dtSchema.Rows[r][0].ToString(), dtSchema.Rows[r][1].ToString());
                        }


                        StringBuilder appendSQLFont = new StringBuilder();


                        //int col = 0;
                        //foreach (DataColumn column in dt.Columns)
                        //{
                        //    if (col > 0) appendSQLFont.Append(column.Caption + ",");
                        //    col++;
                        //}
                        //appendSQLFont.Remove(appendSQLFont.Length - 1, 1);
                        //appendSQLFont.Append(")");


                        StringBuilder appendSQlValue = new StringBuilder();
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            appendSQlValue.Append(" (");
                            for (int c = 0; c < dt.Rows[r].ItemArray.Length; c++)
                            {
                                //if (c > 0)
                                //{
                                String colName = dt.Columns[c].Caption;

                                String colType = hashtable[tableName + "|" + colName].ToString();
                                String value = dt.Rows[r][c].ToString();
                                switch (colType)
                                {
                                    case "double":
                                    case "int":
                                        if (String.IsNullOrEmpty(value))
                                        {
                                            appendSQlValue.Append("0");
                                        }
                                        else {
                                            appendSQlValue.Append(value);
                                        }
                                        break;
                                    case "date":
                                        if (String.IsNullOrEmpty(value))
                                        {
                                            appendSQlValue.Append("'" + DateTime.MinValue.ToString("yyyy-MM-dd") + "'");
                                        }
                                        else {
                                            appendSQlValue.Append("'" + Convert.ToDateTime(value).ToString("yyyy-MM-dd") + "'");
                                        }
                                        break;
                                    case "varchar":
                                        appendSQlValue.Append("'" + value.Replace("'", " ").Trim() + "'");
                                        break;
                                    default:
                                        appendSQlValue.Append("''");
                                        break;
                                }
                                appendSQlValue.Append(",");
                                //}

                            }
                            appendSQlValue.Remove(appendSQlValue.Length - 1, 1);
                            appendSQlValue.Append("),");
                        }

                        appendSQLFont.Append("insert into " + tableName + " values ");
                        appendSQLFont.Append(appendSQlValue.ToString());
                        appendSQLFont.Remove(appendSQLFont.Length - 1, 1);
                        sqlList.Add(appendSQLFont.ToString());

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            Console.WriteLine();

            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String saveFolder = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd,"Script","");
            String saveFile = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd,"Script", "script_alis_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql");

            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFile))
            {
                foreach (String line in sqlList)
                {
       
                        file.WriteLine(line);
                    
                }
            }



            return saveFile;
        }


        private static List<String> Table()
        {
            List<String> tableSql = new List<String>
            {
                "select * from job_info;",
                "select * from job_sample;",
                "select * from m_template;",
                "select * from tb_m_component;",
                "select * from tb_m_detail_spec;",
                "select * from tb_m_detail_spec_ref;",
                "select * from tb_m_specification;",
                "select * from template_seagate_copperwire_coverpage;",
                "select * from template_seagate_copperwire_img;",
                "select * from template_seagate_corrosion_coverpage;",
                "select * from template_seagate_corrosion_img;",
                "select * from template_seagate_dhs_coverpage;",
                "select * from template_seagate_ftir_coverpage;",
                "select * from template_seagate_gcms_coverpage;",
                "select * from template_seagate_gcms_coverpage_img;",
                "select * from template_seagate_hpa_coverpage;",
                "select * from template_seagate_ic_coverpage;",
                "select * from template_seagate_lpc_coverpage;",
                "select * from template_wd_corrosion_coverpage;",
                "select * from template_wd_corrosion_img;",
                "select * from template_wd_dhs_coverpage;",
                "select * from template_wd_ftir_coverpage;",
                "select * from template_wd_ftir_coverpage_tmp;",
                "select * from template_wd_gcms_coverpage;",
                "select * from template_wd_hpa_for1_coverpage;",
                "select * from template_wd_hpa_for3_coverpage;",
                "select * from template_wd_ic_coverpage;",
                "select * from template_wd_ir_coverpage;",
                "select * from template_wd_lpc_coverpage;",
                "select * from template_wd_mesa_coverpage;",
                "select * from template_wd_mesa_img;"
            };


            return tableSql;
        }


    }
}






