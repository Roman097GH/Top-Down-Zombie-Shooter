using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TopDown
{
    public class GameSceneButtonsUI : MonoBehaviour
    {
        private GameplayController _gameplayController;

        [Inject]
        private void Construct(GameplayController gameplayController)
        {
            _gameplayController = gameplayController;
        }

        public void Pause()
        {
            _gameplayController.State.Value = GameState.GamePaused;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            _gameplayController.State.Value = GameState.GameActive;
            Time.timeScale = 1;
        }
        
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MenuGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}