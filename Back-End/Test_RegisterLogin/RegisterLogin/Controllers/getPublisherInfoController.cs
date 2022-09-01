using Microsoft.AspNetCore.Mvc;
using PublisherInfo.Helpers;
using PublisherInfo.Models;

namespace getPublisherInfo.Controllers
{
    [Route("api/publisher/getpubinfo")]
    [ApiController]
    public class addFriendsController : Controller
    {
        [HttpPost]
        public PublisherInfoResponse Post([FromBody] PublisherInfoRequest req)
        {
            PublisherInfoHelper afh = new PublisherInfoHelper();
            PublisherInfoResponse resp = afh.getPublisherInfo(req);

            return resp;
        }
    }
}
