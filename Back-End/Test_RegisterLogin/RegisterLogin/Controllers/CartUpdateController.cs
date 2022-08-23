using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/shopingcart/modifyUserShoppingCart")]
    [ApiController]
    public class CartUpdateController : Controller
    {
        [HttpPost]
        public CartUpdateResponse Post([FromBody] CartUpdateRequest req)
        {
            CartUpdateHelper helper = new CartUpdateHelper();
            CartUpdateResponse resp = helper.UpdateCartInfo(req);

            return resp;
        }
    }
}
