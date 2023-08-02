using UniRx;
using UnityEngine;

namespace TopDown
{
    public class EnemyMakeDamageCheck : MonoBehaviour
    {
        private const string PLAYER = "Player";
        private int _playerLayer;
        public readonly ReactiveCommand<PlayerController> PlayerFoundForAttack = new();

        private void Awake() => _playerLayer = LayerMask.NameToLayer(PLAYER);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
            PlayerFoundForAttack.Execute(player);
        }
    }
}