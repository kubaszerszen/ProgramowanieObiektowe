using HotelSystem.Classes;

namespace HotelSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Guest g = new Guest(1, "Kuba", "Test", "123", "555");
            StandardRoom p = new StandardRoom(10, 2, 150, true);

            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now.AddDays(2);

            Reservation r = new Reservation(1, g, p, d1, d2);

            Invoice f = new Invoice(1, r, DateTime.Now);

            Console.WriteLine("Dziala dls: " + g.LastName);
            Console.WriteLine("Dni: " + r.HowLongReserved());
            Console.WriteLine("Cena: " + f.TotalAmount);

        }
    }
}