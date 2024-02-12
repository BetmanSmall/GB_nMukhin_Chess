using UnityEngine;
namespace ChessGame.Scripts {
    public class CameraControl : MonoBehaviour {
        [SerializeField] private Transform whiteCamera;
        [SerializeField] private Transform blackCamera;
        [SerializeField] private bool cameraOnWhiteTransform = true;

        public void ChangeCameraTransform() {
            ChangeCameraTransform(!cameraOnWhiteTransform);
        }

        public void ChangeCameraTransform(bool _cameraOnWhiteTransform) {
            cameraOnWhiteTransform = _cameraOnWhiteTransform;
            if (cameraOnWhiteTransform) {
                gameObject.transform.SetPositionAndRotation(whiteCamera.position, whiteCamera.rotation);
            } else {
                gameObject.transform.SetPositionAndRotation(blackCamera.position, blackCamera.rotation);
            }
        }
    }
}