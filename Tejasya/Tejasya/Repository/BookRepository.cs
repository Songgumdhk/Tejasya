using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Data;
using Tejasya.Models;

namespace Tejasya.Repository
{

    //repositary name shoud be included in the statupclass
    public class BookRepository: IBookRepository
    {

        private readonly BookStoreContext _context = null;
        private readonly IConfiguration configuration;


        //private readonly IConfiguration _configuration;
        public BookRepository(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;

            //  _configuration = configuration;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,/* for the ppage number to verify*/
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl=model.BookPdfUrl

            };


            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });

            }

           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();
            return newBook.Id;
        }
        //public async Task<List<BookModel>> GetAllBooks()    // this also works in getallbooks
        //{
        //    var books = new List<BookModel>();
        //    var allbooks = await _context.Books.ToListAsync();
        //    if(allbooks?.Any()==true)
        //    {
        //        foreach(var book in allbooks)
        //        {
        //            books.Add(new BookModel()
        //            {
        //                Author=book.Author,
        //                Category=book.Category,
        //                Description=book.Description,
        //                Id=book.Id,
        //                LanguageId=book.LanguageId,
        //                //Language=book.Language.Name,
        //                Title=book.Title,
        //                TotalPages=book.TotalPages,
        //                CoverImageUrl=book.CoverImageUrl
        //            });
        //        }
        //    }
        //    return books;
        //}


        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                 .Select(book => new BookModel()
                 { 
                         Author = book.Author,
                         Category = book.Category,
                         Description = book.Description,
                         Id = book.Id,
                         LanguageId = book.LanguageId,
                         //Language=book.Language.Name,
                         Title = book.Title,
                         TotalPages = book.TotalPages,
                         CoverImageUrl = book.CoverImageUrl
                     }).ToListAsync();
        }


        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     //Language=book.Language.Name,
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl
                 }).Take(count).ToListAsync();
        }


        public  async Task<BookModel> GetBookById(int id)
        {
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
            return await _context.Books.Where(x => x.Id == id)
                .Select(book =>new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl=book.CoverImageUrl,
                    Gallery=book.bookGallery.Select(g=> new GalleyModel()
                    {
                      Id=g.Id,
                      Name=g.Name,
                      URL=g.URL
                    }).ToList(),
                    BookPdfUrl=book.BookPdfUrl
                    
                }).FirstOrDefaultAsync();
           
           
        }
        public List <BookModel> SearchBook(string title, string authorName)
        {
          //  return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
            return null;
         //   return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel (){Id=1, Title="MVC", Author="Sangam", Description="hello from tthe rule of the mVC",Category="Programming",Language="English",TotalPages=134},
        //        new BookModel (){Id=2, Title="Java", Author="Jyoti", Description="hello from tthe rule of the java",Category="android",Language="french",TotalPages=256},
        //        new BookModel (){Id=3, Title="Php", Author="Moomeen",  Description="hello from tthe rule of the php",Category="facebook",Language="Nepali",TotalPages=556},
        //        new BookModel (){Id=4, Title="Js", Author="dhakal", Description="hello from tthe rule of the js",Category="frontend",Language="hindi",TotalPages=5656},
        //        new BookModel (){Id=5, Title="C++", Author="Tejasya", Description="hello from tthe rule of the c++",Category=".net",Language="American",TotalPages=898},
        //    };
        //}

        public string GetAppName()
        {
            //  return "Tejasya";
            return configuration["AppName"];
        }
    }
}
