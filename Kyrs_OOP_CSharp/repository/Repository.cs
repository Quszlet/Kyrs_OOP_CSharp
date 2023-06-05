using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrs_OOP_CSharp.repository
{
    public class Repository
    {

        public string FilePath { get; set; }
        public SqliteConnection connection { get; set; }

        public Repository(string filePath)
        {
            FilePath = filePath;
            connection = new SqliteConnection($"Data source={filePath}.db");
            SqliteCommand command = new SqliteCommand();
            connection.Open();
            command.Connection = connection;

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS books (
                    id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    book_name TEXT NOT NULL,
                    author_name TEXT NOT NULL,
                    author_surname TEXT NOT NULL,
                    theme TEXT NOT NULL
                )";
            command.ExecuteNonQuery();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS customers (
                    id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    name TEXT NOT NULL,
                    surname TEXT NOT NULL,
                    book_id INTEGER NOT NULL,
                    taking_data DATETIME NOT NULL,
                    return_data DATETIME NOT NULL,
                    FOREIGN KEY (book_id) REFERENCES books (id) ON DELETE CASCADE
                )";
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
