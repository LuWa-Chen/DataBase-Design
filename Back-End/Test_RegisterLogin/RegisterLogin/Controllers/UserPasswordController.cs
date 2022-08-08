using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/editPassword")]
    [ApiController]
    public class UserPasswordController : Controller
    {
        [HttpPost]
        public UserPasswordResponse Post([FromBody] UserPasswordRequest req)
        {
            UserPasswordHelper updh = new UserPasswordHelper();
            UserPasswordResponse resp = updh.editUserPassword(req);

            return resp;
        }
    }
}