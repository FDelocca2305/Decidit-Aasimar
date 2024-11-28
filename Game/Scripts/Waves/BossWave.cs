using Game.Core;
using Game.Scripts.Enemies;
using Game.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts.Waves
{
    public class BossWave : Wave
    {
        public BossWave(int currentWave, ObjectManager objectManager, Player player)
            : base(currentWave, objectManager, player) { }

        public override void StartWave()
        {
            int enemyCount = CalculateEnemyCountForWave(ConfigLoader.WaveConfig.BasicWaveEnemyCount);
            Console.WriteLine("enemy count" + enemyCount);
            objectManager.SpawnEnemies(enemyCount, player, difficultyMultiplier, EnemyFactory.EnemyType.Basic);
            Console.WriteLine($"Basic Wave {currentWave} has {enemyCount} enemies.");
        }
    }
}
