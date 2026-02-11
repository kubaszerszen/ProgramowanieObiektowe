using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    public abstract class Room
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public Room(int roomNumber, int capacity, decimal pricePerNight)
        {
            RoomNumber = roomNumber;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = true;
        }
    }
}
