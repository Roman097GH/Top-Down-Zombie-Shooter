using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TopDown
{
    public class GamePanelUI : MonoBehaviour, IInitializable, ITickable {
      [SerializeField] private Slider _sliderHealthBar;
      [SerializeField] private TextMeshProUGUI _enemiesCount;
      [SerializeField] private TextMeshProUGUI _numberOfRounds;
      
      [SerializeField] private Button _attackButton;
       //public ReactiveCommand OnAttack = new ();

      private void UpdateInfo() {
        _enemiesCount.text = "Enemies killed: ";
        _numberOfRounds.text = "Number of rounds: ";
      }

      public void Initialize() {

      }

      public void Tick() {
         
        //_attackButton.onClick.AddListener(ButtonPressed);  
        
        
      }

      // public void ButtonPressed() {
      //   OnAttack.Execute();
      // }
    }
}
