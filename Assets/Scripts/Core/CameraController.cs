using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] Transform followTarget;
        [SerializeField] float horizontalRotationSpeed = 0.1f;
        [SerializeField] float verticalRotationSpeed = 0.5f;
        [SerializeField] float distance = 5;
        [SerializeField] float minVerticalAngle = 2;
        [SerializeField] float maxVerticalAngle = 45;
        [SerializeField] Vector2 framingOffset;
        [SerializeField] bool invertX;
        [SerializeField] bool invertY;

        float rotationY = 0;
        float rotationX = 0;

        float invertXValue = 0;
        float invertYValue = 0;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            invertXValue = invertX ? -1 : 1;
            invertYValue = invertY ? -1 : 1;

            rotationX += playerInput.GetMouseValue().y * invertYValue * verticalRotationSpeed;
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
            
            rotationY += playerInput.GetMouseValue().x * invertXValue * horizontalRotationSpeed;
            
            Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

            Vector3 focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

            transform.position = focusPosition - targetRotation * (Vector3.forward * distance);
            transform.rotation = targetRotation;
        }
    }
}