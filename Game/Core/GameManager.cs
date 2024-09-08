using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class GameManager
    {
        private static GameManager instance;
        private StateManager stateManager;

        private GameManager()
        {
            stateManager = new StateManager();
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void ChangeState(IGameState newState)
        {
            stateManager.ChangeState(newState);
        }

        public void Update(float deltaTime)
        {
            stateManager.Update(deltaTime);
        }

        public void Render()
        {
            stateManager.Render();
        }
    }
}
