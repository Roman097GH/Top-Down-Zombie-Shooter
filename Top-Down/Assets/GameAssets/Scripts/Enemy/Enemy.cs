using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown {
  public class Enemy : EnemyBase {
    //[SerializeField, HideInInspector] private int _playerLayer;
    //[SerializeField] private SphereCollider _colliderRadius;
    //private const string PLAYER = "Player";
    private Player _player;
    private float _distance;

    private EnemyState _enemyState = EnemyState.EEating;

    [SerializeField] private Animator _animator;

    private static readonly int _eatingAnim = Animator.StringToHash("EatingBool");

    //private static readonly int _lookAtAnimTrig = Animator.StringToHash("LookAtTrig");
    private static readonly int _followAnimTrig = Animator.StringToHash("FollowTrig");

    private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");
    //private static readonly int _idleAnimTrig = Animator.StringToHash("IdleTrig");

    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackPeriod = 2.5f;

    [SerializeField] private NavMeshAgent _meshAgent;

    //[SerializeField] private float _rotationSpeed = 1.0f;

    [SerializeField] private EnemyHitCheck _enemyHitCheck;
    [SerializeField] private EnemyStateCheck _enemyStateCheck;

    public override void Initialize(SOEnemy enemyInfo, int enemyLevel) {
      //_playerLayer = LayerMask.NameToLayer(PLAYER);

      EnemyLevelInfo info = enemyInfo.EnemyLevelInfos[enemyLevel];

      _enemyHitCheck.Hit.Subscribe(_ => TakingDamage());
      _enemyStateCheck.IsTrigger.Subscribe(_ => Follow());

      _enemyName = enemyInfo.name;

      
      //_colliderRadius.radius = _followRadius;
      //_followRadius = info.FollowRadius;

      //_attackRadius = info.AttackRadius;

      _moveSpeed = info.MoveSpeed;
      _meshAgent.speed = _moveSpeed;

      _health = info.Health;
      //_damage = info.Damage;
    }

    private void Awake() {
      _player = _enemyStateCheck.Player;
    }

    private void Update() {
      _attackTimer += Time.unscaledDeltaTime;

      switch (_enemyState) {
        case EnemyState.EEating:
          Eating();
          break;

        case EnemyState.EFollow:
          Follow();
          break;

        case EnemyState.EAttack:
          Follow();
          Attack();
          break;

        default: throw new ArgumentOutOfRangeException();
      }
    }

    protected override void Eating() {
      //_animator.SetBool(_eatingAnim, true);
    }

    // protected override void LookAt() {
    //   _animator.SetBool(_eatingAnimTrig, false);
    //   _animator.SetTrigger(_lookAtAnimTrig);
    //
    //   _meshAgent.speed = 0.0f;
    //
    //   Quaternion lookRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
    //   transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    // }

    protected override void Follow() {
      //_animator.SetBool(_eatingAnim, false);
      _animator.SetTrigger(_followAnimTrig);

      _meshAgent.speed = _moveSpeed;

      _meshAgent.SetDestination(_player.transform.position);
    }

    protected override void Attack() {
      // if (!(_attackTimer > _attackPeriod)) return;
      // _attackTimer = 0;
      // _animator.SetTrigger(_attackAnimTrig);
    }

    // protected override void Idle() {
    //   _animator.SetTrigger(_idleAnimTrig);
    // }

    protected override void TakingDamage() {
      _health -= 1;
      Debug.Log(_health);
    }

    // private void OnTriggerEnter(Collider other) {
    //   if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;
    //
    //   _player = player;
    //   _enemyState = EnemyState.EFollow;
    // }
    //
    // private void OnTriggerStay(Collider other) {
    //   if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;
    //
    //   _distance = Vector3.Distance(transform.position, player.transform.position);
    //
    //   if (_distance <= _attackRadius) _enemyState = EnemyState.EAttack;
    //   //else if (_distance <= _followRadius) _enemyState = EnemyState.EFollow;
    //   else _enemyState = EnemyState.EFollow;
    // }
    //
    // private void OnTriggerExit(Collider other) {
    //   if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;
    //
    //   _distance = 0;
    //   _player = null;
    //   _enemyState = EnemyState.EFollow;
    // }
  }
}