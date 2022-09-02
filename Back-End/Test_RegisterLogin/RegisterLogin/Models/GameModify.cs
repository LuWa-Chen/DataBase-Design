using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RegisterLogin.Models
{
    public class GameModifyRequest
    {
        public IFormCollection files { get; set; }
    }

    public class GameModifyResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }

    public class SrcPath
    {
        public string MainPicSrc { get; set; }
        public string AssPicSrc { get; set; }
        public string BacPicSrc { get; set; }
        public string ConPicSrc { get; set; }
        public string LogoPicSrc { get; set; }
        public string ProgramSrc { get; set; }
        public double size { get; set; }
    }
}
