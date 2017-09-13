using Core.Settings;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public string ProjectSettingsPath = "";

        public override void InstallBindings()
        {
            if (string.IsNullOrEmpty(ProjectSettingsPath))
            {
                Debug.LogError("Project settings path is not assigned!");
                return;
            }

            var projectSettings = Resources.Load<GlobalProjectSettings>(ProjectSettingsPath);

            Container.Bind<GlobalProjectSettings>().FromResource(ProjectSettingsPath).AsSingle();

            //if (projectSettings.UseParam1)
            //{
            //    Debug.Log("Bind with param 1");
            //}
        }
    }
}