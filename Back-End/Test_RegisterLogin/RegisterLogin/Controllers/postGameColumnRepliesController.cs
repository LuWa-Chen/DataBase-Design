using Microsoft.AspNetCore.Mvc;
using GameColumnReplies.Models;
using GameColumnReplies.Helpers;
namespace postGameColumnReplies.Controllers
{
    [Route("api/column/replyColumn")]
    [ApiController]
    public class postGameColumnRepliesController : Controller
    {
        [HttpPost]
        public postGameColumnRepliesResponse Get([FromBody] postGameColumnRepliesRequest req)
        {
            GameColumnRepliesHelper nh = new GameColumnRepliesHelper();
            postGameColumnRepliesResponse resp = nh.postReply(req);

            return resp;
        }
    }
}
