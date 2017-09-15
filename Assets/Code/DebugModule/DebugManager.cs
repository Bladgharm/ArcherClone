﻿using Zenject;

namespace DebugModule
{
    public class DebugManager : IInitializable
    {
        private DebugManagerSettings _settings;

        public void Log(object message, UnityEditor.MessageType messageType = UnityEditor.MessageType.Info, string layer = null)
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

        public void Initialize()
        {
            UnityEngine.Debug.Log("Init");
            _settings = new DebugManagerSettings();
        }
    }
}