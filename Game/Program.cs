using System;
using Game.Core;
using Game.Scripts;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenWidth = 800;
            int screenHeight = 600;
            float characterScale = 0.25f;
            Engine.Initialize("Plataformas", screenWidth, screenHeight, false);

            TileMap map = new TileMap(25, 18, 32, 32);
            Character character = new Character(new Vector2(100, 300), screenWidth, screenHeight, characterScale);

            DateTime startTime = DateTime.Now;
            float lastFrameTime = 0f;

            while (true)
            {
                float currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                float deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Engine.Clear(135, 206, 235);

                map.Draw();
                character.Update(deltaTime, map);

                Engine.Draw("character.png", character.Position.x, character.Position.y, characterScale, characterScale);
                Engine.Show();
            }
        }
    }
}