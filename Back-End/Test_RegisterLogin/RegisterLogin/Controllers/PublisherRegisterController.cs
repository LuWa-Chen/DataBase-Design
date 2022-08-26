using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/publisher/register")]
    [ApiController]
    public class PublisherRegisterController : Controller
    {
        [HttpPost]
        public PublisherRegisterResponse Post([FromForm] PublisherRegisterRequest req)
        {
            PublisherRegisterHelper helper = new PublisherRegisterHelper();
            PublisherRegisterResponse resp = helper.publisherRegister(req, Request);

            return resp;
        }
    }
}
