using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Cross Fade Animation Action")]
    public class CrossFadeAnimationAction : StateAction
    {
        [SerializeField] string animationName = "";
        [SerializeField] float dampTime = 0.1f;

        public override void Act(StateController controller)
        {
            controller.GetComponent<Animator>().CrossFade(animationName, dampTime);
        }
    }
}
