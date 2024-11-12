using Game.Core;
using Game.Scripts.Waves.Strategy;
using System;
namespace Game.Scripts.Waves
{
    public class WaveManager
    {
        private ObjectManager objectManager;
        private Player player;
        private int currentWave = 1;
        private int basicWaveCount = 2;
        private float timeBetweenWaves = 3f;
        private float waveTimer;
        private bool waveInProgress;
        private IWaveStrategy waveStrategy;

        public WaveManager(ObjectManager objectManager, Player player)
        {
            this.objectManager = objectManager;
            this.player = player;
            this.waveStrategy = new BasicWaveStrategy();
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
            SelectWaveStrategy();
            //WaveFactory.WaveType waveType;

            //if (basicWaveCount < 2)
            //{
            //    waveType = WaveFactory.WaveType.Basic;
            //    basicWaveCount++;
            //}
            //else
            //{
            //    waveType = WaveFactory.WaveType.Special;
            //    basicWaveCount = 0;  
            //}

            //Wave wave = WaveFactory.CreateWave(waveType, currentWave, objectManager, player);
            //wave.StartWave();
            //Console.WriteLine($"Wave {currentWave} started. Wave Type: {waveType}");
            //currentWave++;

            Wave wave = waveStrategy.GenerateWave(currentWave, objectManager, player);
            wave.StartWave();

            Console.WriteLine($"Wave {currentWave} started. Wave Type: {wave.GetType().Name}");
            currentWave++;
        }

        private void SelectWaveStrategy()
        {
            if (basicWaveCount < 2)
            {
                waveStrategy = new BasicWaveStrategy();
                basicWaveCount++;
            }
            else
            {
                waveStrategy = new SpecialWaveStrategy();
                basicWaveCount = 0;
            }
        }
    }
}
