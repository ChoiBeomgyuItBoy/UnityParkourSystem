using UnityEngine;

namespace ParkourSystem.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 6;
        [SerializeField] float rotationDamp = 10;
        CharacterController controller;

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            float speed = maxSpeed * Mathf.Clamp01(speedFraction);
            Vector3 forces = GetComponent<ForceReceiver>().GetTotalForce();

            controller.Move((destination + forces)* speed * Time.deltaTime);
        }

        public void LookAt(Vector3 target)
        {
            if(target == Vector3.zero) return;

            transform.rotation = Quaternion.Lerp
            (
                transform.rotation, Quaternion.LookRotation(target),
                Time.deltaTime * rotationDamp
            );
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
    }
}