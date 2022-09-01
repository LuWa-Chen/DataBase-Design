using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using RegisterLogin.Models;
using RegisterLogin.Helpers;
using System.Threading.Tasks;

namespace RegisterLogin.Controllers
{
    [Route("api/publisher/publishdiscount")]
    [ApiController]
    public class DiscountPublishController : Controller
    {
        [HttpPost]
        public DiscountPublishResponse Post([FromBody] DiscountPublishRequest req)
        {
            DiscountPublishHelper helper = new DiscountPublishHelper();
            DiscountPublishResponse resp = helper.PublishDiscount(req);

            helper.con.Close();
            return resp;
        }
    }
}
