using Game.Core;
using System;

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
            Console.WriteLine("PANTALLA DE DERROTA");
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
            Engine.Clear(0, 0, 0);
            Engine.Draw("Assets/LoseScreen.png");
            textManager.DrawText("game over", 740, 800, 2f);
            textManager.DrawText("presionar enter para volver al menu principal", 550, 850, 1f);
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo de la Pantalla de Derrota");
        }
    }
}
