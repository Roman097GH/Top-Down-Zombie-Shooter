using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class BestStatsUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TextMeshProUGUI _bestTime;
        [SerializeField] private TextMeshProUGUI _currentTime;

        private CompositeDisposable _disposable;
        private GameSessionStats _gameSessionStats;

        [Inject]
        private void Construct(GameSessionStats gameSessionStats)
        {
            _gameSessionStats = gameSessionStats;
        }

        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            AddListeners();
        }

        private void AddListeners()
        {
            _gameSessionStats.BestTime.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
            _gameSessionStats.CurrentTime.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
        }

        private void UpdateInfo()
        {
            _bestTime.text = "Best time: " + Math.Round(_gameSessionStats.BestTime.Value, 1) + " sec";
            _currentTime.text = "Current time: " + Math.Round(_gameSessionStats.CurrentTime.Value, 1) + " sec";
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}