using UnityEngine;

namespace TopDown {
  public class Machete : MeleeBase {
    [SerializeField] private float _damage = 5f;

    public override void Hit() {
      Debug.Log("MeleeAttack");
    }

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out IDamageable damageable)) {
        Debug.Log(other.gameObject.name);
        damageable.TakeDamage(_damage);
      }
      
      // if (other.TryGetComponent(out IDamageable damageable)) return;
      
    }
  }
}