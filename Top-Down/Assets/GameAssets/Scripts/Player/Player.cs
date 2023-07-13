using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(CapsuleCollider))]
  public class Player : MonoBehaviour {
    private InputHandler _inputHandler;

    [SerializeField] private Rigidbody _playerRb;

    private Vector3 _movement;
    [SerializeField] private float _moveSpeed = 3.0f;

    [SerializeField] private Animator _animator;
    private static readonly int _velocity = Animator.StringToHash("Velocity");
    private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");

    private float _movementMagnitude;
    [SerializeField] private float _animationToIdleSpeed = 1.5f;

    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackPeriod;

    private void OnValidate() => _playerRb = GetComponent<Rigidbody>();

    [Inject]
    private void Construct(InputHandler inputHandler) {
      _inputHandler = inputHandler;
      _inputHandler.OnMove.Subscribe(Move);
      _inputHandler.OnRotate.Subscribe(Rotate);
    }

    private void Attack() {
      _animator.SetTrigger(_attackAnimTrig);
    }

    private void Update() {
      _attackTimer += Time.unscaledDeltaTime;

      if (_attackTimer > _attackPeriod) {
        if (Input.GetMouseButtonDown(1)) {
          _attackTimer = 0;
          Attack();
        }
      }

      if (!(_movementMagnitude > 0.0f)) return;
      _movementMagnitude -= Time.deltaTime * _animationToIdleSpeed;
      _animator.SetFloat(_velocity, _movementMagnitude);
    }

    private void Move(Vector3 movement) {
      _movement = new Vector3(movement.x, 0.0f, movement.y);
      _movementMagnitude = _movement.magnitude;

      _playerRb.velocity = Vector3.ClampMagnitude(_movement, 1.0f) * _moveSpeed;

      _animator.SetFloat(_velocity, _movementMagnitude);
    }

    private void Rotate(Vector3 direction) => transform.rotation = Quaternion.LookRotation(_playerRb.velocity);
  }
}