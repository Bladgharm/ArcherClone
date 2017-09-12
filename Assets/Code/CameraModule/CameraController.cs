using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            return;
        }
        var targetPosition = new Vector3(_targetTransform.position.x, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        _targetTransform = newTarget;
    }
}
