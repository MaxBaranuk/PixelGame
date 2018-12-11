using System.Threading.Tasks;
using Controllers.Builders;
using UI;
using UnityEngine;

namespace Controllers
{
    public class SceneLoader: MonoBehaviour
    {
        private async void Start()
        {
            await LoadScene();
            Destroy(gameObject);
        }

        private static async Task LoadScene()
        {
            var sceneManager = Instantiate(Resources.Load<GameObject>("SceneObjects/SceneManager"));           
            sceneManager.AddComponent<InputHandler>();
            CatalogueBuilder.Build();
            ObjectDetailPanel.Build();
            await SceneObjectBuilder.BuildScene();
            Instantiate(Resources.Load<GameObject>("SceneObjects/Main Camera"));
        }
    }
}