using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/column/getUserColumn")]
    [ApiController]
    public class getUserColumnController : Controller
    {
        [HttpPost]
        public UserColumnResponse Post([FromBody] UserColumnRequest req)
        {
            getUserColumnHelper guch = new getUserColumnHelper();
            UserColumnResponse resp = guch.getUserColumn(req);

            return resp;
        }
    }
}