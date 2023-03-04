using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Look At Obstacle Action")]
    public class LookAtObstacleAction : StateAction
    {
        [SerializeField] float rotationDamp = 500;

        public override void Act(StateController controller)
        {
            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            Transform controllerTransform = controller.transform;

            if(hitData.forwardHit.normal == Vector3.zero) return;

            var targetRotation = Quaternion.LookRotation(-hitData.forwardHit.normal);

            controllerTransform.rotation = Quaternion.RotateTowards
            (
                controllerTransform.rotation, targetRotation,
                Time.deltaTime * rotationDamp
            );
        }
    }
}
