using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/getRecommendedGames")]
    [ApiController]
    public class RecommendedGamesController : Controller
    {
        [HttpPost]
        public RecommendedGamesResponse Post([FromBody] RecommendedGamesRequest req)
        {
            RecommendedGamesHelper rgh = new RecommendedGamesHelper();
            RecommendedGamesResponse resp = rgh.getRecommendedGames(req);

            return resp;
        }
    }
}
