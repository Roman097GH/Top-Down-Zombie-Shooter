using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  public class PlayerInputService : ITickable {
    public readonly ReactiveCommand<Vector3> OnJoystickMove = new();
    public readonly ReactiveCommand<bool> OnAttack = new();

    private readonly Joystick _joystick;

    public PlayerInputService(Joystick joystick) => _joystick = joystick;

    public void Tick() {
      ReadMovement();
    }

    public void ReadMovement() {
      Vector2 input = _joystick.Direction;
      Vector3 movement = new(input.x, 0, input.y);
      OnJoystickMove.Execute(movement);
    }
  }
}