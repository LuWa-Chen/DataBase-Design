using Microsoft.AspNetCore.Mvc;
using testDataForm.Models;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            //long size = FileSave(req).Result;
            double test = 4561.15264;
            Console.WriteLine(System.Convert.ToDouble(test.ToString("0000000.00")));
            resp.result = 1;
            return resp;

        }
        public void printself(HttpRequest req)
        {
            Console.WriteLine(req.Form["teststring"]);
        }
        public async Task<long> FileSave(testDataFormRequest req)
        {
            var file = req.files.Files["MainPicSrc"];
            var filename = file.FileName;
            long fileSize = file.Length;
            string fileext = Path.GetExtension(filename);
            string filepath = "C:\\test\\logo" + fileext;
            var stream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            return fileSize;
        }
    }
}

