using Assets.Code;
using DebugModule;
using UnityEngine;

public class BowInputController : MonoBehaviour
{
    [SerializeField]
    private float _force = 0f;
    [SerializeField]
    private float _shootingAngle = 0f;
    [SerializeField]
    private float _maxForceRadius = 0f;
    [SerializeField]
    private Transform _bowTransform;

    private DebugModule.DebugManagerSettings _settings = new DebugManagerSettings();

    [Header("Debug settings")]
    [SerializeField]
    private bool _drawGizmos = true;

    private Vector2 _startPoint;
    private Vector2 _endPoint;

    private Vector2 _forvardVector;

    private BowController _bowController;

    private void Start()
    {
        _bowController = GetComponent<BowController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startPoint = _bowTransform.position;
            _forvardVector = Vector2.right;
            _bowController.CreateArrow();
        }

        if (Input.GetMouseButton(0))
        {
            var newEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var vector = new Vector2(_startPoint.x - newEndPosition.x, _startPoint.y - newEndPosition.y);
            _endPoint = _startPoint - Vector2.ClampMagnitude(vector, _maxForceRadius);

            var distance = Vector2.Distance(_startPoint, _endPoint);
            _force = Mathf.Clamp01(distance / _maxForceRadius);
            _shootingAngle = Vector2.Angle(_forvardVector, _startPoint - _endPoint);
            if (_endPoint.y > _startPoint.y)
            {
                _shootingAngle = -_shootingAngle;
            }

            _bowController.SetupArrow(_shootingAngle, _force);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _bowController.Shoot();
        }
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_startPoint, _startPoint - _endPoint);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_startPoint, _maxForceRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_startPoint, 0.3f);
        Gizmos.DrawWireSphere(_endPoint, 0.3f);
        Gizmos.DrawLine(_startPoint, _endPoint);
    }
}
