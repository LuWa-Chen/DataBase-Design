namespace UserInfo.Models
{
    public class UserInfoRequest
    {
        public string id { get; set; }
    }

    public class UserInfoResponse
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string area { get; set; }
        public int status { get; set; }
        public string birthday { get; set; }
        public int game_num { get; set; }
        public string intro { get; set; }
        public string profile_photo { get; set; }
        public int result { get; set; }
    }
}
