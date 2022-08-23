using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/shopingcart/getUserShoppingCart")]
    [ApiController]
    public class CartGetController : Controller
    {
        [HttpPost]
        public CartGetResponse Post([FromBody] CartGetRequest req)
        {
            CartGetHelper helper = new CartGetHelper();
            CartGetResponse resp = helper.GetCartInfo(req);

            return resp;
        }
    }
}
