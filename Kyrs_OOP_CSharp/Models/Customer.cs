using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs_OOP_CSharp
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public int IdBook { get; set; }
        public string TakingDate { get; set; }
        public string ReturnDate { get; set; }

        public Customer()
        {

        }

        public Customer(string name, string surname, string number,int idBook, string takingDate, string returnDate)
        {
            Name = name;
            Surname = surname;
            Number = number;
            IdBook = idBook;
            TakingDate = takingDate;
            ReturnDate = returnDate;
        }
    }
}
