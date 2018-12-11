using System.Threading.Tasks;
using Model;
using SceneObjects;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Controllers.Builders
{
    public static class SceneObjectBuilder
    {
        private const string QuadPath = "SceneObjects/Quad";
        private const string GameAreaPath = "SceneObjects/GameArea";
        
        public static async Task BuildScene()
        {
            CreateGameArea(25, 25);
            await CreateSceneObjects();
        }

        public static Building CreateBuildingFromCatalogue(BuildingData data)
        {
            var prefab = data.ModelPrefab;
            var go = Object.Instantiate(prefab).GetComponent<Building>();
            go.SetMetaData(data);
        
            go.ReCalculatePosition(GetCameraPointSquare());
            return go;
        }

        private static void CreateGameArea(int width, int height)
        {
            var quadPrefab = ResourceProvider.GetResource<GameObject>(QuadPath);
            var gameAreaPrefab = ResourceProvider.GetResource<GameObject>(GameAreaPath);        
            var area = Object.Instantiate(gameAreaPrefab);
            var quadHolder = new GameObject("Quads");
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var pos = new Vector3(-width / 2f + 0.5f + i, 0.01f, -height / 2f + 0.5f + j);
                    var item = Object.Instantiate(quadPrefab, pos, Quaternion.identity);
                    item.transform.parent = quadHolder.transform;
                }
            } 
        }

        private static Vector3 GetCameraPointSquare()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0));
            return !Physics.Raycast(ray, out var hit, 100, Layers.QuadLayerMask) ? Vector3.zero : hit.transform.position;
        }
        private static async Task CreateSceneObjects()
        {
            
        }
    }
}
