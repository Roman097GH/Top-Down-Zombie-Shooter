using UniRx;
using UnityEngine;

namespace TopDown
{
    public class EnemyMakeDamageCheck : MonoBehaviour
    {
        private const string PLAYER = "Player";
        private int _playerLayer;
        public readonly ReactiveCommand<PlayerController> PlayerFindForAttack = new();

        private void Awake() => _playerLayer = LayerMask.NameToLayer(PLAYER);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
            PlayerFindForAttack.Execute(player);
            Debug.Log(other.gameObject.name + "Attack");
        }
    }
}