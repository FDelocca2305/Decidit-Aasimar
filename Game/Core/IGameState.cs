using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public interface IGameState
    {
        void Enter();
        void Update(float deltaTime);
        void Render();
        void Exit();
    }
}
