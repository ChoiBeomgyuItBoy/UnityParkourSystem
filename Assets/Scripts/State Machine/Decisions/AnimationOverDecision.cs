using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Animation Over Decision")]
    public class AnimationOverDecision : StateDecision
    {
        [SerializeField] string animationTag = "";
        [SerializeField] float decisionTime = 1;

        public override bool Decide(StateController controller)
        {
            return GetNormalizedTime(controller) > decisionTime;
        }

        private float GetNormalizedTime(StateController controller)
        {
            Animator animator = controller.GetComponent<Animator>();

            var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if(!animator.IsInTransition(0) && currentStateInfo.IsTag(animationTag))
            {
                return currentStateInfo.normalizedTime;
            }

            return 0;
        }   
    }
}
