using System;
using CameraModule.Cameras;
using UnityEngine;

namespace Assets.Code
{
    public class BowController : MonoBehaviour
    {
        [SerializeField]
        private float _shootingForce = 10f;
        [SerializeField]
        private GameObject _arrowPrefab;
        [SerializeField]
        private Transform _arrowSpawnPoint;
        [SerializeField]
        private Transform _bodyTransform;

        private Transform _arrowTransform;

        private float _force = 0f;

        private CameraController _cameraController;

        private void Start()
        {
            _cameraController = GetComponent<CameraController>();
            _cameraController.SetTarget(_bodyTransform);
        }

        public void CreateArrow()
        {
            _arrowTransform = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity).transform;
            _arrowTransform.SetParent(_arrowSpawnPoint);
            _arrowTransform.localPosition = Vector3.zero;
            _arrowTransform.localRotation = Quaternion.identity;
            var arrowComponent = _arrowTransform.GetComponent<ArrowController>();
            arrowComponent.OnArrowHit += OnArrowHit;

            
        }

        private void OnArrowHit()
        {
            Debug.Log("Hit");
            _cameraController.SetTarget(_bodyTransform);
        }

        public void SetupArrow(float angle, float force)
        {
            var rotationVector = new Vector3(0, 0, angle);
            _bodyTransform.rotation = Quaternion.Euler(rotationVector);
            _force = _shootingForce * force;

        }

        public void Shoot()
        {
            _arrowTransform.SetParent(null);
            var arrowRigidbody = _arrowTransform.GetComponent<Rigidbody2D>();
            var velocity = _arrowTransform.right * _force;
            arrowRigidbody.velocity = velocity;
            arrowRigidbody.isKinematic = false;

            _cameraController.SetTarget(_arrowTransform);
            var rotationVector = new Vector3(0, 0, 0);
            _bodyTransform.rotation = Quaternion.Euler(rotationVector);
        }
    }
}