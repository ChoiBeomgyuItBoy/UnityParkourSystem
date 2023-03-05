using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Toggle Character Controller Action")]
    public class ToggleCharacterControllerAction : StateAction
    {
        [SerializeField] bool enabled = false;

        public override void Act(StateController controller)
        {
            controller.GetComponent<CharacterController>().enabled = enabled;
        }
    }
}
