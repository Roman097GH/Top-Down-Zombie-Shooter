using System;
using UnityEngine;

namespace TopDown {
  [Serializable]
  public class EnemyLevelInfo {
    [field: SerializeField] public float FollowRadius { get; private set; }
    [field: SerializeField] public float AttackRadius { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
  }
}