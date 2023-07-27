using UniRx;
using UnityEngine;

namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, HideInInspector] private CharacterController _controller;
        [SerializeField] private ShootBase _shootBase;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Animator _animator;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _initialHealth;
        [SerializeField] private float _damage;
        [SerializeField] private float _decelerationRate = 3.0f; // Добавить в ScriptableObject
        [SerializeField] private int _initialNumberOfBullets;
        [SerializeField] private int _countShotPerMinute;

        private PlayerInputService _playerInputService;
        private EnemyProvider _enemyProvider;
        private HealthItem _healthItem;
        private BulletItem _bulletItem;
        private Transform _targetTransform;

        private static readonly int _velocity = Animator.StringToHash("Velocity");
        private static readonly int _shotAnimTrig = Animator.StringToHash("ShotTrig");
        private static readonly int _deathAnimTrig = Animator.StringToHash("DeathTrig");

        private Vector3 _currentMovement = Vector3.zero;

        private int _currentBullets;

        private float _movementMagnitude;
        private float _nextTimeToFire;
        private float _currentHealth;

        private bool _fire;

        public ReactiveProperty<int> BulletsCount = new();
        public ReactiveProperty<float> Damage = new();

        public void Initialize(PlayerInputService playerInputService, EnemyProvider enemyProvider,
            float playerInfoMoveSpeed, float playerInfoRotationSpeed, float playerInfoHealth,
            int playerInfoNumberOfBullets, int playerInfoCountShotPerMinute, float playerInfoDamage, HealthItem healthItem,
            BulletItem bulletItem)
        {
            _playerInputService = playerInputService;
            _enemyProvider = enemyProvider;
            _moveSpeed = playerInfoMoveSpeed;
            _rotationSpeed = playerInfoRotationSpeed;
            _initialHealth = playerInfoHealth;
            _currentHealth = _initialHealth;
            _countShotPerMinute = playerInfoCountShotPerMinute;
            _initialNumberOfBullets = playerInfoNumberOfBullets;
            _currentBullets = _initialNumberOfBullets;
            BulletsCount.Value = _currentBullets;
            _currentBullets = Mathf.Clamp(_currentBullets, 0, _initialNumberOfBullets);
            _healthItem = healthItem;
            _bulletItem = bulletItem;
            _animator = GetComponentInChildren<Animator>();

            Damage.Value = playerInfoDamage;

            _playerInputService.OnJoystickMove.TakeUntilDestroy(this).Subscribe(HandleMovement);
            _playerInputService.OnAttack.TakeUntilDestroy(this).Subscribe(SetFire);

            _healthItem.PlayerFoundAddHealth.TakeUntilDestroy(this).Subscribe(_ => AddHealth());
            _bulletItem.PlayerFoundAddBullets.TakeUntilDestroy(this).Subscribe(_ => AddBullets());

            _damageable.SetHealth(_currentHealth);
        }

        private void OnValidate() => _controller = GetComponent<CharacterController>();

        private void Update()
        {
            FireDelayProcessing();
            GetPlayerPosition();
            Death();

            Transform transformClosestEnemy = _enemyProvider.GetEnemyClosestTo(GetPlayerPosition());
            SetFollowTarget(transformClosestEnemy);

            if (_targetTransform != null)
            {
                RotatePlayer(_targetTransform.position - transform.position);
            }

            if (!(_movementMagnitude > 0.0f)) return;
            _movementMagnitude -= Time.deltaTime * _decelerationRate;
            _movementMagnitude = Mathf.Clamp(_movementMagnitude, 0.0f, 1.0f);
            _animator.SetFloat(_velocity, _movementMagnitude);
        }

        private void HandleMovement(Vector3 movement)
        {
            Move(movement);
            Rotate(movement);
        }

        private void Move(Vector3 movement)
        {
            _currentMovement = movement * _moveSpeed;
            _movementMagnitude = movement.magnitude;
            _controller.Move(_currentMovement * Time.deltaTime);

            _animator.SetFloat(_velocity, _movementMagnitude);
        }

        private void Rotate(Vector3 direction)
        {
            if (_targetTransform != null || direction == Vector3.zero) return;
            RotatePlayer(direction);
        }

        private void RotatePlayer(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                    Time.deltaTime * _rotationSpeed);
            }
        }

        private void AddHealth()
        {
            _currentHealth = _initialHealth;
            _damageable.SetHealth(_currentHealth);
        }

        private void AddBullets() => _currentBullets = _initialNumberOfBullets;

        private void SetFire(bool fire) => _fire = fire;

        private void SetFollowTarget(Transform targetTransform) => _targetTransform = targetTransform;

        private Vector3 GetPlayerPosition() => transform.position;

        private void FireDelayProcessing()
        {
            if (!_fire || _nextTimeToFire > Time.time || _currentBullets == 0) return;
            _nextTimeToFire = Time.time + 60f / _countShotPerMinute;
            _currentBullets -= 1;
            BulletsCount.Value -= 1;

            Shooting();
        }

        private void Shooting()
        {
            _shootBase.Shot();
            _animator.SetTrigger(_shotAnimTrig);
        }

        private void Death()
        {
            if (_damageable.Health.Value == 0)
            {
                _animator.SetTrigger(_deathAnimTrig);
            }
        }
    }
}