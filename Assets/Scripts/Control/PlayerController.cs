using ParkourSystem.Input;
using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField][Range(0,1)] float speedFraction = 0.8f;

        void Update()
        {
            GetComponent<Mover>().MoveTo(CameraRelativeMovement(), speedFraction);
            GetComponent<Mover>().LookAt(CameraRelativeMovement());
        }

        Vector3 CameraRelativeMovement()
        {
            Vector3 inputValue = playerInput.GetMovementValue();
            Vector3 right = (Camera.main.transform.right * inputValue.x).normalized;
            right.y = 0;
            Vector3 forward = (Camera.main.transform.forward * inputValue.y).normalized;
            forward.y = 0;

            return right + forward;
        }
    }
}