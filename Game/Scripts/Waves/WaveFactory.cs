using System;
using System.Security.AccessControl;
using Game.Core;

namespace Game.Scripts.Waves
{
    public class WaveFactory
    {
        public enum WaveType
        {
            Basic,
            Special
        }

        public static Wave CreateWave(WaveType type, int currentWave, ObjectManager objectManager, Player player)
        {
            Wave wave = null;
            
            switch (type)
            {
                case WaveType.Basic:
                    wave =  new BasicWave(currentWave, objectManager, player);
                    break;
                case WaveType.Special:
                    wave =  new SpecialWave(currentWave, objectManager, player);
                    break;
            }
            return wave;
        }
    }
}