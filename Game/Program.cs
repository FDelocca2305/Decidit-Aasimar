using System;
using Game.Core;
using Game.Scripts;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {

            int screenWidth = 1920;
            int screenHeight = 1080;
            float characterScale = 0.25f;
            int tileWidth = 32;
            int tileHeight = 32;
            float enemySpeed = 50f;
            int mapWidth = screenWidth / tileWidth;
            int mapHeight = screenHeight / tileHeight;
            Engine.Initialize("Plataformas", screenWidth, screenHeight, false);

            TileMap map = new TileMap(mapWidth, mapHeight, tileWidth, tileHeight);
            Character character = new Character(new Vector2(100, screenHeight - 350), screenWidth, screenHeight, characterScale);
            Enemy enemy = new Enemy(600, 100, character, enemySpeed);

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
                enemy.Update(deltaTime, map);  
                                               

                Engine.Draw("character.png", character.Position.x, character.Position.y, characterScale, characterScale);
                enemy.Draw();  

                Engine.Show();
            }
        }
    }
}
