using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tejasya.Data;
using Microsoft.EntityFrameworkCore;
using Tejasya.Repository;
using Microsoft.Extensions.Configuration;
using Tejasya.Models;
using Microsoft.AspNetCore.Identity;
using Tejasya.Helpers;
using Tejasya.Service;

namespace Tejasya
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration )
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //the below code is used to connect bookstorecontext.cs 
            //at forst make connectionn in appsetting.json file and make ctor for the json name in statup file and give the stirng connecntion name as below
            services.AddDbContext<BookStoreContext>(
                options=>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //this is used for the authintication -----add use appUseAuthentication below to work
            services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();    //AddDefaultTokenProviders  is added for the confirmation of the email for signup . itis inbuilt method
           // .AddEntityFrameworkStores<BookStoreContext>();
            //this is uused for the authintaction


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
            });




            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = configuration["Application:LoginPath"];
            });


            services.AddControllersWithViews();
            //the below code helps to compile the code while reloading in the browser.
#if DEBUG
            //services.AddRazorPages().AddRazorRuntimeCompilation();        //the below code helps to compile the code while reloading in the browser.


            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = false;     //this code disable client side validation
            });
#endif

            // services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.Configure<NewBookAlertConfig>("InternalBook",configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook",configuration.GetSection("ThirdPartyBook"));
            services.Configure < SMTPConfigModel>(configuration.GetSection("SMTPConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //the below code is use to run the static files other then wwwroot folder.
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath= "/MyStaticFiles"
            //});
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapDefaultControllerRoute();

            // endpoints.MapControllers();





            //endpoints.MapControllerRoute(
            //  name: "Default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //  pattern: "{action}/{controller}/{id?}");
            //    //these code is used to make the new word added to the url , here bookapp is added.




            endpoints.MapControllerRoute(
              name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
       



        });
            
        }

    }
}
