using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.Models
{
    public class Reservation
    {
        public int Reservation_Num { get; set; }
        public int Reservation_Concert_Num { get; set; }
        public string Reservation_Concert_Artist { get; set; }
        public DateTime Reservation_Date { get; set; }
        public int Reservation_Seat { get; set; }
        
    }
}