using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] private GameActivePanelUI _gameActivePanelObjectUI;
        [SerializeField] private BestStatsUI _gameWinPanelObjectUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyProvider>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameSessionStats>().AsSingle();

            Container.BindInterfacesTo<GameActivePanelUI>().FromInstance(_gameActivePanelObjectUI);
            Container.BindInterfacesTo<BestStatsUI>().FromInstance(_gameWinPanelObjectUI);

            Container.BindInstance(_playerSpawnPoint).WithId(GameIds.PlayerSpawnPointID);
            Container.BindInstance(_playerPrefab).WithId(GameIds.PlayerID);

            Container.Bind<PlayerFactoryService>().AsSingle();
            Container.Bind<EnemyFactoryService>().AsSingle();

            Container.Bind<PlayerCamera>().FromInstance(_playerCamera);
            Container.Bind<Joystick>().FromInstance(_joystick);

            BindScriptableObjects();
        }

        private void BindScriptableObjects() => Container.BindInstance(_playerTypes);
    }
}