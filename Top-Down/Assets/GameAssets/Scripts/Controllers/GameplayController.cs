using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace TopDown {
  [UsedImplicitly]
  public class GameplayController : IInitializable {
    private readonly PlayerFactoryService _playerFactoryService;

    public GameplayController(PlayerFactoryService playerFactoryService) {
      _playerFactoryService = playerFactoryService;
    }
    
    void IInitializable.Initialize() {
      _playerFactoryService.Create(PlayerType.EMale);
    }
  }
}