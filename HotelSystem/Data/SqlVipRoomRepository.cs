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
    public class SqlVipRoomRepository : IVipRoomRepository
    {
        private readonly string _connString = DatabaseConfig.ConnectionString;
        public void AddVipRoom(VipRoom room)
        {
            using var conn = new SqlConnection(_connString);
            string query = @"INSERT INTO VipRooms (Number, Capacity, IsAvailable, BasePrice, HasJacuzzi, HasMiniBar) 
                             VALUES (@nr, @cap, @av, @bp, @jac, @bar)";

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nr", room.Number);
            cmd.Parameters.AddWithValue("@cap", room.Capacity);
            cmd.Parameters.AddWithValue("@av", room.IsAvailable);
            cmd.Parameters.AddWithValue("@bp", room.BasePrice);
            cmd.Parameters.AddWithValue("@jac", room.HasJacuzzi);
            cmd.Parameters.AddWithValue("@bar", room.HasMiniBar);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<VipRoom> GetAllVipRooms()
        {
            var list = new List<VipRoom>();
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("SELECT * FROM VipRooms", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new VipRoom
                {
                    Id = (int)reader["Id"],
                    Number = (int)reader["Number"],
                    Capacity = (int)reader["Capacity"],
                    IsAvailable = (bool)reader["IsAvailable"],
                    BasePrice = (int)reader["BasePrice"],
                    HasJacuzzi = (bool)reader["HasJacuzzi"],
                    HasMiniBar = (bool)reader["HasMiniBar"]
                });
            }
            return list;
        }

        public void UpdateVipRoom(VipRoom room)
        {
            using var conn = new SqlConnection(_connString);
            string query = @"UPDATE VipRooms 
                             SET Number=@nr, Capacity=@cap, IsAvailable=@av, 
                                 BasePrice=@bp, HasJacuzzi=@jac, HasMiniBar=@bar 
                             WHERE Id=@id";

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", room.Id);
            cmd.Parameters.AddWithValue("@nr", room.Number);
            cmd.Parameters.AddWithValue("@cap", room.Capacity);
            cmd.Parameters.AddWithValue("@av", room.IsAvailable);
            cmd.Parameters.AddWithValue("@bp", room.BasePrice);
            cmd.Parameters.AddWithValue("@jac", room.HasJacuzzi);
            cmd.Parameters.AddWithValue("@bar", room.HasMiniBar);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteVipRoom(int id)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("DELETE FROM VipRooms WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
