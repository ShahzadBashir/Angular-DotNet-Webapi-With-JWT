using Npgsql;
using System;

namespace Commons
{
    public class DbConnection
    {
        public static string DbConnectionString { get; set; }

        public DbConnection()
        {
            DbConnectionString = "helo";
        }
    }
}
