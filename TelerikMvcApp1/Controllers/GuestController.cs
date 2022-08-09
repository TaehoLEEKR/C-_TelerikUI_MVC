using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp1.Data_Conection;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        [HttpGet]
        public ActionResult News_Read()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Booking_Read()
        {
            return View();
        }






        /// <summary>
        /// 게스트 조회 부분
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         
        [HttpPost]
        public ActionResult News_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSet ds = new DataSet();
            ds = Data_Conection.News_DB.Read_NewsTable();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;

            Dictionary<string, List<Dictionary<string, object>>> dsList = new Dictionary<string, List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> rows;
            Dictionary<string, object> row;

            foreach (DataTable dt in ds.Tables)
            {
                rows = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                dsList.Add(dt.TableName, rows);
            }
            var result = Enumerable.Range(0, dsList["[daeho].[dbo].[NEWS_READ]"].Count).Select(i => new NewsModels
            {
                News_Num = Int32.Parse(dsList["[daeho].[dbo].[NEWS_READ]"][i]["News_Num"].ToString()),
                News_RegistTime = DateTime.ParseExact(dsList["[daeho].[dbo].[NEWS_READ]"][i]["등록일자"].ToString(), "yyyy-MM-dd", null),
                News_Info_Name = dsList["[daeho].[dbo].[NEWS_READ]"][i]["등록자"].ToString(),
                News_Content = dsList["[daeho].[dbo].[NEWS_READ]"][i]["상세내역"].ToString(),
                News_Summary = dsList["[daeho].[dbo].[NEWS_READ]"][i]["공연예약"].ToString(),
                News_Period = DateTime.ParseExact(dsList["[daeho].[dbo].[NEWS_READ]"][i]["표시기간"].ToString(), "yyyy-MM-dd", null),//dsList["[daeho].[dbo].[NEWS_READ]"][i]["표시기간"],
            });

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 게스트 공연 조회부분
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>




        [HttpPost]
        public ActionResult Booking_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSet ds = new DataSet();
            ds = Data_Conection.BookingConcert.Booking_Read();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;

            Dictionary<string, List<Dictionary<string, object>>> dsList = new Dictionary<string, List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> rows;
            Dictionary<string, object> row;

            foreach (DataTable dt in ds.Tables)
            {
                rows = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                dsList.Add(dt.TableName, rows);
            }
            //[daeho].[dbo].[CONCERT_READ]
            System.Diagnostics.Debug.WriteLine(serializer.Serialize(dsList));
            System.Diagnostics.Debug.WriteLine(dsList["[daeho].[dbo].[CONCERT_READ]"][0]["공연일자"]);
            System.Diagnostics.Debug.WriteLine(dsList.Count.ToString(), dsList.Keys.Count.ToString(), dsList.Values);
            System.Diagnostics.Debug.WriteLine(dsList["[daeho].[dbo].[CONCERT_READ]"].Count.ToString());

            var result = Enumerable.Range(0, dsList["[daeho].[dbo].[CONCERT_READ]"].Count).Select(i => new Concert
            {
                Concert_Num = Int32.Parse(dsList["[daeho].[dbo].[CONCERT_READ]"][i]["Concert_Num"].ToString()),
                Concert_Period = DateTime.ParseExact(dsList["[daeho].[dbo].[CONCERT_READ]"][i]["공연일자"].ToString(), "yyyy-MM-dd", null),
                Concert_Artist = dsList["[daeho].[dbo].[CONCERT_READ]"][i]["Artist"].ToString(),
                Concert_location = dsList["[daeho].[dbo].[CONCERT_READ]"][i]["장소"].ToString(),
                Concert_Content = dsList["[daeho].[dbo].[CONCERT_READ]"][i]["공연내용"].ToString(),
                Concert_Link = dsList["[daeho].[dbo].[CONCERT_READ]"][i]["링크"].ToString()
            });

            return Json(result.ToDataSourceResult(request));

        }
        /// <summary>
        /// 게스트 조회 삭제
        /// </summary>
        /// <param name="news"></param>
        [HttpPost]
        public void News_Read_Delete(NewsModels news)
        {
            int id = news.News_Num;

            DataSet ds = new DataSet();
            ds = Data_Conection.News_DB.Delete_NewsTable(id);
        }

        /// <summary>
        /// 게스트 공연 예약
        /// </summary>
        /// <param name="conc"></param>
        /// <returns></returns>


        public ActionResult Booking_reservation(Concert conc)
        {
            if (conc.Concert_Num == 0)
            {
                int key = Int32.Parse(TempData["Key"].ToString());
                DataSet ds = new DataSet();
                ds = BookingConcert.BookingReservaiton(key);
                return View("Booking_reservation", ds);
            }
            return View();
        }


        /// <summary>
        /// 게스트 공연 데이터 삽입
        /// </summary>
        /// <param name="res"></param>
        public void Booking_Reservation_Insert(Reservation res)
        {
            DataSet ds = new DataSet();
            ds = ReservationConcert.Insert_Reservation(res);
        }
        /// <summary>
        /// 게스트 공연 예약
        /// </summary>
        /// <param name="conc"></param>
        /// <returns></returns>
        public ActionResult Booking_reservation_Post(Concert conc)
        {
            int key = conc.Concert_Num;
            TempData["Key"] = conc.Concert_Num;
            DataSet ds = new DataSet();
            ds = BookingConcert.BookingReservaiton(key);
            TempData.Keep();
            return View("Booking_reservation", ds);
        }
    }
}