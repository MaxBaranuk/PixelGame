using Controllers;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectDetailPanel : MonoBehaviour
    {
        private static GameObject _panel;
        private static Image _image;
        private static TextMeshProUGUI _name;
        
        public static void Build()
        {
            _panel = Instantiate(ResourceProvider.GetResource<GameObject>("UI/ObjectDetail"),
                UiController.Instance.MainCanvas.transform);

            _image = _panel.GetComponentInChildren<Image>();
            _name = _panel.GetComponentInChildren<TextMeshProUGUI>();
            _panel.SetActive(false);
        }

        public static void Show(SceneObjectData data)
        {
            _image.sprite = data.Icon;
            _name.text = data.Name;
            _panel.SetActive(true);
        }

        public static void Close()
        {
            _panel.SetActive(false);
        }
    }
}
