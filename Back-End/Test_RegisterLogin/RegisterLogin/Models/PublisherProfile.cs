using Microsoft.AspNetCore.Http;
namespace PublisherProfile.Models
{
    public class PublisherProfileRequest
    {
        public IFormCollection files { get; set; }
    }
    public class PublisherProfileResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
