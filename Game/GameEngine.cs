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
        public static bool isRunning = true;
        private static DateTime lastFrameTime = DateTime.UtcNow;

        public void Run(Game game)
        {
            while (isRunning)
            {
                GameManager.Instance.Update(GetDeltaTime());
                GameManager.Instance.Render();
                
                Engine.Show();
            }
        }

        public static float GetDeltaTime()
        {
            DateTime currentFrameTime = DateTime.UtcNow;
            float deltaTime = (float)(currentFrameTime - lastFrameTime).TotalSeconds;
            lastFrameTime = currentFrameTime;
            return deltaTime;
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
