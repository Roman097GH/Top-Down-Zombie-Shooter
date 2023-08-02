using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TopDown
{
    public class Enemy : EnemyBase
    {
        [SerializeField, HideInInspector] private EnemyMakeDamageCheck _enemyMakeDamageCheck;
        [SerializeField, HideInInspector] private EnemyStateCheck _enemyStateCheck;
        [SerializeField, HideInInspector] private NavMeshAgent _meshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _healthBarSlider;

        private PlayerController _playerController;

        [SerializeField] private float _attackTimer;
        [SerializeField] private float _attackPeriod = 2.5f;
        private float _distance;
        private Vector3 _playerPosition;

        private EnemyState _enemyState = EnemyState.Default;

        private static readonly int _followAnimTrig = Animator.StringToHash("FollowTrig");
        private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");
        private static readonly int _deathAnimTrig = Animator.StringToHash("DeathTrig");

        private void OnValidate()
        {
            _enemyMakeDamageCheck = GetComponentInChildren<EnemyMakeDamageCheck>();
            _enemyStateCheck = GetComponentInChildren<EnemyStateCheck>();
            _meshAgent = GetComponent<NavMeshAgent>();
        }

        public override void Initialize(SOEnemy enemyInfo, int enemyLevel)
        {
            EnemyLevelInfo info = enemyInfo.EnemyLevelInfos[enemyLevel];

            _enemyName = enemyInfo.name;
            _moveSpeed = info.MoveSpeed;
            _meshAgent.speed = _moveSpeed;
            _health = info.Health;
            _damage = info.Damage;
            _attackRadius = _meshAgent.stoppingDistance;

            SetHealth(_health);

            _enemyStateCheck.PlayerFoundForState.TakeUntilDestroy(this).Subscribe(OnPlayerChangeState);
            _enemyMakeDamageCheck.PlayerFoundForAttack.TakeUntilDestroy(this).Subscribe(OnPlayerChangeAttack);
            
            Health.TakeUntilDestroy(this).Subscribe(OnHealthChange);
            _healthBarSlider.maxValue = Health.Value;
            _healthBarSlider.value = _healthBarSlider.maxValue;
        }

        private void OnHealthChange(float health)
        {
            _healthBarSlider.value = health;
        }

        private void OnPlayerChangeState(PlayerController playerController)
        {
            _playerController = playerController;
            _enemyState = EnemyState.Follow;
        }

        private void OnPlayerChangeAttack(PlayerController playerController)
        {
            _playerController = playerController;
            _enemyState = EnemyState.Attack;
        }

        private void Update()
        {
            _attackTimer += Time.unscaledDeltaTime;
            
            switch (_enemyState)
            {
                case EnemyState.Default:
                    Default();
                    break;

                case EnemyState.Follow:
                    Follow();
                    break;

                case EnemyState.Attack:
                    Follow();
                    Attack();
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Default()
        {
        }

        protected override void Follow()
        {
            _playerPosition = _playerController.transform.position;
            _meshAgent.SetDestination(_playerPosition);
            _animator.SetTrigger(_followAnimTrig);
        }

        protected override void Attack()
        {
            _playerPosition = _playerController.transform.position;
            _distance = Vector3.Distance(transform.position, _playerPosition);

            if (!(_attackTimer > _attackPeriod)) return;
            if (!(_distance <= _attackRadius)) return;
            _attackTimer = 0;
            _animator.SetTrigger(_attackAnimTrig);
            _playerController.GetComponent<Damageable>().TakeDamage(_damage);
        }

        protected override void PerformDeath()
        {
            _animator.SetTrigger(_deathAnimTrig);
            Destroy(gameObject, 1.5f);
        }

        public override Vector3 GetPosition() => transform.position;
    }
}