using UnityEngine;

namespace TopDown {
  [CreateAssetMenu(menuName = "Scriptable/Player info", fileName = "PlayerInfo")]
  public class PlayerInfo : ScriptableObject {
    [field: SerializeField] public GameObject ViewPrefab { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float FireDelay { get; private set; }
    [field: SerializeField] public float NumberOfRounds { get; private set; }
    
    //[field: SerializeField] public float Damage { get; private set; }
  }
}