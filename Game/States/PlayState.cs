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
        private WaveManager waveManager;
        private UIManager uiManager;

        public void Enter()
        {
            Console.WriteLine("Comenzando el Juego");
            player = new Player(960f, 540f);
            objectManager = new ObjectManager();
            objectManager.Add(player);
            gameTimer = new GameTimer();
            waveManager = new WaveManager(objectManager, player);
            uiManager = new UIManager(player);
        }

        public void Update(float deltaTime)
        {
            gameTimer.Update(deltaTime);
            objectManager.UpdateAll(deltaTime, player);
            
            waveManager.Update(deltaTime);

            if (player.Health <= 0)
            {
                GameManager.Instance.ChangeState(new GameOverState());
            }

            if (gameTimer.TimeElapsedInMinutes == 1f)
            {
                GameManager.Instance.ChangeState(new VictoryState());
            }

            if (Engine.GetKey(Keys.ESCAPE))
                GameManager.Instance.ChangeState(new MainMenuState());
        }

        public void Render()
        {
            Engine.Clear(0, 0, 0);
            Engine.Draw("Assets/floor.png");
            objectManager.RenderAll();
            uiManager.Render(gameTimer.GetFormattedTime());
            Engine.Show();
        }

        public void Exit()
        {
            Console.WriteLine("Saliendo del Juego");
        }

        
    }
}
