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
            var movementValue = controller.CamRelativeMovement(playerInput.GetMovementValue());

            bool isOnLedge = controller.GetComponent<EnvironmentScanner>().LedgeCheck(movementValue, out var ledgeData);

            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            return isOnLedge && !hitData.forwardHitFound && ledgeData.angle <= minLedgeAngle;
        }
    }
}
