using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/user/deleteUserGame")]
    [ApiController]
    public class PublisherDeleteUserGameController : Controller
    {
        [HttpPost]
        public UserGameResponse Post([FromBody] UserGameRequest req)
        {
            PublisherDeleteUserGameHelper pdugh = new PublisherDeleteUserGameHelper();
            UserGameResponse resp = pdugh.deleteUserGame(req);

            return resp;
        }
    }
}
