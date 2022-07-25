using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string time { get; set; }
        public string area { get; set; }
    }

    public class RegisterResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
