using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Models;

namespace Tejasya.Repository
{
    public class BookRepository
    {
        public List <BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List <BookModel> SearchBook(string title, string authorName)
        {
          //  return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();
         //   return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel (){Id=1, Title="MVC", Author="Sangam"},
                new BookModel (){Id=2, Title="Java", Author="Jyoti"},
                new BookModel (){Id=3, Title="Php", Author="Moomeen"},
                new BookModel (){Id=4, Title="Js", Author="dhakal"},
                new BookModel (){Id=5, Title="C++", Author="Tejasya"},
            };
        }
    }
}
