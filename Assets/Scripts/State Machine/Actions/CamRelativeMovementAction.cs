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
            EnvironmentScanner scanner = controller.GetComponent<EnvironmentScanner>();
            Vector3 moveDirection = CamRelativeMovement();

            bool onLedge = scanner.LedgeCheck(moveDirection, out var ledgeData);

            if(onLedge)
            {
                float angle = Vector3.Angle(ledgeData.surfaceHit.normal, moveDirection);
    
                if(angle < 90)
                {
                    mover.MoveTo(Vector3.zero, 0);
                }
            }
            else
            {
                mover.MoveTo(moveDirection, speedFraction);
                mover.LookAt(moveDirection);
            }
        }

        Vector3 CamRelativeMovement()
        {
            Vector2 inputValue = playerInput.GetMovementValue();
            CameraController cameraController = Camera.main.GetComponent<CameraController>();

            Vector3 moveInput = new Vector3(inputValue.x, 0, inputValue.y);

            return cameraController.GetPlanarRotation() * moveInput;
        }
    }
}
