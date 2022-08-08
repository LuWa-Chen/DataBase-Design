using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/editProfile")]
    [ApiController]
    public class UserProfileController : Controller
    {
        [HttpPost]
        public UserProfileResponse Post([FromBody] UserProfileRequest req)
        {
            UserProfileHelper uph = new UserProfileHelper();
            UserProfileResponse resp = uph.editUserProflie(req);

            return resp;
        }
    }
}
