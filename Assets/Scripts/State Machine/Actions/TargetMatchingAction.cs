using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Target Matching Action")]
    public class TargetMatchingAction : StateAction
    {
        [SerializeField] AvatarTarget matchBodyPart;
        [SerializeField] float matchStartTime = 0;
        [SerializeField] float matchTargetTime = 0;
        [SerializeField] Vector3 matchPositionWeight = new Vector3(0, 1, 0);
        [SerializeField] bool dynamicMatching = false;

        public override void Act(StateController controller)
        {
            MatchTarget(controller);
        }

        private void MatchTarget(StateController controller)
        {
            EnvironmentScanner scanner = controller.GetComponent<EnvironmentScanner>();
            Animator animator = controller.GetComponent<Animator>();
            var hitData = scanner.ObstacleCheck();

            if(animator.IsInTransition(0)) return;
            if(animator.isMatchingTarget) return;
            if(!hitData.forwardHitFound) return;

            if(!dynamicMatching)
            {
                hitData.matchBodyPart = matchBodyPart;
            }

            animator.MatchTarget
            (
                scanner.GetMatchPosition(), 
                controller.transform.rotation, 
                hitData.matchBodyPart, 
                new MatchTargetWeightMask(matchPositionWeight, 0),
                matchStartTime, matchTargetTime
            );
        }
    }
}
