using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tejasya.Helpers
{
    public class MyCustomValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string bookname = value.ToString();
                if (bookname.Contains("mvc"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("BookName does not contain desired value");
        }
    }
}
