using UniRx;
using UnityEngine;

namespace TopDown {
  public abstract class DestructibleBase : MonoBehaviour {
    private readonly ReactiveProperty<float> _health = new();

    public void SetHealth(float initialHealth) => _health.Value = initialHealth;

    public void TakeDamage(float damage) {
      _health.Value = Mathf.Clamp(_health.Value - damage, 0, _health.Value);

      if (_health.Value == 0) {
        Debug.Log("Death");
      }
    }
  }
}