using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/user/getOrdersList")]
    [ApiController]
    public class OrderGetController : Controller
    {
        [HttpPost]
        public OrderGetResponse Post([FromBody] OrderGetRequest req)
        {
            OrderGetHelper helper = new OrderGetHelper();
            OrderGetResponse resp = helper.GetOrder(req);

            return resp;
        }
    }
}
