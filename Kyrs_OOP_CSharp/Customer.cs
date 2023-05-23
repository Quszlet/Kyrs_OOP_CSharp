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
        public int IdBook { get; set; }
        public string TakingDate { get; set; }
        public string ReturnDate { get; set; }

        public Customer()
        {

        }

        public Customer(string id, string name, string surname, int idBook, string takingDate, string returnDate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            IdBook = idBook;
            TakingDate = takingDate;
            ReturnDate = returnDate;
        }
    }
}
