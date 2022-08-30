using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SalesAdministration.DAL
{
    class DataAccessLayer
    {
        SqlConnection SqlConnection;

        public DataAccessLayer()
        {
            //  SqlConnection = new SqlConnection(@"Data Source=RAGAB\SQLEXPRESS01;Initial Catalog=Product_DB;Integrated Security=True");
            string mode = Properties.Settings.Default.Mode;
            if (mode == "SQL")
            {
                SqlConnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" +
                                                    Properties.Settings.Default.Database + "; Integrated Security=false; User ID=" +
                                                    Properties.Settings.Default.ID + "; Password=" + Properties.Settings.Default.Password + "");
            }
            else
            {
                SqlConnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" + Properties.Settings.Default.Database + "; Integrated Security=true");
            }
        }
        //method to open the connections
        public void open()
        {
            if (SqlConnection.State != ConnectionState.Open)
            {
                SqlConnection.Open();
            }
        }
        //method to close the connections
        public void close()
        {
            if (SqlConnection.State == ConnectionState.Open)
            {
                SqlConnection.Close();
            }
        }
        //method to read data from database
        public DataTable Selectdata(String stored_procedure, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = stored_procedure;
            cmd.Connection = SqlConnection;

            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //method to insert, update , delete
        public void ExecuteCommand(String stored_procedure, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = stored_procedure;
            cmd.Connection = SqlConnection;


            if (param != null)
            {
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                }
                cmd.ExecuteNonQuery();
            }
        }
    }
}
