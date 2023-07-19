using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TopDown {
  public class EnemyProvider : MonoBehaviour {
    private List<Enemy> _enemies;

    private void Start() => AddEnemies();

    private void AddEnemies() => _enemies = new List<Enemy>(FindObjectsOfType<Enemy>());

    public Transform GetEnemyClosestTo(Vector3 pos) {
      if (_enemies.Count == 0) {
        return null;
      }

      Enemy enemy = _enemies.OrderBy(enemy => Vector3.Distance(pos, enemy.GetPosition())).First();
      return (enemy.transform);
    }
  }
}