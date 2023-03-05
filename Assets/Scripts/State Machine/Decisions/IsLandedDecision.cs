using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Is Landed Decision")]
    public class IsLandedDecision : StateDecision
    {
        public override bool Decide(StateController controller)
        {
            Debug.Log(controller.GetComponent<ForceReceiver>().IsGrouned());
            return controller.GetComponent<ForceReceiver>().IsGrouned();
        }
    }
}