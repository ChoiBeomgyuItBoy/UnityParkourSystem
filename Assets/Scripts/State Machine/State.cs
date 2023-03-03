using UnityEngine;

namespace ParkourSystem.StateMachine
{
    [CreateAssetMenu(menuName = "State Machine/New State")]
    public class State : ScriptableObject
    {
        [SerializeField] StateAction[] onEnterActions;
        [SerializeField] StateAction[] onTickActions;
        [SerializeField] StateAction[] onExitActions;

        public void Enter(StateController controller)
        {
            DoActions(controller, onEnterActions);
        }

        public void Tick(StateController controller)
        {
            DoActions(controller, onTickActions);
        }

        public void Exit(StateController controller)
        {
            DoActions(controller, onExitActions);
        }

        private void DoActions(StateController controller, StateAction[] actions)
        {
            foreach(var action in actions)
            {
                action.Act(controller);
            }
        }
    }
}