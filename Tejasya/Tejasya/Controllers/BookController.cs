using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Models;
using Tejasya.Repository;

namespace Tejasya.Controllers
{
    public class BookController : Controller
    {



        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment; // it is added for uploadng images


        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }


        [Route("book-details/{id}", Name = "bookDetailsRoute")]


        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }



        [Authorize]
        public async Task<ViewResult>  AddNewBook()
        {
            ViewData["Title"] = "Add New Book";



            ViewBag.language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2",Disabled=true };
            //var group3 = new SelectListGroup() { Name = "Group 3" };



            //ViewBag.language = new List<SelectListItem>()
            //{
            //   new SelectListItem(){Text="Nepali", Value="1"},
            //     new SelectListItem(){Text="Hindi", Value="2"},
            //      new SelectListItem(){Text="English", Value="3"},
            //       new SelectListItem(){Text="Malayam", Value="4"},
            //        new SelectListItem(){Text="French", Value="5"},
            //        new SelectListItem(){Text="French", Value="5"}
            //};

            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Nepali", Value="1"},
            //     new SelectListItem(){Text="Hindi", Value="2"},
            //      new SelectListItem(){Text="English", Value="3", Disabled=true},
            //       new SelectListItem(){Text="Malayam", Value="4", Selected=true},
            //        new SelectListItem(){Text="French", Value="5",Disabled=true},
            //};


            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text=x.Text,
            //    Value=x.Id.ToString()
            //}).ToList();

            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookmodel)
        {
            ViewData["Title"] = "Add New Book";
            

            if (ModelState.IsValid) /*this is used to validate the input in the addnewbook*/
            {

                if(bookmodel.CoverPhoto != null)            // this code is for uploading photo
                {
                    string folder = "books/cover/";
                    bookmodel.CoverImageUrl= await UploadImage(folder, bookmodel.CoverPhoto);
                }

                if (bookmodel.GalleryFiles != null)            // this code is for uploading photo
                {
                    string folder = "books/gallery/";
                    bookmodel.Gallery = new List<GalleyModel>();
                    foreach (var file in bookmodel.GalleryFiles)
                    {
                        var gallery = new GalleyModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };

                        bookmodel.Gallery.Add(gallery);
                        
                    }
                    
                }

                if (bookmodel.BookPdf != null)            // this code is for uploading photo
                {
                    string folder = "books/pdf/";
                    bookmodel.BookPdfUrl = await UploadImage(folder, bookmodel.BookPdf);
                }


                int id = await _bookRepository.AddNewBook(bookmodel);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.isSuccess = true;
                    ViewBag.BookId = id;
                    return View();
                }
            }
            ViewBag.language =new SelectList( await _languageRepository.GetLanguages(),"Id","Name");
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Nepali", Value="1"},
            //     new SelectListItem(){Text="Hindi", Value="2"},
            //      new SelectListItem(){Text="English", Value="3"},
            //       new SelectListItem(){Text="Malayam", Value="4"},
            //        new SelectListItem(){Text="French", Value="5"},
            //        new SelectListItem(){Text="French", Value="5"}
            //};
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file )// it gives path in parameter
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName; //this code give specific name to the file
                                                                           // folderPath += file.FileName; // this code keep the file name in original form

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath); // it combine with folder path
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));// it upload the file to the location
            return "/" + folderPath; // return the actual path of the image
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //{
        //    new LanguageModel(){ Id = 1, Text="Nepali"},
        //     new LanguageModel(){ Id = 2, Text="English"},
        //      new LanguageModel(){ Id = 3, Text="HIndi"},
        //       new LanguageModel(){ Id = 4, Text="Malayam"},
        //};

        //}


        // if(bookmodel.CoverPhoto != null)            // this code is for uploading photo
        //        {
        //            string folder = "books/cover/";
        ////folder += Guid.NewGuid().ToString() + "_" + bookmodel.CoverPhoto.FileName; //this code give specific name to the file
        //folder +=bookmodel.CoverPhoto.FileName; // this code keep the file name in original form
        //            bookmodel.CoverImageUrl ="/"+folder;// this code is used to save the url in the data base
        //            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
        //await bookmodel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //                 }
    }
}
