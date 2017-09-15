using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DebugModule
{
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

            LoadSettings();
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
            var settingsAsset = Resources.Load<DebugManagerSettingsData>("DebugManagerSettings");
            if (settingsAsset != null)
            {
                settingsAsset.DebugLayers = DebugLayers;
            }
            else
            {
                settingsAsset = CreateSettingsAsset();
                settingsAsset.DebugLayers = DebugLayers;
            }            

            AssetDatabase.SaveAssets();
        }

        public void LoadSettings()
        {
            var settingsAsset = Resources.Load<DebugManagerSettingsData>("DebugManagerSettings");
            if (settingsAsset != null)
            {
                DebugLayers = settingsAsset.DebugLayers;
            }
            else
            {
                settingsAsset = CreateSettingsAsset();
                settingsAsset.DebugLayers = DebugLayers;
                AssetDatabase.SaveAssets();
            }
        }

        [MenuItem("Assets/Debug Manager Settings Asset")]
        public static DebugManagerSettingsData CreateSettingsAsset()
        {
            DebugManagerSettingsData asset = ScriptableObject.CreateInstance<DebugManagerSettingsData>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/DebugManagerSettings.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
            return asset;
        }
    }
}