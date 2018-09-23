using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CataloguePanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        public Transform ProductsHolder;
        
        void Start()
        {
            _closeButton.onClick.AddListener(Close);
        }
        
        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}