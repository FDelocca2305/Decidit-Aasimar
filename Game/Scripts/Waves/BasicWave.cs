using System;
using Game.Core;
using Game.Scripts.Utils;

namespace Game.Scripts.Waves
{
    public class BasicWave : Wave
    {
        public BasicWave(int currentWave, ObjectManager objectManager, Player player) 
            : base(currentWave, objectManager, player) { }

        public override void StartWave()
        {
            int enemyCount = CalculateEnemyCountForWave(ConfigLoader.WaveConfig.BasicWaveEnemyCount);
            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier);
            Console.WriteLine($"Basic Wave {currentWave} has {enemyCount} enemies.");
        }
    }
}