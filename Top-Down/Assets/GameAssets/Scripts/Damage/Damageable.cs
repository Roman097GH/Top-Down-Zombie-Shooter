using UniRx;
using UnityEngine;

namespace TopDown
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public readonly ReactiveProperty<float> Health = new();

        public void SetHealth(float initialHealth)
        {
            Health.Value = initialHealth;
        }

        public void TakeDamage(float damage)
        {
            Health.Value = Mathf.Clamp(Health.Value - damage, 0, Health.Value);

            if (Health.Value <= 0)
            {
                PerformDeath();
            }
        }

        protected virtual void PerformDeath()
        {
        }
    }
}