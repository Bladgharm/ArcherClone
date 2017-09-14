using Zenject;

namespace DebugModule.Installers
{
    public class DebugModuleInstaller : MonoInstaller<bool, DebugModuleInstaller>
    {
        private bool _debugsEnabled;
    }
}