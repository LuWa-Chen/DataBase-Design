using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/getUserGameInfo")]
    [ApiController]
    public class UserGameInfoController : Controller
    {
        [HttpPost]
        public UserGameInfoResponse Post([FromBody] UserGameInfoRequest req)
        {
            UserGameInfoHelper ugih = new UserGameInfoHelper();
            UserGameInfoResponse resp = ugih.getUserGameInfo(req);

            return resp;
        }
    }
}