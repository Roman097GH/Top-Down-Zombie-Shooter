using UnityEngine;

namespace TopDown {
  [CreateAssetMenu(menuName = "Scriptable/Player info", fileName = "PlayerInfo")]
  public class PlayerInfo : ScriptableObject {
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public GameObject ViewPrefab { get; private set; }
    
    [field: SerializeField] public Animator Animator { get; private set; }
  }
}
