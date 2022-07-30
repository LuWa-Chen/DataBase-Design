using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using getGameName.Models;
using GameDetail;

namespace getGameName.Controllers
{
    [Route("api/library/getGameName")]
    [ApiController]
    public class getGameNameController : Controller
    {
        [HttpPost]
        public getGameNameResponse Post([FromBody] getGameNameRequest req)
        {
            GameDetailHelper gd = new GameDetailHelper();
            getGameNameResponse resp = gd.findGameName(req);

            return resp;
        }
    }
}
