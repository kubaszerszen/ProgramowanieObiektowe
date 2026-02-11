using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class Reservation
    {
        public int ReservationId { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Reservation(int reservationId, Guest guest, Room room, DateTime startDate, DateTime endDate)
        {
            ReservationId = reservationId;
            Guest = guest;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int HowLongReserved()
        {
            return (EndDate - StartDate).Days;
        }
    }
}
