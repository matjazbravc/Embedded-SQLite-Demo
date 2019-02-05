using System;
using System.Data.SQLite;
using System.IO;

namespace EmbeddedSQLiteDemo
{
    internal class Program
    {
        private const string CREATE_TEST_TABLE_QUERY = 
            @"CREATE TABLE IF NOT EXISTS [TestTable] (
               [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
               [FirstName] NVARCHAR(64) NULL,
               [LastName] NVARCHAR(64) NULL)";

        private const string DATABASE_FILE = "EmbeddedSQLiteDemo.db";

        private static void Main()
        {
            // If exists - recreate database
            if (File.Exists(DATABASE_FILE))
            {
                File.Delete(DATABASE_FILE);
                SQLiteConnection.CreateFile(DATABASE_FILE);
            }

            using (var connection = new SQLiteConnection($"data source={DATABASE_FILE};Version=3;New=True;Compress=True;"))
            {
                using (var command = new SQLiteCommand(connection))
                {
                    connection.Open();

                    // Create the TestTable
                    command.CommandText = CREATE_TEST_TABLE_QUERY;
                    command.ExecuteNonQuery();

                    // Insert some test data
                    command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('John', 'Snow')";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('Alice', 'Cooper')";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO [TestTable] (FirstName, LastName) VALUES ('Suzane', 'Thompson')";
                    command.ExecuteNonQuery();

                    // Select all data
                    command.CommandText = "SELECT * FROM [TestTable]";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Id"] + " : " + reader["FirstName"] + " " + reader["LastName"]);
                        }
                    }

                    connection.Close();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press <Enter> key to exit.");
            Console.ReadLine();
        }
    }
}