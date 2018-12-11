using System.Globalization;
using Controllers.Builders;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CatalogueItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _coast;
        [SerializeField] private TMP_Text _addCoast;
        [SerializeField] private TMP_Text _name;
        public BuildingData ItemData;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => 
            {
                SceneObjectBuilder.CreateBuildingFromCatalogue(ItemData);
                UiController.Instance.Catalogue.gameObject.SetActive(false);
            });
        }

        public void SetProductData(BuildingData itemData)
        {
            ItemData = itemData;
            _icon.sprite = itemData.Icon;
            _name.text = itemData.Name;
            _coast.text = itemData.GoldCoast.ToString(CultureInfo.CurrentCulture);
            _addCoast.text = itemData.BricksCoast.ToString(CultureInfo.CurrentCulture);
        }
        
        
    }
}