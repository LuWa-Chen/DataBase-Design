using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/library/GetQueryNameList")]
    [ApiController]
    public class GameSearchController : Controller
    {
        [HttpPost]
        public GameSearchResponse Post([FromBody] GameSearchRequest req)
        {
            GameSearchHelper helper = new GameSearchHelper();
            GameSearchResponse resp = helper.SearchGames(req);

            return resp;
        }
    }
}
