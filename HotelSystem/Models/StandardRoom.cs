using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Models
{
    public class StandardRoom : Room
    {
        public int NumberOfBeds { get; set; }

        public bool HasBalcony { get; set; } = false;
    }
}
