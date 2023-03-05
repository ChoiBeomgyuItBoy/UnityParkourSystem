using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Is Landed Decision")]
    public class IsLandedDecision : StateDecision
    {
        public override bool Decide(StateController controller)
        {
            return controller.GetComponent<ForceReceiver>().IsGrouned();
        }
    }
}