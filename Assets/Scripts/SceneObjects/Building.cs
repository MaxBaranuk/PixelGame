using Controllers;
using Model;
using UI;
using UniRx;
using UnityEngine;
using Utils;

namespace SceneObjects
{
    public class Building : MonoBehaviour
    {
        public BuildingData ObjectData;
        public static readonly BoolReactiveProperty IsMoving = new BoolReactiveProperty();
        
        private Animator _animator;
        private Camera _main;
        
        private void Start()
        {
            _main = Camera.main;
            _animator = GetComponent<Animator>();
            InputHandler.PointerMove.SubscribeWithState(this, (tuple, item) =>
            {
                if (!IsMoving.Value)
                    return;
                
                var ray = item._main.ScreenPointToRay(tuple.Item2);

                if (Physics.Raycast(ray, out var hit, 100, Layers.QuadLayerMask))
                    ReCalculatePosition(hit.transform.position);
            });

            IsMoving.Subscribe(b => _animator.SetBool("IsMoving", b));
        }
        
        public void SetMetaData(BuildingData data)
        {
            ObjectData = data;
        }

        public void Select()
        {
            ObjectDetailPanel.Show(ObjectData);
        }

        public void EnterMoveState()
        {
            IsMoving.Value = true;
        }
        public void Unselect()
        {
            IsMoving.Value = false;
            ObjectDetailPanel.Close();
        }

        public void ReCalculatePosition(Vector3 position)
        {
            var intent =  new Vector3(ObjectData.Square.x % 2 == 0 ? 0.5f : 0, 0, ObjectData.Square.y % 2 == 0 ? 0.5f : 0);
            transform.position = position + intent;
        }
    }
}