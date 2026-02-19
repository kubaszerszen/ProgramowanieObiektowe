# 🏨 System Zarządzania Rezerwacjami Hotelowymi (CRUD)

Projekt zaawansowanego systemu do obsługi hotelu, zrealizowany w technologii **C#** oraz **Microsoft SQL Server**. Aplikacja pozwala na pełne zarządzanie bazą gości, pokoi oraz procesem rezerwacji przy użyciu paradygmatu programowania obiektowego.

## 🚀 Kluczowe Funkcjonalności
* 🏨 **Zarządzanie Rezerwacjami:** przypisywanie gości do pokoi w terminach.
* 🛠️ **Pełny CRUD:** Dodawanie, przeglądanie, edycja oraz usuwanie rekordów w bazie danych MSSQL.
* 💳 **Moduł Płatności:** Rejestracja transakcji (BLIK, Karta, Przelew) oraz wyliczanie kosztu pobytu.
* 🛡️ **Stabilność:** Zaimplementowana obsługa wyjątków oraz walidacja danych wejściowych.

## 🛠️ Technologie
* **Język:** C# (.NET 8.0).
* **Środowisko:** Microsoft Visual Studio 2022.
* **Baza danych:** Microsoft SQL Server (MSSQL).

## ⚙️ Instrukcja uruchomienia

1. **Baza danych:** * Uruchom plik `HotelDatabase.sql` w MSSQL.
   * skrypt utworzy bazę `HotelDb`, tabele oraz zasili je danymi testowymi.
   * Jeśli podczas uruchamiania skryptu wystąpi błąd, należy w pliku .sql odnaleźć początkową sekcję CREATE DATABASE i zmienić ścieżki do plików .mdf i .ldf na zgodne z Twoją instalacją SQL Server.
2. **Konfiguracja połączenia:** * Otwórz projekt w Visual Studio 2022 za pomocą pliku `HotelSystem.sln`.
W razie potrzeby dostosowania aplikacji do własnej instancji SQL Server, zmień parametr Server w pliku Program.cs w linii definicji ConnectionString.
     ```csharp
     public static string ConnectionString { get; } = @"Server=.\SQLEXPRESS;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;";.
     ```
