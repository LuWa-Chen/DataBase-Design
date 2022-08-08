using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/publisher/login")]
    [ApiController]
    public class PublisherLoginController : Controller
    {
        [HttpPost]
        public PublisherLoginResponse Post([FromBody] PublisherLoginRequest req)
        {
            PublisherLoginHelper helper = new PublisherLoginHelper();
            PublisherLoginResponse resp = helper.publisherLogin(req);

            return resp;
        }
    }
}
