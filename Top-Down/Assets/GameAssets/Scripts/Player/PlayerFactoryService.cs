using UnityEngine;
using Zenject;

namespace TopDown {
  public class PlayerFactoryService {
    private readonly DiContainer _diContainer;
    private readonly GameObject _playerPrefab;
    private readonly PlayerTypes _playerTypes;
    public PlayerController PlayerController { get; private set; }

    public PlayerFactoryService(DiContainer diContainer, [Inject(Id = GameIds.PlayerID)] GameObject playerPrefab,
                         PlayerTypes playerTypes) {
      _diContainer = diContainer;
      _playerPrefab = playerPrefab;
      _playerTypes = playerTypes;
    }

    public void Create(PlayerType type) {
      PlayerInfo playerInfo = _playerTypes.GetPlayerTypeItem(type).PlayerInfo;
      GameObject player = Object.Instantiate(_playerPrefab);
      GameObject playerView = Object.Instantiate(playerInfo.ViewPrefab, player.transform);
      PlayerController = player.GetComponent<PlayerController>();
      _diContainer.Inject(PlayerController);
      PlayerController.Initialize(type, playerInfo.MoveSpeed, playerInfo.RotationSpeed, playerInfo.Health);
    }
  }
}