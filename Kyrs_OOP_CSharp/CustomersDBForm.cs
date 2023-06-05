﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kyrs_OOP_CSharp.repository;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace Kyrs_OOP_CSharp
{
    public partial class CustomersDBForm : Form
    {
        private CustomerRepository CustomerRepository;
        private BookRepository BookRepository;
        private string FilePathDB;
        private int idChange = 1;
        private int indRow = 0;
        public CustomersDBForm(string filePath)
        {
            InitializeComponent();
            CustomerRepository = new CustomerRepository(filePath);
            BookRepository = new BookRepository(filePath);
            FilePathDB = filePath;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void CustomersDBForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dateTimePicker2.Value = dateTimePicker2.Value.AddDays(1);
            CustomerRepository.GetAllCustomers(dataGridView1);
            dataGridView1.ClearSelection();
            comboBox3.Items.AddRange(BookRepository.GetAllAuthors().ToArray());
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите имя клиента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("Введите фамилию клиента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string customerName, customerSurname, returnDate, takingDate, author, bookName;
            string[] authorNameSurname;
            int bookId;
            Customer customer;
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    button4.Text = "Показать должников";
                    customerName = textBox2.Text;
                    customerSurname = textBox3.Text;
                    takingDate = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                    returnDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                    author = comboBox3.Text;
                    bookName = comboBox4.Text;
                    authorNameSurname = author.Split(" ");
                    bookId = BookRepository.GetBook(bookName, authorNameSurname[0], authorNameSurname[1]);

                    if (dateTimePicker2.Value < dateTimePicker1.Value)
                    {
                        MessageBox.Show("Дата возврата должна быть позже даты взятия!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (CustomerRepository.GetCustomer(customerName, customerSurname, bookId) != -1)
                    {
                        MessageBox.Show("Данный клиент уже взял такую книгу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    customer = new Customer(customerName, customerSurname, bookId, takingDate, returnDate);
                    CustomerRepository.SaveCustomer(customer);
                    CustomerRepository.GetAllCustomers(dataGridView1);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Selected = true;
                    MessageBox.Show("Новая клиент добавлен", "Учет клиентов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    button4.Text = "Показать должников";
                    customerName = textBox2.Text;
                    customerSurname = textBox3.Text;
                    returnDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                    CustomerRepository.UpdateCustomer(idChange, customerName, customerSurname, returnDate);
                    CustomerRepository.GetAllCustomers(dataGridView1);
                    dataGridView1.ClearSelection();
                    MessageBox.Show("Клиент изменен", "Учет клиентов", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    customerName = textBox2.Text;
                    customerSurname = textBox3.Text;
                    CustomerRepository.FindCustomers(dataGridView1, customerName, customerSurname);
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string? author = comboBox3.SelectedItem.ToString();
            comboBox4.Items.AddRange(BookRepository.GetAllBooksAuthors(author).ToArray());
            comboBox4.SelectedIndex = 0;
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^\p{IsCyrillic}+$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                textBox.BackColor = Color.Red;
                MessageBox.Show("Ввести необходимо буквы русского алфавита", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox.BackColor = Color.White;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^\p{IsCyrillic}+$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                textBox.BackColor = Color.Red;
                MessageBox.Show("Ввести необходимо буквы русского алфавита", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox.BackColor = Color.White;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    button1.Text = "Добавить";
                    dateTimePicker2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    break;
                case 1:
                    button1.Text = "Изменить";
                    dateTimePicker2.Enabled = true;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    break;
                case 2:
                    button1.Text = "Найти";
                    dateTimePicker2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    break;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridView dataGridView = (DataGridView)sender;
                DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
                textBox2.Text = dataGridViewRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridViewRow.Cells[2].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                comboBox3.SelectedItem = dataGridViewRow.Cells[4].Value;
                comboBox4.SelectedItem = dataGridViewRow.Cells[3].Value;
            }
        }
        private void dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            if (dateTimePicker.Value.ToString("dd-MM-yyyy") == dateTimePicker.MinDate.ToString("dd-MM-yyyy"))
            {
                e.Cancel = true;
                dateTimePicker.Focus();
                MessageBox.Show("Нельзя выбрать эту дату!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4.Text = "Показать должников";
            CustomerRepository.DeleteCustomer(idChange);
            CustomerRepository.GetAllCustomers(dataGridView1);
            dataGridView1.ClearSelection();
            MessageBox.Show("Клиент удалена", "Учет клиентов", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                indRow = selectedRow.Index;
                idChange = Convert.ToInt32(dataGridView1.Rows[indRow].Cells[0].Value);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                label1.Text = "Фильрация включена";
                label1.ForeColor = Color.Green;
                string parameter = "";
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        parameter = "name";
                        break;
                    case 1:
                        parameter = "surname";
                        break;
                    case 2:
                        parameter = "books.book_name";
                        break;
                    case 3:
                        parameter = "books.author_name || ' ' || books.author_surname";
                        break;
                    case 4:
                        parameter = "taking_data";
                        break;
                    case 5:
                        parameter = "return_data";
                        break;
                    case 6:
                        string? cellValue;
                        for (int i = dataGridView1.Rows.Count - 2; i >= 0; i--)
                        {
                            cellValue = dataGridView1.Rows[i].Cells[7].Value.ToString();
                            if (!cellValue.Contains(textBox1.Text))
                            {
                                dataGridView1.Rows.RemoveAt(i);
                            }
                        }
                        return;
                }
                CustomerRepository.FiltrationCustomers(dataGridView1, parameter, textBox1.Text);
            }
            else
            {
                label1.Text = "Фильрация отключена";
                label1.ForeColor = Color.Red;
                CustomerRepository.GetAllCustomers(dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Показать должников")
            {
                string? cellValue;
                for (int i = dataGridView1.Rows.Count - 2; i >= 0; i--)
                {
                    cellValue = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    if (cellValue == "0")
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
                button4.Text = "Показать клиентов";
            }
            else
            {
                CustomerRepository.GetAllCustomers(dataGridView1);
                button4.Text = "Показать должников";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseDBForm chooseDBForm = new ChooseDBForm(FilePathDB);
            chooseDBForm.Show();
            this.Close();
        }
    }
}
