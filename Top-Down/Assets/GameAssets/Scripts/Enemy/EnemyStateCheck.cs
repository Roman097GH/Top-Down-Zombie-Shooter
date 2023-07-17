using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace TopDown {
  public class EnemyStateCheck : MonoBehaviour {
    public readonly ReactiveCommand<PlayerController> Player = new();
    
    private int _playerLayer;
    private const string PLAYER = "Player";


    private void Awake() {
      _playerLayer = LayerMask.NameToLayer(PLAYER);
    }

    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
      Player.Execute(player);
      //Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other) {
      if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out PlayerController player)) return;
      Player.Execute(null);
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

    //private void OnTriggerExit(Collider other) {
    //if (other.gameObject.layer != _playerLayer || !other.TryGetComponent(out Player player)) return;

    //_distance = 0;
    //_player = null;
    //_enemyState = EnemyState.EFollow;
    //}
  }
}