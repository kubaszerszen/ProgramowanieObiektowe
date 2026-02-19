using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guest Guest { get; set; } = new Guest();
        public Room Room { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }


        public Reservation() { }

        public Reservation(int id, Guest guest, Room room, Employee employee, DateTime start, DateTime end)
        {
            Id = id;
            Guest = guest;
            Room = room;
            Employee = employee;
            StartDate = start;
            EndDate = end;
        }

        public void CalculatePrice()
        {
            if (Room == null) { TotalPrice = 0; return; }

            var duration = (EndDate - StartDate).TotalDays;
            int days = (int)Math.Ceiling(duration);
            if (days <= 0) days = 1;

            TotalPrice = (decimal)days * Room.BasePrice;
        }
    }
}