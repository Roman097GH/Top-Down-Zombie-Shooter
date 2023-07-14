using UniRx;
using UnityEngine;

namespace TopDown {
  public class EnemyHitCheck : MonoBehaviour {
    private int _macheteLayer;
    private const string MACHETE = "Machete";

    public readonly ReactiveCommand Hit = new();

    private void Awake() => _macheteLayer = LayerMask.NameToLayer(MACHETE);

    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.layer != _macheteLayer) return;
      Debug.Log(other.gameObject.name);
      Hit?.Execute();
    }
  }
}