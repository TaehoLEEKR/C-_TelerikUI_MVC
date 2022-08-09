using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class Concert
    {
        public int Concert_Num { get; set; }
        public string Concert_Artist { get; set; }
        public string Concert_Content { get; set; }
        public string Concert_location { get; set; }
        public DateTime Concert_Start_Date { get; set; }
        public DateTime Concert_End_Date { get; set; }
        public string Concert_Link { get; set; }
        public int Max_Seat { get; set; }
        public DateTime Concert_Period { get; set; }
    }
}