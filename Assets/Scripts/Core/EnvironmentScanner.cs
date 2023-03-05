using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] float ledgeOriginOffset = 0.5f;

        public struct ObstacleHitData
        {
            public bool forwardHitFound;
            public bool heightHitFound;
            public RaycastHit forwardHit;
            public RaycastHit heightHit;
            public AvatarTarget matchBodyPart;
        }

        public struct LedgeData
        {
            public float height;
            public float angle;
            public RaycastHit surfaceHit;
        }

        public bool ShouldMirror()
        {
            var hitData = ObstacleCheck();

            var hitPoint = hitData.forwardHit.transform.InverseTransformPoint(hitData.forwardHit.point);

            var shouldMirror = hitPoint.z < 0 && hitPoint.x < 0 || hitPoint.z > 0 && hitPoint.x > 0;

            return shouldMirror;
        }

        public bool LedgeCheck(Vector3 moveDirection, out LedgeData ledgeData)
        {
            ledgeData = new LedgeData();

            if(moveDirection == Vector3.zero) return false;

            Vector3 origin = transform.position + moveDirection * ledgeOriginOffset + Vector3.up;
            List<RaycastHit> hits = new List<RaycastHit>();

            bool inLedge = PhysicsUtil.ThreeRaycast
            (
                origin, Vector3.down, 0.25f, transform, out hits, 
                ledgeRayLength, obstacleLayer, true
            );

            if(inLedge)
            {
                var validHits = hits.Where((h) => transform.position.y - h.point.y > ledgeHeightTreshold).ToList();

                if(validHits.Count() > 0)
                {
                    var surfaceRayorigin = validHits[0].point;
                    surfaceRayorigin.y = transform.position.y - 0.1f;

                    if(Physics.Raycast(surfaceRayorigin, transform.position - surfaceRayorigin, out RaycastHit surfaceHit, 2, obstacleLayer))
                    {
                        Debug.DrawLine(surfaceRayorigin, transform.position, Color.cyan);
                        
                        float height = transform.position.y - validHits[0].point.y;

                        ledgeData.angle = Vector3.Angle(transform.forward, surfaceHit.normal);
                        ledgeData.height = height;
                        ledgeData.surfaceHit = surfaceHit;
                    
                        return true;
                    }
                }
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
