using UnityEngine;
using UnityEngine.InputSystem;

namespace ParkourSystem.Input
{
    [CreateAssetMenu(menuName = "Input/Player Input")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        Controls controls;

        Vector2 movementValue;
        Vector2 mouseValue;

        bool jumped = false;

        public Vector2 GetMovementValue()
        {
            return movementValue;
        }

        public Vector2 GetMouseValue()
        {
            return mouseValue;
        }

        public bool Jumped()
        {
            return jumped;
        }

        void OnEnable()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
        }

        void OnDisable()
        {   
            controls.Player.Disable();
        }

        void Controls.IPlayerActions.OnLocomotion(InputAction.CallbackContext context)
        {
            movementValue = context.ReadValue<Vector2>();
        }

        void Controls.IPlayerActions.OnLook(InputAction.CallbackContext context)
        {
            mouseValue = context.ReadValue<Vector2>();
        }

        void Controls.IPlayerActions.OnJump(InputAction.CallbackContext context)
        {
            jumped = context.performed;
        }
    }
}

