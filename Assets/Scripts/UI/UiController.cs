using UnityEngine;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        public static UiController Instance;
        public Canvas MainCanvas => _canvas;
        public CataloguePanel Catalogue;
        public MainPanelControl MainPanel;

        private static Canvas _canvas;

        private void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<UiController>();
            _canvas = FindObjectOfType<Canvas>();
        }
    }
}
