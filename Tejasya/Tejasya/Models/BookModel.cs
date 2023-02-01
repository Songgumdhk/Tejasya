using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; /*this is used for the validation of the input data*/
using Tejasya.Enums;
using Tejasya.Helpers;
using Microsoft.AspNetCore.Http;

namespace Tejasya.Models
{
    public class BookModel
    {
        public int Id { get; set; }

       // [StringLength(10, MinimumLength = 10, ErrorMessage = "Title should be exactly 10 characters long")]
        
        //[Required(ErrorMessage = "Please enter the title of the book")]
      //  [RegularExpression("^[0-9]+$", ErrorMessage = "Title must be a number.")]
      //  [MyCustomValidation] 
        public string Title { get; set; }

       // [Required (ErrorMessage ="Please enter the aurther name")]
        public string Author { get; set; } 
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }


        public string Language { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        public IFormFile CoverPhoto { get; set; }


        //public LanguageEnum LanguageEnum { get; set; }



        //[Required (ErrorMessage ="Please enter the total number of pages")]
        //[Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        public IFormFileCollection GalleryFiles { get; set; }


        public List<GalleyModel> Gallery { get; set; }


        [Display(Name = "Upload Your book in PDF formate")]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }


    }
}
