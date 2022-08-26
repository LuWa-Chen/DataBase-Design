using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/user/addUserGame")]
    [ApiController]
    public class PublisherAddUserGameController : Controller
    {
        [HttpPost]
        public UserGameResponse Post([FromBody] UserGameRequest req)
        {
            PublisherAddUserGameHelper paugh = new PublisherAddUserGameHelper();
            UserGameResponse resp = paugh.addUserGame(req);

            return resp;
        }
    }
}
