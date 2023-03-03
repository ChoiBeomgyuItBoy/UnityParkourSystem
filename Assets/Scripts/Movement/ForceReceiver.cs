using UnityEngine;

namespace ParkourSystem.Movement
{
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] float groundCheckRadius = 0.2f;
        [SerializeField] Vector3 groundCheckOffset;
        [SerializeField] LayerMask groundLayer;

        bool isGrounded = true;

        float verticalVelocity = 0;

        public Vector3 GetTotalForce()
        {
            return Vector3.up * verticalVelocity;
        }

        void Update()
        {
            GroundCheck();
            ApplyGravity();
        }

        void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
        }

        void ApplyGravity()
        {
            if(isGrounded)
            {
                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }

        // Called in Unity 
        void OnDrawGizmosSelected()
        {
            Color transparentYellow = new Color(0, 1, 0, 0.5f);
            Gizmos.color = transparentYellow;
            Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
        }
    }
}