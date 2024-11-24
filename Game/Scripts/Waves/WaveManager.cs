using Game.Core;
using Game.Scripts.Waves.Strategy;
using System;
using Game.Scripts.Utils;

namespace Game.Scripts.Waves
{
    public class WaveManager
    {
        //Move this to a constantD
        private ObjectManager objectManager;
        private Player player;
        private int currentWave = 1;
        private int basicWaveCount = 0;
        private float timeBetweenWaves = ConfigLoader.WaveConfig.TimeBetweenWaves;
        private int basicWavesBeforeSpecial = ConfigLoader.WaveConfig.BasicWavesBeforeSpecial;
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
            waveTimer += deltaTime;

            float currentInterval = CalculateTimeBetweenWaves();

            if (waveTimer >= currentInterval)
            {
                StartNextWave();
                waveTimer = 0f;
            }
        }

        private float CalculateTimeBetweenWaves()
        {
            float baseInterval = ConfigLoader.WaveConfig.TimeBetweenWaves;
            float scalingFactor = 1f;
            return baseInterval + (currentWave - 1) * scalingFactor;
        }

        private void StartNextWave()
        {
            waveInProgress = true;
            SelectWaveStrategy();

            Wave wave = waveStrategy.GenerateWave(currentWave, objectManager, player);
            wave.StartWave();

            Console.WriteLine($"Wave {currentWave} started. Wave Type: {wave.GetType().Name}");
            currentWave++;
        }

        private void SelectWaveStrategy()
        {
            if (basicWaveCount < basicWavesBeforeSpecial)
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
