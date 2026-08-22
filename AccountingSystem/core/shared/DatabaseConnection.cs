using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.core.Classes
{
    public class DatabaseConnection
    {
        private string connectionString;
        private const string ConnectionStringName = "AccountingDbContext";
        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DatabaseConnection()
        {

        }
        static public SqlConnection GetConnection()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    $"Connection string '{ConnectionStringName}' was not found in App.config.");
            }

            return new SqlConnection(settings.ConnectionString);
        }
    }

}
