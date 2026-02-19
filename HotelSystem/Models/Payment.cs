using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; } 
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; } = "Karta";
        public bool IsPaid { get; set; }
    }
}
