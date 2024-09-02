using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game
{
    public class GameEngine
    {
        private ObjectManager objectManager;
        private Player player;
        private UIManager uiManager;
        private GameTimer gameTimer;
        private WaveManager waveManager;
        private bool isRunning = true;

        public void Initialize()
        {
            Engine.Initialize("Vampire Franco", 1920, 1080);
            player = new Player(960f, 540f);
            objectManager = new ObjectManager(player);
            objectManager.Add(player);
            uiManager = new UIManager(player);
            gameTimer = new GameTimer();
            waveManager = new WaveManager(objectManager, player);
        }

        public void Run()
        {
            while (isRunning)
            {
                Engine.Clear(135, 206, 235);
                Engine.Draw("Assets/floor.png");

                float deltaTime = GetDeltaTime();
                gameTimer.Update(deltaTime);
                objectManager.UpdateAll(deltaTime, player);
                objectManager.RenderAll();
                waveManager.Update(deltaTime);
                uiManager.Render(gameTimer.GetFormattedTime());
                Engine.Show();

                if (Engine.GetKey(Keys.ESCAPE))
                    isRunning = false;
            }

            Engine.CloseWindow();
        }

        public static float GetDeltaTime()
        {
            return 1.0f / 60.0f;
        }

        public static bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(positionA.X - positionB.X);
            float distanceY = Math.Abs(positionA.Y - positionB.Y);

            float sumHalfWidths = sizeA.X / 2 + sizeB.Y / 2;
            float sumHalfHeight = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeight;
        }

    }
}
