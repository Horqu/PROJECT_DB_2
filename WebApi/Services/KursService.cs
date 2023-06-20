using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class KursService
    {
        private readonly string _connectionString;

        public KursService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<string, string> GetCzestoscPiatkiForKursId(int kursId)
        {
            var result = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"

            SELECT 
                Kursy.NazwaKursu, 
                dbo.CzestoscPiatki(Oceny.Ocena) AS CzestoscPiatki
            FROM 
                Kursy
            JOIN 
                Oceny ON Kursy.KursId = Oceny.KursId
            WHERE 
                Kursy.KursId = @KursId
            GROUP BY 
                Kursy.NazwaKursu
            ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KursId", kursId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaKursu = reader["NazwaKursu"].ToString();
                            string czestoscPiatki = reader["CzestoscPiatki"].ToString();

                            result.Add(nazwaKursu, czestoscPiatki);
                        }
                    }
                }

                // Połączenie zostanie automatycznie zamknięte, gdy wyjdziemy z bloku 'using'
            }

            return result;
        }
    }
}
