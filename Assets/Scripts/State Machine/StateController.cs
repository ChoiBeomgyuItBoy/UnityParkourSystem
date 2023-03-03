using UnityEngine;

namespace ParkourSystem.StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] State initialState;

        State currentState = null;

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
