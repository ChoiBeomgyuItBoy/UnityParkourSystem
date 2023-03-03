using ParkourSystem.Core;
using ParkourSystem.Input;
using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField][Range(0,1)] float speedFraction = 0.8f;

        CameraController cameraController;

        void Awake()
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }

        void Update()
        {
            GetComponent<Mover>().MoveTo(CameraRelativeMovement(), speedFraction);
            GetComponent<Mover>().LookAt(CameraRelativeMovement());
            GetComponent<EnvironmentScanner>().ObstacleCheck();
        }

        Vector3 CameraRelativeMovement()
        {
            Vector2 inputValue = playerInput.GetMovementValue();

            Vector3 moveInput = new Vector3(inputValue.x, 0, inputValue.y);

            return cameraController.GetPlanarRotation() * moveInput;
        }
    }
}