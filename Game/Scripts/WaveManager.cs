using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class WaveManager
    {
        private ObjectManager objectManager;
        private Player player;
        private int currentWave = 1;
        private float timeBetweenWaves = 3f;
        private float waveTimer = 0f;
        private bool waveInProgress = false;

        public WaveManager(ObjectManager objectManager, Player player)
        {
            this.objectManager = objectManager;
            this.player = player;
        }

        public void Update(float deltaTime)
        {
            if (!waveInProgress)
            {
                waveTimer += deltaTime;
                if (waveTimer >= timeBetweenWaves)
                {
                    StartNextWave();
                }
            }
            else
            {
                if (objectManager.AreAllEnemiesDefeated())
                {
                    waveInProgress = false;
                    waveTimer = 0f;
                }
            }
        }

        private void StartNextWave()
        {
            waveInProgress = true;
            int enemyCount = CalculateEnemyCountForWave();
            float difficultyMultiplier = 1.0f + (currentWave * 0.1f);

            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier);
            Console.WriteLine($"Horda {currentWave} iniciada con {enemyCount} enemigos");
            currentWave++;
        }

        private int CalculateEnemyCountForWave()
        {
            return currentWave * 5;
        }
    }
}
