using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tejasya.Models
{
    public class SignUpUserModel
    {

        [Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }


        [Required (ErrorMessage ="Please enter your email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Please enter strong Password")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Password does not match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
