using Builders;
using UnityEngine;

namespace Scene
{
    public class SceneManager : MonoBehaviour
    {
        private async void Start()
        {
            await CatalogueBuilder.Build();
            await SceneBuilder.Build();
        }
    }
}
