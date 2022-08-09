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
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Admin_News_Read()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin_News_Register()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Admin_Booking_Read()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin_Concert_Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin_Booking_Reservation_Read()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin_Details()
        {

            return View();
        }
        #region News
        [HttpPost]
        public ActionResult Admin_News_Read([DataSourceRequest] DataSourceRequest request)
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
        [HttpPost]
        public void Delete(NewsModels news)
        {
            int id = news.News_Num;

            DataSet ds = new DataSet();
            ds = Data_Conection.News_DB.Delete_NewsTable(id);
        }
        [HttpPost]
        public ActionResult Admin_News_Register(NewsModels news)
        {
            string id = TempData["ID"].ToString();
            NewsModels newsdata = new NewsModels
            {
                News_Info_Name = id,
                News_Content = news.News_Content,
                News_Period = news.News_Period,
                News_RegistTime = news.News_RegistTime,
                News_Summary = news.News_Summary
            };
            if (news != null)
            {
                DataSet ds = new DataSet();
                ds = Data_Conection.News_DB.Insert_NewsTable(newsdata);
                return RedirectToAction("Admin_News_Read");
            }

            return View();
        }
        #endregion


        #region Booking
        public ActionResult Admin_Booking_reservation(Concert conc)
        {
            if (conc.Concert_Num == 0)
            {
                int key = Int32.Parse(TempData["Key"].ToString());
                DataSet ds = new DataSet();
                ds = BookingConcert.BookingReservaiton(key);
                return View("Admin_Booking_reservation", ds);
            }
            return View();
        }
        public void reservation_Insert(Reservation res)
        {
            DataSet ds = new DataSet();
            ds = ReservationConcert.Insert_Reservation(res);
        }
        [HttpPost]
        public ActionResult reservation_Post(Concert conc)
        {
            int key = conc.Concert_Num;
            TempData["Key"] = conc.Concert_Num;
            DataSet ds = new DataSet();
            ds = BookingConcert.BookingReservaiton(key);
            TempData.Keep();
            return View("Admin_Booking_reservation", ds);
        }


        [HttpPost]
        public ActionResult Admin_Concert_Register(Concert conc)
        {
            string id = TempData["ID"].ToString();
            if (conc == null)
            {
                return View();
            }
            else
            {
                Concert Bookreg = new Concert
                {
                    Concert_Artist = conc.Concert_Artist,
                    Concert_Content = conc.Concert_Content,
                    Concert_location = conc.Concert_location,
                    Concert_Start_Date = conc.Concert_Start_Date,
                    Concert_End_Date = conc.Concert_End_Date,
                    Concert_Link = conc.Concert_Link,
                    Max_Seat = conc.Max_Seat,
                    Concert_Period = conc.Concert_Period
                };
                DataSet ds = new DataSet();
                ds = Data_Conection.BookingConcert.Booking_insert(Bookreg);
                return Json(Bookreg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Admin_Booking_Read([DataSourceRequest] DataSourceRequest request)
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
        [HttpPost]
        public ActionResult Admin_Booking_Reservation_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSet ds = new DataSet();
            ds = Data_Conection.BookingConcert.BookingReservaiton_Read();

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

            var result = Enumerable.Range(0, dsList["[daeho].[dbo].[DETAILS]"].Count).Select(i => new details
            {
                Reservation_Concert_Num = Int32.Parse(dsList["[daeho].[dbo].[DETAILS]"][i]["공연번호"].ToString()),
                공연일자 = DateTime.ParseExact(dsList["[daeho].[dbo].[DETAILS]"][i]["공연일자"].ToString(), "yyyy-MM-dd", null),
                아티스트 = dsList["[daeho].[dbo].[DETAILS]"][i]["아티스트"].ToString(),
                장소 = dsList["[daeho].[dbo].[DETAILS]"][i]["장소"].ToString(),
                좌석수 = Int32.Parse(dsList["[daeho].[dbo].[DETAILS]"][i]["좌석수"].ToString()),
                예약완료 = Int32.Parse(dsList["[daeho].[dbo].[DETAILS]"][i]["예약완료"].ToString())
            });
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Admin_Details([DataSourceRequest] DataSourceRequest request, details det)
        {
            if (det.Reservation_Concert_Num == 0)
            {
                int key = Int32.Parse(TempData["Dkey"].ToString());
            }
            else
            {
                TempData["Dkey"] = det.Reservation_Concert_Num;
            }
            DataSet ds = new DataSet();
            ds = Data_Conection.BookingConcert.BookingDetails(Int32.Parse(TempData["Dkey"].ToString()));

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

            var result = Enumerable.Range(0, dsList["[daeho].[dbo].[P_details_id]"].Count).Select(i => new Details_ID
            {
                ID = Int32.Parse(dsList["[daeho].[dbo].[P_details_id]"][i]["ID"].ToString()),
                Concert_Content = dsList["[daeho].[dbo].[P_details_id]"][i]["Concert_Content"].ToString(),
                아티스트 = dsList["[daeho].[dbo].[P_details_id]"][i]["아티스트"].ToString(),
                예약날짜 = DateTime.ParseExact(dsList["[daeho].[dbo].[P_details_id]"][i]["예약날짜"].ToString(), "yyyy-MM-dd", null),
                예약좌석수 = Int32.Parse(dsList["[daeho].[dbo].[P_details_id]"][i]["예약좌석수"].ToString()),
                표시기한 = DateTime.ParseExact(dsList["[daeho].[dbo].[P_details_id]"][i]["표시기한"].ToString(), "yyyy-MM-dd", null),
                MONTH = Int32.Parse(dsList["[daeho].[dbo].[P_details_id]"][i]["MONTH"].ToString()),
                장소 = dsList["[daeho].[dbo].[P_details_id]"][i]["장소"].ToString(),
                공연일자 = DateTime.ParseExact(dsList["[daeho].[dbo].[P_details_id]"][i]["공연일자"].ToString(), "yyyy-MM-dd", null),
            });
            TempData.Keep();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #endregion




    }
}