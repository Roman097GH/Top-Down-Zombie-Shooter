using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown {
  [RequireComponent(typeof(CharacterController))]
  public class PlayerController : MonoBehaviour {
    private InputHandler _inputHandler;

    [SerializeField, HideInInspector] private CharacterController _controller;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _movement = Vector3.zero;
    private Vector3 _currentMovement = Vector3.zero;

    [SerializeField] private Animator _animator;
    private static readonly int _velocityAnimator = Animator.StringToHash("Velocity");
    private static readonly int _shootAnimTrig = Animator.StringToHash("ShootTrig");
    private float _movementMagnitude;

    [SerializeField] private float _decelerationRate = 1.5f;

    [SerializeField] private float _shootTimer;
    [SerializeField] private float _shootPeriod;

    [SerializeField] private ShootBase _shootBase;
    
    [SerializeField] private float _health;

    private void OnValidate() {
      _controller = GetComponent<CharacterController>();
    }

    [Inject]
    private void Construct(InputHandler inputHandler) {
      _inputHandler = inputHandler;
      _inputHandler.OnMove.Subscribe(Move);
      _inputHandler.OnRotate.Subscribe(Rotate);
    }

    private void Update() {
      _shootTimer += Time.unscaledDeltaTime;

      if (_shootTimer > _shootPeriod) {
        if (Input.GetKeyDown(KeyCode.F)) {
          _shootTimer = 0;
          Shooting();
        }
      }

      if (!(_movementMagnitude > 0.0f)) return;
      _movementMagnitude -= Time.deltaTime * _decelerationRate;
      _movementMagnitude = Mathf.Clamp(_movementMagnitude, 0.0f, 1.0f);

      _animator.SetFloat(_velocityAnimator, _movementMagnitude);
    }

    private void Shooting() {
      _shootBase.Shot();
      _animator.SetTrigger(_shootAnimTrig);
    }

    private void Move(Vector3 movement) {
      _movement = new Vector3(movement.x, 0.0f, movement.y);
      _currentMovement = _movement * _moveSpeed;
      _movementMagnitude = _movement.magnitude;

      _controller.Move(_currentMovement * Time.deltaTime);

      _animator.SetFloat(_velocityAnimator, _movementMagnitude);
    }

    private void Rotate(Vector3 direction) {
      if (_currentMovement != Vector3.zero) {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_currentMovement),
                                             Time.deltaTime * _rotationSpeed);
      }
    }

    public void Initialize(PlayerType type, float playerInfoSpeed, float playerInfoRotationSpeed, float playerInfoHealth) {
      _moveSpeed = playerInfoSpeed;
      _rotationSpeed = playerInfoRotationSpeed;
      _health = playerInfoHealth;
      _animator = GetComponentInChildren<Animator>();
    }
  }
}