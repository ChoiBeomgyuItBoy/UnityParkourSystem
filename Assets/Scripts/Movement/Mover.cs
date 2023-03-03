using System;
using UnityEngine;

namespace ParkourSystem.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 6;
        [SerializeField] float rotationDamp = 10;
        CharacterController controller;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            GetComponent<Animator>().SetFloat("movementSpeed", GetLocalVelocity(), 0.1f, Time.deltaTime);
        }

        float GetLocalVelocity()
        {
            return transform.InverseTransformDirection(controller.velocity).magnitude;
        }

        public void MoveTo(Vector3 motion, float speedFraction)
        {
            float speed = maxSpeed * Mathf.Clamp01(speedFraction);
            controller.Move(motion * speed * Time.deltaTime);
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
    }
}