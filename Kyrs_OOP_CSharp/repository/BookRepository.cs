using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Kyrs_OOP_CSharp.repository
{
    public class BookRepository : Repository, IBookRepository
    {
        public BookRepository(string filePath) : base(filePath)
        {
        }
        public void GetAllBooks(DataGridView dataGridView)
        {
            connection.Open();
            using (var command = new SqliteCommand(@"
                    SELECT id, book_name, author_name, 
                    author_surname, theme FROM books",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataTable.Columns[1].ColumnName = "Название книги";
                    dataTable.Columns[2].ColumnName = "Имя автора";
                    dataTable.Columns[3].ColumnName = "Фамилия автора";
                    dataTable.Columns[4].ColumnName = "Тематика";

                    dataGridView.Columns.Clear();
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.ClearSelection();
                }
            }
            connection.Close();
        }


        public void SaveBook(Book book)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    INSERT INTO books (book_name, author_name, author_surname, theme) 
                    VALUES ('{book.NameBook}', '{book.AuthorName}', '{book.AuthorSurname}', '{book.Theme}')",
                    connection))
            {

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void UpdateBook(Book book)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    UPDATE books SET book_name = '{book.NameBook}', author_name = '{book.AuthorName}', 
                    author_surname = '{book.AuthorSurname}', theme = '{book.Theme}' WHERE id = {book.Id}",
                    connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void DeleteBook(int id)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    DELETE FROM books WHERE id = {id}",
                    connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void FiltrationBooks(DataGridView dataGridView, string parameter, string value)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT id, book_name, author_name, author_surname, theme
                    FROM books WHERE {parameter} LIKE '%{value}%'",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataTable.Columns[1].ColumnName = "Название книги";
                    dataTable.Columns[2].ColumnName = "Имя автора";
                    dataTable.Columns[3].ColumnName = "Фамилия автора";
                    dataTable.Columns[4].ColumnName = "Тематика";

                    dataGridView.Columns.Clear();
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.ClearSelection();
                }
            }
            connection.Close();
        }

        public void FindBooks(DataGridView dataGridView, Book book)
        {
            dataGridView.ClearSelection();
            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT id FROM books WHERE book_name LIKE '%{book.NameBook}%' AND author_name LIKE '%{book.AuthorName}%'
                    AND author_surname LIKE '%{book.AuthorSurname}%' AND theme LIKE '%{book.Theme}%'",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    int id;
                    while (reader.Read())
                    {
                        id = reader.GetInt32("id");
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (Convert.ToInt32(row.Cells[0].Value) == id)
                            {
                                row.Selected = true;
                            }
                        }
                    }
                }
            }
            connection.Close();
        }

        public int GetBook(string name, string authorName, string authorSurname)
        {
            int id = -1;
            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT id FROM books WHERE book_name = '{name}' AND author_name = '{authorName}'
                    AND author_surname = '{authorSurname}'",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("id");
                        
                    }
                }
            }
            connection.Close();
            return id;
        }

        public List<string> GetAllAuthors()
        {
            List<string> authors = new List<string>();
            connection.Open();
            using (var command = new SqliteCommand($@"SELECT DISTINCT author_name, author_surname FROM books", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        authors.Add($"{reader.GetString("author_name")} {reader.GetString("author_surname")}");

                    }
                }
            }
            connection.Close();
            return authors;
        }

        public List<string> GetAllBooksAuthors(string author)
        {
            string[] authorNameSurname = author.Split(" ");
            List<string> booksName = new List<string>();
            connection.Open();
            using (var command = new SqliteCommand($@"SELECT DISTINCT book_name FROM books WHERE 
                    author_name = '{authorNameSurname[0]}' AND author_surname = '{authorNameSurname[1]}'", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        booksName.Add(reader.GetString("book_name"));

                    }
                }
            }
            connection.Close();
            return booksName;
        }
    }
}

