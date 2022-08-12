using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameRankRequest
    {
        public string Rank_name { get; set; }
        public int Number { get; set; }
    }

    public class GameRankResponse
    {
        public List<string> id_list { get; set; } = new List<string>();
        public int result { get; set; }

    }
}