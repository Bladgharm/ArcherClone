using Zenject;

namespace DebugModule.Installers
{
    public class DebugModuleInstaller : Installer<bool, DebugModuleInstaller>
    {
        private readonly bool _enableLogs;

        public DebugModuleInstaller(bool enableLogs)
        {
            _enableLogs = enableLogs;
        }

        public override void InstallBindings()
        {
            Container.Bind<DebugManager>().AsSingle();
            Container.Bind<IInitializable>().To<DebugManager>().AsSingle();
        }
    }
}