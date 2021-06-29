using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class DB
    {
        //MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=main");
        //MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=main;password=kne)+uIUI8=#");
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=main;password=password");
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            } 
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
