using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class GameTimer
    {
        private float timeElapsed;

        public float TimeElapsedInMinutes => (int)timeElapsed / 60;

        public GameTimer()
        {
            timeElapsed = 0f;
        }

        public void Update(float deltaTime)
        {
            timeElapsed += deltaTime;
        }

        public string GetFormattedTime()
        {
            int totalSeconds = (int)timeElapsed;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }
    }
}
