using UnityEngine;

namespace ParkourSystem.StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] State defaultState;

        State currentState = null;

        public void SwitchState(State newState)
        {
            currentState?.Exit(this);
            currentState = newState;
            currentState?.Enter(this);
        }

        private void Start()
        {
            SwitchState(defaultState);
        }

        private void Update()
        {
            currentState?.Tick(this);
        }
    }   
}
