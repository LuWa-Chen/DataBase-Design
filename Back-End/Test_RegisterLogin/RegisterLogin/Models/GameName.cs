namespace getGameName.Models
{
    public class getGameNameRequest
    {
        public string game_id { get; set; }
    }

    public class getGameNameResponse
    {
        public string game_name { get; set; }
        public int result { get; set; }
    }
}
