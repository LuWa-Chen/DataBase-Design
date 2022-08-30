using Microsoft.AspNetCore.Http;
namespace publishgamepost.Models
{
    public class publishgamepostRequest
    {
        public IFormCollection files { get; set; }
    }
    public class publishgamepostResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
