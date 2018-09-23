using System.Threading.Tasks;
using UnityEngine;

namespace Builders
{
    public static class SceneBuilder
    {
        private const string QuadPath = "SceneObjects/Quad";
        private const string GameAreaPath = "SceneObjects/GameArea";
        
        public static async Task Build()
        {
            CreateGameArea(25, 25);
            await CreateSceneObjects();
        }

        private static void CreateGameArea(int width, int height)
        {
            var quadPrefab = Resources.Load<GameObject>(QuadPath);
            var gameAreaPrefab = Resources.Load<GameObject>(GameAreaPath);        
            var area = Object.Instantiate(gameAreaPrefab);
 //           area.transform.localScale = new Vector3(0.1f * width, 1, 0.1f * height);
 //           area.transform.localScale = new Vector3(0.1f * width, 1, 0.1f * height);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var pos = new Vector3(-width / 2f + 0.5f + i, 0.01f, -height / 2f + 0.5f + j);
                    var item = Object.Instantiate(quadPrefab, pos, Quaternion.identity);
                    item.transform.parent = area.transform;
                }
            } 
        }

        private static async Task CreateSceneObjects()
        {
            
        }
    }
}
