using System;

namespace Game.Core
{
    public class Timer
    {
        private float interval;
        private float elapsedTime;

        public Timer(float intervalInSeconds)
        {
            interval = intervalInSeconds;
            elapsedTime = 0f;
        }

        public void Update(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= interval)
            {
                Console.WriteLine($"{interval} segundos han pasado.");
                elapsedTime = 0f; // Reinicia el temporizador
            }
        }
    }
}