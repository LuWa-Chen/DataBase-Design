using Microsoft.AspNetCore.Mvc;

namespace RegisterLogin.Models
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public string name { get; set; }
        public string id { get; set; }
        public int result { get; set; }
    }
}
