using Microsoft.AspNetCore.Mvc;
using PublishComment.Helpers;
using PublishComment.Models;
using System;
namespace PublishComment.Controllers
{
    [Route("api/library/comment")]
    [ApiController]
    public class PublishCommentController : Controller
    {
        [HttpPost]
        public PublishCommentResponse Post([FromBody] PublishCommentRequest req)
        {
            PublishCommentHelper db = new PublishCommentHelper();
            PublishCommentResponse resp = db.PublishComment(req);

            return resp;
        }
    }
}
