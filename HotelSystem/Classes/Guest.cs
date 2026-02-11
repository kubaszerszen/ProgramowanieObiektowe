using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class Guest : Person
    {
        public int GuestId { get; set; }
        public string PhoneNumber { get; set; }

        public Guest(int guestId, string firstName, string lastName, string pesel, string phoneNumber) : base(firstName, lastName, pesel)
        {
            GuestId = guestId;
            PhoneNumber = phoneNumber;
        }
    }
}
