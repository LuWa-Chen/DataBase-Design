using Microsoft.AspNetCore.Mvc;
using publishgamepost.Models;
using publishgamepost.Helpers;
using Microsoft.AspNetCore.Http;

namespace publishgamepost.Controllers
{
    [Route("api/publisher/publishgamepost")]
    [ApiController]
    public class uploadAvatarController : Controller
    {
        [HttpPost]
        public publishgamepostResponse Post([FromForm] publishgamepostRequest req)
        {
            publishgamepostResponse resp = new publishgamepostResponse();
            publishgamepostHelper ulah = new publishgamepostHelper();
            resp = ulah.PublishGamePost(req, Request);
            ulah.con.Close();

            return resp;
        }
    }
}