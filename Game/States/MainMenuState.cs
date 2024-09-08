using Game.Core;
using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.States
{
    public class MainMenuState : IGameState
    {
        private TextManager textManager;

        public MainMenuState()
        {
            textManager = TextManager.Instance;
        }

        public void Enter()
        {
            Console.WriteLine("Entrando al Menú Principal");
        }

        public void Update(float deltaTime)
        {
            if (Engine.GetKey(Keys.RETURN))
            {
                GameManager.Instance.ChangeState(new PlayState());
            }

            if (Engine.GetKey(Keys.ESCAPE)) 
                Engine.CloseWindow();
        }

        public void Render()
        {
            textManager.DrawText("PRESIONA ENTER PARA JUGAR", 100, 100, 1f);
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Menú Principal");
        }
    }
}
