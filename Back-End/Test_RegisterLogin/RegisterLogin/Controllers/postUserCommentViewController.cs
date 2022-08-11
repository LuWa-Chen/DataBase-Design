using Microsoft.AspNetCore.Mvc;
using UserCommentView.Models;
using UserCommentView.Helpers;


namespace postUserCommentViewController.Controllers
{
    [Route("api/gamedetail/postUserCommentView")]
    [ApiController]
    public class postUserCommentController : Controller
    {
        [HttpPost]
        public UserCommentViewResponse Post([FromBody] UserCommentViewRequest req)
        {
            UserCommentViewHelper helper = new UserCommentViewHelper();
            UserCommentViewResponse resp = helper.postUserCommentView(req);

            return resp;
        }
    }
}

