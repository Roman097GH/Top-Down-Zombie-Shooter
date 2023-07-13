using UnityEngine;
using Zenject;

namespace TopDown {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private Player _player;
    [SerializeField] private Joystick _joystick;

    public override void InstallBindings() {
      Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();

      Container.Bind<Joystick>().FromInstance(_joystick);

      Container.BindInstance(_player);

      Container.Bind<EnemyFactoryService>().AsSingle();
    }
  }
}