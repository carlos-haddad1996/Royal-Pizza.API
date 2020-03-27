using System;
//using System.Data.SQLite;
using Microsoft.Data.Sqlite;
namespace Royal_Pizza.API
{
    public class Database
    {
        public SqliteConnection dbConnection;

        public Database()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "crunchyDB.db";
            dbConnection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        }
    }
}
