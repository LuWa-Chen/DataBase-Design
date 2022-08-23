using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserAvatarRequest
    {
        public string id { get; set; }
        public IFormCollection files { get; set; }
    }
    public class UserAvatarResponse
    {
        public int result { get; set; }
    }
    public class AvatarPath
    {
        public string AvatarPicSrc { get; set; }
        public double size { get; set; }
    }
}
