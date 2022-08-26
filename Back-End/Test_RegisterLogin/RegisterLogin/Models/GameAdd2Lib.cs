using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameAdd2LibRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
    }

    public class GameAdd2LibResponse
    {
        public int result { get; set; }
    }
}
