using Game.Core;
using System;

namespace Game.Scripts.Waves.Strategy
{
    public class BasicWaveStrategy : IWaveStrategy
    {
        public Wave GenerateWave(int currentWave, ObjectManager objectManager, Player player)
        {
            return new BasicWave(currentWave, objectManager, player);
        }
    }
}
