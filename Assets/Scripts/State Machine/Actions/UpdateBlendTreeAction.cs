using ParkourSystem.Input;
using ParkourSystem.Movement;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Update Blend Tree Action")]
    public class UpdateBlendTreeAction : StateAction
    {
        [SerializeField] string parameter = "";
        [SerializeField] float dampTime = 0.1f;

        public override void Act(StateController controller)
        {
            var charController = controller.GetComponent<CharacterController>();
            var animator = controller.GetComponent<Animator>();
            float magnitude = charController.velocity.magnitude;

            animator.SetFloat(parameter, magnitude, dampTime, Time.deltaTime);
        }
    }
}
