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
    public partial class CustomersDBForm : Form
    {
        private Repository repository;
        public CustomersDBForm(Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
