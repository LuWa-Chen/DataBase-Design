using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RegisterLogin.Models
{
    public class PublisherRegisterRequest
    {
        public IFormCollection files { get; set; }
    }

    public class PublisherRegisterResponse
    {
        public int result { get; set; }
        public string message { get; set; }
    }
}
