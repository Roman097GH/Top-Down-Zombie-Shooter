using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown
{
    public class StartMenuButtonsUI : MonoBehaviour
    {
        public void StartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}