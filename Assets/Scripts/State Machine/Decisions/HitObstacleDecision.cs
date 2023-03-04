using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/Hit Obstacle Decision")]
    public class HitObstacleDecision : StateDecision
    {
        [SerializeField] float minHeight = 0;
        [SerializeField] float maxHeight = 1;
        [SerializeField] string obstacleTag = "Obstacle";

        public override bool Decide(StateController controller)
        {
            var hitData = controller.GetComponent<EnvironmentScanner>().ObstacleCheck();

            if(!hitData.forwardHitFound) return false;

            if(obstacleTag != "" && hitData.forwardHit.transform.tag != obstacleTag) return false;

            float height = hitData.heightHit.point.y - controller.transform.position.y;

            if(height < minHeight || height > maxHeight) return false;

            return true;
        }
    }
}
