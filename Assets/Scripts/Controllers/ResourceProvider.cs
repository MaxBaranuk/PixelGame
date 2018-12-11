using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public static class ResourceProvider
    {
        private static class ResourceBuffer<T> where T : Object 
        {
            public static readonly Dictionary<string, T> Cache =  new Dictionary<string, T>();
        }
        
        public static T GetResource<T>(string path) where T : Object
        {
            return ResourceBuffer<T>.Cache.ContainsKey(path)
                ? ResourceBuffer<T>.Cache[path]
                : LoadFromResource<T>(path);
        }
        
        private static T LoadFromResource<T> (string path) where T : Object
        {
            var o = Resources.Load<T>(path);
            ResourceBuffer<T>.Cache.Add(path, o);
            return o;
        }
        
    }
}