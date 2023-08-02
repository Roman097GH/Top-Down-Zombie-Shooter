using UnityEngine;

namespace TopDown
{
    public class EnemyFactoryService
    {
        private readonly EnemyProvider _enemyProvider;

        public EnemyFactoryService(EnemyProvider enemyProvider) => _enemyProvider = enemyProvider;

        public void Create(SOEnemy enemyInfo, int enemyLevel, Transform pointTransform)
        {
            EnemyBase enemyBaseInstance =
                Object.Instantiate(enemyInfo.Enemy, pointTransform.position, pointTransform.rotation);
            enemyBaseInstance.Initialize(enemyInfo, enemyLevel);
            _enemyProvider.AddEnemy(enemyBaseInstance);
        }
    }
}