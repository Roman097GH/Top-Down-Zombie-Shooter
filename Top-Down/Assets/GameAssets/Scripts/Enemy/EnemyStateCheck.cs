using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace TopDown {
  public class EnemyStateCheck : MonoBehaviour {
    //[SerializeField] private SphereCollider _colliderRadius;
    public Player Player;
    private int _playerLayer;
    private const string PLAYER = "Player";

    public readonly ReactiveCommand IsTrigger = new();

    //private Player _player;

    private Vector3 currentPosition;

    private void Awake() {
      _playerLayer = LayerMask.NameToLayer(PLAYER);
    }

    private void OnTriggerStay(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;
      IsTrigger?.Execute();
      Player = player;


      //_player = player;
      //_enemyState = EnemyState.EFollow;
    }

    private void Update() {
      PlayerPosition(Player.transform.position);
    }

    public Vector3 PlayerPosition(Vector3 position) {
      Vector3 currentPosition = Player.transform.position;
      return currentPosition;
    }

    // private void OnTriggerStay(Collider other) {
    //   if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;
    //
    //   //_distance = Vector3.Distance(transform.position, player.transform.position);
    //
    //   //if (_distance <= _attackRadius) _enemyState = EnemyState.EAttack;
    //   //else if (_distance <= _followRadius) _enemyState = EnemyState.EFollow;
    //   //else _enemyState = EnemyState.EFollow;
    // }

    private void OnTriggerExit(Collider other) {
      //if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;

      //_distance = 0;
      //_player = null;
      //_enemyState = EnemyState.EFollow;
    }
  }
}