using Microsoft.AspNetCore.Mvc;
using PublisherProfile.Models;
using Microsoft.AspNetCore.Http;
using PublisherProfile.Helpers;

namespace PublisherProfile.Controllers
{
    [Route("api/publisher/editProfile")]
    [ApiController]
    public class PublisherProfileController : Controller
    {
        [HttpPost]
        public PublisherProfileResponse Post([FromForm] PublisherProfileRequest req)
        {
            PublisherProfileResponse resp = new PublisherProfileResponse();
            PublisherProfileHelper hp = new PublisherProfileHelper();
            resp = hp.PublishGame(req, Request);
            hp.con.Close();
            return resp;
        }
    }
}
