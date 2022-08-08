using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserProfileRequest
    {
        public string id { get; set; }
        public string area { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
        public string intro { get; set; }
    }

    public class UserProfileResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
