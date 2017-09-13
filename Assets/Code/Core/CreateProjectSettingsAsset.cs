using UnityEditor;
using UnityEngine;

namespace Core.Settings
{
    public class CreateProjectSettingsAsset
    {
        [MenuItem("Assets/Project Settings Asset")]
        public static void CreateMyAsset()
        {
            GlobalProjectSettings asset = ScriptableObject.CreateInstance<GlobalProjectSettings>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/ProjectSettings.asset");
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}