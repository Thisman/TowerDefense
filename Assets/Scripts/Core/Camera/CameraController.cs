using UnityEngine;

namespace Game.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [Header("Camera Move Settings")]
        [SerializeField]
        private float _cameraMoveSpeed = 10f;

        [SerializeField]
        private float _leftBound = -10f;

        [SerializeField]
        private float _rightBound = 10f;

        [SerializeField]
        private float _bottomBound = -10f;

        [SerializeField]
        private float _topBound = 10f;

        [SerializeField]
        private float _mouseSensitiveEdgeSize = 20f;

        [Header("Camera Zoom Settings")]
        [SerializeField]
        private float zoomSpeed = 10f;

        [SerializeField]
        private float minZoom = 5f;

        [SerializeField]
        private float maxZoom = 15f;

        public void Update()
        {
            ZoomCamera();
            MoveCamera();
        }

        private void MoveCamera()
        {
            Vector3 cameraPosition = gameObject.transform.position;
            Vector3 direction = Vector3.zero;

            // Check mouse movement near screen edges
            if (Input.mousePosition.x < _mouseSensitiveEdgeSize)
                direction.x = -1; // Move left
            else if (Input.mousePosition.x > Screen.width - _mouseSensitiveEdgeSize)
                direction.x = 1;  // Move right

            if (Input.mousePosition.y < _mouseSensitiveEdgeSize)
                direction.y = -1; // Move down
            else if (Input.mousePosition.y > Screen.height - _mouseSensitiveEdgeSize)
                direction.y = 1;  // Move up

            // Check movement using WASD keys
            if (Input.GetKey(KeyCode.W))
                direction.y += 1; // Move up
            if (Input.GetKey(KeyCode.S))
                direction.y -= 1; // Move down
            if (Input.GetKey(KeyCode.A))
                direction.x -= 1; // Move left
            if (Input.GetKey(KeyCode.D))
                direction.x += 1; // Move right

            // Move the camera
            cameraPosition += direction.normalized * _cameraMoveSpeed * Time.deltaTime;

            // Clamp the camera within the defined bounds
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, _leftBound, _rightBound);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, _bottomBound, _topBound);

            // Keep the Z position constant
            cameraPosition.z = -10;

            // Apply the new position
            gameObject.transform.position = cameraPosition;
        }

        private void ZoomCamera()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput != 0)
            {
                // �������� ������ ��������������� ������
                _camera.orthographicSize -= scrollInput * zoomSpeed;
                _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
            }
        }
    }
}