using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kyrs_OOP_CSharp.repository;

namespace Kyrs_OOP_CSharp
{
    public partial class CustomersDBForm : Form
    {
        private CustomerRepository CustomerRepository;
        private BookRepository BookRepository;
        private string FilePathDB;
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
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            CustomerRepository.GetAllCustomers(dataGridView1);
            comboBox3.Items.AddRange(BookRepository.GetAllAuthors().ToArray());
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    string customerName = textBox2.Text;
                    string customerSurname = textBox3.Text;
                    string takingDate = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                    string returnDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                    string author = comboBox3.Text;
                    string bookName = comboBox4.Text;
                    string[] authorNameSurname = author.Split(" ");
                    int bookId = BookRepository.GetBook(bookName, authorNameSurname[0], authorNameSurname[1]);
                    Customer customer = new Customer(customerName, customerSurname, bookId, takingDate, returnDate);
                    CustomerRepository.SaveCustomer(customer);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            CustomerRepository.GetAllCustomers(dataGridView1);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string? author = comboBox3.SelectedItem.ToString();
            comboBox4.Items.AddRange(BookRepository.GetAllBooksAuthors(author).ToArray());
            comboBox4.SelectedIndex = 0;
        }
    }
}
