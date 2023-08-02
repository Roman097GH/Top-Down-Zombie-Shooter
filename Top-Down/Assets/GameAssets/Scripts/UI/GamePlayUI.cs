using UniRx;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameActivePanelUI;
        [SerializeField] private GameObject _gamePausePanelUI;
        [SerializeField] private GameObject _gameWinPanelUI;
        [SerializeField] private GameObject _gameOverPanelUI;

        private GameplayController _gameplayController;

        [Inject]
        private void Construct(GameplayController gameplayController)
        {
            _gameplayController = gameplayController;
        }

        private void OnEnable()
        {
            _gameplayController.State.TakeUntilDestroy(this).Subscribe(TogglePanels);
        }

        private void TogglePanels(GameState gameState)
        {
            _gameActivePanelUI.gameObject.SetActive(gameState == GameState.GameActive);
            _gamePausePanelUI.gameObject.SetActive(gameState == GameState.GamePaused);
            _gameWinPanelUI.gameObject.SetActive(gameState == GameState.GameWin);
            _gameOverPanelUI.gameObject.SetActive(gameState == GameState.GameOver);
        }
    }
}