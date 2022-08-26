using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/shopingcart/addToLibrary")]
    [ApiController]
    public class GameAdd2LibController : Controller
    {
        [HttpPost]
        public GameAdd2LibResponse Post([FromBody] GameAdd2LibRequest req)
        {
            GameAdd2LibHelper helper = new GameAdd2LibHelper();
            GameAdd2LibResponse resp = helper.Add2Lib(req);

            return resp;
        }
    }
}
