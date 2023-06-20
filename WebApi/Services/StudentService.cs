using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class StudentService
    {
        private readonly SqlConnection _connection;

        public StudentService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            // Zapytanie SQL do pobrania wszystkich studentów
            string sqlQuery = "SELECT * FROM Studenci";

            using (SqlCommand command = new SqlCommand(sqlQuery, _connection))
            {
                // Otwórz połączenie
                _connection.Open();

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

                // Zamknij połączenie
                _connection.Close();
            }

            return students;
        }
    }

}
