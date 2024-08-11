using System;
using System.Collections.Generic;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Initialize("Mi Juego", 1920, 1080, false);

            Timer timer = new Timer(3f);
            Character character = new Character(0f, 100f, 4f);

            DateTime startTime = DateTime.Now;
            float lastFrameTime = 0f;

            while (true)
            {
                var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                float deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Engine.Clear();

                // Dibujar el background
                Engine.Draw("background.png", 0, 0);

                // Actualizar el personaje y el temporizador
                character.Update(deltaTime);
                timer.Update(deltaTime);

                // Dibujar el personaje en su nueva posición
                float scaX = 0.5f;
                float scaY = 0.5f;
                Engine.Draw("character.png", character.PositionX, 100, scaX, scaY);

                Engine.Show();

                // Mostrar posición en la consola
                Console.WriteLine($"Posición del personaje: {character.PositionX}");
            }
        }
    }
}