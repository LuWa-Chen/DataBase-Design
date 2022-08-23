using Microsoft.AspNetCore.Mvc;
using Chat.Models;
using Chat.Helpers;
using System;

namespace postChatHistory.Controllers
{
    [Route("api/user/postChatHistory")]
    [ApiController]
    public class postChatHistoryController : Controller
    {
        [HttpPost]
        public postChatHistoryResponse Get([FromBody] postChatHistoryRequest req)
        {
            ChatHelper nh = new ChatHelper();
            postChatHistoryResponse resp = nh.postChatHistory(req);
            Console.WriteLine(DateTime.Now.Date.ToString());

            return resp;
        }
    }
}
