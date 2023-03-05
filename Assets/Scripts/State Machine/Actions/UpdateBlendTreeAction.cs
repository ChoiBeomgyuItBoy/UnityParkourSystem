using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Update Blend Tree Action")]
    public class UpdateBlendTreeAction : StateAction
    {
        [SerializeField] string parameter = "";
        [SerializeField] float dampTime = 0.1f;
        [SerializeField] PlayerInput playerInput;

        public override void Act(StateController controller)
        {
            Vector2 inputVector = playerInput.GetMovementValue();
            float inputValue = Mathf.Abs(inputVector.x) + Mathf.Abs(inputVector.y);

            controller.GetComponent<Animator>().SetFloat(parameter, inputValue, dampTime, Time.deltaTime);
        }
    }
}
