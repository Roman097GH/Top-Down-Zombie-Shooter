using UnityEngine;

namespace TopDown {
  public class EnemyFactoryService {
    public void Create(SOEnemy enemyInfo, int enemyLevel, Transform pointTransform) {
      EnemyBase enemyBaseInstance =
        Object.Instantiate(enemyInfo.Enemy, pointTransform.position, pointTransform.rotation);
      enemyBaseInstance.Initialize(enemyInfo, enemyLevel);
    }
  }
}