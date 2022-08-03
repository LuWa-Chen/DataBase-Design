using Microsoft.AspNetCore.Mvc;
using GameNews.Models;
using News.Helpers;

namespace getGameNews.Controllers
{
    [Route("api/library/getGameNews")]
    [ApiController]
    public class getGameInfoController : Controller
    {
        [HttpPost]
        public GameNewsResponse Get([FromBody] GameNewsRequest req)
        {
            NewsHelper nh = new NewsHelper();
            GameNewsResponse resp = nh.getGameNews(req);

            return resp;
        }
    }
}
