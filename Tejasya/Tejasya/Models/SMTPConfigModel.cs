using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tejasya.Models
{
    public class SMTPConfigModel
    {
        public string SenderAddress { get; set; }
        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UserDefaultCredential { get; set; }
        public bool IsBodyHTMl { get; set; }

    }
}
