using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private Slider _healthBarSlider;

        public readonly ReactiveProperty<float> Health = new();

        public void SetHealth(float initialHealth)
        {
            Health.Value = initialHealth;
            _healthBarSlider.maxValue = Health.Value;
            _healthBarSlider.value = _healthBarSlider.maxValue;
        }

        public void TakeDamage(float damage)
        {
            Health.Value = Mathf.Clamp(Health.Value - damage, 0, Health.Value);
            SetHealthBar(damage);
            Debug.Log(gameObject.name + " Health: " + Health.Value);
        }

        private void SetHealthBar(float damage) => _healthBarSlider.value -= damage;
    }
}