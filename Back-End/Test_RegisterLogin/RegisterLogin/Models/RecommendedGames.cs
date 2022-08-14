using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class RecommendedGamesRequest
    {
        public string game_id { get; set; }
        public string user_id { get; set; }
    }

    public class RecommendedGamesResponse
    {
        public List<string> game_list { get; set; } = new List<string>();
        public int result { get; set; }
    }
}
