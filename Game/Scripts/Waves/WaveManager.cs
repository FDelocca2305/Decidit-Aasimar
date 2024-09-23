using Game.Core;
using System;
namespace Game.Scripts.Waves
{
    public class WaveManager
    {
        private ObjectManager objectManager;
        private Player player;
        private int currentWave = 1;
        private int basicWaveCount;
        private float timeBetweenWaves = 3f;
        private float waveTimer;
        private bool waveInProgress;

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
            
            WaveFactory.WaveType waveType;

            if (basicWaveCount < 2)
            {
                waveType = WaveFactory.WaveType.Basic;
                basicWaveCount++;
            }
            else
            {
                waveType = WaveFactory.WaveType.Special;
                basicWaveCount = 0;  
            }
            
            Wave wave = WaveFactory.CreateWave(waveType, currentWave, objectManager, player);
            wave.StartWave();
            Console.WriteLine($"Wave {currentWave} started. Wave Type: {waveType}");
            currentWave++;
        }
    }
}
