using ParkourSystem.Core;
using UnityEngine;
using static ParkourSystem.Core.EnvironmentScanner;

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

        Vector3 matchPosition;

        public override void Act(StateController controller)
        {
            matchPosition = controller.GetComponent<EnvironmentScanner>().GetMatchPosition();

            MatchTarget(controller);
        }

        private void MatchTarget(StateController controller)
        {
            Animator animator = controller.GetComponent<Animator>();
            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            if(animator.IsInTransition(0)) return;
            if(animator.isMatchingTarget) return;
            if(!hitData.forwardHitFound) return;

            animator.MatchTarget
            (
                matchPosition, controller.transform.rotation, 
                GetTarget(hitData), new MatchTargetWeightMask(matchPositionWeight, 0),
                matchStartTime, matchTargetTime
            );
        }

        private AvatarTarget GetTarget(ObstacleHitData hitData)
        {
            if(!dynamicMatching) return matchBodyPart;

            return hitData.avatarTarget;
        }
    }
}
