using UnityEngine;

namespace ParkourSystem.Core
{
    public class EnvironmentScanner : MonoBehaviour
    {
        [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 0.25f, 0);
        [SerializeField] float forwardRayLength = 0.8f;
        [SerializeField] float heightRayLength = 5;
        [SerializeField] LayerMask obstacleLayer;

        public struct ObstacleHitData
        {
            public bool forwardHitFound;
            public bool heightHitFound;
            public RaycastHit forwardHit;
            public RaycastHit heightHit;
        }

        public ObstacleHitData ObstacleCheck()
        {
            ObstacleHitData hitData = new ObstacleHitData();

            ForwardRayCheck(ref hitData);

            if(hitData.forwardHitFound)
            {
                HeightRayCheck(ref hitData);
            }

            return hitData;
        } 

        private void ForwardRayCheck(ref ObstacleHitData hitData)
        {
            Vector3 forwardOrigin = transform.position + forwardRayOffset;

            hitData.forwardHitFound = Physics.Raycast
            (
                forwardOrigin, transform.forward, 
                out hitData.forwardHit, forwardRayLength, obstacleLayer
            );

            Color hitColor = hitData.forwardHitFound? Color.red : Color.white;
            Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, hitColor);
        }

        private void HeightRayCheck(ref ObstacleHitData hitData)
        {
            Vector3 heightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLength;

            hitData.heightHitFound = Physics.Raycast
            (
                heightOrigin, Vector3.down, 
                out hitData.heightHit, heightRayLength, obstacleLayer
            );
                
            Color hitColor = hitData.heightHitFound? Color.red : Color.white;
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, hitColor);
        }
    }
}
