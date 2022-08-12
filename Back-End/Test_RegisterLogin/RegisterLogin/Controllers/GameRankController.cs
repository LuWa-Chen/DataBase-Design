using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/library/GetGameRank")]
    [ApiController]
    public class GameRankController : Controller
    {
        [HttpPost]
        public GameRankResponse Post([FromBody] GameRankRequest req)
        {
            GameRankHelper grh = new GameRankHelper();
            GameRankResponse resp = grh.getGameRank(req);

            return resp;
        }
    }
}
