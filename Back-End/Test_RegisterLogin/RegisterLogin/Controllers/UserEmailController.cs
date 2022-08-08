using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/editEmail")]
    [ApiController]
    public class UserEmailController : Controller
    {
        [HttpPost]
        public UserEmailResponse Post([FromBody] UserEmailRequest req)
        {
            UserEmailHelper ueh = new UserEmailHelper();
            UserEmailResponse resp = ueh.editUserEmail(req);

            return resp;
        }
    }
}