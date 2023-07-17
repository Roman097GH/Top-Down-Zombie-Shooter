using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  public class InputHandler : ITickable {
    private readonly Joystick _joystick;

    public readonly ReactiveCommand<Vector3> OnMove = new();
    public readonly ReactiveCommand<Vector3> OnRotate = new();

    public InputHandler(Joystick joystick) {
      _joystick = joystick;
    }

    public void Tick() {
      if (_joystick.Direction == Vector2.zero) return;
      OnMove?.Execute(_joystick.Direction);
      OnRotate?.Execute(_joystick.Direction);
    }
  }
}