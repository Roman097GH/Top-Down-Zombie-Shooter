using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  public class InputHandler : ITickable {
    private readonly Joystick _joystick;
    //private readonly Button _buttonAttack;

    public readonly ReactiveCommand<Vector3> OnMove = new();

    public readonly ReactiveCommand<Vector3> OnRotate = new();
    //public readonly ReactiveCommand<EventTrigger> Attack = new();

    public InputHandler(Joystick joystick) {
      _joystick = joystick;
      //_buttonAttack = buttonAttack;
    }

    public void Tick() {
      if (_joystick.Direction == Vector2.zero) return;
      OnMove?.Execute(_joystick.Direction);
      OnRotate?.Execute(_joystick.Direction);
      //Attack?.Execute(_buttonAttack.gameObject.GetComponent<EventTrigger>().);
    }
  }
}