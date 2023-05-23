using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrs_OOP_CSharp
{
    public class Repository : IRepository, IDisposable
    {

        public string FilePath { get; set; }
        public SqliteConnection connection;
        private bool disposed = false;
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
                    author_surname INTEGER NOT NULL,
                    theme TEXT NOT NULL
                )";
            command.ExecuteNonQuery();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS customers (
                    id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    name TEXT NOT NULL,
                    surname TEXT NOT NULL,
                    book_id INTEGER NOT NULL,
                    taking_data TEXT NOT NULL,
                    return_data TEXT NOT NULL,
                    FOREIGN KEY (book_id) REFERENCES books (id)
                )";
            command.ExecuteNonQuery();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS debtors (
                    customer_id INTEGER,
                    book_id INTEGER,
                    FOREIGN KEY (customer_id) REFERENCES customers (customer_id),
                    FOREIGN KEY (book_id) REFERENCES books (book_id),
                    PRIMARY KEY (customer_id, book_id)
                )";
            command.ExecuteNonQuery();
        }

        public List<Customer> GetAllCutomers()
        {
            throw new NotImplementedException();
        }

        public void GetAllBooks(DataGridView dataGridView)
        {
            using (var command = new SqliteCommand(@"
                    SELECT book_name AS Column1, author_name AS Column2, author_surname AS Column3, theme AS Column4 
                    FROM books", 
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    //// Игнорируйте ненужные столбцы
                    //dataTable.Columns.Remove("ColumnName");

                    // Установите DataTable как источник данных для DataGridView
                    dataGridView.DataSource = dataTable;
                }
            }
        }

        public List<Customer> GetAllDebtors()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
