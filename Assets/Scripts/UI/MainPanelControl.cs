using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainPanelControl : MonoBehaviour
    {
        [SerializeField] private Button _buildingsMenu;
        [SerializeField] private GameObject _buildingsPanel;

        void Start()
        {
            _buildingsMenu.onClick.AddListener(OpenBuildingsPanel);
        }

        private void OpenBuildingsPanel()
        {
            _buildingsPanel.SetActive(true);
        }
    }
}
