using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    internal class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly string _connString = @"Server=.\SQLEXPRESS;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public void AddEmployee(Employee emp)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Pesel, Salary) VALUES (@fn, @ln, @ps, @sl)", conn);
            cmd.Parameters.AddWithValue("@fn", emp.FirstName);
            cmd.Parameters.AddWithValue("@ln", emp.LastName);
            cmd.Parameters.AddWithValue("@ps", emp.Pesel);
            cmd.Parameters.AddWithValue("@sl", emp.Salary);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Employee> GetAllEmployees()
        {
            var list = new List<Employee>();
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("SELECT * FROM Employees", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Employee
                {
                    Id = (int)reader["Id"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Pesel = reader["Pesel"].ToString(),
                    Salary = (decimal)reader["Salary"]
                });
            }
            return list;
        }

        public void UpdateEmployee(Employee emp)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("UPDATE Employees SET FirstName=@fn, LastName=@ln, Pesel=@ps, Salary=@sl WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@fn", emp.FirstName);
            cmd.Parameters.AddWithValue("@ln", emp.LastName);
            cmd.Parameters.AddWithValue("@ps", emp.Pesel);
            cmd.Parameters.AddWithValue("@sl", emp.Salary);
            cmd.Parameters.AddWithValue("@id", emp.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int id)
        {
            using var conn = new SqlConnection(_connString);
            var cmd = new SqlCommand("DELETE FROM Employees WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

