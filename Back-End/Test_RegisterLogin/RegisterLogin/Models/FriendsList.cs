using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class get_user_friends
    {
        public string id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string profile_photo { get; set; }
    }
    public class FriendsListRequest
    {
        public string id { get; set; }
    }

    public class FriendsListResponse
    {
        public List<get_user_friends> friends_list { get; set; } = new List<get_user_friends>();
        public int result { get; set; }

    }
}
