using UnityEngine;

namespace ParkourSystem.StateMachine
{
    public abstract class StateDecision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}