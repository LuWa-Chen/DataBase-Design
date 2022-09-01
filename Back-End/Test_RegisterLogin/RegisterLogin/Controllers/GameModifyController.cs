using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/publisher/changepublishgame")]
    [ApiController]
    public class GameModifyController : Controller
    {
        [HttpPost]
        public GameModifyResponse Post([FromForm] GameModifyRequest req)
        {
            GameModifyResponse resp = new GameModifyResponse();
            GameModifyHelper helper = new GameModifyHelper();
            resp = helper.ModifyGame(req, Request);
            return resp;
        }
    }
}
