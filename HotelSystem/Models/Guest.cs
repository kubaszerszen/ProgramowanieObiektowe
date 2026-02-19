using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Models
{
    public class Guest : Person
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
