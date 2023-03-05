using ParkourSystem.Core;
using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Ledge Height Decision")]
    public class LedgeHeightDecision : StateDecision
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] float minHeight = 1;

        public override bool Decide(StateController controller)
        {
            var scanner = controller.GetComponent<EnvironmentScanner>();

            scanner.LedgeCheck(controller.CamRelativeMovement(playerInput.GetMovementValue()), out var ledgeData);

            return ledgeData.height < minHeight;
        }
    }
}
