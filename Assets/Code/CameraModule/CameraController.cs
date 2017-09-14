using CameraModule.Interfaces;
using UnityEngine;

namespace CameraModule.Cameras
{
    public class CameraController : MonoBehaviour, IGameCamera
    {
        [SerializeField]
        private Transform _targetTransform;
        [SerializeField]
        private float _lerpSpeed = 0f;

        private void FixedUpdate()
        {
            if (_targetTransform == null)
            {
                return;
            }
            var targetPosition = new Vector3(_targetTransform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed);
        }

        public void SetTarget(Transform newTarget)
        {
            _targetTransform = newTarget;
        }
    }
}

