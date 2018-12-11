using SceneObjects;
using UI;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class CameraMovement : MonoBehaviour
    {      
        private void Start()
        {
            InputHandler.MouseScroll.Subscribe(value =>
            {
                var pos = transform.position + transform.forward * value;
                if (pos.y < 1 || pos.y > 10)
                    return;              
                transform.position = pos;
            });
            
            InputHandler.PointerMove.SubscribeWithState(this, (vectors, go) =>
            {
                if (!go.isActiveAndEnabled) return;
                
                var forvardDir = Vector3.ProjectOnPlane(transform.up, Vector3.up);
                transform.Translate(-0.01f * forvardDir * vectors.Item3.y * transform.position.y, Space.World);
                transform.Translate(-0.01f * transform.right * vectors.Item3.x * transform.position.y, Space.World);
            });

            Building.IsMoving.Subscribe(b => enabled = !b);
        }
    }
}
