﻿using Microsoft.AspNetCore.Mvc;
using getGameInfo.Models;
using GameDetail.Helpers;

namespace getGameInfo.Controllers
{
    [Route("api/getGameInfo")]
    [ApiController]
    public class getGameInfoController : Controller
    {
        [HttpPost]
        public getGameInfoResponse Get([FromBody] getGameInfoRequest req)
        {
            GameDetailHelper gd = new GameDetailHelper();
            getGameInfoResponse resp = gd.findGameInfo(req);

            return resp;
        }
    }
}