using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/deleteComment")]
    [ApiController]
    public class CommentDeleteController : Controller
    {
        [HttpPost]
        public CommentDeleteResponse Post([FromBody] CommentDeleteRequest req)
        {
            CommentDeleteHelper helper = new CommentDeleteHelper();
            CommentDeleteResponse resp = helper.DeleteComment(req);

            return resp;
        }
    }
}
