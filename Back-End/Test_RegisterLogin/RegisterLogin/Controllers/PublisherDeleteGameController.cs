using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/deleteGame")]
    [ApiController]
    public class PublisherDeleteGameController : Controller
    {
        [HttpPost]
        public PublisherDeleteGameResponse Post([FromBody] PublisherDeleteGameRequest req)
        {
            PublisherDeleteGameHelper pdgh = new PublisherDeleteGameHelper();
            PublisherDeleteGameResponse resp = pdgh.deleteGame(req);

            return resp;
        }
    }
}