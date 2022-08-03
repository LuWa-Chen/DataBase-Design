using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameDetailProRequest
    {
        public string game_id { get; set; }
    }

    public class GameDetailProResponse
    {
        public string game_name { get; set; }
        public string publish_date { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
        public string developer { get; set; }
        public string publisher { get; set; }
        public List<string> image_list { get; set; } = new List<string>();
        public List<string> video_list { get; set; } = new List<string>();
        public List<string> tag_list { get; set; } = new List<string>();
        public int result { get; set; }
    }
}
