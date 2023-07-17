using UnityEngine;

namespace TopDown {
  public abstract class EnemyBase : DamageableBase {
    [SerializeField, HideInInspector] protected string _enemyName;
    [SerializeField, HideInInspector] protected float _followRadius;
    [SerializeField, HideInInspector] protected float _attackRadius;
    [SerializeField, HideInInspector] protected float _moveSpeed;
    [SerializeField, HideInInspector] protected int _health;
    [SerializeField, HideInInspector] protected int _damage;

    public abstract void Initialize(SOEnemy enemyInfo, int enemyLevel);

    protected abstract void Eating();
    protected abstract void Follow();
    protected abstract void Attack();
  }
}