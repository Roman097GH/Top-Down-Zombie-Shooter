using UniRx;
using UnityEngine;

namespace TopDown {
  [RequireComponent(typeof(CharacterController))]
  public class PlayerController : MonoBehaviour {
    [SerializeField, HideInInspector] private CharacterController _controller;
    [SerializeField] private ShootBase _shootBase;
    [SerializeField] private Damageable _damageable;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _numberOfRounds;

    [SerializeField] private float _decelerationRate = 1.5f;
    private float _movementMagnitude;

    private EnemyProvider _enemyProvider;
    private PlayerInputService _playerInputService;
    private Transform _targetTransform;

    private Vector3 _currentMovement = Vector3.zero;

    private static readonly int _velocity = Animator.StringToHash("Velocity");
    private static readonly int _shotAnimTrig = Animator.StringToHash("ShotTrig");
    private static readonly int _deathAnimTrig = Animator.StringToHash("DeathTrig");
    private bool _fire;
    private float _nextTimeToFire;
    [SerializeField] private int _countShotPerMinute = 60;

    public void Initialize(PlayerInputService playerInputService, PlayerType type, EnemyProvider enemyProvider,
                           float playerInfoMoveSpeed, float playerInfoRotationSpeed, float playerInfoHealth,
                           float playerInfoFireDelay, float playerInfoNumberOfRounds) {
      _playerInputService = playerInputService;
      _enemyProvider = enemyProvider;
      _moveSpeed = playerInfoMoveSpeed;
      _rotationSpeed = playerInfoRotationSpeed;
      _health = playerInfoHealth;
      _numberOfRounds = playerInfoNumberOfRounds;

      _animator = GetComponentInChildren<Animator>();

      _playerInputService.OnJoystickMove.TakeUntilDestroy(this).Subscribe(HandleMovement);
      _playerInputService.OnAttack.TakeUntilDestroy(this).Subscribe(SetFire);
      _damageable.SetHealth(_health);
      Debug.Log(gameObject.name + " Health: " + _health);
    }

    private void SetFire(bool fire) => _fire = fire;

    private void OnValidate() => _controller = GetComponent<CharacterController>();

    private void Update() {
      FireDelayProcessing();

      if (_damageable.Health.Value == 0) {
        _animator.SetTrigger(_deathAnimTrig);
      }

      GetPlayerPosition();
      Transform transformClosestEnemy = _enemyProvider.GetEnemyClosestTo(GetPlayerPosition());
      SetFollowTarget(transformClosestEnemy);

      if (_targetTransform != null) {
        RotatePlayer(_targetTransform.position - transform.position);
      }

      if (!(_movementMagnitude > 0.0f)) return;
      _movementMagnitude -= Time.deltaTime * _decelerationRate;
      _movementMagnitude = Mathf.Clamp(_movementMagnitude, 0.0f, 1.0f);
      _animator.SetFloat(_velocity, _movementMagnitude);
    }

    private void FireDelayProcessing() {
      if (!_fire || _nextTimeToFire > Time.time) return;
      _nextTimeToFire = Time.time + 60f / _countShotPerMinute;
      _numberOfRounds -= 1;
      Debug.Log(_numberOfRounds);
      Shooting();
    }

    private void SetFollowTarget(Transform targetTransform) => _targetTransform = targetTransform;

    private Vector3 GetPlayerPosition() => transform.position;

    private void Shooting() {
      _shootBase.Shot();
      _animator.SetTrigger(_shotAnimTrig);
    }

    private void HandleMovement(Vector3 movement) {
      Move(movement);
      Rotate(movement);
    }

    private void Move(Vector3 movement) {
      _currentMovement = movement * _moveSpeed;
      _movementMagnitude = movement.magnitude;
      _controller.Move(_currentMovement * Time.deltaTime);

      _animator.SetFloat(_velocity, _movementMagnitude);
    }

    private void Rotate(Vector3 direction) {
      if (_targetTransform != null || direction == Vector3.zero) return;
      RotatePlayer(direction);
    }

    private void RotatePlayer(Vector3 direction) {
      if (direction != Vector3.zero) {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                                             Time.deltaTime * _rotationSpeed);
      }
    }
  }
}