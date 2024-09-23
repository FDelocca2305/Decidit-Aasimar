using Game.Core;
using System;


namespace Game.States
{
    public class VictoryState : IGameState
    {
        private TextManager textManager;

        public VictoryState()
        {
            textManager = TextManager.Instance;
        }

        public void Enter()
        {
            Console.WriteLine("Pantalla de Victoria");
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
            Engine.Draw("Assets/WinScreen.png");
            textManager.DrawText("ganaste", 740, 800, 2f);
            textManager.DrawText("presionar enter para volver al menu principal", 550, 850, 1f);
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo de la Pantalla de Victoria");
        }
    }
}
