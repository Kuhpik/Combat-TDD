using Zenject;

namespace Game.Installers
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Application>().AsSingle().NonLazy();
            Container.BindExecutionOrder<Application>(100);
        }
    }
}
