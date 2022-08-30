using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/column/getGameColumns")]
    [ApiController]
    public class GameColumnsController : Controller
    {
        [HttpPost]
        public GameColumnResponse Post([FromBody] GameColumnsRequest req)
        {
            GameColumnsHelper helper = new GameColumnsHelper();
            GameColumnResponse resp = helper.GetColumns(req);

            return resp;
        }
    }
}
