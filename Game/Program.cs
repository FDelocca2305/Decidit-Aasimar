using System;
using Game.Core;
using Game.Scripts;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Initialize("Mi Juego", 1920, 1080, false);

            Timer timer = new Timer(3f);
            Character character = new Character(new Vector2(0f, 100f), 100f, 4f);

            DateTime startTime = DateTime.Now;
            float lastFrameTime = 0f;

            while (true)
            {
                var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                float deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Engine.Clear();
                
                Engine.Draw("background.png", 0, 0);
                
                character.Update(deltaTime);
                timer.Update(deltaTime);
                
                float scaX = 0.5f;
                float scaY = 0.5f;
                Engine.Draw("character.png", character.Position.x, character.Position.y, scaX, scaY);

                Engine.Show();
                
                Console.WriteLine($"Posición del personaje: {character.Position}");
            }
        }
    }
}