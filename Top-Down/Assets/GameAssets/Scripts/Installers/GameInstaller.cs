using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace TopDown
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerTypes _playerTypes;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private GamePanelUI _gamePanelObjectUI;
        [SerializeField] private HealthItem _healthItem;
        [SerializeField] private BulletItem _bulletItem;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();

            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();

            Container.BindInstance(_playerSpawnPoint).WithId(GameIds.PlayerSpawnPointID);
            Container.BindInstance(_playerPrefab).WithId(GameIds.PlayerID);

            Container.Bind<PlayerFactoryService>().AsSingle();
            Container.Bind<EnemyFactoryService>().AsSingle();

            Container.Bind<PlayerCamera>().FromInstance(_playerCamera);
            Container.Bind<Joystick>().FromInstance(_joystick);
            Container.Bind<HealthItem>().FromInstance(_healthItem);
            Container.Bind<BulletItem>().FromInstance(_bulletItem);

            Container.BindInterfacesTo<GamePanelUI>().FromInstance(_gamePanelObjectUI);

            BindScriptableObjects();
        }

        private void BindScriptableObjects() => Container.BindInstance(_playerTypes);
    }
}