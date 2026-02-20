using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem;

namespace HotelSystem.Data
{
    public class SqlReservationRepository : IReservationRepository
    {
        private readonly string _connString = Program.DatabaseConfig.ConnectionString;

        public void CreateReservation(Reservation res)
        {
            using var conn = new SqlConnection(_connString);
            string sql = @"INSERT INTO Reservations (GuestId, EmployeeId, RoomId, RoomType, StartDate, EndDate, TotalPrice) 
                           VALUES (@gid, @eid, @rid, @rtype, @start, @end, @price)";

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@gid", res.Guest.Id);
            cmd.Parameters.AddWithValue("@eid", res.Employee.Id);
            cmd.Parameters.AddWithValue("@rid", res.Room.Id);
            cmd.Parameters.AddWithValue("@rtype", res.Room is VipRoom ? "Vip" : "Standard");
            cmd.Parameters.AddWithValue("@start", res.StartDate);
            cmd.Parameters.AddWithValue("@end", res.EndDate);
            cmd.Parameters.AddWithValue("@price", res.TotalPrice);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Reservation> GetAllReservations()
        {
            var reservations = new List<Reservation>();
            using var conn = new SqlConnection(_connString);
            string sql = @"
                SELECT 
                    r.Id, r.GuestId, r.EmployeeId, r.RoomId, r.RoomType, r.StartDate, r.EndDate, r.TotalPrice,
                    g.FirstName AS G_Name, g.LastName AS G_Sur, g.Phone AS G_Tel, g.Pesel AS G_Pesel, g.Email AS G_Email,
                    e.FirstName AS E_Name, e.LastName AS E_Sur, e.Salary AS E_Sal, e.Pesel AS E_Pesel,
                    COALESCE(s.Number, v.Number) AS RealRoomNumber,
                    COALESCE(s.BasePrice, v.BasePrice) AS RealBasePrice
                FROM Reservations r
                JOIN Guests g ON r.GuestId = g.Id
                JOIN Employees e ON r.EmployeeId = e.Id
                LEFT JOIN StandardRooms s ON r.RoomId = s.Id AND r.RoomType = 'Standard'
                LEFT JOIN VipRooms v ON r.RoomId = v.Id AND r.RoomType = 'Vip'";

            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var guest = new Guest
                {
                    Id = (int)reader["GuestId"],
                    FirstName = reader["G_Name"].ToString(),
                    LastName = reader["G_Sur"].ToString(),
                    PhoneNumber = reader["G_Tel"].ToString(),
                    Pesel = reader["G_Pesel"].ToString(),
                    Email = reader["G_Email"].ToString()
                };

                var employee = new Employee
                {
                    Id = (int)reader["EmployeeId"],
                    FirstName = reader["E_Name"].ToString(),
                    LastName = reader["E_Sur"].ToString(),
                    Pesel = reader["E_Pesel"].ToString(),
                    Salary = (decimal)reader["E_Sal"]
                };

                Room room;
                int roomId = (int)reader["RoomId"];
                string type = reader["RoomType"].ToString();


                int roomNumber = reader["RealRoomNumber"] != DBNull.Value ? (int)reader["RealRoomNumber"] : 0;
                int basePrice = reader["RealBasePrice"] != DBNull.Value ? (int)reader["RealBasePrice"] : 0;

                if (type == "Vip")
                {
                    room = new VipRoom { Id = roomId, Number = roomNumber, BasePrice = basePrice };
                }
                else
                {
                    room = new StandardRoom { Id = roomId, Number = roomNumber, BasePrice = basePrice };
                }

                var res = new Reservation(
                    (int)reader["Id"],
                    guest,
                    room,
                    employee,
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                );


                res.TotalPrice = (decimal)reader["TotalPrice"];

                reservations.Add(res);
            }
            return reservations;
        }
        
        public void UpdateReservation(Reservation res)
        {
            using var conn = new SqlConnection(_connString);
            string sql = @"UPDATE Reservations SET 
                           GuestId = @gid, EmployeeId = @eid, RoomId = @rid, 
                           RoomType = @rtype, StartDate = @start, EndDate = @end, TotalPrice = @price 
                           WHERE Id = @id";

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", res.Id);
            cmd.Parameters.AddWithValue("@gid", res.Guest.Id);
            cmd.Parameters.AddWithValue("@eid", res.Employee.Id);
            cmd.Parameters.AddWithValue("@rid", res.Room.Id);
            cmd.Parameters.AddWithValue("@rtype", res.Room is VipRoom ? "Vip" : "Standard");
            cmd.Parameters.AddWithValue("@start", res.StartDate);
            cmd.Parameters.AddWithValue("@end", res.EndDate);
            cmd.Parameters.AddWithValue("@price", res.TotalPrice);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteReservation(int id)
        {
            using var conn = new SqlConnection(_connString);
            string sql = "DELETE FROM Reservations WHERE Id = @id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public bool IsRoomFree(int roomId, DateTime start, DateTime end)
        {
            using var conn = new SqlConnection(_connString);

            string sql = @"SELECT COUNT(*) FROM Reservations 
                   WHERE RoomId = @rid 
                   AND (@start < EndDate AND @end > StartDate)";

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@rid", roomId);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();

            return count == 0;
        }
    }
}