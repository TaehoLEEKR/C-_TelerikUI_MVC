using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class Details_ID
    {
        public int ID { get; set; }
        public string Concert_Content { get; set; }
        public string 아티스트 { get; set; }
        public DateTime 예약날짜 { get; set; }
        public int 예약좌석수 { get; set; }
        public DateTime 표시기한 { get; set; }
        public int MONTH { get; set; }
        public string 장소 { get; set; }
        public DateTime  공연일자 { get; set; }
    }
}