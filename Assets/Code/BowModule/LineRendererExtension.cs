using UnityEngine;

namespace Assets.Code
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererExtension : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        [SerializeField]
        private Transform _topPoint;
        [SerializeField]
        private Transform _bottomPoint;
        [SerializeField]
        private Transform _centerTarget;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 3;
        }

        private void Update()
        {
            if (_centerTarget != null)
            {
                if (_topPoint != null && _bottomPoint != null)
                {
                    _lineRenderer.SetPosition(0, _topPoint.position);
                    _lineRenderer.SetPosition(1, _centerTarget.position);
                    _lineRenderer.SetPosition(2, _bottomPoint.position);
                }
            }
        }

        public void SetTarget(Transform target)
        {
            _centerTarget = target;
        }
    }
}