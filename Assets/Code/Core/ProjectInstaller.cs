using Assets.Code;
using Core.Settings;
using DebugModule.Installers;
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

            //Project settings
            var projectSettings = Resources.Load<GlobalProjectSettings>(ProjectSettingsPath);
            Container.Bind<GlobalProjectSettings>().FromResource(ProjectSettingsPath).AsSingle();

            //Debug module
            DebugModuleInstaller.Install(Container, projectSettings.EnableLogs);

            //Game manager
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        }
    }
}