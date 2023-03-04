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

        Vector3 matchPosition;

        public override void Act(StateController controller)
        {
            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            matchPosition = hitData.heightHit.point;

            MatchTarget(controller);
        }

        private void MatchTarget(StateController controller)
        {
            Animator animator = controller.GetComponent<Animator>();

            if(animator.IsInTransition(0)) return;
            if(animator.isMatchingTarget) return;
            if(matchPosition == Vector3.zero) return;

            animator.MatchTarget
            (
                matchPosition, controller.transform.rotation, 
                matchBodyPart, new MatchTargetWeightMask(matchPositionWeight, 0),
                matchStartTime, matchTargetTime
            );
        }
    }
}
