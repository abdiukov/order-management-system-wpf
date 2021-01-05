using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

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
            //_connectionString = "Data Source=DESKTOP-SDCBG14\\SQLEXPRESS;Initial Catalog=OrderManagementDb;Integrated Security=True";
        }


    }

}