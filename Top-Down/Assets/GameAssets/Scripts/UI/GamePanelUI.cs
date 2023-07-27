using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class GamePanelUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TextMeshProUGUI _enemiesCount;
        [SerializeField] private TextMeshProUGUI _bestScore;

        private CompositeDisposable _disposable;
        private ScoreService _scoreService;

        private int _initialEnemiesCount;
        
        [Inject]
        private void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _initialEnemiesCount = _scoreService.InitialEnemiesCount.Value;
            _disposable = new CompositeDisposable();
            AddListeners();
        }

        private void AddListeners()
        {
            _scoreService.EnemiesKilled.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
            _scoreService.BestKillScore.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
        }

        private void UpdateInfo()
        {
            _enemiesCount.text = "Enemies killed: " + _scoreService.EnemiesKilled.Value + " / " + _initialEnemiesCount;
            _bestScore.text = "Best kill Score: " + _scoreService.BestKillScore.Value;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}