using UnityEngine;
using Zenject;

namespace TopDown
{
    public class StartMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _startMenuCanvasObject;

        public override void InstallBindings()
        {
            Container.InjectGameObject(_startMenuCanvasObject);
        }
    }
}