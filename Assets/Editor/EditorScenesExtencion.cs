using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Editor
{
    public class EditorScenesExtencion : EditorWindow
    {
        private readonly string _settingsKey = "EDITOR_EXTENCIONS_KEY";

        private int _mainScene = 0;
        private EditorBuildSettingsScene _selectedMainScene;
        private int _currentTab;
        private readonly string[] _tabs = new string[]
        {
            "Scenes",
            "Manager",
            "Settings"
        };

        private string[] _buildScenes;
        private ReorderableList _reorderableList;
        private List<EditorBuildSettingsScene> _scenesList;

        private bool _startedFromHere = false;
        private string _lastScenePath;
        private static TempraryEditorData _tempData;

        private Vector2 _scrollPos;

        private string _settingsPath;

        [MenuItem("Window/ScenesManager")]
        static void Init()
        {
            EditorScenesExtencion window = (EditorScenesExtencion) EditorWindow.GetWindow(typeof(EditorScenesExtencion));
            window.minSize = new Vector2(250f, 100f);
            window.Show();
        }

        void OnGUI()
        {
            _currentTab = GUILayout.Toolbar(_currentTab, _tabs);
            switch (_currentTab)
            {
                case 0:
                {
                    DrawScenesHelper();
                    }
                    break;
                case 1:
                {
                    DrawScenesManager();
                }
                    break;
                case 2:
                {
                    DrawSettings();
                }
                    break;
            }
            
        }

        void OnInspectorUpdate()
        {
            this.Repaint();
        }

        void OnEnable()
        {
            //OpenTemp();
            _scenesList = new List<EditorBuildSettingsScene>();
            _reorderableList = new ReorderableList(_scenesList, typeof(EditorBuildSettingsScene), true, true, false, false);
            _reorderableList.drawHeaderCallback += DrawListHeader;
            _reorderableList.drawElementCallback += DrawListElement;
            _reorderableList.onReorderCallback += OnListReorderCallback;
        }

        private void OnListReorderCallback(ReorderableList list)
        {
            EditorBuildSettings.scenes = ((List<EditorBuildSettingsScene>) list.list).ToArray();
        }

        private void DrawListElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            var item = _scenesList[index];
            string sceneName = item.path.Substring(item.path.LastIndexOf('/') + 1);
            sceneName = sceneName.Substring(0, sceneName.Length - 6);

            EditorGUI.BeginChangeCheck();
            bool sceneEnabled = EditorGUI.Toggle(new Rect(rect.x, rect.y, 18, rect.height), item.enabled);
            EditorGUI.LabelField(new Rect(rect.x + 18, rect.y, rect.width - 18, rect.height), sceneName);
            if (EditorGUI.EndChangeCheck())
            {
                item.enabled = sceneEnabled;
                EditorBuildSettings.scenes = ((List<EditorBuildSettingsScene>)_reorderableList.list).ToArray();
            }
        }

        private void DrawListHeader(Rect rect)
        {
            GUI.Label(rect, "Build scenes");
        }

        void OnDisable()
        {
            _reorderableList.drawHeaderCallback -= DrawListHeader;
            _reorderableList.drawElementCallback -= DrawListElement;
        }

        private void DrawScenesHelper()
        {
            var scenes = GetScenes();
            var activeScene = SceneManager.GetActiveScene();
            _buildScenes = GetScenesNames();

            GUILayout.BeginHorizontal();
            _mainScene = EditorGUILayout.Popup("MainScene: ", _mainScene, _buildScenes);
            _selectedMainScene = EditorBuildSettings.scenes[_mainScene];
            if (!EditorApplication.isPlaying)
            {
                if (GUILayout.Button("Start"))
                {
                    _lastScenePath = SceneManager.GetActiveScene().path;
                    EditorSceneManager.OpenScene(_selectedMainScene.path, OpenSceneMode.Single);
                    _startedFromHere = true;
                    EditorApplication.isPlaying = true;
                }
            }
            else
            {
                if (GUILayout.Button("Stop"))
                {
                    EditorApplication.isPlaying = false;
                    EditorApplication.playmodeStateChanged += () =>
                    {
                        if (!EditorApplication.isPlaying)
                        {
                            if (!string.IsNullOrEmpty(_lastScenePath))
                            {
                                EditorSceneManager.OpenScene(_lastScenePath);
                                _lastScenePath = string.Empty;
                            }
                        }
                    };
                    
                }
            }
            GUILayout.EndHorizontal();
            if (!IsSceneContainsInBuildSettings(activeScene))
            {
                if (GUILayout.Button("Add scene to build settings"))
                {
                    EditorSceneManager.SaveScene(activeScene);
                    var original = EditorBuildSettings.scenes;
                    var newSettings = new EditorBuildSettingsScene[original.Length + 1];
                    System.Array.Copy(original, newSettings, original.Length);
                    var sceneToAdd = new EditorBuildSettingsScene(activeScene.path, true);
                    newSettings[newSettings.Length - 1] = sceneToAdd;
                    EditorBuildSettings.scenes = newSettings;
                }
            }

            GUILayout.BeginHorizontal(GUI.skin.box);
            foreach (var scene in scenes)
            {
                string sceneName = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                sceneName = sceneName.Substring(0, sceneName.Length - 6);
                GUILayout.BeginHorizontal(GUI.skin.box);
                if (GUILayout.Button(sceneName))
                {
                    EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
                }
                if (GUILayout.Button("X", GUILayout.MaxWidth(20f)))
                {
                    int numIdx = Array.IndexOf(scenes, scene);
                    List<EditorBuildSettingsScene> tmp = new List<EditorBuildSettingsScene>(scenes);
                    tmp.RemoveAt(numIdx);
                    EditorBuildSettings.scenes = tmp.ToArray();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawScenesManager()
        {
            var activeScene = SceneManager.GetActiveScene();

            if (!IsSceneContainsInBuildSettings(activeScene))
            {
                if (GUILayout.Button("Add scene to build settings"))
                {
                    EditorSceneManager.SaveScene(activeScene);
                    var original = EditorBuildSettings.scenes;
                    var newSettings = new EditorBuildSettingsScene[original.Length + 1];
                    System.Array.Copy(original, newSettings, original.Length);
                    var sceneToAdd = new EditorBuildSettingsScene(activeScene.path, true);
                    newSettings[newSettings.Length - 1] = sceneToAdd;
                    EditorBuildSettings.scenes = newSettings;
                }
            }
            _scenesList = GetScenes(true).ToList();
            _reorderableList.list = _scenesList;
            if (_scenesList != null && _scenesList.Any())
            {
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                GUILayout.BeginVertical(GUI.skin.box);
                _reorderableList.DoLayoutList();
                GUILayout.EndVertical();
                EditorGUILayout.EndScrollView();
            }   
        }

        private void DrawSettings()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Work in progress");
            GUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label("Settings path: ");
            _settingsPath = GUILayout.TextField(_settingsPath);
            if (GUILayout.Button("Select"))
            {
                _settingsPath = EditorUtility.OpenFolderPanel("Settings data folder", Application.dataPath, "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Work in progress: next contains path to data file for saving extencion data");
            GUILayout.EndVertical();
        }

        private bool IsSceneContainsInBuildSettings(Scene scene)
        {
            if (EditorBuildSettings.scenes.Any(s => s.path == scene.path))
                return true;
            return false;
        }

        private EditorBuildSettingsScene[] GetScenes(bool showDisabled = false)
        {
            List<EditorBuildSettingsScene> temp = new List<EditorBuildSettingsScene>();
            if (showDisabled)
            {
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    temp.Add(scene);
                }
            }
            else
            {
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (scene.enabled)
                    {
                        temp.Add(scene);
                    }
                }
            }
            return temp.ToArray();
        }

        private string[] GetScenesNames()
        {
            string[] temp = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                string sceneName = EditorBuildSettings.scenes[i].path.Substring(EditorBuildSettings.scenes[i].path.LastIndexOf('/') + 1);
                sceneName = sceneName.Substring(0, sceneName.Length - 6);
                temp[i] = sceneName;
            }
            return temp;
        }

        private void OpenTemp()
        {
            if (EditorPrefs.HasKey(_settingsKey))
            {
                _settingsPath = EditorPrefs.GetString(_settingsKey);
            }

            _tempData = AssetDatabase.LoadAssetAtPath(_settingsPath, typeof(TempraryEditorData)) as TempraryEditorData;
            if (_tempData == null)
            {
                _tempData = ScriptableObject.CreateInstance<TempraryEditorData>();
                string guid = AssetDatabase.CreateFolder("Assets", "Editor Data");
                _settingsPath = AssetDatabase.GUIDToAssetPath(guid) + "/TempraryEditorData.asset";
                AssetDatabase.CreateAsset(_tempData, _settingsPath);
                AssetDatabase.SaveAssets();
                EditorPrefs.SetString(_settingsKey, _settingsPath);
            }
        }
    }
}