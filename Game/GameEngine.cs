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
        private UIManager uiManager;
        
        private bool isRunning = true;

        public void Run(Game game)
        {
            while (isRunning)
            {
                Engine.Clear(135, 206, 235);
                GameManager.Instance.Update(GetDeltaTime());
                GameManager.Instance.Render();
                
                Engine.Show();
            }
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
