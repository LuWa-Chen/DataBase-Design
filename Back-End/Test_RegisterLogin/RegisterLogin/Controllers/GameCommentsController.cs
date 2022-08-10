using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;


namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/getGameComments")]
    [ApiController]
    public class GameCommentsController : Controller
    {
        [HttpPost]
        public GameCommentsResponse Post([FromBody] GameCommentsRequest req)
        {
            GameCommentsHelper helper = new GameCommentsHelper();
            GameCommentsResponse resp = helper.GetGameCommentsResponse(req);

            return resp;
        }
    }
}
