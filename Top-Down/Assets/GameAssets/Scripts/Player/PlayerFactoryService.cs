using UnityEngine;
using Zenject;

namespace TopDown {
  public class PlayerFactoryService {
    private PlayerController PlayerController { get; set; }
    private readonly PlayerTypes _playerTypes;
    private readonly GameObject _playerPrefab;
    private readonly Transform _playerSpawnPoint;
    private readonly PlayerInputService _playerInputService;
    private readonly PlayerCamera _playerCamera;
    private readonly EnemyProvider _enemyProvider;

    public PlayerFactoryService(PlayerTypes playerTypes, [Inject(Id = GameIds.PlayerID)] GameObject playerPrefab,
                                [Inject(Id = GameIds.PlayerSpawnPointID)]
                                Transform playerSpawnPoint,
                                PlayerInputService playerInputService, PlayerCamera playerCamera, EnemyProvider enemyProvider) {
      _playerTypes = playerTypes;
      _playerPrefab = playerPrefab;
      _playerSpawnPoint = playerSpawnPoint;
      _playerInputService = playerInputService;
      _playerCamera = playerCamera;
      _enemyProvider = enemyProvider;
    }

    public void Create(PlayerType type) {
      PlayerInfo playerInfo = _playerTypes.GetPlayerTypeItem(type).PlayerInfo;
      GameObject player = Object.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
      GameObject playerView = Object.Instantiate(playerInfo.ViewPrefab, player.transform);
      PlayerController = player.GetComponent<PlayerController>();
      PlayerController.Initialize(_playerInputService, type, _enemyProvider, playerInfo.MoveSpeed, playerInfo.RotationSpeed,
                                  playerInfo.Health, playerInfo.FireDelay, playerInfo.NumberOfRounds);
      _playerCamera.Follow(player.transform);
    }
  }
}