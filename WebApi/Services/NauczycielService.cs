using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace WebApi.Services
{
    public interface INauczycielService
    {
        public Dictionary<string, string> GetNauczycielIdAndSredniaOcen(int nauczycielId);
    }
    public class NauczycielService : INauczycielService
    {
        private readonly string _connectionString;

        public NauczycielService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<string, string> GetNauczycielIdAndSredniaOcen(int nauczycielId)
        {
            var result = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                SELECT 
                    K.NauczycielId,
                    dbo.SredniaOcenNauczyciela(O.Ocena) AS SredniaOcena
                FROM 
                    Oceny O
                JOIN 
                    Kursy K ON O.KursId = K.KursId
                WHERE
                    K.NauczycielId = @NauczycielId
                GROUP BY 
                    K.NauczycielID
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NauczycielId", nauczycielId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String nauczycielIdResult = reader["NauczycielId"].ToString();
                            String sredniaOcenResult = reader["SredniaOcena"].ToString();

                            result.Add(nauczycielIdResult, sredniaOcenResult);
                        }
                    }
                }

                // Połączenie zostanie automatycznie zamknięte, gdy wyjdziemy z bloku 'using'
            }

            return result;
        }
    }
}
