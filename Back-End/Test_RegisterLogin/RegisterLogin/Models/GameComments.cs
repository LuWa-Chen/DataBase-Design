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
        public int page_no { get; set; }
        public int comment_type { get; set; }
        public int filter_1 { get; set; }
        public int filter_2 { get; set; }
        public int filter_3 { get; set; }

    }

    public class GameCommentsResponse
    {
        public int comment_num { get; set; }
        public bool is_end { get; set; }
        public List<Dictionary<string, dynamic>> comment_list { get; set; } = new List<Dictionary<string, dynamic>>();
        public int result { get; set; }
    }
}
