using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/getFriendsList")]
    [ApiController]
    public class FriendsListController : Controller
    {
        [HttpPost]
        public FriendsListResponse Post([FromBody] FriendsListRequest req)
        {
            FriendsListHelper flh = new FriendsListHelper();
            FriendsListResponse resp = flh.getFriendsList(req);

            return resp;
        }
    }
}
