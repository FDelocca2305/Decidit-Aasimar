using System;
using Game.Core;
using Game.Scripts.Utils;

namespace Game.Scripts.Waves
{
    public class SpecialWave : Wave
    {
        public SpecialWave(int currentWave, ObjectManager objectManager, Player player)
            : base(currentWave, objectManager, player) { }

        public override void StartWave()
        {
            int enemyCount = CalculateEnemyCountForWave(ConfigLoader.WaveConfig.SpecialWaveEnemyCount);
            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier);
            Console.WriteLine($"Special Wave {currentWave} has {enemyCount} enemies.");
        }
    }
}