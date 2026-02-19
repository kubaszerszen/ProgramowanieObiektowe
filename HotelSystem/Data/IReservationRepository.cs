using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data
{
    public interface IReservationRepository
    {
        void CreateReservation(Reservation res);
        List<Reservation> GetAllReservations();
        void UpdateReservation(Reservation res);
        void DeleteReservation(int id);
        bool IsRoomFree(int roomId, DateTime start, DateTime end);
    }
}
