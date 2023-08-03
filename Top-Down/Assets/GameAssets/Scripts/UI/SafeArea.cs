using UnityEngine;

namespace TopDown
{
    public class SafeArea : MonoBehaviour
    {
        #region Simulations

        private enum SimDevice
        {
            None,
            iPhoneX,
            iPhoneXsMax,
            Pixel3XL_LSL,
            Pixel3XL_LSR
        }

        private static readonly SimDevice Sim = SimDevice.None;

        private readonly Rect[] NSA_iPhoneX =
        {
            new(0f, 102f / 2436f, 1f, 2202f / 2436f),
            new(132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f),
        };

        private readonly Rect[] NSA_iPhoneXsMax =
        {
            new(0f, 102f / 2688f, 1f, 2454f / 2688f),
            new(132f / 2688f, 63f / 1242f, 2424f / 2688f, 1179f / 1242f),
        };

        private readonly Rect[] NSA_Pixel3XL_LSL =
        {
            new(0f, 0f, 1f, 2789f / 2960f),
            new(0f, 0f, 2789f / 2960f, 1f),
        };

        private readonly Rect[] NSA_Pixel3XL_LSR =
        {
            new(0f, 0f, 1f, 2789f / 2960f),
            new(171f / 2960f, 0f, 2789f / 2960f, 1f),
        };

        #endregion

        [SerializeField] private bool _updateWidth = true;
        [SerializeField] private bool _updateHeight = true;

        [SerializeField, HideInInspector] RectTransform _panel;

        private Rect _lastSafeArea = new(0, 0, 0, 0);
        private Vector2Int _lastScreenSize = new(0, 0);
        private ScreenOrientation _lastOrientation = ScreenOrientation.AutoRotation;

        private void OnValidate()
        {
            _panel = GetComponent<RectTransform>();
            enabled = _updateWidth || _updateHeight;
        }

        private void Awake()
        {
            if (_panel == null)
            {
                Debug.LogError("Cannot apply safe area - no RectTransform found on " + name);
                Destroy(gameObject);
            }

            Refresh();
        }

        private void Update() => Refresh();

        private void Refresh()
        {
            if (!_updateWidth && !_updateHeight) return;
            Rect safeArea = GetSafeArea();
            if (safeArea == _lastSafeArea
                && Screen.width == _lastScreenSize.x
                && Screen.height == _lastScreenSize.y
                && Screen.orientation == _lastOrientation) return;
            ApplySafeArea(safeArea);
        }

        private Rect GetSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            if (!Application.isEditor || Sim == SimDevice.None) return safeArea;
            Rect nsa = Sim switch
            {
                SimDevice.iPhoneX => Screen.height > Screen.width ? NSA_iPhoneX[0] : NSA_iPhoneX[1],
                SimDevice.iPhoneXsMax => Screen.height > Screen.width ? NSA_iPhoneXsMax[0] : NSA_iPhoneXsMax[1],
                SimDevice.Pixel3XL_LSL => Screen.height > Screen.width ? NSA_Pixel3XL_LSL[0] : NSA_Pixel3XL_LSL[1],
                SimDevice.Pixel3XL_LSR => Screen.height > Screen.width ? NSA_Pixel3XL_LSR[0] : NSA_Pixel3XL_LSR[1],
                _ => new(0, 0, Screen.width, Screen.height)
            };
            return new(Screen.width * nsa.x, Screen.height * nsa.y, Screen.width * nsa.width,
                Screen.height * nsa.height);
        }

        private void ApplySafeArea(Rect rect)
        {
            _lastSafeArea = rect;
            _lastScreenSize.x = Screen.width;
            _lastScreenSize.y = Screen.height;
            _lastOrientation = Screen.orientation;

            if (!_updateWidth)
            {
                rect.x = 0;
                rect.width = Screen.width;
            }

            if (!_updateHeight)
            {
                rect.y = 0;
                rect.height = Screen.height;
            }

            if (Screen.width <= 0 || Screen.height <= 0) return;
            Vector2 anchorMin = rect.position;
            Vector2 anchorMax = rect.position + rect.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (!(anchorMin.x >= 0) || !(anchorMin.y >= 0) || !(anchorMax.x >= 0) || !(anchorMax.y >= 0)) return;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }
    }
}