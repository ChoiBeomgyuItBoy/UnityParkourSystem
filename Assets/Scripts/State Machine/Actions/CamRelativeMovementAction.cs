using ParkourSystem.Core;
using ParkourSystem.Input;
using ParkourSystem.Movement;
using UnityEngine;
using static ParkourSystem.Core.EnvironmentScanner;

namespace ParkourSystem.StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Cam Relative Movement Action")]
    public class CamRelativeMovementAction : StateAction
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] [Range(0,1)] float speedFraction = 0.8f;

        public override void Act(StateController controller)
        {
            Mover mover = controller.GetComponent<Mover>();
            EnvironmentScanner scanner = controller.GetComponent<EnvironmentScanner>();
            Vector3 moveDirection = controller.CamRelativeMovement(playerInput.GetMovementValue());

            bool onLedge = scanner.LedgeCheck(moveDirection, out var ledgeData);

            if(onLedge)
            {
                float signedAngle = Vector3.SignedAngle(ledgeData.surfaceHit.normal, moveDirection, Vector3.up);
                float angle = Mathf.Abs(signedAngle);

                if(Vector3.Angle(moveDirection, controller.transform.forward) >= 80)
                {
                    mover.LookAt(moveDirection);
                    return;
                }
    
                if(angle < 60)
                {
                    mover.MoveTo(Vector3.zero, 0);
                }
                else if(angle < 90)
                {
                    mover.MoveTo(LedgeMovement(ledgeData.surfaceHit.normal, signedAngle), speedFraction);
                    mover.LookAt(moveDirection);
                }
            }
            else
            {
                mover.MoveTo(moveDirection, speedFraction);
                mover.LookAt(moveDirection);
            }
        }

        Vector3 LedgeMovement(Vector3 ledgeNormal, float signedAngle)
        {
            Vector3 left = Vector3.Cross(Vector3.up, ledgeNormal);

            Vector3 direction = left * Mathf.Sign(signedAngle);

            return direction;
        }
    }
}
