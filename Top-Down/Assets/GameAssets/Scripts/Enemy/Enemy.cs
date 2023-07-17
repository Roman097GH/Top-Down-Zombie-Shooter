using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TopDown {
  public class Enemy : EnemyBase {
    private float _distance;

    private PlayerController _playerController;
    
    private EnemyState _enemyState = EnemyState.EEating;

    [SerializeField] private Animator _animator;
    private static readonly int _eatingAnim = Animator.StringToHash("EatingBool");
    private static readonly int _followAnimTrig = Animator.StringToHash("FollowTrig");
    private static readonly int _attackAnimTrig = Animator.StringToHash("AttackTrig");

    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackPeriod = 2.5f;

    [SerializeField, HideInInspector] private NavMeshAgent _meshAgent;

    [SerializeField, HideInInspector] private EnemyStateCheck _enemyStateCheck;

    private void OnValidate() {
      _meshAgent = GetComponent<NavMeshAgent>();
      _enemyStateCheck = GetComponentInChildren<EnemyStateCheck>();
    }

    public override void Initialize(SOEnemy enemyInfo, int enemyLevel) {
      EnemyLevelInfo info = enemyInfo.EnemyLevelInfos[enemyLevel];

      _enemyName = enemyInfo.name;

      //_colliderRadius.radius = _followRadius;
      //_followRadius = info.FollowRadius;

      //_attackRadius = info.AttackRadius;

      _moveSpeed = info.MoveSpeed;
      _meshAgent.speed = _moveSpeed;

      _health = info.Health;
      
      SetHealth(_health);
      
      _damage = info.Damage;

      _enemyStateCheck.Player.TakeUntilDestroy(this).Subscribe(OnPlayerChange);
    }

    private void OnPlayerChange(PlayerController playerController) {
      Debug.Log(playerController.name);
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

    protected override void Follow() {
      //_animator.SetBool(_eatingAnim, false);
      _animator.SetTrigger(_followAnimTrig);

      _meshAgent.speed = _moveSpeed;

      //_meshAgent.SetDestination(_player.transform.position);
    }

    protected override void Attack() {
      // if (!(_attackTimer > _attackPeriod)) return;
      // _attackTimer = 0;
      // _animator.SetTrigger(_attackAnimTrig);
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