using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public class SqlStandardRoomRepository : IStandardRoomRepository
    {
        private readonly string _connString = @"Server=.\SQLEXPRESS;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public void AddStandardRoom(StandardRoom room)
        {
            using var conn = new SqlConnection(_connString);
            string query = @"INSERT INTO StandardRooms (Number, Capacity, IsAvailable, BasePrice, NumberOfBeds, HasBalcony) 
                             VALUES (@nr, @cap, @av, @bp, @beds, @bal)";

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nr", room.Number);
            cmd.Parameters.AddWithValue("@cap", room.Capacity);
            cmd.Parameters.AddWithValue("@av", room.IsAvailable);
            cmd.Parameters.AddWithValue("@bp", room.BasePrice);
            cmd.Parameters.AddWithValue("@beds", room.NumberOfBeds);
            cmd.Parameters.AddWithValue("@bal", room.HasBalcony);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public List<StandardRoom> GetAllStandardRooms()
        {
            var list = new List<StandardRoom>();
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("SELECT * FROM StandardRooms", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StandardRoom
                {
                    Id = (int)reader["Id"],
                    Number = (int)reader["Number"],
                    Capacity = (int)reader["Capacity"],
                    IsAvailable = (bool)reader["IsAvailable"],
                    BasePrice = (int)reader["BasePrice"],
                    NumberOfBeds = (int)reader["NumberOfBeds"],
                    HasBalcony = (bool)reader["HasBalcony"]
                });
            }
            return list;
        }
        public void UpdateStandardRoom(StandardRoom room)
        {
            using var conn = new SqlConnection(_connString);
            string query = @"UPDATE StandardRooms 
                             SET Number=@nr, Capacity=@cap, IsAvailable=@av, 
                                 BasePrice=@bp, NumberOfBeds=@beds, HasBalcony=@bal 
                             WHERE Id=@id";

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", room.Id);
            cmd.Parameters.AddWithValue("@nr", room.Number);
            cmd.Parameters.AddWithValue("@cap", room.Capacity);
            cmd.Parameters.AddWithValue("@av", room.IsAvailable);
            cmd.Parameters.AddWithValue("@bp", room.BasePrice);
            cmd.Parameters.AddWithValue("@beds", room.NumberOfBeds);
            cmd.Parameters.AddWithValue("@bal", room.HasBalcony);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void DeleteStandardRoom(int id)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("DELETE FROM StandardRooms WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
