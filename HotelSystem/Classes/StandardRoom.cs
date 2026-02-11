using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class StandardRoom : Room
    {
        public bool HasExtraBed { get; set; }
        public StandardRoom(int roomNumber, int capacity, decimal pricePerNight, bool hasExtraBed) : base(roomNumber, capacity, pricePerNight)
        {
            HasExtraBed = hasExtraBed;
        }
    }
}
