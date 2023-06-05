﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrs_OOP_CSharp.repository
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public CustomerRepository(string filePath) : base(filePath)
        {
        }

        public void DeleteCustomer(int id)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    DELETE FROM customers WHERE id = {id}",
                    connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void UpdateCustomer(int id, string name, string surname, string returnDate)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    UPDATE customers SET name = '{name}', surname = '{surname}', 
                    return_data = '{returnDate}' WHERE id = {id}",
                    connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void FiltrationCustomers(DataGridView dataGridView, string parameter, string value)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT customers.id, customers.name, customers.surname, books.book_name,
                    books.author_name || ' ' || books.author_surname AS author, 
                    customers.taking_data, customers.return_data  
                    FROM customers JOIN books ON customers.book_id = books.id WHERE {parameter} LIKE '%{value}%'",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataTable.Columns[1].ColumnName = "Имя";
                    dataTable.Columns[2].ColumnName = "Фамилия";
                    dataTable.Columns[3].ColumnName = "Название книги";
                    dataTable.Columns[4].ColumnName = "Автор";
                    dataTable.Columns[5].ColumnName = "Дата взятия книги";
                    dataTable.Columns[6].ColumnName = "Дата возврата книги";

                    dataTable.Columns.Add("Просрочено дней");

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DateTime currentDate = DateTime.Now.Date;

                        DateTime returnDate = DateTime.ParseExact(dataTable.Rows[i][6].ToString(),
                                                               "dd-MM-yyyy", null);

                        TimeSpan overdueDays = currentDate - returnDate;

                        if (overdueDays.TotalDays > 0)
                        {
                            dataTable.Rows[i][7] = overdueDays.TotalDays;
                        }
                        else
                        {
                            dataTable.Rows[i][7] = 0;
                        }
                    }


                    dataGridView.Columns.Clear();
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.ClearSelection();
                }
            }
            connection.Close();
        }

        public void FindCustomers(DataGridView dataGridView, string name, string surname)
        {
            dataGridView.ClearSelection();
            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT id FROM customers WHERE name LIKE '%{name}%' AND surname LIKE '%{surname}%'",
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

        public void GetAllCustomers(DataGridView dataGridView)
        {
            connection.Open();
            using (var command = new SqliteCommand(@"
                    SELECT customers.id, customers.name, customers.surname, books.book_name,
                    books.author_name || ' ' || books.author_surname AS author, 
                    customers.taking_data, customers.return_data  
                    FROM customers JOIN books ON customers.book_id = books.id;",
                    connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataTable.Columns[1].ColumnName = "Имя";
                    dataTable.Columns[2].ColumnName = "Фамилия";
                    dataTable.Columns[3].ColumnName = "Название книги";
                    dataTable.Columns[4].ColumnName = "Автор";
                    dataTable.Columns[5].ColumnName = "Дата взятия книги";
                    dataTable.Columns[6].ColumnName = "Дата возврата книги";

                    dataTable.Columns.Add("Просрочено дней");

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DateTime currentDate = DateTime.Now.Date;

                        DateTime returnDate = DateTime.ParseExact(dataTable.Rows[i][6].ToString(),
                                                               "dd-MM-yyyy", null);

                        TimeSpan overdueDays = currentDate - returnDate;

                        if (overdueDays.TotalDays > 0)
                        {
                            dataTable.Rows[i][7] = overdueDays.TotalDays;
                        }
                        else
                        {
                            dataTable.Rows[i][7] = 0;
                        }
                    }


                    dataGridView.Columns.Clear();
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.ClearSelection();
                }
            }
            connection.Close();
        }

        public void SaveCustomer(Customer customer)
        {
            connection.Open();
            using (var command = new SqliteCommand($@"
                    INSERT INTO customers (name, surname, book_id, taking_data, return_data) 
                    VALUES ('{customer.Name}', '{customer.Surname}', '{customer.IdBook}', '{customer.TakingDate}',
                    '{customer.ReturnDate}')",
                    connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public int GetCustomer(string name, string surname, int bookId)
        {
            int id = -1;

            connection.Open();
            using (var command = new SqliteCommand($@"
                    SELECT id FROM customers WHERE name = '{name}' AND surname = '{surname}'
                    AND book_id = '{bookId}'",
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
    }
}
