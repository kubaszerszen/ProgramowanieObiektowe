using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public class SqlGuestRepository : IGuestRepository
    {
        private readonly string _connString = @"Server=.\SQLEXPRESS;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public void AddGuest(Guest guest)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("INSERT INTO Guests (FirstName, LastName, Email, Pesel, Phone) VALUES (@fn, @ln, @em, @ps, @ph)", conn);
            cmd.Parameters.AddWithValue("@fn", guest.FirstName);
            cmd.Parameters.AddWithValue("@ln", guest.LastName);
            cmd.Parameters.AddWithValue("@em", guest.Email);
            cmd.Parameters.AddWithValue("@ps", guest.Pesel ?? ""); 
            cmd.Parameters.AddWithValue("@ph", guest.PhoneNumber ?? "");

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Guest> GetAllGuests()
        {
            var list = new List<Guest>();
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("SELECT * FROM Guests", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Guest
                {
                    Id = (int)reader["Id"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Pesel = reader["Pesel"].ToString(),
                    PhoneNumber = reader["Phone"].ToString()
                });
            }
            return list;
        }
        public void UpdateGuest(Guest guest)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("UPDATE Guests SET FirstName=@fn, LastName=@ln, Email=@em, Pesel=@ps, Phone=@ph WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@fn", guest.FirstName);
            cmd.Parameters.AddWithValue("@ln", guest.LastName);
            cmd.Parameters.AddWithValue("@em", guest.Email);
            cmd.Parameters.AddWithValue("@ps", guest.Pesel);
            cmd.Parameters.AddWithValue("@ph", guest.PhoneNumber);
            cmd.Parameters.AddWithValue("@id", guest.Id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteGuest(int id)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("DELETE FROM Guests WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}