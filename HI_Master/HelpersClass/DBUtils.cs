using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HI_Master
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "192.168.1.157";
            int port = 3306;
            string database = "handisitedb";
            string username = "root";
            string password = "root";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
