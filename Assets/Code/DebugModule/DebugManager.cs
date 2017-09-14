using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DebugModule
{
    public class DebugManager
    {
        private DebugManagerSettings _settings;

        public void Log(object message, UnityEditor.MessageType messageType = UnityEditor.MessageType.Info, string layer = null)
        {
            if (_settings != null)
            {
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
        }

        public void LogFormat(string message, UnityEditor.MessageType messageType = UnityEditor.MessageType.Info, string layer = null)
        {
            if (_settings != null)
            {
                if (string.IsNullOrEmpty(layer))
                {
                    layer = "Default";
                }

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
    }

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

    [System.Serializable]
    public class DebugManagerSettings
    {
        public List<DebugManagerSettingsParameter> DebugLayers = new List<DebugManagerSettingsParameter>
                {
                    new DebugManagerSettingsParameter("Default", true)
                };

        public DebugManagerSettings()
        {
            if (DebugLayers == null)
            {
                DebugLayers = new List<DebugManagerSettingsParameter>
                {
                    new DebugManagerSettingsParameter("Default", true)
                };
            }

            SaveSettings();
        }

        public bool IsShowLayer(string layerName)
        {
            return DebugLayers != null && DebugLayers.Any() && DebugLayers.First(l => l.LayerName == layerName).Enabled;
        }

        public void AddNewLayer(string layerName)
        {
            if (DebugLayers == null)
            {
                DebugLayers = new List<DebugManagerSettingsParameter>
                {
                    new DebugManagerSettingsParameter("Default", true)
                };
            }

            if (DebugLayers.Exists(l => l.LayerName == layerName))
            {
                return;
            }

            DebugLayers.Add(new DebugManagerSettingsParameter(layerName, true));
        }

        public void RemoveLayer(string layerName)
        {
            if (DebugLayers.Exists(l => l.LayerName == layerName))
            {
                DebugLayers.Remove(DebugLayers.First(l => l.LayerName == layerName));
            }
        }

        public void SaveSettings()
        {
            
        }

        public void LoadSettings()
        {
            
        }
    }
}