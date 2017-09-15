using Zenject;

namespace DebugModule
{
    public class DebugManager : IInitializable
    {
        private DebugManagerSettings _settings;

        //public DebugManager(bool enableLogs)
        //{
        //    UnityEngine.Debug.Log("Debug manager: "+enableLogs);
        //}

        public void Log(object message, UnityEditor.MessageType messageType = UnityEditor.MessageType.Info, string layer = null)
        {
            if (_settings != null)
            {
                if (string.IsNullOrEmpty(layer))
                {
                    layer = "Default";
                }

                message = ApplyColor(message.ToString(), _settings.GetLayerTextColor(layer));

                if (_settings.IsShowLayer(layer))
                {
                    switch (messageType)
                    {
                        case UnityEditor.MessageType.None:
                        {
                            UnityEngine.Debug.Log(message);
                        }
                            break;
                        case UnityEditor.MessageType.Info:
                        {
                            UnityEngine.Debug.Log(message);
                        }
                            break;
                        case UnityEditor.MessageType.Warning:
                        {
                            UnityEngine.Debug.LogWarning(message);
                        }
                            break;
                        case UnityEditor.MessageType.Error:
                        {
                            UnityEngine.Debug.LogError(message);
                        }
                            break;
                    }
                }
            }
            else
            {
                UnityEngine.Debug.Log("Debug settings is not loaded");
            }
        }

        public void LogFormat(string message, UnityEditor.MessageType messageType = UnityEditor.MessageType.Info, string layer = null)
        {
            if (_settings != null)
            {
                if (string.IsNullOrEmpty(layer))
                {
                    layer = "Default";
                }

                message = ApplyColor(message.ToString(), _settings.GetLayerTextColor(layer));

                if (_settings.IsShowLayer(layer))
                {
                    switch (messageType)
                    {
                        case UnityEditor.MessageType.None:
                        {
                            UnityEngine.Debug.LogFormat(message);
                        }
                            break;
                        case UnityEditor.MessageType.Info:
                        {
                            UnityEngine.Debug.LogFormat(message);
                        }
                            break;
                        case UnityEditor.MessageType.Warning:
                        {
                            UnityEngine.Debug.LogWarningFormat(message);
                        }
                            break;
                        case UnityEditor.MessageType.Error:
                        {
                            UnityEngine.Debug.LogErrorFormat(message);
                        }
                            break;
                    }
                }
            }
        }

        public string ApplyColor(string message, UnityEngine.Color color)
        {
            return string.Format("<color={0}>{1}</color>", "#"+UnityEngine.ColorUtility.ToHtmlStringRGB(color), message);
        }

        public string ColorToHex(UnityEngine.Color color)
        {
            return "#" + color.r.ToString("x2") + color.g.ToString("x2") + color.b.ToString("x2") + color.a.ToString("x2");
        }

        public void Initialize()
        {
            _settings = new DebugManagerSettings();
        }
    }
}