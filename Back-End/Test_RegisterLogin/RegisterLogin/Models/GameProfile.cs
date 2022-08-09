using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameProfileRequest
    {
        public string game_id { get; set; }
    }

    public class GameProfileResponse
    {
        public List<Dictionary<string, string>> about_game { get; set; } = new List<Dictionary<string, string>>();
        public List<string> dlc_list { get; set; } = new List<string>();
        public int result { get; set; }
    }
}
