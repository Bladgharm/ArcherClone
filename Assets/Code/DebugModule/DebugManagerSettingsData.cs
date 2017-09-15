using System.Collections.Generic;
using UnityEngine;

namespace DebugModule
{
    [System.Serializable]
    public class DebugManagerSettingsData : ScriptableObject
    {
        public List<DebugManagerSettingsParameter> DebugLayers;
    }
}