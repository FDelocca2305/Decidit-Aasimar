using Game.Scripts;
using Game.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class ObjectManager
    {
        private Random random = new Random();

        private GameObjectsManager gameObjectsManager = new GameObjectsManager();
        private CollisionManager collisionManager = new CollisionManager();

        public void Add(GameObject gameObject)
        {
            gameObjectsManager.Add(gameObject);

            if (gameObject is Enemy enemy)
            {
                enemy.OnDeath += HandleEnemyDeath;
            }
        }

        public void UpdateAll(float deltaTime, Player player)
        {
            gameObjectsManager.UpdateAll(deltaTime);
            collisionManager.CheckCollisions(player, gameObjectsManager.GetAllGameObjects());
        }

        public void RenderAll()
        {
            gameObjectsManager.RenderAll();
        }

        public void SpawnExperienceOrbs(float x, float y, int amount = 10)
        {
            var orb = new ExperienceOrb(x, y, amount);
            gameObjectsManager.Add(orb);
        }

        public void SpawnEnemies(int count, Player player, float difficultyMultiplier)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 spawnPosition = GenerateSpawnPosition(player);
                EnemyFactory.EnemyType randomType = (EnemyFactory.EnemyType)random.Next(0, Enum.GetNames(typeof(EnemyFactory.EnemyType)).Length);

                var enemy = EnemyFactory.CreateEnemy(randomType, spawnPosition.X, spawnPosition.Y, player, difficultyMultiplier);
                Add(enemy);
            }
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            gameObjectsManager.Remove(enemy);
            SpawnExperienceOrbs(enemy.Transform.Position.X, enemy.Transform.Position.Y);
        }

        private Vector2 GenerateSpawnPosition(Player player)
        {
            int screenWidth = 1920;
            int screenHeight = 1080;

            int region = random.Next(0, 4);
            float x = 0, y = 0;

            switch (region)
            {
                case 0:
                    x = -random.Next(50, 200);
                    y = random.Next(0, screenHeight);
                    break;

                case 1:
                    x = screenWidth + random.Next(50, 200);
                    y = random.Next(0, screenHeight);
                    break;

                case 2:
                    x = random.Next(0, screenWidth);
                    y = -random.Next(50, 200);
                    break;

                case 3:
                    x = random.Next(0, screenWidth);
                    y = screenHeight + random.Next(50, 200);
                    break;
            }

            if (Math.Abs(x - player.Transform.Position.X) < 200 && Math.Abs(y - player.Transform.Position.Y) < 200)
            {
                return GenerateSpawnPosition(player);
            }

            return new Vector2(x, y);
        }

        public bool AreAllEnemiesDefeated()
        {
            foreach (var obj in gameObjectsManager.GetAllGameObjects())
            {
                if (obj.IsActive && obj is Enemy)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
