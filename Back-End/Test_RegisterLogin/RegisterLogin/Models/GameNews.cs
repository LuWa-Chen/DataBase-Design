using System.Collections.Generic;
namespace GameNews.Models
{
    public class GameNewsRequest
    {
        public string game_id { get; set; }
    }
    public class GameNewsResponse
    {
        public List<gamenew> gn { get; set; } = new List<gamenew>();
        public int result { get; set; }
        public string message { get; set; }
    }

    public class gamenew
    {
        public string post_cover { get; set; }
        public string post_title { get; set; }
        public string post_time { get; set; }
        public string content_text { get; set; }
    }
}
