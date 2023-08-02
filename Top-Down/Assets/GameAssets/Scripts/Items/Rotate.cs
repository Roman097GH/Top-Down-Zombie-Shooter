using UnityEngine;

namespace TopDown
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float _speedRotate = 20.0f;

        private void Update()
        {
            transform.Rotate(0, _speedRotate * Time.deltaTime, 0);
        }
    }
}