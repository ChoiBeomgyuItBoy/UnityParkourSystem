using UnityEngine;

namespace ParkourSystem.StateMachine
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}
