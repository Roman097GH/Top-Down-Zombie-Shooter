using System;
using UnityEngine;

namespace TopDown {
  public class Enemy : EnemyBase {
    [SerializeField, HideInInspector] private int _playerLayer;
    [SerializeField] private SphereCollider _colliderRadius;
    private const string PLAYER = "Player";
    private Player _player;
    private float _distance;

    private EEnemyState _enemyState = EEnemyState.EAting;

    [SerializeField] private Animator _animator;
    private static readonly int _eatingAnimTrig = Animator.StringToHash("EatingBool");
    private static readonly int _lookAtAnimTrig = Animator.StringToHash("LookAtTrig");
    private static readonly int _followAnimTrig = Animator.StringToHash("FollowTrig");
    private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");
    private static readonly int _idleAnimTrig = Animator.StringToHash("IdleTrig");

    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackPeriod = 2.5f;

    public override void Initialize(SOEnemy enemyInfo, int enemyLevel) {
      _playerLayer = LayerMask.NameToLayer(PLAYER);

      EnemyLevelInfo info = enemyInfo.EnemyLevelInfos[enemyLevel];

      _enemyName = enemyInfo.name;
      _lookAtRadius = info.LookRadius;
      _colliderRadius.radius = _lookAtRadius;
      _followRadius = info.FollowRadius;
      _attackRadius = info.AttackRadius;

      _moveSpeed = info.MoveSpeed;
      _health = info.Health;
      _damage = info.Damage;
    }

    private void Update() {
      _attackTimer += Time.unscaledDeltaTime;

      switch (_enemyState) {
        case EEnemyState.EAting:
          Eating();
          break;

        case EEnemyState.ELookAt:
          LookAt();
          break;

        case EEnemyState.EFollow:
          Follow();
          break;

        case EEnemyState.EAttack:
          Follow();
          Attack();
          break;

        case EEnemyState.EIdle:
          Idle();
          break;

        default: throw new ArgumentOutOfRangeException();
      }
    }

    protected override void Eating() {
      _animator.SetBool(_eatingAnimTrig, true);
    }

    protected override void LookAt() {
      _animator.SetBool(_eatingAnimTrig, false);
      _animator.SetTrigger(_lookAtAnimTrig);
      transform.LookAt(_player.transform.position);
    }

    protected override void Follow() {
      _animator.SetTrigger(_followAnimTrig);

      var step = _moveSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);

      transform.LookAt(_player.transform.position);
    }

    protected override void Attack() {
      if (!(_attackTimer > _attackPeriod)) return;
      _attackTimer = 0;
      _animator.SetTrigger(_attackAnimTrig);
    }

    protected override void Idle() {
      _animator.SetTrigger(_idleAnimTrig);
    }

    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;

      _player = player;
      _enemyState = EEnemyState.ELookAt;
    }

    private void OnTriggerStay(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;

      _distance = Vector3.Distance(transform.position, player.transform.position);

      if (_distance <= _attackRadius) _enemyState = EEnemyState.EAttack;
      else if (_distance <= _followRadius) _enemyState = EEnemyState.EFollow;
      else _enemyState = EEnemyState.ELookAt;
    }

    private void OnTriggerExit(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;

      _distance = 0;
      _player = null;
      _enemyState = EEnemyState.EIdle;
    }
  }
}