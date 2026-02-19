using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        List<Payment> GetAllPayments();
        List<Payment> GetPaymentsByReservation(int reservationId);
        void UpdatePaymentStatus(int paymentId, bool isPaid);
        void DeletePayment(int paymentId);
    }
}
