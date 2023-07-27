using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class EnemyProvider : ITickable
    {
        private readonly List<EnemyBase> _enemies = new();

        private readonly ScoreService _scoreService;

        public EnemyProvider(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void AddEnemy(EnemyBase enemy)
        {
            _enemies.Add(enemy);
            int countEnemies = _enemies.Count;
            _scoreService.SetInitialEnemiesCount(countEnemies);
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
                _scoreService.SetEnemiesKilledCount(_enemies.Count);
            }
        }
    }
}