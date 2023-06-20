using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IOcenaService
    {
        List<Ocena> GetOceny();
        public void DodajOcene(Ocena nowaOcena);
    }
    public class OcenaService : IOcenaService
    {
        private readonly string _connectionString;

        public OcenaService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DodajOcene(Ocena nowaOcena)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Oceny (KursId, StudentId, Ocena, Data) VALUES (@KursId, @StudentId, @Wartosc, @Data)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KursId", nowaOcena.KursId);
                    command.Parameters.AddWithValue("@StudentId", nowaOcena.StudentId);
                    command.Parameters.AddWithValue("@Wartosc", nowaOcena.Wartosc);
                    command.Parameters.AddWithValue("@Data", nowaOcena.Data);

                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Ocena> GetOceny()
        {
            List<Ocena> oceny = new List<Ocena>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Oceny";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ocena ocena = new Ocena
                            {


                                OcenaId = reader.GetInt32(0),
                                KursId = reader.GetInt32(1),
                                StudentId = reader.GetInt32(2),
                                Wartosc = reader.GetDecimal(3),
                                Data = reader.GetDateTime(4),

                            };

                            oceny.Add(ocena);
                        }
                    }
                }

            }

            return oceny;
        }
    }
}
