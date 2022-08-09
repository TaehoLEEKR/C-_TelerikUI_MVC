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
    public class Information_DB
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Con"].ToString());

        public static DataSet Insert_Information(Information infodata)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"EXEC P_Information '{infodata.Info_name}','{infodata.Info_pw}','{infodata.Info_email}','{infodata.Info_adr}','{infodata.Info_year}','{infodata.Auth}'");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[P_Information]");
            con.Close();

            return ds;
        }

        public static DataSet Delete_Information(Information infodata)
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"DELETE FROM Information where Info_name = '{infodata.Info_name}' and Info_pw ='{infodata.Info_pw}'");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd; 

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[Information]");
            con.Close();

            return ds;
        }
    }
}