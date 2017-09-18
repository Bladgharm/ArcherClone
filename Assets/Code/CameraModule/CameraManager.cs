using CameraModule.Interfaces;
using DebugModule;
using UnityEngine;
using Zenject;

namespace CameraModule.Cameras
{
    public class CameraManager : IGameCamera, IInitializable, IFixedTickable
    {
        private Transform _targetTransform;
        private float _lerpSpeed = 0f;

        private Transform _currentCameraTransform;

        [Inject]
        private DebugManager _debugManager;

        public Camera CurrentCamera { get; set; }

        public CameraManager(float cameraLerpSpeed)
        {
            _lerpSpeed = cameraLerpSpeed;
        }

        public void SetTarget(Transform newTarget)
        {
            _targetTransform = newTarget;
        }

        public void Initialize()
        {
            _debugManager.Log("Camera manager init with lerpSpeed: " + _lerpSpeed);

            if (CurrentCamera == null)
            {
                CurrentCamera = Camera.main;
            }

            _debugManager.Log("Current camera = "+CurrentCamera);
        }

        public void FixedTick()
        {
            if (_targetTransform == null)
            {
                return;
            }
            var targetPosition = new Vector3(_targetTransform.position.x, _currentCameraTransform.position.y, _currentCameraTransform.position.z);
            _currentCameraTransform.position = Vector3.Lerp(_currentCameraTransform.position, targetPosition, _lerpSpeed);
        }

    }
}

