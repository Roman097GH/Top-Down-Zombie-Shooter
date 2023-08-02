using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    [UsedImplicitly]
    public class GameplayController : IInitializable, IDisposable
    {
        private readonly PlayerFactoryService _playerFactoryService;
        private readonly CharacterSelection _characterSelection;
        private readonly GameParametrs _gameParametrs;

        public readonly ReactiveProperty<GameState> State = new(GameState.GameActive);

        public GameplayController(PlayerFactoryService playerFactoryService, GameParametrs gameParametrs)
        {
            _playerFactoryService = playerFactoryService;
            _gameParametrs = gameParametrs;
        }

        void IInitializable.Initialize()
        {
            _playerFactoryService.Create(_gameParametrs.PlayerType);
            _playerFactoryService.PlayerController.OnDeath += OnPlayerDeath;
        }

        void IDisposable.Dispose()
        {
            _playerFactoryService.PlayerController.OnDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            GameOver();
        }

        public void GameWin()
        {
            State.Value = GameState.GameWin;
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            State.Value = GameState.GameOver;
            Time.timeScale = 0;
        }
    }
}