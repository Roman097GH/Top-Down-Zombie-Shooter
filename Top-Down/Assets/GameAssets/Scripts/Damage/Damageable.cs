using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown {
  public class Damageable : MonoBehaviour, IDamageable {
    [SerializeField] private Slider _slider;
    
    public readonly ReactiveProperty<float> Health = new();

    public void SetHealth(float initialHealth) => Health.Value = initialHealth;

    public void TakeDamage(float damage) {
     Health.Value = Mathf.Clamp(Health.Value - damage, 0, Health.Value);

     _slider.value -= damage;
     Debug.Log(Health.Value);
    }
  }
}