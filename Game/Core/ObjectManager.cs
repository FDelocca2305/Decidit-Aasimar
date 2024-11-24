using Game.Scripts;
using Game.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.Utils;

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
                resetAction: (enemy) => enemy.Reset(GenerateSpawnPosition(), ConfigLoader.EnemyConfig.BasicEnemy.Health, ConfigLoader.EnemyConfig.BasicEnemy.Speed, player),
                GetInitialPoolCapacity(new BasicEnemy())
            );

            fastEnemyPool = new ObjectPool<FastEnemy>(
                objectFactory: () => new FastEnemy(),
                resetAction: (enemy) => enemy.Reset(GenerateSpawnPosition(),  ConfigLoader.EnemyConfig.FastEnemy.Health, ConfigLoader.EnemyConfig.FastEnemy.Speed, player),
                GetInitialPoolCapacity(new FastEnemy())
            );
        }

        public Enemy GetEnemy(EnemyFactory.EnemyType type, Vector2 position, Player player, float difficultyMultiplier)
        {
            Enemy enemy = null;

            switch (type)
            {
                case EnemyFactory.EnemyType.Basic:
                    enemy = basicEnemyPool.Get();
                    enemy.Reset(position, ConfigLoader.EnemyConfig.BasicEnemy.Health * difficultyMultiplier, ConfigLoader.EnemyConfig.BasicEnemy.Speed, player);
                    break;

                case EnemyFactory.EnemyType.Fast:
                    enemy = fastEnemyPool.Get();
                    enemy.Reset(position,  ConfigLoader.EnemyConfig.FastEnemy.Health * difficultyMultiplier, ConfigLoader.EnemyConfig.FastEnemy.Speed, player);
                    break;
            }

            return enemy;
        }

        public void ReleaseEnemy(Enemy enemy)
        {
            Console.WriteLine($"Releasing Enemy: {enemy.GetType().Name}, Speed={enemy.Speed}");
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

            var activeObjects = gameObjectsManager.GetAllGameObjects().Where(obj => obj.IsActive);
            collisionManager.CheckCollisions(player, activeObjects);
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
            enemy.IsActive = false;
            gameObjectsManager.Remove(enemy);
            SpawnExperienceOrbs(enemy.Transform.Position.X, enemy.Transform.Position.Y);
            ReleaseEnemy(enemy);
        }

        private Vector2 GenerateSpawnPosition()
        {
            int screenWidth = GlobalConstants.GraphicsConstants.ScreenWidth;
            int screenHeight = GlobalConstants.GraphicsConstants.ScreenHeight;

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
            return !gameObjectsManager.GetAllGameObjects()
                .Any(obj => obj.IsActive && obj is Enemy);
        }

        private int GetInitialPoolCapacity(Enemy enemy)
        {
            float totalWaves = ((ConfigLoader.WaveConfig.TimeUntilGameEnds * 60) - ConfigLoader.WaveConfig.TimeUntilFirstWave) / ConfigLoader.WaveConfig.TimeBetweenWaves;
            Console.WriteLine((int)totalWaves * ConfigLoader.WaveConfig.BasicWaveEnemyCount);
            Console.WriteLine((int)totalWaves * ConfigLoader.WaveConfig.SpecialWaveEnemyCount);
            if (enemy is BasicEnemy)
            {
                return (int)totalWaves * ConfigLoader.WaveConfig.BasicWaveEnemyCount;
            }
            else if (enemy is FastEnemy)
            {
                return (int)totalWaves * ConfigLoader.WaveConfig.SpecialWaveEnemyCount; ;
            }
            else
            {
                return 200;
            }
        }
    }

}
