using System.Collections.Generic;
namespace PublisherGameInfo.Models
{
    public class PublisherGameDetail
    {
        public string game_id { get; set; }
        public string game_name { get; set; }
        public string publish_date { get; set; }
        public double pre_price { get; set; }
        public double now_price { get; set; }
        public double discount { get; set; }
        public List<string> tag { get; set; } = new List<string>();
        public string image { get; set; }
    }
    public class PublisherGameInfoRequest
    {
        public string publisher { get; set; }
    }

    public class PublisherGameInfoResponse
    {
        public List<PublisherGameDetail> games { get; set; } = new List<PublisherGameDetail>();
        public int game_count { get; set; }
        public int result { get; set; }
        public string response { get; set; }
    }
}
