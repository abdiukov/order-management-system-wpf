using System.Configuration;

namespace DataAccess
{
    public class Repository
    {

        readonly public string connectionString;

        /// <summary>
        /// Repository class connects to the database
        /// That connection is stored in the _connectionString.
        /// </summary>
        public Repository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }


    }

}