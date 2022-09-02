namespace PublisherInfo.Models
{
    public class PublisherInfoRequest
    {
        public string id { get; set; }
    }
    public class PublisherInfoResponse
    {
        public string phone_number { get; set; }
        public string email { get; set; }
        public string intro { get; set; }
        public string area { get; set; }
        public int result { get; set; }
        public string message { get; set; }
    }

}
