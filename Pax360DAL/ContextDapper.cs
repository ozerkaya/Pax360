using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL
{
    public class ContextDapper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ContextDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Context");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
