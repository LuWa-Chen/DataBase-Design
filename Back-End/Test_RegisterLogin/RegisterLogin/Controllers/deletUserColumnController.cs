using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Helpers;
using RegisterLogin.Models;

namespace RegisterLogin.Controllers
{
    [Route("api/column/deletUserColumn")]
    [ApiController]
    public class deletUserColumnController : Controller
    {
        [HttpPost]
        public deletUserColumnResponse Post([FromBody] deletUserColumnRequest req)
        {
            deletUserColumnHelper duch = new deletUserColumnHelper();
            deletUserColumnResponse resp = duch.deletUserColumn(req);

            return resp;
        }
    }
}
