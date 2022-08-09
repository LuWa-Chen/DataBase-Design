using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/getGameIntro")]
    [ApiController]
    public class GameProfileController : Controller
    {
        [HttpPost]
        public GameProfileResponse Post([FromBody] GameProfileRequest req)
        {
            GameProfileHelper helper = new GameProfileHelper();
            GameProfileResponse resp = helper.getGameProfile(req);

            return resp;
        }
    }
}
