using System;
using Game.Core;

namespace Game.Scripts.Waves
{
    public class BasicWave : Wave
    {
        public BasicWave(int currentWave, ObjectManager objectManager, Player player) 
            : base(currentWave, objectManager, player) { }

        public override void StartWave()
        {
            int enemyCount = CalculateEnemyCountForWave(5);
            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier);
            Console.WriteLine($"Basic Wave {currentWave} has {enemyCount} enemies.");
        }
    }
}