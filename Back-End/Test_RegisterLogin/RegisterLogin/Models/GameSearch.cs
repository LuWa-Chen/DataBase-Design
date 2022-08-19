using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameSearchRequest
    {
        public string name { get; set; }
        public int game_or_publisher { get; set; }
        public int is_on_sale { get; set; }
        public int is_DLC { get; set; }
        public int rank_condition { get; set; }
        public int page { get; set; }
    }

    public class GameSearchResponse
    {
        public List<string> id_list { get; set; } = new List<string>();
        public int all_page { get; set; }
        public int result { get; set; }
    }
}
