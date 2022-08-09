using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class NewsModels
    {
        public int News_Num { get; set; }
        public string News_Summary { get; set; }
        public string News_Content { get; set; }
        public DateTime News_Period { get; set; }
        public DateTime News_RegistTime { get; set; } = DateTime.Now;
        public string News_Info_Name { get; set; }
    }
}