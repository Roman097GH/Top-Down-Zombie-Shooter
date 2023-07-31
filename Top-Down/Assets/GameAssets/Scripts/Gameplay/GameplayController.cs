using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    [UsedImplicitly]
    public class GameplayController : IInitializable
    {
        private readonly PlayerFactoryService _playerFactoryService;
        private readonly CharacterSelection _characterSelection;
        

        public readonly ReactiveProperty<GameState> State = new(GameState.GameActive);
        private GameParametrs _gameParametrs;

        public GameplayController(PlayerFactoryService playerFactoryService, GameParametrs gameParametrs)
        {
            _gameParametrs = gameParametrs;
            _playerFactoryService = playerFactoryService;
        }

        void IInitializable.Initialize()
        {
            _playerFactoryService.Create(_gameParametrs.PlayerType);
        }
        
        public void GameWin()
        {
            Time.timeScale = 0;
            State.Value = GameState.GameWin;
        }
    }
}