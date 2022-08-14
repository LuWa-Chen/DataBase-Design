using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserAvatarRequest
    {
        public string id { get; set; }
        public string imgUrl { get; set; }
    }

    public class UserAvatarResponse
    {
        public int result { get; set; }
    }
}
