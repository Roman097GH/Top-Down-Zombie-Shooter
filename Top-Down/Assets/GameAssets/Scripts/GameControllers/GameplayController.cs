using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace TopDown {
  [UsedImplicitly]
  public class GameplayController : IInitializable, ITickable {
    private readonly PlayerFactoryService _playerFactoryService;

    private readonly EnemyProvider _enemyProvider;

    private GamePanelUI _gamePanelUI;
    private PlayerController _playerController;

    // [Inject]
    // private void Construct(GamePanelUI gamePanelUI, PlayerController playerController) {
    //   _gamePanelUI = gamePanelUI;
    //   _playerController = playerController;
    // }
    
    // private void Attack() {
    //   _playerController.FireDelayProcessing();
    // }
    
    public GameplayController(PlayerFactoryService playerFactoryService, EnemyProvider enemyProvider) {
      _playerFactoryService = playerFactoryService;
      _enemyProvider = enemyProvider;
    }

    void IInitializable.Initialize() {
      _playerFactoryService.Create(PlayerType.EMale);
      //_gamePanelUI.OnAttack.Subscribe(_ => Attack());
    }

    public void Tick() { }
  }
}