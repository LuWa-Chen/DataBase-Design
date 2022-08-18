using Microsoft.AspNetCore.Mvc;
using testDataForm.Models;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using publishgame.Helpers;

namespace publishgame.Controllers
{
    [Route("api/publisher/publishgame")]
    [ApiController]
    public class publishgameController : Controller
    {
        [HttpPost]
        public testDataFormResponse Post([FromForm] testDataFormRequest req)
        {
            testDataFormResponse resp = new testDataFormResponse();
            publishgameHelper hp = new publishgameHelper();
            resp = hp.PublishGame(req, Request);

            return resp;
        }
    }
}
