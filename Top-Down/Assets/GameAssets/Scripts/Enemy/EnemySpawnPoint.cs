using UnityEngine;
using Zenject;

namespace TopDown {
  public class EnemySpawnPoint : MonoBehaviour {
    [Range(1, 10)] [SerializeField] private int _enemyLevel;
    [SerializeField] private SOEnemy _soEnemy;
    private Player _player;

    [Inject]
    private void Construct(EnemyFactoryService enemyFactoryService) {
      enemyFactoryService.Create(_soEnemy, _enemyLevel - 1, transform);
      Destroy(gameObject);
    }
  }
}