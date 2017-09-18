using UnityEngine;

namespace Core.Settings
{
    public class GlobalProjectSettings : ScriptableObject
    {
        public bool ProjectOrientation3D = false;
        public bool EnableLogs = true;

        [Header("CameraSettings")]
        public float CameraLerpSpeed = 10f;
    }
}