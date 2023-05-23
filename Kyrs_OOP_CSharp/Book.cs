using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs_OOP_CSharp
{
    public class Book
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Theme { get; set; }

        public Book()
        {
            NameBook = "";
            AuthorName = "";
            AuthorSurname = "";
            Theme = "";
        }

        public Book(string nameBook, string authorName, string authorSurname, string theme)
        {
            NameBook = nameBook;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            Theme = theme;
        }
    }
}
