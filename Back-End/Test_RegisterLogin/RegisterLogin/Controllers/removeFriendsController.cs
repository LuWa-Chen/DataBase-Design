using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/removeFriends")]
    [ApiController]
    public class removeFriendsController : Controller
    {
        [HttpPost]
        public FriendResponse Post([FromBody] FriendRequest req)
        {
            removeFriendsHelper rfh = new removeFriendsHelper();
            FriendResponse resp = rfh.removeFriends(req);

            return resp;
        }
    }
}
