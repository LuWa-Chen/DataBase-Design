using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class ColumnContentRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int photo_num { get; set; }
        public IFormCollection files { get; set; }

    }

    public class ColumnContentResponse
    {
        public string column_id { get; set; }
        public int result { get; set; }
    }
    public class ColPicPath
    {
        public string Pic1Src { get; set; }
        public string Pic2Src { get; set; }
        public string Pic3Src { get; set; }
        public string Pic4Src { get; set; }
        public string Pic5Src { get; set; }
        public string Pic6Src { get; set; }
        public string Pic7Src { get; set; }
        public string Pic8Src { get; set; }
        public string Pic9Src { get; set; }
        public double size { get; set; }
    }
}