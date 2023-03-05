using System;
using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Cross Fade Animation Action")]
    public class CrossFadeAnimationAction : StateAction
    {
        [SerializeField] string animationName = "";
        [SerializeField] float dampTime = 0.1f;
        [SerializeField] bool dynamicMirroring = false;
        [SerializeField] string mirroringParameter = "";

        public override void Act(StateController controller)
        {
            if(dynamicMirroring)
            {
                SetMirroring(controller);
            }

            controller.GetComponent<Animator>().CrossFade(animationName, dampTime);
        }

        private void SetMirroring(StateController controller)
        {
            var scanner = controller.GetComponent<EnvironmentScanner>();
            var avatarTarget = scanner.ShouldMirror() ? AvatarTarget.RightHand : AvatarTarget.LeftHand;
            var currentHitData = scanner.ObstacleCheck();
              
            currentHitData.avatarTarget = avatarTarget;
            controller.GetComponent<Animator>().SetBool(mirroringParameter, scanner.ShouldMirror());
        }
    }
}
