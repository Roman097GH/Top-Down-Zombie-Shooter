using Zenject;

namespace TopDown
{
    public class RootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameParametrs>().AsSingle().NonLazy();
        }
    }
}