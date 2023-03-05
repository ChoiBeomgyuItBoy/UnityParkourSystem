using ParkourSystem.Core;
using UnityEngine;

namespace ParkourSystem.StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] State initialState;

        State currentState = null;

        public Vector3 CamRelativeMovement(Vector2 inputValue)
        {
            CameraController cameraController = Camera.main.GetComponent<CameraController>();

            Vector3 moveInput = new Vector3(inputValue.x, 0, inputValue.y);

            return cameraController.GetPlanarRotation() * moveInput;
        }

        public void SwitchState(State newState)
        {
            currentState?.Exit(this);
            currentState = newState;
            currentState?.Enter(this);
        }

        private void Start()
        {
            SwitchState(initialState);
        }

        private void Update()
        {
            currentState?.Tick(this);
        }
    }   
}
