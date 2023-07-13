using UnityEngine;

namespace TopDown {
  public abstract class EnemyBase : MonoBehaviour {
    [SerializeField, HideInInspector] protected string _enemyName;
    [SerializeField, HideInInspector] protected float _lookAtRadius;
    [SerializeField, HideInInspector] protected float _followRadius;
    [SerializeField, HideInInspector] protected float _attackRadius;
    
    [SerializeField, HideInInspector] protected float _moveSpeed;
    [SerializeField, HideInInspector] protected float _health;
    [SerializeField, HideInInspector] protected float _damage;

    public abstract void Initialize(SOEnemy enemyInfo, int enemyLevel);

    protected abstract void Eating();
    protected abstract void LookAt();
    protected abstract void Follow();
    protected abstract void Attack();
    protected abstract void Idle();
  }
}