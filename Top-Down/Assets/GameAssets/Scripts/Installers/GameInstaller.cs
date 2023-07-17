using UnityEngine;
using Zenject;

namespace TopDown {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerTypes _playerTypes;

    [SerializeField] private GameObject _camera;

    public override void InstallBindings() {
      Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();

      Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();

      Container.BindInstance(_playerPrefab).WithId(GameIds.PlayerID);

      Container.Bind<Joystick>().FromInstance(_joystick);


      Container.Bind<PlayerFactoryService>().AsSingle();
      Container.Bind<EnemyFactoryService>().AsSingle();

      Container.BindInstance(_camera);
      
      BindScriptableObjects();
    }

    private void BindScriptableObjects() => Container.BindInstance(_playerTypes);
  }
}