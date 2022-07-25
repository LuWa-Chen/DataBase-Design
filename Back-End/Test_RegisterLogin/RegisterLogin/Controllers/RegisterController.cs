using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/user/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        [HttpPost]
        public RegisterResponse Post([FromBody] RegisterRequest req)
        {
            DBHelper db = new DBHelper();
            RegisterResponse resp = db.register(req);

            return resp;
        }
    }
}
