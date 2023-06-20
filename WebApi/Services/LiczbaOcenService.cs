using Microsoft.Data.SqlClient;
using System;

namespace WebApi.Services
{
    public interface ILiczbaOcenService
    {
        public string GetLiczbaOcen(DateTime startDate, DateTime endDate, int studentId);
    }
    public class LiczbaOcenService : ILiczbaOcenService
    {
        private readonly string _connectionString;

        public LiczbaOcenService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetLiczbaOcen(DateTime startDate, DateTime endDate, int studentId)
        {
            string result = string.Empty;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                SELECT 
                    dbo.LiczbaOcen(O.Ocena, O.Data, StudentId) AS LiczbaOcen
                FROM 
                    Oceny O
                WHERE
                    O.Data BETWEEN @StartDate AND @EndDate
                    AND O.StudentId = @StudentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader["LiczbaOcen"].ToString();
                        }
                    }
                }
            }

            return result;
        }
    }
}

