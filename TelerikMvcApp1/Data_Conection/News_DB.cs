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
    public class News_DB
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Con"].ToString());
         public static DataSet Insert_NewsTable(NewsModels newsdata)
         {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"EXEC P_Tnews '','{newsdata.News_Content}','{newsdata.News_Summary}','{newsdata.News_RegistTime.ToString("yyyy-MM-dd")}','{newsdata.News_Period.ToString("yyyy-MM-dd")}','{newsdata.News_Info_Name}'");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet(); 

            da.Fill(ds, "[daeho].[dbo].[P_tnews]");
            con.Close();

            return ds;
         }

        public static DataSet Read_NewsTable()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"SELECT * FROM NEWS_READ");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[NEWS_READ]");
            con.Close();

            return ds;
        }
        public static DataSet Delete_NewsTable(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format($"DELETE FROM Tnews WHERE News_Num = {id}");

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[NEWS_READ]");
            con.Close();

            return ds;
        }

        
    }
}