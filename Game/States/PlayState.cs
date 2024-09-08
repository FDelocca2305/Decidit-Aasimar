using Game.Core;
using Game.Scripts;
using System;

namespace Game.States
{
    public class PlayState : IGameState
    {
        private Player player;
        private ObjectManager objectManager;
        private GameTimer gameTimer;

        public void Enter()
        {
            Console.WriteLine("Comenzando el Juego");
            player = new Player(960f, 540f);
            objectManager = new ObjectManager(player);
            gameTimer = new GameTimer();
        }

        public void Update(float deltaTime)
        {
            player.Update(deltaTime);
            objectManager.UpdateAll(deltaTime, player);

            if (player.Health <= 0)
            {
                GameManager.Instance.ChangeState(new GameOverState());
            }

            if (gameTimer.TimeElapsedInMinutes == 5f)
            {
                GameManager.Instance.ChangeState(new VictoryState());
            }
        }

        public void Render()
        {
            player.Render();
            objectManager.RenderAll();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Juego");
        }
    }
}
