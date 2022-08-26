using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class PublisherDeleteGameRequest
    {
        public string game_id { get; set; }
    }

    public class PublisherDeleteGameResponse
    {
        public int result { get; set; }
    }
}
