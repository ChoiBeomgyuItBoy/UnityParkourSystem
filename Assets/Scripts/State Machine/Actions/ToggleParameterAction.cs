using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Toggle Parameter Action")]
    public class ToggleParameterAction : StateAction
    {
        [SerializeField] string parameter = "";
        [SerializeField] bool activate = true;

        public override void Act(StateController controller)
        {
            controller.GetComponent<Animator>().SetBool(parameter, activate);
        }
    }
}
