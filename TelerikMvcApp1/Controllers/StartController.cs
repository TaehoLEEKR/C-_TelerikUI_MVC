using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        [HttpGet]
        public ActionResult StartPage()
        {
            return View();
        }
        public ActionResult AdminView()
        {
            return View();
        }
        public ActionResult NormalVieW()
        {
            return View();
        }
        public ActionResult GuestView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StartPage(Information info)
        {
            DataSet ds = new DataSet();
            ds = Data_Conection.Login_DB.Login(info);
            string res = "";
            string auth = "";
            string id = "";
            try
            {
            foreach (DataRow row in ds.Tables["[daeho].[dbo].[P_Login]"].Rows)
            {
                System.Diagnostics.Debug.WriteLine(row["result"]);
                res = row["result"].ToString();
                auth = row["Auth"].ToString();
                    TempData["ID"] = row["Info_name"].ToString();
                    TempData["Auth"] = row["Auth"].ToString();
            }
                if (res == "-1") // 실패
                {
                    return View();
                }
                else if (res == "1" && auth == "Admin") // 어드민
                {
                    return RedirectToAction("AdminView");
                }
                else if (res == "1" && auth == "Normal") // 일반
                {
                    return RedirectToAction("NormalView");
                }
                else // 게스트
                {
                    return RedirectToAction("GuestView");
                }
            }
            catch (ArgumentException e)
            {
                return Content("<script language='javascript' type='text/javascript'>"+
                    "alert('Error Retry');" +
                    "window.location.href = 'http://localhost:64820' " +
                    "</script>");
            }
        }
    }
}

     