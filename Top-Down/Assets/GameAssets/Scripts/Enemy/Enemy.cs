using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown {
  public class Enemy : EnemyBase {
    [SerializeField, HideInInspector] private NavMeshAgent _meshAgent;
    [SerializeField, HideInInspector] private EnemyStateCheck _enemyStateCheck;
    [SerializeField, HideInInspector] private EnemyMakeDamageCheck _enemyMakeDamageCheck;
    [SerializeField, HideInInspector] private Damageable _damageable;
    [SerializeField, HideInInspector] private Animator _animator;

    private PlayerController _playerController;

    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackPeriod = 2.5f;

    private EnemyState _enemyState = EnemyState.EDefault;

    private static readonly int _followAnimTrig = Animator.StringToHash("FollowTrig");
    private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");
    private static readonly int _deathAnimTrig = Animator.StringToHash("DeathTrig");

    private float _distance;

    private bool _isFollow;

    private void OnValidate() {
      _meshAgent = GetComponent<NavMeshAgent>();
      _enemyStateCheck = GetComponentInChildren<EnemyStateCheck>();
      _enemyMakeDamageCheck = GetComponentInChildren<EnemyMakeDamageCheck>();
      _damageable = GetComponent<Damageable>();
    }

    public override void Initialize(SOEnemy enemyInfo, int enemyLevel) {
      EnemyLevelInfo info = enemyInfo.EnemyLevelInfos[enemyLevel];

      _enemyName = enemyInfo.name;
      _attackRadius = info.AttackRadius;
      _moveSpeed = info.MoveSpeed;
      _meshAgent.speed = _moveSpeed;
      _health = info.Health;
      _damage = info.Damage;

      _enemyStateCheck.PlayerFound.TakeUntilDestroy(this).Subscribe(OnPlayerChangeState);
      _enemyMakeDamageCheck.PlayerFindForAttack.TakeUntilDestroy(this).Subscribe(OnPlayerChangeAttack);

      _damageable.SetHealth(_health);
    }

    private void OnPlayerChangeState(PlayerController playerController) {
      _playerController = playerController;
      _enemyState = EnemyState.EFollow;
    }

    private void OnPlayerChangeAttack(PlayerController playerController) {
      _playerController = playerController;
      _enemyState = EnemyState.EAttack;
    }

    private void Update() {
      _attackTimer += Time.unscaledDeltaTime;

      if (_damageable.Health.Value == 0) {
        _enemyState = EnemyState.EDeath;
      }
      
      switch (_enemyState) {
        case EnemyState.EDefault:
          Default();
          break;

        case EnemyState.EFollow:
          Follow();
          break;

        case EnemyState.EAttack:
          Follow();
          Attack();
          break;

        case EnemyState.EDeath:
          Death();
          break;


        default: throw new ArgumentOutOfRangeException();
      }
    }

    protected override void Default() { }

    protected override void Follow() {
      Vector3 playerPosition = _playerController.transform.position;
      _meshAgent.SetDestination(playerPosition);
      _animator.SetTrigger(_followAnimTrig);
      _distance = Vector3.Distance(transform.position, playerPosition);
      _enemyState = _distance <= _attackRadius ? EnemyState.EAttack : EnemyState.EFollow;
    }

    protected override void Attack() {
      if (!(_attackTimer > _attackPeriod)) return;
      _attackTimer = 0;
      _animator.SetTrigger(_attackAnimTrig);
      _playerController.GetComponent<Damageable>().TakeDamage(_damage);
    }

    protected override void Death() {
      _animator.SetTrigger(_deathAnimTrig);
      Destroy(gameObject, 1.5f);
      
    }

    public Vector3 GetPosition() => transform.position;
  }
}