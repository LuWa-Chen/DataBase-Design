using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using RegisterLogin.Helpers;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace RegisterLogin.Controllers
{
    [Route("api/column/uploadColumnContent")]
    [ApiController]
    public class uploadColumnContentController : Controller
    {
        [HttpPost]

        [DisableRequestSizeLimit]//解除请求大小限制
        public ColumnContentResponse Post([FromForm] ColumnContentRequest req)
        {
            ColumnContentResponse resp = new ColumnContentResponse();
            uploadColumnContentHelper ulcch = new uploadColumnContentHelper();
            resp = ulcch.uploadColumnContent(req,resp, Request);

            return resp;
        }
    }
}
