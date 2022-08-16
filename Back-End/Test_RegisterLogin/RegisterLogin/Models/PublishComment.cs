namespace PublishComment.Models
{
    public class PublishCommentRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
        public int rating { get; set; }
        public string content { get; set; }
    }
    public class PublishCommentResponse
    {
        public int result { get; set; }
        public string messsage { get; set; }
    }
}
