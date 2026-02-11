using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class Suite : Room
    {
        public bool HasBar { get; set; }
        public int NumberOfRooms { get; set; }

        public Suite(int roomNumber, int capacity, decimal pricePerNight, bool hasBar, int numberOfRooms) : base(roomNumber, capacity, pricePerNight)
        {
            HasBar = hasBar;
            NumberOfRooms = numberOfRooms;
        }
    }
}
