using UniRx;
using UnityEngine;

namespace TopDown
{
    public class HealthItem : MonoBehaviour
    {
        private const string PLAYER = "Player";

        private int _playerLayer;

        public readonly ReactiveCommand PlayerFoundAddHealth = new();

        private void Awake() => _playerLayer = LayerMask.NameToLayer(PLAYER);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
            PlayerFoundAddHealth.Execute();
            Destroy(gameObject);
            Debug.Log(other.gameObject.name + "Health is collected!");
        }
    }
}