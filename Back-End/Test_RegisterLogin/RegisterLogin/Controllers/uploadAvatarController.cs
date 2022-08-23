using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace RegisterLogin.Controllers
{
    [Route("api/user/uploadAvatar")]
    [ApiController]
    public class uploadAvatarController : Controller
    {
        [HttpPost]
        public UserAvatarResponse Post([FromForm] UserAvatarRequest req)
        {
            UserAvatarResponse resp = new UserAvatarResponse();
            uploadAvatarHelper ulah = new uploadAvatarHelper();
            resp = ulah.uploadAvatar(req, Request);

            return resp;
        }
    }
}
