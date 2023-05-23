using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs_OOP_CSharp
{
    public class Debtor : Customer
    {
       
        public string DaysOverdue { get; set; }

        public Debtor(string id, string name, string surname, int idBook, string takingDate, string returnDate, string daysOverdue) 
            : base(id, name, surname, idBook, takingDate, returnDate)
        {
            DaysOverdue = daysOverdue;
        }
    }
}
