using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Classes
{
    internal class Employee: Person
    {
        public int EmployeeId { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public Employee(int employeeId, string firstName, string lastName, string pesel, string position, decimal salary) : base(firstName, lastName, pesel)
        {
            EmployeeId = employeeId;
            Position = position;
            Salary = salary;
        }
    }
}
