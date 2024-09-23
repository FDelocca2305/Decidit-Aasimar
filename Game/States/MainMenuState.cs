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

        public void Enter()
        {
            Console.WriteLine("Entrando al Menú Principal");
            AudioManager.Instance.PlayTrack("Main", true);
        }

        public void Update(float deltaTime)
        {
            if (Engine.GetKey(Keys.RETURN))
            {
                GameManager.Instance.ChangeState(new PlayState());
            }

            if (Engine.GetKey(Keys.ESCAPE))
            {
                GameManager.Instance.ExitGame();
                Engine.CloseWindow();
            }
                
        }

        public void Render()
        {
            Engine.Clear(0, 0, 0);
            TextManager.Instance.DrawText("presiona enter para continuar", 420, 700, 2f);
            TextManager.Instance.DrawText("presiona escape para salir", 420, 750, 2f);
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Menú Principal");
        }
    }
}
