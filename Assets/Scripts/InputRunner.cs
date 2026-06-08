using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace DefaultCompany.Player
{
    public class InputRunner : ITickable
    {
        private readonly int playerDamage;
        
        private readonly Camera mainCamera;

        public InputRunner(int playerDamage, Camera mainCamera)
        {
            this.playerDamage = playerDamage;
            this.mainCamera = mainCamera;

            EnhancedTouchSupport.Enable();
        }

        public void Tick()
        {
            var activeTouches = Touch.activeTouches;

            foreach (var touch in activeTouches)
            {
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    InflictDamage(touch.screenPosition);
                }
            }
        }

        private void InflictDamage(Vector2 touchPosition)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable hurt))
                {
                    hurt.TakeDamage(playerDamage);
                }
            }
        }
    }
}