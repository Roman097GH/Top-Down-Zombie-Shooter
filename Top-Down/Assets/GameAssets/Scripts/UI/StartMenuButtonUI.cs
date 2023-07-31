using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown
{
    public class StartMenuButtonUI : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(2);
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}