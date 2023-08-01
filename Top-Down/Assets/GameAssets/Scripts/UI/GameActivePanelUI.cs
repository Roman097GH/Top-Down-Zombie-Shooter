using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TopDown
{
    public class GameActivePanelUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private TextMeshProUGUI _enemiesCount;
        [SerializeField] private TextMeshProUGUI _bestTime;
        [SerializeField] private TextMeshProUGUI _time;
        
        [SerializeField] private TextMeshProUGUI _bulletsCount;
        [SerializeField] private Slider _playerHealthBar;

        private CompositeDisposable _disposable;
        private GameSessionStats _gameSessionStats;
        private GameplayController _gameplayController;
        private PlayerFactoryService _playerFactoryService;

        private int _initialEnemiesCount;

        private float _timer;

        [Inject]
        private void Construct(GameSessionStats gameSessionStats, GameplayController gameplayController, PlayerFactoryService playerFactoryService)
        {
            _gameSessionStats = gameSessionStats;
            _gameplayController = gameplayController;
            _playerFactoryService = playerFactoryService;
            _disposable = new CompositeDisposable();
            
            _playerFactoryService.PlayerCreate.TakeUntilDestroy(this).Subscribe(_ => OnPlayerCreated());
        }

        private void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (_gameSessionStats.EnemiesKilled.Value != _gameSessionStats.InitialEnemiesCount.Value)
            {
                _timer += Time.deltaTime;
                _time.text = "Time: " + Math.Round(_timer, 1);
            }
            else if (_gameSessionStats.EnemiesKilled.Value == _gameSessionStats.InitialEnemiesCount.Value)
            {
                _gameSessionStats.SetBestTime(_timer);
                _gameplayController.GameWin();
            }
        }

        public void Initialize()
        {
            _initialEnemiesCount = _gameSessionStats.InitialEnemiesCount.Value;
            
            AddListeners();
            
            
        }

        private void OnPlayerCreated()
        {
            _playerHealthBar.maxValue = _playerFactoryService.PlayerController.Health.Value;
            _playerFactoryService.PlayerController.Health.Subscribe(UpdateHealth).AddTo(_disposable);
        }

        private void UpdateHealth(float health)
        {
            _playerHealthBar.value = health;
        }
        
        private void AddListeners()
        {
            _gameSessionStats.EnemiesKilled.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
            _gameSessionStats.BestTime.Subscribe(_ => UpdateInfo()).AddTo(_disposable);
        }

        private void UpdateInfo()
        {
            _enemiesCount.text = "Enemies killed: " + _gameSessionStats.EnemiesKilled.Value + " / " +
                                 _initialEnemiesCount;
            _bestTime.text = "Best time: " + Math.Round(_gameSessionStats.BestTime.Value, 1);

           
            
            //Debug.Log(_playerHealthBar.value);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}