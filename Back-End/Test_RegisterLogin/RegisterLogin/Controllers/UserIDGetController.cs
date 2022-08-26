using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/getUserId")]
    [ApiController]
    public class UserIDGetController : Controller
    {
        [HttpPost]
        public UserIDGetResponse Post([FromBody] UserIDGetRequest req)
        {
            UserIDGetHelper helper = new UserIDGetHelper();
            UserIDGetResponse resp = helper.GetUID(req);

            return resp;
        }
    }
}
