using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee emp);
        List<Employee> GetAllEmployees();
        void UpdateEmployee(Employee emp);
        void DeleteEmployee(int id);
    }
}
