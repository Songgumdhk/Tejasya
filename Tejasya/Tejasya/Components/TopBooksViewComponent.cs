using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Repository;

namespace Tejasya.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        //public BookController(BookRepository bookRepository,
        //    LanguageRepository languageRepository,
        //    IWebHostEnvironment webHostEnvironment)
        //{
        //    _bookRepository = bookRepository;
        //    _languageRepository = languageRepository;
        //    _webHostEnvironment = webHostEnvironment;

        //}

        private  readonly IBookRepository _bookRepository;
        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
