namespace DebugModule
{
    [System.Serializable]
    public class DebugManagerSettingsParameter
    {
        public string LayerName;
        public bool Enabled;

        public DebugManagerSettingsParameter(string layerName, bool isEnabled)
        {
            LayerName = layerName;
            Enabled = isEnabled;
        }
    }
}