using ParkourSystem.Core;
using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/On Ledge Decision")]
    public class OnLedgeDecision : StateDecision
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] float minLedgeAngle = 50;

        public override bool Decide(StateController controller)
        {
            bool isOnLedge = controller.GetComponent<EnvironmentScanner>().LedgeCheck(CalculateMovement(), out var ledgeData);

            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            return isOnLedge && !hitData.forwardHitFound && ledgeData.angle <= minLedgeAngle;
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
