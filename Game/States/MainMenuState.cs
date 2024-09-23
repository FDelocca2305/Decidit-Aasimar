using Game.Core;
using System;

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
            Engine.Draw("Assets/MainScreen.png");
            TextManager.Instance.DrawText("presiona enter para continuar", 900, 555, 1.6f);
            TextManager.Instance.DrawText("presiona escape para salir", 930, 820, 1.6f);
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Menú Principal");
        }
    }
}
