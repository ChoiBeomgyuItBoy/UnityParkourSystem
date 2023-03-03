using System.Collections.Generic;
using UnityEngine;

namespace ParkourSystem.StateMachine
{
    [System.Serializable]
    public class StateTransition 
    {
        [SerializeField] StateDecision[] decisions;
        [SerializeField] State trueState;

        public IEnumerable<StateDecision> GetDecisions()
        {
            return decisions;
        }

        public State GetTrueState()
        {
            return trueState;
        }
    }
}
