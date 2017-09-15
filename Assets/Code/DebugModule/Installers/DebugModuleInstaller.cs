using UnityEngine;
using Zenject;

namespace DebugModule.Installers
{
    public class DebugModuleInstaller : Installer<DebugModuleInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("Debug manager install");
            Container.Bind<IInitializable>().To<DebugManager>().AsSingle();
        }
    }
}