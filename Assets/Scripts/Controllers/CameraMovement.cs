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
            
            InputHandler.PointerMove.Subscribe(vector2 =>
            {
                var forvardDir = Vector3.ProjectOnPlane(transform.up, Vector3.up);
                transform.Translate(0.01f * forvardDir * vector2.y * transform.position.y, Space.World);
                transform.Translate(0.01f * transform.right * vector2.x * transform.position.y, Space.World);
            });
        }
    }
}
