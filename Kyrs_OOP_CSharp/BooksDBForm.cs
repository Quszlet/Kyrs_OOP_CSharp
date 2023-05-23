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
    public partial class BooksDBForm : Form
    {
        private Repository repository;
        public BooksDBForm(Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void BooksDBForm_Load(object sender, EventArgs e)
        {
            repository.GetAllBooks(dataGridView1);
        }
    }
}
