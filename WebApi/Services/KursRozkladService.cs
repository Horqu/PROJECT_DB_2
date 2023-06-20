using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace WebApi.Services
{
    public class KursRozkladService
    {
        private readonly string _connectionString;

        public KursRozkladService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<string, string> GetKursIdAndRozkladOcen(int kursId)
        {
            var result = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                SELECT 
                    K.KursID,
                    dbo.RozkladOcen(O.Ocena) AS RozkladOcen
                FROM 
                    Oceny O
                JOIN 
                    Kursy K ON O.KursID = K.KursID
                WHERE
                    K.KursID = @KursId
                GROUP BY 
                    K.KursID
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KursId", kursId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            String kursIdResult = reader["KursID"].ToString();
                            String rozkladOcenResult = reader["RozkladOcen"].ToString();

                            result.Add(kursIdResult, rozkladOcenResult);
                        }
                    }
                }

                // Połączenie zostanie automatycznie zamknięte, gdy wyjdziemy z bloku 'using'
            }

            return result;
        }
    }
}
