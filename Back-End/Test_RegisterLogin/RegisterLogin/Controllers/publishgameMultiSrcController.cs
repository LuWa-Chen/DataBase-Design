using Microsoft.AspNetCore.Mvc;
using testDataForm.Models;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using publishgameMultiSrc.Helpers;

namespace publishgameMultiSrc.Controllers
{
    [Route("api/publisher/publishgameMultiSrc")]
    [ApiController]
    public class publishgameMultiSrcController : Controller
    {
        [HttpPost]
        public testDataFormResponse Post([FromForm] testDataFormRequest req)
        {
            testDataFormResponse resp = new testDataFormResponse();
            publishgameMultiSrcHelper hp = new publishgameMultiSrcHelper();
            resp = hp.PublishGameSrc(req, Request);

            return resp;
        }
    }
}
