using UnityEngine;
using Zenject;

namespace TopDown {
  public class PlayerFactoryService {
    private PlayerController PlayerController { get; set; }
    private readonly PlayerTypes _playerTypes;
    private readonly GameObject _playerPrefab;
    private readonly Transform _playerSpawnPoint;
    private readonly InputHandler _inputHandler;
    private readonly FollowCamera _followCamera;
    private readonly EnemyProvider _enemyProvider;

    public PlayerFactoryService(PlayerTypes playerTypes, [Inject(Id = GameIds.PlayerID)] GameObject playerPrefab,
                                [Inject(Id = GameIds.PlayerSpawnPointID)]
                                Transform playerSpawnPoint,
                                InputHandler inputHandler, FollowCamera followCamera, EnemyProvider enemyProvider) {
      _playerTypes = playerTypes;
      _playerPrefab = playerPrefab;
      _playerSpawnPoint = playerSpawnPoint;
      _inputHandler = inputHandler;
      _followCamera = followCamera;
      _enemyProvider = enemyProvider;
    }

    public void Create(PlayerType type) {
      PlayerInfo playerInfo = _playerTypes.GetPlayerTypeItem(type).PlayerInfo;
      GameObject player = Object.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
      GameObject playerView = Object.Instantiate(playerInfo.ViewPrefab, player.transform);
      PlayerController = player.GetComponent<PlayerController>();
      PlayerController.Initialize(_inputHandler, type, _enemyProvider, playerInfo.MoveSpeed, playerInfo.RotationSpeed,
                                  playerInfo.Health, playerInfo.FireDelay, playerInfo.NumberOfRounds);
      _followCamera.Follow(player.transform);
    }
  }
}