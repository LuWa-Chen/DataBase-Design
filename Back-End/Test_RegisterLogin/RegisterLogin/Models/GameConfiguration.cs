using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    public class GameConfigurationRequest
    {
        public string game_id { get; set; }
    }

    public class GameConfigurationResponse
    {
        public List<string> game_features { get; set; } = new List<string>();
        public List<string> minimum_configuration { get; set; } = new List<string>();
        public List<string> recommended_configuration { get; set; } = new List<string>();
        public List<string> ui_language { get; set; } = new List<string>();
        public List<string> sound_language { get; set; } = new List<string>();
        public List<string> text_language { get; set; } = new List<string>();
        public int result { get; set; }
    }
}
