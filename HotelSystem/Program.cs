using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;

namespace HotelSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IGuestRepository guestRepo = new SqlGuestRepository();
                IEmployeeRepository empRepo = new SqlEmployeeRepository();
                IStandardRoomRepository standardRoomRepo = new SqlStandardRoomRepository();
                IVipRoomRepository vipRoomRepo = new SqlVipRoomRepository();
                IReservationRepository reservationRepo = new SqlReservationRepository();
                IPaymentRepository paymentRepo = new SqlPaymentRepository();

                bool exit = false;

                while (!exit)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("=== SYSTEM ZARZADZANIA HOTELEM ===");
                        Console.WriteLine("1. Goscie");
                        Console.WriteLine("2. Pracownicy");
                        Console.WriteLine("3. Pokoje Standardowe");
                        Console.WriteLine("4. Pokoje VIP");
                        Console.WriteLine("5. Rezerwacje");
                        Console.WriteLine("6. Platnosci");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("0. Wyjscie");
                        Console.Write("\nWybierz opcje: ");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                GuestMenu(guestRepo);
                                break;
                            case "2":
                                EmployeeMenu(empRepo);
                                break;
                            case "3":
                                StandardRoomMenu(standardRoomRepo, vipRoomRepo);
                                break;
                            case "4":
                                VipRoomMenu(vipRoomRepo, standardRoomRepo);
                                break;
                            case "5":
                                ReservationMenu(reservationRepo, guestRepo, empRepo, standardRoomRepo, vipRoomRepo);
                                break;
                            case "6":
                                PaymentMenu(paymentRepo, reservationRepo);
                                break;
                            case "0":
                                exit = true;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("BLAD BAZY DANYCH");
                        Console.WriteLine($"Szczegoly: {ex.Message}");
                        Console.WriteLine("Sprawdz polaczenie z serwerem SQL");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("WYSTAPIL NIEOCZEKIWANY BLAD");
                        Console.WriteLine($"Tresc bledu: {ex.Message}");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BLAD STARTU APLIKACJI:");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        // ==========================================
        // MENU GOSCI
        // ==========================================
        static void GuestMenu(IGuestRepository repo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- ZARZADZANIE GOSCMI ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj goscia");
                Console.WriteLine("3. Edytuj goscia");
                Console.WriteLine("4. Usun goscia");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("LISTA GOSCI:");
                        foreach (var guest in repo.GetAllGuests())
                        {
                            Console.WriteLine($"ID: {guest.Id} | {guest.FirstName} {guest.LastName} | PESEL: {guest.Pesel} | Tel: {guest.PhoneNumber} | Email: {guest.Email}");
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            var newGuest = new Guest();
                            Console.Write("Imie: "); newGuest.FirstName = Console.ReadLine();
                            Console.Write("Nazwisko: "); newGuest.LastName = Console.ReadLine();
                            newGuest.Pesel = ReadValidNumber("Pesel", 11);
                            newGuest.PhoneNumber = ReadValidNumber("Telefon", 9);
                            Console.Write("Email: "); newGuest.Email = Console.ReadLine();
                            repo.AddGuest(newGuest);
                            Console.WriteLine("SUKCES: Dodano goscia");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("Podaj ID do edycji: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                var guest = new Guest { Id = id };
                                Console.Write("Nowe Imie: "); guest.FirstName = Console.ReadLine();
                                Console.Write("Nowe Nazwisko: "); guest.LastName = Console.ReadLine();
                                guest.Pesel = ReadValidNumber("Pesel", 11);
                                guest.PhoneNumber = ReadValidNumber("Telefon", 9);
                                Console.Write("Nowy Email: "); guest.Email = Console.ReadLine();
                                repo.UpdateGuest(guest);
                                Console.WriteLine("SUKCES: Zaktualizowano dane");
                            }
                            else Console.WriteLine("BLAD: Nieprawidlowe ID");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        {
                            Console.Write("Podaj ID do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                repo.DeleteGuest(id);
                                Console.WriteLine("SUKCES: Usunieto goscia");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // MENU PRACOWNIKOW
        // ==========================================
        static void EmployeeMenu(IEmployeeRepository repo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- ZARZADZANIE PRACOWNIKAMI ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj pracownika");
                Console.WriteLine("3. Edytuj pracownika");
                Console.WriteLine("4. Usun pracownika");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("LISTA PRACOWNIKOW:");
                        foreach (var employee in repo.GetAllEmployees())
                        {
                            Console.WriteLine($"ID: {employee.Id} | {employee.FirstName} {employee.LastName} | PESEL: {employee.Pesel} | Pensja: {employee.Salary} PLN");
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            var employee = new Employee();
                            Console.Write("Imie: "); employee.FirstName = Console.ReadLine();
                            Console.Write("Nazwisko: "); employee.LastName = Console.ReadLine();
                            employee.Pesel = ReadValidNumber("Pesel", 11);
                            Console.Write("Pensja: "); decimal.TryParse(Console.ReadLine(), out decimal salary); employee.Salary = salary;
                            repo.AddEmployee(employee);
                            Console.WriteLine("SUKCES: Dodano pracownika");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("Podaj ID do edycji: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                var employee = new Employee { Id = id };
                                Console.Write("Nowe Imie: "); employee.FirstName = Console.ReadLine();
                                Console.Write("Nowe Nazwisko: "); employee.LastName = Console.ReadLine();
                                employee.Pesel = ReadValidNumber("Pesel", 11);
                                Console.Write("Nowa Pensja: "); decimal.TryParse(Console.ReadLine(), out decimal salary); employee.Salary = salary;
                                repo.UpdateEmployee(employee);
                                Console.WriteLine("SUKCES: Zaktualizowano dane");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        {
                            Console.Write("Podaj ID do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                repo.DeleteEmployee(id);
                                Console.WriteLine("SUKCES: Usunieto pracownika");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // MENU POKOI STANDARDOWYCH
        // ==========================================
        static void StandardRoomMenu(IStandardRoomRepository repo, IVipRoomRepository vipRepo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- POKOJE STANDARDOWE ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj pokoj");
                Console.WriteLine("3. Edytuj pokoj");
                Console.WriteLine("4. Usun pokoj");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("LISTA POKOI STANDARD:");
                        foreach (var room in repo.GetAllStandardRooms())
                        {
                            string hasBalcony = room.HasBalcony ? "Tak" : "Nie";
                            string isAvailable = room.IsAvailable ? "Tak" : "Nie";
                            Console.WriteLine($"ID: {room.Id} | Nr: {room.Number} | Cena: {room.BasePrice} | Osoby: {room.Capacity} | Lozka: {room.NumberOfBeds} | Balkon: {hasBalcony} | Dostepny: {isAvailable}");
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            Console.Write("Numer pokoju: ");
                            int.TryParse(Console.ReadLine(), out int number);

                            if (IsRoomTaken(number, repo, vipRepo))
                            {
                                Console.WriteLine("BLAD: Ten numer jest juz zajety");
                                Console.ReadKey();
                                break;
                            }

                            var room = new StandardRoom { Number = number };
                            Console.Write("Pojemnosc: "); int.TryParse(Console.ReadLine(), out int capacity); room.Capacity = capacity;
                            Console.Write("Cena bazowa: "); int.TryParse(Console.ReadLine(), out int price); room.BasePrice = price;
                            Console.Write("Liczba lozek: "); int.TryParse(Console.ReadLine(), out int beds); room.NumberOfBeds = beds;
                            Console.Write("Balkon (1-Tak, 0-Nie): "); room.HasBalcony = Console.ReadLine() == "1";
                            room.IsAvailable = true;

                            repo.AddStandardRoom(room);
                            Console.WriteLine("SUKCES: Dodano pokoj");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("Podaj ID do edycji: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.Write("Nowy numer pokoju: ");
                                int.TryParse(Console.ReadLine(), out int number);

                                if (IsRoomTaken(number, repo, vipRepo, id, false))
                                {
                                    Console.WriteLine("BLAD: Ten numer jest juz zajety");
                                    Console.ReadKey();
                                    break;
                                }

                                var room = new StandardRoom { Id = id, Number = number };
                                Console.Write("Nowa pojemnosc: "); int.TryParse(Console.ReadLine(), out int capacity); room.Capacity = capacity;
                                Console.Write("Nowa cena: "); int.TryParse(Console.ReadLine(), out int price); room.BasePrice = price;
                                Console.Write("Nowe lozka: "); int.TryParse(Console.ReadLine(), out int beds); room.NumberOfBeds = beds;
                                Console.Write("Balkon (1-Tak, 0-Nie): "); room.HasBalcony = Console.ReadLine() == "1";
                                Console.Write("Dostepny (1-Tak, 0-Nie): "); room.IsAvailable = Console.ReadLine() == "1";

                                repo.UpdateStandardRoom(room);
                                Console.WriteLine("SUKCES: Zaktualizowano pokoj");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        {
                            Console.Write("Podaj ID do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                repo.DeleteStandardRoom(id);
                                Console.WriteLine("SUKCES: Usunieto pokoj");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // MENU POKOI VIP
        // ==========================================
        static void VipRoomMenu(IVipRoomRepository repo, IStandardRoomRepository stdRepo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- POKOJE VIP ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj pokoj VIP");
                Console.WriteLine("3. Edytuj pokoj VIP");
                Console.WriteLine("4. Usun pokoj VIP");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("LISTA POKOI VIP:");
                        foreach (var room in repo.GetAllVipRooms())
                        {
                            string hasJacuzzi = room.HasJacuzzi ? "Tak" : "Nie";
                            string hasMiniBar = room.HasMiniBar ? "Tak" : "Nie";
                            string isAvailable = room.IsAvailable ? "Tak" : "Nie";
                            Console.WriteLine($"ID: {room.Id} | Nr: {room.Number} | Cena: {room.BasePrice} | Osoby: {room.Capacity} | Jacuzzi: {hasJacuzzi} | MiniBar: {hasMiniBar} | Dostepny: {isAvailable}");
                        }

                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            Console.Write("Numer pokoju: ");
                            int.TryParse(Console.ReadLine(), out int number);

                            if (IsRoomTaken(number, stdRepo, repo))
                            {
                                Console.WriteLine("BLAD: Ten numer jest juz zajety");
                                Console.ReadKey();
                                break;
                            }

                            var room = new VipRoom { Number = number };
                            Console.Write("Pojemnosc: "); int.TryParse(Console.ReadLine(), out int capacity); room.Capacity = capacity;
                            Console.Write("Cena bazowa: "); int.TryParse(Console.ReadLine(), out int price); room.BasePrice = price;
                            Console.Write("Jacuzzi (1-Tak, 0-Nie): "); room.HasJacuzzi = Console.ReadLine() == "1";
                            Console.Write("MiniBar (1-Tak, 0-Nie): "); room.HasMiniBar = Console.ReadLine() == "1";
                            room.IsAvailable = true;

                            repo.AddVipRoom(room);
                            Console.WriteLine("SUKCES: Dodano pokoj VIP");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("Podaj ID do edycji: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.Write("Nowy numer pokoju: ");
                                int.TryParse(Console.ReadLine(), out int number);

                                if (IsRoomTaken(number, stdRepo, repo, id, true))
                                {
                                    Console.WriteLine("BLAD: Ten numer jest juz zajety");
                                    Console.ReadKey();
                                    break;
                                }

                                var room = new VipRoom { Id = id, Number = number };
                                Console.Write("Nowa pojemnosc: "); int.TryParse(Console.ReadLine(), out int capacity); room.Capacity = capacity;
                                Console.Write("Nowa cena: "); int.TryParse(Console.ReadLine(), out int price); room.BasePrice = price;
                                Console.Write("Jacuzzi (1-Tak, 0-Nie): "); room.HasJacuzzi = Console.ReadLine() == "1";
                                Console.Write("MiniBar (1-Tak, 0-Nie): "); room.HasMiniBar = Console.ReadLine() == "1";
                                Console.Write("Dostepny (1-Tak, 0-Nie): "); room.IsAvailable = Console.ReadLine() == "1";

                                repo.UpdateVipRoom(room);
                                Console.WriteLine("SUKCES: Zaktualizowano pokoj VIP");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        {
                            Console.Write("Podaj ID do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                repo.DeleteVipRoom(id);
                                Console.WriteLine("SUKCES: Usunieto pokoj VIP");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // MENU REZERWACJI
        // ==========================================
        static void ReservationMenu(IReservationRepository resRepo, IGuestRepository gRepo, IEmployeeRepository eRepo, IStandardRoomRepository sRepo, IVipRoomRepository vRepo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- REZERWACJE ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj rezerwacje");
                Console.WriteLine("3. Usun rezerwacje");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("LISTA REZERWACJI:");
                        var reservations = resRepo.GetAllReservations();
                        var standardRooms = sRepo.GetAllStandardRooms();
                        var vipRooms = vRepo.GetAllVipRooms();

                        foreach (var reservation in reservations)
                        {
                            int roomNumber = reservation.Room.Number;
                            if (roomNumber == 0)
                            {
                                var foundStd = standardRooms.FirstOrDefault(x => x.Id == reservation.Room.Id);
                                if (foundStd != null) roomNumber = foundStd.Number;
                                else
                                {
                                    var foundVip = vipRooms.FirstOrDefault(x => x.Id == reservation.Room.Id);
                                    if (foundVip != null) roomNumber = foundVip.Number;
                                }
                            }
                            Console.WriteLine($"ID: {reservation.Id} | Gosc: {reservation.Guest.FirstName} {reservation.Guest.LastName} | Pokoj: {roomNumber} | Od: {reservation.StartDate:yyyy-MM-dd} Do: {reservation.EndDate:yyyy-MM-dd} | Cena: {reservation.TotalPrice} PLN");
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            try
                            {
                                Console.Write("ID Goscia: "); int.TryParse(Console.ReadLine(), out int guestId);
                                var guest = gRepo.GetAllGuests().FirstOrDefault(x => x.Id == guestId);
                                if (guest == null) throw new Exception("Nie znaleziono goscia");

                                Console.Write("ID Pracownika: "); int.TryParse(Console.ReadLine(), out int empId);
                                var employee = eRepo.GetAllEmployees().FirstOrDefault(x => x.Id == empId);
                                if (employee == null) throw new Exception("Nie znaleziono pracownika");

                                Console.Write("Typ pokoju (1-Std, 2-Vip): "); string typeInput = Console.ReadLine();
                                Console.Write("ID Pokoju: "); int.TryParse(Console.ReadLine(), out int roomId);

                                Room room = (typeInput == "2")
                                    ? vRepo.GetAllVipRooms().FirstOrDefault(x => x.Id == roomId)
                                    : sRepo.GetAllStandardRooms().FirstOrDefault(x => x.Id == roomId);

                                if (room == null) throw new Exception("Nie znaleziono pokoju");

                                Console.Write("Data od (RRRR-MM-DD): ");
                                DateTime startDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Data do (RRRR-MM-DD): ");
                                DateTime endDate = DateTime.Parse(Console.ReadLine());

                                if (endDate <= startDate) throw new Exception("Data zakonczenia musi byc pozniejsza");

                                bool isFree = resRepo.IsRoomFree(room.Id, startDate, endDate);
                                if (!isFree)
                                {
                                    Console.WriteLine("BLAD: Ten pokoj jest ZAJETY w wybranym terminie");
                                    Console.ReadKey();
                                    break;
                                }

                                var reservation = new Reservation(0, guest, room, employee, startDate, endDate);
                                reservation.CalculatePrice();

                                resRepo.CreateReservation(reservation);
                                Console.WriteLine($"SUKCES: Dodano rezerwacje. Pokoj: {room.Number}, Koszt: {reservation.TotalPrice} PLN.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"BLAD: {ex.Message}");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("ID do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                resRepo.DeleteReservation(id);
                                Console.WriteLine("SUKCES: Usunieto rezerwacje");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // MENU PLATNOSCI
        // ==========================================
        static void PaymentMenu(IPaymentRepository payRepo, IReservationRepository resRepo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- PLATNOSCI ---");
                Console.WriteLine("1. Lista (Wszystkie dane)");
                Console.WriteLine("2. Dodaj platnosc");
                Console.WriteLine("3. Usun platnosc");
                Console.WriteLine("0. Powrot");
                Console.Write("\nWybierz: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("STATUS PLATNOSCI:");

                        var allReservations = resRepo.GetAllReservations();
                        var allPayments = payRepo.GetAllPayments();

                        foreach (var res in allReservations)
                        {

                            var payment = allPayments.FirstOrDefault(p => p.ReservationId == res.Id);

                            string statusText;
                            string extraInfo;

                            if (payment != null && payment.IsPaid)
                            {
                                statusText = "OPLACONE";
                                extraInfo = $"Metoda: {payment.Method}, Data: {payment.PaymentDate:yyyy-MM-dd}";
                            }
                            else
                            {
                                statusText = "DO ZAPLATY";
                                extraInfo = "---";
                            }

                            Console.WriteLine($"ID Rez: {res.Id} | Kwota: {res.TotalPrice} PLN | Status: {statusText} | {extraInfo}");
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        {
                            Console.Write("Podaj ID Rezerwacji: ");
                            if (int.TryParse(Console.ReadLine(), out int resId))
                            {
                                var reservation = resRepo.GetAllReservations().FirstOrDefault(x => x.Id == resId);
                                if (reservation != null)
                                {
                                    if (payRepo.GetAllPayments().Any(p => p.ReservationId == resId && p.IsPaid))
                                    {
                                        Console.WriteLine("INFO: Ta rezerwacja jest juz oplacona");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nWybierz metode platnosci, koszt zapłaty: {reservation.TotalPrice} PLN");
                                        Console.WriteLine("1. BLIK");
                                        Console.WriteLine("2. Przelew");
                                        Console.WriteLine("3. Karta");
                                        Console.Write("Wybor: ");

                                        string mChoice = Console.ReadLine();
                                        string method = "Karta";

                                        if (mChoice == "1") method = "BLIK";
                                        else if (mChoice == "2") method = "Przelew";
                                        else if (mChoice == "3") method = "Karta";
                                        else Console.WriteLine("Nieznana opcja. Wybrano domyslnie: Karta");

                                        var payment = new Payment
                                        {
                                            ReservationId = resId,
                                            Amount = reservation.TotalPrice,
                                            PaymentDate = DateTime.Now,
                                            Method = method,
                                            IsPaid = true
                                        };
                                        payRepo.AddPayment(payment);
                                        Console.WriteLine($"SUKCES: Zaksiegowano wplate metoda: {method}");
                                        Console.WriteLine($"Zapłacono: {reservation.TotalPrice}");
                                    }
                                }
                                else Console.WriteLine("BLAD: Nie znaleziono rezerwacji");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Write("Podaj ID platnosci do usuniecia: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                payRepo.DeletePayment(id);
                                Console.WriteLine("SUKCES: Usunieto platnosc");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        back = true;
                        break;
                }
            }
        }

        // ==========================================
        // Funkcje
        // ==========================================
        static bool IsRoomTaken(int number, IStandardRoomRepository sRepo, IVipRoomRepository vRepo, int? currentRoomId = null, bool isVip = false)
        {
            var standardRooms = sRepo.GetAllStandardRooms();
            var vipRooms = vRepo.GetAllVipRooms();

            bool takenStandard = standardRooms.Any(r => r.Number == number && !(isVip == false && currentRoomId.HasValue && r.Id == currentRoomId.Value));

            bool takenVip = vipRooms.Any(r => r.Number == number && !(isVip == true && currentRoomId.HasValue && r.Id == currentRoomId.Value));

            return takenStandard || takenVip;
        }

        static string ReadValidNumber(string fieldName, int Length)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.Length == Length && input.All(char.IsDigit))
                {
                    return input;
                }
                Console.WriteLine($"BLAD: Wprowadzony numer musi skladac sie z {Length} cyfr");
            }
        }
    }
}