using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Input Decision")]
    public class InputDecision : StateDecision
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] InputType inputType = default;

        public override bool Decide(StateController controller)
        {
            switch(inputType)
            {
                case InputType.Jump when playerInput.Jumped():
                    return true;
            }

            return false;
        }

        enum InputType
        {
            Jump
        }
    }
}