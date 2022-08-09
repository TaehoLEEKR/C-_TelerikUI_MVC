using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class details
    {
        public DateTime 공연일자 { get; set; }
        public string 아티스트 { get; set; }
        public string 장소 { get; set; }
        public int 좌석수 { get; set; }
        public int 예약완료 { get; set; }
        public int Reservation_Concert_Num { get; set; }

    }
}