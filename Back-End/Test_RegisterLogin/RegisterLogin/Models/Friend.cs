using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class FriendRequest
    {
        public string id_A { get; set; }
        public string id_B { get; set; }
    }

    public class FriendResponse
    {
        public int result { get; set; }
    }
}
