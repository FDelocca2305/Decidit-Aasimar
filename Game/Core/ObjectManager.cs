using Game.Scripts;
using Game.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class ObjectManager
    {
        private Random random = new Random();

        private GameObjectsManager gameObjectsManager = new GameObjectsManager();
        private CollisionManager collisionManager = new CollisionManager();

        private ObjectPool<BasicEnemy> basicEnemyPool;
        private ObjectPool<FastEnemy> fastEnemyPool;

        public GameObjectsManager GameObjectsManager { get { return gameObjectsManager; } }

        public ObjectManager(Player player)
        {
            basicEnemyPool = new ObjectPool<BasicEnemy>(
                objectFactory: () => new BasicEnemy(),
                resetAction: (enemy) => enemy.Reset(GenerateSpawnPosition(), 10, 20, player)
            );

            fastEnemyPool = new ObjectPool<FastEnemy>(
                objectFactory: () => new FastEnemy(),
                resetAction: (enemy) => enemy.Reset(GenerateSpawnPosition(), 5, 40, player)
            );
        }

        public Enemy GetEnemy(EnemyFactory.EnemyType type, Vector2 position, Player player, float difficultyMultiplier)
        {
            Enemy enemy = null;

            switch (type)
            {
                case EnemyFactory.EnemyType.Basic:
                    enemy = basicEnemyPool.Get();
                    enemy.Reset(position, 10 * difficultyMultiplier, 20 * difficultyMultiplier, player);
                    break;

                case EnemyFactory.EnemyType.Fast:
                    enemy = fastEnemyPool.Get();
                    enemy.Reset(position, 5 * difficultyMultiplier, 40 * difficultyMultiplier, player);
                    break;
            }

            return enemy;
        }

        public void ReleaseEnemy(Enemy enemy)
        {
            if (enemy is BasicEnemy basicEnemy)
            {
                basicEnemyPool.Release(basicEnemy);
            }
            else if (enemy is FastEnemy fastEnemy)
            {
                fastEnemyPool.Release(fastEnemy);
            }
        }

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

            foreach (var obj in gameObjectsManager.GetAllGameObjects())
            {
                if (obj is Enemy enemy && !enemy.IsActive)
                {
                    ReleaseEnemy(enemy);
                }
            }

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
            EnemyFactory enemyFactory = new EnemyFactory(this);

            for (int i = 0; i < count; i++)
            {
                Vector2 spawnPosition = GenerateSpawnPosition();
                EnemyFactory.EnemyType randomType = (EnemyFactory.EnemyType)random.Next(0, Enum.GetNames(typeof(EnemyFactory.EnemyType)).Length);

                Enemy enemy = enemyFactory.CreateEnemy(randomType, spawnPosition, player, difficultyMultiplier);

                if (enemy != null)
                {
                    Add(enemy);
                }
            }
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            gameObjectsManager.Remove(enemy);
            SpawnExperienceOrbs(enemy.Transform.Position.X, enemy.Transform.Position.Y);
            ReleaseEnemy(enemy);
        }

        private Vector2 GenerateSpawnPosition()
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
