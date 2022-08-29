using System.Collections.Generic;
namespace GameColumnReplies.Models
{
    public class ColumnReplies
    {
        public string name { get; set; }
        public string user_id { get; set; }
        public string content { get; set; }
        public string release_time { get; set; }

    }
    public class getGameColumnRepliesRequest
    {
        public string column_id { get; set; }
    }
    public class getGameColumnRepliesResponse
    {
        public List<ColumnReplies> CR { get; set; } = new List<ColumnReplies> { };
        public int result { get; set; }
        public string message { get; set; }
    }
    public class postGameColumnRepliesRequest
    {
        public string column_id { get; set; }
        public string user_id { get; set; }
        public string content { get; set; }
    }
    public class postGameColumnRepliesResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
