using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace App_MVC.Utils
{
    public class DbConnection
    {
        private static DbConnection instance;
        //private readonly IConfiguration _configuration;
        private string connectionString;

        private DbConnection(IConfiguration configuration)
        {
            // Retrieve the connection string from the web.config file
            connectionString = configuration.GetConnectionString("DbConn").ToString();
        }

        public static DbConnection Instance(IConfiguration configuration)
        {
             if (instance == null)
             {
                 instance = new DbConnection(configuration);
             }
             return instance;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
