using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pax360.Interfaces;
using Pax360.Models;

namespace Pax360.Helpers
{
    public class CounterHelper : ICounterHelper
    {
        private IOptions<ExternalDBSettings> _externalConfig;

        public CounterHelper(IOptions<ExternalDBSettings> externalConfig)
        {
            _externalConfig = externalConfig;
        }
        public decimal GetPrice(string banka, string model)
        {
            using (SqlConnection connection = new SqlConnection(_externalConfig.Value.CounterConnectionString))
            {
                connection.Open();

                try
                {
                    string query = string.Format("SELECT *  FROM Fiyatlar2 where Banka ='{0}' and Model ='{1}'", banka, model);
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return 0;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            return decimal.Parse(reader["Fiyat"].ToString());
                        }
                        reader.Close();
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
