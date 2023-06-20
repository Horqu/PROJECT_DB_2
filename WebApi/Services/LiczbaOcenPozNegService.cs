using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApi.Services
{
    public class LiczbaOcenPozNegService
    {
        private readonly string _connectionString;

        public LiczbaOcenPozNegService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetLiczbaOcenPozNeg(int kursId)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                SELECT 
                    dbo.LiczbaOcenPozNeg(O.Ocena) AS LiczbaOcenPozNeg
                FROM 
                    Oceny O
                WHERE
                    O.KursId = @KursId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KursId", kursId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["LiczbaOcenPozNeg"].ToString();
                        }
                    }
                }
            }

            return result;
        }
    }
}
