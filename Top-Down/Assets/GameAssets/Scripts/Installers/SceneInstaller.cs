using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TopDown
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gameObject;
        
        public override void InstallBindings()
        {
            Container.InjectGameObject(_gameObject);
        }
    }
}
