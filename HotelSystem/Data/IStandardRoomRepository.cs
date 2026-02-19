using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public  interface IStandardRoomRepository
    {
        void AddStandardRoom(StandardRoom room);
        List<StandardRoom> GetAllStandardRooms();
        void UpdateStandardRoom(StandardRoom room);
        void DeleteStandardRoom(int id);
    }
}
