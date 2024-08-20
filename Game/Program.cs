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
            int tileWidth = 20;
            int tileHeight = 20;
            float enemySpeed = 50f;

            // Define dimensiones del mundo basadas en el tamaño del mapa
            int mapWidth = 100 * tileWidth;  
            int mapHeight = 50 * tileHeight; 


            Engine.Initialize("Plataformas", screenWidth, screenHeight, false);

            TileMap map = new TileMap(mapWidth / tileWidth, mapHeight / tileHeight, tileWidth, tileHeight);
            Character character = new Character(new Vector2(100, screenHeight - 350), screenWidth, screenHeight, characterScale);
            Enemy enemy = new Enemy(600, 100, character, enemySpeed);

            float zoom = 1.7f;  // Ajuste del zoom
            Camera camera = new Camera(screenWidth, screenHeight, mapWidth, mapHeight, zoom);

            DateTime startTime = DateTime.Now;
            float lastFrameTime = 0f;

            while (true)
            {
                float currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                float deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                Engine.Clear(135, 206, 235);

                camera.Follow(character);

                Vector2 characterScreenPos = camera.WorldToScreen(character.Position);
                Vector2 enemyScreenPos = camera.WorldToScreen(enemy.Position);

                // Llamada al método Draw del mapa con la cámara como parámetro
                map.Draw(camera);

                character.Update(deltaTime, map);
                enemy.Update(deltaTime, map);

                Engine.Draw("character.png", characterScreenPos.x, characterScreenPos.y, characterScale * camera.Zoom, characterScale * camera.Zoom);
                Engine.Draw("enemy.png", enemyScreenPos.x, enemyScreenPos.y, camera.Zoom, camera.Zoom);

                Engine.Show();
            }
        }
    }
}
