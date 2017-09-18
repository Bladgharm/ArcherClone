using Zenject;

namespace Assets.Code.InputModule
{
    public class InputModuleInstaller : MonoInstaller<InputModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
        }
    }
}
