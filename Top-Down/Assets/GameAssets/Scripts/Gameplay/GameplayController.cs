using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace TopDown {
  [UsedImplicitly]
  public class GameplayController : IInitializable, ITickable {
    private readonly PlayerFactoryService _playerFactoryService;

    private readonly EnemyProvider _enemyProvider;

    public GameplayController(PlayerFactoryService playerFactoryService, EnemyProvider enemyProvider) {
      _playerFactoryService = playerFactoryService;
      
      _enemyProvider = enemyProvider;
    }

    void IInitializable.Initialize() {
      _playerFactoryService.Create(PlayerType.EMale);
    }

    public void Tick() { }
  }
}