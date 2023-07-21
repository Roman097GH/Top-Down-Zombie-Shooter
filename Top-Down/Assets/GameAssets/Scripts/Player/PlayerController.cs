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

    [SerializeField] private float _fireDelay;
    [SerializeField] private float _delayTemp;

    [SerializeField] private float _decelerationRate = 1.5f;
    private float _movementMagnitude;

    private EnemyProvider _enemyProvider;
    private InputHandler _inputHandler;
    private Transform _targetTransform;

    private Vector3 _movement = Vector3.zero;
    private Vector3 _currentMovement = Vector3.zero;

    private static readonly int _velocity = Animator.StringToHash("Velocity");
    private static readonly int _shotAnimTrig = Animator.StringToHash("ShotTrig");
    private static readonly int _deathAnimTrig = Animator.StringToHash("DeathTrig");

    public void Initialize(InputHandler inputHandler, PlayerType type, EnemyProvider enemyProvider,
                           float playerInfoMoveSpeed, float playerInfoRotationSpeed, float playerInfoHealth,
                           float playerInfoFireDelay, float playerInfoNumberOfRounds) {
      _inputHandler = inputHandler;
      _enemyProvider = enemyProvider;
      _moveSpeed = playerInfoMoveSpeed;
      _rotationSpeed = playerInfoRotationSpeed;
      _health = playerInfoHealth;
      _fireDelay = playerInfoFireDelay;
      _numberOfRounds = playerInfoNumberOfRounds;

      _animator = GetComponentInChildren<Animator>();


      _inputHandler.OnJoystickMove.Subscribe(Move);
      _inputHandler.OnJoystickMove.Subscribe(Rotate);

      _damageable.SetHealth(_health);
      Debug.Log(gameObject.name + " Health: " + _health);
    }

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
      _delayTemp += Time.unscaledDeltaTime;
      if (_delayTemp > _fireDelay) {
        if (Input.GetKeyDown(KeyCode.F)) {
          _delayTemp = 0;
          Shooting();
        }
      }
    }

    private void SetFollowTarget(Transform targetTransform) => _targetTransform = targetTransform;

    private Vector3 GetPlayerPosition() => transform.position;

    private void Shooting() {
      _shootBase.Shot();
      _animator.SetTrigger(_shotAnimTrig);
    }

    private void Move(Vector3 movement) {
      _movement = new Vector3(movement.x, 0.0f, movement.y);
      _currentMovement = _movement * _moveSpeed;
      _movementMagnitude = _movement.magnitude;
      _controller.Move(_currentMovement * Time.deltaTime);

      _animator.SetFloat(_velocity, _movementMagnitude);
    }

    private void Rotate(Vector3 direction) {
      if (_targetTransform != null || direction == Vector3.zero) return;
      Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
      RotatePlayer(movement);
    }

    private void RotatePlayer(Vector3 direction) {
      if (direction != Vector3.zero) {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                                             Time.deltaTime * _rotationSpeed);
      }
    }
  }
}