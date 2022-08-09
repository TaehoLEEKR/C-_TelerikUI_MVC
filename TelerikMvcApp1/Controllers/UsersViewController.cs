using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Controllers
{
    public class UsersViewController : Controller
    {
        // GET: UsersView

        [HttpPost]
        public ActionResult Information(Information info)
        {
            Information infodata = new Information
            {
                Auth = info.Auth,
                Info_name = info.Info_name,
                Info_pw = info.Info_pw,
                Info_email = info.Info_email,
                Info_adr = info.Info_adr,
                Info_year = info.Info_year
            };
            DataSet ds = new DataSet();
            ds = Data_Conection.Information_DB.Insert_Information(infodata);

            return View();
        }
        [HttpGet]
        public ActionResult Information()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeleteInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteInfo(Information info)
        {
            Information infodata = new Information
            {
                Info_name = info.Info_name,
                Info_pw = info.Info_pw,
            };
            DataSet ds = new DataSet();
            ds = Data_Conection.Information_DB.Delete_Information(infodata);
            return View();
        }
    }
}