using Newtonsoft.Json;

namespace Models
{
    public class GameData
    {
        [JsonProperty("money")]
        private float _money;
        
        [JsonProperty("sciencePoints")]
        private float _sciencePoints;
    }
}