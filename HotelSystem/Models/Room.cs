using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Models
{
    public abstract class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public int BasePrice { get; set; }

    }

}
