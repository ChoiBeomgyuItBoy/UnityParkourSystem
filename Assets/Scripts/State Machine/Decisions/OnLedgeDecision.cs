using ParkourSystem.Core;
using ParkourSystem.Input;
using UnityEngine;

namespace ParkourSystem.StateMachine.Decisions
{
    [CreateAssetMenu(menuName = "State Machine/Decisions/On Ledge Decision")]
    public class OnLedgeDecision : StateDecision
    {
        [SerializeField] PlayerInput playerInput;

        public override bool Decide(StateController controller)
        {
            return controller.GetComponent<EnvironmentScanner>().CheckLedge(CalculateMovement());
        }

        Vector3 CalculateMovement()
        {
            Vector2 inputValue = playerInput.GetMovementValue();
            CameraController cameraController = Camera.main.GetComponent<CameraController>();

            Vector3 moveInput = new Vector3(inputValue.x, 0, inputValue.y);

            return cameraController.GetPlanarRotation() * moveInput;
        }
    }
}
