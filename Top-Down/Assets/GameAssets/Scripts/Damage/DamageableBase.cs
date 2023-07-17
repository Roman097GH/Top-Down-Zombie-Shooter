using UniRx;
using UnityEngine;

namespace TopDown {
  public class DamageableBase : MonoBehaviour, IDamageable {
    protected readonly ReactiveProperty<float> Health = new();

    public void SetHealth(float initialHealth) {
      Health.Value = initialHealth;
    }

    public void TakeDamage(float damage) {
      Debug.Log(Health.Value = Mathf.Clamp(Health.Value - damage, 0, Health.Value));
    }
  }
}