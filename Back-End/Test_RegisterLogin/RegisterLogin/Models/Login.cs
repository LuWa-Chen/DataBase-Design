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
        public int result { get; set; }
    }
}
