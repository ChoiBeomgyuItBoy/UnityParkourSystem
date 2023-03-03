using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Hit Obstacle Decision")]
    public class HitObstacleDecision : StateDecision
    {
        [SerializeField] float minHeight = 0;
        [SerializeField] float maxHeight = 1;

        public override bool Decide(StateController controller)
        {
            var obstacleCheck = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            if(!obstacleCheck.forwardHitFound) return false;

            float height = obstacleCheck.heightHit.point.y - controller.transform.position.y;

            return height > minHeight || height < maxHeight;
        }
    }
}
