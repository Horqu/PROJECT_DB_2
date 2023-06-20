﻿using System;
using Microsoft.Data.SqlClient;

namespace WebApi.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void ClearTables()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                    DELETE FROM Oceny;
                    DELETE FROM Kursy;
                    DELETE FROM Nauczyciele;
                    DELETE FROM Studenci;

                    SET IDENTITY_INSERT Studenci ON;

                    INSERT INTO Studenci (StudentId, Imie, Nazwisko, DataUrodzenia, Klasa)
                    VALUES
                        (1, 'Katarzyna', 'Malinowska', '2005-02-14', '3A'),
                        (2, 'Tomasz', 'Kowalczyk', '2005-03-05', '3B'),
                        (3, 'Patrycja', 'Lis', '2005-01-22', '3C'),
                        (4, 'Andrzej', 'Mazurek', '2004-11-30', '3B'),
                        (5, 'Aleksandra', 'Baran', '2005-04-16', '3A'),
                        (6, 'Łukasz', 'Duda', '2005-05-12', '3B'),
                        (7, 'Julia', 'Adamska', '2004-12-07', '3C'),
                        (8, 'Paweł', 'Sikora', '2004-10-25', '3B'),
                        (9, 'Paulina', 'Zawadzka', '2005-09-19', '3A'),
                        (10, 'Filip', 'Kaczmarek', '2005-07-17', '3A'),
                        (11, 'Weronika', 'Jaworska', '2004-08-11', '3B'),
                        (12, 'Jakub', 'Wróbel', '2004-06-15', '3C'),
                        (13, 'Klaudia', 'Marciniak', '2004-05-20', '3B'),
                        (14, 'Adam', 'Zięba', '2005-08-12', '3A'),
                        (15, 'Magdalena', 'Jóźwiak', '2005-10-05', '3B'),
                        (16, 'Dawid', 'Dziedzic', '2004-09-01', '3C'),
                        (17, 'Gabriela', 'Rutkowska', '2004-07-17', '3B'),
                        (18, 'Maciej', 'Pietrzak', '2004-11-11', '3A'),
                        (19, 'Joanna', 'Gajewska', '2005-12-03', '3B'),
                        (20, 'Michał', 'Majewski', '2004-06-22', '3C'),
                        (21, 'Monika', 'Sokołowska', '2005-07-18', '3B'),
                        (22, 'Bartłomiej', 'Woźniak', '2005-08-08', '3A'),
                        (23, 'Agata', 'Mikołajczyk', '2005-09-27', '3B'),
                        (24, 'Artur', 'Czarnecki', '2004-07-07', '3C'),
                        (25, 'Karina', 'Sikorska', '2005-08-19', '3B'),
                        (26, 'Marcin', 'Szewczyk', '2005-10-06', '3A'),
                        (27, 'Kamila', 'Ostrowska', '2005-11-30', '3B'),
                        (28, 'Robert', 'Kubiak', '2004-09-15', '3C'),
                        (29, 'Natalia', 'Dąbrowska', '2005-08-21', '3B'),
                        (30, 'Krystian', 'Krupa', '2005-10-07', '3A'),
                        (31, 'Anna', 'Lewandowska', '2005-12-01', '3B'),
                        (32, 'Rafał', 'Zakrzewski', '2004-10-14', '3C'),
                        (33, 'Katarzyna', 'Kołodziej', '2004-08-19', '3B'),
                        (34, 'Tadeusz', 'Piątek', '2005-09-09', '3A'),
                        (35, 'Dorota', 'Wieczorek', '2005-10-28', '3B'),
                        (36, 'Sebastian', 'Kowalski', '2004-08-17', '3C'),
                        (37, 'Elżbieta', 'Zając', '2005-09-20', '3B'),
                        (38, 'Adrian', 'Przybylski', '2005-10-08', '3A'),
                        (39, 'Justyna', 'Nowicka', '2004-12-04', '3B'),
                        (40, 'Damian', 'Pawłowski', '2004-10-16', '3C'),
                        (41, 'Sylwia', 'Głowacka', '2004-09-21', '3B'),
                        (42, 'Konrad', 'Witkowski', '2005-11-10', '3A'),
                        (43, 'Magda', 'Kaczmarczyk', '2004-07-23', '3B'),
                        (44, 'Mateusz', 'Jankowski', '2004-09-11', '3C'),
                        (45, 'Aneta', 'Brzezińska', '2005-10-30', '3B'),
                        (46, 'Bartosz', 'Ciesielski', '2005-12-09', '3A');

                    SET IDENTITY_INSERT Studenci OFF;
                    SET IDENTITY_INSERT Nauczyciele ON;

                    INSERT INTO Nauczyciele (NauczycielId, Imie, Nazwisko, Przedmiot)
                    VALUES
                        (1, 'Andrzej', 'Sobczak', 'Matematyka'),
                        (2, 'Barbara', 'Wilk', 'Język Polski'),
                        (3, 'Cezary', 'Stasiak', 'Historia'),
                        (4, 'Dorota', 'Michalak', 'Biologia'),
                        (5, 'Edward', 'Sadowski', 'Geografia'),
                        (6, 'Franciszek', 'Włodarczyk', 'Fizyka'),
                        (7, 'Grażyna', 'Pawlak', 'Chemia'),
                        (8, 'Henryk', 'Walczak', 'Wychowanie Fizyczne'),
                        (9, 'Iwona', 'Adamek', 'Informatyka'),
                        (10, 'Józef', 'Ratajczak', 'Język Angielski');

                    SET IDENTITY_INSERT Nauczyciele OFF;
                    SET IDENTITY_INSERT Kursy ON;

                    INSERT INTO Kursy (KursId, NazwaKursu, NauczycielId)
                    VALUES
                        (1, 'Matematyka zaawansowana', 1),
                        (2, 'Język Polski podstawowy', 2),
                        (3, 'Historia świata', 3),
                        (4, 'Biologia zaawansowana', 4),
                        (5, 'Geografia podstawowa', 5),
                        (6, 'Fizyka dla zaawansowanych', 6),
                        (7, 'Chemia dla początkujących', 7),
                        (8, 'WF', 8),
                        (9, 'Informatyka dla początkujących', 9),
                        (10, 'Język Angielski zaawansowany', 10);

                    SET IDENTITY_INSERT Kursy OFF;

                    INSERT INTO Oceny (StudentId, KursId, Ocena, Data)
                    VALUES
                        (1, 1, 4.5, '2023-01-10'),
                        (2, 2, 3.5, '2023-01-11'),
                        (3, 3, 5.0, '2023-01-12'),
                        (4, 4, 4.0, '2023-01-13'),
                        (5, 5, 3.0, '2023-01-14'),
                        (6, 6, 5.0, '2023-01-15'),
                        (7, 7, 2.0, '2023-01-16'),
                        (8, 8, 4.0, '2023-01-17'),
                        (9, 9, 4.5, '2023-01-18'),
                        (10, 10, 5.0, '2023-01-19'),
                        (11, 1, 3.5, '2023-01-20'),
                        (12, 2, 2.0, '2023-01-21'),
                        (13, 3, 5.0, '2023-01-22'),
                        (14, 4, 3.0, '2023-01-23'),
                        (15, 5, 4.5, '2023-01-24'),
                        (16, 6, 4.0, '2023-01-25'),
                        (17, 7, 3.5, '2023-01-26'),
                        (18, 8, 2.5, '2023-01-27'),
                        (19, 9, 5.0, '2023-01-28'),
                        (20, 10, 3.5, '2023-01-29'),
                        (21, 1, 4.5, '2023-01-30'),
                        (22, 2, 3.0, '2023-01-31'),
                        (23, 3, 5.0, '2023-02-01'),
                        (24, 4, 4.0, '2023-02-02'),
                        (25, 5, 3.0, '2023-02-03'),
                        (26, 6, 5.0, '2023-02-04'),
                        (27, 7, 2.0, '2023-02-05'),
                        (28, 8, 4.0, '2023-02-06'),
                        (29, 9, 4.5, '2023-02-07'),
                        (30, 10, 5.0, '2023-02-08'),
                        (31, 1, 3.5, '2023-02-09'),
                        (32, 2, 2.0, '2023-02-10'),
                        (33, 3, 5.0, '2023-02-11'),
                        (34, 4, 3.0, '2023-02-12'),
                        (35, 5, 4.5, '2023-02-13'),
                        (36, 6, 4.0, '2023-02-14'),
                        (37, 7, 3.5, '2023-02-15'),
                        (38, 8, 2.5, '2023-02-16'),
                        (39, 9, 5.0, '2023-02-17'),
                        (40, 10, 3.5, '2023-02-18'),
                        (10, 1, 4.0, '2023-02-19'),
                        (20, 2, 3.0, '2023-02-20'),
                        (30, 3, 2.0, '2023-02-21'),
                        (40, 4, 5.0, '2023-02-22'),
                        (1, 5, 3.5, '2023-02-23'),
                        (11, 6, 2.5, '2023-02-24'),
                        (21, 7, 4.0, '2023-02-25'),
                        (31, 8, 4.5, '2023-02-26'),
                        (41, 9, 3.0, '2023-02-27'),
                        (2, 10, 5.0, '2023-02-28'),
                        (12, 1, 2.5, '2023-03-01'),
                        (22, 2, 3.5, '2023-03-02'),
                        (32, 3, 4.0, '2023-03-03'),
                        (42, 4, 3.0, '2023-03-04'),
                        (3, 5, 4.5, '2023-03-05'),
                        (13, 6, 3.5, '2023-03-06'),
                        (23, 7, 4.0, '2023-03-07'),
                        (33, 8, 2.5, '2023-03-08'),
                        (43, 9, 3.0, '2023-03-09'),
                        (4, 10, 5.0, '2023-03-10'),
                        (14, 1, 4.0, '2023-03-11'),
                        (24, 2, 3.0, '2023-03-12'),
                        (34, 3, 4.5, '2023-03-13'),
                        (44, 4, 2.0, '2023-03-14'),
                        (5, 5, 3.5, '2023-03-15'),
                        (15, 6, 4.0, '2023-03-16'),
                        (25, 7, 2.5, '2023-03-17'),
                        (35, 8, 4.5, '2023-03-18'),
                        (45, 9, 3.0, '2023-03-19'),
                        (6, 10, 4.0, '2023-03-20'),
                        (16, 1, 2.0, '2023-03-21'),
                        (26, 2, 4.5, '2023-03-22'),
                        (36, 3, 3.5, '2023-03-23'),
                        (45, 4, 4.0, '2023-03-24'),
                        (7, 5, 3.0, '2023-03-25'),
                        (17, 6, 4.5, '2023-03-26'),
                        (27, 7, 2.5, '2023-03-27'),
                        (37, 8, 3.5, '2023-03-28'),
                        (17, 9, 4.0, '2023-03-29'),
                        (8, 10, 2.0, '2023-03-30'),
                        (18, 1, 3.0, '2023-03-31'),
                        (28, 2, 2.5, '2023-04-01'),
                        (38, 3, 4.5, '2023-04-02'),
                        (38, 4, 4.0, '2023-04-03'),
                        (9, 5, 3.5, '2023-04-04'),
                        (19, 6, 5.0, '2023-04-05'),
                        (29, 7, 4.0, '2023-04-06'),
                        (39, 8, 3.0, '2023-04-07'),
                        (42, 9, 4.5, '2023-04-08'),
                        (10, 10, 4.0, '2023-04-09'),
                        (20, 1, 3.5, '2023-04-10'),
                        (30, 2, 5.0, '2023-04-11'),
                        (40, 3, 3.0, '2023-04-12'),
                        (1, 4, 2.0, '2023-04-13'),
                        (11, 5, 4.0, '2023-04-14'),
                        (21, 6, 3.5, '2023-04-15'),
                        (31, 7, 5.0, '2023-04-16'),
                        (41, 8, 3.0, '2023-04-17'),
                        (2, 9, 4.0, '2023-04-18'),
                        (12, 10, 2.5, '2023-04-19'),
                        (22, 1, 3.5, '2023-04-20'),
                        (32, 2, 4.0, '2023-04-21'),
                        (42, 3, 2.0, '2023-04-22'),
                        (3, 4, 4.0, '2023-04-23'),
                        (13, 5, 3.5, '2023-04-24'),
                        (23, 6, 5.0, '2023-04-25'),
                        (33, 7, 2.0, '2023-04-26'),
                        (43, 8, 3.0, '2023-04-27'),
                        (4, 9, 4.5, '2023-04-28'),
                        (14, 10, 3.0, '2023-04-29'),
                        (24, 1, 4.0, '2023-04-30'),
                        (34, 2, 2.5, '2023-05-01'),
                        (44, 3, 3.0, '2023-05-02'),
                        (5, 4, 4.5, '2023-05-03'),
                        (15, 5, 2.5, '2023-05-04'),
                        (25, 6, 4.0, '2023-05-05'),
                        (35, 7, 3.0, '2023-05-06'),
                        (45, 8, 5.0, '2023-05-07'),
                        (6, 9, 2.0, '2023-05-08'),
                        (16, 10, 4.0, '2023-05-09'),
                        (26, 3, 5.0, '2023-05-10'),
                        (36, 4, 2.5, '2023-05-11'),
                        (2, 5, 4.5, '2023-05-12'),
                        (7, 6, 3.0, '2023-05-13'),
                        (17, 7, 3.5, '2023-05-14'),
                        (27, 8, 4.0, '2023-05-15'),
                        (37, 9, 2.0, '2023-05-16'),
                        (42, 10, 4.5, '2023-05-17'),
                        (8, 1, 3.0, '2023-05-18'),
                        (18, 2, 3.5, '2023-05-19'),
                        (28, 3, 4.0, '2023-05-20'),
                        (38, 4, 5.0, '2023-05-21'),
                        (28, 5, 2.5, '2023-05-22'),
                        (9, 6, 4.5, '2023-05-23'),
                        (19, 7, 3.0, '2023-05-24'),
                        (29, 8, 2.0, '2023-05-25'),
                        (39, 9, 4.0, '2023-05-26'),
                        (43, 10, 3.5, '2023-05-27'),
                        (10, 1, 2.5, '2023-05-28'),
                        (20, 2, 4.5, '2023-05-29'),
                        (30, 3, 3.0, '2023-05-30'),
                        (40, 4, 2.0, '2023-05-31'),
                        (1, 5, 4.0, '2023-06-01'),
                        (11, 6, 3.5, '2023-06-02'),
                        (21, 7, 2.5, '2023-06-03'),
                        (31, 8, 4.0, '2023-06-04'),
                        (41, 9, 3.0, '2023-06-05'),
                        (2, 10, 4.5, '2023-06-06'),
                        (12, 1, 3.0, '2023-06-07'),
                        (22, 2, 4.5, '2023-06-08'),
                        (32, 3, 2.5, '2023-06-09'),
                        (42, 4, 3.5, '2023-06-10'),
                        (3, 5, 4.0, '2023-06-11'),
                        (13, 6, 2.0, '2023-06-12'),
                        (23, 7, 4.5, '2023-06-13'),
                        (33, 8, 3.0, '2023-06-14'),
                        (43, 9, 4.0, '2023-06-15'),
                        (4, 10, 2.0, '2023-06-16');
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}