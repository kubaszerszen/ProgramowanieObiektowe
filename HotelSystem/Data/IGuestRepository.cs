using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public interface IGuestRepository
    {        
        void AddGuest(Guest guest);
        List<Guest> GetAllGuests();
        void UpdateGuest(Guest guest);
        void DeleteGuest(int id);
    }
}