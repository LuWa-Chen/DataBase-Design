using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserIDGetRequest
    {
        public string user_name { get; set; }
    }
    public class UserIDGetResponse
    {
        public string user_id { get; set; }
        public int result { get; set; }
    }
}
