using System;
using HotelSystem.Models;
using HotelSystem.Data;
using HotelSystem.Utils;

namespace HotelSystem
{
    internal class Program
    {
        // Repozytorium - nasze połączenie z bazą danych
        static GuestRepository repository = new GuestRepository();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("       SYSTEM ZARZĄDZANIA HOTELEM          ");
                Console.WriteLine("===========================================");
                Console.WriteLine(" 1. [LISTA] Pokaż wszystkich gości");
                Console.WriteLine(" 2. [DODAJ] Zarejestruj nowego gościa");
                Console.WriteLine(" 3. [EDYTUJ] Zmień dane gościa");
                Console.WriteLine(" 4. [USUŃ] Wymelduj / Usuń gościa");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" 0. WYJŚCIE");
                Console.WriteLine("===========================================");
                Console.Write("Wybierz opcję: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowGuests(); break;
                    case "2": AddGuest(); break;
                    case "3": UpdateGuest(); break;
                    case "4": DeleteGuest(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // --- 1. WYŚWIETLANIE ---
        static void ShowGuests()
        {
            Console.Clear();
            Console.WriteLine(">>> LISTA GOŚCI <<<");
            try
            {
                var guests = repository.GetAll();
                if (guests.Count == 0) Console.WriteLine("Baza jest pusta.");

                foreach (var g in guests)
                {
                    Console.WriteLine($"ID: {g.Id} | {g.FirstName} {g.LastName} | PESEL: {g.Pesel} | Tel: {g.PhoneNumber ?? "brak"}");
                }
            }
            catch (Exception) { Console.WriteLine("Błąd pobierania danych. Sprawdź logs.txt"); }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić...");
            Console.ReadKey();
        }

        // --- 2. DODAWANIE ---
        static void AddGuest()
        {
            Console.Clear();
            Console.WriteLine(">>> DODAWANIE GOŚCIA <<<");

            Guest g = new Guest();
            Console.Write("Imię: "); g.FirstName = Console.ReadLine() ?? "";
            Console.Write("Nazwisko: "); g.LastName = Console.ReadLine() ?? "";
            Console.Write("PESEL: "); g.Pesel = Console.ReadLine() ?? "";
            Console.Write("Telefon: "); g.PhoneNumber = Console.ReadLine();

            // Walidacja (na ocenę 4.0)
            if (string.IsNullOrEmpty(g.FirstName) || g.Pesel.Length != 11)
            {
                Console.WriteLine("Błąd: Imię nie może być puste, a PESEL musi mieć 11 znaków!");
            }
            else
            {
                try
                {
                    repository.Add(g);
                    Console.WriteLine("Dodano pomyślnie!");
                }
                catch (Exception) { Console.WriteLine("Błąd zapisu!"); }
            }
            Console.ReadKey();
        }

        // --- 3. EDYCJA ---
        static void UpdateGuest()
        {
            Console.Clear();
            Console.WriteLine(">>> EDYCJA GOŚCIA <<<");
            Console.Write("Podaj ID gościa do zmiany: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Guest g = new Guest { Id = id };
                Console.Write("Nowe Imię: "); g.FirstName = Console.ReadLine() ?? "";
                Console.Write("Nowe Nazwisko: "); g.LastName = Console.ReadLine() ?? "";
                Console.Write("Nowy PESEL: "); g.Pesel = Console.ReadLine() ?? "";
                Console.Write("Nowy Telefon: "); g.PhoneNumber = Console.ReadLine();

                try
                {
                    repository.Update(g);
                    Console.WriteLine("Zaktualizowano dane.");
                }
                catch (Exception) { Console.WriteLine("Błąd edycji."); }
            }
            Console.ReadKey();
        }

        // --- 4. USUWANIE ---
        static void DeleteGuest()
        {
            Console.Clear();
            Console.WriteLine(">>> USUWANIE GOŚCIA <<<");
            Console.Write("Podaj ID gościa do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    repository.Delete(id);
                    Console.WriteLine("Gość został usunięty z bazy.");
                }
                catch (Exception) { Console.WriteLine("Błąd podczas usuwania."); }
            }
            Console.ReadKey();
        }
    }
}