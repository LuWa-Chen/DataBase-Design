using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class PublisherLoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class PublisherLoginResponse
    {
        public int result { get; set; }
        public string id { get; set; }
    }
}
