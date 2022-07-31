using System.Collections.Generic;
namespace getGameInfo.Models
{
    public class getGameInfoRequest
    {
        public string game_id { get; set; }
    }

    public class getGameInfoResponse
    {
        public string game_name { get; set; }
        public string publish_date { get; set; }
        public double price { get; set; }
        public string cover { get; set; }
        public string general_intro { get; set; }
        public List<string> tag { get; set; } = new List<string>();
        public int result { get; set; }

    }
}
