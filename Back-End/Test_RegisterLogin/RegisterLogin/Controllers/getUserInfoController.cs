using Microsoft.AspNetCore.Mvc;
using UserInfo.Models;
using UserInfo.Helpers;

namespace getUserInfo.Controllers
{
    [Route("api/user/getUserInfo")]
    [ApiController]
    public class getGameInfoController : Controller
    {
        [HttpPost]
        public UserInfoResponse Get([FromBody] UserInfoRequest req)
        {
            UserInfoHelper nh = new UserInfoHelper();
            UserInfoResponse resp = nh.getUserInfo(req);

            return resp;
        }
    }
}