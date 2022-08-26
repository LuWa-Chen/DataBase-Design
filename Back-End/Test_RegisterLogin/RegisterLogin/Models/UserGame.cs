using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class UserGameRequest
    {
        public string user_id { get; set; }
        public string game_id { get; set; }
        public int via_cdk { get; set; }
    }

    public class UserGameResponse
    {
        public int result { get; set; }
    }
}