using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using testDataForm.Models;
using System.IO;
using System;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Drawing;

namespace testDataForm.Controllers
{
    [Route("api/publisher/publishsource")]
    [ApiController]
public class testDataFormController : Controller
    {
        [HttpPost]
        public testDataFormResponse Post([FromForm] testDataFormRequest req)
        {
            testDataFormResponse resp = new testDataFormResponse();
            FileSave(req);
            Console.WriteLine(Request.Form["teststring"]);
            resp.result = 1;
            return resp;
        }
        public async Task<IActionResult> FileSave(testDataFormRequest req)
        {
            var filename = req.files.Files["MainPicSrc"].FileName;
            var file = req.files.Files["MainPicSrc"];
            long fileSize = file.Length;
            string fileext = Path.GetExtension(filename);
            string filepath = "C:\\test\\logo" + fileext;
            var stream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();
            return Ok(new { count = req.files.Count });
        }
    }
}
