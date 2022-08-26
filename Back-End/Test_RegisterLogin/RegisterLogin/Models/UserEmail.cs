using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserEmailRequest
    {
        public string id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class UserEmailResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}