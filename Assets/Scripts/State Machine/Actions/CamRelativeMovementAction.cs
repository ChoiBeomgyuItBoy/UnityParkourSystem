using ParkourSystem.Core;
using ParkourSystem.Input;
using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Cam Relative Movement Action")]
    public class CamRelativeMovementAction : StateAction
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] [Range(0,1)] float speedFraction = 0.8f;

        public override void Act(StateController controller)
        {
            Mover mover = controller.GetComponent<Mover>();

            mover.MoveTo(CalculateMovement(), speedFraction);
            mover.LookAt(CalculateMovement());
        }

        Vector3 CalculateMovement()
        {
            Vector2 inputValue = playerInput.GetMovementValue();
            CameraController cameraController = Camera.main.GetComponent<CameraController>();

            Vector3 moveInput = new Vector3(inputValue.x, 0, inputValue.y);

            return cameraController.GetPlanarRotation() * moveInput;
        }
    }
}
