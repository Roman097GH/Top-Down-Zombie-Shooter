using UnityEngine;

namespace TopDown
{
    public class HealthItem : MonoBehaviour
    {
        private PlayerController _playerController;

        private const string PLAYER = "Player";

        private int _playerLayer;

        private void Awake() => _playerLayer = LayerMask.NameToLayer(PLAYER);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
            _playerController = player;
            _playerController.Health.Value = _playerController.InitialHealth.Value;
            Destroy(gameObject);
        }
    }
}