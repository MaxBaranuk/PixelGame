using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingsPanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
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