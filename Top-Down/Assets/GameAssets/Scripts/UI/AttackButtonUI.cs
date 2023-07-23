using UniRx;
using Zenject;

namespace TopDown {
  public class AttackButtonUI : ButtonUI {
    private PlayerInputService _playerInputService;

    [Inject]
    private void Construct(PlayerInputService playerInputService) {
      _playerInputService = playerInputService;
      ButtonState.TakeUntilDestroy(this).Subscribe(TriggerAttack);
    }

    private void TriggerAttack(bool state) => _playerInputService.OnAttack.Execute(state);
  }
}