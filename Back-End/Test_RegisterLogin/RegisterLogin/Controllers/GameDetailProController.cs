using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/getGameCarousel")]
    [ApiController]
    public class GameDetailProController : Controller
    {
        [HttpPost]
        public GameDetailProResponse Post([FromBody] GameDetailProRequest req)
        {
            GameDetailProHelper helper = new GameDetailProHelper();
            GameDetailProResponse resp = helper.getGameDetailPro(req);

            return resp;
        }
    }
}
