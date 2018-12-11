using SceneObjects;
using UniRx;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
        
        private float _pointerDownTime;
        private bool _isDrag;
        private static Building _currentSelectedObject;
        
        private void Start()
        {
            InputHandler.PointerDown.Subscribe(point =>
            {
                _pointerDownTime = Time.timeSinceLevelLoad;
                _isDrag = false;
            });

            InputHandler.PointerMove.Subscribe(tuple =>
            {
                _isDrag = true;
            });

            InputHandler.PointerUp.Subscribe(point =>
            {
                if (_isDrag)
                    return;

                if (_currentSelectedObject)
                {
                    _currentSelectedObject.Unselect();
                    _currentSelectedObject = null;
                }
                              
                var ray = Camera.main.ScreenPointToRay(point);
                if (!Physics.Raycast(ray, out var hit, 100, Layers.SceneObjectLayerMask)) return;
                var item = hit.transform.gameObject.GetComponent<Building>();

                if (!item) return;
                
                if (item == _currentSelectedObject) return;
                    
                if (Time.timeSinceLevelLoad - _pointerDownTime > 0.5f)
                    item.EnterMoveState();
                    
                item.Select();
                _currentSelectedObject = item;
                
            });
        }
    }
}
