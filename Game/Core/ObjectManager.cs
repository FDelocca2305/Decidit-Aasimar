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
        private List<GameObject> gameObjects = new List<GameObject>();
        private Random random = new Random();
        private List<ExperienceOrb> experienceOrbs = new List<ExperienceOrb>();

        private List<GameObject> objectsToRemove = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();

        private Player player;

        public ObjectManager(Player player)
        {
            this.player = player;
        }

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);

            if (gameObject is Enemy enemy)
            {
                enemy.OnDeath += HandleEnemyDeath;
            }

            if (gameObject is ExperienceOrb orb)
            {
                experienceOrbs.Add(orb);
            }

        }

        public void AddAll()
        {
            objectsToAdd.ForEach(obt =>
            {
                Add(obt);
            });
        }

        public void UpdateAll(float deltaTime, Player player)
        {
            AddAll();
            foreach (var obj in gameObjects)
            {
                if (obj.IsActive)
                    obj.Update(deltaTime);
                else
                    objectsToRemove.Add(obj);
            }

            foreach (var orb in experienceOrbs)
            {
                if (orb.IsActive && orb.CheckCollision(player))
                {
                    player.CollectExperience(orb);
                    orb.IsActive = false;
                    objectsToRemove.Add(orb);
                }
            }

            foreach (var obj in gameObjects)
            {
                if (obj.IsActive && obj is Enemy enemy)
                {
                    if (GameEngine.IsBoxColliding(player.Position, new Vector2(30, 44), enemy.Position, new Vector2(48, 60)))
                    {
                        player.TakeDamage(10);
                    }

                    if (player.IsAttacking && GameEngine.IsBoxColliding(player.Position, new Vector2(100, 50), enemy.Position, new Vector2(48,60)))
                    {
                        enemy.TakeDamage(player.Attack);
                    }
                }
            }

            foreach (var obj in objectsToRemove)
            {
                gameObjects.Remove(obj);
                if (obj is ExperienceOrb orb)
                {
                    experienceOrbs.Remove(orb);
                }
            }
            objectsToRemove.Clear();
        }

        public void SpawnExperienceOrbs(float x, float y)
        {
            var orb = new ExperienceOrb(x, y, 10);
            objectsToAdd.Add(orb);
        }

        public void RenderAll()
        {
            foreach (var obj in gameObjects)
            {
                if (obj.IsActive)
                    obj.Render();
            }
        }

        public void SpawnEnemies(int count, Player player, float difficultyMultiplier)
        {
            Random random = new Random();
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
            objectsToRemove.Add(enemy);
            SpawnExperienceOrbs(enemy.Position.X, enemy.Position.Y);
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

            if (Math.Abs(x - player.Position.X) < 200 && Math.Abs(y - player.Position.Y) < 200)
            {
                return GenerateSpawnPosition(player);
            }

            return new Vector2(x, y);
        }

        public bool AreAllEnemiesDefeated()
        {
            foreach (var obj in gameObjects)
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
