using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegisterLogin.Models;
using RegisterLogin.Helpers;

namespace RegisterLogin.Controllers
{
    [Route("api/GetGameCarousel")]
    [ApiController]
    public class GameDetailController : Controller
    {
        [HttpPost]
        public GetGameDetailResponse Get([FromBody] GetGameDetailRequest req)
        {
            GameDetailPlusHelper gd = new GameDetailPlusHelper();
            GetGameDetailResponse resp = gd.getGameDetail(req);

            return resp;
        }
    }
}
