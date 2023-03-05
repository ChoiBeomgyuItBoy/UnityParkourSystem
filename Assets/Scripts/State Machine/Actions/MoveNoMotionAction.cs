using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Move No Motion Action")]
    public class MoveNoMotionAction : StateAction
    {
        public override void Act(StateController controller)
        {
            controller.GetComponent<Mover>().MoveTo(Vector3.zero, 0);
        }
    }
}
