using Game.Core;

namespace Game.Scripts.Waves.Strategy
{
    public class SpecialWaveStrategy : IWaveStrategy
    {
        public Wave GenerateWave(int currentWave, ObjectManager objectManager, Player player)
        {
            return new SpecialWave(currentWave, objectManager, player);
        }
    }
}
