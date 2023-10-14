using MySqlConnector;
using System.Data;

namespace BuinessLogic
{
    public class MySQLDatabase : IDatabase, IDisposable
    {
        private const string HOST = "sql8.freesqldatabase.com";
        private const string USER = "sql8653133";
        private const string DATABASE = "sql8653133";
        private const string PASSWORD = "kp5mhEhU8Z";

        private string _connectionString = $"Server={HOST}; Database={DATABASE}; Uid={USER}; Pwd={PASSWORD};";

        private MySqlConnection _connection;

        public MySQLDatabase()
        {

            _connection = new MySqlConnection(_connectionString);


        }

        public void Dispose()
        {
        }

        public IEnumerable<Housemate> GetHouseMates()
        {
            List<Housemate> housemates = new();

            _connection.Open();


            var query = "SELECT * FROM `Housemates`";
            var command = new MySqlCommand(query, _connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (int)reader["ID"];
                    var name = (string)reader["Name"];
                    var dob = (DateTime)reader["DOB"];
                    var emoji = (string)reader["Emoji"];

                    housemates.Add(new Housemate(id.ToString(), name, dob, emoji));
                }
            }

            _connection.Close();


            return housemates;
        }

        public async void Save(IEnumerable<Housemate> housemates)
        {
            if (_connection.State == ConnectionState.Open) return;


            _connection.Open();
            using (MySqlCommand command = new())
            {
                command.Connection = _connection;

                var count = 1;
                // Loop through your IEnumerable<Housemate>
                foreach (Housemate housemate in housemates)
                {
                    command.CommandText = "UPDATE Housemates SET  Name = @Name, DOB=@DOB, Emoji=@Emoji WHERE ID= @ID";


                    // Set the parameter values from the Housemate object
                    command.Parameters.AddWithValue("@ID", count);
                    command.Parameters.AddWithValue("@Name", housemate.Name);
                    command.Parameters.AddWithValue("@DOB", housemate.DOB);
                    command.Parameters.AddWithValue("@Emoji", count++.ToString());

                    // Execute the SQL command
                    await command.ExecuteNonQueryAsync();

                    command.Parameters.Clear();

                }
            }
            _connection.Close();

        }


    }
}
