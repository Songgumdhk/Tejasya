using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Models;

namespace Tejasya.Data
{
    // public class BookStoreContext: DbContext   // it is used while there is no autthincation in the website
    /*public class BookStoreContext : IdentityDbContext */ //it is used while there is used authintaction
    public class BookStoreContext : IdentityDbContext<ApplicationUser> // it is used when colums in datatable is added in the AspNetUsers table in database. It is added in the startup file also
    {
        

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            :base(options)
        {

        }

        public DbSet<Books> Books { get; set; }


        public DbSet<Language> Language { get; set; }


        public DbSet<BookGallery> BookGallery { get; set; }



        //the code for the connection to data base is written in the startup.cs page 
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //    optionsbuilder.UseSqlServer("server=.;database=bookstore;integrated security=true;");
        //    base.OnConfiguring(optionsbuilder);
        //}


    }
}
