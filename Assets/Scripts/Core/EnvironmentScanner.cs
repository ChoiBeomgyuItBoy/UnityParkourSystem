using UnityEngine;

namespace ParkourSystem.Core
{
    public class EnvironmentScanner : MonoBehaviour
    {
        [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 0.25f, 0);
        [SerializeField] float forwardRayLength = 0.8f;
        [SerializeField] float heightRayLength = 5;
        [SerializeField] float ledgeRayLength = 10;
        [SerializeField] LayerMask obstacleLayer;
        [SerializeField] float ledgeHeightTreshold = 0.75f;

        public struct ObstacleHitData
        {
            public bool forwardHitFound;
            public bool heightHitFound;
            public RaycastHit forwardHit;
            public RaycastHit heightHit;
            public AvatarTarget matchBodyPart;
        }

        public bool ShouldMirror()
        {
            var hitData = ObstacleCheck();

            var hitPoint = hitData.forwardHit.transform.InverseTransformPoint(hitData.forwardHit.point);

            var shouldMirror = hitPoint.z < 0 && hitPoint.x < 0 || hitPoint.z > 0 && hitPoint.x > 0;

            return shouldMirror;
        }

        public bool CheckLedge(Vector3 moveDirection)
        {
            if(moveDirection == Vector3.zero) return false;

            float originOffset = 0.5f;
            Vector3 origin = transform.position + moveDirection * originOffset + Vector3.up;
            bool inLedge = Physics.Raycast(origin, Vector3.down, out RaycastHit hit, ledgeRayLength, obstacleLayer);

            if(inLedge)
            {
                Debug.DrawRay(origin, Vector3.down * ledgeRayLength, Color.green);

                float height = transform.position.y - hit.point.y;

                return height > ledgeHeightTreshold;
            }

            return false;
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
