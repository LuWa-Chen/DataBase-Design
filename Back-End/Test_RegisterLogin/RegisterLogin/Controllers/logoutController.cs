using Microsoft.AspNetCore.Mvc;
using logout.Helpers;
using logout.Models;
using System;

namespace logout.Controllers
{
    [Route("api/user/logout")]
    [ApiController]
    public class logoutController : Controller
    {
        [HttpPost]
        public logoutResponse Post([FromBody] logoutRequest req)
        {
            logoutHelper db = new logoutHelper();
            logoutResponse resp = db.logout(req);

            return resp;
        }
    }
}