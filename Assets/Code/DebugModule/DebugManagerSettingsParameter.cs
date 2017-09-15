namespace DebugModule
{
    [System.Serializable]
    public class DebugManagerSettingsParameter
    {
        public string LayerName;
        public bool Enabled;
        public UnityEngine.Color Color;

        public DebugManagerSettingsParameter(string layerName, bool isEnabled, UnityEngine.Color color)
        {
            LayerName = layerName;
            Enabled = isEnabled;
            Color = color;
        }
    }
}