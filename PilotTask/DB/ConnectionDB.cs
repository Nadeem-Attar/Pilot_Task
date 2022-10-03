
using System.Configuration;
using System.Data.SqlClient;

namespace PilotTask.DB
{
    public class ConnectionDB
    {
        public static SqlConnection SqlConnectionDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["pilot_task"].ToString();
            return new SqlConnection(connectionString);
        }
    }
}