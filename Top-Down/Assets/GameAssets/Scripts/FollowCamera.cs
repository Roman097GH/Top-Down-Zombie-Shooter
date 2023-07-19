using Cinemachine;
using UnityEngine;

namespace TopDown {
  public class FollowCamera : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera _camera;

    public void Follow(Transform playerTransform) {
      _camera.Follow = playerTransform;
      _camera.LookAt = playerTransform;
    }
  }
}