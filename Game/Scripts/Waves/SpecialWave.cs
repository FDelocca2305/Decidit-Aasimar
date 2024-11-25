using System;
using Game.Core;
using Game.Scripts.Enemies;
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
            Console.WriteLine("enemy count" + enemyCount);
            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier, EnemyFactory.EnemyType.Fast);
            Console.WriteLine($"Special Wave {currentWave} has {enemyCount} enemies.");
        }
    }
}