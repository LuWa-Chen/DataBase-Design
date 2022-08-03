namespace GameNews.Models
{
    public class GameNewsRequest
    {
        public string game_id { get; set; }
        public int post_index { get; set; }
    }

    public class GameNewsResponse
    {
        public string post_cover { get; set; }
        public string post_title { get; set; }
        public string post_time { get; set; }
        public string content_text { get; set; }
        public int result { get; set; }
    }
}
