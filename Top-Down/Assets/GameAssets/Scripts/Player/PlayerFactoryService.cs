using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class PlayerFactoryService
    {
        public PlayerController PlayerController { get; private set; }
        private readonly PlayerTypes _playerTypes;
        private readonly GameObject _playerPrefab;
        private readonly Transform _playerSpawnPoint;
        private readonly PlayerInputService _playerInputService;
        private readonly PlayerCamera _playerCamera;
        private readonly EnemyProvider _enemyProvider;
        private readonly HealthItem _healthItem;
        private readonly BulletItem _bulletItem;

        public readonly ReactiveCommand PlayerCreate = new();

        public PlayerFactoryService(PlayerTypes playerTypes, [Inject(Id = GameIds.PlayerID)] GameObject playerPrefab,
            [Inject(Id = GameIds.PlayerSpawnPointID)]
            Transform playerSpawnPoint,
            PlayerInputService playerInputService, PlayerCamera playerCamera,
            EnemyProvider enemyProvider, HealthItem healthItem, BulletItem bulletItem)
        {
            _playerTypes = playerTypes;
            _playerPrefab = playerPrefab;
            _playerSpawnPoint = playerSpawnPoint;
            _playerInputService = playerInputService;
            _playerCamera = playerCamera;
            _enemyProvider = enemyProvider;
            _healthItem = healthItem;
            _bulletItem = bulletItem;
        }

        public void Create(PlayerType type)
        {
            PlayerInfo playerInfo = _playerTypes.GetPlayerTypeItem(type).PlayerInfo;
            GameObject player = Object.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            Object.Instantiate(playerInfo.ViewPrefab, player.transform);
            PlayerController = player.GetComponent<PlayerController>();
            PlayerController.Initialize(_playerInputService, _enemyProvider, playerInfo.MoveSpeed,
                playerInfo.RotationSpeed, playerInfo.Health, playerInfo.NumberOfBullets, playerInfo.CountShotPerMinute,
                playerInfo.Damage, playerInfo.DecelerationRate, _healthItem, _bulletItem);
            _playerCamera.Follow(player.transform);

            PlayerCreate.Execute();
        }
    }
}