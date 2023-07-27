using UniRx;
using UnityEngine;

namespace TopDown
{
    public class EnemyStateCheck : MonoBehaviour
    {
        private const string PLAYER = "Player";
        private int _playerLayer;
        public readonly ReactiveCommand<PlayerController> PlayerFindForState = new();

        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer(PLAYER);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
            PlayerFindForState.Execute(player);
            Debug.Log(other.gameObject.name + "Follow");
        }
    }
}