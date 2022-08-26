using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/cdk")]
    [ApiController]
    public class CDKExchangeController : Controller
    {
        [HttpPost]
        public CDKExchangeResponse Post([FromBody] CDKExchangeRequest req)
        {
            CDKExchangeHelper helper = new CDKExchangeHelper();
            CDKExchangeResponse resp = helper.ExchangeCDK(req);

            return resp;
        }
    }
}
