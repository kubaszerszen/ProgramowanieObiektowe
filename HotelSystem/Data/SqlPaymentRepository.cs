using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelSystem.Program;

namespace HotelSystem.Data
{
    public class SqlPaymentRepository : IPaymentRepository
    {
        private readonly string _connString = DatabaseConfig.ConnectionString;

        public void AddPayment(Payment p)
        {
            using var conn = new SqlConnection(_connString);
            string sql = @"INSERT INTO Payments (ReservationId, Amount, PaymentDate, Method, IsPaid) 
                           VALUES (@rid, @amt, @date, @meth, @paid)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@rid", p.ReservationId);
            cmd.Parameters.AddWithValue("@amt", p.Amount);
            cmd.Parameters.AddWithValue("@date", p.PaymentDate);
            cmd.Parameters.AddWithValue("@meth", p.Method);
            cmd.Parameters.AddWithValue("@paid", p.IsPaid);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Payment> GetAllPayments()
        {
            var list = new List<Payment>();
            using var conn = new SqlConnection(_connString);
            string sql = "SELECT * FROM Payments";
            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Payment
                {
                    Id = (int)reader["Id"],
                    ReservationId = (int)reader["ReservationId"],
                    Amount = (decimal)reader["Amount"],
                    PaymentDate = (DateTime)reader["PaymentDate"],
                    Method = reader["Method"].ToString(),
                    IsPaid = (bool)reader["IsPaid"]
                });
            }
            return list;
        }

        public List<Payment> GetPaymentsByReservation(int reservationId)
        {
            return GetAllPayments().Where(x => x.ReservationId == reservationId).ToList();
        }

        public void UpdatePaymentStatus(int paymentId, bool isPaid)
        {
            using var conn = new SqlConnection(_connString);
            string sql = "UPDATE Payments SET IsPaid = @paid WHERE Id = @id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@paid", isPaid);
            cmd.Parameters.AddWithValue("@id", paymentId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeletePayment(int id)
        {
            using var conn = new SqlConnection(_connString);
            string sql = "DELETE FROM Payments WHERE Id = @id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
