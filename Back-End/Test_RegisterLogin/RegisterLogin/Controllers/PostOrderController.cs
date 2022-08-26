using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/shopingcart/postOrders")]
    [ApiController]
    public class PostOrderController : Controller
    {
        [HttpPost]
        public PostOrderResponse Post([FromBody] PostOrderRequest req)
        {
            PostOrderHelper poh = new PostOrderHelper();
            PostOrderResponse resp = poh.PostOrder(req);

            return resp;
        }
    }
}