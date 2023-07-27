using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class ScoreService : IInitializable
    {
        public ReactiveProperty<int> BestKillScore = new();
        public ReactiveProperty<int> EnemiesKilled = new();
        public ReactiveProperty<int> InitialEnemiesCount = new();

        private string BEST_KILL_SCORE = "BestKillScore";

        public void Initialize()
        {
            BestKillScore.Value = PlayerPrefs.GetInt(BEST_KILL_SCORE);
        }

        public void SetInitialEnemiesCount(int count)
        {
            InitialEnemiesCount.Value = count;
        }

        public void SetEnemiesKilledCount(int count)
        {
            int killed = InitialEnemiesCount.Value - count;
            EnemiesKilled.Value = killed;

            if (BestKillScore.Value >= killed) return;

            PlayerPrefs.SetInt(BEST_KILL_SCORE, killed);
            BestKillScore.Value = killed;

            PlayerPrefs.Save();
        }
    }
}