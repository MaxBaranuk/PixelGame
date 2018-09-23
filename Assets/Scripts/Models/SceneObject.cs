using JSONConverters;
using Meta.JsonConverters;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
   [JsonConverter(typeof(SceneObjectConverter))]
   public class SceneObject
   {
      [JsonProperty("position")]
      private Vector3 _position;
      [JsonProperty("rotation"), JsonConverter(typeof(QuaternionJsonConverter))]
      private Quaternion _rotation;
   }
}
