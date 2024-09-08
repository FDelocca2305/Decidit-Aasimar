using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    internal class StateManager
    {
        private IGameState currentState;

        public void ChangeState(IGameState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }

        public void Update(float deltaTime)
        {
            if (currentState != null)
            {
                currentState.Update(deltaTime);
            }
        }

        public void Render()
        {
            if (currentState != null)
            {
                currentState.Render();
            }
        }
    }
}
