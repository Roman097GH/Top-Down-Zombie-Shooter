using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TopDown
{
    public class ButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        protected readonly ReactiveProperty<bool> ButtonState = new();

        public void OnPointerDown(PointerEventData _) => ButtonState.Value = true;

        public void OnPointerUp(PointerEventData _) => ButtonState.Value = false;
    }
}