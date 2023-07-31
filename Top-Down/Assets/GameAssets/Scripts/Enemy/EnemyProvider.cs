using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class EnemyProvider : ITickable
    {
        private readonly List<EnemyBase> _enemies = new();

        private readonly GameSessionStats _gameSessionStats;

        public EnemyProvider(GameSessionStats gameSessionStats)
        {
            _gameSessionStats = gameSessionStats;
        }

        public void AddEnemy(EnemyBase enemy)
        {
            _enemies.Add(enemy);
            int countEnemies = _enemies.Count;
            _gameSessionStats.SetInitialEnemiesCount(countEnemies);
        }

        public Transform GetEnemyClosestTo(Vector3 pos)
        {
            if (_enemies.Count == 0)
            {
                return null;
            }

            EnemyBase enemy = _enemies.OrderBy(enemy => Vector3.Distance(pos, enemy.GetPosition())).First();
            return (enemy.transform);
        }

        public void Tick()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null) continue;
                _enemies.RemoveAt(i);
                _gameSessionStats.SetEnemiesKilledCount(_enemies.Count);
            }
        }
    }
}