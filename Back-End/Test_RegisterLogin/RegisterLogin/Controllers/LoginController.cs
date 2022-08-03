using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Helpers;
using RegisterLogin.Models;
using System;

namespace RegisterLogin.Controllers
{
    [Route("api/user/login")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public LoginResponse Post([FromBody] LoginRequest req)
        {
            DBHelper db = new DBHelper();
            LoginResponse resp = db.checkLogin(req);
           
            return resp;
        }
    }
}