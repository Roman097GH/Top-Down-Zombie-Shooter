using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class GameSessionStats : IInitializable
    {
        public readonly ReactiveProperty<int> InitialEnemiesCount = new();
        public readonly ReactiveProperty<int> EnemiesKilled = new();
        public readonly ReactiveProperty<int> BestKillScore = new();
        public readonly ReactiveProperty<float> BestTime = new();
        
        
        public ReactiveProperty<float> CurrentTime = new();

        private const string BEST_KILL_SCORE = "BestKillScore";
        private const string BEST_TIME_SCORE = "BestTimeScore";

        public void Initialize()
        {
            BestKillScore.Value = PlayerPrefs.GetInt(BEST_KILL_SCORE);
            BestTime.Value = PlayerPrefs.GetFloat(BEST_TIME_SCORE);
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

        public void SetBestTime(float time)
        {
            CurrentTime.Value = time;
            
            if (BestTime.Value > 0 &&  BestTime.Value <= time) return;
            
            PlayerPrefs.SetFloat(BEST_TIME_SCORE, time);
            BestTime.Value = time;
            
            PlayerPrefs.Save();
        }
    }
}