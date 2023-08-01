using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] private GameActivePanelUI _gameActivePanelUI;
        [SerializeField] private GameObject _gameWinUI;
        [SerializeField] private GameObject _gamePauseUI;
        [SerializeField] private GameObject _gameOverUI;

        private GameplayController _gameplayController;

        [Inject]
        private void Construct(GameplayController gameplayController)
        {
            _gameplayController = gameplayController;
        }

        private void OnValidate()
        {
            _gameActivePanelUI = GetComponentInChildren<GameActivePanelUI>();
        }

        private void OnEnable()
        {
            _gameplayController.State.TakeUntilDestroy(this).Subscribe(TogglePanels);
        }

        private void TogglePanels(GameState gameState)
        {
            _gameActivePanelUI.gameObject.SetActive(gameState == GameState.GameActive);
            _gamePauseUI.gameObject.SetActive(gameState == GameState.GamePaused);
            _gameWinUI.gameObject.SetActive(gameState == GameState.GameWin);
            _gameOverUI.gameObject.SetActive(gameState == GameState.GameOver);
        }
    }
}