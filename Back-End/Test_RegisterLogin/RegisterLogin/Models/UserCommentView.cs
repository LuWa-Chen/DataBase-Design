namespace UserCommentView.Models
{
    public class UserCommentViewRequest
    {
        public string game_id { get; set; }
        public string user_id { get; set; }
        public string creator_id { get; set; }
        public int comment_view { get; set; }
    }

    public class UserCommentViewResponse
    {
        public int result { get; set; }
        public string response { get; set; }
    }
}
