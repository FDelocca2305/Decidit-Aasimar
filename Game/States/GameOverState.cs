using Game.Core;
using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.States
{
    public class GameOverState : IGameState
    {
        private TextManager textManager;

        public GameOverState()
        {
            textManager = TextManager.Instance;
        }

        public void Enter()
        {
            Console.WriteLine("Jugador Perdió - Pantalla de Derrota");
        }

        public void Update(float deltaTime)
        {
            if (Engine.GetKey(Keys.RETURN))
            {
                GameManager.Instance.ChangeState(new MainMenuState());
            }
        }

        public void Render()
        {
            textManager.DrawText("GAME OVER", 100, 100, 2f);
            textManager.DrawText("PRESIONA ENTER PARA VOLVER AL MENU", 100, 150, 1f);
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo de la Pantalla de Derrota");
        }
    }
}
