using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrs_OOP_CSharp
{
    public partial class ChooseDBForm : Form
    {
        private Repository repository;
        public ChooseDBForm(Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            CustomersDBForm customersDBForm = new CustomersDBForm(repository);
            customersDBForm.Show();
            Close();
        }

        private void BooksButton_Click(object sender, EventArgs e)
        {
            BooksDBForm booksDBForm = new BooksDBForm(repository);
            booksDBForm.Show();
            Close();
        }
    }
}
