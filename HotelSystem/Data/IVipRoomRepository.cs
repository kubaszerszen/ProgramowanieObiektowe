using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public interface IVipRoomRepository
    {
        void AddVipRoom(VipRoom room);
        List<VipRoom> GetAllVipRooms();
        void UpdateVipRoom(VipRoom room);
        void DeleteVipRoom(int id);
    }
}
