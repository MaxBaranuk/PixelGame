using System.Collections.Generic;
using DataBase;
using Model;
using UI;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Controllers.Builders
{
    public static class CatalogueBuilder
    {
        private const string CatalogueItemPath = "UI/CatalogueItem";
        private const string CatalogueItemsHolderPath = "UI/CataloguePanel";
        private static CatalogueItem _itemPrefab;
        private static CataloguePanel _cataloguePanel;
        
        public static void Build()
        {
            var holderPrefab = ResourceProvider.GetResource<CataloguePanel>(CatalogueItemsHolderPath);
            _cataloguePanel = Object.Instantiate(holderPrefab, UiController.Instance.MainCanvas.transform);
            _itemPrefab = ResourceProvider.GetResource<CatalogueItem>(CatalogueItemPath);
            
            var products = GetProductsFromAsset(); 
            products.ForEach(CreateItem);
            UiController.Instance.Catalogue = _cataloguePanel;
            _cataloguePanel.gameObject.SetActive(false);
        }

        private static void CreateItem(BuildingData itemData)
        {
            var item = Object.Instantiate(_itemPrefab, _cataloguePanel.ProductsHolder);
            item.SetProductData(itemData);
        }

        private static List<BuildingData> GetProductsFromAsset()
        {
            var list = new List<BuildingData>();
            var data = AssetDatabase.LoadAssetAtPath ("Assets/BuildingsList.asset", typeof(BuildingsCatalogue)) as BuildingsCatalogue;
            if (data != null) list = data.Buildings;
                return list;
        }
    }
}