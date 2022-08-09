using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/gamedetail/getGameConfigurations")]
    [ApiController]
    public class GameConfigurationController : Controller
    {
        [HttpPost]
        public GameConfigurationResponse Post([FromBody] GameConfigurationRequest req)
        {
            GameConfigurationHelper helper = new GameConfigurationHelper();
            GameConfigurationResponse resp = helper.getGameConfiguration(req);

            return resp;
        }
    }
}
