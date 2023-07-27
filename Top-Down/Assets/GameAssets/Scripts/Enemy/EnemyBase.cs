using UnityEngine;

namespace TopDown {
  public abstract class EnemyBase : Damageable {
    [SerializeField, HideInInspector] protected string _enemyName;
    [SerializeField, HideInInspector] protected float _attackRadius;
    [SerializeField, HideInInspector] protected float _moveSpeed;
    [SerializeField, HideInInspector] protected int _health;
    [SerializeField, HideInInspector] protected int _damage;

    public abstract void Initialize(SOEnemy enemyInfo, int enemyLevel);

    protected abstract void Default();
    protected abstract void Follow();
    protected abstract void Attack();
    protected abstract void Death();

    public abstract Vector3 GetPosition();
  }
}