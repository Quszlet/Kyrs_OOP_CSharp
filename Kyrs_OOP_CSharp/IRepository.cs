using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs_OOP_CSharp
{
    public interface IRepository
    {
        List<Customer> GetAllCutomers();
        void GetAllBooks(DataGridView dataGridView);
        List<Customer> GetAllDebtors();
        
    }
}
