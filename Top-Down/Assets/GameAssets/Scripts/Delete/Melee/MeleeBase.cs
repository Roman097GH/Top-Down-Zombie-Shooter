using System;
using UnityEngine;
using System.Threading.Tasks;

namespace TopDown {
  public abstract class MeleeBase : MonoBehaviour {
    public abstract void Hit();
    // [SerializeField] private int _damage = 10;
    //
    // private bool _canDamage;
    //
    // public async void Attack(bool canDamage) {
    //   _canDamage = canDamage;
    //   if (!_canDamage) return;
    //   await Task.Delay(TimeSpan.FromSeconds(1.0f));
    //   _canDamage = false;
    // }
    //
    // private void OnTriggerEnter(Collider other) {
    //   if (!_canDamage || !other.TryGetComponent(out DestructibleBase destructible)) return;
    //   destructible.TakeDamage(_damage);
    //   Attack(false);
    // }
  }
}