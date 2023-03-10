using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tejasya.Models
{
    public class UserEmailOptions
    {
        public List<String> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
