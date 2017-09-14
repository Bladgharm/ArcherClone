using CameraModule.Interfaces;
using Core.Settings;
using Zenject;

namespace CameraModule.Installers
{
    public class CameraInstaller : MonoInstaller<CameraInstaller>
    {
        [Inject]
        private GlobalProjectSettings _projectSettings;

        public override void InstallBindings()
        {
            Container.Bind<IGameCamera>().To<Cameras.CameraController>().AsSingle();
        }
    }
}