using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> states;
        private IState activeState;
        public GameStateMachine()
        {
            states = new Dictionary<Type, IState>();
        }
        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            activeState = states[typeof(TState)];
            activeState.Enter();
        }
    }
}