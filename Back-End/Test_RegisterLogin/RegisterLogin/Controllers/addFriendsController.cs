using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/addFriends")]
    [ApiController]
    public class addFriendsController : Controller
    {
        [HttpPost]
        public FriendResponse Post([FromBody] FriendRequest req)
        {
            addFriendsHelper afh = new addFriendsHelper();
            FriendResponse resp = afh.addFriends(req);

            return resp;
        }
    }
}