using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class Invoice
    {
        public int InvoiceId { get; set; }
        public Reservation Reservation { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Invoice(int invoiceId, Reservation reservation, DateTime invoiceDate)
        {
            InvoiceId = invoiceId;
            Reservation = reservation;
            InvoiceDate = invoiceDate;

            int stayDuration = reservation.HowLongReserved();

            if (stayDuration <= 0) stayDuration = 1;
            
            TotalAmount = stayDuration * reservation.Room.PricePerNight;
        }
    }
}
