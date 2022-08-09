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
    public class BookingConcert
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Con"].ToString());
        public static DataSet Booking_insert(Concert bookreg)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into TConcert" +
                $" Values(" +
                $"'{bookreg.Concert_Content}'," +
                $"'{bookreg.Concert_location}'," +
                $"'{bookreg.Concert_Start_Date.ToString("yyyy-MM-dd")}'," +
                $"'{bookreg.Concert_End_Date.ToString("yyyy-MM-dd")}'," +
                $"'{bookreg.Concert_Link}'," +
                $"{bookreg.Max_Seat}," +
                $"'{bookreg.Concert_Period.ToString("yyyy-MM-dd")}'," +
                $"'{bookreg.Concert_Artist}'); ";
            
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter Dadap = new SqlDataAdapter();
            Dadap.SelectCommand = cmd;

            DataSet ds = new DataSet();

            Dadap.Fill(ds, "[daeho].[dbo].[TConcert]");
            con.Close();
            return ds;
        }
        public static DataSet Booking_Read()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT * FROM CONCERT_READ";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter Dadap = new SqlDataAdapter();
            Dadap.SelectCommand = cmd;

            DataSet ds = new DataSet();


            Dadap.Fill(ds, "[daeho].[dbo].[CONCERT_READ]");
            con.Close();

            return ds;
        }
        public static DataSet BookingReservaiton(int key)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT Concert_Num,Concert_Artist,Concert_Content, Concert_location, Concert_Start_Date FROM Tconcert where Concert_Num = {key}";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[TConcert]");
            con.Close();

            return ds;
        }
        public static DataSet BookingReservaiton_Read()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT * FROM DETAILS";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[DETAILS]");
            con.Close();

            return ds;
        }
        public static DataSet BookingDetails(int key)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"EXEC P_details_id {key}";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds, "[daeho].[dbo].[P_details_id]");
            con.Close();

            return ds;
        }
    }
}