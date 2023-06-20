using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class StudentService
    {
        private readonly string _connectionString;

        public StudentService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            // Zapytanie SQL do pobrania wszystkich studentów
            string sqlQuery = "SELECT * FROM Studenci";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Otwórz połączenie
                connection.Open();

                // Wykonaj zapytanie SQL
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Przeczytaj wyniki zapytania
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            Imie = reader["Imie"].ToString(),
                            Nazwisko = reader["Nazwisko"].ToString(),
                            DataUrodzenia = Convert.ToDateTime(reader["DataUrodzenia"]),
                            Klasa = reader["Klasa"].ToString()
                        };

                        // Dodaj studenta do listy
                        students.Add(student);
                    }
                }

                // Połączenie zostanie zamknięte automatycznie, gdy wyjdziemy z bloku 'using'
            }

            return students;
        }
    }


}
