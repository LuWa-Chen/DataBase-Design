using System.Collections.Generic;
namespace getGameInfo.Models
{
    public class getGameInfoRequest
    {
        public List<string> game_id { get; set; } = new List<string>();
    }

    public class getGameInfoResponse
    {
        public List<string> game_name { get; set; } = new List<string>();
        public List<string> publish_date { get; set; } = new List<string>();
        public List<double> price { get; set; } = new List<double>();
        public List<string> general_intro { get; set; } = new List<string>();
        public List<double> discount { get; set; } = new List<double>();
        public int result { get; set; }

    }
}
