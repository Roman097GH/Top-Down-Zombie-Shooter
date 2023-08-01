using UniRx;
using UnityEngine;
using UnityEngine.UI;

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

            if (Health.Value <=0)
            {
                PerformDeath();
            }
            
            Debug.Log(gameObject.name + " Health: " + Health.Value);
        }

        protected virtual void PerformDeath()
        {
            
        }
        
       
    }
}