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
    public partial class ChooseDBForm : Form
    {
        private string FilePathDB;
        public ChooseDBForm(string filePath)
        {
            InitializeComponent();
            this.FilePathDB = filePath;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            CustomersDBForm customersDBForm = new CustomersDBForm(FilePathDB);
            customersDBForm.Show();
            Close();
        }

        private void BooksButton_Click(object sender, EventArgs e)
        {
            BooksDBForm booksDBForm = new BooksDBForm(FilePathDB);
            booksDBForm.Show();
            Close();
        }
    }
}
