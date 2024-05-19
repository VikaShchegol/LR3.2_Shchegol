using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConsoleApp6
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {


            string host = "localhost";
            int port = 3306;
            string database = "lr1_shchegol_27";
            string username = "monty";
            string password = "some_pass";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
