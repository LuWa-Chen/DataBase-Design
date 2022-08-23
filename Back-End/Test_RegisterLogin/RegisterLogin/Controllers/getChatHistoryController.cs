using Microsoft.AspNetCore.Mvc;
using Chat.Models;
using Chat.Helpers;

namespace getChatHistory.Controllers
{
    [Route("api/user/getChatHistory")]
    [ApiController]
    public class getChatHistoryController : Controller
    {
        [HttpPost]
        public getChatHistoryResponse Get([FromBody] getChatHistoryRequest req)
        {
            ChatHelper nh = new ChatHelper();
            getChatHistoryResponse resp = nh.getChat(req); 

            return resp;
        }
    }
}
