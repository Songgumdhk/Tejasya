using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Tejasya.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Tejasya.Repository;
using Tejasya.Service;
using Microsoft.AspNetCore.Authorization;

namespace Tejasya.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookConfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration,
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService) /* type ctor and click tab twice*/
        {
            _newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            _thirdPartyBookConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }



        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public string Title { get; set; }
        [ViewData]
        public BookModel Book { get; set; }




        public async Task<ViewResult> Index()
        {

           // UserEmailOptions options = new UserEmailOptions
           // {
           //     ToEmails=new List<string>() { "test@gmail.com"},
           //     PlaceHolders= new List<KeyValuePair<string, string>> ()
           //     {
           //         new KeyValuePair<string, string>("{{UserName}}","Tejasya")
           //     }
           // };

           //await _emailService.SendTestEmail(options);




            //ViewBag.Title = "Tejasya";

            //dynamic data = new ExpandoObject();
            //data.id = 1;
            //data.name = "tejasya";

            //ViewBag.data = data;

            ////viewbag is used to transfer data from controller to its view, not other view.
            //ViewBag.type = new BookModel() { Id = 5, Author = "this is author" };





            //ViewData["property1"] = "Nitish Kaushik";


            //ViewData["book"] = new BookModel()
            //{
            //    Author="Sangam",

            ////viewdata is used to transfer data from controller to view simmilar to viewbag but it is slightly differnet from viewbag, as of now i dont know
            //    Id=1
            //};

            //var newBook = configuration.GetSection("NewBookAlert").GetValue<bool>("DisplayNewBookAlert");
            //var result = newBook.GetValue<bool>("DisplayNewBookAlert");
            //var bookName = newBook.GetValue<string>("BookName");

            // var newBookAlert = new NewBookAlertConfig();
            //configuration.Bind("NewBookAlert", newBookAlert);




            var userId = _userService.GetUserId();

            var isLoggedIn = _userService.IsAuthenticated();

            bool isDisplay = _newBookAlertConfiguration.DisplayNewBookAlert;
            bool isDisplay1 = _thirdPartyBookConfiguration.DisplayNewBookAlert;





            // var value = _messageRepository.GetName();




            // bool isDisplay = newBookAlert.DisplayNewBookAlert;


            // var result1 = ConfigurationBinder["DisplayNewBookAlert"];
            //var result = _newBookAlertConfiguration["AppName"];
            //var key1 = _newBookAlertConfiguration["infoObj:key1"];
            //var key2 = _newBookAlertConfiguration["infoObj:key2"];
            //var key3 = _newBookAlertConfiguration["infoObj:key3:key3obj1"];



            //Title = "Home Page from Controller";
            //CustomProperty = "Custom Value";

            //Book = new BookModel() {Id=1, Author="Moomeen" };

            return View();
        }


        // [Route("about-us/")][HttpGet]
        // [Route("about-us/")]
        //  [Route("about-us/",Name ="aboutus")]
        //  [Route("about-us/", Name = "aboutus", Order =1)]

        public ViewResult Aboutus(int id)
        {
            Title = "About us Page from Controller";
            return View();
        }

        //  [HttpGet("contact-us")]
        [Authorize(Roles ="Admin,User")]
        public ViewResult ContactUs()
        {
            Title = "Contact us  Page from Controller";
            return View();
        }

    }
}
