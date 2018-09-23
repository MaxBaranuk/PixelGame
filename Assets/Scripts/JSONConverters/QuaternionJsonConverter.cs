using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Meta.JsonConverters
{
    public class QuaternionJsonConverter : JsonConverter
    {
        private const string X_PROPERTY_NAME = "x";
        private const string Y_PROPERTY_NAME = "y";
        private const string Z_PROPERTY_NAME = "z";
        private const string W_PROPERTY_NAME = "w";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var quaternion = (Quaternion)value;
            var jsonObject = new JObject
            {
                {X_PROPERTY_NAME, quaternion.x},
                {Y_PROPERTY_NAME, quaternion.y},
                {Z_PROPERTY_NAME, quaternion.z},
                {W_PROPERTY_NAME, quaternion.w}
            };
            jsonObject.WriteTo(writer);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Quaternion);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var x = jsonObject.Property(X_PROPERTY_NAME).Value.ToObject<float>();
            var y = jsonObject.Property(Y_PROPERTY_NAME).Value.ToObject<float>();
            var z = jsonObject.Property(Z_PROPERTY_NAME).Value.ToObject<float>();
            var w = jsonObject.Property(W_PROPERTY_NAME).Value.ToObject<float>();

            return new Quaternion(x, y, z, w);
        }
    }
}