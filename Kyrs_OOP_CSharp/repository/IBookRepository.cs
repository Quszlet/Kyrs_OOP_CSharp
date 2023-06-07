using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs_OOP_CSharp.repository
{
    public interface IBookRepository
    {
        void GetAllBooks(DataGridView dataGridView);
        List<string> GetAllAuthors();
        List<string> GetAllBooksAuthors(string author);
        int GetBook(string name, string authorName, string authorSurname);
        void SaveBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        void FiltrationBooks(DataGridView dataGridView, string parameter, string value);
        void FindBooks(DataGridView dataGridView, Book book);

        void DeleteAllBooks();
    }
}
