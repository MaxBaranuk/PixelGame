using System.Collections.Generic;
using JSONConverters;
using Newtonsoft.Json;

namespace Models
{
    public class SceneRoot
    {
        [JsonProperty("sceneObjects"), JsonConverter(typeof(SceneObjectConverter))]
        public List<SceneObject> SceneObjects = new List<SceneObject>();

        [JsonProperty("gameData")]
        public GameData Data;
    }
}