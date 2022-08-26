using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class CommentDeleteRequest
    {
        public string game_id { get; set; }
        public string user_id { get; set; }
    }
    public class CommentDeleteResponse
    {
        public int result { get; set; }
    }
}
