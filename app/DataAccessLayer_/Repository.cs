using System.Configuration;

namespace DataAccess
{
    public class Repository
    {

        readonly public string _connectionString;

        /// <summary>
        /// Repository class connects to the database
        /// That connection is stored in the _connectionString.
        /// </summary>
        public Repository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }


    }

}