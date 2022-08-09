using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class Information
    {
        public string Info_name { get; set; }
        public string Info_pw { get; set; }
        public string Info_email { get; set; }
        public string Info_adr { get; set; }
        public string Info_year { get; set; }
        public string Auth { get; set; }

    }
    public enum AuthEnum

    {
        Admin,
        Guest,
        Normal
    }


}