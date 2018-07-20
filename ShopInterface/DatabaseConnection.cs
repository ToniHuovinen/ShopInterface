using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopInterface
{
    public static class DatabaseConnection
    {
        public static void GetFromDatabase(string query)
        {
            // Create connection, command and reader. Change the connection string according to your needs, mines just default local db for debug use
            // Using phpMyAdmin (XAMPP) as database
            MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=store");
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            MySqlDataReader reader;

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Form1.productListing.Add(new Product(reader.GetInt32(1), reader.GetString(2), (decimal)reader.GetFloat(3)));
            }
            reader.Close();
            connection.Close();
        }

        public static void SaveToDatabase()
        {

        }
    }
}
