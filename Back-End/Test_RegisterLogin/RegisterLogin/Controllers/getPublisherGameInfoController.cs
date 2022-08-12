using Microsoft.AspNetCore.Mvc;
using PublisherGameInfo.Models;
using PublisherGameInfo.Helpers;


namespace getPublisherGameInfo.Controllers
{
    [Route("api/publisher/getgameinfo")]
    [ApiController]
    public class getPublisherGameInfoController : Controller
    {
        [HttpPost]
        public PublisherGameInfoResponse Post([FromBody] PublisherGameInfoRequest req)
        {
            PublisherGameInfoHelper helper = new PublisherGameInfoHelper();
            PublisherGameInfoResponse resp = helper.getPublisherGameInfo(req);

            return resp;
        }
    }
}
