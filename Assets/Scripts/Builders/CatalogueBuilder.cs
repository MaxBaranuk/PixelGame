using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UI;
using UniRx;
using UnityEngine;

namespace Builders
{
    public static class CatalogueBuilder
    {
        private const string CatalogueItemPath = "UI/CatalogueItem";
        private const string CatalogueItemsHolderPath = "UI/CataloguePanel";
        private static CatalogueItem _itemPrefab;
        private static CataloguePanel _cataloguePanel;
        public static async Task Build()
        {
            var holderPrefab = Resources.Load<CataloguePanel>(CatalogueItemsHolderPath);
            _cataloguePanel = Object.Instantiate(holderPrefab, UiController.Instance.MainCanvas.transform);
            _itemPrefab = Resources.Load<CatalogueItem>(CatalogueItemPath);
            
            var lines = ReadCsv();
            var products = await GetProductsFromCsv(lines); 
            products.ForEach(CreateItem);
            UiController.Instance.Catalogue = _cataloguePanel;
            _cataloguePanel.gameObject.SetActive(false);
        }

        private static string[] ReadCsv()
        {
            var fileData = Resources.Load<TextAsset>("CatalogueDetail");
            return fileData.text.Split("\n"[0]);
        }

        private static void CreateItem(CatalogueProduct product)
        {
            var item = Object.Instantiate(_itemPrefab, _cataloguePanel.ProductsHolder);
            item.SetProductData(product);
        }

        private static async Task<List<CatalogueProduct>> GetProductsFromCsv(IEnumerable<string> lines) 
        {
            var list = new List<CatalogueProduct>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                
                var lineData = (line.Trim()).Split(","[0]);
                float.TryParse(lineData[2], out var coast);
                float.TryParse(lineData[3], out var addCoast);
                var name = lineData[1];
                var prefab = Resources.LoadAsync<GameObject>($"SceneObjects/Buildings/{lineData[0]}");
                var sprite = Resources.LoadAsync<Sprite>($"Sprites/Buildings/{lineData[0]}");

                while (!prefab.isDone && !sprite.isDone)
                {
                    await Observable.NextFrame();
                }

                var product = new CatalogueProduct
                {
                    ModelPrefab = prefab.asset as GameObject,
                    Icon = sprite.asset as Sprite,
                    Name = name,
                    Coast = coast,
                    AddCoast = addCoast
                };
                
                list.Add(product);
            }
            return list;
        }
    }
}