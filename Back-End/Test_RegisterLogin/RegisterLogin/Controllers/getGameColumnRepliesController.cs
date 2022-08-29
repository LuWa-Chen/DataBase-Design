using Microsoft.AspNetCore.Mvc;
using GameColumnReplies.Models;
using GameColumnReplies.Helpers;

namespace getGameColumnReplies.Controllers
{
    [Route("api/column/getGameColumnReplies")]
    [ApiController]
    public class getGameColumnRepliesController : Controller
    {
        [HttpPost]
        public getGameColumnRepliesResponse Get([FromBody] getGameColumnRepliesRequest req)
        {
            GameColumnRepliesHelper nh = new GameColumnRepliesHelper();
            getGameColumnRepliesResponse resp = nh.getReply(req);

            return resp;
        }
    }
}
