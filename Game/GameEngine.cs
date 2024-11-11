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

        public void Run()
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
    }
}
