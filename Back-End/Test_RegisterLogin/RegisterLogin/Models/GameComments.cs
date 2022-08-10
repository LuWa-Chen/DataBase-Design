using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameCommentsRequest
    {
        public string game_id { get; set; }
        public string user_id { get; set; }
    }

    public class GameCommentsResponse
    {
        public List<Dictionary<string, dynamic>> comment_list { get; set; } = new List<Dictionary<string, dynamic>>();
        public int result { get; set; }
    }
}
