using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  public class InputHandler : ITickable {
    private readonly Joystick _joystick;

    public readonly ReactiveCommand<Vector3> OnJoystickMove = new();

    public InputHandler(Joystick joystick) {
      _joystick = joystick;
    }

    public void Tick() {
      if (_joystick.Direction == Vector2.zero) return;
      OnJoystickMove?.Execute(_joystick.Direction);
    }
  }
}