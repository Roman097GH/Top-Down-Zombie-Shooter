using UnityEngine;
using Zenject;

namespace TopDown
{
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private GameObject _malePrefab;
        [SerializeField] private GameObject _chemicalPrefab;
        [SerializeField] private GameObject _spacemanPrefab;

        private GameParametrs _gameParametrs;

        [Inject]
        private void Construct(GameParametrs gameParametrs)
        {
            _gameParametrs = gameParametrs;
        }

        public void SelectMale()
        {
            _malePrefab.gameObject.SetActive(true);
            _chemicalPrefab.gameObject.SetActive(false);
            _spacemanPrefab.gameObject.SetActive(false);
            _gameParametrs.PlayerType = PlayerType.Male;
        }

        public void SelectChemical()
        {
            _malePrefab.gameObject.SetActive(false);
            _chemicalPrefab.gameObject.SetActive(true);
            _spacemanPrefab.gameObject.SetActive(false);
            _gameParametrs.PlayerType = PlayerType.Chemical;
        }

        public void SelectSpaceman()
        {
            _malePrefab.gameObject.SetActive(false);
            _chemicalPrefab.gameObject.SetActive(false);
            _spacemanPrefab.gameObject.SetActive(true);
            _gameParametrs.PlayerType = PlayerType.Spaceman;
        }
    }
}