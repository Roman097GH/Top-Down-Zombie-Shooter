using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneAsset _sceneAsset;

        private void Start() => SceneManager.LoadScene(_sceneAsset.name);
    }
}