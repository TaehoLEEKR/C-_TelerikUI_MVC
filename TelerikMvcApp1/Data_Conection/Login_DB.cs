using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Data_Conection
{
    public class Login_DB
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Con"].ToString());

        public static DataSet Login(Information info)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"EXEC P_Login '{info.Info_name}', '{info.Info_pw}'");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[P_Login]");
            con.Close();

            return ds;
        }
    }
}