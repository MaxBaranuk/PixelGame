using System;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSONConverters
{
    public class SceneObjectConverter: JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(SceneObject).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {            
            var jsonObject = JObject.Load(reader);
//            var typeString = (string)jsonObject.Property(SceneObjectRecord.TYPE_PROPERTY_NAME);
//            var record = typeString.Match().To<SceneObjectRecord>()
//                .With(nameof(SceneObjectRecord)).Return(new SceneObjectRecord())
//                .With(nameof(EditableSceneObjectRecord)).Return(new EditableSceneObjectRecord())
//                .With(nameof(BayRecord)).Return(new BayRecord())
//                .Else(_ => { throw new JsonReaderException($"Can't parse SceneObjectRecord, unknown type: \"{typeString}\"."); })
//                .Result();

            var record = new SceneObject();
            using (var jsonReader = jsonObject.CreateReader())
            {
                serializer.Populate(jsonReader, record);
            }

            return record;
        }
    }
}