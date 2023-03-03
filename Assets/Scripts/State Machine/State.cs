using System.Collections.Generic;
using UnityEngine;

namespace ParkourSystem.StateMachine
{
    [CreateAssetMenu(menuName = "State Machine/New State")]
    public class State : ScriptableObject
    {
        [SerializeField] StateAction[] onEnterActions;
        [SerializeField] StateAction[] onTickActions;
        [SerializeField] StateAction[] onExitActions;
        [SerializeField] StateTransition[] transitions;

        public void Enter(StateController controller)
        {
            DoActions(controller, onEnterActions);
        }

        public void Tick(StateController controller)
        {
            DoActions(controller, onTickActions);
            CheckTransitions(controller);
        }

        public void Exit(StateController controller)
        {
            DoActions(controller, onExitActions);
        }

        private void DoActions(StateController controller, StateAction[] actions)
        {
            if(actions.Length == 0) return;

            foreach(var action in actions)
            {
                action.Act(controller);
            }
        }

        private void CheckTransitions(StateController controller)
        {
            if(transitions.Length == 0) return;

            foreach(var transition in transitions)
            {
                bool succeded = CheckDecisions(controller, transition.GetDecisions());

                if(succeded)
                {
                    controller.SwitchState(transition.GetTrueState());
                }
            }
        }

        private bool CheckDecisions(StateController controller, IEnumerable<StateDecision> decisions)
        {
            foreach(var decision in decisions)
            {
                if(!decision.Decide(controller))
                {
                    return false;
                }
            }

            return true;
        }
    }
}