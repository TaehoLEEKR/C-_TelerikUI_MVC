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
    public class ReservationConcert

    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Con"].ToString());

        public static DataSet Insert_Reservation(Reservation res)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"EXEC Insert_Reservation '{res.Reservation_Concert_Artist}','{res.Reservation_Date.ToString("yyyy-MM-dd")}',{res.Reservation_Seat},{res.Reservation_Concert_Num}";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter Dadap = new SqlDataAdapter();
            Dadap.SelectCommand = cmd;

            DataSet ds = new DataSet();


            Dadap.Fill(ds, "[daeho].[dbo].[CONCERT_READ]");
            con.Close();

            return ds;
        }

    }
}