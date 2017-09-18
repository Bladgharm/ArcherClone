using UnityEngine;

namespace CameraModule.Interfaces
{
    public interface IGameCamera
    {
        Camera CurrentCamera { get; set; }

        void SetTarget(Transform newTarget);
    }
}