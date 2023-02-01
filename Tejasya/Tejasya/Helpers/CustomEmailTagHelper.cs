using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tejasya.Helpers
{
    public class CustomEmailTagHelper : TagHelper
 
    {
        //the custon tag helper must be added in the view imports page

        //public override void Process(TagHelperContext context, TagHelperOutput output)  // this is for harcoded data
        //{
        //    output.TagName = "a";
        //    output.Attributes.SetAttribute("href", "mailto:songgum.dh@gmail.com");
        //    output.Attributes.Add("id","my-email-id");
        //    output.Content.SetContent("my-email");
        //}

        // this is for dynamic vaule
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("my-email");
        }
    }
}
