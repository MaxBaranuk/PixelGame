using System.Globalization;
using Models;
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
        public CatalogueProduct Product;

        public void SetProductData(CatalogueProduct product)
        {
            Product = product;
            _icon.sprite = product.Icon;
            _name.text = product.Name;
            _coast.text = product.Coast.ToString(CultureInfo.CurrentCulture);
            _addCoast.text = product.AddCoast.ToString(CultureInfo.CurrentCulture);
        }
    }
}