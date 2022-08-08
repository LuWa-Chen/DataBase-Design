using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserPasswordRequest
    {
        public string id { get; set; }
        public string password { get; set; }
    }

    public class UserPasswordResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}