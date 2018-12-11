using UnityEditor;
using UnityEngine;

namespace DataBase
{
    public static class CreateCatalogueStorage
    {
        [MenuItem("Assets/Create/Buildings List")]
        public static BuildingsCatalogue  Create()
        {
            var asset = ScriptableObject.CreateInstance<BuildingsCatalogue>();
            AssetDatabase.CreateAsset(asset, "Assets/BuildingsList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
        
//        [MenuItem("Assets/Create/Citizen List")]
//        public static BuildingsCatalogue  Create()
//        {
//            var asset = ScriptableObject.CreateInstance<BuildingsCatalogue>();
//            AssetDatabase.CreateAsset(asset, "Assets/BuildingsList.asset");
//            AssetDatabase.SaveAssets();
//            return asset;
//        }
    }
}