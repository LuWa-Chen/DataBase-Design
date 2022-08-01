using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RegisterLogin.Models
{
    public class GetGameDetailRequest
    {
        public string game_id { get; set; }
    }

    public class GetGameDetailResponse
    {
        public string game_name { get; set; }
        public string publish_date { get; set; }
        public double price { get; set; }
        public string developer { get; set; }
        public string publisher { get; set; }
        public List<string> videoList { get; set; } = new List<string>();
        public List<string> imageList { get; set; } = new List<string>();
        public List<string> tag { get; set; } = new List<string>();
        public int result { get; set; }

    }
}
