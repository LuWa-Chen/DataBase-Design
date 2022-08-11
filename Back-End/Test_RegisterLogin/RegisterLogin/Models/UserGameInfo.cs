using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class get_user_game_info
    {
        public string game_id { get; set; }
        public string game_name { get; set; }
        public string cover { get; set; }
    }
    public class UserGameInfoRequest
    {
        public string id { get; set; }
    }

    public class UserGameInfoResponse
    {
        public int game_num { get; set; }
        public List<get_user_game_info> game_info_set{ get; set; } = new List<get_user_game_info>();
        public int result { get; set; }

    }
}