﻿using MySqlConnector;
using System.Data;

namespace BuinessLogic
{
    public class MySQLDatabase : IDatabase, IDisposable
    {
        private MySqlConnection? _connection;
        public IEnumerable<Housemate>? _housemates;

        public event EventHandler<IEnumerable<Housemate>> ListUpdated;

        private SemaphoreSlim _semaphore = new(1);
        private CancellationTokenSource TokenSource = new();
        private readonly string _connectionString;

        public MySQLDatabase(string connectionString)
        {

            _connectionString = connectionString;
            Task.Run(Initalise).Wait();
        }

        public async void Initalise()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();

            await PeriodicQuery();
        }

        private void OnUpdateRecived(IEnumerable<Housemate> e)
        {
            ListUpdated?.Invoke(this, e);
        }

        private async Task PeriodicQuery()
        {
            while (!TokenSource.IsCancellationRequested)
            {
                await Task.Delay(new TimeSpan(0, 0, 30));
                await GetHousemates();
            }
        }

        public async Task<IEnumerable<Housemate>> GetHousemates()
        {
            _semaphore.Wait();
            try
            {
                List<Housemate> housemates = new();

                var query = "SELECT * FROM `Housemates`";
                var command = new MySqlCommand(query, _connection);

                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var id = (int)reader["ID"];
                        var name = (string)reader["Name"];
                        var dob = (DateTime)reader["DOB"];
                        var emoji = (string)reader["Emoji"];

                        housemates.Add(new Housemate(id, name, dob, emoji));
                    }
                }
                var orderedList = housemates.OrderBy(h => h.ID);
                OnUpdateRecived(orderedList);

                return orderedList;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            TokenSource.Cancel();
            _semaphore.Wait();
            try
            {
                _connection.Close();
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async void Save(IEnumerable<Housemate> housemates)
        {

            _semaphore.Wait();
            try
            {
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
                        command.Parameters.AddWithValue("@Emoji", housemate.Emoji);

                        // Execute the SQL command
                        await command.ExecuteNonQueryAsync();

                        command.Parameters.Clear();
                        count++;

                    }
                }
            }
            finally
            {
                _semaphore.Release();
                await GetHousemates();
            }


        }


    }
}
