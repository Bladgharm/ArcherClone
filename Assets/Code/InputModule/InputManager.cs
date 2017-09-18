using DebugModule;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Code.InputModule
{
    public class InputManager : IInitializable, ITickable
    {
        private bool _isLocked = false;

        [Inject]
        private DebugManager _debugManager;
        [Inject]
        private GameManager _gameManager;
        /*Inject game manager with current character*/

        public void Initialize()
        {
            
        }

        public void Tick()
        {
            if (_isLocked)
            {
                return;
            }

            if (!EventSystem.current.IsPointerOverGameObject(0) && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                }
            }
        }

        public void SetLockInput(bool locked)
        {
            _isLocked = locked;
        }
    }
}