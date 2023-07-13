using System.Collections.Generic;
using UnityEngine;

namespace TopDown {
  [CreateAssetMenu(menuName = "Scriptable/Enemy", fileName = "New Enemy")]
  public class SOEnemy : ScriptableObject {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public EnemyBase Enemy { get; private set; }
    [field: SerializeField] public List<EnemyLevelInfo> EnemyLevelInfos { get; private set; }
  }
}